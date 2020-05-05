using System;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using FluentValidation.Attributes;

namespace ODMSModel.PDIVehicleResult
{
    [Validator(typeof(PDIVehicleResultViewModelValidator))]
    public class PDIVehicleResultViewModel : ModelBase
    {
        public int PDIVehicleResultId { get; set; }

        [Display(Name = "PDIVehicleResult_Display_WorkOrderDetail",
            ResourceType = typeof(MessageResource))]
        public int WorkOrderId { get; set; }

        [Display(Name = "PDIVehicleResult_Display_WorkOrderDetail",
            ResourceType = typeof(MessageResource))]
        public int WorkOrderDetailId { get; set; }

        public int DealerId { get; set; }
        [Display(Name = "PDIVehicleResult_Display_DealerName",
            ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        public int VehicleId { get; set; }
        [Display(Name = "PDIVehicleResult_Display_VinNo",
            ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }

        public new int StatusId { get; set; }
        [Display(Name = "PDIVehicleResult_Display_StatusName",
            ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }

        [Display(Name = "PDIVehicleResult_Display_EngineNo",
            ResourceType = typeof(MessageResource))]
        public string EngineNo { get; set; }

        [Display(Name = "PDIVehicleResult_Display_TransmissionSerialNo",
            ResourceType = typeof(MessageResource))]
        public string TransmissionSerialNo { get; set; }

        [Display(Name = "PDIVehicleResult_Display_DifferentialSerialNo",
            ResourceType = typeof(MessageResource))]
        public string DifferentialSerialNo { get; set; }

        [Display(Name = "PDIVehicleResult_Display_PDICheckNote",
            ResourceType = typeof(MessageResource))]
        public string PDICheckNote { get; set; }

        [Display(Name = "PDIVehicleResult_Display_ApprovalNote",
            ResourceType = typeof(MessageResource))]
        public string ApprovalNote { get; set; }

        public int? ApprovalUserId { get; set; }
        [Display(Name = "PDIVehicleResult_Display_ApprovalUserName",
            ResourceType = typeof(MessageResource))]
        public string ApprovalUserName { get; set; }

        [Display(Name = "PDIVehicleResult_Display_CreateDate",
            ResourceType = typeof(MessageResource))]
        public DateTime CreateDate { get; set; }

        public int? GroupId { get; set; }
        [Display(Name = "PDIVehicleResult_Display_GroupName",
            ResourceType = typeof(MessageResource))]
        public string GroupName { get; set; }

        public int? TypeId { get; set; }
        [Display(Name = "PDIVehicleResult_Display_TypeName",
            ResourceType = typeof(MessageResource))]
        public string TypeName { get; set; }

        [Display(Name = "PDIVehicleResult_Display_ModelKod",
            ResourceType = typeof(MessageResource))]
        public string ModelKod { get; set; }

        public int CustomerId { get; set; }
        [Display(Name = "PDIVehicleResult_Display_CustomerName",
            ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }
    }

}
