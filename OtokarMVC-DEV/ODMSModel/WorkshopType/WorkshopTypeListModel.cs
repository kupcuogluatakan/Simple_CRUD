using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
namespace ODMSModel.WorkshopType
{
    public class WorkshopTypeListModel :BaseListWithPagingModel
    {
         public WorkshopTypeListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"WorkshopTypeId", "ID_WORKSHOP_TYPE"},
                     {"WorkshopTypeName", "WORKSHOP_TYPE_NAME"},
                     {"Description", "DESCRIPTION"},
                     {"IsActiveString", "IS_ACTIVE_S"},
                 };
            SetMapper(dMapper);
        }

         public WorkshopTypeListModel()
        {
        }

        
        public int WorkshopTypeId { get; set; }

        [Display(Name = "WorkshopType_Display_Name", ResourceType = typeof(MessageResource))]
        public string WorkshopTypeName { get; set; }

        [Display(Name = "Global_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }


    }
}
