using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using ODMSModel.MaintenancePart;
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
    public class MaintenancePartController : ControllerBase
    {
        //Index
        [AuthorizationFilter(CommonValues.PermissionCodes.MaintenancePart.MaintenancePartsIndex)]
        [HttpGet]
        public ActionResult MaintenancePartIndex(int? id)
        {
            MaintenancePartViewModel model_MaintP = new MaintenancePartViewModel();
            SetComboBox();
            if (id > 0)
            {
                model_MaintP.MaintId = (int)id;
                model_MaintP.IsRequestRoot = true;
            }
            else
            {
                model_MaintP.IsRequestRoot = false;
            }

            return PartialView(model_MaintP);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.MaintenancePart.MaintenancePartsIndex)]
        [HttpPost]
        public ActionResult MaintenancePartIndex(HttpPostedFileBase excelFile, int? MaintId)
        {
            MaintenancePartViewModel model_MaintP = new MaintenancePartViewModel();

            MaintenancePartViewModel model = new MaintenancePartViewModel();
            if (ModelState.IsValid)
            {
                if (excelFile != null)
                {
                    var bo = new MaintenancePartBL();
                    Stream s = excelFile.InputStream;
                    List<MaintenancePartViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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
                            MaintenancePartViewModel vModel = new MaintenancePartViewModel()
                            {
                                MaintId = row.MaintId,
                                PartId = row.PartId
                            };

                            new MaintenancePartBL().GetMaintenancePart(UserManager.UserInfo, vModel);

                            if (!string.IsNullOrEmpty(vModel.PartName))
                                row.CommandType = CommonValues.DMLType.Update;
                            else
                                row.CommandType = CommonValues.DMLType.Insert;

                            bo.DMLMaintenancePart(UserManager.UserInfo, row);

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


        [AuthorizationFilter(CommonValues.PermissionCodes.MaintenancePart.MaintenancePartsIndex, CommonValues.PermissionCodes.MaintenancePart.MaintenancePartsDetails)]
        public JsonResult ListMaintenancePart([DataSourceRequest]DataSourceRequest request, MaintenancePartListModel maintPModel)
        {
            MaintenancePartBL maintPBL = new MaintenancePartBL();
            MaintenancePartListModel model_MaintP = new MaintenancePartListModel(request);
            int totalCount = 0;

            model_MaintP.MaintId = maintPModel.MaintId;
            model_MaintP.PartId = maintPModel.PartId;
            model_MaintP.IsActiveSearch = maintPModel.IsActiveSearch;

            var rValue = maintPBL.GetMaintenancePartList(UserManager.UserInfo, model_MaintP, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        public ActionResult ListMaintenancePartForExcel([DataSourceRequest]DataSourceRequest request, MaintenancePartListModel maintPModel)
        {
            MaintenancePartBL maintPBL = new MaintenancePartBL();
            MaintenancePartListModel model_MaintP = new MaintenancePartListModel(request);
            int totalCount = 0;

            model_MaintP.MaintId = maintPModel.MaintId;
            model_MaintP.PartId = maintPModel.PartId;
            model_MaintP.IsActiveSearch = maintPModel.IsActiveSearch;

            var maintModel = new MaintenanceListModel();
            maintModel.MaintId = maintPModel.MaintId;
            var maintVal = new MaintenanceBL().GetMaintenanceForMaintId(UserManager.UserInfo, maintModel).Model;

            var rValue = maintPBL.GetMaintenancePartListForExcel(UserManager.UserInfo, model_MaintP, maintVal, out totalCount).Data;

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

            dt.Columns.Add(MessageResource.SparePart_Display_PartCode, typeof(string));
            dt.Columns.Add(MessageResource.SparePart_Display_PartName, typeof(string));
            dt.Columns.Add(MessageResource.MaintenancePart_Display_IsMust, typeof(string));
            dt.Columns.Add(MessageResource.MaintenancePart_Display_IsAlternAllow, typeof(string));
            dt.Columns.Add(MessageResource.MaintenancePart_Display_IsDifBrandAllow, typeof(string));
            dt.Columns.Add(MessageResource.MaintenancePart_Display_Quantity, typeof(string));

            dt.Columns.Add(MessageResource.Scrap_Display_Unit, typeof(string));
            dt.Columns.Add(MessageResource.Global_Display_IsActiveString, typeof(string));



            foreach (var item in rValue)
            {
                var dtRow = dt.NewRow();
                dtRow[0] = item.MaintId;
                dtRow[1] = item.MaintName;
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
                dtRow[13] = item.PartCode;
                dtRow[14] = item.PartName;
                dtRow[15] = item.IsMustS;
                dtRow[16] = item.IsAlernateAllowS;
                dtRow[17] = item.IsDifBrandAllowS;
                dtRow[18] = item.Quantity;
                dtRow[19] = item.Unit;
                dtRow[20] = item.IsActiveString;
                dt.Rows.Add(dtRow);
            }

            var filename = MessageResource.MaintenancePart_PageTitle_Index + ".xls";
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

        //Details
        [AuthorizationFilter(CommonValues.PermissionCodes.MaintenancePart.MaintenancePartsIndex, CommonValues.PermissionCodes.MaintenancePart.MaintenancePartsDetails)]
        [HttpGet]
        public ActionResult MaintenancePartDetails(int id = 0, int partId = 0)
        {
            MaintenancePartViewModel model_MaintP = new MaintenancePartViewModel();
            MaintenancePartBL maintPBL = new MaintenancePartBL();

            model_MaintP.MaintId = id;
            model_MaintP.PartId = partId;

            maintPBL.GetMaintenancePart(UserManager.UserInfo, model_MaintP);

            return View(model_MaintP);
        }


        //Create
        [AuthorizationFilter(CommonValues.PermissionCodes.MaintenancePart.MaintenancePartsCreate, CommonValues.PermissionCodes.MaintenancePart.MaintenancePartsIndex)]
        [HttpGet]
        public ActionResult MaintenancePartCreate(int id = 0)
        {
            SetComboBox();
            MaintenancePartViewModel x = new MaintenancePartViewModel();
            x.MaintId = id;
            x.IsActive = true;
            return View(x);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.MaintenancePart.MaintenancePartsCreate, CommonValues.PermissionCodes.MaintenancePart.MaintenancePartsIndex)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaintenancePartCreate(MaintenancePartViewModel maintPModel)
        {
            SetComboBox();
            MaintenancePartBL maintPBL = new MaintenancePartBL();
            if (ModelState.IsValid)
            {
                int maintID = maintPModel.MaintId;

                maintPModel.PartId = Session["ChangedPartId"].GetValue<int>();
                maintPModel.CommandType = CommonValues.DMLType.Insert;
                maintPBL.DMLMaintenancePart(UserManager.UserInfo, maintPModel);

                CheckErrorForMessage(maintPModel, true);
                ModelState.Clear();

                //SetComboBox();
                maintPModel = new MaintenancePartViewModel() { MaintId = maintID };
                return View(maintPModel);

            }

            return View(maintPModel);
        }


        //Update
        [AuthorizationFilter(CommonValues.PermissionCodes.MaintenancePart.MaintenancePartsUpdate, CommonValues.PermissionCodes.MaintenancePart.MaintenancePartsIndex)]
        [HttpGet]
        public ActionResult MaintenancePartUpdate(int id = 0, int partId = 0)
        {
            ClearTempData();
            SetComboBox();
            MaintenancePartViewModel model_MaintP = new MaintenancePartViewModel();
            MaintenancePartBL maintPBL = new MaintenancePartBL();

            model_MaintP.MaintId = id;
            model_MaintP.PartId = partId;

            maintPBL.GetMaintenancePart(UserManager.UserInfo, model_MaintP);

            return View(model_MaintP);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.MaintenancePart.MaintenancePartsUpdate, CommonValues.PermissionCodes.MaintenancePart.MaintenancePartsIndex)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaintenancePartUpdate(MaintenancePartViewModel maintPModel)
        {
            SetComboBox();
            MaintenancePartBL maintPBL = new MaintenancePartBL();
            if (ModelState.IsValid)
            {
                maintPModel.CommandType = CommonValues.DMLType.Update;
                maintPBL.DMLMaintenancePart(UserManager.UserInfo, maintPModel);
                CheckErrorForMessage(maintPModel, true);

            }

            return View(maintPModel);
        }


        //Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.MaintenancePart.MaintenancePartsDelete, CommonValues.PermissionCodes.MaintenancePart.MaintenancePartsIndex)]
        public ActionResult MaintenancePartDelete(int id = 0, int partId = 0)
        {
            MaintenancePartBL maintPBL = new MaintenancePartBL();
            MaintenancePartViewModel maintPModel = new MaintenancePartViewModel
            {
                MaintId = id,
                PartId = partId,
                CommandType = CommonValues.DMLType.Delete
            };

            maintPBL.DMLMaintenancePart(UserManager.UserInfo, maintPModel);

            if (maintPModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, maintPModel.ErrorMessage);
        }


        private void SetComboBox()
        {
            List<SelectListItem> list_SelectList = CommonBL.ListStatus().Data;
            ViewBag.IASelectList = list_SelectList;
        }

        [ValidateAntiForgeryToken]
        public JsonResult IsPartChanged(MaintenancePartViewModel model)
        {
            int result = 0;
            var splitPartName = !string.IsNullOrEmpty(model.PartName) ? model.PartName.Split('/') : null;

            List<AutocompleteSearchListModel> partList = null;

            if (splitPartName != null)
                partList = SparePartBL.ListOriginalSparePartAsAutoCompSearch(UserManager.UserInfo, splitPartName[0]).Data;

            if (partList != null)
            {
                foreach (var item in partList.Where(item => item.Column1 == splitPartName[0]))
                {
                    model.PartId = item.Id;
                }

                result = new SparePartBL().IsPartChanged(model.PartId.Value, splitPartName[0]).Model;
                Session["ChangedPartId"] = result;
            }

            bool isPartChanged = model.PartId.Value != result;

            return Json(isPartChanged);
        }

        public ActionResult ExcelSample()
        {
            MaintenancePartBL maintBL = new MaintenancePartBL();
            var ms = maintBL.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.MaintenancePart_PageTitle_Index + CommonValues.ExcelExtOld);
        }
    }
}
