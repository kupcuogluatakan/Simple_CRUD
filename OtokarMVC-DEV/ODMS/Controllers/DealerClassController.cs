using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.DealerClass;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DealerClassController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.StatusList = CommonBL.ListStatusBool().Data;
        }

        #region DealerClass Index

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerClass.DealerClassIndex)]
        [HttpGet]
        public ActionResult DealerClassIndex()
        {
            SetDefaults();
            DealerClassListModel model = new DealerClassListModel();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerClass.DealerClassIndex, CommonValues.PermissionCodes.DealerClass.DealerClassIndex)]
        public ActionResult ListDealerClass([DataSourceRequest] DataSourceRequest request, DealerClassListModel model)
        {
            var dealerClassBo = new DealerClassBL();

            var v = new DealerClassListModel(request)
            {
                DealerClassCode = model.DealerClassCode,
                SSIdDealerClass = model.SSIdDealerClass,
                DealerClassName = model.DealerClassName,
                IsActive = model.IsActive
            };

            var totalCnt = 0;
            var returnValue = dealerClassBo.ListDealerClass(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region DealerClass Create

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerClass.DealerClassIndex, CommonValues.PermissionCodes.DealerClass.DealerClassCreate)]
        public ActionResult DealerClassCreate()
        {
            SetDefaults();

            var model = new DealerClassViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerClass.DealerClassIndex, CommonValues.PermissionCodes.DealerClass.DealerClassCreate)]
        [HttpPost]
        public ActionResult DealerClassCreate(DealerClassViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var dealerClassBo = new DealerClassBL();

            int totalCount = 0;
            DealerClassListModel controlModel = new DealerClassListModel();
            controlModel.SSIdDealerClass = viewModel.SSIdDealerClass;
            List<DealerClassListModel> list = dealerClassBo.ListDealerClass(UserManager.UserInfo, controlModel, out totalCount).Data;

            controlModel = new DealerClassListModel();
            controlModel.DealerClassCode = viewModel.DealerClassCode;
            List<DealerClassListModel> classList = dealerClassBo.ListDealerClass(UserManager.UserInfo, controlModel, out totalCount).Data;

            SetDefaults();

            if (!list.Any() && !classList.Any())
            {
                if (ModelState.IsValid)
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;

                    viewModel.DealerClassCode = viewModel.DealerClassCode.ToUpper();
                    viewModel.SSIdDealerClass = viewModel.SSIdDealerClass.ToUpper();
                    dealerClassBo.DMLDealerClass(UserManager.UserInfo, viewModel);

                    CheckErrorForMessage(viewModel, true);
                    ModelState.Clear();
                }
            }
            else
            {
                SetMessage(MessageResource.CustomerDiscount_Error_DataHave, CommonValues.MessageSeverity.Fail);
                return View(viewModel);
            }

            return View();
        }

        #endregion

        #region DealerClass Update

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerClass.DealerClassIndex, CommonValues.PermissionCodes.DealerClass.DealerClassUpdate)]
        [HttpGet]
        public ActionResult DealerClassUpdate(string id)
        {
            SetDefaults();
            var v = new DealerClassViewModel();
            if (id != null)
            {
                var dealerClassBo = new DealerClassBL();
                v.DealerClassCode = id;

                dealerClassBo.GetDealerClass(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerClass.DealerClassIndex, CommonValues.PermissionCodes.DealerClass.DealerClassUpdate)]
        [HttpPost]
        public ActionResult DealerClassUpdate(DealerClassViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var dealerClassBo = new DealerClassBL();

            if (viewModel.IsActive == false)//pasif yapılıyor ise
            {
                var control = CheckDealerClassCode(viewModel.DealerClassCode);

                if (control.Any()) //IsActive pasif yapılma koşulu
                {
                    SetDefaults();

                    SetMessage(MessageResource.DealerClass_Error_ServiceIsHave, CommonValues.MessageSeverity.Fail);

                    return View(viewModel);
                }
            }

            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;

                int totalCount = 0;
                DealerClassListModel controlModel = new DealerClassListModel();
                controlModel.SSIdDealerClass = viewModel.SSIdDealerClass;
                List<DealerClassListModel> list = dealerClassBo.ListDealerClass(UserManager.UserInfo, controlModel, out totalCount).Data;
                var control = (from l in list.AsEnumerable()
                               where l.DealerClassCode != viewModel.DealerClassCode
                               select l);
                if (!control.Any())
                {
                    viewModel.SSIdDealerClass = viewModel.SSIdDealerClass.ToUpper();
                    dealerClassBo.DMLDealerClass(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);
                }
                else
                {
                    SetMessage(MessageResource.CustomerDiscount_Error_DataHave, CommonValues.MessageSeverity.Fail);
                }
            }

            SetDefaults();
            return View(viewModel);
        }

        #endregion

        #region DealerClass Delete


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerClass.DealerClassIndex, CommonValues.PermissionCodes.DealerClass.DealerClassDelete)]
        public ActionResult DeleteDealerClass(string DealerClassCode)
        {
            DealerClassViewModel viewModel = new DealerClassViewModel { DealerClassCode = DealerClassCode };

            var dealerClassBo = new DealerClassBL();

            var control = CheckDealerClassCode(DealerClassCode);

            if (!control.Any())
            {
                dealerClassBo.GetDealerClass(UserManager.UserInfo, viewModel);

                viewModel.CommandType = CommonValues.DMLType.Delete;
                dealerClassBo.DMLDealerClass(UserManager.UserInfo, viewModel);

                ModelState.Clear();
                if (viewModel.ErrorNo == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
            }
            else
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                    MessageResource.DealerClass_Error_ServiceIsHave);//"Tanımlı aktif servis bulunmaktadır. Servis sınıfı pasif yapılamaz!");
            }
           
        }

        #endregion

        private static IEnumerable<string> CheckDealerClassCode(string DealerClassCode)
        {
            var dealerClassCode = new DealerBL();

            List<SelectListItem> dealerClassList = dealerClassCode.ListDealerClassCodeAsSelectListItem().Data;

            var control = (from a in dealerClassList
                           where a.Text == DealerClassCode
                           select a.Value);
            return control;
        }
    }
}