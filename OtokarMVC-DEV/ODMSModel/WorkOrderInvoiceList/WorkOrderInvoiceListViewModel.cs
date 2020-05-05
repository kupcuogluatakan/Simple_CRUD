using System;

namespace ODMSModel.WorkOrderInvoiceList
{
    //Update ve Create yok!
    //[Validator(typeof(WorkOrderInvoiceListViewModelValidator))]
    public class WorkOrderInvoiceListViewModel : ModelBase
    {
        public WorkOrderInvoiceListViewModel()
        {
        }

        //[Display(Name = "CustomerDiscount_Display_IdCustomer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? IdCustomer { get; set; }

        //[Display(Name = "CustomerDiscount_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64 IdWorkOrderInv { get; set; }

        //[Display(Name = "CustomerDiscount_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? IdWorkOrder { get; set; }

        //[Display(Name = "CustomerDiscount_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerName { get; set; }

        //[Display(Name = "CustomerDiscount_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerLastName { get; set; }

        //[Display(Name = "CustomerDiscount_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string InvoiceNo { get; set; }

        //[Display(Name = "CustomerDiscount_Display_PartDiscountRation", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? InvoiceDate { get; set; }

        //[Display(Name = "CustomerDiscount_Display_LabourDiscountRation", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Plate { get; set; }


        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
