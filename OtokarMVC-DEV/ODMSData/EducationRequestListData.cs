using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon.Security;
using ODMSModel.EducationRequests;
using ODMSCommon;
namespace ODMSData
{
    public class EducationRequestListData : DataAccessBase
    {
        public List<EducationRequestsListModel> GetEducationRequests(UserInfo user, EducationRequestsListModel filter, out int totalCnt)
        {
            var retVal = new List<EducationRequestsListModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_EDUCATION_REQUESTS");
                db.AddInParameter(cmd, "EDUCATION_CODE", DbType.String, MakeDbNull(filter.EducationCode));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
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
                        var item = new EducationRequestsListModel
                        {
                            CustomerName = reader["CUSTOMER_NAME"].GetValue<string>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            EducationCode = filter.EducationCode,
                            EducationRequestId = reader["ID_EDUCATION_REQUEST"].GetValue<int>(),
                            RequestTime = reader["CREATE_DATE"].GetValue<DateTime>(),
                            TCIdentityNo = reader["TC_IDENTITY_NO"].GetValue<string>()
                        };
                        retVal.Add(item);
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

            return retVal;
        }
    }
}
