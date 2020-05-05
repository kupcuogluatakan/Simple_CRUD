using System;

namespace ODMSModel.Guarantee
{
    public class GuaranteeXMLModel 
    {
        public string GuaranteeId { get; set; }
        public string GuaranteeSeq { get; set; }
        public string DealerSSId { get; set; }
        public string VehicleVinNo { get; set; }
        public string RequestUser { get; set; }
        public string SSIdGuarantee { get; set; }
        public string FauilureCode { get; set; }
        public DateTime? BreakdownDate { get; set; }
        public string CurrencyCode { get; set; }
        public string WorkOrderId { get; set; }
        public DateTime? VehicleEnteryDate { get; set; }
        public DateTime? VehicleLeaveDate { get; set; }
        public string CategoryLookval { get; set; }
        public string ApproveUser { get; set; }
        public string RequestDesc { get; set; }
        public string RequestDesc2 { get; set; }
        public string VehicleKm { get; set; }
        public string PdiGif { get; set; }
        public string CampaignCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerFax { get; set; }
        public string VehiclePlate { get; set; }
        public string VehicleNotes { get; set; }
        public string RequestWarrantyStatus { get; set; }
        public string LabourTotalPrice { get; set; }
        public string PartsTotalPrice { get; set; }
        public string PartsColumn { get; set; }
        public string LaboursColumn { get; set; }
        public string IndicatorTypeName { get; set; }
        public string ProcessTypeName { get; set; }
        public string ExternalLaboursColumn { get; set; }
    }
}
