using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODMSWebService.Model
{
    public class Vehicle 
    {
        public string CustomerNameSurname { get; set; }
        public int VehicleId  { get; set; }
        public string VinNo  { get; set; }
        public string ModelCode  { get; set; }
        public string ModelName  { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public int ModelYear  { get; set; }
    }
}