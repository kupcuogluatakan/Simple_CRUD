using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSBusiness.Business;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel;
using ODMSModel.SaleOrder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Permission = ODMSCommon.CommonValues.PermissionCodes.SaleOrder;

namespace ODMS.Controllers
{
    public class SaleOrderController : ControllerBase
    {
        #region Sale Order Remaining
        [HttpGet]
        [AuthorizationFilter(Permission.SaleOrderRemainingIndex)]
        public ActionResult SaleOrderRemaining(int? stockTypeId, string partCode)
        {
            var bus = new SaleOrderBL();
            LoadFilterCombos(bus);
            return stockTypeId.HasValue && !string.IsNullOrEmpty(partCode)
                ? View(new SaleOrderRemainingFilter { StockTypeId = stockTypeId, PartCode = partCode })
                : View();

        }
        [AuthorizationFilter(Permission.SaleOrderRemainingIndex)]
        public JsonResult ListRemainingSaleOrders([DataSourceRequest] DataSourceRequest request, SaleOrderRemainingFilter model)
        {
            var v = new SaleOrderRemainingFilter(request)
            {
                BeginDate = model.BeginDate,
                CustomerId = model.CustomerId,
                EndDate = model.EndDate,
                MaxRecordCount = model.MaxRecordCount,
                PartCode = model.PartCode,
                PartName = model.PartName,
                PartType = model.PartType,
                StockTypeId = model.StockTypeId,
                PurchaseOrderType = model.PurchaseOrderType
            };

            int totalCount = 0;
            var dto = new SaleOrderBL().ListSaleOrderRemaining(UserManager.UserInfo, v, out totalCount).Data;
            return Json(new { Data = dto, Total = totalCount });
        }
        private void LoadFilterCombos(SaleOrderBL bus)
        {
            ViewBag.CustomerList = bus.ListSaleOrderCustomers(UserManager.UserInfo).Data;
            ViewBag.PurchaseOrderTypeList = bus.ListPurchaseOrderTypes(UserManager.UserInfo).Data;
            ViewBag.PartTypeList = bus.ListPartTypes().Data;
            ViewBag.StockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
        }
        [HttpPost]
        [AuthorizationFilter(Permission.SaleOrderRemainingIndex)]
        public ActionResult CreateSaleOrderDocument()
        {
            var list = ParseModelFromRequestInputStream<List<SaleOrderRemainingListItem>>();
            var bus = new SaleOrderBL();

            ModelBase model = bus.CreateSaleOrderDocument(UserManager.UserInfo, list).Model;

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                model.ErrorMessage);

        }

        [HttpPost]
        [AuthorizationFilter(Permission.SaleOrderRemainingIndex)]
        public JsonResult GetSelectedSaleOrderPartsStockQuants()
        {
            var list = ParseModelFromRequestInputStream<List<SaleOrderRemainingListItem>>();
            var bus = new SaleOrderBL();
            var argument = String.Join(",", list.Select(c => c.SoDetSeqNo));

            return Json(bus.ListSelectedSaleOrderPartsStockQuants(argument).Data);

        }
        #endregion

    }
}
