using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMSBusiness;
using ODMSModel.DealerFleetVehicle;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    public class DealerFleetVehicleController : ControllerBase
    {
        public ActionResult DealerFleetVehicleIndex(int id)
        {
            var model = new DealerFleetVehicleListModel()
            {
                FleetId = id
            };
            return PartialView(model);
        }

        public ActionResult ListDealerFleetVehicle([DataSourceRequest]DataSourceRequest request, DealerFleetVehicleListModel hModel)
        {
            int totalCount = 0;
            var bl = new DealerFleetVehicleBL();

            var model = new DealerFleetVehicleListModel(request)
            {
                FleetId = hModel.FleetId
            };

            var rValue = bl.ListDealerFleetVehicle(UserManager.UserInfo, model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }
    }
}
