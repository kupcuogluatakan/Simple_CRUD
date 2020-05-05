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
using ODMSModel.LabourSubGroup;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    public class LabourSubGroupController : ControllerBase
    {
        private readonly LabourSubGroupBL _service = new LabourSubGroupBL();

        [AuthorizationFilter(CommonValues.PermissionCodes.LabourSubGroup.LabourSubGroupIndex)]
        public ActionResult LabourSubGroupIndex(string mainId)
        {
            var model = new LabourSubGroupListModel()
            {
                MainGroupId = mainId
            };

            return PartialView(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourSubGroup.LabourSubGroupCreate)]
        public ActionResult LabourSubGroupIndex(LabourSubGroupListModel listModel, HttpPostedFileBase excelFileS)
        {
            SetDefaults();

            var listModels = new List<LabourSubGroupViewModel>();

            var model = new LabourSubGroupViewModel { CommandType = CommonValues.DMLType.Insert };

            using (Stream s = excelFileS.InputStream)
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

                return RedirectToAction("LabourMainGroupIndex", "LabourMainGroup");
            }
            else
            {
                _service.Insert(UserManager.UserInfo, model, listModels);
                CheckErrorForMessage(model, true);
                return RedirectToAction("LabourMainGroupIndex", "LabourMainGroup");
            }
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourSubGroup.LabourSubGroupIndex)]
        public ActionResult LabourSubGroupAllIndex()
        {
            SetDefaults();

            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.LabourSubGroup.LabourSubGroupIndex)] 
        public JsonResult Read([DataSourceRequest]DataSourceRequest request, LabourSubGroupListModel model)
        {
            int totalCnt;

            var referenceModel = new LabourSubGroupListModel(request)
            {
                IsActive = model.IsActive,
                MainGroupId = model.MainGroupId,
                Description = model.Description,
                MainGroupName = model.MainGroupName,
                SubGroupId = model.SubGroupId,
                LabourSubGroupName = model.LabourSubGroupName
            };

            var list = _service.List(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = list,
                Total = totalCnt
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.LabourSubGroup.LabourSubGroupCreate)]
        public ActionResult LabourSubGroupCreate(string mainId)
        {
            LabourSubGroupViewModel model = new LabourSubGroupViewModel()
            {
                MainGroupId = mainId,
                IsActive = true
            };

            return PartialView(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourSubGroup.LabourSubGroupCreate)]
        public ActionResult LabourSubGroupAllIndex(LabourSubGroupListModel listModel, HttpPostedFileBase excelFile)
        {
            SetDefaults();

            var listModels = new List<LabourSubGroupViewModel>();

            var model = new LabourSubGroupViewModel { CommandType = CommonValues.DMLType.Insert };

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
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourSubGroup.LabourSubGroupCreate)]
        public ActionResult LabourSubGroupCreate(LabourSubGroupViewModel model)
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

        [AuthorizationFilter(CommonValues.PermissionCodes.LabourSubGroup.LabourSubGroupUpdate)]
        public ActionResult LabourSubGroupUpdate(string mainId, string subId)
        {
            return PartialView(_service.Get(UserManager.UserInfo, new LabourSubGroupViewModel()
            {
                SubGroupId = subId,
                MainGroupId = mainId
            }).Model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourSubGroup.LabourSubGroupUpdate)]
        public ActionResult LabourSubGroupUpdate(LabourSubGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Update;
                _service.Update(UserManager.UserInfo, model);

                CheckErrorForMessage(model, true);
            }

            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.LabourSubGroup.LabourSubGroupDetail)]
        public ActionResult LabourSubGroupDetail(string mainId, string subId)
        {
            return PartialView(_service.Get(UserManager.UserInfo, new LabourSubGroupViewModel()
            {
                MainGroupId = mainId,
                SubGroupId = subId
            }).Model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourSubGroup.LabourSubGroupDelete)]
        public ActionResult LabourSubGroupDelete(string mainId, string subId)
        {
            var model = new LabourSubGroupViewModel
            {
                MainGroupId = mainId,
                SubGroupId = subId,
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
            return File(ms, CommonValues.ExcelContentType, MessageResource.LabourSubGroup_PageTitle_Index + CommonValues.ExcelExtOld);
        }

        private void SetDefaults()
        {
            ViewBag.StatusList = CommonBL.ListStatus().Data;
        }
    }
}
