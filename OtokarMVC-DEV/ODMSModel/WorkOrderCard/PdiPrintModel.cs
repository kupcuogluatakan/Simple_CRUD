using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.WorkOrderCard
{
    public class PdiPrintModel
    {
        [Display(Name="WorkOrderCard_Display_TransmissionSerialNo",ResourceType =typeof(MessageResource))]
        public string TransmissionNo { get; set; }
        [Display(Name = "WorkOrderCard_Display_DifferencialSerialNo", ResourceType = typeof(MessageResource))]
        public string DifferentialNo { get; set; }
        [Display(Name = "Global_Display_Service", ResourceType = typeof(MessageResource))]
        public string Dealer { get; set; }
        [Display(Name = "Vehicle_Display_EngineNo", ResourceType = typeof(MessageResource))]
        public string EngineNo{ get; set; }
        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "WorkOrderCard_Pdi_CreateDate", ResourceType = typeof(MessageResource))]
        public DateTime PdiCreateDate { get; set; }
        [Display(Name = "WorkOrderCard_Pdi_Sender", ResourceType = typeof(MessageResource))]
        public string Sender { get; set; }
        [Display(Name = "WorkOrderCard_Pdi_Approver", ResourceType = typeof(MessageResource))]
        public string Approver { get; set; }
        [Display(Name = "WorkOrderCard_Display_PdiCheckNote", ResourceType = typeof(MessageResource))]
        public string SenderNote  { get; set; }
        [Display(Name = "WorkOrderCard_Pdi_ApproverNote", ResourceType = typeof(MessageResource))]
        public string ApproverNote { get; set; }
        [Display(Name = "WorkOrderCard_Pdi_Status", ResourceType = typeof(MessageResource))]
        public string Status { get; set; }

        public List<PdiResultItem> Items { get; set; }

        public bool IsControlled { get; set; }

        public PdiPrintModel()
        {
            Items= new List<PdiResultItem>();
        }

    }
}
