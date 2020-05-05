using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
namespace ODMSModel.WorkOrderDocuments
{
    public class WorkOrderDocumentsListModel : BaseListWithPagingModel
    {
        public WorkOrderDocumentsListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"WorkOrderDocId", "ID_WORK_ORDER_DOC"},
                     {"WorkOrderId", "ID_WORK_ORDER"},
                     {"DocId", "DOC_ID"},
                     {"DocumentName", "DOC_NAME"},
                     {"Description", "TXT_NOTE"},
                     {"IsActiveString","IS_ACTIVE"}
                 };
            SetMapper(dMapper);

        }

        public WorkOrderDocumentsListModel()
        {
        }
        [Display(Name = "WorkOrderDoc_Display_IsActiveName", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
        public bool IsActive { get; set; }

        public int TotalActiveCount { get; set; }

        public  long WorkOrderDocId { get; set; }

        public long WorkOrderId { get; set; }

        public long DocId { get; set; }

        [Display(Name = "WorkOrderDoc_Display_FileName", ResourceType = typeof(MessageResource))]
        public string DocumentName { get; set; }

        [Display(Name = "Global_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

    }
}
