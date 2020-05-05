using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Customer;
using ODMSModel.CustomerAddress;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CustomerAddressController : ControllerBase
    {
        private void SetDefaults()
        {
            // AddressTypeList
            List<SelectListItem> addressTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.AddressTypeLookup).Data;
            ViewBag.AddressTypeList = addressTypeList;
            // CountryList
            List<SelectListItem> countryList = CommonBL.ListCountries(UserManager.UserInfo).Data;
            ViewBag.CountryList = countryList;
            // StatusList
            List<SelectListItem> statusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.StatusLookup).Data;
            ViewBag.StatusList = statusList;
        }
        [HttpGet]
        public JsonResult ListCities(int? id)
        {
            return id.HasValue ? Json(CommonBL.ListCities(UserManager.UserInfo, id.Value).Data, JsonRequestBehavior.AllowGet) : Json(null, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListTowns(int? id)
        {
            return id.HasValue ? Json(CommonBL.ListTowns(UserManager.UserInfo, id.Value).Data, JsonRequestBehavior.AllowGet) : Json(null, JsonRequestBehavior.AllowGet);
        }

        #region Customer Address Index

        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerAddress.CustomerAddressIndex)]
        [HttpGet]
        public ActionResult CustomerAddressIndex(string customerId)
        {
            SetDefaults();
            CustomerAddressListModel model = new CustomerAddressListModel();
            if (customerId != null)
            {
                CustomerIndexViewModel custModel = new CustomerIndexViewModel();
                CustomerBL cust = new CustomerBL();
                custModel.CustomerId = customerId.GetValue<int>();
                cust.GetCustomer(UserManager.UserInfo, custModel);

                model.CustomerId = customerId.GetValue<int>();
                model.CustomerName = custModel.CustomerName;
            }
            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerAddress.CustomerAddressIndex, CommonValues.PermissionCodes.CustomerAddress.CustomerAddressDetails)]
        public ActionResult ListCustomerAddress([DataSourceRequest] DataSourceRequest request, CustomerAddressListModel model)
        {
            var customerAddressBo = new CustomerAddressBL();
            var v = new CustomerAddressListModel(request);
            var totalCnt = 0;
            v.CustomerId = model.CustomerId;
            var returnValue = customerAddressBo.ListCustomerAddresses(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Customer Address Create
        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerAddress.CustomerAddressIndex, CommonValues.PermissionCodes.CustomerAddress.CustomerAddressCreate)]
        [HttpGet]
        public ActionResult CustomerAddressCreate(int customerId, string customerName, string addressTypeId)
        {
            if (String.IsNullOrEmpty(customerName))
            {
                CustomerBL cBo = new CustomerBL();
                CustomerIndexViewModel cModel = new CustomerIndexViewModel();
                cModel.CustomerId = customerId;
                cBo.GetCustomer(UserManager.UserInfo, cModel);
                customerName = cModel.CustomerName;
            }

            CustomerAddressIndexViewModel model = new CustomerAddressIndexViewModel
            {
                CustomerId = customerId,
                CustomerName = customerName,
                IsActive = true
            };

            SetDefaults();

            if (!string.IsNullOrEmpty(addressTypeId))
                model.AddressTypeId = addressTypeId.GetValue<int>();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerAddress.CustomerAddressIndex, CommonValues.PermissionCodes.CustomerAddress.CustomerAddressCreate)]
        [HttpPost]
        public ActionResult CustomerAddressCreate(CustomerAddressIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var customerAddressBo = new CustomerAddressBL();

            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Insert;

                int totalCount = 0;
                CustomerAddressListModel caListModel = new CustomerAddressListModel();
                caListModel.CustomerId = viewModel.CustomerId;
                List<CustomerAddressListModel> addressList = customerAddressBo.ListCustomerAddresses(UserManager.UserInfo, caListModel, out totalCount).Data;
                if (totalCount != 0)
                {
                    var control = (from e in addressList
                                   where e.AddressTypeId == viewModel.AddressTypeId
                                   select e);
                    if (control.Any())
                    {
                        SetMessage(MessageResource.CustomerAddress_Warning_SameTypeExists,
                                   CommonValues.MessageSeverity.Fail);
                        return View(viewModel);
                    }
                }

                customerAddressBo.DMLCustomerAddress(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();
                CustomerAddressIndexViewModel model = new CustomerAddressIndexViewModel
                {
                    CustomerName = viewModel.CustomerName,
                    CustomerId = viewModel.CustomerId,
                    AddressId = viewModel.AddressId,
                    AddressTypeId = viewModel.AddressTypeId
                };
                return View(model);
            }

            return View(viewModel);
        }

        #endregion

        #region Customer Address Update
        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerAddress.CustomerAddressIndex, CommonValues.PermissionCodes.CustomerAddress.CustomerAddressUpdate)]
        [HttpGet]
        public ActionResult CustomerAddressUpdate(int id = 0)
        {
            SetDefaults();
            var v = new CustomerAddressIndexViewModel();
            if (id > 0)
            {
                var customerAddressBo = new CustomerAddressBL();
                v.AddressId = id;
                customerAddressBo.GetCustomerAddress(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerAddress.CustomerAddressIndex, CommonValues.PermissionCodes.CustomerAddress.CustomerAddressUpdate)]
        [HttpPost]
        public ActionResult CustomerAddressUpdate(CustomerAddressIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var customerAddressBo = new CustomerAddressBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = viewModel.AddressId > 0
                                            ? CommonValues.DMLType.Update
                                            : CommonValues.DMLType.Insert;
                customerAddressBo.DMLCustomerAddress(UserManager.UserInfo, viewModel);

                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region Customer Address Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerAddress.CustomerAddressIndex, CommonValues.PermissionCodes.CustomerAddress.CustomerAddressDelete)]
        public ActionResult DeleteCustomerAddress(int addressId)
        {
            CustomerAddressIndexViewModel viewModel = new CustomerAddressIndexViewModel() { AddressId = addressId };
            var customerAddressBo = new CustomerAddressBL();
            viewModel.CommandType = viewModel.AddressId > 0 ? CommonValues.DMLType.Delete : string.Empty;
            customerAddressBo.DMLCustomerAddress(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region Customer Address Details
        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerAddress.CustomerAddressIndex, CommonValues.PermissionCodes.CustomerAddress.CustomerAddressDetails)]
        [HttpGet]
        public ActionResult CustomerAddressDetails(int id = 0)
        {
            var v = new CustomerAddressIndexViewModel();
            var customerAddressBo = new CustomerAddressBL();

            v.AddressId = id;
            customerAddressBo.GetCustomerAddress(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
    }
}