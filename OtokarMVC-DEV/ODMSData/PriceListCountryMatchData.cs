using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;
using ODMSCommon;
using System;
using ODMSCommon.Security;
using ODMSModel.PriceListCountryMatch;

namespace ODMSData
{
    public class PriceListCountryMatchData:DataAccessBase
    {
        public List<SelectListItem> GetPriceList()
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_PRICE_LIST_MST_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_PRICE_LIST"].GetValue<string>(),
                            Text = reader["DESCRIPTION"].GetValue<string>()
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

        public List<SelectListItem> GetCountriesIncluded(UserInfo user,int priceListId)
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_PRICE_LIST_COUNTRIES_INCLUDED");
                db.AddInParameter(cmd, "ID_PRICE_LIST",DbType.Int32,priceListId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_COUNTRY"].GetValue<string>(),
                            Text = reader["COUNTRY_NAME"].GetValue<string>(),
                            Selected = reader["IS_DEFAULT"].GetValue<bool>()
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

        public List<SelectListItem> GetCountriesExcluded(UserInfo user,int priceListId)
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_PRICE_LIST_COUNTRIES_EXCLUDED");
                db.AddInParameter(cmd, "ID_PRICE_LIST", DbType.Int32, priceListId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_COUNTRY"].GetValue<string>(),
                            Text = reader["COUNTRY_NAME"].GetValue<string>(),
                            Selected =false 

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

        public void Save(UserInfo user, PriceListCountryMatchSaveModel model)
        {
            //generationg data parameter string 
            //see Stored procedure P_SAVE_SPARE_PART_PRICE_LIST_COUNTRY for more info

            var sb = new StringBuilder();
            string data = string.Empty;
            if (model.CountryList != null && model.CountryList.Count > 0)
            {
                model.CountryList.ForEach(c => sb.Append(c).Append("@@"));
                data = sb.ToString();
                data = data.Substring(0, data.Length - 2);//remove unnecessary @@
            }
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SAVE_SPARE_PART_PRICE_LIST_COUNTRY");
                db.AddInParameter(cmd, "ID_PRICE_LIST", DbType.Int32, MakeDbNull(model.PriceListId));
                db.AddInParameter(cmd, "DATA", DbType.String, MakeDbNull(data));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    model.CountryList= new List<string>();
                    while (reader.Read())
                    {
                        model.CountryList.Add(reader["COUNTRY_NAME"].GetValue<string>());
                    }
                    reader.Close();
                }

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
