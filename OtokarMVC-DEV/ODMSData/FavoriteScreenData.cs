using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Common;
using ODMSModel.FavoriteScreen;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSData
{
    public class FavoriteScreenData : DataAccessBase
    {
        public List<MenuListModel> ListAllScreen(UserInfo user)
        {
            var retVal = new List<MenuListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_ALL_SCREEN");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, MakeDbNull(user.UserId));
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var menuListModel = new MenuListModel
                        {
                            MenuId = reader["MENU_ID"].GetValue<int>(),
                            MenuText = reader["MENU_TEXT"].GetValue<string>()
                        };
                        retVal.Add(menuListModel);
                    }
                    reader.Close();
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
            return retVal;
        }
        public List<MenuListModel> ListFavoriteScreen(UserInfo user)
        {
            var retVal = new List<MenuListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_FAVORITE_SCREEN");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, MakeDbNull(user.UserId));
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var menuListModel = new MenuListModel
                        {
                            MenuId = reader["MENU_ID"].GetValue<int>(),
                            MenuText = reader["MENU_TEXT"].GetValue<string>()
                        };
                        retVal.Add(menuListModel);
                    }
                    reader.Close();
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
            return retVal;
        }
        public MenuInfo GetUserFavoriteMenu(UserInfo user, int userId)
        {
            System.Data.Common.DbDataReader dReader = null;
            var menuInfo = new MenuInfo { MenuItems = new List<MenuItemInfo>() };
            try
            {

                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_FAVORITE_SCREEN");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, MakeDbNull(userId));
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    var menuItemInfo = new MenuItemInfo
                    {
                        MenuItemId = dReader["MENU_ID"].GetValue<int>(),
                        MenuItemParentId = 4000,
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

        public void Save(UserInfo user,SaveModel model)
        {
            if (!model.ScreenIdList.Any())
                return;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SAVE_FAVORITE_SCREEN_RELATIONS");
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "@SCREEN_IDS", DbType.String, model.SerializedScreenIds);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
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
    }

}
