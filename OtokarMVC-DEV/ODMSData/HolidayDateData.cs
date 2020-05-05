using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.HolidayDate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSData
{
    public class HolidayDateData : DataAccessBase
    {
        public List<HolidayDateListModel> ListHolidayDate(UserInfo user, HolidayDateListModel filter, out int totalCount)
        {
            var retVal = new List<HolidayDateListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_HOLIDAY_DATE");
                db.AddInParameter(cmd, "ID_HOLIDAY_DATE", DbType.Int32, MakeDbNull(filter.IdHolidayDate));
                db.AddInParameter(cmd, "HOLIDAY_DATE", DbType.Date, MakeDbNull(filter.HolidayDate));
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(filter.Description));
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.String, MakeDbNull(filter.IdCountry));
                //AddPagingParameters(cmd, filter);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var HolidayDateListModel = new HolidayDateListModel
                        {
                            IdHolidayDate = reader["ID_HOLIDAY_DATE"].GetValue<int>(),
                            HolidayDate = reader["HOLIDAY_DATE"].GetValue<DateTime>(),
                            Description = reader["DESCRIPTION"].GetValue<string>(),
                            IdCountry = reader["ID_COUNTRY"].GetValue<int>(),
                            LanguageCode = reader["LANGUAGE_CODE"].GetValue<string>()
                        };

                        retVal.Add(HolidayDateListModel);
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

        public void DMLHolidayDate(UserInfo user, HolidayDateViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_HOLIDAY_DATE");
                db.AddInParameter(cmd, "ID_HOLIDAY_DATE", DbType.Int32, model.IdHolidayDate);
                db.AddInParameter(cmd, "HOLIDAY_DATE", DbType.DateTime, model.HolidayDate);
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, model.Description);
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, model.IdCountry);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                //db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                //model.IdHolidayDate =Convert.ToInt32(db.GetParameterValue(cmd, "ID_HolidayDate").ToString());
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

        public HolidayDateViewModel GetHolidayDate(UserInfo user, HolidayDateViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_HOLIDAY_DATE");
                db.AddInParameter(cmd, "ID_HOLIDAY_DATE", DbType.Int32, MakeDbNull(filter.IdHolidayDate));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.IdHolidayDate = dReader["ID_HOLIDAY_DATE"].GetValue<int>();
                    filter.HolidayDate = dReader["HOLIDAY_DATE"].GetValue<DateTime>();
                    filter.Description = dReader["DESCRIPTION"].GetValue<string>();
                    filter.IdCountry = dReader["ID_COUNTRY"].GetValue<int>();
                    filter.LanguageCode = dReader["LANGUAGE_CODE"].GetValue<string>();
                }

                dReader.Close();
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
            return filter;
        }
    }
}
