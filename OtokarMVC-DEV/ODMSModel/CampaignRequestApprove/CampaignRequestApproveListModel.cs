using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.CampaignRequestApprove
{
    public class CampaignRequestApproveListModel : BaseListWithPagingModel
    {
        public CampaignRequestApproveListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CampaignRequestId", "CAMPAIGN_REQUEST_ID"},
                    {"RequestDealerName", "RD.DEALER_NAME"},
                    {"SupplierDealerName", "SD.DEALER_NAME"},
                    {"ModelKod", "MODEL_KOD"},
                    {"CampaignCode", "CAMPAIGN_CODE"},
                    {"CampaignName", "CAMPAIGN_NAME"},
                    {"Quantity", "QUANTITY"},
                    {"RequestStatusName", "REQUEST_STATUS"},
                    {"SupplierTypeName","SUPPLIER_TYPE"},
                    {"RequestNote", "REQUEST_NOTE"},
                    {"VinCodes", "VIN_CODES"},
                    {"UpdateUserName","UPDATE_USER_NAME" }
                };
            SetMapper(dMapper);
        }

        public CampaignRequestApproveListModel()
        {
            
        }
        
        [Display(Name = "CampaignRequestApprove_Display_CampaignRequestId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int CampaignRequestId { get; set; }

        public int? RequestDealerId { get; set; }
        [Display(Name = "CampaignRequestApprove_Display_RequestDealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RequestDealerName { get; set; }
        
        public int? SupplierDealerId { get; set; }
        [Display(Name = "CampaignRequestApprove_Display_SupplierDealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SupplierDealerName { get; set; }
        
        [Display(Name = "CampaignRequestApprove_Display_ModelKod", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ModelKod { get; set; }

        [Display(Name = "CampaignRequestApprove_Display_CampaignCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignCode { get; set; }
        [Display(Name = "CampaignRequestApprove_Display_CampaignName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignName { get; set; }
        
        [Display(Name = "CampaignRequestApprove_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? Quantity { get; set; }

        public int? RequestStatusId { get; set; }
        [Display(Name = "CampaignRequestApprove_Display_RequestStatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RequestStatusName { get; set; }

        public int? SupplierTypeId  { get; set; }
        [Display(Name = "CampaignRequestApprove_Display_SupplierTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SupplierTypeName { get; set; }

        [Display(Name = "CampaignRequestApprove_Display_RequestNote", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RequestNote { get; set; }

        [Display(Name = "CampaignRequest_Display_VinNumbers", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinCodes { get; set; }

        public string ApprovedVinCodes { get; set; }
        public string ApprovedIdCampaignRequest { get; set; }


        public string RejectionNote { get; set; }

        [Display(Name = "CampaignRequestApprove_Display_UpdateUser", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string UpdateUserName { get; set; }

    }
}
