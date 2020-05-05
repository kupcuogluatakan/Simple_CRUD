using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ODMSModel.ProposalDocuments
{
    public class ProposalDocumentsViewModel : ModelBase
    {
        public bool IsRequestRoot { get; set; }

        public int ProposalDocId { get; set; }

        public int ProposalId { get; set; }
        public int ProposalSeq { get; set; }

        public int DocId { get; set; }

        public HttpPostedFileBase file { get; set; }

        public string DocName { get; set; }

        public string DocMimeType { get; set; }

        public byte[] DocImage { get; set; }

        [Display(Name = "Global_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }
    }
}
