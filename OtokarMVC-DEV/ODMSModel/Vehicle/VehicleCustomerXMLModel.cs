using System;

namespace ODMSModel.Vehicle
{
    public class VehicleCustomerXMLModel : ModelBase
    {
        public string CustomerNo { get; set; }
        public string CustomerSSID { get; set; }
        public string DealerName { get; set; }
        public string TCIdentity { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string VatNo { get; set; }
        public string VatOffice { get; set; }
        public string Address { get; set; }
        public string CityPlateCode { get; set; }
        public string CityName { get; set; }
        public string CountryCode { get; set; }
        public string Desc { get; set; }
        public string Company { get; set; }
        public string VinNo { get; set; }
        public string EngineNo { get; set; }
        public string Plate { get; set; }
        public string WarrantyKm { get; set; }
        public string IsPublic { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string CustType { get; set; }
        public DateTime? WarrantyColorEndDate { get; set; }
        public DateTime? WarrantyCorrosionEndDate { get; set; }
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }


    }
}
