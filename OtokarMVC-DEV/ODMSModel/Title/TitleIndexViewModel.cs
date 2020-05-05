using FluentValidation.Attributes;
using ODMSModel.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Title
{
    [Validator(typeof(TitleIndexModelValidator))]
    public class TitleIndexViewModel : ModelBase
    {

        public TitleIndexViewModel()
        {
            TitleName = new MultiLanguageModel();
        }
        public string MultiLanguageContentAsText { get; set; }
        [Display(Name = "Global_Display_TitleId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int TitleId { get; set; }

        private MultiLanguageModel _titleName;
        [Display(Name = "Global_Display_TitleName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public MultiLanguageModel TitleName { get { return _titleName ?? new MultiLanguageModel(); } set { _titleName = value; } }
        [Display(Name = "Global_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool _Status { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StatusName { get; set; }
        [Display(Name = "Global_Display_IsTechnician", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int IsTechnician { get; set; }
        [Display(Name = "Global_Display_IsTechnician", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsTechnicianName { get; set; }

    }
}
