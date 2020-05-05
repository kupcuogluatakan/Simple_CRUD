using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.EducationContributers;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class EducationContributersController : ControllerBase
    {

        [AuthorizationFilter(CommonValues.PermissionCodes.EducationContributers.EducationContributersIndex, CommonValues.PermissionCodes.EducationDates.EducationDatesIndex)]
        [HttpGet]
        public ActionResult EducationContributersIndex(string id)
        {
            EducationContributersViewModel eduContViewModel = new EducationContributersViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                eduContViewModel.EducationCode = id;
                eduContViewModel.IsRequestRoot = true;
            }
            else
            {
                eduContViewModel.IsRequestRoot = false;
            }
            return PartialView(eduContViewModel);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Education.EducationIndex)]
        public JsonResult ListEducationContributers([DataSourceRequest] DataSourceRequest request, EducationContributersListModel eduContListModel)
        {
            var eduContBL = new EducationContributersBL();
            var eduContModel = new EducationContributersListModel(request)
            {
                EducationCode = eduContListModel.EducationCode,
                EducationDate = eduContListModel.EducationDate
            };
            int totalCount = 0;

            var rValue = eduContBL.GetEducationContList(UserManager.UserInfo, eduContModel, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });

        }


        //Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.EducationContributers.EducationContributersDelete)]
        public JsonResult EducationContributersDelete(string eduCode, int rowId, string tcNo)
        {
            EducationContributersBL eduContBL = new EducationContributersBL();
            EducationContributersViewModel eduContViewModel = new EducationContributersViewModel();
            eduContViewModel.EducationCode = eduCode;
            eduContViewModel.RowNumber = rowId;
            eduContViewModel.TCIdentity = tcNo;
            eduContViewModel.CommandType = CommonValues.DMLType.Delete;

            eduContBL.DMLEducationContributers(UserManager.UserInfo, eduContViewModel);

            if (eduContViewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, eduContViewModel.ErrorMessage);
        }

        public ActionResult ExcelSample()
        {
            var bo = new EducationContributersBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.EducationCont_PageTitle_Index + CommonValues.ExcelExtOld);
        }

        //Create & Upload
        [AuthorizationFilter(CommonValues.PermissionCodes.EducationContributers.EducationContributersCreate, CommonValues.PermissionCodes.EducationDates.EducationDatesIndex)]
        [HttpGet]
        public ActionResult EducationContributersCreate(string id, int rowId)
        {
            EducationContributersViewModel eduContModel = new EducationContributersViewModel();
            if (!string.IsNullOrEmpty(id) & rowId > 0)
            {
                eduContModel.EducationCode = id;
                eduContModel.RowNumber = rowId;
            }
            return View(eduContModel);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.EducationContributers.EducationContributersCreate)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EducationContributersCreate(HttpPostedFileBase file, EducationContributersViewModel model)
        {
            if (file != null)
            {
                string ext = Path.GetExtension(file.FileName);

                if (ext.IndexOf(CommonValues.ExcelExtNew) != -1 || ext.IndexOf(CommonValues.ExcelExtOld) != -1)
                {
                    EducationContributersBL eduContBL = new EducationContributersBL();
                    List<EducationContributersViewModel> listModels = new List<EducationContributersViewModel>();
                    if (file != null && !string.IsNullOrEmpty(model.EducationCode) && model.RowNumber > 0)
                    {
                        model.CommandType = CommonValues.DMLType.Insert;
                        using (Stream s = file.InputStream)
                        {
                            listModels = eduContBL.ParseExcel(UserManager.UserInfo, model, s).Data;
                        }

                    }

                    if (listModels.Exists(q => q.ErrorNo >= 1))
                    {
                        var ms = eduContBL.SetExcelReport(listModels, model.ErrorMessage);

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

                        return View(model);
                    }
                    else
                    {
                        CheckErrorForMessage(model, true);
                        return View(model);
                    }
                }
            }
            return View(model);
        }
    }
}
