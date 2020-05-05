using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.MaintenanceLabour;
using Permission = ODMSCommon.CommonValues.PermissionCodes.MaintenanceLabour;
using ODMSModel.Maintenance;
using System.Data;
using System.Web.UI.WebControls;
using System;
using System.Web;
using System.IO;
using ODMSModel.DownloadFileActionResult;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class MaintenanceLabourController : ControllerBase
    {
        #region MaintenanceLabour Index

        [HttpGet]
        [AuthorizationFilter(Permission.MaintenanceLabourIndex)]
        public ActionResult MaintenanceLabourIndex(int? id)
        {
            var model = new MaintenanceLabourViewModel();
            if (id.GetValueOrDefault(0) == 0)
                model.HideElements = true;
            // FillComboBoxes();
            var isMustList = new List<SelectListItem>
            {
                new SelectListItem() {Text = MessageResource.Global_Display_Yes, Value = "true"},
                new SelectListItem() {Text = MessageResource.Global_Display_No, Value = "false"}
            };
            ViewBag.IsMustList = isMustList;
            model.SearchIsActive = null;
            model.IsMust = null;
            model.MaintenanceId = id.GetValueOrDefault(0);
            return PartialView(model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.MaintenanceLabourIndex)]
        public ActionResult MaintenanceLabourIndex(HttpPostedFileBase excelFile)
        {
            MaintenanceLabourViewModel model = new MaintenanceLabourViewModel();
            if (ModelState.IsValid)
            {
                if (excelFile != null)
                {
                    var bo = new MaintenanceLabourBL();
                    Stream s = excelFile.InputStream;
                    List<MaintenanceLabourViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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

                        return base.Json(new { Success = false, Message = model.ErrorMessage, FileId = fileViewModel.Id });
                    }
                    else
                    {
                        foreach (var row in modelList)
                        {
                            var result = new MaintenanceLabourBL().GetMaintenanceLabour(UserManager.UserInfo, row.MaintenanceId, row.LabourId).Model;
                            if (!string.IsNullOrEmpty(result.LabourName))
                                row.CommandType = CommonValues.DMLType.Update;
                            else
                                row.CommandType = CommonValues.DMLType.Insert;

                            bo.DMLMaintenanceLabour(UserManager.UserInfo, row);
                            if (row.ErrorNo > 0)
                            {
                                return base.Json(new { Success = false, Message = row.ErrorMessage });
                            }
                        }

                        return base.Json(new { Success = true, Message = MessageResource.Global_Display_Success });
                    }

                }
                else
                {
                    return base.Json(new { Success = false, Message = MessageResource.Global_Warning_ExcelNotSelected });
                }
            }
            return base.Json(new { Success = false, Message = MessageResource.Global_Warning_ExcelNotSelected });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.MaintenanceLabourIndex)]
        public ActionResult ListMaintenanceLabours([DataSourceRequest]DataSourceRequest request, MaintenanceLabourListModel viewModel)
        {
            var bus = new MaintenanceLabourBL();
            var model = new MaintenanceLabourListModel(request)
            {
                MaintenanceId = viewModel.MaintenanceId,
                IsMust = viewModel.IsMust,
                Quantity = viewModel.Quantity,
                IsActive = viewModel.IsActive
            };

            var totalCnt = 0;
            var returnValue = bus.ListMaintenanceLabours(UserManager.UserInfo, model, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }


        public ActionResult ListMaintenanceLabourForExcel([DataSourceRequest]DataSourceRequest request, MaintenanceLabourListModel viewModel)
        {
            var bus = new MaintenanceLabourBL();
            var model = new MaintenanceLabourListModel(request)
            {
                MaintenanceId = viewModel.MaintenanceId,
                IsMust = viewModel.IsMust,
                Quantity = viewModel.Quantity,
                IsActive = viewModel.IsActive
            };
            var totalCnt = 0;

            var maintModel = new MaintenanceListModel();
            var maintVal = new MaintenanceBL().GetMaintenanceForMaintId(UserManager.UserInfo, maintModel).Model;

            var rValue = bus.ListMaintenanceLaboursForExcel(UserManager.UserInfo, model, maintVal, out totalCnt).Data;

            var dt = new DataTable();
            dt.Columns.Add(MessageResource.Maintenance_Display_ID, typeof(string));
            dt.Columns.Add(MessageResource.Maintenance_Display_Name, typeof(string));
            dt.Columns.Add(MessageResource.VehicleModel_Display_Code, typeof(string));
            dt.Columns.Add(MessageResource.VehicleType_Display_Name, typeof(string));
            dt.Columns.Add(MessageResource.Vehicle_Display_EngineType, typeof(string));
            dt.Columns.Add(MessageResource.Maintenance_Display_Type, typeof(string));
            dt.Columns.Add(MessageResource.Maintenance_Display_Km, typeof(string));
            dt.Columns.Add(MessageResource.Maintenance_Display_Month, typeof(string));
            dt.Columns.Add(MessageResource.Global_Display_MainCategory, typeof(string));
            dt.Columns.Add(MessageResource.Global_Display_Category, typeof(string));
            dt.Columns.Add(MessageResource.Global_Display_SubCategory, typeof(string));
            dt.Columns.Add(MessageResource.AppointmentIndicatorFailureCode_Display_Code, typeof(string));
            dt.Columns.Add(MessageResource.Global_Display_IsActive, typeof(string));

            dt.Columns.Add(MessageResource.Labour_Display_Code, typeof(string));
            dt.Columns.Add(MessageResource.Labour_Display_Name, typeof(string));
            dt.Columns.Add(MessageResource.Global_Display_Quantity, typeof(string));
            dt.Columns.Add(MessageResource.MaintenanceLabour_Display_IsMust, typeof(string));


            foreach (var item in rValue)
            {
                var dtRow = dt.NewRow();
                dtRow[0] = item.MaintenanceId;
                dtRow[1] = item.MaintenanceName;
                dtRow[2] = item.VehicleModel;
                dtRow[3] = item.VehicleTypeName;
                dtRow[4] = item.EngineType;
                dtRow[5] = item.MaintTypeName;
                dtRow[6] = item.MaintKM;
                dtRow[7] = item.MaintMonth;
                dtRow[8] = item.MainCategoryName;
                dtRow[9] = item.CategoryName;
                dtRow[10] = item.SubCategoryName;
                dtRow[11] = item.FailureCode;
                dtRow[12] = item.IsActiveS;
                dtRow[13] = item.LabourCode;
                dtRow[14] = item.LabourName;
                dtRow[15] = item.Quantity;
                dtRow[16] = item.IsMustString;
                dt.Rows.Add(dtRow);
            }
            var filename = MessageResource.MaintenanceLabour_PageTitle_Index + ".xls";
            var tw = new System.IO.StringWriter();
            var hw = new System.Web.UI.HtmlTextWriter(tw);
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

            var excelBytes = new ExcelHelper().GenerateExcel(headerList, rows, new List<FilterDto>(), reportName: filename, reportObject: dt);


            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);

        }


        #endregion

        #region MaintenanceLabour Create
        [HttpGet]
        [AuthorizationFilter(Permission.MaintenanceLabourIndex, Permission.MaintenanceLabourCreate)]
        public ActionResult MaintenanceLabourCreate(int? id)
        {
            var model = new MaintenanceLabourViewModel() { IsCreate = true };
            if (id.GetValueOrDefault(0) == 0)
                model.HideElements = true;
            //FillComboBoxes();
            model.IsMust = null;
            model.SearchIsActive = null;
            model.MaintenanceId = id.GetValueOrDefault(0);
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.MaintenanceLabourIndex, Permission.MaintenanceLabourCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult MaintenanceLabourCreate(MaintenanceLabourViewModel model)
        {
            //FillComboBoxes();
            model.IsCreate = true;
            if (ModelState.IsValid == false)
                return View(model);
            var bus = new MaintenanceLabourBL();
            model.CommandType = CommonValues.DMLType.Insert;
            bus.DMLMaintenanceLabour(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            return View(new MaintenanceLabourViewModel() { MaintenanceId = model.MaintenanceId, IsCreate = true });
        }
        #endregion

        #region Maintenance Labour Update
        [HttpGet]
        [AuthorizationFilter(Permission.MaintenanceLabourIndex, Permission.MaintenanceLabourUpdate)]
        public ActionResult MaintenanceLabourUpdate(int? MaintenanceId, int? LabourId)
        {
            //FillComboBoxes();
            if (!(MaintenanceId.HasValue && MaintenanceId > 0) || !(LabourId.HasValue && LabourId > 0))
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View();
            }
            var model = new MaintenanceLabourBL().GetMaintenanceLabour(UserManager.UserInfo, MaintenanceId.GetValueOrDefault(), LabourId.GetValueOrDefault()).Model;
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.MaintenanceLabourIndex, Permission.MaintenanceLabourUpdate)]
        [ValidateAntiForgeryToken]
        public ActionResult MaintenanceLabourUpdate(MaintenanceLabourViewModel model)
        {
            //FillComboBoxes();
            if (ModelState.IsValid == false)
                return View(model);
            var bus = new MaintenanceLabourBL();
            model.CommandType = CommonValues.DMLType.Update;
            bus.DMLMaintenanceLabour(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            return View(model);
        }
        #endregion

        #region Maintenance Labour Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.MaintenanceLabourIndex, Permission.MaintenanceLabourDelete)]
        public ActionResult MaintenanceLabourDelete(int? MaintenanceId, int? LabourId)
        {
            if (!(MaintenanceId.HasValue && MaintenanceId > 0) || !(LabourId.HasValue && LabourId > 0))
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_NoRecordFound);
            }

            var bus = new MaintenanceLabourBL();
            var model = new MaintenanceLabourViewModel
            {
                LabourId = LabourId ?? 0,
                MaintenanceId = MaintenanceId ?? 0,
                CommandType = CommonValues.DMLType.Delete
            };

            bus.DMLMaintenanceLabour(UserManager.UserInfo, model);

            CheckErrorForMessage(model, true);
            ModelState.Clear();
            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }
        #endregion

        #region Maintenance Labour Details

        [HttpGet]
        [AuthorizationFilter(Permission.MaintenanceLabourIndex, Permission.MaintenanceLabourDetails)]
        public ActionResult MaintenanceLabourDetails(int? MaintenanceId, int? LabourId)
        {
            if (!(MaintenanceId.HasValue && MaintenanceId > 0) || !(LabourId.HasValue && LabourId > 0))
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View();
            }
            return View(new MaintenanceLabourBL().GetMaintenanceLabour(UserManager.UserInfo, MaintenanceId.GetValueOrDefault(), LabourId.GetValueOrDefault()).Model);
        }

        #endregion

       
        public ActionResult ExcelSample()
        {
            MaintenanceLabourBL maintBL = new MaintenanceLabourBL();
            var ms = maintBL.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.MaintenanceLabour_PageTitle_Index + CommonValues.ExcelExtOld);
        }

    }
}
