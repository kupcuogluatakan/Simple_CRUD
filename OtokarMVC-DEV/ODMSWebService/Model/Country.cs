using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODMSWebService.Model
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int TownId { get; set; }
        public string TownName { get; set; }
    }
}