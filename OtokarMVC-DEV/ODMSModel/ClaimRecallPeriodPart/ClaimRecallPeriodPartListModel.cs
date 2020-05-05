using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.ClaimRecallPeriodPart
{
    public class ClaimRecallPeriodPartListModel : BaseListWithPagingModel
    {
        public ClaimRecallPeriodPartListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
            {
                {"PartCode", "PART_CODE"},
                {"PartName", "ADMIN_DESC"},
                {"CreateUser", "CREATE_USER"},
                {"CreateDate", "CREATE_DATE"},
                {"UpdateDate", "UPDATE_DATE"},
                {"UpdateUser", "UPDATE_USER"},
                {"IsActiveString", "IS_ACTIVE"},
            };
            SetMapper(dMapper);
        }

        public ClaimRecallPeriodPartListModel()
        {
        }

        //[Display(Name = "ClaimRecallPeriodPart_Display_ClaimRecallPeriodId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        //public int ClaimRecallPeriodId { get; set; }

        public long? PartId { get; set; }
        [Display(Name = "ClaimRecallPeriodPart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "ClaimRecallPeriodPart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "ClaimRecallPeriodPart_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "ClaimRecallPeriodPart_Display_CreateUser", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CreateUser { get; set; }

        [Display(Name = "ClaimRecallPeriodPart_Display_UpdateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? UpdateDate { get; set; }

        [Display(Name = "ClaimRecallPeriodPart_Display_UpdateUser", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string UpdateUser { get; set; }

    }
}
