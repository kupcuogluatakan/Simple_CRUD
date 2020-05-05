using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.CriticalStockCard;
using ODMSModel.CycleCountResult;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.Data;
using ODMSModel.DownloadFileActionResult;

namespace ODMSBusiness
{
    public class CycleCountResultBL : BaseBusiness, IDownloadFile<CycleCountResultViewModel>
    {
        private readonly CycleCountResultData data = new CycleCountResultData();
        
        public ResponseModel<CycleCountResultListModel> ListCycleCountResults(ODMSCommon.Security.UserInfo user,CycleCountResultListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CycleCountResultListModel>();
            totalCnt = 0;
            try
            {
                CycleCountStockDiffBL cycleCountStockDiffBL = new CycleCountStockDiffBL();
                StockTypeDetailBL stockTypeDetailBL = new StockTypeDetailBL();

                var boCycleCountResult = new CycleCountResultData();
                var result = boCycleCountResult.ListCycleCountResults(user,filter, out totalCnt);

                var groupedList = result.GroupBy(x => new { x.WarehouseId, x.StockCardId }).Select(g => new CycleCountResultListModel
                {
                    WarehouseId = g.Key.WarehouseId,
                    StockCardId = g.Key.StockCardId,//real value is partId
                    TotalQty = g.Min(d=>d.TotalQty)
                }).ToList();

                foreach (var item in groupedList)
                {
                    decimal approvedQty = result.Where(p => p.WarehouseId == item.WarehouseId && p.StockCardId == item.StockCardId).Sum(p => p.ApprovedCountQuantity);
                    decimal beforeQty = result.Where(p => p.WarehouseId == item.WarehouseId && p.StockCardId == item.StockCardId).Sum(p => p.BeforeCountQuantity);

                    var expectedQty = item.DetailTotalQty - (beforeQty - approvedQty);

                    if (expectedQty == item.DetailTotalQty)
                    {
                        foreach (var x in result.Where(x => x.WarehouseId == item.WarehouseId && x.StockCardId == item.StockCardId))
                        {
                            x.QtyState = StockState.Green;
                            x.TotalApprovedQty = expectedQty;
                            x.BeforeCycleCountValue = item.DetailTotalQty;
                        }
                    }
                    else
                    {
                        if (item.TotalQty != null)
                        {
                            if (expectedQty == item.TotalQty)
                            {
                                foreach (var x in result.Where(x => x.WarehouseId == item.WarehouseId && x.StockCardId == item.StockCardId))
                                {
                                    x.QtyState = StockState.Yellow;
                                    x.TotalApprovedQty = expectedQty;
                                    x.BeforeCycleCountValue = item.DetailTotalQty;
                                }
                            }
                            else
                            {
                                foreach (var x in result.Where(x => x.WarehouseId == item.WarehouseId && x.StockCardId == item.StockCardId))
                                {
                                    x.QtyState = StockState.Red;
                                    x.TotalApprovedQty = expectedQty;
                                    x.BeforeCycleCountValue = item.DetailTotalQty;
                                }
                            }
                        }
                        else
                        {
                            foreach (var x in result.Where(x => x.WarehouseId == item.WarehouseId && x.StockCardId == item.StockCardId))
                            {
                                x.QtyState = StockState.Red;
                                x.TotalApprovedQty = expectedQty;
                                x.BeforeCycleCountValue = item.DetailTotalQty;
                            }
                        }
                    }
                }

                response.Data = result;
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

        public ResponseModel<CycleCountResultViewModel> GetCycleCountResult(ODMSCommon.Security.UserInfo user, CycleCountResultViewModel filter)
        {
            var response = new ResponseModel<CycleCountResultViewModel>();
            try
            {
                response.Model = data.GetCycleCountResult(user, filter);
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
        public ResponseModel<CycleCountResultViewModel> DMLCycleCountResultBulk(ODMSCommon.Security.UserInfo user, int cycleCountId, DataTable cycleCountResultList)
        {
            var response = new ResponseModel<CycleCountResultViewModel>();
            try
            {
                string errorMessage = data.DMLCycleCountResultBulk(user, cycleCountId, cycleCountResultList);
                response.Message = errorMessage;                
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }
        public ResponseModel<CycleCountResultViewModel> DMLCycleCountResult(ODMSCommon.Security.UserInfo user, CycleCountResultViewModel model)
        {
            var response = new ResponseModel<CycleCountResultViewModel>();
            try
            {
                data.DMLCycleCountResult(user, model);
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

        public ResponseModel<CycleCountResultViewModel> ParseExcel(ODMSCommon.Security.UserInfo user, CycleCountResultViewModel filter, Stream s)
        {
            var response = new ResponseModel<CycleCountResultViewModel>();
            try
            {
                DataSet ds = ExcelHelper.GetDataFromExcel(s);
                DataTable excelRows = ds.Tables[0]; 

                if (excelRows.Columns.Count != 4)
                {
                    filter.ErrorMessage = MessageResource.CycleCountResult_Warning_ExcelFormat;
                }

                response.Model = new CycleCountResultViewModel() { ExcelList = excelRows };
                response.Total = excelRows.Rows.Count;
                response.Message = string.IsNullOrEmpty(filter.ErrorMessage) ? MessageResource.Global_Display_Success : filter.ErrorMessage;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public MemoryStream SetExcelReport(List<CycleCountResultViewModel> modelList, string errMsg)
        {

            MemoryStream stream = new MemoryStream();
            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);


            // Add a WorkbookPart to the document.
            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add Sheets to the Workbook.
            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Sheet 1"
            };

            sheets.Append(sheet);

            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            Row row;
            row = new Row() { RowIndex = 1 };
            sheetData.Append(row);

            Cell refCell = null;
            foreach (Cell cell in row.Elements<Cell>())
            {
                if (cell.CellReference == "A1")
                {
                    refCell = cell;
                    break;
                }
            }

            Cell wareHouseCell = new Cell() { CellValue = new CellValue("Depo") };
            Cell rackCell = new Cell() { CellValue = new CellValue("Raf") };
            Cell partCodeCell = new Cell() { CellValue = new CellValue("Parca Kodu") };
            Cell quantityCell = new Cell() { CellValue = new CellValue("Miktar") };

            wareHouseCell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            rackCell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            partCodeCell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            quantityCell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

            row.Append(wareHouseCell);
            row.Append(rackCell);
            row.Append(partCodeCell);
            row.Append(quantityCell);

            worksheetPart.Worksheet.Save();
            workbookpart.Workbook.Save();

            // Close the document.
            spreadsheetDocument.Close();

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}
