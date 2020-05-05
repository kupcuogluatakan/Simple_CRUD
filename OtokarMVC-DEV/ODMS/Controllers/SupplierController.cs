using System.Collections.Generic;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.PurchaseOrder;
using ODMSModel.Supplier;
using System;
using System.Web.Mvc;
using System.Linq;
using Permission = ODMSCommon.CommonValues.PermissionCodes.Supplier;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class SupplierController : ControllerBase
    {
        #region Supplier Index

        [HttpGet]
        [AuthorizationFilter(Permission.SupplierIndex)]
        public ActionResult SupplierIndex()
        {
            FillComboBoxes();
            SupplierViewModel listModel = new SupplierViewModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                listModel.DealerId = UserManager.UserInfo.GetUserDealerId();

            listModel.DealerPoGroup = CommonBL.GetDealer(UserManager.UserInfo).Data.First().IdPoGroup;
            return View(listModel);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.SupplierIndex)]
        public ActionResult ListSuppliers([DataSourceRequest]DataSourceRequest request, SupplierListModel viewModel)
        {
            var bus = new SupplierBL();
            var model = new SupplierListModel(request);

            if (viewModel.IsActiveString != null)
            {
                model.SearchIsActive = Convert.ToBoolean(Convert.ToInt16(viewModel.IsActiveString));
            }
            else
            {
                model.IsActiveString = null;
            }

            model.DealerId = viewModel.DealerId;          
            model.SupplierName = viewModel.SupplierName;

            model.Phone = viewModel.Phone;
            model.MobilePhone = viewModel.MobilePhone;
            model.TaxOffice = viewModel.TaxOffice;
            model.ContactPerson = viewModel.ContactPerson;

            var totalCnt = 0;
            var returnValue = bus.ListSuppliers(UserManager.UserInfo,model, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion Supplier Index

        #region Supplier Create

        [HttpGet]
        [AuthorizationFilter(Permission.SupplierIndex, Permission.SupplierCreate)]
        public ActionResult SupplierCreate()
        {
            FillComboBoxes();
            SupplierViewModel model = new SupplierViewModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            model.IsActive = true;
            model.DealerPoGroup = CommonBL.GetDealer(UserManager.UserInfo).Data.First().IdPoGroup;
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.SupplierIndex, Permission.SupplierCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult SupplierCreate(SupplierViewModel model)
        {
            FillComboBoxes();
            if (ModelState.IsValid == false)
                return View(model);
            var bus = new SupplierBL();
            model.CommandType = CommonValues.DMLType.Insert;
            bus.DMLSupplier(UserManager.UserInfo,model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            return RedirectToAction("SupplierCreate");
        }

        #endregion Supplier Create

        #region Supplier Update

        [HttpGet]
        [AuthorizationFilter(Permission.SupplierIndex, Permission.SupplierUpdate)]
        public ActionResult SupplierUpdate(int? id)
        {
            FillComboBoxes();
            if (!(id.HasValue && id > 0))
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View();
            }

            var model = new SupplierBL().GetSupplier(UserManager.UserInfo, id.GetValueOrDefault()).Model;
            model.DealerPoGroup = CommonBL.GetDealer(UserManager.UserInfo).Data.First().IdPoGroup;
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.SupplierIndex, Permission.SupplierCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult SupplierUpdate(SupplierViewModel model)
        {
            FillComboBoxes();
            if (ModelState.IsValid == false)
                return View(model);
            var bus = new SupplierBL();
            model.CommandType = CommonValues.DMLType.Update;
            bus.DMLSupplier(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            return View(model);
        }

        #endregion Supplier Update

        #region Supplier Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.SupplierIndex, Permission.SupplierDelete)]
        public ActionResult SupplierDelete(int? id)
        {
            if (!(id.HasValue && id > 0))
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_NoRecordFound);
            }

            /* TFS No : 27469 OYA Purchase Order Kontrolü eklendi */
            int totalCount = 0;
            PurchaseOrderBL poBo = new PurchaseOrderBL();
            PurchaseOrderListModel poListModel = new PurchaseOrderListModel();
            poListModel.IdSupplier = id.GetValueOrDefault();
            List<PurchaseOrderListModel> poList = poBo.ListPurchaseOrder(UserManager.UserInfo,poListModel, out totalCount).Data;
            var control = (from e in poList.AsEnumerable()
                           where e.Status == (int) CommonValues.PurchaseOrderStatus.NewRecord
                                 || e.Status == (int) CommonValues.PurchaseOrderStatus.OpenPurchaseOrder
                           select e);
            if (control.Any())
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Supplier_Warning_DeletePoControl);
            }
            else
            {
                var bus = new SupplierBL();
                var model = new SupplierViewModel {SupplierId = id ?? 0, CommandType = CommonValues.DMLType.Delete};
                bus.DMLSupplier(UserManager.UserInfo, model);
                ModelState.Clear();
                if (model.ErrorNo == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                          MessageResource.Global_Display_Success);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                                                      model.ErrorMessage);
            }
        }

        #endregion Supplier Delete

        #region Supplier Details

        [HttpGet]
        [AuthorizationFilter(Permission.SupplierIndex, Permission.SupplierDetails)]
        public ActionResult SupplierDetails(int? id)
        {
            if (!(id.HasValue && id > 0))
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View();
            }
            return View(new SupplierBL().GetSupplier(UserManager.UserInfo,id.GetValueOrDefault()).Model);
        }

        #endregion Supplier Details

        #region Private Methods

        private void FillComboBoxes()
        {
            var bus = new SupplierBL();
            ViewBag.DealerList = bus.ListDealers().Data;
            ViewBag.CountryList = CommonBL.ListCountries(UserManager.UserInfo).Data;
        }

        #endregion Private Methods

        #region Cascading Combobox Actions

        [HttpGet]
        public JsonResult ListCities(int? id)
        {
            return id.HasValue ? Json(new SupplierBL().ListCities(UserManager.UserInfo,id.GetValueOrDefault()).Data, JsonRequestBehavior.AllowGet) : Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListTowns(int? id)
        {
            return id.HasValue ? Json(new SupplierBL().ListTowns(UserManager.UserInfo,id.Value).Data, JsonRequestBehavior.AllowGet) : Json(null, JsonRequestBehavior.AllowGet);
        }

        #endregion Cascading Combobox Actions
    }
}