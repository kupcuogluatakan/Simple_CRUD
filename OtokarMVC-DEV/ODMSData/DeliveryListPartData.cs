using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.DeliveryListPart;

namespace ODMSData
{
    public class DeliveryListPartData : DataAccessBase
    {
        public List<DeliveryListPartListModel> ListDeliveryListPart(UserInfo user,DeliveryListPartListModel filter, out int totalCount)
        {
            var listModel = new List<DeliveryListPartListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_DELIVERY_PART");

                db.AddInParameter(cmd, "DELIVERY_ID", DbType.Int64, filter.DeliveryId);
                AddPagingParametersWithLanguage(user, cmd, filter);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new DeliveryListPartListModel
                        {
                            DeliverySeqNo = dr["DELIVERY_SEQ_NO"].GetValue<Int64>(),
                            PartName = dr["PART_NAME"].ToString(),
                            PartId = dr["PART_ID"].GetValue<int>(),
                            ShipQnty = dr["SHIP_QUANT"].GetValue<decimal>(),
                            PackageQnty = dr["PACKAGE_QUANT"].GetValue<decimal>(),
                            ReceiveQnty = dr["RECEIVED_QUANT"].GetValue<decimal>(),
                            Id = dr["DELIVERY_SEQ_NO"].GetValue<int>(),
                            StockType = dr["MAINT_NAME"].ToString(),
                            ChildCount = dr["CHILD_COUNT"].GetValue<int>(),
                            Barcode = dr["BARCODE"].ToString(),
                            PoDetSeqNo = dr["PO_DET_SEQ_NO"].GetValue<long>(),
                            InvoicePrice = dr["INVOICE_PRICE"].GetValue<decimal>(),
                            SAPOfferNo = dr["SAP_OFFER_NO"].ToString(),
                            PoNumber = dr["PO_NUMBER"].ToString(),
                            PartCode = dr["PART_CODE"].ToString()
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

        public void DMLDeliveryListDetail(UserInfo user, DeliveryListPartSubViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DELIVERY_DET");
                db.AddInParameter(cmd, "DELIVERY_SEQ_NO", DbType.Int32, model.DeliverySeqNo);
                db.AddInParameter(cmd, "ID_DELIVERY", DbType.Int32, model.DeliveryId);
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, model.PartId);
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, model.StockTypeId);
                db.AddInParameter(cmd, "PO_DET_SEQ_NO", DbType.Int32, model.PoDetSeqNo);
                db.AddInParameter(cmd, "SAP_OFFER_NO", DbType.String, model.SapOfferNo);
                db.AddInParameter(cmd, "SAP_ROW_NO", DbType.String, model.SapRowNo);
                db.AddInParameter(cmd, "SAP_ORIGINAL_ROW_NO", DbType.String, model.SapOriginalRowNo);
                db.AddInParameter(cmd, "SHIP_QUANT", DbType.Decimal, model.ShipQnty);
                db.AddInParameter(cmd, "RECEIVED_QUANT", DbType.Decimal, model.ReceiveQnty);
                db.AddInParameter(cmd, "INVOICE_PRICE", DbType.Decimal, model.InvoicePrice);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, MakeDbNull(model.CommandType));
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
                string errorMessage = ex.Message;
            }
            finally
            {
                CloseConnection();
            }
        }


        public void DMLDeliveryListPart(UserInfo user, DeliveryListPartViewModel model)
        {
            bool isSuccess = true;
            CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("P_DML_DELIVERY_PART");
            CreateConnection(cmd);
            DbTransaction transaction = connection.BeginTransaction();
            try
            {
                foreach (var submodel in model.ListModel)
                {
                    //Set Command Type 'Insert' or 'Update' by ResultId
                    //listModel.CommandType = model.ResultId != -1 ? CommonValues.DMLType.Update : CommonValues.DMLType.Insert;

                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "DELIVERY_SEQ_NO", DbType.Int64, submodel.DeliverySeqNo);
                    db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, submodel.ReceiveQnty);
                    db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    db.ExecuteNonQuery(cmd, transaction);
                    model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();

                    if (model.ErrorNo > 0)
                    {
                        isSuccess = false;
                        model.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());
                        break;
                    }

                }

            }
            catch (Exception Ex)
            {
                isSuccess = false;
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.Err_Generic_Unexpected + "System Message : (" + Ex.Message + ")";
            }
            finally
            {
                if (isSuccess)
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

        public void CompleteDeliveryListPart(UserInfo user, DeliveryListPartViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_COMPLETE_DELIVERY_PART");

                db.AddInParameter(cmd, "DELIVERY_ID", DbType.Int64, model.DeliveryId);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
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
    }
}
