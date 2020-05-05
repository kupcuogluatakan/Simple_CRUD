using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData.Extentions;
using ODMSModel;
using ODMSModel.Delivery;
using ODMSModel.DeliveryListPart;

namespace ODMSData
{
    public class DeliveryData : DataAccessBase
    {
        public List<DeliveryListModel> ListDelivery(UserInfo user, DeliveryListModel filter, out int totalCnt)
        {
            var retVal = new List<DeliveryListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DELIVERY_MST");
                db.AddInParameter(cmd, "WAYBILL_NO", DbType.String, MakeDbNull(filter.WayBillNo));
                db.AddInParameter(cmd, "WAYBILL_DATE", DbType.DateTime, MakeDbNull(filter.WayBillDate));
                db.AddInParameter(cmd, "ID_SUPPLIER", DbType.Int64, MakeDbNull(filter.SupplierId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int64, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "STATUS", DbType.Int64, MakeDbNull(filter.Status));
                db.AddOutParameter(cmd, "CURRENCY", DbType.String, 3);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new DeliveryListModel();
                        listModel.DeliveryId = reader["ID_DELIVERY"].GetValue<long>();
                        listModel.WayBillNo = reader["VAYBILL_NO"].ToString();
                        listModel.WayBillDate = reader["WAYBILL_DATE"].GetValue<DateTime?>();
                        listModel.SupplierName = reader["SUPPLIER_NAME"].ToString();
                        listModel.Status = reader["STATUS_NAME"].ToString();
                        listModel.StatusId = int.Parse(reader["STATUS_LOOKVAL"].ToString());
                        listModel.TotalPrice = reader["TOTAL_PRICE"].GetValue<decimal>();
                        listModel.IsPlaced = reader["IS_PLACED"].GetValue<bool>();
                        listModel.SupplierId = reader["ID_SUPPLIER"].GetValue<int>();
                        retVal.Add(listModel);
                    }
                    reader.Close();
                }

                string currency = db.GetParameterValue(cmd, "CURRENCY").ToString();
                retVal.ForEach(
                    c =>
                        c.TotalPriceString =
                            string.Format(CultureInfo.CurrentCulture, "{0} {1}", c.TotalPrice.RoundUp(2).ToString("N2"), currency));
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

        public void DMLDelivery(UserInfo user, DeliveryCreateModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DELIVERY_MST",
                    MakeDbNull(model.DeliveryId),
                    MakeDbNull(string.IsNullOrEmpty(model.PurchaseNo) ? "" : model.PurchaseNo),
                    MakeDbNull(user.GetUserDealerId()),
                    MakeDbNull(model.SupplierId),
                    MakeDbNull(model.SapDeliveryNo),
                    MakeDbNull(model.WayBillNo),
                    MakeDbNull(model.WayBillDate),
                    MakeDbNull(model.CommandType),
                    MakeDbNull(user.UserId),
                    null, null, null,
                    MakeDbNull(model.InvoiceNo),
                    MakeDbNull(model.InvoiceSerialNo),
                    MakeDbNull(model.InvoiceDate)
                    );
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.DeliveryId = db.GetParameterValue(cmd, "ID_DELIVERY").GetValue<long>();
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

        public void DMLDeliveryAndDetails(UserInfo user, DeliveryCreateModel model, List<DeliveryListPartSubViewModel> filter)
        {
            bool isSuccessfull = true;
            CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("P_DML_DELIVERY_MST");
            CreateConnection(cmd);
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                #region Delivery MST
                cmd.Parameters.Clear();
                db.AddParameter(cmd, "ID_DELIVERY", DbType.Int32, ParameterDirection.InputOutput, "ID_DELIVERY", DataRowVersion.Default, model.DeliveryId);
                db.AddInParameter(cmd, "PURCHASE_DET", DbType.String, String.IsNullOrEmpty(model.PurchaseNo)
                                                                 ? ""
                                                                 : model.PurchaseNo);
                db.AddInParameter(cmd, "ID_DEALER ", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "ID_SUPPLIER ", DbType.Int32, MakeDbNull(model.SupplierId));
                db.AddInParameter(cmd, "SAP_DELIVERY_NO ", DbType.String, MakeDbNull(model.SapDeliveryNo));
                db.AddInParameter(cmd, "WAYBILL_NO ", DbType.String, MakeDbNull(model.WayBillNo));
                db.AddInParameter(cmd, "WAYBILL_DATE ", DbType.DateTime, MakeDbNull(model.WayBillDate));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "ID_STATUS", DbType.Int32, model.StatusId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                db.ExecuteNonQuery(cmd, transaction);
                model.DeliveryId = db.GetParameterValue(cmd, "ID_DELIVERY").GetValue<long>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage =
                        ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());
                    isSuccessfull = false;
                }

                #endregion

                #region Delivery DET
                cmd = db.GetStoredProcCommand("P_DML_DELIVERY_DET");
                foreach (DeliveryListPartSubViewModel detailModel in filter)
                {
                    cmd.Parameters.Clear();
                    detailModel.DeliveryId = model.DeliveryId;
                    db.AddInParameter(cmd, "DELIVERY_SEQ_NO", DbType.Int32, detailModel.DeliverySeqNo);
                    db.AddInParameter(cmd, "ID_DELIVERY", DbType.Int32, detailModel.DeliveryId);
                    db.AddInParameter(cmd, "ID_PART", DbType.Int32, detailModel.PartId);
                    db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, detailModel.StockTypeId);
                    db.AddInParameter(cmd, "PO_DET_SEQ_NO", DbType.Int32, detailModel.PoDetSeqNo);
                    db.AddInParameter(cmd, "SAP_OFFER_NO", DbType.String, detailModel.SapOfferNo);
                    db.AddInParameter(cmd, "SAP_ROW_NO", DbType.String, detailModel.SapRowNo);
                    db.AddInParameter(cmd, "SAP_ORIGINAL_ROW_NO", DbType.Int32, detailModel.SapOriginalRowNo);
                    db.AddInParameter(cmd, "SHIP_QUANT", DbType.Decimal, detailModel.ShipQnty);
                    db.AddInParameter(cmd, "RECEIVED_QUANT", DbType.Decimal, detailModel.ReceiveQnty);
                    db.AddInParameter(cmd, "INVOICE_PRICE", DbType.Decimal, detailModel.InvoicePrice);
                    db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, MakeDbNull(detailModel.CommandType));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                    db.ExecuteNonQuery(cmd, transaction);

                    detailModel.DeliverySeqNo = db.GetParameterValue(cmd, "DELIVERY_SEQ_NO").GetValue<long>();
                    detailModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    detailModel.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    if (detailModel.ErrorNo > 0)
                    {
                        isSuccessfull = false;
                        detailModel.ErrorMessage = ResolveDatabaseErrorXml(detailModel.ErrorMessage);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                model.ErrorNo = 1;
                isSuccessfull = false;
                model.ErrorMessage = ex.Message;
            }
            finally
            {
                if (isSuccessfull)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
                CloseConnection();
            }
        }

        public DeliveryViewModel GetDelivery(UserInfo user, long id)
        {
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<DeliveryViewModel>.MapAllProperties().ExcludeBaseModelProperties(typeof(ModelBase)).Build();
                return db.ExecuteSprocAccessor("P_GET_DELIVERY_MST", rowMapper, id, MakeDbNull(user.LanguageCode)).SingleOrDefault();
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

        public ModelBase CancelDelivery(UserInfo user, long deliveryId)
        {
            var model = new ModelBase { ErrorNo = 0 };
            DbTransaction transaction = null;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_CANCEL_DELIVERY_MST",
                    deliveryId,
                    MakeDbNull(user.UserId)
                    );
                CreateConnection(cmd);
                transaction = connection.BeginTransaction();
                using (transaction)
                {

                    db.ExecuteNonQuery(cmd, transaction);
                    transaction.Commit();
                }


            }
            catch (Exception ex)
            {
                var innerex = ex as SqlException;
                model.ErrorMessage = innerex != null
                    ? (innerex.Number == 50000
                        ? CommonUtility.GetResourceValue(innerex.Message)
                        : MessageResource.Err_Generic_Unexpected)
                    : MessageResource.Err_Generic_Unexpected;
                model.ErrorNo = 1;
                if (transaction != null)
                {
                    transaction.Rollback();
                }
            }
            finally
            {
                CloseConnection();
            }
            return model;
        }

        public DeliveryCreateModel CheckDeteleItem(UserInfo user, DeliveryCreateModel model)
        {

            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_CHECK_DELETE_ITEM_DELIVERY");
                db.AddInParameter(cmd, "WAYBILL_NO", DbType.String, MakeDbNull(model.WayBillNo));
                db.AddInParameter(cmd, "SUPPLIER_ID", DbType.Int64, MakeDbNull(model.SupplierId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int64, MakeDbNull(user.GetUserDealerId()));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        model.HasDeleteItem = true;
                        model.DeliveryId = reader["ID_DELIVERY"].GetValue<long>();
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

            return model;
        }

        public void DeleteDeliveryItem(DeliveryCreateModel model)
        {

            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DELETE_ITEM_DELIVERY", MakeDbNull(model.DeliveryId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();
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

        public bool Exists(long supplierId, string wayBillNo, int dealerId)
        {
            bool exists = false;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_EXISTS_DELIVERY");
                db.AddInParameter(cmd, "SUPPLIER_ID", DbType.Int32, supplierId);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, dealerId);
                db.AddInParameter(cmd, "WAYBILL_NO", DbType.String, wayBillNo);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        exists = reader[0].GetValue<bool>();
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

            return exists;
        }
    }
}
