using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Kendo.Mvc.UI;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.WorkOrderPicking;
using ODMSModel.WorkOrderPickingDetail;
using ODMS.Filters;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class WorkOrderPickingDetailController : ControllerBase
    {
        //
        // GET: /WorkOrderPickingDetail/

        public ActionResult WorkOrderPickingDetailIndex(Int64? id, int? statusId, int? isReturn)
        {
            var model = new WorkOrderPickingDetailListModel
            {
                WOPMstId = id.GetValue<int>(),
                ParentStatusId = statusId.GetValue<int>(),
                IsReturn = isReturn != 0
            };
            ViewBag.AllowQuantityEdit = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.AllowStockPickQuantityEdit).Model;

            return PartialView(model);
        }

        public ActionResult ListWorkOrderPickingDetail([DataSourceRequest]DataSourceRequest request, WorkOrderPickingDetailListModel hModel)
        {
            var bl = new WorkOrderPickingDetailBL();
            var model = new WorkOrderPickingDetailListModel(request) { WOPMstId = hModel.WOPMstId };
            int totalCount = 0;

            var rValue = bl.ListWorkOrderPickingDetail(UserManager.UserInfo,model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }
        public ActionResult ListWorkOrderPickingDetailSub(WOPDetSubListModel hModel)
        {
            var bl = new WorkOrderPickingDetailBL();
            var model = new WOPDetSubListModel { WOPDetId = hModel.WOPDetId };
            int totalCount = 0;

            var rValue = bl.ListWorkOrderPickingDetailSub(UserManager.UserInfo, model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        [HttpPost]
        public JsonResult SaveWorkOrderPickingDetail()
        {
            var bl = new WorkOrderPickingDetailBL();
            var resolveRequest = HttpContext.Request;
            var model = new WOPDetSubViewModel();
            var listModel = new List<WOPDetSubListModel>();
            resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);
            string jsonString = new StreamReader(resolveRequest.InputStream).ReadToEnd();
            {
                var serializer = new JavaScriptSerializer();
                listModel = (List<WOPDetSubListModel>)serializer.Deserialize(jsonString, typeof(List<WOPDetSubListModel>));
            }

            //Check Total Quantity for all sub row and match thir parent request quantity
            var errListModel = listModel.GroupBy(p => p.WOPDetId).Select(c => new WOPDetSubListModel
            {
                Quantity = c.Sum(y => y.Quantity),
                Text = c.Select(p => p.Text).FirstOrDefault(),
                WOPDetId = c.Select(p => p.WOPDetId).FirstOrDefault()
            }).Where(p => Convert.ToDouble(p.Quantity) > Convert.ToDouble(p.Text));



            if (errListModel.Any())//Quantity not Match
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.Error_DB_WorkOrderPickingDetailQuantity;
            }
            else if (listModel.All(c => c.Value == "0" || c.Value == ""))
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.Error_DB_WorkOrderPickingDetailSelectWarehouse;
            }
            else//Quantity Match
            {
                model.ListSubModel = listModel.Where(c => !(c.Value == "0" || c.Value == "")).ToList();
                bl.DMLWOPDetSub(UserManager.UserInfo,model);
                if (model.ErrorNo > 0)
                {
                    errListModel = model.ListSubModel;
                }

            }

            return Json(new
            {
                ErrorNo = model.ErrorNo,
                ErrorMessage = model.ErrorMessage,
                Data = errListModel
            });

            //if (model.ErrorNo == 0)
            //    return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            //return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpGet]
        public JsonResult SaveWorkOrderPickingDetail_ForWebTest(int detailId, int quantity, int resultId, string value, string text)
        {
            var bl = new WorkOrderPickingDetailBL();
            var resolveRequest = HttpContext.Request;
            var model = new WOPDetSubViewModel();
            var listModel = new List<WOPDetSubListModel>();
            var detail = new WOPDetSubListModel();
            detail.WOPDetId = detailId;
            detail.Quantity = quantity;
            detail.ResultId = resultId;
            detail.Text = text;
            detail.Value = value;
            listModel.Add(detail);

            //Check Total Quantity for all sub row and match thir parent request quantity
            var errListModel = listModel.GroupBy(p => p.WOPDetId).Select(c => new WOPDetSubListModel
            {
                Quantity = c.Sum(y => y.Quantity),
                Text = c.Select(p => p.Text).FirstOrDefault(),
                WOPDetId = c.Select(p => p.WOPDetId).FirstOrDefault()
            }).Where(p => Convert.ToDouble(p.Quantity) > Convert.ToDouble(p.Text));



            if (errListModel.Any())//Quantity not Match
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.Error_DB_WorkOrderPickingDetailQuantity;
            }
            else if (listModel.All(c => c.Value == "0" || c.Value == ""))
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.Error_DB_WorkOrderPickingDetailSelectWarehouse;
            }
            else//Quantity Match
            {
                model.ListSubModel = listModel.Where(c => !(c.Value == "0" || c.Value == "")).ToList();
                bl.DMLWOPDetSub(UserManager.UserInfo,model);
                if (model.ErrorNo > 0)
                {
                    errListModel = model.ListSubModel;
                }

            }

            return Json(new
            {
                ErrorNo = model.ErrorNo,
                ErrorMessage = model.ErrorMessage,
                Data = errListModel
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteWorkOrderPickingDetail(int mstId, int? dealerId)
        {
            var bl = new WorkOrderPickingDetailBL();
            var model = new WorkOrderPickingViewModel { WorkOrderPickingId = mstId };
            model.DealerId = dealerId.HasValue ? dealerId.Value : 0;
            bl.CompleteWorkOrderPicking(UserManager.UserInfo,model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpPost]
        public JsonResult WorkOrderPickingDetailDelete(int id)
        {
            var bl = new WorkOrderPickingDetailBL();
            var model = new WOPDetSubViewModel { CommandType = CommonValues.DMLType.Delete, ResultId = id };

            bl.DeleteWOPDetSub(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);

        }

        [HttpPost]
        public JsonResult ListRackByPart(int id)
        {
            var bl = new WorkOrderPickingDetailBL();

            return Json(bl.ListRackWarehouseByDetId(UserManager.UserInfo,id).Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult StockCardDefaultRackReturn(int partId)
        {
            var bl = new WorkOrderPickingDetailBL();
            int value = 0;
            string text = string.Empty;

            bl.StockCardDefaultRackReturn(UserManager.UserInfo,partId, out value, out text);

            return Json(new
            {
                Value = value,
                Text = text
            });
        }

        [HttpPost]
        public JsonResult DefaultRack(Int64 mstId)
        {

            var bl = new WorkOrderPickingDetailBL();
            var model = new WorkOrderPickingViewModel()
            {
                CommandType = "R",//DEFAULT RACK
                WorkOrderPickingId = mstId
            };

            bl.WorkOrderPickingDetailRack(UserManager.UserInfo,model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);

        }


    }
}
