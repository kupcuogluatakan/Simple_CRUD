using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSModel.GuaranteeCompLabourMargin;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class GuaranteeCompLabourMarginController : ControllerBase
    {
        #region GuaranteeCompLabourMargin Index

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeCompLabourMargin.GuaranteeCompLabourMarginIndex)]
        [HttpGet]
        public ActionResult GuaranteeCompLabourMarginIndex()
        {
            FillDropDownListData();

            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeCompLabourMargin.GuaranteeCompLabourMarginIndex)]
        public ActionResult ListGuaranteeCompLabourMargin([DataSourceRequest]DataSourceRequest request, GuaranteeCompLabourMarginListModel model)
        {
            //Sorgula butonuna tıklandığın
            // Gridi dolduran metod budur. Model içindeki search kriterlerine göre arama yaparak gride datasource sağlar.

            var guaranteeCompLabourMarginBo = new GuaranteeCompLabourMarginBL();
            var v = new GuaranteeCompLabourMarginListModel(request) { CountryId = model.CountryId };

            var totalCnt = 0;
            var returnValue = guaranteeCompLabourMarginBo.ListGuaranteeCompLabourMargin(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region GuaranteeCompLabourMargin Create

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeCompLabourMargin.GuaranteeCompLabourMarginIndex, CommonValues.PermissionCodes.GuaranteeCompLabourMargin.GuaranteeCompLabourMarginCreate)]
        public ActionResult GuaranteeCompLabourMarginCreate()
        {
            FillDropDownListData();
            return View();
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeCompLabourMargin.GuaranteeCompLabourMarginIndex, CommonValues.PermissionCodes.GuaranteeCompLabourMargin.GuaranteeCompLabourMarginCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult GuaranteeCompLabourMarginCreate(GuaranteeCompLabourMarginViewModel model)
        {
            FillDropDownListData();

            ModelState.Remove("GrntPrice");
            ModelState.Remove("GrntRatio");

            if (!ModelState.IsValid)
                return View(model);

            bool isValid;
            var guaranteeCompLabourMarginBo = ValidateModelGuaranteeCompLabourMarginViewModel(model, out isValid);
            ModelState.Clear();
            if (isValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                guaranteeCompLabourMarginBo.DMLGuaranteeCompLabourMargin(UserManager.UserInfo, model);

                if (CheckErrorForMessage(model, true))
                    return View(model);

                return View();
            }
            return View(model);
        }

        private GuaranteeCompLabourMarginBL ValidateModelGuaranteeCompLabourMarginViewModel(GuaranteeCompLabourMarginViewModel model, out bool isValid)
        {
            var guaranteeCompLabourMarginBo = new GuaranteeCompLabourMarginBL();
            isValid = true;
            if (model.MaxPrice == null)
            {
                int totalCount = 0;
                GuaranteeCompLabourMarginListModel listModel = new GuaranteeCompLabourMarginListModel
                {
                    CountryId = model.CountryId.GetValue<int>()
                };

                List<GuaranteeCompLabourMarginListModel> list = guaranteeCompLabourMarginBo.ListGuaranteeCompLabourMargin(UserManager.UserInfo, listModel, out totalCount).Data;
                if (totalCount > 0)
                {
                    var control = (from r in list.AsEnumerable()
                                   where r.MaxPrice == null
                                   select r);
                    if (control.Any())
                    {
                        SetMessage(MessageResource.GuaranteeCompLabourMargin_Error_CountryIdMaxPriceNullDuplicate, CommonValues.MessageSeverity.Fail);
                        isValid = false;
                    }
                    var controlduplicate = (from r in list.AsEnumerable()
                                            where r.MaxPrice == model.MaxPrice
                                            select r);
                    if (controlduplicate.Any())
                    {
                        SetMessage(MessageResource.GuaranteeCompLabourMargin_Error_CountryIdMaxPriceDuplicate, CommonValues.MessageSeverity.Fail);
                        isValid = false;
                    }
                }
            }
            if ((model.GrntPrice == null && model.GrntRatio == null) || (model.GrntPrice == 0 && model.GrntRatio == 0))
            {
                SetMessage(MessageResource.GuaranteeCompLabourMargin_Error_GrntRatioOrGrntPrice, CommonValues.MessageSeverity.Fail);
                isValid = false;
            }
            if ((model.GrntPrice != null && model.GrntPrice != 0) && (model.GrntRatio != null && model.GrntRatio != 0))
            {
                SetMessage(MessageResource.GuaranteeCompLabourMargin_Error_GrntRatioAndGrntPrice, CommonValues.MessageSeverity.Fail);
                isValid = false;
            }
            if (model.GrntRatio < 100)
            {
                SetMessage(MessageResource.GuaranteeCompLabourMargin_Error_GrntRatioGreaterThan, CommonValues.MessageSeverity.Fail);
                isValid = false;
            }
            return guaranteeCompLabourMarginBo;
        }

        #endregion

        #region GuaranteeCompLabourMargin Update

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeCompLabourMargin.GuaranteeCompLabourMarginIndex, CommonValues.PermissionCodes.GuaranteeCompLabourMargin.GuaranteeCompLabourMarginUpdate)]
        public ActionResult GuaranteeCompLabourMarginUpdate(int? id)
        {
            if (!id.HasValue)
                return View();

            FillDropDownListData();

            return View(GetGuaranteeCompLabourMarginById(id.Value));
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeCompLabourMargin.GuaranteeCompLabourMarginIndex, CommonValues.PermissionCodes.GuaranteeCompLabourMargin.GuaranteeCompLabourMarginUpdate)]
        [ValidateAntiForgeryToken]
        public ActionResult GuaranteeCompLabourMarginUpdate(GuaranteeCompLabourMarginViewModel model)
        {
            FillDropDownListData();

            ModelState.Remove("GrntPrice");
            ModelState.Remove("GrntRatio");

            if (!ModelState.IsValid)
                return View(model);

            ModelState.Clear();
            bool isValid;
            var guaranteeCompLabourMarginBo = ValidateModelGuaranteeCompLabourMarginViewModel(model, out isValid);

            if (isValid)
            {
                model.CommandType = CommonValues.DMLType.Update;
                guaranteeCompLabourMarginBo.DMLGuaranteeCompLabourMargin(UserManager.UserInfo, model);

                CheckErrorForMessage(model, true);
            }
            return View(model);
        }

        #endregion

        #region GuaranteeCompLabourMargin Delete

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.GuaranteeCompLabourMargin.GuaranteeCompLabourMarginIndex, ODMSCommon.CommonValues.PermissionCodes.GuaranteeCompLabourMargin.GuaranteeCompLabourMarginDelete)]
        [ValidateAntiForgeryToken]
        public ActionResult GuaranteeCompLabourMarginDelete(GuaranteeCompLabourMarginViewModel model)
        {
            ViewBag.HideElements = false;

            var bo = new GuaranteeCompLabourMarginBL();
            model.CommandType = model.IdGrntLabourMrgn > 0 ? ODMSCommon.CommonValues.DMLType.Delete : string.Empty;
            bo.DMLGuaranteeCompLabourMargin(UserManager.UserInfo, model);

            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);

            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        #endregion

        private void FillDropDownListData()
        {
            ViewBag.CountryList = CommonBL.ListCountries(UserManager.UserInfo).Data;
        }

        private GuaranteeCompLabourMarginViewModel GetGuaranteeCompLabourMarginById(int id)
        {
            var guaranteeCompLabourMarginBo = new GuaranteeCompLabourMarginBL();
            var viewModel = guaranteeCompLabourMarginBo.GetGuaranteeCompLabourMargin(UserManager.UserInfo, id).Model;
            CheckErrorForMessage(viewModel, false);
            return viewModel;
        }

    }
}
