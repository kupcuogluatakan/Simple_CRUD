using System.Data.Common;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ODMSCommon.Security;
using ODMSModel.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Dealer;
using ODMSModel.PurchaseOrderDetail;
using ODMSModel.ServiceCallSchedule;

namespace ODMSData
{
    /// <remarks>
    /// readerlardaki GetValue<string> Gereksiz ToString() methodu ile değiştiriyorum Taner
    /// Burda standarttan ziyade performans sorunu var. GetValue<string> reflection kullanıyor.
    /// </remarks>

    public class PurchaseOrderData : DataAccessBase
    {
        public List<PurchaseOrderListModel> ListPurchaseOrder(UserInfo user, PurchaseOrderListModel filter, out int totalCnt)
        {
            var retVal = new List<PurchaseOrderListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PURCHASE_ORDER");
                db.AddInParameter(cmd, "PO_NUMBER", DbType.String, filter._PoNumber);
                db.AddInParameter(cmd, "PO_TYPE", DbType.Int32, filter.PoType);
                db.AddInParameter(cmd, "STATUS", DbType.Int32, filter.Status);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, filter.IdDealer);
                db.AddInParameter(cmd, "ID_SUPPLIER", DbType.Int32, filter.IdSupplier);
                db.AddInParameter(cmd, "IS_SAS_NO_SENT", DbType.Int32, filter.IsSASNoSent);
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, filter.IdStockType);
                db.AddInParameter(cmd, "BEGIN_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));

                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "ORDER_LOCATION", DbType.Int16, MakeDbNull(filter.OrderLocation));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(filter.CampaignCode));

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
                        var purchaseOrderListModel = new PurchaseOrderListModel
                        {
                            PoNumber = reader["PO_NUMBER"].GetValue<Int64>(),
                            PoTypeName = reader["PURCHASE_ORDER_TYPE_NAME"].ToString(),
                            DesiredShipDate = reader["PO_DESIRED_SHIP_DATE"].GetValue<DateTime?>(),
                            VinNo = reader["VIN_NO"].ToString(),
                            Plate = reader["PLATE"].ToString(),
                            StatusName = reader["STATUS_NAME"].ToString(),
                            SapOfferNo = reader["SAP_OFFER_NO"].ToString()
                        };
                        purchaseOrderListModel.ManuelPriceAllow = reader["MANUEL_PRICE_ALLOW"].GetValue<bool>();
                        purchaseOrderListModel.DealerBranchSSID = reader["DEALER_BRANCH_SSID"].ToString();
                        purchaseOrderListModel.IsBranchOrder = reader["IS_BRANCH_ORDER"].GetValue<bool>();
                        purchaseOrderListModel.IsProposal = reader["IS_PROPOSAL"].GetValue<bool>();
                        purchaseOrderListModel.IsBranchOrderName = reader["IS_BRANCH_ORDER"].GetValue<bool>() == true ? MessageResource.Global_Display_Yes : MessageResource.Global_Display_No;
                        purchaseOrderListModel.Status = reader["STATUS"].GetValue<int?>();
                        purchaseOrderListModel.IdSupplier = reader["ID_SUPPLIER"].GetValue<int>();
                        purchaseOrderListModel.IsProposalName = reader["IS_PROPOSAL_NAME"].GetValue<string>();
                        purchaseOrderListModel.SupplierIdDealer = reader["SUPPLIER_ID_DEALER"].GetValue<long>();
                        purchaseOrderListModel.SupplyType = ((purchaseOrderListModel.IdSupplier == null || purchaseOrderListModel.IdSupplier == 0)
                            && purchaseOrderListModel.SupplierIdDealer == null || purchaseOrderListModel.SupplierIdDealer == 0)
                            ? CommonValues.SupplyPort.Otokar :
                            (purchaseOrderListModel.IdSupplier != null && purchaseOrderListModel.IdSupplier != 0)
                            ? CommonValues.SupplyPort.Supplier : CommonValues.SupplyPort.DealerService;
                        purchaseOrderListModel.SupplyTypeId = (int)purchaseOrderListModel.SupplyType;
                        purchaseOrderListModel.OrderDate = reader["ORDER_DATE"].GetValue<DateTime?>();
                        purchaseOrderListModel.Location = reader["LOCATION"].ToString();
                        purchaseOrderListModel.StockType = reader["STOCK_TYPE_NAME"].ToString();
                        string supplierDealerConfirm = reader["SUPPLIER_DEALER_CONFIRM_NAME"].ToString();
                        purchaseOrderListModel.SupplierDealerConfirm = reader["SUPPLIER_DEALER_CONFIRM"].GetValue<int>();
                        purchaseOrderListModel.StatusName = reader["STATUS_NAME"].ToString();
                        if (purchaseOrderListModel.SupplyType == CommonValues.SupplyPort.DealerService)
                        {
                            purchaseOrderListModel.StatusName = purchaseOrderListModel.StatusName + " - " +
                                                                supplierDealerConfirm;
                        }
                        purchaseOrderListModel.TotalReceivedQuantity = reader["TOTAL_RECEIVED_QUANTITY"].GetValue<decimal>();
                        purchaseOrderListModel.TotalShipmentQuantity = reader["TOTAL_SHIPMENT_QUANTITY"].GetValue<decimal>();
                        purchaseOrderListModel.TotalPrice = reader["TOTAL_PRICE"].GetValue<decimal>();
                        retVal.Add(purchaseOrderListModel);
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

        public PurchaseOrderViewModel GetPurchaseOrder(UserInfo user, PurchaseOrderViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PURCHASE_ORDER");
                db.AddInParameter(cmd, "PO_NUMBER", DbType.Int64, MakeDbNull(filter.PoNumber));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.PoNumber = dReader["PO_NUMBER"].GetValue<Int64>();
                    filter.CurrencyCode = dReader["CURRENCY_CODE"].GetValue<string>();
                    filter.IdStockType = dReader["ID_STOCK_TYPE"].GetValue<int>();
                    filter.DesiredShipDate = dReader["PO_DESIRED_SHIP_DATE"].GetValue<DateTime?>();
                    filter.PoType = dReader["ID_PO_TYPE"].GetValue<int>();
                    filter.PoTypeName = dReader["PURCHASE_ORDER_TYPE_NAME"].ToString();
                    filter.Status = dReader["STATUS_LOOKVAL"].GetValue<int?>();
                    filter.StatusName = dReader["STATUS_NAME"].ToString();
                    filter.IdDealer = dReader["ID_DEALER"].GetValue<int>();
                    filter.VehicleId = dReader["ID_VEHICLE"].GetValue<int?>();
                    filter.BranchSSID = dReader["BRANCH_SSID"].GetValue<string>();
                    filter.IsBranchOrder = dReader["IS_BRANCH_ORDER"].GetValue<bool>();
                    filter.IdSupplier = dReader["ID_SUPPLIER"].GetValue<Int64?>();
                    filter.SupplierIdDealer = dReader["SUPPLIER_ID_DEALER"].GetValue<Int64?>();
                    filter.Location = dReader["LOCATION"].ToString();
                    /*
                        idsupplier and supplier id dealer = null ise Otokar 
                        supplier_id_dealer dolu ise ise bayi
                        id_supplier dolu ise tedarikçi
                     */
                    if (filter.IdSupplier == null && filter.SupplierIdDealer == null)
                        filter.SupplyType = (int)CommonValues.SupplyPort.Otokar;
                    else if (filter.SupplierIdDealer != null)
                    {
                        filter.SupplyType = (int)CommonValues.SupplyPort.DealerService;
                    }
                    else
                        filter.SupplyType = (int)CommonValues.SupplyPort.Supplier;

                    filter.DealerName = dReader["DEALER_NAME"].ToString();
                    filter.SupplierName = dReader["SUPPLIER_NAME"].ToString();
                    filter.ModelKod = dReader["MODEL_KOD"].ToString();

                    filter.StockTypeName = dReader["ADMIN_DESC"].ToString();
                    filter.ProposalType = dReader["PROPOSAL_TYPE"].ToString();
                    filter.CreateDate = dReader["CREATE_DATE"].GetValue<DateTime>();
                    filter.DeliveryPriority = dReader["DELIVERY_PRIORITY"].GetValue<int>();
                    filter.SalesOrganization = dReader["SALES_ORGANIZATION"].ToString();
                    filter.Description = dReader["DESCRIPTION"].ToString();
                    filter.DealerSSID = dReader["DEALER_SSID"].ToString();
                    filter.Division = dReader["DIVISION"].ToString();
                    filter.OrderReason = dReader["ORD_REASON"].ToString();
                    filter.DistrChan = dReader["DIST_CHAN"].ToString();
                    filter.ItemCategory = dReader["ITEM_CATEG"].ToString();
                    filter.SupplierDealerConfirm = dReader["SUPPLIER_DEALER_CONFIRM"].GetValue<int>();
                    filter.OrderDate = dReader["ORDER_DATE"].GetValue<DateTime>();
                    filter.UpdateUser = dReader["UPDATE_USER"].GetValue<string>();
                    filter.VinNo = dReader["VIN_NO"].GetValue<string>();
                    filter.IsProposal = dReader["IS_PROPOSAL"].GetValue<bool>();
                    filter.IsProposalName = dReader["IS_PROPOSAL_NAME"].GetValue<string>();
                    filter.CreateUser = dReader["CREATED_USER"].GetValue<string>();
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

        public void DMLPurchaseOrder(UserInfo user, PurchaseOrderViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, model.IdDealer);
                db.AddParameter(cmd, "PO_NUMBER", DbType.Int64, ParameterDirection.InputOutput, "PO_NUMBER", DataRowVersion.Default, model.PoNumber);
                //db.AddInParameter(cmd, "SUPPLY_TYPE", DbType.Int32, model.SupplyType);
                db.AddInParameter(cmd, "ID_SUPPLIER", DbType.Int64, model.IdSupplier);
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, model.VehicleId);
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, model.IdStockType);
                db.AddInParameter(cmd, "PO_DESIRED_SHIP_DATE", DbType.DateTime, MakeDbNull(model.DesiredShipDate));
                db.AddInParameter(cmd, "ID_PO_TYPE", DbType.Int32, model.PoType);
                db.AddInParameter(cmd, "IS_BRANCH_ORDER", DbType.Boolean, model.IsBranchOrder);
                db.AddInParameter(cmd, "STATUS_LOOKVAL", DbType.Int32, model.Status);
                db.AddInParameter(cmd, "STATUS_DETAIL", DbType.Int32, model.StatusDetail);
                db.AddInParameter(cmd, "PROPOSAL_TYPE", DbType.String, model.ProposalType);
                db.AddInParameter(cmd, "DELIVERY_PRIORITY", DbType.Int16, model.DeliveryPriority);
                db.AddInParameter(cmd, "SALES_ORGANIZATION", DbType.String, model.SalesOrganization);
                db.AddInParameter(cmd, "DIVISION", DbType.String, model.Division);
                db.AddInParameter(cmd, "DISTR_CHAN", DbType.String, model.DistrChan);
                db.AddInParameter(cmd, "ORD_REASON", DbType.String, model.OrderReason);
                db.AddInParameter(cmd, "ITEM_CATEG", DbType.String, model.ItemCategory);
                db.AddInParameter(cmd, "IS_SAS_NO_SENT", DbType.Int16, model.IsSASNoSent);
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(model.ModelKod));
                db.AddInParameter(cmd, "SUPPLIER_ID_DEALER", DbType.Int32, MakeDbNull(model.SupplierIdDealer));
                db.AddInParameter(cmd, "SUPPLIER_DEALER_CONFIRM", DbType.Int32, model.SupplierDealerConfirm);
                db.AddInParameter(cmd, "IS_PRICE_FIXED", DbType.Boolean, model.IsPriceFixed);
                db.AddInParameter(cmd, "IS_PROPOSAL", DbType.Boolean, model.IsProposal);
                db.AddInParameter(cmd, "ID_DEFECT", DbType.Int32, model.IdDefect);

                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.String, MakeDbNull(user.UserId.ToString()));
                db.AddInParameter(cmd, "OPERATING_DATE", DbType.Date, MakeDbNull(DateTime.Now));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.PoNumber = db.GetParameterValue(cmd, "PO_NUMBER").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                {
                    model.ErrorMessage = MessageResource.PurchaseOrder_Error_Complete;
                }
                else if (model.ErrorNo > 0)
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

        public bool CheckPurchaseOrderDefectPart(PurchaseOrderViewModel viewModel, long idPart)
        {
            CreateDatabase();
            var cmd = db.GetStoredProcCommand("P_CHECK_PO_DEFECT_PART");
            db.AddInParameter(cmd, "PO_NUMBER", DbType.Int32, MakeDbNull(viewModel.PoNumber));
            db.AddInParameter(cmd, "ID_PART", DbType.Int32, MakeDbNull(idPart));
            CreateConnection(cmd);
            bool returnVal = false;
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnVal = reader["RESULT"].GetValue<bool>();
                }
                reader.Close();
            }
            return returnVal;
        }
        public List<string> ListOrderPart(long? poNumber)
        {
            var listItem = new List<string>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_PURCHASE_ORDER_PART");
                db.AddInParameter(cmd, "PO_NUMBER", DbType.Int64, MakeDbNull(poNumber));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listItem.Add(dr["POD_PART"].ToString());
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

            return listItem;
        }

        public void WSResultToDb(UserInfo user, PurchaseOrderServiceModel serviceModel)
        {
            bool isSuccess = true;
            int quantity = 0;
            CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("P_COMPLETE_PURCHASE_ORDER_DET");
            CreateConnection(cmd);
            DbTransaction transaction = connection.BeginTransaction();
            try
            {
                foreach (var withQuantity in serviceModel.AllParts.Split(';'))
                {
                    quantity += 10;
                    string partCode = withQuantity.Split('!')[0];

                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "PO_NUMBER", DbType.Int64, serviceModel.PoNumber);
                    db.AddInParameter(cmd, "ROW_NUMBER", DbType.Int32, quantity);
                    db.AddInParameter(cmd, "PART_CODE", DbType.String, partCode);
                    db.AddInParameter(cmd, "ORDER_NUMBER", DbType.Int32, serviceModel.OrderNo);
                    db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    db.ExecuteNonQuery(cmd, transaction);
                    serviceModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();

                    if (serviceModel.ErrorNo > 0)
                    {
                        isSuccess = false;
                        serviceModel.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());
                        break;
                    }

                }

            }
            catch (Exception Ex)
            {
                isSuccess = false;
                serviceModel.ErrorNo = 1;
                serviceModel.ErrorMessage = MessageResource.Err_Generic_Unexpected + "System Message : (" + Ex.Message + ")";
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
                //transaction.
                CloseConnection();
            }
        }

        public List<SelectListItem> PurchaseOrderTypes(UserInfo user, string supplyType)
        {
            List<SelectListItem> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<SelectListItem>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_PURCHASE_ORDER_TYPE_BY_SUPPLY_TYPE", rowMapper
                    , MakeDbNull(supplyType)
                    , MakeDbNull(user.LanguageCode)
                    , MakeDbNull((user.ActiveDealerId == 0) ? user.DealerID : user.ActiveDealerId)
                ).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return list;
        }

        public dynamic GetPurchaseOrderTypeStockType(UserInfo user, string poType)
        {

            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<SelectListItem>.BuildAllProperties();
                var item = db.ExecuteSprocAccessor("P_GET_PURCHASE_ORDER_TYPE_STOCK_TYPE", rowMapper, MakeDbNull(poType), MakeDbNull(user.LanguageCode)).SingleOrDefault();
                if (item != null)
                {
                    dynamic model = new
                    {
                        StockTypeId = item.Value,
                        StockTypeName = item.Text
                    };
                    return model;
                }
                return null;
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

        public string GetOfferNo(string poNumber)
        {
            string rValue = string.Empty;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_SAP_OFFER_NO");

                db.AddInParameter(cmd, "PO_NUMBER", DbType.String, poNumber);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        rValue = dr["SAP_OFFER_NO"].ToString();
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

        public void UpdatePurchaseOrderIsSASNoSentValue(UserInfo user, List<PurchaseOrderViewModel> purchaseOrderModelList, ServiceCallLogModel logModel)
        {
            bool isSuccessfull = true;
            CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER");
            CreateConnection(cmd);
            var listError = new List<ServiceCallScheduleErrorListModel>();

            string userId = string.Empty;
            string lang = user.LanguageCode;
            try
            {
                if (user == null)
                {
                    userId = "WEBSERVIS";
                    lang = "TR";
                }
                else
                {
                    userId = user.UserId.ToString();
                }
            }
            catch
            {
                userId = "WEBSERVIS";
                lang = "TR";
            }

            try
            {
                foreach (PurchaseOrderViewModel purchaseOrderModel in purchaseOrderModelList)
                {
                    #region Purchase Order MST
                    cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER");
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, purchaseOrderModel.IdDealer);
                    db.AddParameter(cmd, "PO_NUMBER", DbType.Int64, ParameterDirection.InputOutput, "PO_NUMBER",
                                    DataRowVersion.Default, purchaseOrderModel.PoNumber);
                    db.AddInParameter(cmd, "ID_SUPPLIER", DbType.Int64, purchaseOrderModel.IdSupplier);
                    db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, purchaseOrderModel.VehicleId);
                    db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, purchaseOrderModel.IdStockType);
                    db.AddInParameter(cmd, "PO_DESIRED_SHIP_DATE", DbType.DateTime, purchaseOrderModel.DesiredShipDate);
                    db.AddInParameter(cmd, "ID_PO_TYPE", DbType.Int32, purchaseOrderModel.PoType);
                    db.AddInParameter(cmd, "IS_BRANCH_ORDER", DbType.Boolean, purchaseOrderModel.IsBranchOrder);
                    db.AddInParameter(cmd, "STATUS_LOOKVAL", DbType.Int32, purchaseOrderModel.Status);
                    db.AddInParameter(cmd, "STATUS_DETAIL", DbType.Int32, purchaseOrderModel.StatusDetail);
                    db.AddInParameter(cmd, "PROPOSAL_TYPE", DbType.String, purchaseOrderModel.ProposalType);
                    db.AddInParameter(cmd, "DELIVERY_PRIORITY", DbType.Int16, purchaseOrderModel.DeliveryPriority);
                    db.AddInParameter(cmd, "SALES_ORGANIZATION", DbType.String, purchaseOrderModel.SalesOrganization);
                    db.AddInParameter(cmd, "DIVISION", DbType.String, purchaseOrderModel.Division);
                    db.AddInParameter(cmd, "DISTR_CHAN", DbType.String, purchaseOrderModel.DistrChan);
                    db.AddInParameter(cmd, "ORD_REASON", DbType.String, purchaseOrderModel.OrderReason);
                    db.AddInParameter(cmd, "ITEM_CATEG", DbType.String, purchaseOrderModel.ItemCategory);
                    db.AddInParameter(cmd, "IS_SAS_NO_SENT", DbType.Int16, purchaseOrderModel.IsSASNoSent);
                    db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(purchaseOrderModel.ModelKod));
                    db.AddInParameter(cmd, "SUPPLIER_ID_DEALER", DbType.Int32, MakeDbNull(purchaseOrderModel.SupplierIdDealer));
                    db.AddInParameter(cmd, "SUPPLIER_DEALER_CONFIRM", DbType.Int32, MakeDbNull(purchaseOrderModel.SupplierDealerConfirm));
                    db.AddInParameter(cmd, "IS_PRICE_FIXED", DbType.Int32, MakeDbNull(purchaseOrderModel.IsPriceFixed));
                    db.AddInParameter(cmd, "IS_PROPOSAL", DbType.Int32, MakeDbNull(purchaseOrderModel.IsProposal));
                    db.AddInParameter(cmd, "ID_DEFECT", DbType.Int32, purchaseOrderModel.IdDefect);

                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, purchaseOrderModel.CommandType);
                    db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(lang));
                    db.AddInParameter(cmd, "OPERATING_USER", DbType.String, MakeDbNull(userId));
                    db.AddInParameter(cmd, "OPERATING_DATE", DbType.Date, MakeDbNull(DateTime.Now));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                    db.ExecuteNonQuery(cmd);
                    purchaseOrderModel.PoNumber = db.GetParameterValue(cmd, "PO_NUMBER").GetValue<int>();
                    purchaseOrderModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    purchaseOrderModel.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    if (purchaseOrderModel.ErrorNo == 2)
                    {
                        purchaseOrderModel.ErrorMessage = MessageResource.PurchaseOrder_Error_Complete;
                    }
                    else if (purchaseOrderModel.ErrorNo > 0)
                    {
                        purchaseOrderModel.ErrorMessage = ResolveDatabaseErrorXml(purchaseOrderModel.ErrorMessage);
                    }
                    if (purchaseOrderModel.ErrorNo != 0)
                    {
                        listError.Add(new ServiceCallScheduleErrorListModel()
                        {
                            Action = MessageResource.PurchaseOrder_Display_PoNumber + " : " + purchaseOrderModel.PoNumber,
                            Error = purchaseOrderModel.ErrorMessage
                        });
                        isSuccessfull = false;
                        logModel.IsSuccess = false;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                isSuccessfull = false;
                logModel.IsSuccess = false;
                logModel.LogErrorDesc = ex.Message;
            }
            finally
            {
                if (!isSuccessfull)
                {
                    logModel.ErrorModel = listError;
                }
                CloseConnection();
            }
        }
        public void DMLPurchaseOrderAndDetails(UserInfo user, List<PurchaseOrderViewModel> purchaseOrderModelList, ServiceCallLogModel logModel)
        {
            bool isSuccessfull = true;
            CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER");
            CreateConnection(cmd);
            DbTransaction transaction = connection.BeginTransaction();
            var listError = new List<ServiceCallScheduleErrorListModel>();

            string userId = string.Empty;
            string lang = user.LanguageCode;
            try
            {
                if (user == null)
                {
                    userId = "WEBSERVIS";
                    lang = "TR";
                }
                else
                {
                    userId = user.UserId.ToString();
                }
            }
            catch
            {
                userId = "WEBSERVIS";
                lang = "TR";
            }
            try
            {
                foreach (PurchaseOrderViewModel purchaseOrderModel in purchaseOrderModelList)
                {
                    #region Purchase Order MST
                    cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER");
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, purchaseOrderModel.IdDealer);
                    db.AddParameter(cmd, "PO_NUMBER", DbType.Int64, ParameterDirection.InputOutput, "PO_NUMBER",
                                    DataRowVersion.Default, purchaseOrderModel.PoNumber);
                    db.AddInParameter(cmd, "ID_SUPPLIER", DbType.Int64, purchaseOrderModel.IdSupplier);
                    db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, purchaseOrderModel.VehicleId);
                    db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, purchaseOrderModel.IdStockType);
                    db.AddInParameter(cmd, "PO_DESIRED_SHIP_DATE", DbType.DateTime, purchaseOrderModel.DesiredShipDate);
                    db.AddInParameter(cmd, "ID_PO_TYPE", DbType.Int32, purchaseOrderModel.PoType);
                    db.AddInParameter(cmd, "IS_BRANCH_ORDER", DbType.Boolean, purchaseOrderModel.IsBranchOrder);
                    db.AddInParameter(cmd, "STATUS_LOOKVAL", DbType.Int32, purchaseOrderModel.Status);
                    db.AddInParameter(cmd, "STATUS_DETAIL", DbType.Int32, purchaseOrderModel.StatusDetail);
                    db.AddInParameter(cmd, "PROPOSAL_TYPE", DbType.String, purchaseOrderModel.ProposalType);
                    db.AddInParameter(cmd, "DELIVERY_PRIORITY", DbType.Int16, purchaseOrderModel.DeliveryPriority);
                    db.AddInParameter(cmd, "SALES_ORGANIZATION", DbType.String, purchaseOrderModel.SalesOrganization);
                    db.AddInParameter(cmd, "DIVISION", DbType.String, purchaseOrderModel.Division);
                    db.AddInParameter(cmd, "DISTR_CHAN", DbType.String, purchaseOrderModel.DistrChan);
                    db.AddInParameter(cmd, "ORD_REASON", DbType.String, purchaseOrderModel.OrderReason);
                    db.AddInParameter(cmd, "ITEM_CATEG", DbType.String, purchaseOrderModel.ItemCategory);
                    db.AddInParameter(cmd, "IS_SAS_NO_SENT", DbType.Int16, purchaseOrderModel.IsSASNoSent);
                    db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(purchaseOrderModel.ModelKod));
                    db.AddInParameter(cmd, "SUPPLIER_ID_DEALER", DbType.Int32, MakeDbNull(purchaseOrderModel.SupplierIdDealer));
                    db.AddInParameter(cmd, "SUPPLIER_DEALER_CONFIRM", DbType.Int32, MakeDbNull(purchaseOrderModel.SupplierDealerConfirm));
                    db.AddInParameter(cmd, "IS_PRICE_FIXED", DbType.Int32, MakeDbNull(purchaseOrderModel.IsPriceFixed));
                    db.AddInParameter(cmd, "IS_PROPOSAL", DbType.Int32, MakeDbNull(purchaseOrderModel.IsProposal));
                    db.AddInParameter(cmd, "ID_DEFECT", DbType.Int32, null);

                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, purchaseOrderModel.CommandType);
                    db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(lang));
                    db.AddInParameter(cmd, "OPERATING_USER", DbType.String, MakeDbNull(userId));
                    db.AddInParameter(cmd, "OPERATING_DATE", DbType.Date, MakeDbNull(DateTime.Now));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                    db.ExecuteNonQuery(cmd, transaction);
                    purchaseOrderModel.PoNumber = db.GetParameterValue(cmd, "PO_NUMBER").GetValue<int>();
                    purchaseOrderModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    purchaseOrderModel.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    if (purchaseOrderModel.ErrorNo == 2)
                    {
                        purchaseOrderModel.ErrorMessage = MessageResource.PurchaseOrder_Error_Complete;
                    }
                    else if (purchaseOrderModel.ErrorNo > 0)
                    {
                        purchaseOrderModel.ErrorMessage = ResolveDatabaseErrorXml(purchaseOrderModel.ErrorMessage);
                    }
                    if (purchaseOrderModel.ErrorNo != 0)
                    {
                        listError.Add(new ServiceCallScheduleErrorListModel()
                        {
                            Action = MessageResource.PurchaseOrder_Display_PoNumber + " : " + purchaseOrderModel.PoNumber,
                            Error = purchaseOrderModel.ErrorMessage
                        });
                        isSuccessfull = false;
                        logModel.IsSuccess = false;
                    }
                    #endregion

                    #region Purchase Order DET

                    foreach (PurchaseOrderDetailViewModel purchaseOrderDetailModel in purchaseOrderModel.detailList)
                    {
                        cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER_DET");
                        cmd.Parameters.Clear();
                        purchaseOrderDetailModel.PurchaseOrderNumber = purchaseOrderModel.PoNumber.GetValue<int>();
                        db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, purchaseOrderDetailModel.CommandType);
                        db.AddParameter(cmd, "PO_DET_SEQ_NO", DbType.Int64, ParameterDirection.InputOutput,
                                        "PO_DET_SEQ_NO", DataRowVersion.Default,
                                        MakeDbNull(purchaseOrderDetailModel.PurchaseOrderDetailSeqNo));
                        db.AddInParameter(cmd, "PO_NUMBER", DbType.Int32,
                                          MakeDbNull(purchaseOrderDetailModel.PurchaseOrderNumber));
                        db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(purchaseOrderDetailModel.PartId));
                        db.AddInParameter(cmd, "PACKAGE_QUANTITY", DbType.Decimal,
                                          purchaseOrderDetailModel.PackageQuantity);
                        db.AddInParameter(cmd, "DESIRE_QUANTITY", DbType.Decimal,
                                          purchaseOrderDetailModel.DesireQuantity);
                        db.AddInParameter(cmd, "ORDER_QUANTITY", DbType.Decimal, purchaseOrderDetailModel.OrderQuantity);
                        db.AddInParameter(cmd, "SHIPMENT_QUANTITY", DbType.Decimal,
                                          purchaseOrderDetailModel.ShipmentQuantity);
                        db.AddInParameter(cmd, "ORDER_PRICE", DbType.Decimal, purchaseOrderDetailModel.OrderPrice);
                        db.AddInParameter(cmd, "DESIRE_DELIVERY_DATE", DbType.DateTime,
                                          MakeDbNull(purchaseOrderDetailModel.DesireDeliveryDate));
                        db.AddInParameter(cmd, "STATUS_ID", DbType.Int32, purchaseOrderDetailModel.StatusId);
                        db.AddInParameter(cmd, "DENY_REASON", DbType.String,
                                          MakeDbNull(purchaseOrderDetailModel.DenyReason));
                        db.AddInParameter(cmd, "SAP_SHIP_ID_PART", DbType.String, purchaseOrderDetailModel.SAPShipIdPart);
                        db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String,
                                          MakeDbNull(lang));
                        db.AddInParameter(cmd, "CURRENCY_CODE", DbType.String, MakeDbNull(purchaseOrderDetailModel.CurrencyCode));
                        db.AddInParameter(cmd, "LIST_PRICE", DbType.Decimal, purchaseOrderDetailModel.ListPrice);
                        db.AddInParameter(cmd, "LIST_DISCOUNT_RATIO", DbType.Decimal, purchaseOrderDetailModel.ListDiscountRatio);
                        db.AddInParameter(cmd, "CONFIRM_PRICE", DbType.Decimal, purchaseOrderDetailModel.ConfirmPrice);
                        db.AddInParameter(cmd, "APPLIED_DISCOUNT_RATIO", DbType.Decimal, purchaseOrderDetailModel.AppliedDiscountRatio);
                        db.AddInParameter(cmd, "SPECIAL_EXPLANATION", DbType.String, purchaseOrderDetailModel.SpecialExplanation);

                        db.AddInParameter(cmd, "OPERATING_USER", DbType.String, MakeDbNull(userId));
                        db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                        db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                        db.ExecuteNonQuery(cmd, transaction);
                        purchaseOrderDetailModel.PurchaseOrderDetailSeqNo =
                            db.GetParameterValue(cmd, "PO_DET_SEQ_NO").GetValue<long>();
                        purchaseOrderDetailModel.PurchaseOrderNumber =
                            db.GetParameterValue(cmd, "PO_NUMBER").GetValue<int>();
                        purchaseOrderDetailModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                        purchaseOrderDetailModel.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                        if (purchaseOrderDetailModel.ErrorNo > 0)
                        {
                            purchaseOrderDetailModel.ErrorMessage =
                                ResolveDatabaseErrorXml(purchaseOrderDetailModel.ErrorMessage);
                            isSuccessfull = false;

                            listError.Add(new ServiceCallScheduleErrorListModel()
                            {
                                Action = MessageResource.PurchaseOrder_Display_PoNumber + " : " + purchaseOrderDetailModel.PurchaseOrderNumber + " " +
                                MessageResource.PurchaseOrderDetail_Display_SequenceNo + " : " + purchaseOrderDetailModel.PurchaseOrderDetailSeqNo,
                                Error = purchaseOrderDetailModel.ErrorMessage
                            });

                            logModel.IsSuccess = false;
                        }


                        #region Puchase Order Complete
                        cmd = db.GetStoredProcCommand("P_COMPLETE_PURCHASE_ORDER_DET");
                        cmd.Parameters.Clear();
                        db.AddInParameter(cmd, "PO_NUMBER", DbType.String, purchaseOrderDetailModel.PurchaseOrderNumber);
                        db.AddInParameter(cmd, "ROW_NUMBER", DbType.Int32, purchaseOrderDetailModel.RowNumber);
                        db.AddInParameter(cmd, "PART_CODE", DbType.String, purchaseOrderDetailModel.PartCode);
                        db.AddInParameter(cmd, "ORDER_NUMBER", DbType.String, purchaseOrderDetailModel.OrderNumber);
                        db.AddInParameter(cmd, "OPERATING_USER", DbType.String, MakeDbNull(userId));
                        db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                        db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                        db.ExecuteNonQuery(cmd, transaction);
                        int errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                        string errorMsg = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                        if (errorNo > 0)
                        {
                            isSuccessfull = false;
                            listError.Add(new ServiceCallScheduleErrorListModel()
                            {
                                Action = MessageResource.PurchaseOrder_Display_PoNumber + " : " + purchaseOrderDetailModel.PurchaseOrderNumber,
                                Error = errorMsg
                            });
                        }
                        #endregion
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                isSuccessfull = false;
                logModel.IsSuccess = false;
                logModel.LogErrorDesc = ex.Message;
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
                    logModel.ErrorModel = listError;
                }
                CloseConnection();
            }
        }
    }
}
