using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Dealer;
using ODMSModel.PurchaseOrderGroup;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PurchaseOrderGroupController : ControllerBase
    {

        private readonly PurchaseOrderGroupBL _service = new PurchaseOrderGroupBL();

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderGroup.PurchaseOrderGroupIndex)]
        public ActionResult PurchaseOrderGroupIndex()
        {
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderGroup.PurchaseOrderGroupSelect)]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request, PurchaseOrderGroupListModel model)
        {
            int totalCnt;

            var referenceModel = new PurchaseOrderGroupListModel(request) { IsActive = model.IsActive };

            var list = _service.List(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = list,
                Total = totalCnt
            });
        }

        #region Create
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderGroup.PurchaseOrderGroupCreate)]
        public ActionResult PurchaseOrderGroupCreate()
        {
            var model = new PurchaseOrderGroupViewModel { IsActive = true };
            return PartialView(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderGroup.PurchaseOrderGroupCreate)]
        public ActionResult PurchaseOrderGroupCreate(PurchaseOrderGroupViewModel model)
        {

            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                _service.Insert(UserManager.UserInfo, model);

                if (model.ErrorNo == 0)
                {
                    foreach (var item in ModelState.Keys.Where(item => item != "PurchaseOrderGroupId"))
                        ModelState[item].Value = new ValueProviderResult(string.Empty, string.Empty, CultureInfo.CurrentCulture);
                }

                CheckErrorForMessage(model, true);
            }

            return PartialView(model);
        }
        #endregion

        #region Update
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderGroup.PurchaseOrderGroupUpdate)]
        public ActionResult PurchaseOrderGroupUpdate(int id)
        {
            return PartialView(_service.Get(new PurchaseOrderGroupViewModel() { PurchaseOrderGroupId = id }).Model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderGroup.PurchaseOrderGroupUpdate)]
        public ActionResult PurchaseOrderGroupUpdate(PurchaseOrderGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = ODMSCommon.CommonValues.DMLType.Update;
                _service.Update(UserManager.UserInfo, model);

                CheckErrorForMessage(model, true);
            }

            return PartialView(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderGroup.PurchaseOrderGroupDelete)]
        public ActionResult PurchaseOrderGroupDelete(int id)
        {
            /*TFs No : 27627 OYA 17.12.2014 
             Grup pasife çekiliyorken dealer tablosu kontrol edilecek. eğer bu grupta olan bir servis var ise pasif e çekilmesi engellenecek. 
             Uyarı olarak " Bu gruba tanımlı servisler var. Pasife çekilemez" denilecek. */
            int totalCount = 0;
            DealerBL dBo = new DealerBL();
            DealerListModel dModel = new DealerListModel();
            dModel.PoGroupId = id;
            dBo.ListDealers(UserManager.UserInfo, dModel, out totalCount);
            if (totalCount > 0)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.PurchaseOrderGroup_Warning_DealerExists);
            }
            else
            {
                var model = new PurchaseOrderGroupViewModel
                {
                    PurchaseOrderGroupId = id,
                    CommandType = CommonValues.DMLType.Delete
                };

                _service.Delete(UserManager.UserInfo, model);

                ModelState.Clear();

                return model.ErrorNo == 0
                           ? GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                            MessageResource.Global_Display_Success)
                           : GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
            }
        }
        #endregion

        #region Detail
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderGroup.PurchaseOrderGroupDetail)]
        public ActionResult PurchaseOrderGroupDetail(int id)
        {
            return PartialView(_service.Get(new PurchaseOrderGroupViewModel() { PurchaseOrderGroupId = id }).Model);
        }
        #endregion
    }
}
