using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.SparePartSAPUnit;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class SparePartSAPUnitController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.UnitsList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.Unit).Data;
        }

        private readonly SparePartSAPUnitBL _service = new SparePartSAPUnitBL();

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSAPUnit.SparePartSAPUnitIndex)]
        public ActionResult SparePartSAPUnitIndex()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSAPUnit.SparePartSAPUnitSelect)]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request, SparePartSAPUnitListModel model)
        {
            int totalCnt;

            var referenceModel = new SparePartSAPUnitListModel(request) { IsActive = model.IsActive, SparePartCode = model.SparePartCode };

            var list = _service.List(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = list,
                Total = totalCnt
            });
        }

        #region Create

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSAPUnit.SparePartSAPUnitCreate)]
        public ActionResult SparePartSAPUnitCreate()
        {
            SetDefaults();
            var model = new SparePartSAPUnitViewModel { IsActive = true };
            return PartialView(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSAPUnit.SparePartSAPUnitCreate)]
        public ActionResult SparePartSAPUnitCreate(SparePartSAPUnitViewModel model)
        {
            SetDefaults();
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                _service.Insert(UserManager.UserInfo, model);
                
                CheckErrorForMessage(model, true);
            }

            return PartialView(model);
        }

        #endregion

        #region Update

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSAPUnit.SparePartSAPUnitUpdate)]
        public ActionResult SparePartSAPUnitUpdate(int id)
        {
            SetDefaults();
            return PartialView(_service.Get(UserManager.UserInfo, new SparePartSAPUnitViewModel() { PartId = id }).Model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSAPUnit.SparePartSAPUnitUpdate)]
        public ActionResult SparePartSAPUnitUpdate(SparePartSAPUnitViewModel model)
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

        #region Detail

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSAPUnit.SparePartSAPUnitDetail)]
        public ActionResult SparePartSAPUnitDetail(int id)
        {
            return PartialView(_service.Get(UserManager.UserInfo, new SparePartSAPUnitViewModel() { PartId = id }).Model);
        }

        #endregion

        #region Delete

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartSAPUnit.SparePartSAPUnitDelete)]
        public ActionResult SparePartSAPUnitDelete(int id)
        {
            var model = new SparePartSAPUnitViewModel
            {
                PartId = id,
                CommandType = CommonValues.DMLType.Delete
            };

            _service.Delete(UserManager.UserInfo, model);

            ModelState.Clear();

            return model.ErrorNo == 0 ?
                GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success) :
                GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        #endregion
    }
}
