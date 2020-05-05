using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.CycleCount
{
    public class CycleCountListModel : BaseListWithPagingModel
    {
        public CycleCountListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CycleCountId" , "ID_CYCLE_COUNT"},
                    {"CycleCountName","CYCLE_COUNT_NAME"},
                    {"CycleCountStatus","CYCLE_COUNT_STATUS"},
                    {"StockTypeName","ID_sTOCK_TYPE"},
                    {"StartDate","START_DATE"},
                    {"EndDate","END_DATE"},
                    {"ConfirmDate","CONFIRM_DATE"}
                };
            SetMapper(dMapper);
        }

        public CycleCountListModel()
        {
        }

        [Display(Name = "CycleCount_Display_CycleCountId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int CycleCountId { get; set; }

        [Display(Name = "CycleCount_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CycleCountName { get; set; }

        public int? CycleCountStatus { get; set; }

        [Display(Name = "CycleCount_Display_CycleCountStatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CycleCountStatusName { get; set; }

        [Display(Name = "CycleCount_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "CycleCount_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "CycleCount_Display_ConfirmDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? ConfirmDate { get; set; }
        public int? StockTypeId { get; set; }

        [Display(Name = "CycleCount_Display_StockTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockTypeName { get; set; }
    }
}