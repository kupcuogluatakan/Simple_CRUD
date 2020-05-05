using System.Globalization;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.PurchaseOrderMatch;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    public class PurchaseOrderMatchController : ControllerBase
    {
        private readonly PurchaseOrderMatchBL _service = new PurchaseOrderMatchBL();
        private readonly PurchaseOrderTypeBL _purchaseOrderTypeService = new PurchaseOrderTypeBL();
        private readonly PurchaseOrderGroupBL _purchaseOrderGroupService = new PurchaseOrderGroupBL();

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderMatch.PurchaseOrderMatchIndex)]
        public ActionResult PurchaseOrderMatchIndex()
        {
            SetDefaults();

            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderMatch.PurchaseOrderMatchSelect)]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request, PurchaseOrderMatchListModel model)
        {
            int totalCnt;

            var referenceModel = new PurchaseOrderMatchListModel(request)
            {
                IsActive = model.IsActive,
                PurhcaseOrderGroupId = model.PurhcaseOrderGroupId,
                PurhcaseOrderTypeId = model.PurhcaseOrderTypeId
            };

            var list = _service.List(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = list,
                Total = totalCnt
            });
        }

        #region Create
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderMatch.PurchaseOrderMatchCreate)]
        public ActionResult PurchaseOrderMatchCreate()
        {
            SetDefaults();
            var model = new PurchaseOrderMatchViewModel { IsActive = true };
            return PartialView(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderMatch.PurchaseOrderMatchCreate)]
        public ActionResult PurchaseOrderMatchCreate(PurchaseOrderMatchViewModel model)
        {
            SetDefaults();

            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                _service.Insert(UserManager.UserInfo, model);

                if (model.ErrorNo == 0)
                {
                    foreach (var item in ModelState.Keys)
                        ModelState[item].Value = new ValueProviderResult(string.Empty, string.Empty, CultureInfo.CurrentCulture);
                }

                CheckErrorForMessage(model, true);
            }


            return PartialView(model);
        }
        #endregion

        #region Update
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderMatch.PurchaseOrderMatchUpdate)]
        public ActionResult PurchaseOrderMatchUpdate(int groupId, int typeId)
        {
            SetDefaults();

            return PartialView(_service.Get(UserManager.UserInfo, new PurchaseOrderMatchViewModel()
            {
                PurhcaseOrderGroupId = groupId,
                PurhcaseOrderTypeId = typeId
            }).Model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderMatch.PurchaseOrderMatchUpdate)]
        public ActionResult PurchaseOrderMatchUpdate(PurchaseOrderMatchViewModel model)
        {
            SetDefaults();

            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Update;
                _service.Update(UserManager.UserInfo, model);

                CheckErrorForMessage(model, true);
            }

            return PartialView(model);
        }

        #endregion

        #region Delete

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderMatch.PurchaseOrderMatchDelete)]
        public ActionResult PurchaseOrderMatchDelete(int groupId, int typeId)
        {
            var model = new PurchaseOrderMatchViewModel
            {
                PurhcaseOrderGroupId = groupId,
                PurhcaseOrderTypeId = typeId,
                CommandType = CommonValues.DMLType.Delete
            };

            _service.Delete(UserManager.UserInfo, model);

            ModelState.Clear();

            return model.ErrorNo == 0 ?
                GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success) :
                GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        #endregion

        #region Detail

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderMatch.PurchaseOrderMatchDetail)]
        public ActionResult PurchaseOrderMatchDetail(int groupId, int typeId)
        {
            return PartialView(_service.Get(UserManager.UserInfo, new PurchaseOrderMatchViewModel()
            {
                PurhcaseOrderGroupId = groupId,
                PurhcaseOrderTypeId = typeId
            }).Model);
        }

        #endregion

        public void SetDefaults()
        {
            ViewBag.PurchaseOrderTypeList = _purchaseOrderTypeService.PurchaseOrderTypeList(UserManager.UserInfo).Data;
            ViewBag.PurchaseOrderGroupList = _purchaseOrderGroupService.PurchaseOrderGroupList().Data;
        }
    }
}
