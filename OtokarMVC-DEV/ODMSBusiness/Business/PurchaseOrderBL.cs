using System;
using System.Data;
using System.Linq;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.Dealer;
using ODMSModel.PurchaseOrder;
using System.Collections.Generic;
using System.Web.Mvc;
using ODMSBusiness.ReportService;
using ODMSModel.PurchaseOrderDetail;
using ODMSModel.PurchaseOrderType;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.SparePart;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class PurchaseOrderBL : BaseBusiness
    {
        private readonly PurchaseOrderData data = new PurchaseOrderData();
        private readonly PurchaseOrderDetailData dataPurchaseOrderDetail = new PurchaseOrderDetailData();

        private readonly PurchaseOrderDetailBL purchaseOrderDetailBl = new PurchaseOrderDetailBL();
        private readonly PurchaseOrderTypeBL purchaseOrderTypeBl = new PurchaseOrderTypeBL();
        public ResponseModel<PurchaseOrderListModel> ListPurchaseOrder(UserInfo user, PurchaseOrderListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<PurchaseOrderListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListPurchaseOrder(user, filter, out totalCnt);
                response.Total = totalCnt;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;

        }
        public ResponseModel<PurchaseOrderViewModel> GetPurchaseOrder(UserInfo user, PurchaseOrderViewModel purchaseOrderModel)
        {
            var response = new ResponseModel<PurchaseOrderViewModel>();
            try
            {
                response.Model = data.GetPurchaseOrder(user, purchaseOrderModel);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<PurchaseOrderViewModel> DMLPurchaseOrder(UserInfo user, PurchaseOrderViewModel model)
        {
            var response = new ResponseModel<PurchaseOrderViewModel>();
            try
            {
                data.DMLPurchaseOrder(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }
        public ResponseModel<string> SetDataForOtokarWebService(PurchaseOrderViewModel filter)
        {
            var response = new ResponseModel<string>();
            try
            {
                //const int sapCustCodeLength = 10;
                //string sapSSID = string.Empty;

                var listParts = data.ListOrderPart(filter.PoNumber);//Getting Parts with PartCode!Quantity

                filter.AllDetParts = string.Join(";", listParts.ToArray());//Setting them with PartCode1!Quantity1;PartCode2!Quantity2

                // SetSapCustomerDigit(model, sapCustCodeLength);
                filter.CreateDate = filter.CreateDate.AddDays(30);
                response.Data = data.ListOrderPart(filter.PoNumber);//Getting Parts with PartCode!Quantity
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;

        }

        private void SetSapCustomerDigit(PurchaseOrderViewModel model, int sapDigit)
        {
            //DealerSSID
            if (!string.IsNullOrEmpty(model.DealerSSID))
            {
                int digit = model.DealerSSID.Length; //6 
                for (int I = 0; I < sapDigit - digit; I++)
                {
                    model.DealerSSID = "0" + model.DealerSSID;
                }
            }
            else
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.PurchaseOrder_Warning_CustomerSAPCodeEmpty;
            }

            //BracnchSSID
            if (!string.IsNullOrEmpty(model.BranchSSID))
            {
                int digit = model.BranchSSID.Length; //6 
                for (int I = 0; I < sapDigit - digit; I++)
                {
                    model.BranchSSID = "0" + model.BranchSSID;
                }
            }
        }


        public ResponseModel<PurchaseOrderServiceModel> ServiceToDb(UserInfo user, PurchaseOrderServiceModel model)
        {
            var response = new ResponseModel<PurchaseOrderServiceModel>();
            try
            {
                if (model.ListModel.Any(p => p.Type == "E"))
                {
                    model.ErrorNo = 1;
                    foreach (PurchaseOrderServiceListModel model_1 in model.ListModel)
                    {
                        if (model_1.Type == "E")
                        {
                            model.ErrorMessage += model_1.Message + "<br/>";
                        }

                    }
                    throw new Exception(model.ErrorMessage);
                }
                else
                {
                    int iorderNo = 0;
                    var purchaseOrderServiceListModel = model.ListModel.FirstOrDefault(p => p.Type == "S" && p.ID == "V1" && p.Number == "311");
                    if (purchaseOrderServiceListModel != null)
                    {
                        string orderNo = purchaseOrderServiceListModel.MessageV2;

                        Int32.TryParse(orderNo, out iorderNo);
                    }
                    if (iorderNo > 0)
                    {
                        model.OrderNo = iorderNo;
                        data.WSResultToDb(user, model);
                        //Tum parçaları db ye karşılıklarını at
                    }
                    else
                    {
                        throw new Exception(MessageResource.PurchaseOrder_Warning_OrderNotCreated);
                    }

                }

                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;

        }

        public ResponseModel<SelectListItem> PurchaseOrderTypes(UserInfo user, string supplyType)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.PurchaseOrderTypes(user, supplyType);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<dynamic> GetPurchaseOrderTypeStockType(UserInfo user, string poType)
        {
            var response = new ResponseModel<dynamic>();
            try
            {
                response.Model = data.GetPurchaseOrderTypeStockType(user, poType);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<string> GetOfferNo(string poNumber)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.GetOfferNo(poNumber);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ServiceCallLogModel SaveFreePurchaseOrder(UserInfo user, DataTable dtMaster, DataTable dtDetail)
        {
            var logModel = new ServiceCallLogModel() { IsSuccess = true, ErrorModel = new List<ServiceCallScheduleErrorListModel>() };
            DealerBL dealerBo = new DealerBL();
            int dealerId = 0;

            List<PurchaseOrderViewModel> poList = new List<PurchaseOrderViewModel>();
            foreach (DataRow masterRow in dtMaster.Rows)
            {
                string vbeln = masterRow["VBELN"].GetValue<string>();
                string kunnr = masterRow["KUNNR"].GetValue<string>();
                string vkorg = masterRow["VKORG"].GetValue<string>();
                string vtweg = masterRow["VTWEG"].GetValue<string>();
                string spart = masterRow["SPART"].GetValue<string>();
                string auart = masterRow["AUART"].GetValue<string>();
                string augru = masterRow["AUGRU"].GetValue<string>();

                PurchaseOrderDetailViewModel detViewmModel = dataPurchaseOrderDetail.GetPurchaseOrderDetailsBySapInfo(vbeln, null,
                    null);
                if (detViewmModel.PurchaseOrderNumber == 0)
                {
                    DealerViewModel dealerModel = dealerBo.GetDealerBySSID(user, kunnr).Model;
                    if (dealerModel.DealerId != 0)
                    {
                        dealerId = dealerModel.DealerId;
                    }
                    else
                    {
                        logModel.ErrorModel.Add(new ServiceCallScheduleErrorListModel()
                        {
                            Action = MessageResource.Dealer_Display_Name + " (kunnr): " + kunnr,
                            Error = string.Format(MessageResource.PurchaseOrder_Warning_SSIDDealerNotFound, kunnr)
                        });
                        //logModel.IsSuccess = tr;
                    }

                    if (dealerModel.DealerId != 0)
                    {
                        #region Purchase Order Type

                        // TFS No : 27549 nolu change request'e istinaden artık general parameters tablosundan alınacak. OYA 10.12.2014
                        int poTypeId =
                            CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.FreePoType).Model
                                .GetValue<int>();
                        PurchaseOrderTypeViewModel potModel = new PurchaseOrderTypeViewModel();
                        potModel.PurchaseOrderTypeId = poTypeId;
                        potModel = purchaseOrderTypeBl.Get(user, potModel).Model;

                        #endregion

                        PurchaseOrderViewModel poModel = new PurchaseOrderViewModel();
                        poModel.IdDealer = dealerId;
                        poModel.IdStockType = potModel.StockTypeId;
                        poModel.PoType = potModel.PurchaseOrderTypeId;
                        poModel.IsBranchOrder = false;
                        poModel.Status = (int)CommonValues.PurchaseOrderStatus.OpenPurchaseOrder;
                        poModel.ProposalType = auart;
                        poModel.DeliveryPriority = potModel.DeliveryPriority;
                        poModel.SalesOrganization = vkorg;
                        poModel.DistrChan = vtweg;
                        poModel.Division = spart;
                        poModel.OrderReason = augru;
                        poModel.ItemCategory = potModel.ItemCategory;
                        poModel.CommandType = CommonValues.DMLType.Insert;
                        poList.Add(poModel);

                        poModel.detailList = new List<PurchaseOrderDetailViewModel>();
                        List<DataRow> detailList = (from DataRow r in dtDetail.Rows
                                                    where r["VBELN"].GetValue<string>() == vbeln
                                                    select r).ToList();
                        foreach (DataRow detailRow in detailList)
                        {
                            string matnr = detailRow["MATNR"].GetValue<string>();
                            string kwmeng = detailRow["KWMENG"].GetValue<string>();
                            string posnr = detailRow["POSNR"].GetValue<string>();

                            SparePartBL spBo = new SparePartBL();
                            SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                            spModel.PartCode = matnr;
                            spBo.GetSparePart(user, spModel);

                            PurchaseOrderDetailViewModel poDetailModel = new PurchaseOrderDetailViewModel();
                            poDetailModel.PurchaseOrderNumber = poModel.PoNumber.GetValue<int>();
                            if (spModel.PartId != 0)
                            {
                                poDetailModel.PartId = spModel.PartId;
                                poDetailModel.PackageQuantity = spModel.ShipQuantity;
                                poDetailModel.PartCode = spModel.PartCode;
                            }
                            else
                            {
                                logModel.ErrorModel.Add(new ServiceCallScheduleErrorListModel()
                                {
                                    Action = MessageResource.PurchaseOrder_Display_PoNumber + " (vbeln) : " + vbeln,
                                    Error = string.Format(MessageResource.PurchaseOrder_Warning_PartCodeNotFound, matnr)
                                });
                                logModel.IsSuccess = false;
                            }
                            poDetailModel.DesireQuantity = string.IsNullOrEmpty(kwmeng)
                                ? 0
                                : kwmeng.ToCommaString().GetValue<decimal>();
                            poDetailModel.OrderQuantity = poDetailModel.DesireQuantity;
                            poDetailModel.OrderPrice = 0;
                            poDetailModel.StatusId = (int)CommonValues.PurchaseOrderDetailStatus.Open;
                            poDetailModel.CommandType = CommonValues.DMLType.Insert;

                            poDetailModel.RowNumber = posnr;
                            poDetailModel.OrderNumber = vbeln;

                            poModel.detailList.Add(poDetailModel);
                        }
                    }
                }
            }
            var duplicatedList = (from r in poList
                                  where r.detailList.GroupBy(t => new { t.OrderNumber, t.PartCode }).Count() > 1
                                  select r into po
                                  from det in po.detailList
                                  group det by new {det.OrderNumber, det.PartCode} into grp
                                  select new { grp.Key.OrderNumber, grp.Key.PartCode, Count = grp.Count() }).ToList();
            List<ServiceCallScheduleErrorListModel> errorList =
                (from g in duplicatedList
                 where g.Count > 1
                 select new ServiceCallScheduleErrorListModel()
                 {
                     Action = MessageResource.PurchaseOrder_Display_PoNumber + " (vbeln) : " + g.OrderNumber,
                     Error = MessageResource.Error_DB_PODUniqPart + g.PartCode
                 }).ToList();

            if (errorList.Count() > 0)
            {
                logModel.ErrorModel.AddRange(errorList);
                logModel.IsSuccess = false;
            }

            if (logModel.IsSuccess)
                data.DMLPurchaseOrderAndDetails(user, poList, logModel);

            return logModel;
        }

        public ResponseModel<PurchaseOrderListModel> ReturnIsSASNoNotSentPurchaseOrders(UserInfo user)
        {
            var response = new ResponseModel<PurchaseOrderListModel>();
            try
            {
                int totalCnt = 0;
                PurchaseOrderListModel filter = new PurchaseOrderListModel();
                filter.IsSASNoSent = false;
                response.Data = data.ListPurchaseOrder(user, filter, out totalCnt);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public ResponseModel<string> GetPurchaseOrderSAPOfferNo(UserInfo user, string poNumber)
        {
            var response = new ResponseModel<string>();
            string executeSql = string.Empty;
            int totalDetCount = 0;
            try
            {
                PurchaseOrderDetailListModel detListModel = new PurchaseOrderDetailListModel();
                detListModel.PurchaseOrderNumber = poNumber.GetValue<int>();
                ResponseModel<PurchaseOrderDetailListModel> detailList = purchaseOrderDetailBl.ListPurchaseOrderDetails(user, detListModel, out totalDetCount, out executeSql);
                if (totalDetCount > 0)
                    response.Model = detailList.Data.FirstOrDefault().SAPOfferNo;
                //else
                //    throw new Exception("Satın alma bulunamadı!");

                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), executeSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;


        }

        public ResponseModel<bool> UpdatePurchaseOrderIsSASNoSentValue(UserInfo user, List<string> poNumberList, ServiceCallLogModel logModel)
        {
            var response = new ResponseModel<bool>();
            try
            {
                List<PurchaseOrderViewModel> poList = new List<PurchaseOrderViewModel>();
                foreach (var poNumber in poNumberList)
                {
                    PurchaseOrderViewModel poModel = new PurchaseOrderViewModel();
                    poModel.PoNumber = poNumber.GetValue<int>();
                    data.GetPurchaseOrder(user, poModel);
                    poModel.IsSASNoSent = true;
                    poModel.CommandType = CommonValues.DMLType.Update;
                    poList.Add(poModel);
                }
                data.UpdatePurchaseOrderIsSASNoSentValue(user, poList, logModel);
                response.Model = true;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<bool> CheckPurchaseOrderDefectPart(PurchaseOrderViewModel viewModel, long idPart)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = data.CheckPurchaseOrderDefectPart(viewModel, idPart);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
    }
}
