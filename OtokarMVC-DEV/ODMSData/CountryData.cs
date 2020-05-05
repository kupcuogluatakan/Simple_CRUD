using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.CountryVatRatio;
using System;

namespace ODMSData
{
    public class CountryData : DataAccessBase
    {
        public List<CountryListModel> ListCountry(string languageCode)
        {
            var listModels = new List<CountryListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_COUNTRY_ALL");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, languageCode);
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var newModel = new CountryListModel
                        {
                            CountryId = dr["ID_COUNTRY"].GetValue<int>(),
                            CountryName = dr["COUNTRY_NAME"].GetValue<string>(),
                            CityId = dr["ID_CITY"].GetValue<int>(),
                            CityName = dr["CITY_NAME"].GetValue<string>(),
                            TownId = dr["ID_TOWN"].GetValue<int>(),
                            TownName = dr["TOWN_NAME"].GetValue<string>()
                        };

                        listModels.Add(newModel);
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

            return listModels;
        }

        public CountryListModel ListCountryLastVersion()
        {
            var retVal = new CountryListModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_COUNTRY_LAST_VERSION");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dealerListModel = new CountryListModel
                        {
                            CountryLastUpdate = reader["COUNTRY_LAST_UPDATE"].GetValue<int>(),
                            CityLastUpdate = reader["CITY_LAST_UPDATE"].GetValue<int>(),
                            TownLastUpdate = reader["TOWN_LAST_UPDATE"].GetValue<int>()
                        };
                        retVal = dealerListModel;
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
