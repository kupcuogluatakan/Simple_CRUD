using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.DealerPurchaseOrder;

namespace ODMSData
{
    public class DealerPurchaseOrderData : DataAccessBase
    {

        public void DMLDealerPurchaseOrder(UserInfo user, DealerPurchaseOrderViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_DEALER_PURCHASE_ORDER");

                db.AddParameter(cmd, "PO_NUMBER", DbType.Int64, ParameterDirection.InputOutput, "PO_NUMBER", DataRowVersion.Default, model.PurchaseOrderId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, (model.DealerId <= 0 ? user.GetUserDealerId() : model.DealerId));
                db.AddInParameter(cmd, "SUPPLIER_ID", DbType.Int32, model.SupplierId);
                db.AddInParameter(cmd, "PAY_VIA_CENTER", DbType.Boolean, model.IsViaCenter);
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, model.VehicleId);
                db.AddParameter(cmd, "PO_TYPE", DbType.Int32, ParameterDirection.InputOutput, "PO_TYPE", DataRowVersion.Default, model.PurchaseOrderType);
                db.AddInParameter(cmd, "SHIPMENT_DATE", DbType.DateTime, model.ShipDate);
                db.AddInParameter(cmd, "ORDER_DATE", DbType.DateTime, model.OrderDate);
                db.AddInParameter(cmd, "STATUS", DbType.Int32, model.PurchaseStatus);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.PurchaseOrderId = db.GetParameterValue(cmd, "PO_NUMBER").GetValue<int>();
                model.PurchaseOrderType = db.GetParameterValue(cmd, "PO_TYPE").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());

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

        public void GetDealerPurchaseOrder(UserInfo user, DealerPurchaseOrderViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_DEALER_PURCHASE_ORDER");
                db.AddInParameter(cmd, "PO_NUMBER", DbType.String, filter.PurchaseOrderId);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, filter.DealerId <= 0 ? user.GetUserDealerId() : filter.DealerId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            filter.DealerId = dr["ID_DEALER"].GetValue<int>();
                            filter.SupplierId = dr["SUPPLIER_ID_DEALER"].GetValue<int>();
                            filter.VehicleName = dr["VIN_NO"].GetValue<string>();
                            filter.IsViaCenter = dr["PAYVIACENTER_LOOKVAL"].GetValue<int>().GetValue<bool>();
                            filter.VehicleId = dr["ID_VEHICLE"].GetValue<int>();
                            filter.ShipDate = dr["PO_DESIRED_SHIP_DATE"].GetValue<DateTime?>();
                            filter.PurchaseOrderType = dr["ID_PO_TYPE"].GetValue<int>();
                            filter.PurchaseStatus = dr["STATUS_LOOKVAL"].GetValue<int>();

                        }
                    }
                    else
                    {
                        filter.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                        if (filter.ErrorNo > 0)
                            filter.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());
                    }
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

        public void GetPartDetails(UserInfo user, DealerPurchaseOrderViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_DEALER_SALE_SPAREPART");

                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.DealerId));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.PartId = dr["ID_PART"].GetValue<int>();
                        filter.DiscountRatio = dr["DISCOUNT_RATIO"].GetValue<decimal>();
                        filter.DiscountPrice = dr["DISCOUNT_PRICE"].GetValue<decimal>();
                        filter.PartName = dr["PART_NAME"].GetValue<string>();
                        filter.ListPrice = dr["LIST_PRICE"].GetValue<decimal>();

                    }
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

        public void DMLDealerPurchasePart(UserInfo user, DealerPurchaseOrderViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_DEALER_ORDER_SPARE_PART");

                db.AddInParameter(cmd, "PO_NUMBER", DbType.Int64, model.PurchaseOrderId);
                db.AddInParameter(cmd, "PO_DET_SEQ_NO", DbType.Int64, model.PoDetSeqNo);
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, model.PartId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, model.Quantity);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());

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

        public List<ODMSModel.DealerSaleSparepart.DealerSaleSparepartListModel> ListDealerOrderPart(UserInfo user,ODMSModel.DealerSaleSparepart.DealerSaleSparepartListModel filter, out int totalCount)
        {
            var listModel = new List<ODMSModel.DealerSaleSparepart.DealerSaleSparepartListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_DEALER_ORDER_PART");

                db.AddInParameter(cmd, "PO_NUMBER", DbType.Int64, filter.PurchaseOrderId);
                AddPagingParametersWithLanguage(user, cmd, filter);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new ODMSModel.DealerSaleSparepart.DealerSaleSparepartListModel
                        {
                            PODetSeqNo = dr["PO_DET_SEQ_NO"].GetValue<int>(),
                            Quantity = dr["DESIRE_QUANT"].GetValue<int>(),
                            ListPrice = dr["LIST_PRICE"].GetValue<decimal>(),
                            PartName = dr["PART_NAME"].ToString(),
                            PartCode = dr["PART_CODE"].ToString(),
                            DiscountRatio = dr["DISCOUNT_RATIO"].GetValue<decimal>(),
                            DiscountPrice = dr["DISCOUNT_PRICE"].GetValue<decimal>(),
                            TotalPrice = dr["TOTAL_PRICE"].GetValue<decimal>()
                        };

                        listModel.Add(model);
                    }
                    dr.Close();
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

            return listModel;
        }



        public int GetVehicleMustByPOType(int poType)
        {
            int rValue = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_VEHICLE_SELECTION_BY_PO_TYPE");
                db.AddInParameter(cmd, "PO_TYPE_ID", DbType.Int32, poType);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        rValue = dr["IS_VEHICLE_SELECTION_MUST"].GetValue<int>();
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
            return rValue;
        }
    }
}
