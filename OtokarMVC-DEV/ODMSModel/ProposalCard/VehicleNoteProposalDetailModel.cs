using System;

namespace ODMSModel.ProposalCard
{
    public class VehicleNoteProposalDetailModel
    {
        public string Note { get; set; }
        public int NoteId { get; set; }
        public DateTime CreateDate { get; set; }
        public int? Next { get; set; }
        public int? Prev { get; set; }
        public int Total { get; set; }

 
    }
}
