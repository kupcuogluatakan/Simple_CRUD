using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.ViewModel;

namespace ODMSModel.SparePartAssemble
{
    [Validator(typeof(SparePartAssembleIndexViewModelValidator))]
    public class SparePartAssembleIndexViewModel : ModelBase
    {
        public SparePartAssembleIndexViewModel()
        {
            PartSearch = new AutocompleteSearchViewModel();
        }

        public int? IdDealer { get; set; }

        [UIHint("KendoAutoComplete")]
        public Int64? IdPart { get; set; }
        public Int64? IdPartAssemble { get; set; }

        //PartCode
        [Display(Name = "SparePartAssemble_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "SparePartAssemble_Display_PartCode", ResourceType = typeof(MessageResource))]
        public AutocompleteSearchViewModel PartSearch { get; set; }

        //PartCodeAssemble
        [Display(Name = "SparePartAssemble_Display_PartCodeAssemble", ResourceType = typeof(MessageResource))]
        public string PartCodeAssemble { get; set; }

        //Quantity
        [Display(Name = "SparePartAssemble_Display_Quantity", ResourceType = typeof(MessageResource))]
        public decimal? Quantity { get; set; }

        //IsActive
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
    }
}
