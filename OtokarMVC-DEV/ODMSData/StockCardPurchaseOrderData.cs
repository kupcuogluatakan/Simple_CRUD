using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.StockCardPurchaseOrder;
using ODMSCommon.Security;

namespace ODMSData
{
    public class StockCardPurchaseOrderData : DataAccessBase
    {
        public List<StockCardPurchaseOrderListModel> ListStockCardPurchaseOrder(UserInfo user, StockCardPurchaseOrderListModel hModel, out int totalCount, out int errorCode, out string errorDesc)
        {
            var listModel = new List<StockCardPurchaseOrderListModel>();
            errorDesc = "";
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_STOCK_CARD_PURCHASE_ORDER");

                db.AddInParameter(cmd, "PO_NUMBER", DbType.Int64, MakeDbNull(hModel.PoNumber));
                db.AddInParameter(cmd, "STATUS", DbType.Int64, hModel.StatusId);
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(hModel.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(hModel.EndDate));
                db.AddInParameter(cmd, "DEALER_ID", DbType.String, MakeDbNull(hModel.DealerId));
                db.AddInParameter(cmd, "PART_ID", DbType.String, MakeDbNull(hModel.PartId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, hModel.SortColumn);
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, hModel.SortDirection);
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, hModel.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, hModel.PageSize);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new StockCardPurchaseOrderListModel
                        {
                            CreateDate = dr["CREATE_DATE"].GetValue<DateTime>(),
                            OrderQuantity = dr["ORDER_QUANT"].GetValue<string>(),
                            OrderType = dr["PURCHASE_ORDER_TYPE"].GetValue<string>(),
                            PoNumber = dr["PO_NUMBER"].GetValue<Int64>(),
                            ShipQuantity = dr["SHIP_QUANT"].GetValue<string>(),
                            Status = dr["STATUS"].GetValue<string>(),
                            Supplier = dr["SUPPLIER"].GetValue<string>(),
                            StockType = dr["MAINT_NAME"].ToString()
                        };

                        listModel.Add(model);
                    }
                    dr.Close();
                }
                listModel.ForEach(c => c.CreateDateS = c.CreateDate.ToString("dd/MM/yyyy").Replace(CommonValues.Dot, CommonValues.Slash));
                totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                errorCode = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (errorCode > 0)
                    errorDesc = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return listModel;
        }
    }
}
