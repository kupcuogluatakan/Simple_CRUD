using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness.Business;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.SparePartClassCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PartWithModelPairingController : ControllerBase

    {
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.PartWithModelPairing.PartWithModelPairingIndex)]
        public ActionResult PartWithModelParingIndex()
        {
            var model = new PartWithModelPairingBL().ListCodes(UserManager.UserInfo).Data;
            return View(model);
        }

        public ActionResult GetJanpolVal(string Code)
        {
            var model = new PartWithModelPairingBL().ListCodes(UserManager.UserInfo).Data.Where(x => x.Code == Code).FirstOrDefault();
            return Json(new { Data = model.IsJanpol });
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.PartWithModelPairing.PartWithModelPairingIndex)]
        public ActionResult ListPartWithModelPairingIncluded([DataSourceRequest] DataSourceRequest request, string code)
        {
            if (code != null || code != "")
            {
                var bo = new PartWithModelPairingBL();
                var result = bo.ListIncludedCode(UserManager.UserInfo, code.ToString()).Data;
                return Json(new { Data = result });
            }
            else
            {
                return Json(new { Data = new List<SparePartClassCodeListModel>() });
            }

        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.PartWithModelPairing.PartWithModelPairingIndex)]
        public ActionResult ListPartWithModelPairingNotIncluded([DataSourceRequest] DataSourceRequest request, string code)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                var bo = new PartWithModelPairingBL();
                var result = bo.ListNotIncludedCode(UserManager.UserInfo, code.ToString()).Data;
                return Json(new { Data = result });
            }
            else
            {
                return Json(new { Data = new List<SparePartClassCodeListModel>() });
            }
        }

        public ActionResult Save(SaveModel model)
        {
            var bo = new PartWithModelPairingBL();
            try
            {
                bo.Save(UserManager.UserInfo, model);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            }
            catch (Exception)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.RolePermission_Save_Error);
            }
        }

        public ActionResult JanpolRegister(string partClass, bool isJanpol)
        {
            try
            {
                var bo = new PartWithModelPairingBL();

                bo.JanpolRegister(UserManager.UserInfo, partClass, isJanpol);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);

            }
            catch (Exception ex)
            {

                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.RolePermission_Save_Error);
            }
        }


    }
}