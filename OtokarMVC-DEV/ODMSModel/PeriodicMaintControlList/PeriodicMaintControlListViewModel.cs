using FluentValidation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.PeriodicMaintControlList
{
    [Validator(typeof(PeriodicMaintControlListViewModelValidator))]
    public class PeriodicMaintControlListViewModel : ModelBase
    {
        public PeriodicMaintControlListViewModel()
        {
        }

        public int PeriodicMaintCtrlListId { get; set; }

        [Display(Name = "PeriodicMaintControlList_Display_Idtype", ResourceType = typeof(MessageResource))]
        public int? IdType { get; set; }

        [Display(Name = "PeriodicMaintControlList_Display_Language", ResourceType = typeof(MessageResource))]
        public string LanguageCustom { get; set; }

        [Display(Name = "PeriodicMaintControlList_Display_DocName", ResourceType = typeof(MessageResource))]
        public Int64? DocId { get; set; }

        [Display(Name = "PeriodicMaintControlList_Display_ModelKod", ResourceType = typeof(MessageResource))]
        public string ModelKod { get; set; }

        [Display(Name = "PeriodicMaintControlList_Display_TypeName", ResourceType = typeof(MessageResource))]
        public string TypeName { get; set; }

        [Display(Name = "PeriodicMaintControlList_Display_DocumentDesc", ResourceType = typeof(MessageResource))]
        public string DocumentDesc { get; set; }

        [Display(Name = "PeriodicMaintControlList_Display_DocName", ResourceType = typeof(MessageResource))]
        public string DocName { get; set; }

        [Display(Name = "Vehicle_Display_EngineType", ResourceType = typeof(MessageResource))]
        public string EngineType { get; set; }
    }
}
