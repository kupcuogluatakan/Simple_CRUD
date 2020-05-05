using System.Collections.Generic;
using ODMSData;
using ODMSModel.Maintenance;
using ODMSCommon.Resources;
using System.IO;
using ODMSCommon;
using System.Text;
using System.Linq;
using System.Web.Mvc;
using ODMSModel.VehicleType;
using ODMSCommon.Security;
using System.Data;
using ODMSModel.DownloadFileActionResult;

namespace ODMSBusiness
{
    public class MaintenanceBL : BaseBusiness, IDownloadFile<MaintenanceViewModel>
    {
        private readonly MaintenanceData data = new MaintenanceData();
        public ResponseModel<MaintenanceListModel> GetMaintenanceList(UserInfo user, MaintenanceListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<MaintenanceListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetMaintenanceList(user, filter, out totalCnt);
                response.Total = totalCnt;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<MaintenanceListModel> GetMaintenanceForMaintId(UserInfo user, MaintenanceListModel filter)
        {
            var response = new ResponseModel<MaintenanceListModel>();
            try
            {
                response.Model = data.GetMaintenanceForMaintId(user, filter);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<MaintenanceListModel> GetMaintenanceForPkColumns(MaintenanceListModel filter)
        {
            var response = new ResponseModel<MaintenanceListModel>();
            try
            {
                response.Model = data.GetMaintenanceForPkColumns(filter);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<MaintenanceViewModel> DMLMaintenance(UserInfo user, MaintenanceViewModel model)
        {
            if (model.MaintKM == 0)
                model.MaintKM = null;
            if (model.MaintMonth == 0)
                model.MaintMonth = null;

            var response = new ResponseModel<MaintenanceViewModel>();
            try
            {
                data.DMLMaintenance(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<MaintenanceViewModel> GetMaintenance(UserInfo user, MaintenanceViewModel filter)
        {
            var response = new ResponseModel<MaintenanceViewModel>();
            try
            {
                data.GetMaintenance(user, filter);
                response.Model = filter;
                response.Message = MessageResource.Global_Display_Success;
                if (filter.ErrorNo > 0)
                    throw new System.Exception(filter.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<MaintenanceViewModel> ParseExcel(UserInfo user, MaintenanceViewModel model, Stream s)
        {
            List<MaintenanceViewModel> excelList = new List<MaintenanceViewModel>();

            var response = new ResponseModel<MaintenanceViewModel>();
            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                List<string> languageCodes = new List<string>();
                StringBuilder multiLanguageText = new StringBuilder();

                #region ColumnControl
                if (excelRows.Columns.Count < 14)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.Labour_Warning_ColumnProblem;
                }
                // kolon isimleri
                for (int i = 13; i < excelRows.Columns.Count; i++)
                {
                    string descLang = excelRows.Columns[i].ToString();
                    if (!string.IsNullOrEmpty(descLang) && descLang.Contains('_'))
                    {
                        List<string> langList = descLang.Split('_').ToList();
                        string langCode = langList.ElementAt(1);
                        languageCodes.Add(langCode);
                    }
                }
                #endregion

                foreach (DataRow excelRow in excelRows.Rows)
                {
                    MaintenanceViewModel row = new MaintenanceViewModel();
                    if (languageCodes.Count == 0)
                    {
                        row.ErrorNo = 1;
                        row.ErrorMessage = MessageResource.Labour_Warning_MissingLanguageCode;
                    }
                    else
                    {
                        #region MaintID
                        row._MaintId = excelRow[0].GetValue<string>();
                        if (!string.IsNullOrEmpty(excelRow[0].GetValue<string>()))
                        {

                            int maintId;
                            if (!int.TryParse(row._MaintId, out maintId))
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Labour_Warning_MaintIdMustBeNumeric;
                            }
                            else
                            {
                                var maintModel = new MaintenanceListModel() { MaintId = row._MaintId.GetValue<int>() };
                                var maintVal = new MaintenanceBL().GetMaintenanceForMaintId(user, maintModel).Model;

                                if (maintVal.MaintName == null)
                                {
                                    row.ErrorNo = 1;
                                    row.ErrorMessage = MessageResource.Labour_Warning_EmptyMaintId;
                                }
                                else
                                {
                                    row.MaintId = maintVal.MaintId;
                                }
                            }
                        }

                        #endregion

                        #region MultiLanguage

                        multiLanguageText.Clear();
                        for (int i = 13; i < excelRows.Columns.Count; i++)
                        {
                            string multiLanguageValue = excelRow[i].GetValue<string>();
                            if (!string.IsNullOrEmpty(multiLanguageValue))
                            {
                                string languageCode = languageCodes.ElementAt(i - 13);

                                multiLanguageText.Append(languageCode.ToUpper());
                                multiLanguageText.Append(CommonValues.Pipe);
                                multiLanguageText.Append(multiLanguageValue);
                                multiLanguageText.Append(CommonValues.Pipe);
                            }
                        }
                        row.MultiLanguageContentAsText = multiLanguageText.ToString();
                        if (multiLanguageText.ToString().Length == 0)
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_EmptyAdminDesc;
                        }
                        if (!multiLanguageText.ToString().Contains("TR"))
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_MissingAdminDescTR;
                        }

                        #endregion

                        #region VehicleModelCode
                        string modelCode = excelRow[1].GetValue<string>();
                        string typeName = excelRow[2].GetValue<string>();
                        SelectListItem vt = new SelectListItem();
                        if (string.IsNullOrEmpty(modelCode))
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_EmptyMainGroupCode;
                        }
                        else
                        {
                            var list_VehicleT = VehicleTypeBL.ListVehicleTypeAsSelectList(user, null).Data;

                            vt = (from r in list_VehicleT.AsEnumerable() where r.Text == modelCode + " " + typeName select r).FirstOrDefault();

                            if (vt == null)
                            {
                                // exceldeki ilgili satırın yanına hata yazılır.
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.LabourDuration_Warning_CannotFindVehicleModel;
                            }
                            else
                            {
                                row.VehicleTypeId = int.Parse(vt.Value);
                            }

                        }
                        row.VehicleTypeName = typeName;
                        row.VehicleModelName = modelCode;

                        #endregion

                        #region EngineType
                        string engineType = excelRow[3].GetValue<string>();
                        if (string.IsNullOrEmpty(engineType))
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.LabourDuration_Warning_EmptyEngineType;
                        }
                        else if (vt == null || int.Parse(vt.Value) == 0)
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.LabourDuration_Warning_CannotFindVehicleModel;
                        }
                        else
                        {
                            var engineTypes = VehicleBL.ListVehicleEngineTypesAsSelectListItem(user, int.Parse(vt.Value)).Data;
                            var engineControl = (from r in engineTypes.AsEnumerable() where r.Text == engineType select r.Text);
                            if (!engineControl.Any())
                            {
                                // exceldeki ilgili satırın yanına hata yazılır.
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.LabourDuration_Warning_CannotFindEngineType;
                            }
                        }
                        row.EngineType = engineType;
                        #endregion

                        #region MaintenanceType
                        string maintType = excelRow[4].GetValue<string>();
                        if (string.IsNullOrEmpty(maintType))
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_EmptySubGroupCode;
                        }
                        else
                        {
                            List<SelectListItem> list_MaintT = CommonBL.ListLookup(user, CommonValues.LookupKeys.MaintenanceTypeLookup).Data;
                            var maintControl = (from r in list_MaintT.AsEnumerable() where r.Text == maintType select r.Value);
                            if (!maintControl.Any())
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Labour_Warning_CannotFindMaintenanceType;
                            }
                            else
                            {
                                row.MaintTypeId = maintControl.ElementAt(0).GetValue<string>();
                            }

                        }
                        row.MaintTypeName = maintType;

                        #endregion

                        #region Km
                        row.MaintKM = excelRow[5].GetValue<int?>();
                        if (row.MaintKM == null || row.MaintKM <= 0)
                        {
                            if (row.MaintTypeId != null && row.MaintTypeId == "IT_K" && row.MaintTypeId == "IT_P")
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Maintenance_Error_KMMust;
                            }
                        }
                        #endregion

                        #region Month
                        row.MaintMonth = excelRow[6].GetValue<int?>();
                        #endregion

                        #region Main Category
                        string mainCategory = excelRow[7].GetValue<string>();
                        if (string.IsNullOrEmpty(mainCategory))
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_EmptymainCategory;
                        }
                        else
                        {
                            List<SelectListItem> list_mainCat = AppointmentIndicatorMainCategoryBL.ListAppointmentIndicatorMainCategories(user, true).Data;
                            var mainControl = (from r in list_mainCat.AsEnumerable()
                                               where r.Text == mainCategory
                                               select r.Value);
                            if (!mainControl.Any())
                            {
                                // exceldeki ilgili satırın yanına hata yazılır.
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Labour_Warning_CannotFindmainCategory;
                            }
                            else
                            {
                                row.MainCategoryId = mainControl.ElementAt(0).GetValue<int>();
                            }
                        }
                        row.MainCategoryName = mainCategory;
                        #endregion

                        #region Category
                        string Category = excelRow[8].GetValue<string>();
                        if (!string.IsNullOrEmpty(Category))
                        {
                            List<SelectListItem> list_Cat = AppointmentIndicatorCategoryBL.ListAppointmentIndicatorCategories(user, row.MainCategoryId, true).Data;
                            var CatControl = (from r in list_Cat.AsEnumerable() where r.Text == Category select r.Value);
                            if (!CatControl.Any())
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Labour_Warning_CannotFindCategory;
                            }
                            else
                                row.CategoryId = CatControl.ElementAt(0).GetValue<int>();
                        }
                        row.CategoryName = Category;

                        #endregion

                        #region SubCategory
                        string subCategory = excelRow[9].GetValue<string>();
                        if (!string.IsNullOrEmpty(subCategory))
                        {
                            List<SelectListItem> list_subCat = AppointmentIndicatorSubCategoryBL.ListAppointmentIndicatorSubCategories(user, row.SubCategoryId, true).Data;
                            var subCatControl = (from r in list_subCat.AsEnumerable() where r.Text == subCategory select r.Value);
                            if (!subCatControl.Any())
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Labour_Warning_CannotFindSubCategory;
                            }
                            else
                                row.SubCategoryId = subCatControl.ElementAt(0).GetValue<int>();
                        }
                        row.SubCategoryName = subCategory;
                        #endregion

                        #region FailureCode
                        string failureCode = excelRow[10].GetValue<string>();
                        if (!string.IsNullOrEmpty(failureCode))
                        {
                            var f_Code = new WorkOrderCardBL().ListFailureCodes2(UserManager.UserInfo).Data;
                            var fCodeControl = (from r in f_Code.AsEnumerable() where r.Description == failureCode select r.Value);
                            if (!fCodeControl.Any())
                            {
                                // exceldeki ilgili satırın yanına hata yazılır.
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Labour_Warning_CannotFindFailureCode;
                            }
                            else
                            {
                                row.FailureCodeId = fCodeControl.ElementAt(0).GetValue<int>();
                            }
                        }
                        row.FailureCodeName = failureCode;

                        #endregion

                        #region Admin Desc

                        row.AdminDesc = excelRow[11].GetValue<string>();
                        if (string.IsNullOrEmpty(row.AdminDesc))
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_EmptyAdminDesc;
                        }
                        else if (row.AdminDesc.Length > 250)
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_LengthAdminDesc;
                        }

                        #endregion

                        #region Is Active

                        int isAct;
                        if (!int.TryParse(excelRow[12].GetValue<string>(), out isAct))
                        {
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.Labour_Warning_IsActiveMustBeNumeric;
                        }
                        else
                        {
                            if (isAct != 0 && isAct != 1)
                            {
                                row.ErrorNo = 1;
                                row.ErrorMessage = MessageResource.Labour_Warning_IsActiveMust1or0;
                            }
                            else
                            {
                                row.IsActive = isAct.GetValue<int>() != 0;
                            }
                        }

                        #endregion

                    }
                    excelList.Add(row);
                }
                var errorCount = (from r in excelList.AsEnumerable()
                                  where r.ErrorNo == 1
                                  select r);
                if (errorCount.Any())
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.Labour_Warning_FoundError;
                }
                if (excelRows.Rows.Count == 0)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyExcel;
                }

                response.Data = excelList;
                response.Total = excelList.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public MemoryStream SampleExcelFormat()
        {
            string lastTable = CommonValues.TableStart + CommonValues.RowStart + CommonValues.ColumnStart +
                               MessageResource.Maintenance_Display_Name + " </TD>" +
                               CommonValues.ColumnStart +
                               MessageResource.VehicleModel_Display_Code +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.VehicleType_Display_Name +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.Vehicle_Display_EngineType +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.Maintenance_Display_Type +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.Maintenance_Display_Km +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.Maintenance_Display_Month +
                               CommonValues.ColumnEnd +
                                 CommonValues.ColumnStart +
                               MessageResource.Global_Display_MainCategory +
                               CommonValues.ColumnEnd +
                                 CommonValues.ColumnStart +
                               MessageResource.Global_Display_Category +
                               CommonValues.ColumnEnd +
                                 CommonValues.ColumnStart +
                               MessageResource.Global_Display_SubCategory +
                               CommonValues.ColumnEnd +
                                 CommonValues.ColumnStart +
                               MessageResource.AppointmentIndicatorFailureCode_Display_Code +
                               CommonValues.ColumnEnd +
                                 CommonValues.ColumnStart +
                               MessageResource.BreakdownDefinition_Display_AdminDesc +
                               CommonValues.ColumnEnd +
                                 CommonValues.ColumnStart +
                               MessageResource.Global_Display_IsActive +
                               CommonValues.ColumnEnd +
                               CommonValues.RowEnd +
                               CommonValues.TableEnd;

            var ms = new MemoryStream();
            var sw = new StreamWriter(ms, Encoding.UTF8);
            var sb = new StringBuilder();

            sb.Append(lastTable);

            sw.WriteLine(sb.ToString());

            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }

        public MemoryStream SetExcelReport(List<MaintenanceViewModel> modelList, string errMsg)
        {
            string errorMessage = CommonValues.RowStart + errMsg + CommonValues.RowEnd;

            string preTable = CommonValues.TableStart + CommonValues.RowStart +
                               CommonValues.ColumnStart +
                               MessageResource.Maintenance_Display_ID +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.VehicleModel_Display_Code +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.VehicleType_Display_Name +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.Vehicle_Display_EngineType +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.Maintenance_Display_Type +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.Maintenance_Display_Km +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.Maintenance_Display_Month +
                               CommonValues.ColumnEnd +
                                 CommonValues.ColumnStart +
                               MessageResource.Global_Display_MainCategory +
                               CommonValues.ColumnEnd +
                                 CommonValues.ColumnStart +
                               MessageResource.Global_Display_Category +
                               CommonValues.ColumnEnd +
                                 CommonValues.ColumnStart +
                               MessageResource.Global_Display_SubCategory +
                               CommonValues.ColumnEnd +
                                 CommonValues.ColumnStart +
                               MessageResource.AppointmentIndicatorFailureCode_Display_Code +
                               CommonValues.ColumnEnd +
                                 CommonValues.ColumnStart +
                               MessageResource.BreakdownDefinition_Display_AdminDesc +
                               CommonValues.ColumnEnd +
                                 CommonValues.ColumnStart +
                               MessageResource.Global_Display_IsActive +
                               CommonValues.ColumnEnd +
                               CommonValues.ColumnStart +
                               MessageResource.Maintenance_Display_Name + "_" + UserManager.UserInfo.LanguageCode + CommonValues.ColumnEnd;





            if (modelList != null)
            {
                preTable = preTable + ("<TD width='400px'>Log</TD>");
            }
            const string lastTable = CommonValues.RowEnd + CommonValues.TableEnd;
            MemoryStream ms = new MemoryStream();

            var sw = new StreamWriter(ms, Encoding.UTF8);

            StringBuilder sb = new StringBuilder();
            sb.Append(errorMessage);
            sb.Append(preTable);
            if (modelList != null)
            {
                foreach (var model in modelList)
                {
                    sb.AppendFormat(CommonValues.RowStart +
                                    CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{1}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{2}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{3}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{4}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{5}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{6}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{7}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{8}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{9}" + CommonValues.ColumnEnd +
                                     CommonValues.ColumnStart + "{10}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{11}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{12}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{13}" + CommonValues.ColumnEnd,
                                    model._MaintId, model.VehicleModelName, model.VehicleTypeName, model.EngineType,
                                    model.MaintTypeName, model.MaintKM, model.MaintMonth,
                                    model.MainCategoryName, model.CategoryName, model.SubCategoryName, model.FailureCodeName, model.AdminDesc, model.IsActive.GetValue<int>(),
                                    model.MultiLanguageContentAsText);
                    if (model.ErrorNo > 0)
                    {
                        sb.AppendFormat("<TD bgcolor='#FFCCCC'>{0}</TD>" + CommonValues.RowStart, model.ErrorMessage);
                    }
                    else
                    {
                        sb.AppendFormat(CommonValues.ColumnStart + CommonValues.ColumnEnd + CommonValues.RowEnd);
                    }
                }
            }
            sb.Append(lastTable);

            sw.WriteLine(sb.ToString());

            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }


    }
}
