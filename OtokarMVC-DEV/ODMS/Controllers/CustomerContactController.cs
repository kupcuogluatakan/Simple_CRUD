using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Customer;
using ODMSModel.CustomerContact;
using System.Linq;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CustomerContactController : ControllerBase
    {
        private void SetDefaults()
        {
            // ContactTypeList
            List<SelectListItem> contactTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.CustContTypeLookup).Data;
            ViewBag.ContactTypeList = contactTypeList;
        }

        #region Customer Contact Index

        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerContact.CustomerContactIndex)]
        [HttpGet]
        public ActionResult CustomerContactIndex(string customerId)
        {
            SetDefaults();
            CustomerContactListModel model = new CustomerContactListModel();
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

        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerContact.CustomerContactIndex, CommonValues.PermissionCodes.CustomerContact.CustomerContactDetails)]
        public ActionResult ListCustomerContact([DataSourceRequest] DataSourceRequest request, CustomerContactListModel model)
        {
            var customerContactBo = new CustomerContactBL();
            var v = new CustomerContactListModel(request);
            var totalCnt = 0;
            v.CustomerId = model.CustomerId;
            var returnValue = customerContactBo.ListCustomerContactes(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Customer Contact Create
        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerContact.CustomerContactIndex, CommonValues.PermissionCodes.CustomerContact.CustomerContactCreate)]
        [HttpGet]
        public ActionResult CustomerContactCreate(int customerId, string customerName)
        {
            CustomerContactIndexViewModel model = new CustomerContactIndexViewModel();
            model.CustomerId = customerId;
            model.CustomerName = customerName;
            model.Name = customerName;

            CustomerIndexViewModel custMo = new CustomerIndexViewModel();
            custMo.CustomerId = customerId;
            CustomerBL custBo = new CustomerBL();
            custBo.GetCustomer(UserManager.UserInfo, custMo);
            model.Surname = custMo.CustomerLastName;

            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerContact.CustomerContactIndex,
            CommonValues.PermissionCodes.CustomerContact.CustomerContactCreate)]
        [HttpPost]
        public ActionResult CustomerContactCreate(CustomerContactIndexViewModel viewModel,
                                                  IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var customerContactBo = new CustomerContactBL();

            if (ModelState.IsValid)
            {
                int totalCount = 0;
                // TFS No: 29446 Ayni tipte bir iletişim bilgi yaratılabilir. OYA
                CustomerContactListModel contactListModel = new CustomerContactListModel();
                contactListModel.CustomerId = viewModel.CustomerId;
                List<CustomerContactListModel> contactList = customerContactBo.ListCustomerContactes(UserManager.UserInfo, contactListModel, out totalCount).Data;
                if (totalCount != 0)
                {
                    var control = (from e in contactList.AsEnumerable()
                                   where e.ContactTypeId == viewModel.ContactTypeId
                                   select e);
                    if (control.Any())
                    {
                        SetMessage(MessageResource.CustomerContact_Warning_SameContactTypeExists, CommonValues.MessageSeverity.Fail);
                        return View(viewModel);
                    }
                }

                viewModel.CommandType = viewModel.ContactId > 0
                                            ? CommonValues.DMLType.Update
                                            : CommonValues.DMLType.Insert;
                customerContactBo.DMLCustomerContact(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);

                CustomerIndexViewModel custMo = new CustomerIndexViewModel();
                custMo.CustomerId = viewModel.CustomerId;
                CustomerBL custBo = new CustomerBL();
                custBo.GetCustomer(UserManager.UserInfo, custMo);

                ModelState.Clear();
                CustomerContactIndexViewModel model = new CustomerContactIndexViewModel
                {
                    CustomerName = viewModel.CustomerName,
                    CustomerId = viewModel.CustomerId,
                    Name = viewModel.CustomerName,
                    Surname = custMo.CustomerLastName
                };
                return View(model);
            }
            return View(viewModel);
        }

        #endregion

        #region Customer Contact Update
        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerContact.CustomerContactIndex, CommonValues.PermissionCodes.CustomerContact.CustomerContactUpdate)]
        [HttpGet]
        public ActionResult CustomerContactUpdate(int id = 0)
        {
            SetDefaults();
            var v = new CustomerContactIndexViewModel();
            if (id > 0)
            {
                var customerContactBo = new CustomerContactBL();
                v.ContactId = id;
                customerContactBo.GetCustomerContact(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerContact.CustomerContactIndex, CommonValues.PermissionCodes.CustomerContact.CustomerContactUpdate)]
        [HttpPost]
        public ActionResult CustomerContactUpdate(CustomerContactIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var customerContactBo = new CustomerContactBL();
            if (ModelState.IsValid)
            {
                int totalCount = 0;
                // TFS No: 29446 Ayni tipte bir iletişim bilgi yaratılabilir. OYA
                CustomerContactListModel contactListModel = new CustomerContactListModel();
                contactListModel.CustomerId = viewModel.CustomerId;
                List<CustomerContactListModel> contactList = customerContactBo.ListCustomerContactes(UserManager.UserInfo, contactListModel, out totalCount).Data;
                if (totalCount != 0)
                {
                    var control = (from e in contactList.AsEnumerable()
                                   where e.ContactTypeId == viewModel.ContactTypeId
                                   && e.ContactId != viewModel.ContactId
                                   select e);
                    if (control.Any())
                    {
                        SetMessage(MessageResource.CustomerContact_Warning_SameContactTypeExists, CommonValues.MessageSeverity.Fail);
                        return View(viewModel);
                    }
                }

                viewModel.CommandType = viewModel.ContactId > 0
                                            ? CommonValues.DMLType.Update
                                            : CommonValues.DMLType.Insert;
                customerContactBo.DMLCustomerContact(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region Customer Contact Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerContact.CustomerContactIndex, CommonValues.PermissionCodes.CustomerContact.CustomerContactDelete)]
        public ActionResult DeleteCustomerContact(int contactId)
        {
            CustomerContactIndexViewModel viewModel = new CustomerContactIndexViewModel() { ContactId = contactId };
            var customerContactBo = new CustomerContactBL();
            viewModel.CommandType = viewModel.ContactId > 0 ? CommonValues.DMLType.Delete : string.Empty;
            customerContactBo.DMLCustomerContact(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region Customer Contact Details
        [AuthorizationFilter(CommonValues.PermissionCodes.CustomerContact.CustomerContactIndex, CommonValues.PermissionCodes.CustomerContact.CustomerContactDetails)]
        [HttpGet]
        public ActionResult CustomerContactDetails(int id = 0)
        {
            var v = new CustomerContactIndexViewModel();
            var customerContactBo = new CustomerContactBL();

            v.ContactId = id;
            customerContactBo.GetCustomerContact(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
    }
}