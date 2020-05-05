using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.GuaranteeAuthorityGroupVehicleModels;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class GuaranteeAuthorityGroupVehicleController : ControllerBase
    {

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeAuthorityGroupVehicle.GuaranteeAuthorityGroupVehicleIndex, CommonValues.PermissionCodes.GuaranteeAuthorityGroupVehicle.GuaranteeAuthorityGroupVehicleIndex)]
        public ActionResult ListGuaranteeAuthorityGroupVehicle([DataSourceRequest] DataSourceRequest request, GuaranteeAuthorityGroupVehicleListModel model)
        {
            var guaranteeAuthorityGroupVehicleBo = new GuaranteeAuthorityGroupVehicleBL();
            var v = new GuaranteeAuthorityGroupVehicleListModel(request) { IdGroup = model.IdGroup };

            var returnValue = guaranteeAuthorityGroupVehicleBo.ListGuaranteeAuthorityGroupVehicle(v).Data;

            return Json(new
            {
                Data = returnValue
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeAuthorityGroupVehicle.GuaranteeAuthorityGroupVehicleIndex, CommonValues.PermissionCodes.GuaranteeAuthorityGroupVehicle.GuaranteeAuthorityGroupVehicleIndex)]
        public ActionResult ListGuaranteeAuthorityGroupVehicleNotInclude([DataSourceRequest] DataSourceRequest request, GuaranteeAuthorityGroupVehicleListModel model)
        {
            var guaranteeAuthorityGroupVehicleBo = new GuaranteeAuthorityGroupVehicleBL();
            var v = new GuaranteeAuthorityGroupVehicleListModel(request) { IdGroup = model.IdGroup };
            var returnValue = guaranteeAuthorityGroupVehicleBo.ListGuaranteeAuthorityGroupVehicleNotInclude(v).Data;

            return Json(new
            {
                Data = returnValue
            });
        }

        public ActionResult GuaranteeAuthorityGroupVehicleIndex(int id)
        {
            return View(id);
        }

        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.GuaranteeAuthorityGroupVehicle.GuaranteeAuthorityGroupVehicleIndex, CommonValues.PermissionCodes.GuaranteeAuthorityGroupVehicle.GuaranteeAuthorityGroupVehicleSave)]
        public ActionResult SaveGuaranteeAuthorityGroupVehicle(GuaranteeAuthorityGroupVehicleSaveModel model)
        {

            var bo = new GuaranteeAuthorityGroupVehicleBL();
            try
            {
                bo.SaveGuaranteeAuthorityGroupVehicle(UserManager.UserInfo, model);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            }
            catch
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Global_Display_Error);
            }
        }

    }
}