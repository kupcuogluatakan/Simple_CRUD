using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Customer;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CustomerController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.CustomerTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.CustomerTypeLookup).Data;
            ViewBag.CompanyTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.CompanyTypeLookup).Data;
            ViewBag.StatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.StatusLookup).Data;
            ViewBag.GovernmentTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.GoverntmentTypeLookup).Data;
            ViewBag.CountryList = CommonBL.ListCountries(UserManager.UserInfo).Data;
            ViewBag.YesNoList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.YesNo).Data;
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
        }

        #region Customer Index

        [AuthorizationFilter(CommonValues.PermissionCodes.Customer.CustomerIndex)]
        [HttpGet]
        public ActionResult CustomerIndex()
        {
            SetDefaults();
            CustomerIndexViewModel model = new CustomerIndexViewModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Customer.CustomerIndex, CommonValues.PermissionCodes.Customer.CustomerDetails)]
        public ActionResult ListCustomer([DataSourceRequest] DataSourceRequest request, CustomerIndexViewModel model)
        {
            var customerBo = new CustomerBL();
            var v = new CustomerListModel(request);
            var totalCnt = 0;
            v.CustomerName = model.CustomerName;
            v.DealerId = model.DealerId;
            v.MobileNo = model.MobileNo;
            v.CountryId = model.CountryId;
            v.CustomerTypeId = model.CustomerTypeId;
            v.WitholdingStatus = model.WitholdingStatus;
            v.TcIdentityNo = model.TcIdentityNo;
            v.TaxNo = model.TaxNo;
            v.PassportNo = model.PassportNo;
            var returnValue = customerBo.ListCustomers(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Customer.CustomerIndex, CommonValues.PermissionCodes.Customer.CustomerDetails)]
        [HttpPost]
        public ActionResult ListWitholding(int? countryId)
        {
            return countryId.GetValueOrDefault(0) > 0
                ? Json(new CustomerBL().GetWitholdingList(countryId ?? 0).Data)
                : Json(null);
        }

        #endregion

        #region Customer Create
        [AuthorizationFilter(CommonValues.PermissionCodes.Customer.CustomerIndex, CommonValues.PermissionCodes.Customer.CustomerCreate)]
        [HttpGet]
        public ActionResult CustomerCreate()
        {
            SetDefaults();
            CustomerIndexViewModel model = new CustomerIndexViewModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Customer.CustomerIndex, CommonValues.PermissionCodes.Customer.CustomerCreate)]
        [HttpPost]
        public ActionResult CustomerCreate(CustomerIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var customerBo = new CustomerBL();

            if (ModelState.IsValid)
            {
                if (viewModel.DealerId == null)
                {
                    viewModel.DealerId = viewModel.IsDealerCustomer ? UserManager.UserInfo.GetUserDealerId() : viewModel.DealerId;
                }

                if (viewModel.DealerId != null)
                {
                    int count = 0;
                    CustomerListModel listModel = new CustomerListModel { DealerId = viewModel.DealerId };
                    List<CustomerListModel> customerList = customerBo.ListCustomers(UserManager.UserInfo, listModel, out count).Data;
                    var control = (from row in customerList.AsEnumerable()
                                   where row.DealerId != null
                                   select row);
                    if (control.Any())
                    {
                        SetMessage(MessageResource.Customer_Warning_DealerCustomerExists, CommonValues.MessageSeverity.Fail);
                        return View(viewModel);
                    }
                }

                viewModel.CommandType = viewModel.CustomerId > 0
                                            ? CommonValues.DMLType.Update
                                            : CommonValues.DMLType.Insert;

                customerBo.DMLCustomer(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();
                CustomerIndexViewModel model = new CustomerIndexViewModel();
                if (UserManager.UserInfo.GetUserDealerId() != 0)
                    model.DealerId = UserManager.UserInfo.GetUserDealerId();
                return View(model);
            }
            return View(viewModel);
        }

        #endregion

        #region Customer Update
        [AuthorizationFilter(CommonValues.PermissionCodes.Customer.CustomerIndex, CommonValues.PermissionCodes.Customer.CustomerUpdate)]
        [HttpGet]
        public ActionResult CustomerUpdate(int id = 0)
        {
            SetDefaults();
            var v = new CustomerIndexViewModel();
            if (id > 0)
            {
                var customerBo = new CustomerBL();
                v.CustomerId = id;
                customerBo.GetCustomer(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Customer.CustomerIndex, CommonValues.PermissionCodes.Customer.CustomerUpdate)]
        [HttpPost]
        public ActionResult CustomerUpdate(CustomerIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var customerBo = new CustomerBL();
            if (ModelState.IsValid)
            {
                if (viewModel.DealerId == null)
                {
                    viewModel.DealerId = viewModel.IsDealerCustomer ? UserManager.UserInfo.GetUserDealerId() : viewModel.DealerId;
                }
                if (viewModel.DealerId != null)
                {
                    int count = 0;
                    CustomerListModel listModel = new CustomerListModel { DealerId = viewModel.DealerId };
                    List<CustomerListModel> customerList = customerBo.ListCustomers(UserManager.UserInfo, listModel, out count).Data;
                    var control = (from row in customerList.AsEnumerable()
                                   where row.DealerId != null
                                   select row);
                    if (control.Any())
                    {
                        SetMessage(MessageResource.Customer_Warning_DealerCustomerExists, CommonValues.MessageSeverity.Fail);
                        return View(viewModel);
                    }
                }

                viewModel.CommandType = viewModel.CustomerId > 0
                                            ? CommonValues.DMLType.Update
                                            : CommonValues.DMLType.Insert;
                customerBo.DMLCustomer(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region Customer Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Customer.CustomerIndex, CommonValues.PermissionCodes.Customer.CustomerDelete)]
        public ActionResult DeleteCustomer(int customerId)
        {
            CustomerIndexViewModel viewModel = new CustomerIndexViewModel() { CustomerId = customerId };
            var customerBo = new CustomerBL();
            viewModel.CommandType = viewModel.CustomerId > 0 ? CommonValues.DMLType.Delete : string.Empty;
            customerBo.DMLCustomer(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region Customer Details
        [AuthorizationFilter(CommonValues.PermissionCodes.Customer.CustomerIndex, CommonValues.PermissionCodes.Customer.CustomerDetails)]
        [HttpGet]
        public ActionResult CustomerDetails(int id = 0)
        {
            var v = new CustomerIndexViewModel();
            var customerBo = new CustomerBL();

            v.CustomerId = id;
            customerBo.GetCustomer(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion

    }
}

