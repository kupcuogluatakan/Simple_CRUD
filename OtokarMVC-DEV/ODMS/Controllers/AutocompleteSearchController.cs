using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ODMSBusiness;
using ODMSModel.ListModel;
using ODMSModel.Customer;
using ODMSData;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    public class AutocompleteSearchController : Controller
    {
        //
        // GET: /AutocompleteSearch/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AutocompleteSearch(string SearchType, string ControlId, string Title1, string Title2, string DefaultText, string DefaultValue, string ExtraParameter = "", string CallbackFunction = "", bool Required = false)
        {
            ViewBag.SearchType = SearchType.Trim();
            ViewBag.ControlBlockId = "block" + ControlId.Trim();
            ViewBag.ControlId = ControlId.Trim();
            ViewBag.ControlTxtId = "txt" + ControlId.Trim();
            ViewBag.Title1 = Title1;
            ViewBag.Title2 = Title2;
            ViewBag.DefaultText = DefaultText;
            ViewBag.DefaultValue = DefaultValue;
            ViewBag.ExtraParameter = ExtraParameter;
            ViewBag.CallbackFunction = CallbackFunction;
            ViewBag.Required = Required;
            return View();
        }
        public ActionResult AutocompleteSearchForBarcode(string SearchType, string ControlId, string Title1, string Title2, string DefaultText, string DefaultValue, string ExtraParameter = "", string CallbackFunction = "", bool Required = false)
        {
            ViewBag.SearchType = SearchType.Trim();
            ViewBag.ControlBlockId = "block" + ControlId.Trim();
            ViewBag.ControlId = ControlId.Trim();
            ViewBag.ControlTxtId = "txt" + ControlId.Trim();
            ViewBag.Title1 = Title1;
            ViewBag.Title2 = Title2;
            ViewBag.DefaultText = DefaultText;
            ViewBag.DefaultValue = DefaultValue;
            ViewBag.ExtraParameter = ExtraParameter;
            ViewBag.CallbackFunction = CallbackFunction;
            ViewBag.Required = Required;
            return View();
        }


        public JsonResult LoadComboData(string strSearch, string SearchFor, string ExtraParameter = null)
        {

            //strSearch = strSearch.Trim();
            strSearch = Uri.UnescapeDataString(strSearch);


            if (SearchFor.Trim() == "test")
            {
                var res = GenerateData();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            if (SearchFor.Trim().IndexOf("SparePart") == 0)//Orhan: Aynı ekranda 2 "SparePart" eklenebilmesini sağlıyoruz.
            {
                var rValue = SetSparePartData(strSearch, ExtraParameter);

                return Json(rValue, JsonRequestBehavior.AllowGet);
            }

            if (SearchFor.Trim() == "OrginalSparePart")
            {
                var rValue = SparePartBL.ListOriginalSparePartAsAutoCompSearch(UserManager.UserInfo, strSearch).Data;

                return Json(rValue, JsonRequestBehavior.AllowGet);
            }

            if (SearchFor.Trim() == "NotOrginalSparePart")
            {
                var rValue = SparePartBL.ListNotOriginalSparePartAsAutoCompSearch(UserManager.UserInfo, strSearch).Data;

                return Json(rValue, JsonRequestBehavior.AllowGet);
            }
            if (SearchFor.Trim() == "StockCardPartsWithDealer")
            {
                var rValue = SparePartBL.ListStockCardPartsAsAutoCompSearch(UserManager.UserInfo,strSearch).Data;

                return Json(rValue, JsonRequestBehavior.AllowGet);
            }
            if (SearchFor.Trim() == "AllSparePart")
            {
                var listOrginal = SparePartBL.ListOriginalSparePartAsAutoCompSearch(UserManager.UserInfo, strSearch).Data;
                var listNotOrginal = SparePartBL.ListNotOriginalSparePartAsAutoCompSearch(UserManager.UserInfo, strSearch).Data;

                listOrginal.AddRange(listNotOrginal);

                return Json(listOrginal, JsonRequestBehavior.AllowGet);
            }

            if (SearchFor.Trim() == "LabourName")
            {

                var rValue = SetLabourNameData(strSearch, ExtraParameter);

                return Json(rValue, JsonRequestBehavior.AllowGet);
            }
            if (SearchFor.Trim() == "WorkOrderLabourName")
            {
                // strSearch = HttpUtility.HtmlDecode(strSearch);

                var rValue = SetWorkOrderLabourNameData(strSearch, ExtraParameter);

                return Json(rValue, JsonRequestBehavior.AllowGet);
            }
            if (SearchFor.Trim() == "ProposalLabourName")
            {
                // strSearch = HttpUtility.HtmlDecode(strSearch);

                var rValue = SetWorkOrderLabourNameData(strSearch, ExtraParameter);

                return Json(rValue, JsonRequestBehavior.AllowGet);
            }
            if (SearchFor.Trim() == "LabourList")
            {
                var maintenanceLabourService = new MaintenanceLabourBL();

                var rValue = maintenanceLabourService.ListLabours(UserManager.UserInfo, strSearch).Data;

                return Json(rValue, JsonRequestBehavior.AllowGet);
            }
            if (SearchFor.Trim() == "CustomerList")
            {
                int totalcnt = 0;
                var data = new CustomerData();
                var rValue = data.SearchCustomer(strSearch);


                // new CustomerBL().ListCustomers(new CustomerListModel(), out totalcnt);
                return Json(rValue, JsonRequestBehavior.AllowGet);
            }
            //if (SearchFor.Trim() == "project")
            //{
            //    var res = (from P in db.ProjectMasts
            //               where P.ProjectDesc.ToLower().Contains(strSearch.ToLower()) || P.ProjectName.ToLower().Contains(strSearch.ToLower())
            //               select P).ToList();

            //    return Json(res, JsonRequestBehavior.AllowGet);
            //}
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public List<AutocompleteSearchListModel> SetSparePartData(string strSearch, string extraParameter)
        {
            List<AutocompleteSearchListModel> list_ACSearchModel = new List<AutocompleteSearchListModel>();

            list_ACSearchModel = SparePartBL.ListSparePartAsAutoCompSearch(UserManager.UserInfo,strSearch, extraParameter).Data;

            return list_ACSearchModel;

        }
        private List<AutocompleteSearchListModel> SetLabourNameData(string strSearch, string extParam)
        {

            return new LabourBL().ListLabourNameAsAutoCompleteSearch(UserManager.UserInfo,strSearch, extParam).Data;

        }
        //Extra parameter is VehicleId here
        private List<AutocompleteSearchListModel> SetWorkOrderLabourNameData(string strSearch, string ExtraParameter)
        {

            return new LabourBL().ListWorkOrderLabourNameAsAutoCompleteSearch(UserManager.UserInfo, strSearch, ExtraParameter).Data;

        }

        public List<AutocompleteSearchListModel> GenerateData()
        {
            List<AutocompleteSearchListModel> completeList = new List<AutocompleteSearchListModel> { };
            for (int i = 0; i < 5; i++)
            {
                AutocompleteSearchListModel temp = new AutocompleteSearchListModel
                {
                    Column1 = "test #" + (i + 1).ToString(),
                    Column2 = "ikinco kolon #" + (i + 1).ToString(),
                    Column3 = "ikinco kolon #" + (i + 1).ToString(),
                    Column4 = "ikinco kolon #" + (i + 1).ToString(),
                    Id = i
                };
                completeList.Add(temp);
            }
            return completeList;
        }
    }
}
