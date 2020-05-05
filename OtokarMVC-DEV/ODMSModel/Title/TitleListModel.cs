using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Title
{
    public class TitleListModel : BaseListWithPagingModel
    {
        public TitleListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {

        }
        public TitleListModel()
        {

        }
        [Display(Name = "Global_Display_TitleId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int TitleId { get; set; }
        [Display(Name = "Global_Display_TitleName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TitleName { get; set; }
        [Display(Name = "Global_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int IsActive { get; set; }
        [Display(Name = "Global_Display_TitleName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MultiLanguageContentAsText { get; set; }
        [Display(Name = "Global_Display_IsTechnician", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int IsTechnician { get; set; }
        [Display(Name = "Global_Display_IsTechnician", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsTechnicianName { get; set; }
    }
}
