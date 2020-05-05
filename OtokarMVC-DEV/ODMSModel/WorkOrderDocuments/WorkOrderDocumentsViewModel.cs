using System.ComponentModel.DataAnnotations;
using System.Web;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.WorkOrderDocuments
{
    [Validator(typeof(WorkOrderDocumentsViewModelValidator))]
    public class WorkOrderDocumentsViewModel : ModelBase
    {
        public bool IsRequestRoot { get; set; }

        public int WorkOrderDocId { get; set; }

        public int WorkOrderId { get; set; }

        public int DocId { get; set; }

        public HttpPostedFileBase file  { get; set; }

        public string DocName { get; set; }

        public string DocMimeType { get; set; }

        public byte[] DocImage { get; set; }

        [Display(Name = "Global_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }
    }
}
