using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.CountryVatRatio;
using ODMSModel.Dealer;
using ODMSModel.ListModel;
using ODMSModel.SparePart;
using ODMSModel.SparePartCountryVatRatio;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class SparePartController : ControllerBase
    {
        #region SparePart Index

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePart.SparePartIndex)]
        [HttpGet]
        public ActionResult SparePartIndex()
        {
            SetDefaults();
            var model = new SparePartIndexViewModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.DealerId = UserManager.UserInfo.GetUserDealerId();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePart.SparePartIndex, CommonValues.PermissionCodes.SparePart.SparePartDetails)]
        public ActionResult ListSparePart([DataSourceRequest] DataSourceRequest request, SparePartListModel model)
        {
            var sparePartBo = new SparePartBL();
            var v = new SparePartListModel(request);
            var totalCnt = 0;
            v.IsOriginal = model.IsOriginal;
            v.DealerId = model.DealerId;
            v.PartTypeCode = model.PartTypeCode;
            v.Unit = model.Unit;
            v.GuaranteeAuthorityNeed = model.GuaranteeAuthorityNeed;
            v.PartId = model.PartId;
            v.Brand = model.Brand;

            var returnValue = sparePartBo.ListSpareParts(UserManager.UserInfo,v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region SparePart Create
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePart.SparePartIndex, CommonValues.PermissionCodes.SparePart.SparePartCreate)]
        [HttpGet]
        public ActionResult SparePartCreate()
        {
            SetDefaults();
            var model = new SparePartIndexViewModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            model.IsActive = true;
            model.IsOrderAllowed = true;

            // TFS No : 28456 OYA 09.02.2015 dealer'a ait ülkenin default vat ratio değeri gelmesi sağlandı.
            int totalCount = 0;
            DealerBL dealerBo = new DealerBL();
            DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, model.DealerId.GetValue<int>()).Model;
            CountryVatRatioListModel cvrModel = new CountryVatRatioListModel();
            cvrModel.CountryId = dealerModel.Country.GetValue<int>();
            CountryVatRatioBL cvrBo = new CountryVatRatioBL();
            List<CountryVatRatioListModel> cvrList = cvrBo.ListCountryVatRatios(UserManager.UserInfo, cvrModel, out totalCount).Data;
            if (totalCount != 0)
                model.VatRatio = cvrList.ElementAt(0).PartVatRatio.ToString();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePart.SparePartIndex, CommonValues.PermissionCodes.SparePart.SparePartCreate)]
        [HttpPost]
        public ActionResult SparePartCreate(SparePartIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var sparePartBo = new SparePartBL();

            SparePartIndexViewModel orgMo = new SparePartIndexViewModel();
            orgMo.PartId = viewModel.OriginalPartId;
            sparePartBo.GetSparePart(UserManager.UserInfo, orgMo);
            viewModel.OriginalPartId = orgMo.PartId;
            viewModel.OriginalPartName = orgMo.PartCode + CommonValues.Slash + orgMo.PartNameInLanguage;

            if (ModelState.IsValid)
            {
                bool isValid = true;
                List<SelectListItem> languageList = LanguageBL.ListLanguageAsSelectListItem(UserManager.UserInfo).Data;
                List<string> langList = viewModel.MultiLanguageContentAsText.Split(new string[1] { CommonValues.Pipe }, System.StringSplitOptions.None).ToList<string>();
                for (int i = 0; i < langList.Count; i++)
                {
                    var control = (from e in languageList
                                   where e.Value == langList[i]
                                   select e);
                    if (control.Any())
                    {
                        if (i + 1 <= langList.Count)
                        {
                            string definition = langList[i + 1];
                            if (definition.Length > 50)
                            {
                                isValid = false;
                            }
                        }
                    }
                }
                if (!isValid)
                {
                    SetMessage(
                        MessageResource.SparePart_Warning_MultilanguageTextLength,
                        CommonValues.MessageSeverity.Fail);
                }
                else
                {
                    int totalCount = 0;
                    SparePartListModel cMo = new SparePartListModel();
                    cMo.PartCode = viewModel.PartCode;
                    List<SparePartListModel> control = sparePartBo.ListSpareParts(UserManager.UserInfo,cMo, out totalCount).Data;
                    if (control.Any())
                    {
                        SetMessage(MessageResource.SparePart_Warning_PartCodeExists, CommonValues.MessageSeverity.Fail);
                    }
                    else
                    {
                        SetDefaults();
                        //TFS NO : 29729 OYA
                        //if (UserManager.UserInfo.IsDealer)
                        //{
                        viewModel.PartTypeCode = orgMo.PartTypeCode;
                        //}

                        viewModel.CommandType = viewModel.PartId > 0
                                                    ? CommonValues.DMLType.Update
                                                    : CommonValues.DMLType.Insert;
                        sparePartBo.DMLSparePart(UserManager.UserInfo, viewModel);
                        CheckErrorForMessage(viewModel, true);

                        // TFS No : 28299 OYA 02.02.2015 Vat Ratio kontrolü eklendi.
                        if (!string.IsNullOrEmpty(viewModel.VatRatio))
                        {
                            DealerBL dealerBo = new DealerBL();
                            DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
                            decimal? countryVatRatio = CommonBL.GetCountryVatRatio(dealerModel.Country).Model;
                            if (countryVatRatio == null || countryVatRatio != viewModel.VatRatio.GetValue<decimal>())
                            {
                                SparePartCountryVatRatioBL spcvrBo = new SparePartCountryVatRatioBL();
                                SparePartCountryVatRatioViewModel spcvrModel = new SparePartCountryVatRatioViewModel();
                                spcvrModel.CommandType = CommonValues.DMLType.Insert;
                                spcvrModel.IdCountry = dealerModel.Country;
                                spcvrModel.IdPart = viewModel.PartId;
                                spcvrModel.IsActive = true;
                                spcvrModel.VatRatio = viewModel.VatRatio.GetValue<decimal>();
                                spcvrBo.DMLSparePartCountryVatRatio(UserManager.UserInfo, spcvrModel);
                            }
                        }
                        ModelState.Clear();
                        var model = new SparePartIndexViewModel();
                        if (UserManager.UserInfo.GetUserDealerId() != 0)
                            model.DealerId = UserManager.UserInfo.GetUserDealerId();
                        return View(model);
                    }
                }
            }
            SetDefaults();
            return View(viewModel);
        }

        #endregion

        #region SparePart Update
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePart.SparePartIndex, CommonValues.PermissionCodes.SparePart.SparePartUpdate)]
        [HttpGet]
        public ActionResult SparePartUpdate(int id = 0)
        {
            var v = new SparePartIndexViewModel();
            if (id > 0)
            {
                SetDefaults();
                var sparePartBo = new SparePartBL();
                v.PartId = id;
                sparePartBo.GetSparePart(UserManager.UserInfo, v);
                var originalPartId = v.IsOriginal == 1 ? id : v.OriginalPartId;
                var orgMo = new SparePartIndexViewModel { PartId = originalPartId };
                sparePartBo.GetSparePart(UserManager.UserInfo, orgMo);
                v.OriginalPartName = orgMo.PartCode + CommonValues.Slash + v.OriginalPartName;

                DealerBL dealerBo = new DealerBL();
                DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;

                SparePartCountryVatRatioViewModel spcvrModel = new SparePartCountryVatRatioViewModel();
                spcvrModel.IdCountry = dealerModel.Country;
                spcvrModel.IdPart = id;
                SparePartCountryVatRatioBL spcvrBo = new SparePartCountryVatRatioBL();
                spcvrBo.GetSparePartCountryVatRatio(UserManager.UserInfo, spcvrModel);
                v.VatRatio = spcvrModel.VatRatio.GetValue<string>();
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePart.SparePartIndex, CommonValues.PermissionCodes.SparePart.SparePartUpdate)]
        [HttpPost]
        public ActionResult SparePartUpdate(SparePartIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var sparePartBo = new SparePartBL();
            var originalPartId = viewModel.IsOriginal == 1 ? viewModel.PartId : viewModel.OriginalPartId;

            SparePartIndexViewModel orgMo = new SparePartIndexViewModel();
            orgMo.PartId = originalPartId;
            sparePartBo.GetSparePart(UserManager.UserInfo, orgMo);
            viewModel.OriginalPartName = orgMo.PartCode + CommonValues.Slash + orgMo.PartNameInLanguage;

            bool isValid = true;
            if (ModelState.IsValid)
            {
                // bayi ise
                if (UserManager.UserInfo.IsDealer)
                {
                    // orjinal parça ise
                    if (viewModel.IsOriginal.GetValue<bool>())
                    {
                        isValid = false;
                        viewModel.ErrorNo = 1;
                        viewModel.ErrorMessage = MessageResource.SparePart_Warning_OriginalPartDealerUpdate;
                    }
                    // eşlenik parça ise
                    else
                    {
                        // sadece kendi dealer id ile aynı olan kaydı güncelleyebilir.
                        if (viewModel.DealerId != UserManager.UserInfo.GetUserDealerId())
                        {
                            isValid = false;
                            viewModel.ErrorNo = 1;
                            viewModel.ErrorMessage = MessageResource.SparePart_Warning_DealerUpdate;
                        }
                    }
                }
            }
            if (isValid)
            {
                int totalCount = 0;
                SparePartListModel cMo = new SparePartListModel();
                cMo.PartCode = viewModel.PartCode;
                List<SparePartListModel> controlList = sparePartBo.ListSpareParts(UserManager.UserInfo,cMo, out totalCount).Data;
                var control = (from r in controlList.AsEnumerable()
                               where r.PartId != viewModel.PartId
                               && r.PartCode == viewModel.PartCode
                               select r);
                if (controlList.Any() && control.Any())
                {
                    SetMessage(MessageResource.SparePart_Warning_PartCodeExists, CommonValues.MessageSeverity.Fail);
                }
                else
                {
                    //TFS NO : 29729 OYA
                    //if (UserManager.UserInfo.IsDealer)
                    //{
                    viewModel.PartTypeCode = orgMo.PartTypeCode;
                    //}
                    viewModel.CommandType = viewModel.PartId > 0
                                                ? CommonValues.DMLType.Update
                                                : CommonValues.DMLType.Insert;
                    sparePartBo.DMLSparePart(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);

                    // TFS No : 28299 OYA 02.02.2015 Vat Ratio kontrolü eklendi.
                    if (!string.IsNullOrEmpty(viewModel.VatRatio))
                    {
                        DealerBL dealerBo = new DealerBL();
                        DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;

                        decimal? countryVatRatio = CommonBL.GetCountryVatRatio(dealerModel.Country).Model;

                        SparePartCountryVatRatioBL spcvrBo = new SparePartCountryVatRatioBL();
                        SparePartCountryVatRatioViewModel spcvrModel = new SparePartCountryVatRatioViewModel();
                        spcvrModel.IdCountry = dealerModel.Country;
                        spcvrModel.IdPart = viewModel.PartId;
                        spcvrBo.GetSparePartCountryVatRatio(UserManager.UserInfo, spcvrModel);

                        if (spcvrModel.VatRatio == null)
                        {
                            spcvrModel.CommandType = CommonValues.DMLType.Insert;
                        }
                        else
                        {
                            if (viewModel.VatRatio == countryVatRatio.GetValue<string>())
                                spcvrModel.CommandType = CommonValues.DMLType.Delete;
                            else
                                spcvrModel.CommandType = CommonValues.DMLType.Update;
                        }
                        spcvrModel.IdCountry = dealerModel.Country;
                        spcvrModel.IdPart = viewModel.PartId;
                        spcvrModel.IsActive = true;
                        spcvrModel.VatRatio = viewModel.VatRatio.GetValue<decimal>();
                        spcvrBo.DMLSparePartCountryVatRatio(UserManager.UserInfo, spcvrModel);
                    }
                }
            }

            return View(viewModel);
        }

        #endregion

        #region SparePart Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePart.SparePartIndex, CommonValues.PermissionCodes.SparePart.SparePartDelete)]
        public ActionResult DeleteSparePart(int partId)
        {
            SparePartIndexViewModel viewModel = new SparePartIndexViewModel() { PartId = partId };
            var sparePartBo = new SparePartBL();
            viewModel.CommandType = viewModel.PartId > 0 ? CommonValues.DMLType.Delete : string.Empty;
            sparePartBo.DMLSparePart(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }
        #endregion

        #region SparePart Details
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePart.SparePartIndex, CommonValues.PermissionCodes.SparePart.SparePartDetails)]
        [HttpGet]
        public ActionResult SparePartDetails(int id = 0)
        {
            var v = new SparePartIndexViewModel();
            var sparePartBo = new SparePartBL();

            v.PartId = id;
            sparePartBo.GetSparePart(UserManager.UserInfo, v);

            //if (v.GuaranteeAuthorityNeed==false)
            //{
            //    v.GuaranteeAuthorityNeedName
            //}

            if (v.OriginalPartId != 0)
            {
                SparePartIndexViewModel orgMo = new SparePartIndexViewModel();
                orgMo.PartId = v.OriginalPartId;
                sparePartBo.GetSparePart(UserManager.UserInfo, orgMo);
                v.OriginalPartName = orgMo.PartCode + CommonValues.Slash + v.OriginalPartName;
            }

            DealerBL dealerBo = new DealerBL();
            DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;

            SparePartCountryVatRatioViewModel spcvrModel = new SparePartCountryVatRatioViewModel();
            spcvrModel.IdCountry = dealerModel.Country;
            spcvrModel.IdPart = id;
            SparePartCountryVatRatioBL spcvrBo = new SparePartCountryVatRatioBL();
            spcvrBo.GetSparePartCountryVatRatio(UserManager.UserInfo, spcvrModel);
            v.VatRatio = spcvrModel.VatRatio.GetValue<string>();

            return View(v);
        }

        #endregion

        #region Supply Discount Ratio

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePart.SparePartIndex)]
        public ActionResult GetSupplyDisCountRatios(int id)
        {
            ViewBag.PartId = id;
            return View("_SupplyDisCountRatios");
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePart.SparePartIndex)]
        public ActionResult ListSupplyDisCountRatios([DataSourceRequest] DataSourceRequest request, int partId)
        {
            var sparePartBo = new SparePartBL();
            var v = new SparePartSupplyDiscountRatioListModel(request);
            var totalCnt = 0;
            v.PartId = partId;
            var returnValue = sparePartBo.ListSparePartsSupplyDiscountRatios(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Helpers

        private void SetDefaults()
        {
            ViewBag.GuaranteeAuthorityNeedList = CommonBL.ListGuaranteeAuthorityNeedAsSelectListItem().Data;
            ViewBag.StatusList = CommonBL.ListStatus().Data;
            ViewBag.DealersList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.VatRatioList = CommonBL.ListVatRatio().Data;
            ViewBag.UnitsList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.Unit).Data;
            ViewBag.YesNoList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.YesNo).Data;
            ViewBag.YesNoBooleanList = CommonBL.ListYesNo().Data;
            ViewBag.SparePartTypeList = SparePartTypeBL.ListSparePartTypeAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
        }
        public JsonResult GetSparePart(int partId, int dealerId, int processId)
        {
            var model = new SparePartIndexViewModel { PartId = partId };
            var spBo = new SparePartBL();
            spBo.GetSparePart(UserManager.UserInfo, model);
            string partCode = model.PartCode;

            DealerBL dealerBo = new DealerBL();
            DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;
            string ssid = (dealerModel.SSID != null) ? dealerModel.SSID.Substring(dealerModel.SSID.Length - 5, 5) : null;/*BURASININ 10 KARAKTERDEN FAZLA OLDUĞUNU OSMAN TEYID ETTI*/

            int count = 0;
            var listModel = new SparePartListModel { OriginalPartId = partId, IsOriginal = 0 };
            if (UserManager.UserInfo.GetUserDealerId() > 0)
            {
                listModel.DealerId = UserManager.UserInfo.GetUserDealerId();
            }
            if (model.OriginalPartId == 0 && processId == 0)
            {
                spBo.ListSpareParts(UserManager.UserInfo,listModel, out count);
                string newCodeNumber = (count + 1).ToString().PadLeft(2, '0');
                partCode = partCode + "_TR" + newCodeNumber + "_" + ssid;
            }
            return string.IsNullOrEmpty(partCode) ? Json(new { SubPartCode = MessageResource.Error_SparePart_Undefined, PartName = MessageResource.Error_SparePart_Undefined }) :
                Json(new
                {
                    SubPartCode = partCode,
                    PartName = model.MultiLanguageContentAsText,
                    Barcode = model.Barcode,
                    model.GuaranteeAuthorityNeedName,
                    model.GuaranteeAuthorityNeed,
                    CompatibleGuaranteeUsage = model.CompatibleGuaranteeUsage != 0,
                    CompatibleGuaranteeUsageName = model.CompatibleGuaranteeUsage != 0 ? MessageResource.Global_Display_Yes : MessageResource.Global_Display_No
                });
        }

        #endregion
    }
}
