using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.PDIVehicleResult
{
    public class PDIVehicleResultListModel : BaseListWithPagingModel
   {

        public PDIVehicleResultListModel(Kendo.Mvc.UI.DataSourceRequest request):base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"VinNo", "M.VIN_NO"},
                    {"StatusName", "M.STATUS_LOOKVAL"},
                    {"EngineNo", "M.ENGINE_NO"},
                    {"TransmissionSerialNo", "M.TRANSMISSION_SERIALNO"},
                    {"DifferentialSerialNo", "M.DIFFERENTAIL_SERIALNO"},
                    {"PDICheckNote", "M.PDI_CHECK_NOTE"},
                    {"ApprovalNote", "M.APPROVAL_NOTE"},
                    {"ApprovalUserName", "U.CUSTOMER_NAME,U.USER_LAST_NAME"},
                    {"ModelKod", "VM.MODEL_KOD"},
                    {"TypeName", "VT.TYPE_NAME"},
                    {"CustomerName", "C.CUSTOMER_NAME"},
                    {"CreateDate", "M.CREATE_DATE"},
                    {"GroupName", "G.GROUP_NAME"}
                };
            SetMapper(dMapper);
        }

        public PDIVehicleResultListModel()
        {

        }
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

        [Display(Name = "PDIVehicleResult_Display_DealerSsId",
           ResourceType = typeof(MessageResource))]
        public string DealerSsId { get; set; }

        public int VehicleId { get; set; }
        [Display(Name = "PDIVehicleResult_Display_VinNo",
            ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }

        public int? StatusId { get; set; }
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
            ResourceType = typeof (MessageResource))]
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

        [Display(Name = "PDIVehicleResult_Display_StartDate",
            ResourceType = typeof(MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "PDIVehicleResult_Display_EndDate",
            ResourceType = typeof(MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_DealerIdList",
    ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }
    }
}
