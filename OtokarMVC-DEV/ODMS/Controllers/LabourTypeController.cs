using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Resources;
using ODMSModel.LabourType;
using System.Collections.Generic;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class LabourTypeController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.LabourType.LabourTypeIndex)]
        public ActionResult LabourTypeIndex()
        {   
            return View(new LabourTypeIndexModel());
        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.LabourType.LabourTypeIndex, ODMSCommon.CommonValues.PermissionCodes.LabourType.LabourTypeCreate)]
        public ActionResult LabourTypeCreate()
        {
            ViewBag.VatRatioList = GetVatRatioList();
            var model = new LabourTypeDetailModel {IsActive = true};
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.LabourType.LabourTypeIndex, ODMSCommon.CommonValues.PermissionCodes.LabourType.LabourTypeCreate)]
        public ActionResult LabourTypeCreate(LabourTypeDetailModel model)
        {
            ViewBag.VatRatioList = GetVatRatioList();

            if (ModelState.IsValid)
            {
                var bo = new LabourTypeBL();
                model.CommandType = model.Id > 0 ? ODMSCommon.CommonValues.DMLType.Update : ODMSCommon.CommonValues.DMLType.Insert;
                bo.DMLLabourType(UserManager.UserInfo,model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();
                return View(new LabourTypeDetailModel());
            }
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.LabourType.LabourTypeIndex, ODMSCommon.CommonValues.PermissionCodes.LabourType.LabourTypeUpdate)]
        public ActionResult LabourTypeUpdate(int id = 0)
        {
            ViewBag.VatRatioList = GetVatRatioList();
            var referenceModel = new LabourTypeDetailModel();
            if (id > 0)
            {
                var bo = new LabourTypeBL();
                referenceModel.Id = id;
                referenceModel = bo.GetLabourType(UserManager.UserInfo,referenceModel).Model;
            }
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.LabourType.LabourTypeIndex, ODMSCommon.CommonValues.PermissionCodes.LabourType.LabourTypeUpdate)]
        public ActionResult LabourTypeUpdate(LabourTypeDetailModel viewModel)
        {
            ViewBag.VatRatioList = GetVatRatioList();
            var bo = new LabourTypeBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = viewModel.Id > 0 ? ODMSCommon.CommonValues.DMLType.Update : ODMSCommon.CommonValues.DMLType.Insert;
                bo.DMLLabourType(UserManager.UserInfo,viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.LabourType.LabourTypeIndex, ODMSCommon.CommonValues.PermissionCodes.LabourType.LabourTypeDelete)]
        public ActionResult LabourTypeDelete(LabourTypeDeleteModel referenceModel)
        {
            ViewBag.HideElements = false;
            var model = new LabourTypeDetailModel() {Id = referenceModel.Id};
            var bo = new LabourTypeBL();
            model.CommandType = model.Id > 0 ? ODMSCommon.CommonValues.DMLType.Delete : string.Empty;
            bo.DMLLabourType(UserManager.UserInfo,model);
            //CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.LabourType.LabourTypeIndex, ODMSCommon.CommonValues.PermissionCodes.LabourType.LabourTypeDetails)]
        public ActionResult LabourTypeDetails(int id = 0)
        {
            var referenceModel = new LabourTypeDetailModel { Id = id };
            var bo = new LabourTypeBL();

            var model = bo.GetLabourType(UserManager.UserInfo,referenceModel).Model;

            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.LabourType.LabourTypeIndex, ODMSCommon.CommonValues.PermissionCodes.LabourType.LabourTypeDetails)]
        public ActionResult ListLabourTypes([DataSourceRequest]DataSourceRequest request, LabourTypeListModel model)
        {
            var bo = new LabourTypeBL();
            var referenceModel = new LabourTypeListModel(request) { Name = model.Name, SearchIsActive = model.SearchIsActive };
            int totalCnt;
            var returnValue = bo.ListLabourTypes(UserManager.UserInfo,referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        
        private List<SelectListItem> GetVatRatioList()
        {
            return CommonBL.ListVatRatio().Data;
        }

    }
}
