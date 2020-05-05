using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalDocuments
{
    public class ProposalDocumentsListModel : BaseListWithPagingModel
    {
        public ProposalDocumentsListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"ProposalDocId", "ID_WORK_ORDER_DOC"},
                     {"ProposalId", "ID_WORK_ORDER"},
                     {"DocId", "DOC_ID"},
                     {"DocumentName", "DOC_NAME"},
                     {"Description", "TXT_NOTE"},
                     {"IsActiveName","IS_ACTIVE"}
                 };
            SetMapper(dMapper);

        }

        public ProposalDocumentsListModel()
        {
        }
        [Display(Name = "WorkOrderDoc_Display_IsActiveName", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
        public bool IsActive { get; set; }

        public int TotalActiveCount { get; set; }

        public long ProposalDocId { get; set; }

        public long ProposalId { get; set; }
        public int ProposalSeq { get; set; }

        public long DocId { get; set; }

        [Display(Name = "WorkOrderDoc_Display_FileName", ResourceType = typeof(MessageResource))]
        public string DocumentName { get; set; }

        [Display(Name = "Global_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

    }
}
