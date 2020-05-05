using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMS.OtokarService;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.PurchaseOrder;
using ODMSModel.PurchaseOrderSuggestion;
using ODMSModel.PurchaseOrderSuggestionDetail;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.SparePart;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PurchaseOrderSuggestionDetailController : ControllerBase
    {
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderSuggestionDetail.PurchaseOrderSuggestionDetailIndex)]
        public ActionResult PurchaseOrderSuggestionDetailIndex(Int64? mstId)
        {
            var bl = new PurchaseOrderSuggestionDetailBL();
            var model = new POSuggestionDetailViewModel();
            if (mstId != null)
            {
                model.MrpId = mstId.GetValue<long>();
                bl.GetInitialInfoSuggestionDetail(model);

                int totalCount = 0;
                var masterModel = new PurchaseOrderSuggestionListModel();
                masterModel.IdMrp = mstId;
                var masterBo = new PurchaseOrderSuggestionBL();
                List<PurchaseOrderSuggestionListModel> list = masterBo.ListPurchaseOrderSuggestion(UserManager.UserInfo, masterModel, out totalCount).Data;
                if (totalCount > 0)
                {
                    model.PoNumber = list.ElementAt(0).PoNumber.GetValue<long>();
                }
            }
            PurchaseOrderController poc = new PurchaseOrderController();
            model.CreditLimit = poc.GetCreditLimit();

            return PartialView(model);
        }

        public JsonResult ListPOSuggestionDetail([DataSourceRequest]DataSourceRequest reqeust, POSuggestionDetailListModel lModel)
        {
            var bl = new PurchaseOrderSuggestionDetailBL();
            int totalCount = 0;
            var model = new POSuggestionDetailListModel(reqeust) { MrpId = lModel.MrpId };

            long poNumber = 0;

            var masterModel = new PurchaseOrderSuggestionListModel();
            masterModel.IdMrp = lModel.MrpId;
            var masterBo = new PurchaseOrderSuggestionBL();

            List<PurchaseOrderSuggestionListModel> list = masterBo.ListPurchaseOrderSuggestion(UserManager.UserInfo, masterModel, out totalCount).Data;
            if (totalCount > 0)
            {
                poNumber = list.ElementAt(0).PoNumber.GetValue<long>();
            }

            var rValue = bl.ListPOSuggestionDetail(UserManager.UserInfo, model, out totalCount).Data;

            SparePartIndexViewModel spModel = new SparePartIndexViewModel();

            SparePartBL spBo = new SparePartBL();
            foreach (POSuggestionDetailListModel row in rValue)
            {
                string fromPartsCode = string.Empty;
                if (row.IsDivided == 1 || row.IsChanged == 1)
                {
                    row.ChangeDivideName = row.IsDivided == 1 ? MessageResource.PurchaseOrderSuggestionDetail_Display_Divided : MessageResource.PurchaseOrderSuggestionDetail_Display_Changed;
                    List<string> partIdList = row.FromParts.Split(',').ToList<string>();
                    foreach (string part in partIdList)
                    {
                        spModel.PartId = part.GetValue<int>();
                        if (spModel.PartId != 0)
                        {
                            spBo.GetSparePart(UserManager.UserInfo, spModel);
                            string partName = spModel.PartCode + CommonValues.Slash + spModel.PartNameInLanguage;
                            fromPartsCode += fromPartsCode.Length != 0 ? " , " : "";
                            fromPartsCode += partName;
                        }
                    }
                    row.FromParts = fromPartsCode;
                }
                if (poNumber == 0)
                    row.OrderQuantity = row.PropPoQuantity.GetValue<decimal>();
            }


            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });

        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderSuggestionDetail.PurchaseOrderSuggestionDetailAction)]
        public JsonResult CompletePurchaseOrderSuggestionDetail()
        {
            var bl = new PurchaseOrderSuggestionDetailBL();
            var resolveRequest = HttpContext.Request;
            var model = new POSuggestionDetailViewModel();
            var listModel = new List<POSuggestionDetailListModel>();

            resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);
            string jsonString = new StreamReader(resolveRequest.InputStream).ReadToEnd();
            {
                var serializer = new JavaScriptSerializer();
                listModel = (List<POSuggestionDetailListModel>)serializer.Deserialize(jsonString, typeof(List<POSuggestionDetailListModel>));
            }

            //Check if MRP has any part
            if (listModel.Any())
            {
                model.MrpId = listModel.Select(p => p.MrpId).FirstOrDefault();
                model.ListModel = listModel;
                model.CommandType = CommonValues.DMLType.Insert;
                bl.DMLPOSuggestionDetail(UserManager.UserInfo, model);

                if (!(model.ErrorNo > 0))
                {
                    var poBl = new PurchaseOrderBL();
                    var poModel = new PurchaseOrderViewModel()
                    {
                        PoNumber = model.PoNumber
                    };

                    poBl.GetPurchaseOrder(UserManager.UserInfo, poModel);

                    OtokarWebOrderCreate(poModel);

                    model.OrderNo = poModel.OrderNo;
                    model.ErrorNo = poModel.ErrorNo;
                    model.ErrorMessage = string.Format(MessageResource.PurchaseOrderSuggestionDetail_Warning_OtokarError, poModel.PoNumber, poModel.OrderNo);

                    if (!(model.ErrorNo > 0))
                    {
                        model.CommandType = CommonValues.DMLType.Update;
                        bl.DMLPOSuggestionDetail(UserManager.UserInfo, model);
                    }

                }
            }
            else
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.Error_DB_PurcahseOrderSuggestDetailNoPart;
            }

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success + string.Format(MessageResource.PurchaseOrderSuggestionDetail_Warning_Success, model.PoNumber, model.OrderNo));
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        private void OtokarWebOrderCreate(PurchaseOrderViewModel poModel)
        {
            //if (General.IsTest)
            //    return;

            var bl = new PurchaseOrderBL();
            var serviceModel = new PurchaseOrderServiceModel();
            var callBl = new ServiceCallScheduleBL();
            var logModel = new ServiceCallLogModel();
            try
            {
                using (var pssc = GetClient())
                {
                    string psUser = WebConfigurationManager.AppSettings["PSSCUser"];
                    string psPassword = WebConfigurationManager.AppSettings["PSSCPass"];

                    if (poModel.ErrorNo <= 0)
                    {
                        bl.SetDataForOtokarWebService(poModel);
                        //Call LOG for request
                        logModel.ReqResDic = new Dictionary<string, string>()
                            {
                                { "DealerSSID", poModel.DealerSSID },
                                { "OrderReason", poModel.OrderReason },
                                { "ProposalType", poModel.ProposalType },
                                { "SalesOrg", poModel.SalesOrganization },
                                { "DistrChan", poModel.DistrChan },
                                { "Division", poModel.Division },
                                { "ModelKod", poModel.ModelKod },
                                { "CreateDate", poModel.CreateDate.ToString("yyyyMMdd") },
                                { "ItemCategory", poModel.ItemCategory },
                                { "Description", poModel.Description },
                                { "AllDetParts", poModel.AllDetParts }
                            };
                        logModel.ServiceName = "WEB_ORDER_CREATE";// "RETAIL_PRICE_SERVICE";
                        logModel.IsManuel = true;
                        logModel.LogType = CommonValues.LogType.Request;

                        callBl.LogRequestService(logModel);

                        //Calling Otokar Purchase Order Create Service
                        DataSet rValue = pssc.ZYP_SD_WEB_ORDER_CREATE(psUser,
                            psPassword,
                            poModel.DealerSSID,//Dealer => DealerSSID*
                            string.Empty,
                            poModel.OrderReason,//PURCHASE_ORDER_MST => ORDER_REASON*
                            poModel.ProposalType,//PURCHASE_ORDER_TYPE => PROPOSAL_TYPE*
                            poModel.SalesOrganization,//model.SalesOrganization  PURCHASE_ORDER_MST => SALES_ORGANIZATION* 5100 ez
                            poModel.DistrChan,//PURCHASE_ORDER_MST => DIST_CHAN*
                            poModel.Division,//PURCHASE_ORDER_MST => DIVISION*
                            poModel.ModelKod,
                            poModel.CreateDate.ToString("yyyyMMdd"),//PURCHASE_ORDER_MST => CREATE_DATE+30*
                            poModel.DeliveryPriority.ToString().Length == 1 ? "0" + poModel.DeliveryPriority : poModel.DeliveryPriority.ToString(),//PURCHASE_ORDER_MST => DELIVERY_PRIORITY*
                            poModel.ItemCategory,//PURCHASE_ORDER_MST => ITEM_CATEG*
                            poModel.Description,//PURCHASE_ORDER_MST => DESCRIPTION*
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            "X",
                            poModel.AllDetParts//model.AllDetParts//DET 'daki tüm parça ve quantityler
                            );
                        DataTable dt = new DataTable();
                        dt = rValue.Tables["Table1"];

                        if (dt != null)
                        {
                            //Fill our model from datatable
                            serviceModel.ListModel = dt.AsEnumerable().Select(row => new PurchaseOrderServiceListModel
                            {
                                Type = row.Field<string>("TYPE"),
                                ID = row.Field<string>("ID"),
                                Number = row.Field<string>("NUMBER"),
                                Message = row.Field<string>("MESSAGE"),
                                MessageV1 = row.Field<string>("MESSAGE_V1"),
                                MessageV2 = row.Field<string>("MESSAGE_V2")

                            }).ToList();

                            serviceModel.AllParts = poModel.AllDetParts;
                            serviceModel.PoNumber = poModel.PoNumber;

                            bl.ServiceToDb(UserManager.UserInfo, serviceModel);

                            if (serviceModel.ErrorNo > 0)
                            {
                                //Log
                                logModel.LogType = CommonValues.LogType.Response;
                                logModel.LogErrorDesc = MessageResource.Error_Service_NoData;
                                callBl.LogResponseService(logModel);

                                poModel.ErrorNo = 1;
                                poModel.ErrorMessage = serviceModel.ErrorMessage;
                            }
                            poModel.OrderNo = serviceModel.OrderNo;
                            //Log
                            logModel.LogType = CommonValues.LogType.Response;
                            logModel.LogXml = XElement.Parse(rValue.ToXml());
                            callBl.LogResponseService(logModel);
                        }
                        else
                        {
                            //Log
                            logModel.LogType = CommonValues.LogType.Response;
                            logModel.LogErrorDesc = MessageResource.Error_Service_NoData;
                            callBl.LogResponseService(logModel);

                            poModel.ErrorNo = 1;
                            poModel.ErrorMessage = MessageResource.Error_Service_NoData;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                //Log
                logModel.LogType = CommonValues.LogType.Response;
                logModel.LogErrorDesc = ex.Message;
                callBl.LogResponseService(logModel);

                poModel.ErrorNo = 1;
                poModel.ErrorMessage = ex.Message;
            }
        }

    }
}
