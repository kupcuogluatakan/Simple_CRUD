using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon.Security;
using ODMSModel.ListModel;
using ODMSModel.Menu;
using ODMSModel.ViewModel;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSData
{
    public class MenuData : DataAccessBase
    {
        public List<MenuListModel> ListMenu(UserInfo user, MenuListModel filter, out int totalCount)
        {
            var retVal = new List<MenuListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_MENU");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "MENU_TEXT", DbType.String, filter.MenuText);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var menuListModel = new MenuListModel
                        {
                            MenuId = reader["MENU_ID"].GetValue<int>(),
                            PermissionId = reader["PERMISSION_ID"].GetValue<int?>(),
                            LinkName = reader["LINK_NAME"].GetValue<string>(),
                            MenuText = reader["MENU_TEXT"].GetValue<string>(),
                            OrderNo = reader["ORDER_NO"].GetValue<int>(),
                            Status = reader["STATUS"].GetValue<string>()
                        };
                        retVal.Add(menuListModel);
                    }
                    reader.Close();
                }
                totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                CloseConnection();
            }

            return retVal;
        }

        public MenuIndexViewModel GetMenu(UserInfo user, MenuIndexViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_MENU");
                db.AddInParameter(cmd, "MENU_ID", DbType.Int32, MakeDbNull(filter.MenuId));
                db.AddInParameter(cmd, "CONTROLLER", DbType.String, MakeDbNull(filter.Controller));
                db.AddInParameter(cmd, "ACTION", DbType.String, MakeDbNull(filter.Action));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(((user == null) ? "TR" : user.LanguageCode)));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.LinkName = dReader["LINK_NAME"].GetValue<string>();
                    filter.OrderNo = dReader["ORDER_NO"].GetValue<int>();
                    filter.PermissionId = dReader["PERMISSION_ID"].GetValue<int?>();
                    filter.StatusId = (CommonValues.Status)dReader["STATUS_ID"].GetValue<int>();
                    filter.Status = dReader["STATUS"].GetValue<string>();
                    filter.MenuId = dReader["MENU_ID"].GetValue<int>();
                }
                if (dReader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(dReader);
                    filter.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table, "MENU_TEXT");
                    filter.MenuText = (MultiLanguageModel)CommonUtility.DeepClone(filter.MenuText);
                    filter.MenuText.MultiLanguageContentAsText = filter.MultiLanguageContentAsText;
                }

                dReader.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dReader != null && !dReader.IsClosed)
                {
                    dReader.Close();
                    dReader.Dispose();
                }
                CloseConnection();
            }
            return filter;
        }


        public void DMLMenu(UserInfo user, MenuIndexViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_MENU_MAIN");
                db.AddParameter(cmd, "MENU_ID", DbType.Int32, ParameterDirection.InputOutput, "MENU_ID", DataRowVersion.Default, model.MenuId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ORDER_NO", DbType.Int32, MakeDbNull(model.OrderNo));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, MakeDbNull(model.MultiLanguageContentAsText));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.MenuId = db.GetParameterValue(cmd, "MENU_ID").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.MainMenu_Error_NullId;
                else if (model.ErrorNo == 1)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public MenuInfo GetUserMenu(UserInfo user, int userId)
        {
            System.Data.Common.DbDataReader dReader = null;
            var menuInfo = new MenuInfo { MenuItems = new List<MenuItemInfo>() };
            try
            {

                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_USER_MENU");
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, MakeDbNull(userId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    var menuItemInfo = new MenuItemInfo
                    {
                        MenuItemId = dReader["MENU_ID"].GetValue<int>(),
                        MenuItemParentId = dReader["PARENT_MENU_ID"].GetValue<int?>(),
                        ControllerName = dReader["CONTROLLER_NAME"].GetValue<string>(),
                        ActionName = dReader["ACTION_NAME"].GetValue<string>(),
                        Text = dReader["MENU_TEXT"].GetValue<string>(),
                        OrderNo = dReader["ORDER_NO"].GetValue<int>()
                    };
                    menuInfo.MenuItems.Add(menuItemInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dReader != null && !dReader.IsClosed)
                {
                    dReader.Close();
                    dReader.Dispose();
                }
                CloseConnection();
            }
            return menuInfo;
        }

        public List<SelectListItem> GetDefinedSubMenuList(UserInfo user, int id)
        {
            var listItem = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_SUB_MENU_FROM_PARENT");

                db.AddInParameter(cmd, "MENU_ID", DbType.Int32, id);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem()
                        {
                            Text = dr["MENU_TEXT"].GetValue<string>(),
                            Value = dr["MENU_ID"].GetValue<string>()
                        };

                        listItem.Add(item);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return listItem;
        }

        public List<SelectListItem> GetSubMenuList(UserInfo user, int id)
        {
            var listItem = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_SUB_MENU");

                db.AddInParameter(cmd, "MENU_ID", DbType.Int32, id);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem()
                        {
                            Text = dr["MENU_TEXT"].GetValue<string>(),
                            Value = dr["MENU_ID"].GetValue<string>()
                        };

                        listItem.Add(item);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return listItem;
        }

        public void DMLMenuPlacement(UserInfo user, MenuPlacementViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_MENU_PLACEMENT");

                db.AddInParameter(cmd, "MENU_ID", DbType.Int32, model.MenuId);
                db.AddInParameter(cmd, "SUB_MENUS", DbType.String, model.SubMenuString);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, user.UserId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage =
                        ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
