using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.OtokarSparePartSaleInvoiceSearch;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using System.Web.Configuration;
using System.Data;
using System;
using System.Globalization;

namespace ODMSBusiness
{
    public class OtokarSparePartSaleInvoiceSearchBL : BaseBusiness
    {
        public ResponseModel<OtokarSparePartSaleInvoiceSearchViewModel> ListInvoices(UserInfo user, OtokarSparePartSaleInvoiceSearchViewModel filter, out int totalCnt)
        {
            var response = new ResponseModel<OtokarSparePartSaleInvoiceSearchViewModel>();
            totalCnt = 0;
            try
            {
                List<OtokarSparePartSaleInvoiceSearchViewModel> returnModel = new List<OtokarSparePartSaleInvoiceSearchViewModel>();
                var dealerBo = new DealerBL();

                DealerViewModel dealerModel = dealerBo.GetDealer(user, filter.DealerId).Model;
                using (var pssc = GetClient())
                {
                    string psUser = WebConfigurationManager.AppSettings["PSSCUser"];
                    string psPassword = WebConfigurationManager.AppSettings["PSSCPass"];
                    string startDate = filter.StartDate.GetValue<DateTime>().Year.ToString()
                        + filter.StartDate.GetValue<DateTime>().Month.ToString().PadLeft(2, '0')
                        + filter.StartDate.GetValue<DateTime>().Day.ToString().PadLeft(2, '0');
                    string endDate = filter.EndDate.GetValue<DateTime>().Year.ToString()
                        + filter.EndDate.GetValue<DateTime>().Month.ToString().PadLeft(2, '0')
                        + filter.EndDate.GetValue<DateTime>().Day.ToString().PadLeft(2, '0');

                    var rValue = pssc.ZYP_SD_INVOICE_LIST(psUser, psPassword, startDate, endDate, dealerModel.SSID);
                    DataTable dt = rValue.Tables["Table1"];
                    foreach (DataRow row in dt.Rows)
                    {
                        OtokarSparePartSaleInvoiceSearchViewModel item = new OtokarSparePartSaleInvoiceSearchViewModel();
                        item.WaybillNo = row["VGBEL"].ToString();
                        item.InvoiceDate = row["FKDAT"].GetValue<DateTime>();
                        item.InvoicePriceWithVAT = row["TOTAL"].ToString().Replace(".", ",").GetValue<decimal>().ToString("N", new System.Globalization.CultureInfo("tr-TR"));
                        item.CurrencyCode = row["WAERK"].ToString();
                        item.InvoiceNo = row["VBELN"].ToString();
                        returnModel.Add(item);
                    }
                }

                response.Data = returnModel;
                response.Total = totalCnt;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), string.Empty);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
                throw ex;
            }

            return response;
        }
    }
}
