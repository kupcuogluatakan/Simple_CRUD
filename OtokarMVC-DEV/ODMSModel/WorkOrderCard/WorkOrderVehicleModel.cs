using ODMSModel.Vehicle;

namespace ODMSModel.WorkOrderCard
{
    public class WorkOrderVehicleModel:VehicleIndexViewModel
    {
        public new bool   WarrantyStatus { get; set; }
        public bool HasCampaign { get; set; }
        public int NoteCount { get; set; }
        public int PopupNoteCount { get; set; }
        public int DealerNoteCount { get; set; }
        public bool IsPdiAccomplished { get; set; }
        public bool PdiCheck { get; set; }
        public bool IsBodyWorkRequired { get; set; }
        public string BodyWorkName { get; set; }
        public int VehicleTypeId { get; set; }
        public string VCodeKod { get; set; }
        public new string  VehicleModel { get; set; }
        public new string  VehicleType { get; set; }
        public bool VatExclude { get; set; }
        public bool IsPdiApplicable { get; set; }
        public bool IsCampaignApplicable{ get; set; }
        public new string Location { get; set; }
        public new string ResponsiblePerson { get; set; }
        public new string ResponsiblePersonPhone { get; set; }
        public new bool IsHourMaint { get; set; }
        public int VehicleHour { get; set; }
        public string EngineType { get; set; }
        public int WorkOrderCount { get; set; }
        public int PeriodicMaintCount { get; set; }
    }
}
