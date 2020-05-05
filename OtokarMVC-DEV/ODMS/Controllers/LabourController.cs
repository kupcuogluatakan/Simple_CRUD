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
using ODMSModel.Labour;
using ODMSModel.ViewModel;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class LabourController : ControllerBase
    {
        //Index
        [AuthorizationFilter(CommonValues.PermissionCodes.Labour.LabourIndex)]
        [HttpGet]
        public ActionResult LabourIndex()
        {
            SetComboBox();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.LabourDuration.LabourDurationIndex, CommonValues.PermissionCodes.LabourDuration.LabourDurationDetails)]
        public ActionResult ListLabourForExcel([DataSourceRequest] DataSourceRequest request, LabourAndLabourDetListModel labourModel)
        {
            LabourBL labourBL = new LabourBL();
            var rValue = labourBL.GetLabourListForExcel(UserManager.UserInfo).Data;


            var list = new List<LabourAndLabourDetListModel>();

            var dt = new DataTable();
            dt.Columns.Add(MessageResource.LabourAndLabourDet_LABOUR_CODE, typeof(string));
            dt.Columns.Add(MessageResource.LabourAndLabourDet_LABOUR_NAME, typeof(string));
            dt.Columns.Add(MessageResource.LabourAndLabourDet_LABOUR_SSID, typeof(string));
            dt.Columns.Add(MessageResource.LabourAndLabourDet_LABOUR_GROUP_DESC, typeof(string));
            dt.Columns.Add(MessageResource.LabourAndLabourDet_LABOUR_SUBGROUP_DESC, typeof(string));
            dt.Columns.Add(MessageResource.LabourAndLabourDet_LABOUR_REPAIR_CODE, typeof(string));
            dt.Columns.Add(MessageResource.LabourAndLabourDet_LABOUR_TYPE_NAME, typeof(string));
            dt.Columns.Add(MessageResource.LabourAndLabourDet_MODEL_KOD, typeof(string));
            dt.Columns.Add(MessageResource.LabourAndLabourDet_DURATION, typeof(string));
            dt.Columns.Add(MessageResource.LabourAndLabourDet_IS_ACTIVE_STRING, typeof(string));
            dt.Columns.Add(MessageResource.LabourAndLabourDet_MODEL_NAME, typeof(string));
            dt.Columns.Add(MessageResource.LabourAndLabourDet_TYPE_NAME, typeof(string));
            dt.Columns.Add(MessageResource.LabourAndLabourDet_ENGINE_TYPE, typeof(string));


            foreach (var item in rValue)
            {
                var dtRow = dt.NewRow();
                dtRow[0] = item.LABOUR_CODE;
                dtRow[1] = item.LABOUR_NAME;
                dtRow[2] = item.LABOUR_SSID;
                dtRow[3] = item.LABOUR_GROUP_DESC;
                dtRow[4] = item.LABOUR_SUBGROUP_DESC;
                dtRow[5] = item.LABOUR_REPAIR_CODE;
                dtRow[6] = item.LABOUR_TYPE_NAME;
                dtRow[7] = item.MODEL_KOD;
                dtRow[8] = item.DURATION;
                dtRow[9] = item.IS_ACTIVE_STRING;
                dtRow[10] = item.MODEL_NAME;
                dtRow[11] = item.TYPE_NAME;
                dtRow[12] = item.ENGINE_TYPE;
                dt.Rows.Add(dtRow);
            }
            var fileName = MessageResource.Labour_And_Labour_Det_Report_Display_Index + ".xls";
            var tw = new StringWriter();
            var hw = new HtmlTextWriter(tw);
            var dgGrid = new DataGrid { DataSource = dt };
            dgGrid.DataBind();
            dgGrid.RenderControl(hw);

            var rows = new List<List<Tuple<object, string>>>();
            var headerList = new List<string>();
            foreach (DataColumn item in dt.Columns)
            {
                headerList.Add(item.ColumnName);
            }
            foreach (DataRow item in dt.Rows)
            {
                var row = new List<Tuple<object, string>>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(new Tuple<object, string>(item[col.ColumnName].ToString(), col.ColumnName));
                }
                rows.Add(row);
            }
            var excelBytes = new ExcelHelper().GenerateExcel(headerList, rows, new List<FilterDto>(), reportName: fileName, reportObject: dt);
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        }

        [AuthorizationFilter(CommonValues.PermissionCodes.LabourDuration.LabourDurationIndex, CommonValues.PermissionCodes.LabourDuration.LabourDurationDetails)]
        public JsonResult ListLabour([DataSourceRequest] DataSourceRequest request, LabourListModel labourModel)
        {
            LabourBL labourBL = new LabourBL();
            LabourListModel model_Labour = new LabourListModel(request);
            int totalCount = 0;

            model_Labour.IsActiveSearch = labourModel.IsActiveSearch;
            model_Labour.LabourSSID = labourModel.LabourSSID;
            model_Labour.LabourCode = labourModel.LabourCode;
            model_Labour.RepairCode = labourModel.RepairCode;
            model_Labour.LabourName = labourModel.LabourName;
            model_Labour.LabourSubGroupId = labourModel.LabourSubGroupId;
            model_Labour.LabourMainGroupId = labourModel.LabourMainGroupId;

            var rValue = labourBL.GetLabourList(UserManager.UserInfo,model_Labour, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });

        }


        //Details
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourDuration.LabourDurationIndex, CommonValues.PermissionCodes.LabourDuration.LabourDurationDetails)]
        [HttpGet]
        public ActionResult LabourDetails(int id = 0)
        {
            LabourBL labourBL = new LabourBL();
            LabourViewModel model_Labour = new LabourViewModel();
            model_Labour.LabourId = id;

            labourBL.GetLabour(UserManager.UserInfo, model_Labour);

            return View(model_Labour);
        }


        //Create
        [AuthorizationFilter(CommonValues.PermissionCodes.Labour.LabourCreate, CommonValues.PermissionCodes.Labour.LabourIndex)]
        [HttpGet]
        public ActionResult LabourCreate()
        {
            SetComboBox();
            LabourViewModel model = new LabourViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Labour.LabourCreate, CommonValues.PermissionCodes.Labour.LabourIndex)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LabourCreate(LabourViewModel labourModel)
        {
            LabourBL labourBL = new LabourBL();

            if (ModelState.IsValid)
            {
                labourModel.LabourSSID = labourModel.LabourCode;
                labourModel.CommandType = CommonValues.DMLType.Insert;
                labourBL.DMLLabour(UserManager.UserInfo,labourModel);

                CheckErrorForMessage(labourModel, true);

                if (labourModel.ErrorNo == 0)
                    ModelState.Clear();

                SetComboBox();
                return View();
            }
            SetComboBox();
            return View(labourModel);
        }


        //Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Labour.LabourDelete, CommonValues.PermissionCodes.Labour.LabourIndex)]
        public ActionResult LabourDelete(int labourId)
        {
            LabourBL labourBL = new LabourBL();
            LabourViewModel labourModel = new LabourViewModel { LabourId = labourId };
            labourModel.CommandType = labourModel.LabourId > 0 ? CommonValues.DMLType.Delete : "";

            labourBL.DMLLabour(UserManager.UserInfo, labourModel);

            if (labourModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, labourModel.ErrorMessage);
        }


        //Update
        [AuthorizationFilter(CommonValues.PermissionCodes.Labour.LabourUpdate, CommonValues.PermissionCodes.Labour.LabourIndex)]
        [HttpGet]
        public ActionResult LabourUpdate(int id = 0)
        {
            LabourBL labourBL = new LabourBL();
            LabourViewModel model_Labour = new LabourViewModel { LabourId = id };

            labourBL.GetLabour(UserManager.UserInfo, model_Labour);
            SetComboBox();

            return View(model_Labour);

        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Labour.LabourUpdate, CommonValues.PermissionCodes.Labour.LabourIndex)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LabourUpdate(LabourViewModel labourModel)
        {
            LabourBL labourBL = new LabourBL();
            if (ModelState.IsValid)
            {
                labourModel.LabourSSID = labourModel.LabourCode;
                labourModel.CommandType = labourModel.LabourId > 0 ? CommonValues.DMLType.Update : "";
                labourBL.DMLLabour(UserManager.UserInfo, labourModel);
                if (!CheckErrorForMessage(labourModel, true))
                {
                    labourModel.LabourName = (MultiLanguageModel)CommonUtility.DeepClone(labourModel.LabourName);
                    labourModel.LabourName.MultiLanguageContentAsText = labourModel.MultiLanguageContentAsText;
                }
            }
            SetComboBox();

            return View(labourModel);
        }


        //DefaultSet
        private void SetComboBox()
        {
            List<SelectListItem> list_MainGroup = LabourBL.ListMainGrpAsSelectList(UserManager.UserInfo).Data;
            List<SelectListItem> list_SelectList = CommonBL.ListStatus().Data;
            ViewBag.IASelectList = list_SelectList;
            ViewBag.LMGSelectList = list_MainGroup;
            ViewBag.LaborTypeList = LabourTypeBL.ListLabourTypesAsSelectListItems(UserManager.UserInfo).Data;
        }

        [HttpGet]
        public JsonResult ListSubGroup(int id)
        {
            return Json(LabourBL.ListSubGrpAsSelectList(UserManager.UserInfo,id).Data, JsonRequestBehavior.AllowGet);
        }


        #region Excel Upload

        public ActionResult ExcelSample()
        {
            var bo = new LabourBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.Labour_PageTitle_Index + CommonValues.ExcelExtOld);
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Labour.LabourIndex)]
        public ActionResult LabourIndex(LabourListModel listModel, HttpPostedFileBase excelFile)
        {
            SetComboBox();
            LabourViewModel model = new LabourViewModel();

            if (ModelState.IsValid)
            {
                if (excelFile != null)
                {
                    var bo = new LabourBL();
                    Stream s = excelFile.InputStream;
                    List<LabourViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
                    // excel dosyasındaki veriler kontrol edilir.
                    if (model.ErrorNo > 0)
                    {
                        var ms = bo.SetExcelReport(modelList, model.ErrorMessage);

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
                        int totalCount = 0;
                        foreach (var row in modelList)
                        {
                            LabourListModel controlModel = new LabourListModel
                            {
                                LabourMainGroupId = row.LabourMainGroupId,
                                LabourSubGroupId = row.LabourSubGroupId,
                                LabourTypeId = row.LabourTypeId,
                                RepairCode = row.RepairCode
                            };

                            List<LabourListModel> list = bo.GetLabourList(UserManager.UserInfo,controlModel, out totalCount).Data;
                            if (totalCount > 0)
                            {
                                row.LabourId = list.ElementAt(0).LabourId;
                                row.CommandType = CommonValues.DMLType.Update;
                            }
                            else
                            {
                                row.CommandType = CommonValues.DMLType.Insert;
                            }
                            bo.DMLLabour(UserManager.UserInfo, row);
                            if (row.ErrorNo > 0)
                            {
                                SetMessage(row.ErrorMessage, CommonValues.MessageSeverity.Fail);
                                return View();
                            }
                        }

                        SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                    }
                }
                else
                {
                    SetMessage(MessageResource.Global_Warning_ExcelNotSelected, CommonValues.MessageSeverity.Fail);
                }
                ModelState.Clear();
            }
            return View();
        }

        #endregion
    }
}
