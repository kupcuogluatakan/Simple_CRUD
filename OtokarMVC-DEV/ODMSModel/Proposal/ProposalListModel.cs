using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Proposal
{
    public class ProposalListModel: BaseListWithPagingModel
    {
        public ProposalListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"CustomerName", "CUSTOMER_ID"},
                     {"VehiclePlate", "PLATE"},
                     {"ProposalDate", "PROPOSAL_DATE"},
                     {"DealerName", "ID_DEALER"},
                     {"ProposalStatus","STATUS_DESC"},
                     {"VehicleCode","V_CODE_KOD"},
                     {"VinNo","VIN_NO"},
                     {"EngineNo","ENGINE_NO"},
                     {"VehicleModel","VEHICLE_MODEL"},
                     {"WarrantyStartDate","WARRANTY_START_DATE"},
                     {"WarrantyEndDate","WARRANTY_END_DATE"},
                     {"WarrantyEndKilometer","WARRANTY_END_KM"},


                 };
            SetMapper(dMapper);

        }

        public ProposalListModel() { }
        [Display(Name = "User_Display_ProposalId", ResourceType = typeof(MessageResource))]
        public long ProposalId { get; set; }
        [Display(Name = "User_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "Appointment_Display_VehiclePlate", ResourceType = typeof(MessageResource))]
        public string VehiclePlate { get; set; }
        [Display(Name = "Proposal_Display_ProposalDate", ResourceType = typeof(MessageResource))]
        public DateTime ProposalDate { get; set; }
        [Display(Name = "Proposal_Display_ProposalStatus", ResourceType = typeof(MessageResource))]
        public String ProposalStatus { get; set; }
        [Display(Name = "Proposal_Display_ProposalStatus", ResourceType = typeof(MessageResource))]
        public string ProposalStatusId { get; set; }

        public int VehicleId { get; set; }
        [Display(Name = "Vehicle_Display_VehicleCode", ResourceType = typeof(MessageResource))]
        public string VehicleCode { get; set; }
        [Display(Name = "LabourDuration_Display_VehicleModelName", ResourceType = typeof(MessageResource))]
        public string VehicleModel { get; set; }
        [Display(Name = "Appointment_Display_VehicleType", ResourceType = typeof(MessageResource))]
        public string VehicleType { get; set; }
        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "Vehicle_Display_EngineNo", ResourceType = typeof(MessageResource))]
        public string EngineNo { get; set; }
        [Display(Name = "Vehicle_Display_WarrantyStartDate", ResourceType = typeof(MessageResource))]
        public DateTime WarrantyStartDate { get; set; }
        [Display(Name = "Vehicle_Display_WarrantyEndDate", ResourceType = typeof(MessageResource))]
        public DateTime WarrantyEndDate { get; set; }
        [Display(Name = "Vehicle_Display_WarrantyEndKilometer", ResourceType = typeof(MessageResource))]
        public string WarrantyEndKilometer { get; set; }

        [Display(Name = "GuaranteeReport_DealerSSID", ResourceType = typeof(MessageResource))]
        public string DealerSSID { get; set; }

        public int? DealerId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Display(Name = "Proposal_Display_IsConvert", ResourceType = typeof(MessageResource))]
        public bool IsConvert { get; set; }

        [Display(Name = "Proposal_Display_IsConvert", ResourceType = typeof(MessageResource))]
        public string IsConvertText { get; set; }

        [Display(Name = "Proposal_WorkOrderAndSale_No", ResourceType = typeof(MessageResource))]
        public string WorkOrderId { get; set; }
        [Display(Name = "Proposal_ProposalSeq", ResourceType = typeof(MessageResource))]
        public int ProposalSeq { get; set; }

        public long SparePartSaleId { get; set; }
    }
}
