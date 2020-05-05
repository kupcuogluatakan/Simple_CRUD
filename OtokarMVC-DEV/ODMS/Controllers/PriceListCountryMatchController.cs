using System.Collections.Generic;
using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Resources;
using ODMSModel.PriceListCountryMatch;
using Permission = ODMSCommon.CommonValues.PermissionCodes.PriceListCountryMatch;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PriceListCountryMatchController : ControllerBase
    {

        [AuthorizationFilter(Permission.PriceListCountryMatchIndex)]
        public ActionResult PriceListCountryMatchIndex()
        {
            ViewBag.PriceList = new PriceListCountryMatchBL().GetPriceLists().Data;
            var model = new PriceListCountryMatchSaveModel();
            new PriceListCountryMatchBL().Save(UserManager.UserInfo, model);
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.PriceListCountryMatchIndex)]
        public JsonResult ListPriceListIncludedInCountry(int? priceListId)
        {
            return priceListId.HasValue == false
                ? Json(new { Data = new List<SelectListItem>() })
                : Json(new { Data = new PriceListCountryMatchBL().GetCountriesIncluded(UserManager.UserInfo, priceListId ?? 0).Data });
        }

        [HttpPost]
        public JsonResult ListPriceListNotIncludedInCountry(int? priceListId)
        {
            return priceListId.HasValue == false
                ? Json(new { Data = new List<SelectListItem>() })
                : Json(new { Data = new PriceListCountryMatchBL().GetCountriesExcluded(UserManager.UserInfo, priceListId ?? 0).Data });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.PriceListCountryMatchIndex, Permission.PriceListCountryMatchUpdate)]
        public JsonResult Save(int PriceListId, IList<int> includedPriceListIds, IList<bool> IsDefaultList)
        {
            var plCount = includedPriceListIds != null ? includedPriceListIds.Count : 0;
            var dlCount = IsDefaultList != null ? IsDefaultList.Count : 0;
            if (/*plCount > 0 && dlCount > 0 && */ plCount == dlCount)
            {
                var bo = new PriceListCountryMatchBL();
                var model = new PriceListCountryMatchSaveModel();
                model.PriceListId = PriceListId;
                for (int i = 0; i < plCount; i++)
                {
                    var item = string.Format("{0}||{1}", includedPriceListIds[i].ToString(),
                        IsDefaultList[i] ? "true" : "false");

                    model.CountryList.Add(item);
                }


                bo.Save(UserManager.UserInfo, model);
                if (model.ErrorNo > 0)
                {
                    return Json(new { Result = false, Message = model.ErrorMessage, countryList = model.CountryList });
                }

                return Json(new { Result = true, Message = MessageResource.Global_Display_Success, countryList = model.CountryList });
            }

            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Err_Generic_Unexpected);

        }

    }
}
