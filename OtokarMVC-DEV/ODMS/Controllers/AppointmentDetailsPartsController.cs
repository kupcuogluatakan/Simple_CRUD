using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.AppointmentDetailsParts;
using ODMSModel.Dealer;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class AppointmentDetailsPartsController : ControllerBase
    {
        //Index
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsIndex)]
        [HttpGet]
        public ActionResult AppointmentDetailsPartsIndex(int? id)
        {
            AppointmentDetailsPartsViewModel model_AppDPModel = new AppointmentDetailsPartsViewModel();
            var bl = new AppointmentDetailsPartsBL();

            if (id > 0)
            {
                model_AppDPModel.AppointIndicId = (int)id;
                bl.GetAppIndicType(UserManager.UserInfo, model_AppDPModel);
            }
            return PartialView(model_AppDPModel);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsIndex, ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsDetails)]
        public JsonResult ListAppointmentDetailsParts([DataSourceRequest] DataSourceRequest request, AppointmentDetailsPartsListModel appDPListModel)
        {
            AppointmentDetailsPartsBL appDPBL = new AppointmentDetailsPartsBL();
            AppointmentDetailsPartsListModel list_AppDPModel = new AppointmentDetailsPartsListModel(request);
            int totalCount = 0;

            list_AppDPModel.AppointIndicId = appDPListModel.AppointIndicId;

            var rValue = appDPBL.GetAppointmentDPList(UserManager.UserInfo, list_AppDPModel, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }


        //Details
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsIndex, ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsDetails)]
        [HttpGet]
        public ActionResult AppointmentDetailsPartsDetails(int id = 0)
        {
            AppointmentDetailsPartsViewModel model_AppDPModel = new AppointmentDetailsPartsViewModel();
            AppointmentDetailsPartsBL appDPBL = new AppointmentDetailsPartsBL();

            model_AppDPModel.Id = id;
            appDPBL.GetAppointmentDP(UserManager.UserInfo, model_AppDPModel);

            return View(model_AppDPModel);
        }


        //Create
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsIndex, ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsCreate)]
        [HttpGet]
        public ActionResult AppointmentDetailsPartsCreate(int AppointIndicId)
        {
            AppointmentDetailsPartsViewModel model_AppDPModel = new AppointmentDetailsPartsViewModel();
            var bl = new AppointmentDetailsBL();

            model_AppDPModel.AppointIndicId = AppointIndicId;
            var model = bl.GetAppointmentDetails(UserManager.UserInfo, AppointIndicId).Model;
            model_AppDPModel.GroupList = model.MainCategoryName + "   /   " + model.CategoryName + "   /   " +
                                         model.SubCategoryName;

            int dealerId = UserManager.UserInfo.GetUserDealerId();
            DealerBL dealerBo = new DealerBL();
            DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;

            model_AppDPModel.CurrencyCode = dealerModel.CurrencyCode;

            return View(model_AppDPModel);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsIndex, ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsCreate)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AppointmentDetailsPartsCreate(AppointmentDetailsPartsViewModel appDPModel)
        {
            AppointmentDetailsPartsBL appDPBL = new AppointmentDetailsPartsBL();
            if (ModelState.IsValid)
            {
                appDPModel.CommandType = ODMSCommon.CommonValues.DMLType.Insert;
                appDPBL.DMLAppointmentDetailsParts(UserManager.UserInfo, appDPModel);

                CheckErrorForMessage(appDPModel, true);

                ModelState.Clear();


                int dealerId = UserManager.UserInfo.GetUserDealerId();
                DealerBL dealerBo = new DealerBL();
                DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;

                return View(new AppointmentDetailsPartsViewModel()
                {
                    AppointIndicId = appDPModel.AppointIndicId,
                    GroupList = appDPModel.GroupList,
                    CurrencyCode = dealerModel.CurrencyCode
                });
            }
            return View(appDPModel);
        }


        //Update
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsIndex, ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsUpdate)]
        [HttpGet]
        public ActionResult AppointmentDetailsPartsUpdate(int id)
        {
            AppointmentDetailsPartsViewModel model_AppDPModel = new AppointmentDetailsPartsViewModel();
            AppointmentDetailsPartsBL appDPBL = new AppointmentDetailsPartsBL();

            model_AppDPModel.Id = id;
            appDPBL.GetAppointmentDP(UserManager.UserInfo, model_AppDPModel);

            int dealerId = UserManager.UserInfo.GetUserDealerId();
            DealerBL dealerBo = new DealerBL();
            DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;

            model_AppDPModel.CurrencyCode = dealerModel.CurrencyCode;

            return View(model_AppDPModel);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsIndex, ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsUpdate)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AppointmentDetailsPartsUpdate(AppointmentDetailsPartsViewModel appDPModel)
        {
            AppointmentDetailsPartsBL appDPBL = new AppointmentDetailsPartsBL();
            if (ModelState.IsValid)
            {
                appDPModel.CommandType = ODMSCommon.CommonValues.DMLType.Update;
                appDPBL.DMLAppointmentDetailsParts(UserManager.UserInfo, appDPModel);
                CheckErrorForMessage(appDPModel, true);

                return View(appDPModel);
            }
            return View(appDPModel);
        }


        //Delete
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsIndex, ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsDelete)]
        [HttpGet]
        public ActionResult AppointmentDetailsPartsDelete(int id = 0)
        {
            AppointmentDetailsPartsViewModel model_AppDPModel = new AppointmentDetailsPartsViewModel();
            AppointmentDetailsPartsBL appDPBL = new AppointmentDetailsPartsBL();

            model_AppDPModel.Id = id;
            appDPBL.GetAppointmentDP(UserManager.UserInfo, model_AppDPModel);

            return View(model_AppDPModel);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsIndex, ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsParts.AppointmentDetailsPartsDelete)]
        [HttpPost]
        public ActionResult AppointmentDetailsPartsDelete(AppointmentDetailsPartsViewModel appDPModel)
        {
            AppointmentDetailsPartsBL appDPBL = new AppointmentDetailsPartsBL();
            appDPModel.CommandType = ODMSCommon.CommonValues.DMLType.Delete;

            appDPBL.DMLAppointmentDetailsParts(UserManager.UserInfo, appDPModel);

            var result = CheckErrorForMessage(appDPModel, true);
            if (!result)
            {
                appDPModel.HideFormElements = true;
            }

            if (appDPModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, appDPModel.ErrorMessage);
        }
    }
}
