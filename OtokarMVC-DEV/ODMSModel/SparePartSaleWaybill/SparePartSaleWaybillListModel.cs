using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.SparePartSaleWaybill
{
    public class SparePartSaleWaybillListModel : BaseListWithPagingModel
    {
        public bool IsSelected { get; set; }
        public int SparePartSaleWaybillId { get; set; }
        public int DealerId { get; set; }
        public string WaybillSerialNo { get; set; }
        [Display(Name = "SparePartSale_Display_WayBillNo", ResourceType = typeof(MessageResource))]
        public string WaybillNo { get; set; }
        [Display(Name = "SparePartSale_Display_WayBillDate", ResourceType = typeof(MessageResource))]
        public DateTime WaybillDate { get; set; }
        public string WaybillReferenceNo { get; set; }
        public int ShippingAddressId { get; set; }
        public int CustomerId { get; set; }
        public long? DeliveryId { get; set; }
        public long? SparePartSaleInvoiceId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
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
        public string SparePartSaleIdList { get; set; }
        [Display(Name = "SparePartSaleWaybill_Display_TotalListPriceWithoutVAT", ResourceType = typeof(MessageResource))]
        public decimal? TotalListPriceWithoutVAT { get; set; }
        [Display(Name = "SparePartSaleWaybill_Display_TotalPriceWithoutVAT", ResourceType = typeof(MessageResource))]
        public decimal? TotalPriceWithoutVAT { get; set; }
        public bool IsActive { get; set; }
        public SparePartSaleWaybillListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
            {
            };
            SetMapper(dMapper);
        }

        public SparePartSaleWaybillListModel() { }

    }
}
