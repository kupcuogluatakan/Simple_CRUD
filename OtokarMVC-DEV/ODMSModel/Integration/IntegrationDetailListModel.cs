using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Integration
{
    public class IntegrationDetailListModel : BaseListWithPagingModel
    {
        public IntegrationDetailListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            //var dMapper = new Dictionary<string, string>
            //     {
            //         {"MaintId", "ID_MAINT"},
            //         {"MaintName", "MAINT_NAME"},
            //         {"VehicleTypeName", "TYPE_NAME"},
            //         {"EngineType", "ENGINE_TYPE"},
            //         {"MaintTypeName", "MAINT_TYPE_NAME"},
            //         {"MaintMonth", "MAINT_MONTH"},
            //         {"MaintKM", "MAINT_KM"},
            //         {"IsActiveS", "ACTIVITY_STATUS"},
            //         {"MainCategoryName","MAIN_CATEGORY_NAME"},
            //         {"CategoryName","AICL.DESCRIPTION"},
            //         {"SubCategoryName","AISCL.DESCRIPTION"},
            //         {"FailureCode","AIFC.CODE"}
            //     };
            //SetMapper(dMapper);
        }
        public IntegrationDetailListModel()
        {

        }

        [Display(Name = "Integration_IntegrationType_Id", ResourceType = typeof(MessageResource))]
        public int? IntegrationTypeId { get; set; }

        [Display(Name = "Integration_Log_Id", ResourceType = typeof(MessageResource))]
        public int? LogId { get; set; }

        [Display(Name = "Integration_Request", ResourceType = typeof(MessageResource))]
        public string RequestParams { get; set; }

        [Display(Name = "Integration_Response", ResourceType = typeof(MessageResource))]
        public string Response { get; set; }

        [Display(Name = "Integration_Call_Start", ResourceType = typeof(MessageResource))]
        public DateTime CallStartDate { get; set; }

        [Display(Name = "Integration_Call_Finish", ResourceType = typeof(MessageResource))]
        public DateTime CallFinishDate { get; set; }

        [Display(Name = "Integration_Error_Content", ResourceType = typeof(MessageResource))]
        public string ErrorContent { get; set; }

        [Display(Name = "Integration_Error", ResourceType = typeof(MessageResource))]
        public string Error { get; set; }

    }
}
