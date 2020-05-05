using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.GuaranteeAuthorityGroupDealers;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class GuaranteeAuthorityGroupDealersController : ControllerBase
    {

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeAuthorityGroupDealers.GuaranteeAuthorityGroupDealersIndex, CommonValues.PermissionCodes.GuaranteeAuthorityGroupDealers.GuaranteeAuthorityGroupDealersIndex)]
        public ActionResult ListGuaranteeAuthorityGroupDealers([DataSourceRequest] DataSourceRequest request, GuaranteeAuthorityGroupDealersListModel model)
        {
            var guaranteeAuthorityGroupDealersBo = new GuaranteeAuthorityGroupDealersBL();
            var v = new GuaranteeAuthorityGroupDealersListModel(request) { IdGroup = model.IdGroup };

            var returnValue = guaranteeAuthorityGroupDealersBo.ListGuaranteeAuthorityGroupDealers(v).Data;

            return Json(new
            {
                Data = returnValue
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeAuthorityGroupDealers.GuaranteeAuthorityGroupDealersIndex, CommonValues.PermissionCodes.GuaranteeAuthorityGroupDealers.GuaranteeAuthorityGroupDealersIndex)]
        public ActionResult ListGuaranteeAuthorityGroupDealersNotInclude([DataSourceRequest] DataSourceRequest request, GuaranteeAuthorityGroupDealersListModel model)
        {
            var guaranteeAuthorityGroupDealersBo = new GuaranteeAuthorityGroupDealersBL();
            var v = new GuaranteeAuthorityGroupDealersListModel(request) { IdGroup = model.IdGroup };
            var returnValue = guaranteeAuthorityGroupDealersBo.ListGuaranteeAuthorityGroupDealersNotInclude(v).Data;

            return Json(new
            {
                Data = returnValue
            });
        }

        public ActionResult GuaranteeAuthorityGroupDealersIndex(int id)
        {
            return View(id);
        }

        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.GuaranteeAuthorityGroupDealers.GuaranteeAuthorityGroupDealersIndex, CommonValues.PermissionCodes.GuaranteeAuthorityGroupDealers.GuaranteeAuthorityGroupDealersSave)]
        public ActionResult SaveGuaranteeAuthorityGroupDealers(GuaranteeAuthorityGroupDealersSaveModel model)
        {

            var bo = new GuaranteeAuthorityGroupDealersBL();
            try
            {
                bo.SaveGuaranteeAuthorityGroupDealers(UserManager.UserInfo, model);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            }
            catch
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Global_Display_Error);
            }
        }
    }
}