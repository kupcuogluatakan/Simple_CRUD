using System;

namespace ODMSModel.WorkOrderCard
{
    public class VehicleNoteDetailModel
    {
        public string Note { get; set; }
        public int NoteId { get; set; }
        public DateTime CreateDate { get; set; }
        public int? Next { get; set; }
        public int? Prev { get; set; }
        public int Total { get; set; }
    }
}
