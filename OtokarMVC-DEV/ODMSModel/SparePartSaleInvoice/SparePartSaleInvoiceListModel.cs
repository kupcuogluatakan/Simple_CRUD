using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.SparePartSaleInvoice
{
    public class SparePartSaleInvoiceListModel : BaseListWithPagingModel
    {
        public int SparePartSaleInvoiceId { get; set; }
        public int DealerId { get; set; }
        public int CustomerId { get; set; }
        public string InvoiceSerialNo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int? DueDuration { get; set; }
        public int? PaymentTypeId { get; set; }
        public int? BankId { get; set; }
        public short? InstalmentNumber { get; set; }
        public decimal PayAmount { get; set; }
        public string TransmitNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
        public string SparePartSaleWaybillIdList { get; set; }
        public int BillingAddressId { get; set; }
        public string CustomerAddressCountryText { get; set; }
        public string CustomerAddressCityText { get; set; }
        public string CustomerAddressTownText { get; set; }
        public string CustomerAddressZipCode { get; set; }
        public string CustomerAddress1 { get; set; }
        public string CustomerAddress2 { get; set; }
        public string CustomerAddress3 { get; set; }
        public string CustomerTaxOffice { get; set; }
        public string CustomeTaxNo { get; set; }
        public string CustomerTCIdentity { get; set; }
        public string CustomerPassportNo { get; set; }
        public bool IsActive { get; set; }
        public SparePartSaleInvoiceListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
            {
            };
            SetMapper(dMapper);
        }

        public SparePartSaleInvoiceListModel() { }

    }
}
