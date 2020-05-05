using ODMSCommon.Security;
using ODMSModel.SparePartCountryVatRatio;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using System.Web.Mvc;
using ODMSModel.ViewModel;

namespace ODMSData
{
    public class SparePartCountryVatRatioData : DataAccessBase
    {
        public List<SparePartCountryVatRatioListModel> ListSparePartCountryVatRatio(UserInfo user,SparePartCountryVatRatioListModel filter, out int totalCount)
        {
            var retVal = new List<SparePartCountryVatRatioListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_COUNTRY_VAT_RATIO");

                db.AddInParameter(cmd, "ID_PART", DbType.Int64, filter.IdPart);
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, filter.IdCountry);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, filter.IsActive);

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
                        var sparePartCountryVatRatioListModel = new SparePartCountryVatRatioListModel
                            {
                                IdPart = reader["ID_PART"].GetValue<Int64>(),
                                IdCountry = reader["ID_COUNTRY"].GetValue<Int32>(),
                                PartCode = reader["PART_CODE"].GetValue<string>(),
                                PartName = reader["PART_NAME"].GetValue<string>(),
                                CountryName = reader["COUNTRY_NAME"].GetValue<string>(),
                                VatRatio = reader["VAT_RATIO"].GetValue<decimal>(),
                                IsActive = reader["IS_ACTIVE"].GetValue<int>(),
                                IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>()
                            };

                        retVal.Add(sparePartCountryVatRatioListModel);
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

        public SparePartCountryVatRatioViewModel GetSparePartCountryVatRatio(UserInfo user,SparePartCountryVatRatioViewModel sparePartCountryVatRatioModel)
        {
            AutocompleteSearchViewModel autoPartSearch = new AutocompleteSearchViewModel();

            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART_COUNTRY_VAT_RATIO");
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(sparePartCountryVatRatioModel.IdPart));
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, MakeDbNull(sparePartCountryVatRatioModel.IdCountry));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    sparePartCountryVatRatioModel.VatRatio = dReader["VAT_RATIO"].GetValue<decimal>();
                    sparePartCountryVatRatioModel.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    sparePartCountryVatRatioModel.PartName = dReader["PART_NAME"].GetValue<string>();
                    sparePartCountryVatRatioModel.IdPart = dReader["ID_PART"].GetValue<int>();
                    sparePartCountryVatRatioModel.CountryName = dReader["COUNTRY_NAME"].GetValue<string>();

                    autoPartSearch.DefaultValue = dReader["ID_PART"].GetValue<string>();
                    autoPartSearch.DefaultText = dReader["PART_NAME"].GetValue<string>();
                    sparePartCountryVatRatioModel.PartSearch = autoPartSearch;
                }
                if (dReader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(dReader);
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


            return sparePartCountryVatRatioModel;
        }

        public void DMLSparePartCountryVatRatio(UserInfo user,SparePartCountryVatRatioViewModel filter)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_COUNTRY_VAT_RATIO");
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, filter.IdPart);
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, filter.IdCountry);
                db.AddInParameter(cmd, "VAT_RATIO", DbType.Decimal, filter.VatRatio);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);

                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, filter.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "OPERATING_DATE", DbType.Date, MakeDbNull(DateTime.Now));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                filter.IdPart = db.GetParameterValue(cmd, "ID_PART").GetValue<long>();
                filter.IdCountry = db.GetParameterValue(cmd, "ID_COUNTRY").GetValue<int>();
                filter.VatRatio = db.GetParameterValue(cmd, "VAT_RATIO").GetValue<decimal>();
                filter.IsActive = db.GetParameterValue(cmd, "IS_ACTIVE").GetValue<bool>();

                filter.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                filter.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (filter.ErrorNo > 0)
                    filter.ErrorMessage = ResolveDatabaseErrorXml(filter.ErrorMessage);
                else
                {
                    var modelAS = new AutocompleteSearchViewModel
                        {
                            DefaultText = filter.PartName,
                            DefaultValue = filter.IdPart.ToString()
                        };

                    filter.PartSearch = modelAS;
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

        public List<SelectListItem> ListCountryNameAsSelectListItem(UserInfo user)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_COUNTRY_NAME_VAT_RATIO_COMBO");
               db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                            {
                                Value = reader["ID_COUNTRY"].GetValue<string>(),
                                Text = reader["COUNTRY_NAME"].GetValue<string>()
                            };
                        retVal.Add(lookupItem);
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
    }
}
