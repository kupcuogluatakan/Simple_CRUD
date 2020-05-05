using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.SparePartCountryVatRatio
{
    public class SparePartCountryVatRatioListModel : BaseListWithPagingModel
    {
        public SparePartCountryVatRatioListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PartCode","PART_CODE"},
                    {"PartName", "PART_NAME"},
                    {"CountryName", "COUNTRY_NAME"},
                    {"VatRatio", "VAT_RATIO"},
                    {"IsActiveName", "IS_ACTIVE_NAME"}
                };
            SetMapper(dMapper);
        }

        public SparePartCountryVatRatioListModel()
        { 
        }

        //IdPart
        [Display(Name = "SparePartCountryVatRatio_Display_PartName", ResourceType = typeof(MessageResource))]
        public Int64? IdPart { get; set; }

        //PartCode
        [Display(Name = "SparePartCountryVatRatio_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }

        //PartName
        [Display(Name = "SparePartCountryVatRatio_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }

        //IdCountry
        [Display(Name = "SparePartCountryVatRatio_Display_CountryName", ResourceType = typeof(MessageResource))]
        public int? IdCountry { get; set; }
        //CountryName
        [Display(Name = "SparePartCountryVatRatio_Display_CountryName", ResourceType = typeof(MessageResource))]
        public string CountryName { get; set; }

        //VatRatio
        [Display(Name = "SparePartCountryVatRatio_Display_VatRatio", ResourceType = typeof(MessageResource))]
        public decimal? VatRatio { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
    }
}
