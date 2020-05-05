using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using ODMSCommon;
using ODMSData.DataContracts;
using ODMSModel.SupplierDispatchPart;
using ODMSCommon.Security;
using ODMSModel.Delivery;

namespace ODMSData
{
    public class SupplierDispatchPartData : DataAccessBase, ISupplierDispatchPart<SupplierDispatchPartViewModel>
    {
        public void Delete(UserInfo user, SupplierDispatchPartViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DELIVERY_DET");
                db.AddInParameter(cmd, "DELIVERY_SEQ_NO", DbType.Int32, model.DeliverySeqNo);
                db.AddInParameter(cmd, "ID_DELIVERY", DbType.Int32, DBNull.Value);
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, model.PartId);
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, 1);
                db.AddInParameter(cmd, "PO_DET_SEQ_NO", DbType.Int32, null);
                db.AddInParameter(cmd, "SAP_OFFER_NO", DbType.String, null);
                db.AddInParameter(cmd, "SAP_ROW_NO", DbType.String, null);
                db.AddInParameter(cmd, "SAP_ORIGINAL_ROW_NO", DbType.String, null);
                db.AddInParameter(cmd, "SHIP_QUANT", DbType.Decimal, model.Qty);
                db.AddInParameter(cmd, "RECEIVED_QUANT", DbType.Decimal, null);
                db.AddInParameter(cmd, "INVOICE_PRICE", DbType.Decimal, model.InvoicePrice);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.DeliverySeqNo = db.GetParameterValue(cmd, "DELIVERY_SEQ_NO").GetValue<int>();
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

        public SupplierDispatchPartViewModel Get(UserInfo user, SupplierDispatchPartViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DELIVERY_DET");
                db.AddInParameter(cmd, "DELIVERY_SEQ_NO", DbType.Int32, MakeDbNull(filter.DeliverySeqNo));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (dReader = cmd.ExecuteReader())
                {
                    if (dReader.Read())
                    {
                        filter.PartCode = dReader["PART_CODE"].ToString();
                        filter.Qty = dReader["SHIP_QUANT"].GetValue<decimal>();
                        filter.InvoicePrice = dReader["INVOICE_PRICE"].GetValue<decimal>();
                        filter.PartId = dReader["ID_PART"].GetValue<int>();
                    }
                    dReader.Close();
                }

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

        public void Insert(UserInfo user, SupplierDispatchPartViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DELIVERY_DET");
                db.AddInParameter(cmd, "DELIVERY_SEQ_NO", DbType.Int32, model.DeliverySeqNo);
                db.AddInParameter(cmd, "ID_DELIVERY", DbType.Int32, model.DeliveryId);
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, model.PartId);
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, 1);
                db.AddInParameter(cmd, "PO_DET_SEQ_NO", DbType.Int32, null);
                db.AddInParameter(cmd, "SAP_OFFER_NO", DbType.String, null);
                db.AddInParameter(cmd, "SAP_ROW_NO", DbType.String, null);
                db.AddInParameter(cmd, "SAP_ORIGINAL_ROW_NO", DbType.String, null);
                db.AddInParameter(cmd, "SHIP_QUANT", DbType.Decimal, model.Qty);
                db.AddInParameter(cmd, "RECEIVED_QUANT", DbType.Decimal, null);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "INVOICE_PRICE", DbType.Decimal, model.InvoicePrice);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.DeliverySeqNo = db.GetParameterValue(cmd, "DELIVERY_SEQ_NO").GetValue<int>();
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

        public void Insert(UserInfo user, SupplierDispatchPartOrderViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DELIVERY_DET_MULTIBLE",
                    MakeDbNull(model.DeliveryId),
                    MakeDbNull(string.IsNullOrEmpty(model.PurchaseNo) ? "" : model.PurchaseNo),
                    MakeDbNull(user.GetUserDealerId()),
                    MakeDbNull(model.SupplierId),
                    MakeDbNull(model.CommandType),
                    MakeDbNull(user.UserId),
                    null, null
                    );
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

        public IEnumerable<SupplierDispatchPartListModel> List(UserInfo user, SupplierDispatchPartListModel filter, out int totalCnt)
        {

            // Currency info is added => Taner
            var retVal = new List<SupplierDispatchPartListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DELIVERY_DET");
                db.AddInParameter(cmd, "ID_DELIVERY", DbType.String, MakeDbNull(filter.DeliveryId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddOutParameter(cmd, "CURRENCY", DbType.String, 3);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var supplier = new SupplierDispatchPartListModel
                        {
                            DeliverySeqNo = reader["DELIVERY_SEQ_NO"].GetValue<long>(),
                            PartCode = reader["PART_CODE"].ToString(),
                            Qty = reader["SHIP_QUANT"].GetValue<decimal>(),
                            InvoiceP = reader["INVOICE_PRICE"].GetValue<decimal>(),
                            PartName = reader["PART_NAME"].ToString(),
                            PONumber = reader["PO_NUMBER"].GetValue<long?>(),
                            DesirePartCode = reader["DESIRE_PART_CODE"].GetValue<string>(),
                            PartId = reader["ID_PART"].GetValue<long>()
                        };

                        retVal.Add(supplier);
                    }

                    
                    reader.Close();
                }

                string currency = db.GetParameterValue(cmd, "CURRENCY").ToString();
                retVal.ForEach(
                    c =>
                        c.InvoicePriceString =
                            string.Format(CultureInfo.CurrentCulture, "{0} {1}", c.InvoiceP.RoundUp(2).ToString("N2"), currency));

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

        public void Update(UserInfo user, SupplierDispatchPartViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DELIVERY_DET");
                db.AddInParameter(cmd, "DELIVERY_SEQ_NO", DbType.Int32, model.DeliverySeqNo);
                db.AddInParameter(cmd, "ID_DELIVERY", DbType.Int32, DBNull.Value);
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, model.PartId);
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, 1);
                db.AddInParameter(cmd, "PO_DET_SEQ_NO", DbType.Int32, null);
                db.AddInParameter(cmd, "SAP_OFFER_NO", DbType.String, null);
                db.AddInParameter(cmd, "SAP_ROW_NO", DbType.String, null);
                db.AddInParameter(cmd, "SAP_ORIGINAL_ROW_NO", DbType.String, null);
                db.AddInParameter(cmd, "SHIP_QUANT", DbType.Decimal, model.Qty);
                db.AddInParameter(cmd, "RECEIVED_QUANT", DbType.Decimal, null);
                db.AddInParameter(cmd, "INVOICE_PRICE", DbType.Decimal, model.InvoicePrice);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.DeliverySeqNo = db.GetParameterValue(cmd, "DELIVERY_SEQ_NO").GetValue<int>();
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

        public SupplierDispatchPartViewModel Update(UserInfo user, IEnumerable<SupplierDispatchPartListModel> listModel)
        {
            SupplierDispatchPartViewModel resultModel = new SupplierDispatchPartViewModel();

            try
            {
                CreateDatabase();

                foreach (var model in listModel)
                {
                    model.CommandType = CommonValues.DMLType.Update;

                    var cmd = db.GetStoredProcCommand("P_DML_DELIVERY_DET");
                    db.AddInParameter(cmd, "DELIVERY_SEQ_NO", DbType.Int32, model.DeliverySeqNo);
                    db.AddInParameter(cmd, "ID_DELIVERY", DbType.Int32, DBNull.Value);
                    db.AddInParameter(cmd, "ID_PART", DbType.Int32, model.PartId);
                    db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, 1);
                    db.AddInParameter(cmd, "PO_DET_SEQ_NO", DbType.Int32, null);
                    db.AddInParameter(cmd, "SAP_OFFER_NO", DbType.String, null);
                    db.AddInParameter(cmd, "SAP_ROW_NO", DbType.String, null);
                    db.AddInParameter(cmd, "SAP_ORIGINAL_ROW_NO", DbType.String, null);
                    db.AddInParameter(cmd, "SHIP_QUANT", DbType.Decimal, model.Qty);
                    db.AddInParameter(cmd, "RECEIVED_QUANT", DbType.Decimal, null);
                    db.AddInParameter(cmd, "INVOICE_PRICE", DbType.Decimal, model.InvoiceP);
                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                    db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                    CreateConnection(cmd);
                    cmd.ExecuteNonQuery();

                    model.DeliverySeqNo = db.GetParameterValue(cmd, "DELIVERY_SEQ_NO").GetValue<int>();
                    model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();

                    if (model.ErrorNo > 0)
                    {
                        resultModel.ErrorNo = model.ErrorNo;
                        resultModel.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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

            return resultModel;
        }

        public void ChangeDeliveryMstStatus(UserInfo user, DeliveryViewModel model)
        {

            IEnumerable<SupplierDispatchPartListModel> deliveryDetList = new List<SupplierDispatchPartListModel>();
            if (model.StatuId.GetValueOrDefault() == 3)
            {
                var totalCnt = 0;
                deliveryDetList = List(user, new SupplierDispatchPartListModel() { DeliveryId = model.DeliveryId }, out totalCnt);
            }

            CreateDatabase();
            var success = true;
            DbCommand cmd = db.GetStoredProcCommand("P_CHANGE_DELIVERY_MST_STATUS");
            CreateConnection(cmd);
            DbTransaction transaction = connection.BeginTransaction();
            try
            {
                db.AddInParameter(cmd, "DELIVERY_ID", DbType.Int64, MakeDbNull(model.DeliveryId));
                db.AddInParameter(cmd, "STATUS", DbType.Int32, model.StatuId.GetValue<int>());
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32,
                    MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                db.ExecuteNonQuery(cmd, transaction);

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();
                else if (model.StatuId.GetValue<int>() == 3)
                {

                    //MAL KABUL KALTIĞINDAN COMPLE İSE SHIPT QUANTLARI RECİEVED A ATIYORUZ
                    cmd = db.GetStoredProcCommand("P_DML_DELIVERY_DET");
                    foreach (var item in deliveryDetList)
                    {

                        cmd.Parameters.Clear();
                        db.AddInParameter(cmd, "DELIVERY_SEQ_NO", DbType.Int32, item.DeliverySeqNo);
                        db.AddInParameter(cmd, "ID_DELIVERY", DbType.Int32, DBNull.Value);
                        db.AddInParameter(cmd, "ID_PART", DbType.Int32, item.PartId);
                        db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, 1);
                        db.AddInParameter(cmd, "PO_DET_SEQ_NO", DbType.Int32, null);
                        db.AddInParameter(cmd, "SAP_OFFER_NO", DbType.String, null);
                        db.AddInParameter(cmd, "SAP_ROW_NO", DbType.String, null);
                        db.AddInParameter(cmd, "SAP_ORIGINAL_ROW_NO", DbType.String, null);
                        db.AddInParameter(cmd, "SHIP_QUANT", DbType.Decimal, item.Qty);
                        db.AddInParameter(cmd, "RECEIVED_QUANT", DbType.Decimal, item.Qty);
                        db.AddInParameter(cmd, "INVOICE_PRICE", DbType.Decimal, item.InvoiceP);
                        db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, CommonValues.DMLType.Update);
                        db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32,
                            MakeDbNull(user.UserId));
                        db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                        db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                        db.ExecuteNonQuery(cmd, transaction);


                        if (db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>() > 0)
                        {
                            success = false;
                            model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                            if (model.ErrorNo > 0)
                                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();

                            break;
                        }


                    }
                }


            }
            catch (Exception)
            {
                success = false;
            }
            finally
            {
                if (success)
                    transaction.Commit();
                else
                {
                    transaction.Rollback();
                }
                CloseConnection();
            }
        }
    }
}
