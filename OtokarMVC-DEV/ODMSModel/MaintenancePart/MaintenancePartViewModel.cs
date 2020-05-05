using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.Validation;
using FluentValidation.Attributes;
using ODMSModel.ViewModel;

namespace ODMSModel.MaintenancePart
{
    [Validator(typeof(MaintenancePartViewModelValidator))]
    public class MaintenancePartViewModel : ModelBase
    {
        public MaintenancePartViewModel()
        {
            PartSearch = new AutocompleteSearchViewModel();
        }

        public bool IsRequestRoot { get; set; }

        public bool HideFormElements { get; set; }

        [Display(Name = "Maintenance_Display_Name", ResourceType = typeof(MessageResource))]
        public int MaintId { get; set; }

        [Display(Name = "Maintenance_Display_Name", ResourceType = typeof(MessageResource))]
        public string MaintName { get; set; }

         [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public int? PartId { get; set; }

        [Display(Name = "SparePart_Display_PartCodeName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public AutocompleteSearchViewModel PartSearch { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "MaintenancePart_Display_IsAlternAllow", ResourceType = typeof(MessageResource))]
        public bool IsAlternateAllow { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "MaintenancePart_Display_IsDifBrandAllow", ResourceType = typeof(MessageResource))]
        public bool IsDifBrandAllow { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "MaintenancePart_Display_IsMust", ResourceType = typeof(MessageResource))]
        public bool IsMust { get; set; }

        [Display(Name = "MaintenancePart_Display_Quantity", ResourceType = typeof(MessageResource))]
        public decimal? Quantity { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? Search { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveString { get; set; }
    }
}
