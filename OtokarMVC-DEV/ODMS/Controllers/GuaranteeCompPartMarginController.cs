using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSModel.GuaranteeCompPartMargin;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class GuaranteeCompPartMarginController : ControllerBase
    {
        #region GuaranteeCompPartMargin Index

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeCompPartMargin.GuaranteeCompPartMarginIndex)]
        [HttpGet]
        public ActionResult GuaranteeCompPartMarginIndex()
        {
            FillDropDownListData();

            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeCompPartMargin.GuaranteeCompPartMarginIndex)]
        public ActionResult ListGuaranteeCompPartMargin([DataSourceRequest]DataSourceRequest request, GuaranteeCompPartMarginListModel model)
        {
            var guaranteeCompPartMarginBo = new GuaranteeCompPartMarginBL();
            var v = new GuaranteeCompPartMarginListModel(request) { CountryId = model.CountryId };

            var totalCnt = 0;
            var returnValue = guaranteeCompPartMarginBo.ListGuaranteeCompPartMargin(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region GuaranteeCompPartMargin Create

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeCompPartMargin.GuaranteeCompPartMarginIndex, CommonValues.PermissionCodes.GuaranteeCompPartMargin.GuaranteeCompPartMarginCreate)]
        public ActionResult GuaranteeCompPartMarginCreate()
        {
            FillDropDownListData();
            return View();
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeCompPartMargin.GuaranteeCompPartMarginIndex,
            CommonValues.PermissionCodes.GuaranteeCompPartMargin.GuaranteeCompPartMarginCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult GuaranteeCompPartMarginCreate(GuaranteeCompPartMarginViewModel model)
        {
            FillDropDownListData();

            var validator = new GuaranteeCompPartMarginViewModelValidator();
            var result = validator.Validate(model);


            if (!result.IsValid)
                return View(model);

            bool isValid;
            var guaranteeCompPartMarginBo = ValidateGuaranteeCompPartMarginModel(model, out isValid);

            if (isValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                guaranteeCompPartMarginBo.DMLGuaranteeCompPartMargin(UserManager.UserInfo, model);

                if (CheckErrorForMessage(model, true))
                    return View(model);

                ModelState.Clear();
                return View();
            }
            else
            {
                return View(model);
            }
        }

        private GuaranteeCompPartMarginBL ValidateGuaranteeCompPartMarginModel(GuaranteeCompPartMarginViewModel model,
                                                                               out bool isValid)
        {
            var guaranteeCompPartMarginBo = new GuaranteeCompPartMarginBL();
            isValid = true;

            if (model.MaxPrice == null)
            {
                int totalCount = 0;
                GuaranteeCompPartMarginListModel listModel = new GuaranteeCompPartMarginListModel
                {
                    CountryId = model.CountryId
                };
                List<GuaranteeCompPartMarginListModel> list = guaranteeCompPartMarginBo.ListGuaranteeCompPartMargin(UserManager.UserInfo, listModel, out totalCount).Data;
                if (totalCount > 0)
                {
                    var control = (from r in list.AsEnumerable()
                                   where r.MaxPrice == null
                                   select r);
                    if (control.Any())
                    {
                        SetMessage(MessageResource.GuaranteeCompLabourMargin_Error_CountryIdMaxPriceNullDuplicate,
                                   CommonValues.MessageSeverity.Fail);
                        isValid = false;
                    }

                    var controlduplicate = (from r in list.AsEnumerable()
                                            where r.MaxPrice == model.MaxPrice
                                            select r);
                    if (controlduplicate.Any())
                    {
                        SetMessage(MessageResource.GuaranteeCompLabourMargin_Error_CountryIdMaxPriceDuplicate,
                                   CommonValues.MessageSeverity.Fail);
                        isValid = false;
                    }
                }
            }
            if (model.GrntPrice == null && model.GrntRatio == null)
            {
                SetMessage(MessageResource.GuaranteeCompLabourMargin_Error_GrntRatioOrGrntPrice, CommonValues.MessageSeverity.Fail);
                isValid = false;
            }
            if (model.GrntPrice != null && model.GrntRatio != null)
            {
                SetMessage(MessageResource.GuaranteeCompLabourMargin_Error_GrntRatioAndGrntPrice, CommonValues.MessageSeverity.Fail);
                isValid = false;
            }
            if (model.GrntRatio < 100)
            {
                SetMessage(MessageResource.GuaranteeCompLabourMargin_Error_GrntRatioGreaterThan, CommonValues.MessageSeverity.Fail);
                isValid = false;
            }
            return guaranteeCompPartMarginBo;
        }

        #endregion

        #region GuaranteeCompPartMargin Update

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeCompPartMargin.GuaranteeCompPartMarginIndex, CommonValues.PermissionCodes.GuaranteeCompPartMargin.GuaranteeCompPartMarginUpdate)]
        public ActionResult GuaranteeCompPartMarginUpdate(int? id)
        {
            if (!id.HasValue)
                return View();

            FillDropDownListData();

            return View(GetGuaranteeCompPartMarginById(id.Value));
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeCompPartMargin.GuaranteeCompPartMarginIndex,
            CommonValues.PermissionCodes.GuaranteeCompPartMargin.GuaranteeCompPartMarginUpdate)]
        [ValidateAntiForgeryToken]
        public ActionResult GuaranteeCompPartMarginUpdate(GuaranteeCompPartMarginViewModel model)
        {
            FillDropDownListData();


            var validator = new GuaranteeCompPartMarginViewModelValidator();
            var result = validator.Validate(model);


            if (!result.IsValid)
                return View(model);

            bool isValid;
            var guaranteeCompPartMarginBo = ValidateGuaranteeCompPartMarginModel(model, out isValid);

            if (isValid)
            {
                model.CommandType = CommonValues.DMLType.Update;
                guaranteeCompPartMarginBo.DMLGuaranteeCompPartMargin(UserManager.UserInfo, model);

                CheckErrorForMessage(model, true);
                ModelState.Clear();
            }
            return View(model);
        }

        #endregion

        #region GuaranteeCompPartMargin Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.GuaranteeCompPartMargin.GuaranteeCompPartMarginIndex, ODMSCommon.CommonValues.PermissionCodes.GuaranteeCompPartMargin.GuaranteeCompPartMarginDelete)]
        public ActionResult GuaranteeCompPartMarginDelete(GuaranteeCompPartMarginViewModel model)
        {
            ViewBag.HideElements = false;

            var bo = new GuaranteeCompPartMarginBL();
            model.CommandType = model.IdGrntPartMrgn > 0 ? ODMSCommon.CommonValues.DMLType.Delete : string.Empty;
            bo.DMLGuaranteeCompPartMargin(UserManager.UserInfo, model);

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

        private GuaranteeCompPartMarginViewModel GetGuaranteeCompPartMarginById(int id)
        {
            var guaranteeCompPartMarginBo = new GuaranteeCompPartMarginBL();
            var viewModel = guaranteeCompPartMarginBo.GetGuaranteeCompPartMargin(UserManager.UserInfo, id).Model;
            CheckErrorForMessage(viewModel, false);
            return viewModel;
        }

    }
}
