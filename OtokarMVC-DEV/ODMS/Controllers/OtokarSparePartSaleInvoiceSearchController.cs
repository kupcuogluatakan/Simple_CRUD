using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSModel.OtokarSparePartSaleInvoiceSearch;
using System.Collections.Generic;
using ODMSCommon;
using ODMSCommon.Resources;
using Kendo.Mvc.UI;
using System.Data;
using System.Web.Configuration;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using System;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class OtokarSparePartSaleInvoiceSearchController : ControllerBase
    {

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.OtokarSparePartSaleInvoiceSearch.OtokarSparePartSaleInvoiceSearchIndex)]
        public ActionResult OtokarSparePartSaleInvoiceSearchIndex()
        {
            return View(new OtokarSparePartSaleInvoiceSearchViewModel() { DealerId = UserManager.UserInfo.GetUserDealerId() });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.OtokarSparePartSaleInvoiceSearch.OtokarSparePartSaleInvoiceSearchIndex)]
        public ActionResult ListOtokarSparePartSaleInvoiceSearchIndex([DataSourceRequest] DataSourceRequest request, OtokarSparePartSaleInvoiceSearchViewModel model)
        {
            List<OtokarSparePartSaleInvoiceSearchViewModel> returnModel = new List<OtokarSparePartSaleInvoiceSearchViewModel>();
            if (model.StartDate == null || model.EndDate == null)
            {
                SetMessage("Tarih seçmelisiniz!", CommonValues.MessageSeverity.Fail);
            }
            else
            {
                OtokarSparePartSaleInvoiceSearchBL otokarSparePartSaleInvoiceBl = new OtokarSparePartSaleInvoiceSearchBL();
                int totalCount = 0; 
                var response = otokarSparePartSaleInvoiceBl.ListInvoices(UserManager.UserInfo, model, out totalCount);

                if (!response.IsSuccess)
                {
                    SetMessage(response.Message, CommonValues.MessageSeverity.Fail);
                }
                returnModel = response.Data;

                SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
            }
            return Json(new
            {
                Data = returnModel,
                Total = returnModel.Count
            });
        }
    }
}