using System.Globalization;
using ODMSCommon.Security;
using ODMSModel.PurchaseOrderSuggestion;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSData
{
    public class PurchaseOrderSuggestionData : DataAccessBase
    {
        public List<PurchaseOrderSuggestionListModel> ListPurchaseOrderSuggestion(UserInfo user, PurchaseOrderSuggestionListModel filter, out int totalCount)
        {
            var retVal = new List<PurchaseOrderSuggestionListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PURCHASE_ORDER_SUGGESTION");
                db.AddInParameter(cmd, "MRP_ID", DbType.Int32, MakeDbNull(filter.IdMrp));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, filter.IdDealer);
                db.AddInParameter(cmd, "IS_AUTO", DbType.Boolean, filter.IsAuto);
                db.AddInParameter(cmd, "PLAN_DATE_START", DbType.DateTime, MakeDbNull(filter.PlanDateStart));
                db.AddInParameter(cmd, "PLAN_DATE_END", DbType.DateTime, MakeDbNull(filter.PlanDateEnd));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, filter.IsActive);
                db.AddInParameter(cmd, "PO_NUMBER", DbType.Int64, filter.PoNumber);
                db.AddInParameter(cmd, "IS_PO_CREATE", DbType.Boolean, filter.IsPoCreate);

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
                        var purchaseOrderSuggestionListModel = new PurchaseOrderSuggestionListModel
                        {
                            IsAuto = reader["IS_AUTO"].GetValue<bool>(),
                            IdMrp = reader["ID_MRP"].GetValue<Int64>(),
                            IsAutoName = reader["IS_AUTO_NAME"].GetValue<string>(),
                            PlanDate = reader["PLAN_DATE"].GetValue<DateTime?>(),
                            PoDate = reader["PO_DATE"].GetValue<DateTime?>(),
                            PoNumber = reader["PO_NUMBER"].GetValue<long?>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>(),
                            PurchaseOrderRate = reader["ORDER_RATE"].GetValue<decimal>(),
                            IsPoCreate = (reader["PO_DATE"].GetValue<DateTime?>() == null) ? MessageResource.Global_Display_No : MessageResource.Global_Display_Yes
                        };

                        retVal.Add(purchaseOrderSuggestionListModel);
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

        public void PurchaseOrderSuggest(UserInfo user, PurchaseOrderSuggestionViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("SP_CRT_MRP");
                cmd.CommandTimeout = 1200;
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "IS_AUTO", DbType.Boolean, 0);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.String,
                    user.UserId.ToString(CultureInfo.InvariantCulture));
                db.AddOutParameter(cmd, "MRP_ID", DbType.Int64, 0);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo == 1)
                    model.ErrorMessage = MessageResource.PurchaseOrderSuggestion_Warning_NoDataFound;
                else if (model.ErrorNo > 1)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
            }
            catch (Exception ex)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.PurchaseOrderSuggest_Warning_UnexpectedError;
            }
            finally
            {
                CloseConnection();
            }
        }

    }
}
