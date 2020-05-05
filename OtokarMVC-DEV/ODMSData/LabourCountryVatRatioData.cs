using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.LabourCountryVatRatio;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class LabourCountryVatRatioData : DataAccessBase
    {
        public void DMLLabourCountryVatRatio(UserInfo user, LabourCountryVatRatioViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_LABOUR_COUNTRY_VAT_RATIO");
                db.AddInParameter(cmd, "LABOUR_ID", DbType.Int32, MakeDbNull(model.LabourId));
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, MakeDbNull(model.CountryId));
                db.AddInParameter(cmd, "VAT_RATIO", DbType.Decimal, MakeDbNull(model.VatRatio));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, MakeDbNull(model.CommandType));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
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

        public List<LabourCountryVatRatioListModel> ListLabourCountryVatRatios(UserInfo user, LabourCountryVatRatioListModel filter, out int totalCnt)
        {
            var list = new List<LabourCountryVatRatioListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_LABOUR_COUNTRY_VAT_RATIO");
                db.AddInParameter(cmd, "LABOUR_ID", DbType.Int32, MakeDbNull(filter.LabourId));
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, MakeDbNull(filter.CountryId));
                db.AddInParameter(cmd, "VAT_RATIO", DbType.Decimal, MakeDbNull(filter.VatRatio));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(filter.SearchIsActive));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
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
                        var item = new LabourCountryVatRatioListModel
                        {
                            VatRatio = reader["VAT_RATIO"].GetValue<decimal>(),
                            CountryId = reader["ID_COUNTRY"].GetValue<int>(),
                            LabourId = reader["ID_LABOUR"].GetValue<int>(),
                            LabourName = reader["LABOUR_TYPE_DESC"].GetValue<string>(),
                            CountryName = reader["COUNTRY_NAME"].GetValue<string>(),
                            IsActiveString = reader["IS_ACTIVE_STRING"].GetValue<string>()
                        };
                        list.Add(item);
                    }
                    reader.Close();
                }
                totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return list;
        }

        public LabourCountryVatRatioViewModel GetLabourCountryVatRatio(UserInfo user, int countryId, int labourId)
        {
            var model = new LabourCountryVatRatioViewModel();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_LABOUR_COUNTRY_VAT_RATIO");
                db.AddInParameter(cmd, "LABOUR_ID", DbType.Int32, MakeDbNull(labourId));
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, MakeDbNull(countryId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        model.VatRatio = reader["VAT_RATIO"].GetValue<decimal>();
                        model.CountryId = countryId;
                        model.LabourId = labourId;
                        model.LabourName = reader["LABOUR_TYPE_DESC"].GetValue<string>();
                        model.CountryName = reader["COUNTRY_NAME"].GetValue<string>();
                        model.CountryName = reader["COUNTRY_NAME"].GetValue<string>();
                        model.IsActive = reader["IS_ACTIVE"].GetValue<bool>();
                        model.IsActiveString = reader["IS_ACTIVE_STRING"].GetValue<string>();

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
            return model;
        }


        public List<SelectListItem> ListLaboursBySubGroup(UserInfo user, int subGroupId)
        {
            var list = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_LABOUR_BY_SUB_GROUP_COMBO");
                db.AddInParameter(cmd, "LABOUR_SUB_GROUP_ID", DbType.Int32, subGroupId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Text = reader["LABOUR_TYPE_DESC"].GetValue<string>(),
                            Value = reader["ID_LABOUR"].GetValue<string>()
                        };
                        list.Add(item);
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

            return list;
        }
    }
}
