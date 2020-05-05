using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.WorkshopType;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class WorkshopTypeController : ControllerBase
    {
        //
        // GET: /WorkshopType/
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkshopType.WorkshopTypeIndex)]
        public ActionResult WorkshopTypeIndex()
        {
            SetComboBox();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.WorkshopType.WorkshopTypeIndex)]
        public ActionResult ListWorkshopType([DataSourceRequest] DataSourceRequest request, WorkshopTypeListModel wModel)
        {
            var bl = new WorkshopTypeBL();
            var model = new WorkshopTypeListModel(request) {IsActiveSearch = wModel.IsActiveSearch};
            int totalCount = 0;

            var rValue = bl.GetListWorkshopType(UserManager.UserInfo,model,out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });

        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkshopType.WorkshopTypeIndex, CommonValues.PermissionCodes.WorkshopType.WorkshopTypeCreate)]
        public ActionResult WorkshopTypeCreate()
        {
            var model = new WorkshopTypeViewModel {IsActive = true};
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkshopType.WorkshopTypeIndex, CommonValues.PermissionCodes.WorkshopType.WorkshopTypeCreate)]
        public ActionResult WorkshopTypeCreate(WorkshopTypeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var bl = new WorkshopTypeBL();
            model.CommandType = CommonValues.DMLType.Insert;

            bl.DMLWorkshopType(UserManager.UserInfo,model);

            if (CheckErrorForMessage(model, true))
                return View(model);
            
            ModelState.Clear();

            return View(new WorkshopTypeViewModel());
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkshopType.WorkshopTypeIndex, CommonValues.PermissionCodes.WorkshopType.WorkshopTypeUpdate)]
        public ActionResult WorkshopTypeUpdate(int id)
        {
            var bl = new WorkshopTypeBL();
            var model = new WorkshopTypeViewModel();
            model.WorkshopTypeId = id;

            bl.GetWorkshopType(UserManager.UserInfo,model);

            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkshopType.WorkshopTypeIndex, CommonValues.PermissionCodes.WorkshopType.WorkshopTypeUpdate)]
        public ActionResult WorkshopTypeUpdate(WorkshopTypeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var bl = new WorkshopTypeBL();
            model.CommandType = CommonValues.DMLType.Update;

            bl.DMLWorkshopType(UserManager.UserInfo,model);
            CheckErrorForMessage(model, true);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkshopType.WorkshopTypeIndex, CommonValues.PermissionCodes.WorkshopType.WorkshopTypeDelete)]
        public ActionResult WorkshopTypeDelete(int id)
        {
            var bl = new WorkshopTypeBL();
            var model = new WorkshopTypeViewModel {CommandType = CommonValues.DMLType.Delete, WorkshopTypeId = id};

            bl.DMLWorkshopType(UserManager.UserInfo,model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                model.ErrorMessage);

        }

        private void SetComboBox()
        {
            List<SelectListItem> list_SelectList = CommonBL.ListStatus().Data;
            ViewBag.IASelectList = list_SelectList;
        }

    }
}
