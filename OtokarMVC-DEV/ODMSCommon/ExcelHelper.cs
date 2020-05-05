using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Validation;
using System.Data;
using ExcelDataReader;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ODMSCommon
{
    public class ExcelHelper
    {
        public static DataSet GetDataFromExcel(Stream s)
        {
            IExcelDataReader er = ExcelReaderFactory.CreateReader(s);
            DataSet data = er.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true                    
                }
            });
            return data;
        }
        private Stylesheet CreateStylesheet()
        {
            Stylesheet ss = new Stylesheet();   
            var nfs = new NumberingFormats();
            var nformatDateTime = new NumberingFormat
            {
                NumberFormatId = UInt32Value.FromUInt32(14),
                FormatCode = StringValue.FromString("dd/mm/yyyy;@")
            };
            nfs.Append(nformatDateTime);
            ss.Append(nfs);
            return ss;
        }

        private string ColumnLetter(int intCol)
        {
            var intFirstLetter = ((intCol) / 676) + 64;
            var intSecondLetter = ((intCol % 676) / 26) + 64;
            var intThirdLetter = (intCol % 26) + 65;

            var firstLetter = (intFirstLetter > 64)
                ? (char)intFirstLetter : ' ';
            var secondLetter = (intSecondLetter > 64)
                ? (char)intSecondLetter : ' ';
            var thirdLetter = (char)intThirdLetter;

            return string.Concat(firstLetter, secondLetter,
                thirdLetter).Trim();
        }

        private Cell CreateTextCell(string header, UInt32 index,
            object val)
        {
            var cell = new Cell
            {
                CellReference = header + index,
                
            };
            FillCell(cell, val);
            return cell;
        }

        private void FillCell(Cell cell, object val)
        {
            DateTime dateval;
            long longval;
            int intval;
            short shortval;
            Decimal decimalval;
           
            if (Int64.TryParse(val.ToString(), out longval))
            {
                cell.DataType = CellValues.Number;
                cell.CellValue = new CellValue(val.ToString());               
            } 
            else if (Int32.TryParse(val.ToString(), out intval))
            {
                cell.DataType=CellValues.Number;
                cell.CellValue=new CellValue(val.ToString());
            }
            else if (Int16.TryParse(val.ToString(), out shortval))
            {
                cell.DataType = CellValues.Number;
                cell.CellValue = new CellValue(val.ToString());
            }
            else if (Decimal.TryParse(val.ToString(), out decimalval))
            {
                cell.DataType = CellValues.Number;
                cell.CellValue = new CellValue(decimalval.RoundUp(2).ToString().Replace(System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator, "."));
            }
            else if (DateTime.TryParse(val.ToString(), out dateval))
            {
                cell.DataType= CellValues.Date;
                cell.CellValue = new CellValue(dateval.ToString());
   
            }
            else
            {
                //cell.DataType == CellValues.InlineString;
                cell.InlineString = new InlineString { Text = new Text { Text = val.ToString()} };
            }
         
        }
        private void AddHeaders(ExcelWorksheet worksheet, IEnumerable<string> headers)
        {
            var coloumCount = headers.Count() + 1;

            using (var range = worksheet.Cells[1, 1, 1, coloumCount])
            {
                int i = 0;
                foreach (var header in headers)
                {
                    worksheet.Cells[1, i + 1].Value = header;
                    i++;
                }

                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                range.Style.Font.Bold = true;
            }
        }
        public byte[] GenerateExcel(IEnumerable<string> headers, IEnumerable<IEnumerable<Tuple<object, string>>> list, List<FilterDto> filters, string reportName = "Sheet 1", object reportObject = null)
        {
            using (var package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(reportName);

                AddHeaders(worksheet, headers);

                InsertData(worksheet, list, reportObject);


                if (filters != null && filters.Any())
                {
                    ExcelWorksheet filterSheet = package.Workbook.Worksheets.Add("Filtreler");
                    AddFilters(filterSheet, filters);
                }

                return package.GetAsByteArray();

            }


        }

        private void AddFilters(ExcelWorksheet filterSheet, List<FilterDto> filters)
        {
            var rowIdex = 1;
            foreach (var filter in filters)
            {
                filterSheet.Cells[rowIdex, 1].Style.Font.Bold = true;
                filterSheet.Cells[rowIdex, 1].AutoFitColumns();
                filterSheet.Cells[rowIdex, 1].Value = filter.FilterName;

                filterSheet.Cells[rowIdex, 2].Value = filter.FilterValue;
                filterSheet.Cells[rowIdex, 2].AutoFitColumns();
                rowIdex++;
            }
        }
        private void SetRangeFormat(ExcelRange range, Type type, object val)
        {
            if (type == typeof(DateTime) || type == typeof(Nullable<DateTime>))
            {
                range.Style.Numberformat.Format = "dd/mm/yy hh:mm";
                if (!string.IsNullOrEmpty(val?.ToString()))
                {
                    range.Value = val;
                    //range.Value = DateTime.Parse(val.ToString(),new CultureInfo(UserManager.LanguageCode));
                    //range.Value = DateTime.ParseExact(val.ToString(), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                }
            }
            if (type == typeof(Int64) || type == typeof(Int32) || type == typeof(Int16)
                || type == typeof(Int64?) || type == typeof(Int32?) || type == typeof(Int16?)
                )
            {
                range.Style.Numberformat.Format = "#,##0";
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                if (!string.IsNullOrEmpty(val?.ToString()))
                {
                    range.Value = long.Parse(val.ToString());
                }
            }
            if (type == typeof(Decimal) || type == typeof(Decimal?))
            {
                range.Style.Numberformat.Format = "#,##0.00";
                if (!string.IsNullOrEmpty(val?.ToString()))
                {
                    range.Value = Decimal.Parse(val.ToString().Replace(",", "."));
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }
            }


        }
        private Type[] GetPropertyTypes(IEnumerable<Tuple<object, string>> firstList, object reportObject)
        {
            var arr = new Type[firstList.Count()];
            var reportObjectType = reportObject.GetType();
            int i = 0;
            foreach (var item in firstList)
            {
                try
                {
                    arr[i] = reportObjectType.GetProperty(item.Item2).PropertyType;
                }
                catch
                {
                    arr[i] = typeof(String);
                }

                i++;
            }
            return arr;
        }


        private void InsertData(ExcelWorksheet worksheet, IEnumerable<IEnumerable<Tuple<object, string>>> list, object reportObject)
        {
            if (list == null || !list.Any()) return;

            var propertyTypeArray = GetPropertyTypes(list.First(), reportObject);

            // SetStyleFormats(worksheet, propertyTypeArray, list.Count() + 2);

            var rowindex = 0;
            foreach (var row in list)
            {
                int colindex = 0;

                foreach (var item in row)
                {
                    SetRangeFormat(worksheet.Cells[rowindex + 2, colindex + 1], propertyTypeArray[colindex], item.Item1);
                    worksheet.Cells[rowindex + 2, colindex + 1].Value = item.Item1;
                    colindex++;
                }
                rowindex++;
            }

        }
    }
}

