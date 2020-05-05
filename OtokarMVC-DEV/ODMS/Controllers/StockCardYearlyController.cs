using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.StockCardYearly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    public class Result
    {
        public decimal? _1 { get; set; }
        public decimal? _2 { get; set; }
        public decimal? _3 { get; set; }
        public decimal? _4 { get; set; }
        public decimal? _5 { get; set; }
        public decimal? _6 { get; set; }
        public decimal? _7 { get; set; }
        public decimal? _8 { get; set; }
        public decimal? _9 { get; set; }
        public decimal? _10 { get; set; }
        public decimal? _11 { get; set; }
        public decimal? _12 { get; set; }

    }
    [PreventDirectFilter]
    public class StockCardYearlyController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.StockTypeList = CommonBL.ListSelectedStockTypes(UserManager.UserInfo).Data;//ListStockTypes();
        }

        public ActionResult StockCardYearlyIndex(int IdDealer, Int64 IdPart, int Year)
        {
            SetDefaults();

            var stockCardYearlyList = new StockCardYearlyListModel
                {
                    IdDealer = IdDealer,
                    IdPart = IdPart,
                    Year = Year
                };
            //stockCardYearlyList.IdStockType = IdStockType;

            return View(stockCardYearlyList);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.StockCardYearly.StockCardYearlyIndex, CommonValues.PermissionCodes.StockCardYearly.StockCardYearlyIndex)]
        public ActionResult ListStockCardYearly([DataSourceRequest] DataSourceRequest request, StockCardYearlyListModel model)
        {
            SetDefaults();

            var stockCardYearlyBo = new StockCardYearlyBL();
            var v = new StockCardYearlyListModel(request);

            v.IdStockType = model.IdStockType;
            v.IdDealer = model.IdDealer;
            v.IdPart = model.IdPart;
            if (model.Year == DateTime.Now.Year)
            {
                v.Date = null;
            }
            else
            {
                int y = (int)model.Year;
                int m = DateTime.Now.Month;

                v.Date = new DateTime(y, m, 1);

            }
            

            var returnValue = stockCardYearlyBo.ListStockCardYearly(UserManager.UserInfo,v).Data;

            var resultList = new List<Result>();

            var result = new Result() {
                _1 = returnValue.FirstOrDefault(x => x.Month == 1) != null ? returnValue.FirstOrDefault(x => x.Month == 1).Quantity : 0,
                _2 = returnValue.FirstOrDefault(x => x.Month == 2) != null ? returnValue.FirstOrDefault(x => x.Month == 2).Quantity : 0,
                _3 = returnValue.FirstOrDefault(x => x.Month == 3) != null ? returnValue.FirstOrDefault(x => x.Month == 3).Quantity : 0,
                _4 = returnValue.FirstOrDefault(x => x.Month == 4) != null ? returnValue.FirstOrDefault(x => x.Month == 4).Quantity : 0,
                _5 = returnValue.FirstOrDefault(x => x.Month == 5) != null ? returnValue.FirstOrDefault(x => x.Month == 5).Quantity : 0,
                _6 = returnValue.FirstOrDefault(x => x.Month == 6) != null ? returnValue.FirstOrDefault(x => x.Month == 6).Quantity : 0,
                _7 = returnValue.FirstOrDefault(x => x.Month == 7) != null ? returnValue.FirstOrDefault(x => x.Month == 7).Quantity : 0,
                _8 = returnValue.FirstOrDefault(x => x.Month == 8) != null ? returnValue.FirstOrDefault(x => x.Month == 8).Quantity : 0,
                _9 = returnValue.FirstOrDefault(x => x.Month == 9) != null ? returnValue.FirstOrDefault(x => x.Month == 9).Quantity : 0,
                _10 = returnValue.FirstOrDefault(x => x.Month == 10) != null ? returnValue.FirstOrDefault(x => x.Month == 10).Quantity : 0,
                _11 = returnValue.FirstOrDefault(x => x.Month == 11) != null ? returnValue.FirstOrDefault(x => x.Month == 11).Quantity : 0,
                _12 = returnValue.FirstOrDefault(x => x.Month == 12) != null ? returnValue.FirstOrDefault(x => x.Month == 12).Quantity : 0

            };
            resultList.Add(result);
            return Json(new
            {
                Data = resultList
            });
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.StockCardYearly.StockCardYearlyIndex, CommonValues.PermissionCodes.StockCardYearly.StockCardYearlyIndex)]
        public void StartUpdateMonthlyStock(int idDealer, Int64 idPart, int idStockType)//(StockCardYearlyListModel model)
        {
            StockCardYearlyListModel model = new StockCardYearlyListModel()
            {
                IdDealer = idDealer,
                IdPart = idPart,
                IdStockType = idStockType
            };

            var stockCardYearlyBo = new StockCardYearlyBL();
            stockCardYearlyBo.StartUpdateMonthlyStock(UserManager.UserInfo,model);
        }
    }
}