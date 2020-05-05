using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.DealerPurchaseOrderPartConfirm;
using ODMSModel.PurchaseOrder;
using ODMSModel.PurchaseOrderDetail;

namespace ODMSData
{
    public class DealerPurchaseOrderPartConfirmData : DataAccessBase
    {
        public List<DealerPurchaseOrderPartConfirmListModel> ListDealerPurchaseOrderPartConfirms(UserInfo user,DealerPurchaseOrderPartConfirmListModel filter, out int total)
        {
            var retVal = new List<DealerPurchaseOrderPartConfirmListModel>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_PURCHASE_ORDER_PART_CONFIRM");
                db.AddInParameter(cmd, "PO_NUMBER", DbType.String, MakeDbNull(filter.PoNumber));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dealerListModel = new DealerPurchaseOrderPartConfirmListModel
                        {
                            OrderQuantity = reader["ORDER_QUANTITY"].GetValue<decimal>(),
                            PartCodeName = reader["PART_CODE_NAME"].ToString(),
                            PartId = reader["PART_ID"].GetValue<int>(),
                            PoNumber = reader["PO_NUMBER"].GetValue<long>(),
                            PurchaseOrderDetailSeqNo = reader["PURCHASE_ORDER_DETAIL_SEQ_NO"].GetValue<long>(),
                            ShipmentQuantity = reader["SHIPMENT_QUANTITY"].GetValue<decimal>(),
                            StockQuantity = reader["STOCK_QUANTITY"].GetValue<decimal>(),
                            StatusId = reader["STATUS_ID"].GetValue<int>(),
                            StatusName = reader["STATUS_NAME"].ToString(),
                            SupplierDealerId = reader["SUPPLIER_ID_DEALER"].GetValue<int>(),
                            DealerId = reader["ID_DEALER"].GetValue<int>()
                        };
                        retVal.Add(dealerListModel);
                    }
                    reader.Close();
                }
                total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public void DMLDealerPurchaseOrderPartConfirm(UserInfo user, PurchaseOrderViewModel model, List<PurchaseOrderDetailViewModel> filter)
        {
            string errorMessage = string.Empty;
            bool isSuccess = true;
            CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER_DET");
            CreateConnection(cmd);
            DbTransaction transaction = connection.BeginTransaction();
            try
            {
                #region Purchase Order Detail DML
                foreach (PurchaseOrderDetailViewModel orderDetailModel in filter)
                {
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, orderDetailModel.CommandType);
                    db.AddInParameter(cmd, "PO_DET_SEQ_NO", DbType.Int32, MakeDbNull(orderDetailModel.PurchaseOrderDetailSeqNo));
                    db.AddInParameter(cmd, "PO_NUMBER", DbType.Int32, MakeDbNull(orderDetailModel.PurchaseOrderNumber));
                    db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(orderDetailModel.PartId));
                    db.AddInParameter(cmd, "PACKAGE_QUANTITY", DbType.Decimal, MakeDbNull(orderDetailModel.PackageQuantity));
                    db.AddInParameter(cmd, "DESIRE_QUANTITY", DbType.Decimal, MakeDbNull(orderDetailModel.DesireQuantity));
                    db.AddInParameter(cmd, "ORDER_QUANTITY", DbType.Decimal, orderDetailModel.OrderQuantity);
                    db.AddInParameter(cmd, "SHIPMENT_QUANTITY", DbType.Decimal, MakeDbNull(orderDetailModel.ShipmentQuantity));
                    db.AddInParameter(cmd, "ORDER_PRICE", DbType.Decimal, orderDetailModel.OrderPrice);
                    db.AddInParameter(cmd, "DESIRE_DELIVERY_DATE", DbType.DateTime, MakeDbNull(orderDetailModel.DesireDeliveryDate));
                    db.AddInParameter(cmd, "STATUS_ID", DbType.Int32, orderDetailModel.StatusId);
                    db.AddInParameter(cmd, "DENY_REASON", DbType.String, orderDetailModel.DenyReason);
                    db.AddInParameter(cmd, "SAP_SHIP_ID_PART", DbType.String, MakeDbNull(orderDetailModel.SAPShipIdPart));
                    db.AddInParameter(cmd, "CURRENCY_CODE", DbType.String, MakeDbNull(orderDetailModel.CurrencyCode));
                    db.AddInParameter(cmd, "LIST_PRICE", DbType.Decimal, orderDetailModel.ListPrice);
                    db.AddInParameter(cmd, "LIST_DISCOUNT_RATIO", DbType.Decimal, orderDetailModel.ListDiscountRatio);
                    db.AddInParameter(cmd, "CONFIRM_PRICE", DbType.Decimal, orderDetailModel.ConfirmPrice);
                    db.AddInParameter(cmd, "APPLIED_DISCOUNT_RATIO", DbType.Decimal, orderDetailModel.AppliedDiscountRatio);
                    db.AddInParameter(cmd, "SPECIAL_EXPLANATION", DbType.String, orderDetailModel.SpecialExplanation);

                    db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                    db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                    db.ExecuteNonQuery(cmd, transaction);
                    orderDetailModel.PurchaseOrderNumber = db.GetParameterValue(cmd, "PO_NUMBER").GetValue<int>();
                }
                #endregion
            }
            catch (Exception Ex)
            {
                string dbErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();
                isSuccess = false;
                errorMessage = string.IsNullOrEmpty(dbErrorMessage) ? Ex.Message : ResolveDatabaseErrorXml(dbErrorMessage);
            }
            finally
            {
                if (isSuccess)
                {
                    transaction.Commit();
                }
                else
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = errorMessage;
                    transaction.Rollback();
                }
                CloseConnection();
            }
        }

    }
}

