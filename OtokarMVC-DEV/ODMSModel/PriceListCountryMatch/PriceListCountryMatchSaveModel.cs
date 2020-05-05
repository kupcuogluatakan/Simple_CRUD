using System;
using System.Collections.Generic;

namespace ODMSModel.PriceListCountryMatch
{
    public class PriceListCountryMatchSaveModel:ModelBase
    {
        public int? PriceListId { get; set; }
        public List<String> CountryList { get; set; }

        public PriceListCountryMatchSaveModel()
        {
            CountryList = new List<String>();
        }
    }
}
