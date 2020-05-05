using System.IO;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Common;
using ODMSModel.FleetRequestVehicle;
using ODMSModel.WorkOrderDocuments;
using Perm = ODMSCommon.CommonValues.PermissionCodes.FleetRequestVehicle;
using ODMSModel.Customer;
using ODMSModel.Vehicle;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class FleetRequestVehicleController : ControllerBase
    {
        #region Get Actions

        [HttpGet]
        [AuthorizationFilter(Perm.FleetRequestVehicleIndex)]
        public ActionResult FleetRequestVehicleIndex(int id = 0)//Fleet Request Id
        {
            var status = new FleetRequestVehicleBL().GetFleetRequestStatus(id).Model;
            return PartialView(new FleetRequestVehicleViewModel { HideElements = id == 0 || status == null, FleetRequestId = id, FleetRequestStatus = status });
        }

        [HttpGet]
        [AuthorizationFilter(Perm.FleetRequestVehicleIndex, Perm.FleetRequestVehicleCreate)]
        public ActionResult FleetRequestVehicleCreate(int fleetRequestId)
        {
            ViewBag.FleetRequestId = fleetRequestId;
            return View();
        }

        [HttpGet]
        [AuthorizationFilter(Perm.FleetRequestVehicleIndex, Perm.FleetRequestVehicleUpdate)]
        public ActionResult FleetRequestVehicleUpdate(int fleetRequestVehicleId)
        {
            var model = new FleetRequestVehicleBL().GetFleetRequestVehicle(fleetRequestVehicleId).Model;
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(Perm.FleetRequestVehicleIndex, Perm.FleetRequestVehicleDetails)]
        public ActionResult FleetRequestVehicleDetails(int fleetRequestVehicleId)
        {
            var model = new FleetRequestVehicleBL().GetFleetRequestVehicle(fleetRequestVehicleId).Model;
            return View(model);
        }

        #endregion

        #region Post Actions

        [HttpPost]
        [AuthorizationFilter(Perm.FleetRequestVehicleIndex, Perm.FleetRequestVehicleCreate)]
        //[ValidateAntiForgeryToken]
        public ActionResult FleetRequestVehicleCreate(FleetRequestVehicleViewModel model)
        {

            CustomerIndexViewModel custModel = new CustomerIndexViewModel();
            custModel.CustomerId = model.CustomerId;

            CustomerBL custBo = new CustomerBL();
            custBo.GetCustomer(UserManager.UserInfo, custModel);
            model.CustomerName = custModel.CustomerName;

            VehicleIndexViewModel vecModel = new VehicleIndexViewModel();
            vecModel.VehicleId = model.VehicleId;

            VehicleBL vecBo = new VehicleBL();
            vecBo.GetVehicle(UserManager.UserInfo, vecModel);
            model.VehicleName = vecModel.VinNo;

            ViewBag.FleetRequestId = model.FleetRequestId;
            if (model.Document == null)
            {
                ModelState.AddModelError("Document", "*");
            }

            if (ModelState.IsValid)
            {
                model.DocumentId = SaveDocument(model.Document);
                model.CommandType = CommonValues.DMLType.Insert;
                new FleetRequestVehicleBL().DMLFleetRequestVehicle(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                return RedirectToAction("FleetRequestVehicleCreate", new { fleetRequestId = model.FleetRequestId });
            }
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(Perm.FleetRequestVehicleIndex, Perm.FleetRequestVehicleCreate)]
        //[ValidateAntiForgeryToken]
        public ActionResult FleetRequestVehicleUpdate(FleetRequestVehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Document != null)
                {
                    model.DocumentId = SaveDocument(model.Document);
                }

                model.CommandType = CommonValues.DMLType.Update;
                new FleetRequestVehicleBL().DMLFleetRequestVehicle(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                return RedirectToAction("FleetRequestVehicleUpdate", new { fleetRequestVehicleId = model.FleetRequestVehicleId });
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AuthorizationFilter(Perm.FleetRequestVehicleIndex, Perm.FleetRequestVehicleDelete)]
        public ActionResult FleetRequestVehicleDelete(int? FleetRequestVehicleId)
        {
            if (!(FleetRequestVehicleId.HasValue && FleetRequestVehicleId > 0))
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_NoRecordFound);
            }

            var bus = new FleetRequestVehicleBL();
            var model = new FleetRequestVehicleViewModel
            {
                FleetRequestVehicleId = FleetRequestVehicleId ?? 0,
                CommandType = CommonValues.DMLType.Delete
            };

            bus.DMLFleetRequestVehicle(UserManager.UserInfo, model);

            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpPost]
        [AuthorizationFilter(Perm.FleetRequestVehicleIndex)]
        public ActionResult ListFleetRequestVehicles([DataSourceRequest]DataSourceRequest request, int fleetRequestId)
        {
            var bus = new FleetRequestVehicleBL();
            var model = new FleetRequestVehicleListModel(request) { FleetRequestId = fleetRequestId };
            var totalCnt = 0;
            var returnValue = bus.ListFleetRequestVehicle(model, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        private int SaveDocument(HttpPostedFileBase document)
        {
            MemoryStream target = new MemoryStream();
            document.InputStream.CopyTo(target);
            byte[] data = target.ToArray();

            var documentInfo = new DocumentInfo()
            {
                DocBinary = data,
                DocMimeType = document.ContentType,
                DocName = document.FileName,
                CommandType = CommonValues.DMLType.Insert
            };

            DocumentBL documentBo = new DocumentBL();
            documentBo.DMLDocument(UserManager.UserInfo, documentInfo);
            return documentInfo.DocId;
        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.FleetRequestVehicle.FleetRequestVehicleIndex)]
        public ActionResult DownloadVehicleDocument(int id)
        {
            var woDocBL = new WorkOrderDocumentsBL();
            var model = new WorkOrderDocumentsViewModel { DocId = id };

            woDocBL.GetWorkOrderDocument(model);

            return File(model.DocImage, model.DocMimeType, model.DocName);
        }

    }
}
