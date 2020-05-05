using System;

namespace ODMSModel.Vehicle
{
    public class VehicleSalesXMLModel :ModelBase
    {
        public string VinNo { get; set; }
        public string DealerSSID { get; set; }
        public string DealerCode { get; set; }
        public string DealerType { get; set; }
        public string EngineNo { get; set; }
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string CustomerNo { get; set; }
        public string Plate { get; set; }
        public string WarrantyKm { get; set; }
        public DateTime? WarrantyColorEndDate { get; set; }
        public DateTime? WarrantyCorrosionEndDate { get; set; }
    }
}
