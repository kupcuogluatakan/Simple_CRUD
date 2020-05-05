using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODMSWebService.Model
{
    public class Dealer 
    {
        public int DealerId { get; set; }
        public string DealerName { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public int TownId { get; set; }
        public string Town { get; set; }
        public string GroupName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitute { get; set; }
    }
}