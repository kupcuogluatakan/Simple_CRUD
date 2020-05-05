using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.ViewModel;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class LanguageData : DataAccessBase
    {
        public List<SelectListItem> ListLanguageAsSelectListItem(UserInfo user)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LANGUAGES");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                            {
                                Value = reader["LANGUAGE_CODE"].GetValue<string>(),
                                Text = reader["LANGUAGE_NAME"].GetValue<string>()
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
        public List<LanguageViewModel> GetLanguages(UserInfo user)
        {
            var retVal = new List<LanguageViewModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LANGUAGES");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);              
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var languageViewModel = new LanguageViewModel
                            {
                                LanguageCode = reader["LANGUAGE_CODE"] != null
                                                   ? reader["LANGUAGE_CODE"].ToString()
                                                   : string.Empty,
                                LanguageName = reader["LANGUAGE_NAME"] != null
                                                   ? reader["LANGUAGE_NAME"].ToString()
                                                   : string.Empty
                            };
                        retVal.Add(languageViewModel);
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
