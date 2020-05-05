using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSModel.ListModel;

namespace ODMSModel.CountryVatRatio
{
    public class CountryListModel : BaseListWithPagingModel
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int TownId { get; set; }
        public string TownName { get; set; }
        public int CountryLastUpdate { get; set; }
        public int CityLastUpdate { get; set; }
        public int TownLastUpdate { get; set; }
    }
}
