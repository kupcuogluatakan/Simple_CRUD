using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.AnnouncementDealer;
using Permission = ODMSCommon.CommonValues.PermissionCodes.AnnouncementDealer;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class AnnouncementDealerController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(Permission.AnnouncementDealerIndex)]
        public ActionResult AnnouncementDealerIndex(int id = 0, int isActive = 0)
        {
            if (id == 0)
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
            }
            else
            {
                var customerGroupList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.CustomerGroupLookup).Data;
                customerGroupList.Insert(0, new SelectListItem { Selected = true, Value = "0", Text = MessageResource.Global_Display_Select });
                ViewBag.CustomerGroupList = customerGroupList;

                var vehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
                vehicleModelList.Insert(0, new SelectListItem { Selected = true, Value = "", Text = MessageResource.Global_Display_Select });
                ViewBag.VehicleModelList = vehicleModelList;
            }
            ViewBag.MasterIsActive = isActive;
            ViewBag.AnnouncementId = id;
            return View();
        }

        [HttpPost]
        [AuthorizationFilter(Permission.AnnouncementDealerIndex)]
        public ActionResult ListDealersIncluded(int announcementId = 0)
        {
            return announcementId == 0
                ? Json(new { Data = new List<SelectListItem>() })
                : Json(new { Data = new AnnouncementDealerBL().ListAnnouncementDealersIncluded(announcementId).Data });
        }
        [HttpPost]
        [AuthorizationFilter(Permission.AnnouncementDealerIndex)]
        public ActionResult ListDealersExcluded(int announcementId = 0, int customerGroupId = 0, string vehicleModelId = "")
        {
            return announcementId == 0
                ? Json(new { Data = new List<SelectListItem>() })
                : Json(
                    new
                    {
                        Data = new AnnouncementDealerBL().ListAnnouncementDealersExcluded(announcementId, customerGroupId, vehicleModelId).Data
                    });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.AnnouncementDealerIndex, Permission.AnnouncementDealerSave)]
        public ActionResult Save(AnnouncementDealerModel model)
        {
            if (model.AnnouncementId > 0)
            {
                model.DealerList = model.DealerList != null
                    ? model.DealerList.Distinct().ToList()
                    : null;
                new AnnouncementDealerBL().Save(UserManager.UserInfo, model);
                if (model.ErrorNo > 0)
                {
                    return Json(new { Result = false, Message = model.ErrorMessage });
                }
                return Json(new { Result = true, Message = MessageResource.Global_Display_Success });
            }
            return Json(new { Result = false, Message = MessageResource.Error_DB_NoRecordFound });
        }

    }
}
