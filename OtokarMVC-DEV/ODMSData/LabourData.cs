using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Labour;
using ODMSModel.ListModel;

namespace ODMSData
{
    public class LabourData : DataAccessBase
    {
        private const string sp_getLabourList = "P_LIST_LABOURS";
        private const string sp_getLabourMainGrpCombo = "P_LIST_LABOUR_MAIN_GROUPS_COMBO";
        private const string sp_getLabourSubGrpCombo = "P_LIST_LABOUR_SUB_GROUPS_COMBO";
        private const string sp_dmlLabour = "P_DML_LABOUR_MAIN";
        private const string sp_getLabour = "P_GET_LABOUR";
        public List<LabourListModel> GetLabourList(UserInfo user,LabourListModel labourModel, out int totalCount)
        {
            List<LabourListModel> list_LabourM = new List<LabourListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getLabourList);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, labourModel.IsActiveSearch);
                db.AddInParameter(cmd, "LABOUR_CODE", DbType.String, MakeDbNull(labourModel.LabourCode));
                db.AddInParameter(cmd, "REPAIR_CODE", DbType.String, MakeDbNull(labourModel.RepairCode));
                db.AddInParameter(cmd, "LABOUR_SSID", DbType.String, MakeDbNull(labourModel.LabourSSID));
                db.AddInParameter(cmd, "LABOUR_NAME", DbType.String, MakeDbNull(labourModel.LabourName));
                db.AddInParameter(cmd, "LABOUR_SUB_GROUP_ID", DbType.Int32, MakeDbNull(labourModel.LabourSubGroupId));
                db.AddInParameter(cmd, "LABOUR_MAIN_GROUP_ID", DbType.Int32, MakeDbNull(labourModel.LabourMainGroupId));
                db.AddInParameter(cmd, "LABOUR_TYPE_ID", DbType.Int32, MakeDbNull(labourModel.LabourTypeId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(labourModel.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(labourModel.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, labourModel.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(labourModel.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        LabourListModel model = new LabourListModel
                        {
                            LabourId = dr["ID_LABOUR"].GetValue<int>(),
                            LabourMainGrp = dr["LABOUR_GROUP_DESC"].GetValue<string>(),
                            LabourSubGrp = dr["LABOUR_SUBGROUP_DESC"].GetValue<string>(),
                            LabourCode = dr["LABOUR_CODE"].GetValue<string>(),
                            LabourSSID = dr["LABOUR_SSID"].GetValue<string>(),
                            LabourName = dr["LABOUR_NAME"].GetValue<string>(),
                            IsActiveS = dr["IS_ACTIVE_STRING"].GetValue<string>(),
                            Description = dr["ADMIN_DESC"].GetValue<string>(),
                            RepairCode = dr["REPAIR_CODE"].GetValue<string>(),
                            LabourType = dr["LABOUR_TYPE_NAME"].GetValue<string>(),
                            LabourTypeId = dr["LABOUR_TYPE_ID"].GetValue<int>()
                        };

                        list_LabourM.Add(model);
                    }
                    dr.Close();
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

            return list_LabourM;
        }

        public List<LabourAndLabourDetListModel> GetLabourListForExcel(UserInfo user)
        {
            List<LabourAndLabourDetListModel> list_LabourM = new List<LabourAndLabourDetListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_LABOR_AND_LABOUR_DET");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));


                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        LabourAndLabourDetListModel model = new LabourAndLabourDetListModel
                        {
                            LABOUR_CODE = dr["LABOUR_CODE"].ToString(),
                            LABOUR_NAME = dr["LABOUR_NAME"].ToString(),
                            LABOUR_SSID = dr["LABOUR_SSID"].ToString(),
                            LABOUR_GROUP_DESC = dr["LABOUR_GROUP_DESC"].ToString(),
                            LABOUR_SUBGROUP_DESC = dr["LABOUR_SUBGROUP_DESC"].ToString(),
                            LABOUR_REPAIR_CODE = dr["LABOUR_REPAIR_CODE"].ToString(),
                            LABOUR_TYPE_NAME = dr["LABOUR_TYPE_NAME"].ToString(),
                            MODEL_KOD = dr["MODEL_KOD"].ToString(),
                            DURATION = dr["DURATION"].ToString(),
                            IS_ACTIVE = dr["IS_ACTIVE"].ToString(),
                            MODEL_NAME = dr["MODEL_NAME"].ToString(),
                            TYPE_NAME = dr["TYPE_NAME"].ToString(),
                            ENGINE_TYPE = dr["ENGINE_TYPE"].ToString(),
                            IS_ACTIVE_STRING = dr["IS_ACTIVE_STRING"].ToString()
                        };

                        list_LabourM.Add(model);
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

            return list_LabourM;
        }

        public List<SelectListItem> ListLabourAsSelectList(UserInfo user)
        {
            List<SelectListItem> listLabour = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getLabourList);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, true);
                db.AddInParameter(cmd, "LABOUR_CODE", DbType.String, null);
                db.AddInParameter(cmd, "LABOUR_NAME", DbType.String, null);
                db.AddInParameter(cmd, "LABOUR_SUB_GROUP_ID", DbType.String, null);
                db.AddInParameter(cmd, "LABOUR_MAIN_GROUP_ID", DbType.Int32, null);
                db.AddInParameter(cmd, "LABOUR_TYPE_ID", DbType.Int32, null);
                db.AddInParameter(cmd, "REPAIR_CODE", DbType.String, null);
                db.AddInParameter(cmd, "LABOUR_SSID", DbType.String, null);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, null);
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, null);
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, 0);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, DBNull.Value);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SelectListItem item = new SelectListItem
                        {
                            Value = dr["ID_LABOUR"].GetValue<string>(),
                            Text = dr["LABOUR_NAME"].GetValue<string>()
                        };

                        listLabour.Add(item);
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

            return listLabour;
        }

        public List<SelectListItem> ListMainGrpAsSelectList(UserInfo user)
        {
            List<SelectListItem> list_MainGrp = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getLabourMainGrpCombo);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SelectListItem item = new SelectListItem
                        {
                            Value = dr["ID_MAIN_GROUP"].GetValue<string>(),
                            // Text =  dr["LABOUR_GROUP_DESC"].GetValue<string>() + " ( " + dr["ID_MAIN_GROUP"].GetValue<string>() + " )"
                            //Otokar isteğine göre tekrar düzenliyorum => taner
                            Text = string.Format("{0} - {1}", dr["ID_MAIN_GROUP"].GetValue<string>(), dr["LABOUR_GROUP_DESC"].GetValue<string>())
                        };

                        list_MainGrp.Add(item);
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

            return list_MainGrp;
        }

        public List<SelectListItem> ListSubGrpAsSelectList(UserInfo user,int? id)
        {
            List<SelectListItem> list_SubGrp = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getLabourSubGrpCombo);

                db.AddInParameter(cmd, "LABOUR_MAIN_GROUP_ID", DbType.Int32, MakeDbNull(id));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SelectListItem item = new SelectListItem
                        {
                            Value = dr["ID_SUB_GROUP"].GetValue<string>(),
                            Text = string.Format("{0} - {1}", dr["ID_SUB_GROUP"].GetValue<string>(), dr["LABOUR_SUBGROUP_DESC"].GetValue<string>())
                        };
                        list_SubGrp.Add(item);
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

            return list_SubGrp;
        }

        public void DMLLabour(UserInfo user,LabourViewModel labourModel)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_dmlLabour);

                db.AddParameter(cmd, "LABOUR_ID", DbType.Int32, ParameterDirection.InputOutput, "LABOUR_ID", DataRowVersion.Default, labourModel.LabourId);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, labourModel.AdminDesc);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, labourModel.CommandType);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, labourModel.IsActive);
                db.AddInParameter(cmd, "ID_LABOUR_TYPE", DbType.Int32, labourModel.LabourTypeId);
                db.AddInParameter(cmd, "IS_EXTERNAL_LABOUR", DbType.Int32, labourModel.IsExternal);
                db.AddInParameter(cmd, "IS_DEALER_DURATION", DbType.Int32, labourModel.IsDealerDuration);
                db.AddInParameter(cmd, "LABOUR_CODE", DbType.String, labourModel.LabourCode);
                db.AddInParameter(cmd, "MAIN_GROUP_ID", DbType.Int32, labourModel.LabourMainGroupId);
                db.AddInParameter(cmd, "SUB_GROUP_ID", DbType.Int32, labourModel.LabourSubGroupId);
                db.AddInParameter(cmd, "LABOUR_SSID", DbType.String, labourModel.LabourSSID);
                db.AddInParameter(cmd, "REPAIR_CODE", DbType.String, labourModel.RepairCode);
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, labourModel.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                labourModel.LabourId = db.GetParameterValue(cmd, "LABOUR_ID").GetValue<int>();
                labourModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                labourModel.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (labourModel.ErrorNo > 0)
                {
                    labourModel.ErrorMessage = ResolveDatabaseErrorXml(labourModel.ErrorMessage);
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
        }

        public void GetLabour(UserInfo user,LabourViewModel labourModel)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getLabour);

                db.AddInParameter(cmd, "LABOUR_ID", DbType.Int32, MakeDbNull(labourModel.LabourId));
                db.AddInParameter(cmd, "LABOUR_CODE", DbType.String, MakeDbNull(labourModel.LabourCode));
                db.AddInParameter(cmd, "LABOUR_SSID", DbType.String, MakeDbNull(labourModel.LabourSSID));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        labourModel.LabourId = dr["ID_LABOUR"].GetValue<int>();
                        labourModel.AdminDesc = dr["ADMIN_DESC"].GetValue<string>();
                        labourModel.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                        labourModel.IsDealerDuration = dr["DEALER_DURATION_CHCK"].GetValue<bool>();
                        labourModel.LabourCode = dr["LABOUR_CODE"].GetValue<string>();
                        labourModel.LabourName.txtSelectedLanguageCode = dr["LABOUR_NAME"].GetValue<string>();
                        labourModel.RepairCode = dr["REPAIR_CODE"].GetValue<string>();
                        labourModel.LabourMainGroupName = dr["LABOUR_GROUP_DESC"].GetValue<string>();
                        labourModel.LabourMainGroupId = dr["ID_MAIN_GROUP"].GetValue<int>();
                        labourModel.LabourSubGroupName = dr["LABOUR_SUBGROUP_DESC"].GetValue<string>();
                        labourModel.LabourSubGroupId = dr["ID_SUB_GROUP"].GetValue<int>();
                        labourModel.LabourSSID = dr["LABOUR_SSID"].GetValue<string>();
                        labourModel.IsExternal = dr["IS_EXTERNAL_LABOUR"].GetValue<bool>();
                        labourModel.LabourTypeId = dr["ID_LABOUR_TYPE"].GetValue<int>();
                        labourModel.LabourType = dr["LABOUR_PRICE_TYPE_DESC"].GetValue<string>();
                    }
                    if (dr.NextResult())
                    {
                        var table = new DataTable();
                        table.Load(dr);
                        labourModel.LabourName.MultiLanguageContentAsText = labourModel.MultiLanguageContentAsText
                                                                          = GetLanguageContentFromDataSet(table, "LABOUR_TYPE_DESC");
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


        }

        public List<AutocompleteSearchListModel> ListLabourNamesAsAutoCompleteSearch(UserInfo user,string strSearch, string extParam)
        {
            List<AutocompleteSearchListModel> list_ACSearchModel = new List<AutocompleteSearchListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_LABOURS_AUTOSEARCH");
                db.AddInParameter(cmd, "VALUE", DbType.String, strSearch);
                db.AddInParameter(cmd, "APPOINTMENT_ID", DbType.String, MakeDbNull(extParam));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AutocompleteSearchListModel model = new AutocompleteSearchListModel
                        {
                            Column1 = dr["LABOUR_TYPE_DESC"].GetValue<string>(),
                            Id = dr["ID_LABOUR"].GetValue<int>()
                        };

                        list_ACSearchModel.Add(model);
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
            return list_ACSearchModel;
        }

        public List<AutocompleteSearchListModel> ListWorkOrderLabourNameAsAutoCompleteSearch(UserInfo user,string strSearch, string ExtraParameter)
        {
            var list_ACSearchModel = new List<AutocompleteSearchListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_LABOURS_FOR_WORK_ORDER_DETAIL_AUTO_COMPLETE");
                db.AddInParameter(cmd, "SEARCH_TEXT", DbType.String, strSearch);
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.String, ExtraParameter);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new AutocompleteSearchListModel
                        {
                            Column1 = dr["LABOUR_TYPE_DESC"].GetValue<string>(),
                            Id = dr["ID_LABOUR"].GetValue<int>()
                        };

                        list_ACSearchModel.Add(model);
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
            return list_ACSearchModel;
        }
    }
}
