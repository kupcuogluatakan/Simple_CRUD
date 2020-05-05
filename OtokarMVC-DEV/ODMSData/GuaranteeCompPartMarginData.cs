using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.GuaranteeCompPartMargin;
using ODMSCommon.Security;

namespace ODMSData
{
    public class GuaranteeCompPartMarginData : DataAccessBase
    {
        public List<GuaranteeCompPartMarginListModel> ListGuaranteeCompPartMargin(UserInfo user, GuaranteeCompPartMarginListModel filter, out int totalCnt)
        {
            var listModels = new List<GuaranteeCompPartMarginListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_GUARANTEE_COMP_PART_MARGIN");

                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, MakeDbNull(filter.CountryId));
                db.AddInParameter(cmd, "MAX_PRICE", DbType.Int32, MakeDbNull(filter.MaxPrice));
                db.AddInParameter(cmd, "GRNT_RATIO", DbType.Decimal, MakeDbNull(filter.GrntRatio));
                db.AddInParameter(cmd, "GRNT_PRICE", DbType.Decimal, MakeDbNull(filter.GrntPrice));
                db.AddInParameter(cmd, "CURRENCY_NAME", DbType.String, MakeDbNull(filter.CurrencyName));
                db.AddInParameter(cmd, "CURRENCY_CODE", DbType.String, MakeDbNull(filter.CurrencyCode));
                db.AddInParameter(cmd, "COUNTRY_NAME", DbType.String, MakeDbNull(filter.CountryName));
                db.AddInParameter(cmd, "SHORT_CODE", DbType.String, MakeDbNull(filter.ShortCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var newModel = new GuaranteeCompPartMarginListModel
                        {
                            IdGrntPartMrgn = dr["ID_GRNT_PART_MRGN"].GetValue<int>(),
                            CountryId = dr["ID_COUNTRY"].GetValue<int>()
                        };


                        if (!String.IsNullOrEmpty(dr["MAX_PRICE"].ToString()))
                        {
                            newModel.MaxPrice = dr["MAX_PRICE"].GetValue<int>();
                            newModel.MaxPriceStr = Convert.ToString(dr["MAX_PRICE"].GetValue<int>());
                        }
                        else
                        {
                            newModel.MaxPrice = null;
                            newModel.MaxPriceStr = CommonValues.Minus;
                        }

                        if (!String.IsNullOrEmpty(dr["GRNT_RATIO"].ToString()))
                            newModel.GrntRatio = dr["GRNT_RATIO"].GetValue<decimal>();
                        else
                            newModel.GrntRatio = null;

                        if (!String.IsNullOrEmpty(dr["GRNT_PRICE"].ToString()))
                            newModel.GrntPrice = dr["GRNT_PRICE"].GetValue<decimal>();
                        else
                            newModel.GrntPrice = null;


                        newModel.CreateUser = dr["CREATE_USER"].GetValue<string>();
                        newModel.CreateDate = dr["CREATE_DATE"].GetValue<DateTime>();
                        newModel.UpdateUser = dr["UPDATE_USER"].GetValue<string>();
                        newModel.UpdateDate = dr["UPDATE_DATE"].GetValue<DateTime>();
                        newModel.CurrencyName = dr["CURRENCY_NAME"].GetValue<string>();
                        newModel.CurrencyCode = dr["CURRENCY_CODE"].GetValue<string>();
                        newModel.CountryName = dr["COUNTRY_NAME"].GetValue<string>();
                        newModel.ShortCode = dr["SHORT_CODE"].GetValue<string>();
                        newModel.GrntPriceAndCurrencyCode = Convert.ToString(dr["GRNT_PRICE"].GetValue<decimal>()) + CommonValues.EmptySpace + dr["CURRENCY_CODE"].GetValue<string>();

                        listModels.Add(newModel);
                    }
                    dr.Close();
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

            return listModels;
        }

        /// <summary>
        /// Muadil Parça Mar Oranı tanım tablosuna ait insert/update işlemleri bu metot ile yapılır.
        /// </summary>
        /// <param name="model"></param>
        public void DMLGuaranteeCompPartMargin(UserInfo user, GuaranteeCompPartMarginViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_GUARANTEE_COMP_PART_MARGIN");
                db.AddParameter(cmd, "ID_GRNT_PART_MRGN", DbType.Int32, ParameterDirection.InputOutput, "ID_GRNT_PART_MRGN", DataRowVersion.Default, model.IdGrntPartMrgn);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, model.CountryId);
                db.AddInParameter(cmd, "MAX_PRICE", DbType.Int32, MakeDbNull(model.MaxPrice));
                db.AddInParameter(cmd, "GRNT_RATIO", DbType.Decimal, MakeDbNull(model.GrntRatio));
                db.AddInParameter(cmd, "GRNT_PRICE", DbType.Decimal, MakeDbNull(model.GrntPrice));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.IdGrntPartMrgn = db.GetParameterValue(cmd, "ID_GRNT_PART_MRGN").GetValue<int>();
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

        public GuaranteeCompPartMarginViewModel GetGuaranteeCompPartMarginById(UserInfo user, int guaranteeCompPartMarginId)
        {
            System.Data.Common.DbDataReader dReader = null;
            var guaranteeCompPartMarginModel = new GuaranteeCompPartMarginViewModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_GUARANTEE_COMP_PART_MARGIN");
                db.AddInParameter(cmd, "ID_GRNT_PART_MRGN", DbType.Int32, MakeDbNull(guaranteeCompPartMarginId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (dReader = cmd.ExecuteReader())
                {
                    while (dReader.Read())
                    {
                        guaranteeCompPartMarginModel.IdGrntPartMrgn = dReader["ID_GRNT_PART_MRGN"].GetValue<int>();
                        guaranteeCompPartMarginModel.CountryId = dReader["ID_COUNTRY"].GetValue<int>();

                        if (!String.IsNullOrEmpty(dReader["MAX_PRICE"].ToString()))
                            guaranteeCompPartMarginModel.MaxPrice = dReader["MAX_PRICE"].GetValue<int>();
                        else
                            guaranteeCompPartMarginModel.MaxPrice = null;

                        if (!String.IsNullOrEmpty(dReader["GRNT_RATIO"].ToString()))
                            guaranteeCompPartMarginModel.GrntRatio = dReader["GRNT_RATIO"].GetValue<decimal>();
                        else
                            guaranteeCompPartMarginModel.GrntRatio = null;

                        if (!String.IsNullOrEmpty(dReader["GRNT_PRICE"].ToString()))
                            guaranteeCompPartMarginModel.GrntPrice = dReader["GRNT_PRICE"].GetValue<decimal>();
                        else
                            guaranteeCompPartMarginModel.GrntPrice = null;

                        guaranteeCompPartMarginModel.CreateUser = dReader["CREATE_USER"].GetValue<string>();
                        guaranteeCompPartMarginModel.CreateDate = dReader["CREATE_DATE"].GetValue<DateTime>();
                        guaranteeCompPartMarginModel.UpdateUser = dReader["UPDATE_USER"].GetValue<string>();
                        guaranteeCompPartMarginModel.UpdateDate = dReader["UPDATE_DATE"].GetValue<DateTime>();
                        guaranteeCompPartMarginModel.CurrencyName = dReader["CURRENCY_NAME"].GetValue<string>();

                        guaranteeCompPartMarginModel.CurrencyCode = dReader["CURRENCY_CODE"].GetValue<string>();
                        guaranteeCompPartMarginModel.CountryName = dReader["COUNTRY_NAME"].GetValue<string>();
                    }
                    dReader.Close();
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


            return guaranteeCompPartMarginModel;
        }

    }
}
