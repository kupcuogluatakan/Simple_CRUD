using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.PDIGOSApproveGroupDealers;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PDIGOSApproveGroupDealersController : ControllerBase
    {

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIGOSApproveGroupDealers.PDIGOSApproveGroupDealersIndex, CommonValues.PermissionCodes.PDIGOSApproveGroupDealers.PDIGOSApproveGroupDealersIndex)]
        public ActionResult ListPDIGOSApproveGroupDealers([DataSourceRequest] DataSourceRequest request, PDIGOSApproveGroupDealersListModel model)
        {
            var guaranteeAuthorityGroupDealersBo = new PDIGOSApproveGroupDealersBL();
            var v = new PDIGOSApproveGroupDealersListModel(request) { IdGroup = model.IdGroup };

            var returnValue = guaranteeAuthorityGroupDealersBo.ListPDIGOSApproveGroupDealers(v).Data;

            return Json(new
            {
                Data = returnValue
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIGOSApproveGroupDealers.PDIGOSApproveGroupDealersIndex, CommonValues.PermissionCodes.PDIGOSApproveGroupDealers.PDIGOSApproveGroupDealersIndex)]
        public ActionResult ListPDIGOSApproveGroupDealersNotInclude([DataSourceRequest] DataSourceRequest request, PDIGOSApproveGroupDealersListModel model)
        {
            var guaranteeAuthorityGroupDealersBo = new PDIGOSApproveGroupDealersBL();
            var v = new PDIGOSApproveGroupDealersListModel(request) { IdGroup = model.IdGroup };
            var returnValue = guaranteeAuthorityGroupDealersBo.ListPDIGOSApproveGroupDealersNotInclude(v).Data;

            return Json(new
            {
                Data = returnValue
            });
        }

        public ActionResult PDIGOSApproveGroupDealersIndex(int id)
        {
            return View(id);
        }

        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.PDIGOSApproveGroupDealers.PDIGOSApproveGroupDealersIndex, CommonValues.PermissionCodes.PDIGOSApproveGroupDealers.PDIGOSApproveGroupDealersSave)]
        public ActionResult SavePDIGOSApproveGroupDealers(PDIGOSApproveGroupDealersSaveModel model)
        {

            var bo = new PDIGOSApproveGroupDealersBL();
            try
            {
                bo.SavePDIGOSApproveGroupDealers(UserManager.UserInfo, model);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            }
            catch
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Global_Display_Error);
            }
        }


    }
}