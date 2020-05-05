using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Workshop;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class WorkshopController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Workshop.WorkshopIndex)]
        public ActionResult WorkshopIndex()
        {
            var bo = new WorkshopBL();
            var userInfo = UserManager.UserInfo;
            if (userInfo != null)
            {
                var model = bo.GetWorkshopIndexModel().Model;
                return View(model);
            }
            return View();
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Workshop.WorkshopIndex, CommonValues.PermissionCodes.Workshop.WorkshopCreate)]
        public ActionResult WorkshopCreate()
        {
            var bo = new WorkshopBL();
            ViewBag.DealerList = bo.GetDealerList().Data;
            var model = new WorkshopDetailModel {IsActive = true};
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Workshop.WorkshopIndex, CommonValues.PermissionCodes.Workshop.WorkshopCreate)]
        public ActionResult WorkshopCreate(WorkshopDetailModel model)
        {
            var bo = new WorkshopBL();
            ViewBag.DealerList = bo.GetDealerList().Data;
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                bo.DMLWorkshop(UserManager.UserInfo,model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();
                return View();
            }
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Workshop.WorkshopIndex, CommonValues.PermissionCodes.Workshop.WorkshopUpdate)]
        public ActionResult WorkshopUpdate(int id = 0)
        {
            var bo = new WorkshopBL();
            ViewBag.DealerList = bo.GetDealerList().Data;
            var referenceModel = new WorkshopDetailModel();
            if (id > 0)
            {
                referenceModel.Id = id;
                referenceModel = bo.GetWorkshop(referenceModel).Model;
            }
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Workshop.WorkshopIndex, CommonValues.PermissionCodes.Workshop.WorkshopUpdate)]
        public ActionResult WorkshopUpdate(WorkshopDetailModel viewModel)
        {
            var bo = new WorkshopBL();
            ViewBag.DealerList = bo.GetDealerList().Data;
            if (ModelState.IsValid)
            {
                viewModel.CommandType = viewModel.Id > 0 ? CommonValues.DMLType.Update : CommonValues.DMLType.Insert;
                bo.DMLWorkshop(UserManager.UserInfo,viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Workshop.WorkshopIndex, CommonValues.PermissionCodes.Workshop.WorkshopDelete)]
        public ActionResult WorkshopDelete(int id)
        {
            ViewBag.HideElements = false;

            var bo = new WorkshopBL();
            var model = new WorkshopDetailModel {Id = id, CommandType = CommonValues.DMLType.Delete};
            bo.DMLWorkshop(UserManager.UserInfo,model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Workshop.WorkshopIndex, CommonValues.PermissionCodes.Workshop.WorkshopDetails)]
        public ActionResult WorkshopDetails(int id = 0)
        {
            var referenceModel = new WorkshopDetailModel { Id = id };
            var bo = new WorkshopBL();

            var model = bo.GetWorkshop(referenceModel).Model;

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Workshop.WorkshopIndex, CommonValues.PermissionCodes.Workshop.WorkshopDetails)]
        public ActionResult ListWorkshops([DataSourceRequest]DataSourceRequest request, WorkshopListModel model)
        {
            var bo = new WorkshopBL();
            var referenceModel = new WorkshopListModel(request) { Name = model.Name, DealerId = model.DealerId, SearchIsActive = model.SearchIsActive };
            int totalCnt;
            var returnValue = bo.ListWorkshops(referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
    }
}
