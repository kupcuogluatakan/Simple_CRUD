using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class WorkOrderCardNumericalOverviewReport
    {
        public string DealerName{ get; set; }
        public int TotalVehicle { get; set; }
        public decimal TotalPrice { get; set; }
        public string CurrencyCode { get; set; }
        public int TotalWorkOrder { get; set; }
        //Campaign
        public int TotalCampaign { get; set; }
        public decimal TotalCampaignPrice { get; set; }
        public decimal TotalCampaignPercentage { get; set; }
        //PDI
        public int TotalPDI { get; set; }
        public decimal TotalPDIPrice { get; set; }
        public decimal TotalPDIPercentage { get; set; }
        //VehicleGuarantee
        public int TotalVehicleGuarantee { get; set; }
        public decimal TotalVehicleGuaranteePrice { get; set; }
        public decimal ToatlVehicleGuaranteePercentage { get; set; }
        //Customer
        public int TotalCustomer { get; set; }
        public decimal TotalCustomerPrice { get; set; }
        public decimal TotalCustomerPercentage { get; set; }
        //SparePart
        public int TotalSparePart { get; set; }
        public decimal TotalSparePartPrice { get; set; }
        public decimal TotalSparePartPercentage { get; set; }
        //CommercialGuarantee
        public int TotalCommercialGuarantee { get; set; }
        public decimal TotalCommercialGuaranteePrice { get; set; }
        public decimal TotalCommercialGuaranteePercentage { get; set; }
        //RoadAssistance
        public int TotalRoadAssistance { get; set; }
        public decimal TotalRoadAssistancePrice { get; set; }
        public decimal TotalRoadAssistancePercentage { get; set; }

    }
}
