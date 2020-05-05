using ODMS.Filters;
using ODMSBusiness;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.StockCardPriceListModel;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class StockCardPricePartialController : ControllerBase
    {
        #region Fields

        private readonly StockCardPriceListBL _stockCardPriceListService = new StockCardPriceListBL();
        private readonly StockCardPriceListModel _stockCardPriceListModel = new StockCardPriceListModel();

        #endregion

        #region Actions

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.StockCard.StockCardPriceList)]
        public ActionResult Index(int stockId)
        {
            Session["StockID"] = stockId;

            if (stockId == 0)
                return RedirectToAction("Index", "ErrorPage");

            _stockCardPriceListModel.DealerId = UserManager.UserInfo.GetUserDealerId();
            _stockCardPriceListService.Select(UserManager.UserInfo,_stockCardPriceListModel);

            ViewBag.PriceList = _stockCardPriceListModel.PriceList;

            return PartialView(_stockCardPriceListModel);
        }

        [HttpPost]
        public JsonResult GetAllPrice(int? priceId)
        {
            _stockCardPriceListModel.StockCardId = Session["StockID"].GetValue<int>();
            _stockCardPriceListModel.PartId = _stockCardPriceListService.Get(UserManager.UserInfo, _stockCardPriceListModel).Model.PartId;
            _stockCardPriceListModel.DealerId = UserManager.UserInfo.GetUserDealerId();
            _stockCardPriceListModel.PriceId = priceId.Value;

            _stockCardPriceListModel.CompanyPrice = _stockCardPriceListService.Get(UserManager.UserInfo, _stockCardPriceListModel, CommonValues.StockCardPriceType.C).Model.CompanyPrice;
            _stockCardPriceListModel.CostPrice = _stockCardPriceListService.Get(UserManager.UserInfo, _stockCardPriceListModel, CommonValues.StockCardPriceType.D).Model.CostPrice;
            _stockCardPriceListModel.ListPrice = _stockCardPriceListService.Get(UserManager.UserInfo, _stockCardPriceListModel, CommonValues.StockCardPriceType.L).Model.ListPrice;

            return Json(_stockCardPriceListModel);
        }

        [HttpPost]
        public JsonResult GetListPrice(int priceId)
        {
            _stockCardPriceListModel.StockCardId = Session["StockID"].GetValue<int>();
            _stockCardPriceListModel.PartId = _stockCardPriceListService.Get(UserManager.UserInfo,_stockCardPriceListModel).Model.PartId;
            _stockCardPriceListModel.DealerId = UserManager.UserInfo.GetUserDealerId();
            _stockCardPriceListModel.PriceId = priceId;

            _stockCardPriceListModel.ListPrice = _stockCardPriceListService.Get(UserManager.UserInfo,_stockCardPriceListModel, CommonValues.StockCardPriceType.L).Model.ListPrice;

            return Json(_stockCardPriceListModel);
        }

        #endregion

    }
}
