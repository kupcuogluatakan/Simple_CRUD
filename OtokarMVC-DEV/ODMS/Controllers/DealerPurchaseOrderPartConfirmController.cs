using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.DealerPurchaseOrderPartConfirm;
using ODMSModel.PurchaseOrder;
using ODMSModel.PurchaseOrderDetail;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DealerPurchaseOrderPartConfirmController : ControllerBase
    {
        #region DealerPurchaseOrderPartConfirm Index

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerPurchaseOrderPartConfirm.DealerPurchaseOrderPartConfirmIndex)]
        [HttpGet]
        public ActionResult DealerPurchaseOrderPartConfirmIndex(long? poNumber)
        {
            var orderBl = new PurchaseOrderBL();
            var orderModel = new PurchaseOrderViewModel { PoNumber = poNumber.GetValue<long>() };
            orderBl.GetPurchaseOrder(UserManager.UserInfo, orderModel);

            var model = new DealerPurchaseOrderPartConfirmListModel
            {
                PoNumber = poNumber.GetValue<long>(),
                OrderStatusId = orderModel.Status.GetValue<int>(),
                SupplierDealerConfirm = orderModel.SupplierDealerConfirm
            };

            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerPurchaseOrderPartConfirm.DealerPurchaseOrderPartConfirmIndex)]
        public ActionResult ListDealerPurchaseOrderPartConfirm([DataSourceRequest] DataSourceRequest request, DealerPurchaseOrderPartConfirmListModel model)
        {
            var dealerPurchaseOrderPartConfirmBo = new DealerPurchaseOrderPartConfirmBL();
            var v = new DealerPurchaseOrderPartConfirmListModel(request);
            var totalCnt = 0;
            v.PoNumber = model.PoNumber;
            var returnValue = dealerPurchaseOrderPartConfirmBo.ListDealerPurchaseOrderPartConfirms(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [AuthorizationFilter(
            CommonValues.PermissionCodes.DealerPurchaseOrderPartConfirm.DealerPurchaseOrderPartConfirmSave)]
        [HttpPost]
        public ActionResult DealerPurchaseOrderPartConfirmSave([DataSourceRequest] DataSourceRequest request,
                                                               [Bind(Prefix = "models")] IEnumerable<DealerPurchaseOrderPartConfirmListModel>
                                                                   dealerPurchaseOrderPartConfirmList)
        {
            PurchaseOrderViewModel orderModel = new PurchaseOrderViewModel();
            List<PurchaseOrderDetailViewModel> orderDetailList = new List<PurchaseOrderDetailViewModel>();

            long poNumber = dealerPurchaseOrderPartConfirmList.ElementAt(0).PoNumber;

            SaveResults(orderDetailList, dealerPurchaseOrderPartConfirmList, poNumber);
            DealerPurchaseOrderPartConfirmBL orderPartBl = new DealerPurchaseOrderPartConfirmBL();
            orderPartBl.DMLDealerPurchaseOrderPartConfirm(UserManager.UserInfo, orderModel, orderDetailList);

            return Json(new
            {
                OprErrorNo = orderModel.ErrorNo,
                OprErrorMessage = orderModel.ErrorMessage
            });
        }

        private static void SaveResults(
            List<PurchaseOrderDetailViewModel> orderDetailList,
            IEnumerable<DealerPurchaseOrderPartConfirmListModel> dealerPurchaseOrderPartConfirmList, long poNumber)
        {
            PurchaseOrderDetailBL orderDetailBl = new PurchaseOrderDetailBL();

            foreach (var result in dealerPurchaseOrderPartConfirmList)
            {
                PurchaseOrderDetailViewModel orderDetailModel = new PurchaseOrderDetailViewModel
                {
                    PurchaseOrderNumber = poNumber.GetValue<int>(),
                    PurchaseOrderDetailSeqNo = result.PurchaseOrderDetailSeqNo.GetValue<int>()
                };
                orderDetailBl.GetPurchaseOrderDetail(UserManager.UserInfo, orderDetailModel);
                orderDetailModel.ShipmentQuantity = result.ShipmentQuantity;
                //orderDetailModel.StatusId = (int) CommonValues.PurchaseOrderDetailStatus.Closed;
                orderDetailModel.CommandType = CommonValues.DMLType.Update;
                orderDetailList.Add(orderDetailModel);
            }
        }
        #endregion

    }
}