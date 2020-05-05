using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.LabourMainGroup;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    public class LabourMainGroupController : ControllerBase
    {
        private readonly LabourMainGroupBL _service = new LabourMainGroupBL();

        [AuthorizationFilter(CommonValues.PermissionCodes.LabourMainGroup.LabourMainGroupIndex)]
        public ActionResult LabourMainGroupIndex()
        {
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.LabourMainGroup.LabourMainGroupIndex)]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request, LabourMainGroupListModel model)
        {
            int totalCnt;

            var referenceModel = new LabourMainGroupListModel(request) { IsActive = model.IsActive };

            var list = _service.List(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = list,
                Total = totalCnt
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.LabourMainGroup.LabourMainGroupCreate)]
        public ActionResult LabourMainGroupCreate()
        {
            var model = new LabourMainGroupViewModel { IsActive = true };
            return PartialView(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourMainGroup.LabourMainGroupCreate)]
        public ActionResult LabourMainGroupIndex(HttpPostedFileBase excelFile, LabourMainGroupListModel listModel)
        {
            var listModels = new List<LabourMainGroupViewModel>();

            var model = new LabourMainGroupViewModel { CommandType = CommonValues.DMLType.Insert };

            using (Stream s = excelFile.InputStream)
            {
                listModels = _service.ParseExcel(UserManager.UserInfo, model, s).Data;
            }

            if (listModels.Exists(q => q.ErrorNo >= 1))
            {
                var ms = _service.SetExcelReport(listModels, model.ErrorMessage);

                var fileViewModel = new DownloadFileViewModel
                {
                    FileName = CommonValues.ErrorLog + DateTime.Now + CommonValues.ExcelExtOld,
                    ContentType = CommonValues.ExcelContentType,
                    MStream = ms,
                    Id = Guid.NewGuid()
                };

                Session[CommonValues.DownloadFileFormatCookieKey] = fileViewModel.Add(fileViewModel);
                TempData[CommonValues.DownloadFileIdCookieKey] = fileViewModel.Id;

                SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);

                return View();
            }
            else
            {
                _service.Insert(UserManager.UserInfo, model, listModels);
                CheckErrorForMessage(model, true);
                return View();
            }
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourMainGroup.LabourMainGroupCreate)]
        public ActionResult LabourMainGroupCreate(LabourMainGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                _service.Insert(UserManager.UserInfo, model);

                if (model.ErrorNo == 0)
                {
                    foreach (var item in ModelState.Keys.Where(item => item != "MainGroupId"))
                        ModelState[item].Value = new ValueProviderResult(string.Empty, string.Empty, CultureInfo.CurrentCulture);

                    model.MultiLanguageContentAsText = string.Empty;
                }

                CheckErrorForMessage(model, true);
            }

            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.LabourMainGroup.LabourMainGroupUpdate)]
        public ActionResult LabourMainGroupUpdate(string id)
        {
            return PartialView(_service.Get(UserManager.UserInfo, new LabourMainGroupViewModel() { MainGroupId = id }).Model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourMainGroup.LabourMainGroupUpdate)]
        public ActionResult LabourMainGroupUpdate(LabourMainGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Update;
                _service.Update(UserManager.UserInfo, model);

                CheckErrorForMessage(model, true);
            }

            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.LabourMainGroup.LabourMainGroupDetail)]
        public ActionResult LabourMainGroupDetail(string id)
        {
            return PartialView(_service.Get(UserManager.UserInfo, new LabourMainGroupViewModel() { MainGroupId = id }).Model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourMainGroup.LabourMainGroupDelete)]
        public ActionResult LabourMainGroupDelete(string id)
        {
            var model = new LabourMainGroupViewModel
            {
                MainGroupId = id,
                CommandType = CommonValues.DMLType.Delete
            };

            _service.Delete(UserManager.UserInfo, model);

            ModelState.Clear();

            return model.ErrorNo == 0 ?
                GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success) :
                GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        public ActionResult ExcelSample()
        {
            var ms = _service.SampleExcelFormat();
            return File(ms, CommonValues.ExcelContentType, MessageResource.LabourMainGroup_PageTitle_Index + CommonValues.ExcelExtOld);
        }
    }
}