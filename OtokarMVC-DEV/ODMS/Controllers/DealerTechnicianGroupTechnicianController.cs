using System.Collections.Generic;
using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Resources;
using ODMSModel.Common;
using Perm = ODMSCommon.CommonValues.PermissionCodes.DealerTechnicianGroupTechnician;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DealerTechnicianGroupTechnicianController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(Perm.DealerTechnicianGroupTechnicianIndex)]
        public ActionResult DealerTechnicianGroupTechnicianIndex(int id = 0)
        {
            ViewBag.DealerTechnicianGroupId = id;
            ViewBag.DealerTechnicianGroups = new DealerTechnicianGroupTechnicianBL().ListDealerTechnicianGroupsAsSelectListItem(UserManager.UserInfo).Data;

            return View();
        }
        [HttpPost]
        [AuthorizationFilter(Perm.DealerTechnicianGroupTechnicianIndex)]
        public ActionResult ListTechnicianGroupTechniciansIncluded(int id = 0)
        {
            return id == 0
                ? Json(new { Data = new List<ComboBoxModel>() })
                : Json(new { Data = new DealerTechnicianGroupTechnicianBL().ListTechnicianGroupTechniciansIncluded(UserManager.UserInfo, id).Data });
        }
        [HttpPost]
        [AuthorizationFilter(Perm.DealerTechnicianGroupTechnicianIndex)]
        public ActionResult ListTechnicianGroupTechniciansExcluded(int id = 0)
        {
            return id == 0
                ? Json(new { Data = new List<ComboBoxModel>() })
                : Json(new { Data = new DealerTechnicianGroupTechnicianBL().ListTechnicianGroupTechniciansExcluded(UserManager.UserInfo, id).Data });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Perm.DealerTechnicianGroupTechnicianIndex)]
        public ActionResult SaveTechnicianGroupTechnicians(int dealerTechnicianGroupId, List<int> TechinicanIds)
        {

            var model = new DealerTechnicianGroupTechnicianBL().SaveTechnicianGroupTechnicians(UserManager.UserInfo, dealerTechnicianGroupId, TechinicanIds).Model;

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);

        }
    }
}
