using System.Globalization;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Equipment;
using System.Web.Mvc;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class EquipmentTypeController : ControllerBase
    {
        #region Fields

        private readonly EquipmentTypeBL _equipmentTypeService = new EquipmentTypeBL();

        #endregion

        #region Methods

        #region Index

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Equipment.EquipmentIndex)]
        public ActionResult EquipmentTypeIndex()
        {
            return View();
        }

        #endregion

        #region Create

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Equipment.EquipmentCreate)]
        public ActionResult EquipmentTypeCreate()
        {
            return View();
        }

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Equipment.EquipmentCreate)]
        public ActionResult EquipmentTypeCreate(EquipmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = ODMSCommon.CommonValues.DMLType.Insert;
                _equipmentTypeService.Insert(UserManager.UserInfo, model);

                foreach (var item in ModelState.Keys)
                    ModelState[item].Value = new ValueProviderResult(string.Empty, string.Empty, CultureInfo.CurrentCulture);

                model.MultiLanguageContentAsText = string.Empty;

                CheckErrorForMessage(model, true);
            }
            return View(model);
        }

        #endregion

        #region Update

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Equipment.EquipmentUpdate)]
        public ActionResult EquipmentTypeUpdate(string equipmentId)
        {
            var model = new EquipmentViewModel { EquipmentId = equipmentId.GetValue<int>() };

            return View(_equipmentTypeService.Get(UserManager.UserInfo, model).Model);
        }

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Equipment.EquipmentUpdate)]
        public ActionResult EquipmentTypeUpdate(EquipmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = ODMSCommon.CommonValues.DMLType.Update;
                _equipmentTypeService.Update(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
            }
            return View(model);
        }

        #endregion

        #region Display

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Equipment.EquipmentIndex)]
        public ActionResult EquipmentTypeDisplay(string equipmentId)
        {
            var model = new EquipmentViewModel { EquipmentId = equipmentId.GetValue<int>() };

            return View(_equipmentTypeService.Get(UserManager.UserInfo, model).Model);
        }

        #endregion

        #region Delete

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Equipment.EquipmentDelete)]
        public ActionResult EquipmentTypeDelete(string equipmentId)
        {
            var model = new EquipmentViewModel { EquipmentId = equipmentId.GetValue<int>(), CommandType = CommonValues.DMLType.Delete };
            _equipmentTypeService.Delete(UserManager.UserInfo, model);

            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_FKReference);
        }

        #endregion

        #region Select

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Equipment.EquipmentSelect)]
        public ActionResult GetData([DataSourceRequest]DataSourceRequest request, EquipmentTypeListModel model)
        {
            var referenceModel = new EquipmentTypeListModel(request);
            int totalCnt;

            var list = _equipmentTypeService.List(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
                {
                    Data = list,
                    Total = totalCnt
                });
        }

        #endregion

        #endregion
    }
}
