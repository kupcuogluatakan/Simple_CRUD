using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.SparePartAssemble
{
    public class SparePartAssembleListModel : BaseListWithPagingModel
    {
        public SparePartAssembleListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PartCode","PART_CODE"},
                    {"PartCodeAssemble", "PART_CODE_ASSEMBLE"},
                    {"Quantity", "QUANTITY"},
                    {"IsActiveName", "IS_ACTIVE_NAME"}
                };
            SetMapper(dMapper);
        }

        public SparePartAssembleListModel()
        { 
        }

        public int? IdDealer { get; set; }
        public Int64? IdPart { get; set; }
        public Int64? IdPartAssemble { get; set; }


        //PartCode
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }

        //PartCodeAssemble
        [Display(Name = "SparePartAssemble_Display_PartCodeAssemble", ResourceType = typeof(MessageResource))]
        public string PartCodeAssemble { get; set; }

        //Quantity
        [Display(Name = "SparePartAssemble_Display_Quantity", ResourceType = typeof(MessageResource))]
        public decimal? Quantity { get; set; }

        //IsActive
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool? IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
    }
}
