using System.Data.Common;
using ODMSCommon.Security;
using ODMSModel.DealerPurchaseOrderConfirm;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.SparePartSale;
using ODMSModel.SparePartSaleDetail;
using ODMSCommon.Resources;

namespace ODMSData
{
    public class DealerPurchaseOrderConfirmData : DataAccessBase
    {
        public List<DealerPurchaseOrderConfirmListModel> ListDealerPurchaseOrderConfirm(UserInfo user, DealerPurchaseOrderConfirmListModel filter, out int totalCount)
        {
            var retVal = new List<DealerPurchaseOrderConfirmListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_PURCHASE_ORDER_CONFIRM");
                db.AddInParameter(cmd, "ID_SUPPLIER", DbType.Int32, filter.IdSupplier);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, filter.IdDealer);
                db.AddInParameter(cmd, "STATUS", DbType.Int32, filter.StatusId);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var purchaseOrderListModel = new DealerPurchaseOrderConfirmListModel
                        {
                            PoNumber = reader["PO_NUMBER"].GetValue<Int64>(),
                            DealerName = reader["DEALER_NAME"].ToString(),
                            StatusName = reader["STATUS_NAME"].ToString(),
                            StatusId = reader["STATUS"].GetValue<int>(),
                            DesiredShipDate = reader["PO_DESIRED_SHIP_DATE"].GetValue<DateTime?>(),
                            SupplierDealerConfirm = reader["SUPPLIER_DEALER_CONFIRM"].GetValue<int>()
                        };

                        retVal.Add(purchaseOrderListModel);
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

        public int DMLDealerPurchaseOrderConfirmSave(UserInfo user, SparePartSaleViewModel model, List<SparePartSaleDetailDetailModel> filter)
        {
            int errorNo = 0;
            string errorMessage = String.Empty;
            string dbErrorMessage = String.Empty;
            bool isSuccess = true;
            CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_SALE");
            CreateConnection(cmd);
            DbTransaction transaction = connection.BeginTransaction();
            try
            {
                #region Spare Part Sale DML

                cmd.Parameters.Clear();
                db.AddParameter(cmd, "ID_PART_SALE", DbType.Int32, ParameterDirection.InputOutput, "ID_PART_SALE", DataRowVersion.Default, model.SparePartSaleId);
                db.AddInParameter(cmd, "CUSTOMER_TYPE_ID", DbType.Int32, model.CustomerTypeId);
                db.AddInParameter(cmd, "ID_DEALER ", DbType.Int32, model.DealerId);
                db.AddInParameter(cmd, "CUSTOMER_ID ", DbType.Int32, MakeDbNull(model.CustomerId));
                db.AddInParameter(cmd, "ID_SPARE_PART_SALE_WAYBILL", DbType.Int64, (model.CommandType == "I") ? null : model.SparePartSaleWaybillId);
                db.AddInParameter(cmd, "SALE_TYPE_ID", DbType.Int32, MakeDbNull(model.SaleTypeId));
                db.AddInParameter(cmd, "SALE_DATE ", DbType.DateTime, MakeDbNull(model.SaleDate));
                db.AddInParameter(cmd, "SALE_RESPONSIBLE ", DbType.String, MakeDbNull(model.SaleResponsible));
                db.AddInParameter(cmd, "SALE_STATUS_ID ", DbType.Int32, MakeDbNull(model.SaleStatusLookVal));
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(model.VehicleId));
                db.AddInParameter(cmd, "IS_RETURN ", DbType.Int32, MakeDbNull(model.IsReturn));
                db.AddInParameter(cmd, "PO_NUMBER ", DbType.Int64, MakeDbNull(model.PoNumber));
                db.AddInParameter(cmd, "ID_STOCK_TYPE ", DbType.Int32, MakeDbNull(model.StockTypeId));
                db.AddInParameter(cmd, "ID_PRICE_LIST ", DbType.Int32, MakeDbNull(model.PriceListId));
                db.AddInParameter(cmd, "VAT_EXCLUDE ", DbType.Int32, model.VatExclude);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                db.ExecuteNonQuery(cmd, transaction);
                model.SparePartSaleId = db.GetParameterValue(cmd, "ID_PART_SALE").GetValue<int>();

                #endregion

                #region Spare Part Sale Detail DML

                cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_SALE_DETAIL");
                foreach (SparePartSaleDetailDetailModel saleDetailModel in filter)
                {
                    cmd.Parameters.Clear();
                    saleDetailModel.PartSaleId = model.SparePartSaleId;
                    db.AddInParameter(cmd, "SPARE_PART_SALE_DETAIL_ID", DbType.Int32, saleDetailModel.SparePartSaleDetailId);
                    db.AddInParameter(cmd, "ID_PART_SALE", DbType.Int32, model.SparePartSaleId);
                    db.AddInParameter(cmd, "ID_PART", DbType.String, saleDetailModel.SparePartId);
                    db.AddInParameter(cmd, "CURRENCY_CODE", DbType.String, MakeDbNull(saleDetailModel.CurrencyCode));
                    db.AddInParameter(cmd, "LIST_PRICE", DbType.Double, MakeDbNull(saleDetailModel.ListPrice));
                    db.AddInParameter(cmd, "DEALER_PRICE", DbType.Double, saleDetailModel.DealerPrice);
                    db.AddInParameter(cmd, "DISCOUNT_PRICE", DbType.Double, MakeDbNull(saleDetailModel.DiscountPrice));
                    db.AddInParameter(cmd, "PLAN_QUANTITY", DbType.Decimal, saleDetailModel.PlanQuantity);
                    db.AddInParameter(cmd, "PICK_PLANNED_QUANTITY", DbType.Decimal, MakeDbNull(0));
                    db.AddInParameter(cmd, "PICKED_QUANTITY", DbType.Decimal, MakeDbNull(saleDetailModel.PickedQuantity));
                    db.AddInParameter(cmd, "SO_DET_SEQ_NO", DbType.Int64, MakeDbNull(saleDetailModel.SoDetSeqNo));
                    db.AddInParameter(cmd, "RETURNED_QUANTITY", DbType.Int64, MakeDbNull(saleDetailModel.ReturnedQuantity));
                    db.AddInParameter(cmd, "STATUS_ID", DbType.Int32, MakeDbNull(saleDetailModel.StatusId));
                    db.AddInParameter(cmd, "RETURN_REASON_TEXT", DbType.String, MakeDbNull(saleDetailModel.ReturnReasonText));
                    db.AddInParameter(cmd, "PRICE_LIST_DATE", DbType.DateTime, MakeDbNull(saleDetailModel.PriceListDate));
                    db.AddInParameter(cmd, "DELIVERY_SEQ_NO", DbType.Int64, MakeDbNull(saleDetailModel.DeliverySeqNo));
                    db.AddInParameter(cmd, "IS_PRICE_FIXED", DbType.Int32, MakeDbNull(saleDetailModel.IsPriceFixed));

                    db.AddInParameter(cmd, "DISCOUNT_RATIO", DbType.Double, MakeDbNull(saleDetailModel.DiscountRatio));
                    db.AddInParameter(cmd, "VAT_RATIO", DbType.Decimal, MakeDbNull(saleDetailModel.VatRatio));
                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, saleDetailModel.CommandType);
                    db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                    db.ExecuteNonQuery(cmd, transaction);

                }
                #endregion
            }
            catch (Exception Ex)
            {
                errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                dbErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();
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
                    if (errorNo == 2)
                    {
                        model.ErrorMessage = MessageResource.SparePartSaleDetail_Error_NullId;
                        model.ErrorNo = 1;
                    }
                    else if (errorNo == 1)
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = errorMessage;
                    }
                    transaction.Rollback();
                }
                CloseConnection();
            }

            return model.SparePartSaleId;
        }
        public void DMLDealerPurchaseOrderConfirm(UserInfo user, DealerPurchaseOrderConfirmViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DEALER_PURCHASE_ORDER_CONFIRM");
                db.AddInParameter(cmd, "PO_NUMBER", DbType.Int64, model.PoNumber);
                db.AddInParameter(cmd, "STATUS", DbType.Int32, model.StatusId);
                db.AddInParameter(cmd, "SUPPLIER_DEALER_CONFIRM", DbType.Int32, model.SupplierDealerConfirm);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "OPERATING_DATE", DbType.Date, MakeDbNull(DateTime.Now));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.PoNumber = Int64.Parse(db.GetParameterValue(cmd, "PO_NUMBER").ToString());

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
    }
}
