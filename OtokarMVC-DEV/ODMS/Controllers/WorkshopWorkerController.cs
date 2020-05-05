using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.WorkshopWorker;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class WorkshopWorkerController : ControllerBase
    {
        private void SetDefaults()
        {
            var bo = new WorkshopWorkerBL();
            ViewBag.WorkshopList = bo.GetWorkshopList().Data;
            ViewBag.WorkerList = bo.GetWorkerList().Data;
        }
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkshopWorker.WorkshopWorkerIndex)]
        public ActionResult WorkshopWorkerIndex()
        {
            var bo = new WorkshopWorkerBL();
            var model = bo.GetWorkshopWorkerIndexModel().Model;
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkshopWorker.WorkshopWorkerIndex, CommonValues.PermissionCodes.WorkshopWorker.WorkshopWorkerCreate)]
        public ActionResult WorkshopWorkerCreate()
        {
            SetDefaults();
            var model = new WorkshopWorkerDetailModel {IsActive = true};
            return View(model);
        }

        private bool IsModelValid(WorkshopWorkerDetailModel model, bool isInsert)
        {
            // bir çalışan aynı tarihlerde iki ayrı çalışmada aktif olarak olamaz.
            var bo = new WorkshopWorkerBL();
            int totalCount = 0;
            var listMo = new WorkshopWorkerListModel
                {
                    WorkerId = model.WorkerId.GetValue<int>(),
                    IsActive = true
                };
            var workshopList = bo.ListWorkshopWorkers(listMo, out totalCount).Data;
            var controlDate = (from r in workshopList.AsEnumerable()
                               where r.WorkshopId != model.WorkshopId &&
                                     (((r.StartDate >= model.StartDate) &&
                                       (r.EndDate <= model.EndDate))
                                      ||
                                      ((r.StartDate >= model.StartDate) &&
                                       (r.EndDate >= model.EndDate && r.StartDate <= model.EndDate))
                                      ||
                                      ((r.StartDate <= model.StartDate) &&
                                       (r.EndDate >= model.EndDate))
                                      ||
                                      ((r.StartDate <= model.StartDate) &&
                                       (r.EndDate <= model.EndDate && r.EndDate >= model.StartDate)))
                               select r);
            if (controlDate.Any())
            {
                SetMessage(MessageResource.WorkshopWorker_Warning_WorkerDateExists,
                           CommonValues.MessageSeverity.Fail);
                return false;
            }
            var controlWorkshop = (from r in workshopList.AsEnumerable()
                                   where r.WorkshopId == model.WorkshopId
                                   select r);
            if (controlWorkshop.Any() && isInsert)
            {
                SetMessage(MessageResource.WorkshopWorker_Warning_WorkerWorkshopExists,
                           CommonValues.MessageSeverity.Fail);
                return false;
            }

            return true;
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkshopWorker.WorkshopWorkerIndex, CommonValues.PermissionCodes.WorkshopWorker.WorkshopWorkerCreate)]
        public ActionResult WorkshopWorkerCreate(WorkshopWorkerDetailModel model)
        {
            var bo = new WorkshopWorkerBL();
            SetDefaults();
            if (ModelState.IsValid)
            {
                if (IsModelValid(model, true))
                {
                    model.CommandType = CommonValues.DMLType.Insert;
                    bo.DMLWorkshopWorker(UserManager.UserInfo,model);
                    CheckErrorForMessage(model, true);
                    ModelState.Clear();
                    return View();
                }
            }
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkshopWorker.WorkshopWorkerIndex, CommonValues.PermissionCodes.WorkshopWorker.WorkshopWorkerUpdate)]
        public ActionResult WorkshopWorkerUpdate(int workshopId = 0, int workerId = 0)
        {
            var bo = new WorkshopWorkerBL();
            SetDefaults();

            var referenceModel = new WorkshopWorkerDetailModel();
            if (workshopId > 0 && workerId > 0)
            {
                referenceModel.WorkshopId = workshopId;
                referenceModel.WorkerId = workerId;
                referenceModel = bo.GetWorkshopWorker(referenceModel).Model;
            }
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkshopWorker.WorkshopWorkerIndex, CommonValues.PermissionCodes.WorkshopWorker.WorkshopWorkerUpdate)]
        public ActionResult WorkshopWorkerUpdate(WorkshopWorkerDetailModel viewModel)
        {
            var bo = new WorkshopWorkerBL();
            SetDefaults();

            if (ModelState.IsValid)
            {
                if (IsModelValid(viewModel, false))
                {
                    viewModel.CommandType = CommonValues.DMLType.Update;
                    bo.DMLWorkshopWorker(UserManager.UserInfo,viewModel);
                    CheckErrorForMessage(viewModel, true);
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkshopWorker.WorkshopWorkerIndex, CommonValues.PermissionCodes.WorkshopWorker.WorkshopWorkerDelete)]
        public ActionResult WorkshopWorkerDelete(int workshopId, int workerId)
        {
            ViewBag.HideElements = false;

            var bo = new WorkshopWorkerBL();
            var model = new WorkshopWorkerDetailModel { WorkshopId = workshopId, WorkerId = workerId, CommandType = CommonValues.DMLType.Delete };
            bo.DMLWorkshopWorker(UserManager.UserInfo,model);
           // CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkshopWorker.WorkshopWorkerIndex, CommonValues.PermissionCodes.WorkshopWorker.WorkshopWorkerDetails)]
        public ActionResult WorkshopWorkerDetails(int workshopId = 0, int workerId = 0)
        {
            var referenceModel = new WorkshopWorkerDetailModel { WorkshopId = workshopId, WorkerId = workerId };
            var bo = new WorkshopWorkerBL();

            var model = bo.GetWorkshopWorker(referenceModel).Model;

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.WorkshopWorker.WorkshopWorkerIndex, CommonValues.PermissionCodes.WorkshopWorker.WorkshopWorkerDetails)]
        public ActionResult ListWorkshopWorkers([DataSourceRequest]DataSourceRequest request, WorkshopWorkerListModel model)
        {
            var bo = new WorkshopWorkerBL();
            var referenceModel = new WorkshopWorkerListModel(request) { WorkerId = model.WorkerId, WorkshopId = model.WorkshopId, StartDate = model.StartDate, EndDate =  model.EndDate, SearchIsActive = model.SearchIsActive };
            int totalCnt;
            var returnValue = bo.ListWorkshopWorkers(referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

    }
}
