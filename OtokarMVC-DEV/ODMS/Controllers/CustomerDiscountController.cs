using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.CustomerDiscount;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CustomerDiscountController : ControllerBase
    {
        #region Member Varables

        private void SetDefaults()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.CustomerList = CustomerBL.ListCustomerAsSelectListItem(UserManager.UserInfo).Data;
        }

        #endregion

        #region CustomerDiscount Index

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CustomerDiscount.CustomerDiscountIndex)]
        [HttpGet]
        public ActionResult CustomerDiscountIndex()
        {
            SetDefaults();
            CustomerDiscountListModel model = new CustomerDiscountListModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();

            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CustomerDiscount.CustomerDiscountIndex, ODMSCommon.CommonValues.PermissionCodes.CustomerDiscount.CustomerDiscountDetails)]
        public ActionResult ListCustomerDiscount([DataSourceRequest] DataSourceRequest request, CustomerDiscountListModel model)
        {
            var customerDiscountBo = new CustomerDiscountBL();

            var v = new CustomerDiscountListModel(request)
            {
                CustomerName = model.CustomerName,
                IdCustomer = model.IdCustomer,
                IdDealer = model.IdDealer,
                PartDiscountRatio = model.PartDiscountRatio,
                LabourDiscountRatio = model.LabourDiscountRatio
            };

            var totalCnt = 0;
            var returnValue = customerDiscountBo.ListCustomerDiscount(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region CustomerDiscount Create

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CustomerDiscount.CustomerDiscountIndex, ODMSCommon.CommonValues.PermissionCodes.CustomerDiscount.CustomerDiscountCreate)]
        public ActionResult CustomerDiscountCreate()
        {
            SetDefaults();

            var model = new CustomerDiscountIndexViewModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CustomerDiscount.CustomerDiscountIndex, ODMSCommon.CommonValues.PermissionCodes.CustomerDiscount.CustomerDiscountCreate)]
        [HttpPost]
        public ActionResult CustomerDiscountCreate(CustomerDiscountIndexViewModel viewModel)
        {
            var customerDiscountBo = new CustomerDiscountBL();

            CustomerDiscountIndexViewModel viewControlModel = new CustomerDiscountIndexViewModel();
            viewModel.IdDealer = viewModel.IdDealer ?? UserManager.UserInfo.GetUserDealerId();
            viewControlModel.IdDealer = viewModel.IdDealer;
            viewControlModel.IdCustomer = viewModel.IdCustomer;
            customerDiscountBo.GetCustomerDiscount(UserManager.UserInfo, viewControlModel);

            SetDefaults();

            if (viewControlModel.PartDiscountRatio == null && viewControlModel.LabourDiscountRatio == null)
            {
                if (ModelState.IsValid)
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;

                    customerDiscountBo.DMLCustomerDiscount(UserManager.UserInfo, viewModel);

                    CheckErrorForMessage(viewModel, true);

                    ModelState.Clear();
                }

                var model = new CustomerDiscountIndexViewModel();
                if (UserManager.UserInfo.GetUserDealerId() != 0)
                    model.IdDealer = UserManager.UserInfo.GetUserDealerId();
                return View(model);
            }
            else
            {
                SetMessage(MessageResource.CustomerDiscount_Error_DataHave, CommonValues.MessageSeverity.Fail);

                return View(viewModel);
            }
        }

        #endregion

        #region CustomerDiscount Update
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CustomerDiscount.CustomerDiscountIndex, ODMSCommon.CommonValues.PermissionCodes.CustomerDiscount.CustomerDiscountUpdate)]
        [HttpGet]
        public ActionResult CustomerDiscountUpdate(Int64? idCustomer, int? idDealer)
        {
            SetDefaults();
            var v = new CustomerDiscountIndexViewModel();
            if (idCustomer != 0)
            {
                var customerDiscountBo = new CustomerDiscountBL();
                v.IdCustomer = idCustomer;
                v.IdDealer = idDealer;
                customerDiscountBo.GetCustomerDiscount(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CustomerDiscount.CustomerDiscountIndex, ODMSCommon.CommonValues.PermissionCodes.CustomerDiscount.CustomerDiscountUpdate)]
        [HttpPost]
        public ActionResult CustomerDiscountUpdate(CustomerDiscountIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var customerDiscountBo = new CustomerDiscountBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;

                customerDiscountBo.DMLCustomerDiscount(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }

            SetDefaults();
            return View(viewModel);
        }

        #endregion

        #region CustomerDiscount Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CustomerDiscount.CustomerDiscountIndex, ODMSCommon.CommonValues.PermissionCodes.CustomerDiscount.CustomerDiscountDelete)]
        public ActionResult DeleteCustomerDiscount(int idCustomer, int idDealer)
        {
            CustomerDiscountIndexViewModel viewModel = new CustomerDiscountIndexViewModel
            {
                IdCustomer = idCustomer,
                IdDealer = idDealer
            };

            var customerDiscountBo = new CustomerDiscountBL();
            customerDiscountBo.GetCustomerDiscount(UserManager.UserInfo, viewModel);

            viewModel.CommandType = CommonValues.DMLType.Delete;

            customerDiscountBo.DMLCustomerDiscount(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region CustomerDiscount Details
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CustomerDiscount.CustomerDiscountIndex, ODMSCommon.CommonValues.PermissionCodes.CustomerDiscount.CustomerDiscountDetails)]
        [HttpGet]
        public ActionResult CustomerDiscountDetails(int idCustomer, int? idDealer)
        {
            var v = new CustomerDiscountIndexViewModel();
            var customerDiscountBo = new CustomerDiscountBL();

            v.IdCustomer = idCustomer;
            v.IdDealer = idDealer;
            SetDefaults();
            customerDiscountBo.GetCustomerDiscount(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
    }
}