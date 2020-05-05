using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSModel.ViewModel;
namespace ODMSModel.AppointmentDetailsParts
{
    [Validator(typeof(AppointmentDetailsPartsViewModelValidator))]
    public class AppointmentDetailsPartsViewModel : ModelBase
    {
        public AppointmentDetailsPartsViewModel()
        {
            PartSearch = new AutocompleteSearchViewModel();
        }

        public bool HideFormElements { get; set; }

        public int IsManuel { get; set; }
        public string BarcodeList { get; set; }
        public int Id { get; set; }
        public int AppointIndicId { get; set; }
        public string IndicType { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public AutocompleteSearchViewModel PartSearch { get; set; }
        [Display(Name = "SparePart_Display_PartCodeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long PartId { get; set; }
        public long SelectedPartId { get; set; }
        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "AppointDetailsParts_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Quantity { get; set; }

        [Display(Name = "AppointDetailsParts_Display_ListPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ListPrice { get; set; }

        [Display(Name = "AppointDetailsParts_Display_GroupList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GroupList { get; set; }

        [Display(Name = "Currency_Display_CurrencyCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyCode { get; set; }
        
        public string MainCat { get; set; }
        public string Cat { get; set; }
        public string SubCat { get; set; }
        public string txtPartId { get; set; }
        public int ProposalSeq { get; set; }

    }
}
