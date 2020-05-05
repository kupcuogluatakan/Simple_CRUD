using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ViewModel;
using FluentValidation.Attributes;

namespace ODMSModel.WorkshopType
{
    [Validator(typeof(WorkshopTypeModelValidator))]
    public class WorkshopTypeViewModel:ModelBase
    {
        public WorkshopTypeViewModel()
        {
            WorkshopTypelName = new MultiLanguageModel();
        }

        public string MultiLanguageContentAsText { get; set; }

        //[Display(Name = "BodyworkDetail_Display_Code", ResourceType = typeof(MessageResource))]
        public int WorkshopTypeId { get; set; }

        [Display(Name = "Global_Display_Description", ResourceType = typeof(MessageResource))]
        public string Descripion { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        private MultiLanguageModel _workshopTypeName;
        [Display(Name = "Workshop_Display_Name", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel WorkshopTypelName { get { return _workshopTypeName ?? new MultiLanguageModel(); } set { _workshopTypeName = value; } }
    }
}
