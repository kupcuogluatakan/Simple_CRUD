using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.VatRatio;
using ODMSCommon.Security;

namespace ODMSData
{
    public class VatRatioData : DataAccessBase
    {
        public List<VatRatioListModel> ListVatRatios(UserInfo user, VatRatioListModel filter, out int totalCount)
        {
            var list = new List<VatRatioListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VAT_RATIO");
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
                        var item = new VatRatioListModel
                        {
                            VatRatio = reader["VAT_RATIO"].GetValue<decimal>(),
                            IsActive = reader["STATUS"].GetValue<bool>(),
                            IsActiveString = reader["IS_ACTIVE_STRING"].GetValue<string>(),
                            InvoiceLabel = reader["INVOICE_LABEL"].GetValue<string>()
                        };
                        list.Add(item);
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

            return list;
        }

        public void DMLVatRatio(UserInfo user, VatRatioModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_VAT_RATIO");
                db.AddInParameter(cmd, "VAT_RATIO", DbType.Decimal, MakeDbNull(model.VatRatio));
                db.AddInParameter(cmd, "INVOICE_LABEL", DbType.String, model.InvoiceLabel);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(model.IsActive));
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

        //TODO : Id set edilmeli
        public void DMLVatRatioExp(UserInfo user, VatRatioExpModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_VAT_RATIO_EXP");
                db.AddInParameter(cmd, "VAT_RATIO", DbType.Decimal, MakeDbNull(model.VatRatio));
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, MakeDbNull(model.CountryId));
                db.AddInParameter(cmd, "EXPLANATION", DbType.String, MakeDbNull(model.Explation));
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

        public VatRatioExpModel GetVatRatioExp(UserInfo user, decimal vatRatio, int countryId)
        {
            var model = new VatRatioExpModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_VAT_RATIO_EXPL");
                db.AddInParameter(cmd, "VAT_RATIO", DbType.Decimal, MakeDbNull(vatRatio));
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, MakeDbNull(countryId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.VatRatio = reader["VAT_RATIO"].GetValue<decimal>();
                        model.Country = reader["COUNTRY_NAME"].GetValue<string>();
                        model.CountryId = reader["ID_COUNTRY"].GetValue<int>();
                        model.Explation = reader["EXPLANATION"].GetValue<string>();
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

        public List<VatRatioExpListModel> ListVatRatioExps(UserInfo user, VatRatioExpListModel filter, out int totalCount)
        {
            var list = new List<VatRatioExpListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VAT_RATIO_EXPL");
                db.AddInParameter(cmd, "VAT_RATIO", DbType.Decimal, MakeDbNull(filter.VatRatio));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, 0);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, DBNull.Value);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new VatRatioExpListModel
                        {
                            VatRatio = reader["VAT_RATIO"].GetValue<decimal>(),
                            CountryId = reader["ID_COUNTRY"].GetValue<int>(),
                            Country = reader["COUNTRY_NAME"].GetValue<string>(),
                            Explanation = reader["EXPLANATION"].GetValue<string>()
                        };
                        list.Add(item);
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

            return list;
        }

        public List<SelectListItem> ListVatRatiosAsSelectList(int sparePartId, int countryId)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VAT_RATIO_FOR_SPARE_PART");
                db.AddInParameter(cmd, "SPARE_PART_ID", DbType.Int32, MakeDbNull(sparePartId));
                db.AddInParameter(cmd, "COUNTRY_ID", DbType.String, MakeDbNull(countryId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string spVatRatio = reader["SPARE_PART_VAT_RATIO"].GetValue<string>();
                        string cVatRatio = reader["COUNTRY_VAT_RATIO"].GetValue<string>();
                        SelectListItem listModel;
                        if (spVatRatio == string.Empty)
                        {
                            listModel = new SelectListItem
                            {
                                Value = cVatRatio,
                                Text = cVatRatio
                            };
                        }
                        else
                        {
                            listModel = new SelectListItem
                            {
                                Value = spVatRatio,
                                Text = spVatRatio
                            };
                        }
                        result.Add(listModel);
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
            return result;
        }

        public List<SelectListItem> ListLabelsAsSelectList()
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VAT_RATIO_LABELS_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string spVatRatio = reader["Text"].GetValue<string>();
                        string cVatRatio = reader["Value"].GetValue<string>();
                        SelectListItem listModel;
                        if (spVatRatio == string.Empty)
                        {
                            listModel = new SelectListItem
                            {
                                Value = cVatRatio,
                                Text = cVatRatio
                            };
                        }
                        else
                        {
                            listModel = new SelectListItem
                            {
                                Value = spVatRatio,
                                Text = spVatRatio
                            };
                        }
                        result.Add(listModel);
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
            return result;
        }
    }
}
