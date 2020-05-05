using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.AppointmentDetailsParts;
using ODMSModel.ViewModel;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class AppointmentDetailsPartsData : DataAccessBase
    {
        private const string sp_getAppointDetailsPList = "P_LIST_APPOINTMENT_INDICATOR_PARTS";
        private const string sp_getAppointDetailsP = "P_GET_APPOINTMENT_INDICATOR_PARTS";
        private const string sp_dmlAppointDetailsP = "P_DML_APPOINTMENT_INDICATOR_PARTS";

        public List<AppointmentDetailsPartsListModel> GetAppointmentDPList(UserInfo user,AppointmentDetailsPartsListModel filter, out int totalCount)
        {
            List<AppointmentDetailsPartsListModel> list_AppDPModel = new List<AppointmentDetailsPartsListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getAppointDetailsPList);
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_ID", DbType.Int32, filter.AppointIndicId);
                AddPagingParametersWithLanguage(user, cmd, filter);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new AppointmentDetailsPartsListModel
                        {
                            Id = dr["APPOINTMENT_INDICATOR_CONTENT_ID"].GetValue<int>(),
                            AppointIndicId = dr["APPOINTMENT_INDICATOR_ID"].GetValue<int>(),
                            Quantity = dr["QUANTITY"].GetValue<string>(),
                            ListPrice = dr["LIST_PRICE"].GetValue<string>() + " " + dr["CURRENCY_CODE"].GetValue<string>(),
                            CurrencyCode = dr["CURRENCY_CODE"].GetValue<string>(),
                            PartName = dr["PART_NAME"].GetValue<string>()
                        };
                        list_AppDPModel.Add(model);
                    }
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
            return list_AppDPModel;
        }

        public void DMLAppointmentDetailsParts(UserInfo user, AppointmentDetailsPartsViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_dmlAppointDetailsP);
                db.AddParameter(cmd, "APPOINTMENT_INDICATOR_CONTENT_ID", DbType.Int32, ParameterDirection.InputOutput, "APPOINTMENT_INDICATOR_CONTENT_ID", DataRowVersion.Default, model.Id);
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_ID", DbType.Int32, model.AppointIndicId);
                db.AddInParameter(cmd, "PART_ID", DbType.Decimal, model.PartId);
                db.AddInParameter(cmd, "QUANTITY", DbType.Double, model.Quantity);
                db.AddOutParameter(cmd, "PART_NAME", DbType.String, 200);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.Id = db.GetParameterValue(cmd, "APPOINTMENT_INDICATOR_CONTENT_ID").GetValue<int>();
                model.PartName = db.GetParameterValue(cmd, "PART_NAME").GetValue<string>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                }
                else
                {
                    var modelAS = new AutocompleteSearchViewModel
                    {
                        DefaultText = model.PartName,
                        DefaultValue = model.PartId.ToString()
                    };

                    model.PartSearch = modelAS;
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

        public void GetAppointment(UserInfo user, AppointmentDetailsPartsViewModel filter)
        {
            AutocompleteSearchViewModel autoPartSearch = new AutocompleteSearchViewModel();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getAppointDetailsP);
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_CONTENT_ID", DbType.Int32, filter.Id);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.Id = dr["APPOINTMENT_INDICATOR_CONTENT_ID"].GetValue<int>();
                        filter.AppointIndicId = dr["APPOINTMENT_INDICATOR_ID"].GetValue<int>();
                        filter.MainCat = dr["MAIN_CAT"].GetValue<string>();
                        filter.Cat = dr["CAT"].GetValue<string>();
                        filter.SubCat = dr["SUB_CAT"].GetValue<string>();
                        filter.GroupList = filter.MainCat + "   /   " + filter.Cat +
                                                     "   /   " + filter.SubCat;
                        filter.ListPrice = dr["LIST_PRICE"].GetValue<string>();
                        filter.Quantity = dr["QUANTITY"].GetValue<decimal>();
                        filter.PartName = dr["PART_NAME"].GetValue<string>();
                        filter.PartId = dr["ID_PART"].GetValue<long>();
                        autoPartSearch.DefaultValue = dr["ID_PART"].GetValue<string>();
                        autoPartSearch.DefaultText = dr["PART_NAME"].GetValue<string>();

                        filter.PartSearch = autoPartSearch;
                    }
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

        public void GetAppIndicType(AppointmentDetailsPartsViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("GET_APPOINTMENT_INDICATOR_TYPE_BY_ID");
                db.AddInParameter(cmd, "APP_INDIC_ID", DbType.Int64, filter.AppointIndicId);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.IndicType = dr["INDICATOR_TYPE_CODE"].GetValue<string>();
                    }
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
    }
}
