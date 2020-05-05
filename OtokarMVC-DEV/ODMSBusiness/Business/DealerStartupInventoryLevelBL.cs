using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.SparePart;
using ODMSCommon.Security;
using ODMSModel.DealerStartupInventoryLevel;
using System.Data;

namespace ODMSBusiness
{
    public class DealerStartupInventoryLevelBL : BaseBusiness, IDownloadFile<DealerStartupInventoryLevelViewModel>
    {
        private readonly DealerStartupInventoryLevelData data = new DealerStartupInventoryLevelData();

        public ResponseModel<DealerStartupInventoryLevelListModel> ListDealerStartupInventoryLevels(UserInfo user,DealerStartupInventoryLevelListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<DealerStartupInventoryLevelListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListDealerStartupInventoryLevels(user,filter, out totalCnt);
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

        public ResponseModel<DealerStartupInventoryLevelViewModel> GetDealerStartupInventoryLevel(DealerStartupInventoryLevelViewModel filter)
        {
            var response = new ResponseModel<DealerStartupInventoryLevelViewModel>();
            try
            {
                response.Model = data.GetDealerStartupInventoryLevel(filter);
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

        public ResponseModel<DealerStartupInventoryLevelViewModel> DMLDealerStartupInventoryLevel(UserInfo user, DealerStartupInventoryLevelViewModel model)
        {
            var response = new ResponseModel<DealerStartupInventoryLevelViewModel>();
            try
            {
                data.DMLDealerStartupInventoryLevel(user, model);
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

        public ResponseModel<DealerStartupInventoryLevelViewModel> ParseExcel(UserInfo user, DealerStartupInventoryLevelViewModel model, Stream s)
        {
            List<DealerStartupInventoryLevelViewModel> excelList = new List<DealerStartupInventoryLevelViewModel>();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            List<SelectListItem> dealerClassList = new DealerBL().ListDealerClassesAsSelectListItem().Data;

            var response = new ResponseModel<DealerStartupInventoryLevelViewModel>();

            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0];

                if (excelRows.Columns.Count < 3)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.Global_Warning_ExcelFormat;
                }
                else
                {
                    foreach (DataRow excelRow in excelRows.Rows)
                    {
                        DealerStartupInventoryLevelViewModel row = new DealerStartupInventoryLevelViewModel
                        {
                            DealerClassName = excelRow[0].GetValue<string>()
                        };

                        if (string.IsNullOrEmpty(row.DealerClassName))
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyDealerClass;
                        }
                        // bayi tipi kontrol ediliyor.
                        var dealerClassControl = (from dc in dealerClassList.AsEnumerable()
                                                  where dc.Value == row.DealerClassName
                                                  select dc.Value);
                        if (!dealerClassControl.Any())
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_DealerClassNotFound;
                        }
                        else
                        {
                            row.DealerClassCode = dealerClassControl.ElementAt(0).GetValue<string>();
                        }

                        row.PartName = excelRow[1].GetValue<string>();
                        if (string.IsNullOrEmpty(row.PartName))
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyPartCode;
                        }
                        // parça kontrol ediliyor.
                        SparePartBL spBo = new SparePartBL();
                        SparePartIndexViewModel spModel = new SparePartIndexViewModel { PartCode = row.PartName };
                        spBo.GetSparePart(user, spModel);
                        if (spModel.PartId == 0)
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_PartCodeNotFound;
                        }
                        else
                        {
                            row.PartId = spModel.PartId;
                        }

                        decimal quantity;
                        if (decimal.TryParse(excelRow[2].GetValue<string>(), out quantity))
                        {
                            row.Quantity = quantity;
                        }
                        else
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_QuantityLength;
                        }
                        if (row.Quantity > 999)
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_QuantityLength;
                        }
                        if (row.Quantity == 0)
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyQuantity;
                        }
                        row.IsActive = true;

                        var existed = (from r in excelList.AsEnumerable()
                                       where
                                           r.DealerClassName == row.DealerClassName && r.PartName == row.PartName &&
                                           r.Quantity != row.Quantity
                                       select r);
                        if (existed.Any())
                        {
                            // exceldeki ilgili satırın yanına hata yazılır.
                            row.ErrorNo = 1;
                            row.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_DuplicateValue;
                        }

                        excelList.Add(row);
                    }

                    var errorCount = (from r in excelList.AsEnumerable()
                                      where r.ErrorNo > 0
                                      select r);
                    if (errorCount.Any())
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_NotFoundError;
                    }
                    if (excelRows.Rows.Count == 0)
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.DealerStartupInventoryLevel_Warning_EmptyExcel;
                    }
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

        public MemoryStream SetExcelReport(List<DealerStartupInventoryLevelViewModel> listModels, string errorMessage)
        {
            string preTable = CommonValues.TableStart + CommonValues.RowStart
                + CommonValues.ColumnStart + MessageResource.DealerStartupInventoryLevel_Display_DealerClassName
                              + CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.DealerStartupInventoryLevel_Display_PartName +
                              CommonValues.ColumnEnd + CommonValues.ColumnStart + MessageResource.DealerStartupInventoryLevel_Display_Quantity + CommonValues.ColumnEnd;
            if (listModels != null)
            {
                preTable = preTable + ("<TD width='400px'>Log</TD>");
            }
            const string lastTable = CommonValues.RowEnd + CommonValues.TableEnd;
            MemoryStream ms = new MemoryStream();

            var sw = new StreamWriter(ms, Encoding.UTF8);

            StringBuilder sb = new StringBuilder();
            sb.Append(errorMessage);
            sb.Append("</BR>");
            sb.Append(preTable);
            if (listModels != null)
            {
                foreach (var model in listModels)
                {
                    sb.AppendFormat(CommonValues.RowStart +
                                    CommonValues.ColumnStart + "{0}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{1}" + CommonValues.ColumnEnd +
                                    CommonValues.ColumnStart + "{2}" + CommonValues.ColumnEnd, model.DealerClassName, model.PartName,
                                    model.Quantity);
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
