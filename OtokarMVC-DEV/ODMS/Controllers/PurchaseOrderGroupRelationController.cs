using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.PurchaseOrderGroupRelation;

namespace ODMS.Controllers
{
    public class PurchaseOrderGroupRelationController : ControllerBase
    {
        private readonly PurchaseOrderGroupBL _purchaseOrderGroupService = new PurchaseOrderGroupBL();
        private readonly PurchaseOrderGroupRelationBL _purchaseOrderGroupRelationService = new PurchaseOrderGroupRelationBL();

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderGroupRelation.PurchaseOrderGroupRelationIndex)]
        public ActionResult PurchaseOrderGroupRelationIndex()
        {
            ViewBag.PurchaseOrderGroupList = _purchaseOrderGroupService.PurchaseOrderGroupList().Data;

            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderGroupRelation.PurchaseOrdeGroupRelationSelect)]
        public ActionResult ListGroupIncluededDealer([DataSourceRequest] DataSourceRequest request, PurchaseOrderGroupRelationListModel model)
        {
            var returnValue = _purchaseOrderGroupRelationService.ListOfIncluded(model).Data;

            return Json(new
            {
                Data = returnValue
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderGroupRelation.PurchaseOrdeGroupRelationSelect)]
        public ActionResult ListGroupNotIncluededDealer([DataSourceRequest] DataSourceRequest request, PurchaseOrderGroupRelationListModel model)
        {
            var returnValue = _purchaseOrderGroupRelationService.ListOfNotIncluded(model).Data;

            return Json(new
            {
                Data = returnValue
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderGroupRelation.PurchaseOrderGroupRelationCreate)]
        public ActionResult SaveGroupDealers(PurchaseGroupRelationSaveModel model)
        {
            _purchaseOrderGroupRelationService.Update(model);

            return model.ErrorNo == 0 ? GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success) :
                                        GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Global_Display_Error);
        }
    }
}
