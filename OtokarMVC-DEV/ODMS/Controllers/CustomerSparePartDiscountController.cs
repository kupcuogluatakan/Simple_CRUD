using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Customer;
using ODMSModel.CustomerSparePartDiscount;
using ODMSModel.ObjectSearch;
using ODMSModel.SparePart;
using Permission = ODMSCommon.CommonValues.PermissionCodes.CustomerSparePartDiscount;
using ODMSBusiness.Business;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CustomerSparePartDiscountController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.SparePartClassCodeList = SparePartBL.GetSparePartClassCodeListForComboBox(UserManager.UserInfo).Data;
            ViewBag.YesNoList = CommonBL.ListYesNo().Data;
            ViewBag.CustomerList = CustomerBL.ListCustomerNameAndNoAsSelectListItem().Data;
            ViewBag.OrgTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.PartSaleOrgTypeLookup).Data;
            ViewBag.CustomerIdList = CustomerBL.ListCustomerNameAndNoAsSelectListItem().Data;
        }

        [HttpGet]
        [AuthorizationFilter(Permission.CustomerSparePartDiscountIndex)]
        public ActionResult CustomerSparePartDiscountIndex()
        {
            SetDefaults();
            return View(new CustomerSparePartDiscountListModel());
        }

        [HttpGet]
        public JsonResult GetCustomerList(string orgTypeId)
        {
            CustomerListModel custListModel = new CustomerListModel();
            List<SelectListItem> customerList = new List<SelectListItem>();
            CustomerBL custBo = new CustomerBL();
            int totalCount = 0;

            if (orgTypeId.GetValue<int>() == 1)
            {
                customerList = custBo.ListCustomers(UserManager.UserInfo, custListModel, out totalCount).Data.Where(c => c.DealerId != null && c.DealerId != UserManager.UserInfo.GetUserDealerId()).Select(s => new SelectListItem()
                {
                    Text = s.CustomerName,
                    Value = s.CustomerId.ToString()
                }).ToList();
            }
            else
            {
                custListModel.DealerId = null;
                customerList = custBo.ListCustomers(UserManager.UserInfo, custListModel, out totalCount).Data.Where(c => c.DealerId != UserManager.UserInfo.GetUserDealerId()).ToList().Select(s => new SelectListItem()
                {
                    Text = s.CustomerName,
                    Value = s.CustomerId.ToString()
                }).ToList();
            }

            return !string.IsNullOrWhiteSpace(orgTypeId)
                ? Json(customerList, JsonRequestBehavior.AllowGet)
                : Json(CustomerBL.ListCustomerAsSelectListItem(UserManager.UserInfo).Data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetOrgTypeList(string customerId)
        {
            CustomerBL customerBo = new CustomerBL();
            CustomerIndexViewModel custModel = new CustomerIndexViewModel();
            custModel.CustomerId = customerId.GetValue<int>();
            customerBo.GetCustomer(UserManager.UserInfo, custModel);
            int orgTypeId = 0;
            List<SelectListItem> orgTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.PartSaleOrgTypeLookup).Data;

            if (customerId.HasValue())
            {
                if (custModel.DealerId == null)
                    orgTypeId = (int)CommonValues.CustomerSparePartDiscount.Customer;
                else
                    orgTypeId = (int)CommonValues.CustomerSparePartDiscount.ServiceDealer;
            }
            return Json(new { result = orgTypeList, selectedOrgTypeId = orgTypeId }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.CustomerSparePartDiscountIndex)]
        public ActionResult GetSparePartData(int partId)
        {
            SparePartIndexViewModel model = new SparePartIndexViewModel();
            model.PartId = partId;
            new SparePartBL().GetSparePart(UserManager.UserInfo, model);
            return Json(new { SparePartClassCode = model.ClassCode });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.CustomerSparePartDiscountIndex)]
        public ActionResult ListCustomerSparePartDiscount([DataSourceRequest]DataSourceRequest request, CustomerSparePartDiscountListModel model)
        {
            var totalCnt = 0;
            var bo = new CustomerSparePartDiscountBL();
            var referenceModel = new CustomerSparePartDiscountListModel(request) { DealerId = UserManager.UserInfo.GetUserDealerId(), PartName = model.PartName, PartCode = model.PartCode, PartClassList = model.PartClassList, CustomerIdList = model.CustomerIdList };
            var returnValue = bo.List(UserManager.UserInfo, referenceModel,out totalCnt).Data;
            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.CustomerSparePartDiscountIndex, Permission.CustomerSparePartDiscountCreate)]
        public ActionResult CustomerSparePartDiscountCreate()
        {
            CustomerSparePartDiscountViewModel model = new CustomerSparePartDiscountViewModel();
            model.IsActive = true;

            SetDefaults();

            return View(model);
        }
        [HttpGet]
        [AuthorizationFilter(Permission.CustomerSparePartDiscountIndex, Permission.CustomerSparePartDiscountUpdate)]
        public ActionResult CustomerSparePartDiscountUpdate(long id)
        {
            var bo = new CustomerSparePartDiscountBL();

            SetDefaults();

            return View("CustomerSparePartDiscountCreate", bo.GetById(id, ODMSCommon.Security.UserManager.LanguageCode).Model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.CustomerSparePartDiscountIndex, Permission.CustomerSparePartDiscountCreate)]
        public ActionResult CustomerSparePartDiscountCreate(CustomerSparePartDiscountViewModel model)
        {
            SetDefaults();
            ModelState.Remove("PartId");
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                new CustomerSparePartDiscountBL().DMLCustomerSparePartDiscount(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);

                CustomerSparePartDiscountViewModel newModel = new CustomerSparePartDiscountViewModel();
                model.IsActive = true;
                return View(newModel);
            }
            return View(model);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.CustomerSparePartDiscountIndex, Permission.CustomerSparePartDiscountCreate)]
        public ActionResult CustomerSparePartDiscountUpdate(CustomerSparePartDiscountViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Update;
                new CustomerSparePartDiscountBL().DMLCustomerSparePartDiscount(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                return RedirectToAction("CustomerSparePartDiscountUpdate", new { id = model.CustomerSparePartDiscountId });
            }
            SetDefaults();
            return View("CustomerSparePartDiscountCreate", model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.CustomerSparePartDiscountIndex, Permission.CustomerSparePartDiscountDelete)]
        public ActionResult CustomerSparePartDiscountDelete(long id)
        {
            var bus = new CustomerSparePartDiscountBL();
            var model = new CustomerSparePartDiscountViewModel { CustomerSparePartDiscountId = id, CommandType = CommonValues.DMLType.Delete };
            bus.DMLCustomerSparePartDiscount(UserManager.UserInfo, model);
            ViewBag.HideFormElements = false;
            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.CustomerSparePartDiscountIndex)]
        public ActionResult SearchCustomer(int? orgTypeId)
        {
            ViewBag.OrgTypeId = orgTypeId;
            return View("_CustomerSearch");
        }

        [AuthorizationFilter(Permission.CustomerSparePartDiscountIndex)]
        public ActionResult SearchCustomer([DataSourceRequest]DataSourceRequest request, CustomerSearchViewModel model)
        {
            var objectSearchBo = new ObjectSearchBL();
            var v = new CustomerSearchListModel(request);
            var totalCnt = 0;
            v.CustomerFullName = model.CustomerFullName;
            v.TCIdentityNo = model.TCIdentityNo;
            v.TaxNo = model.TaxNo;
            v.PassportNo = model.PassportNo;
            v.OrgTypeId = model.OrgTypeId;
            var returnValue = objectSearchBo.SearchCustomer(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
    }
}