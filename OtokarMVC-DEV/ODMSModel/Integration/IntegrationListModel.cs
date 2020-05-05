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
    public class IntegrationListModel :  BaseListWithPagingModel
    {
        public IntegrationListModel([DataSourceRequest] DataSourceRequest request) : base(request)
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
        public IntegrationListModel()
        {
                
        }

        [Display(Name = "Integration_IntegrationType_Id", ResourceType = typeof(MessageResource))]
        public int? IntegrationTypeId { get; set; }

        [Display(Name = "Integration_Service_Id", ResourceType = typeof(MessageResource))]
        public int ServiceId { get; set; }

        [Display(Name = "Integration_Service_Name", ResourceType = typeof(MessageResource))]
        public string ServiceName { get; set; }

        [Display(Name = "Integration_Service_Description", ResourceType = typeof(MessageResource))]
        public string ServiceDescription { get; set; }

        [Display(Name = "Integration_Last_Call_Date", ResourceType = typeof(MessageResource))]
        public DateTime? LastCallDate { get; set; }

        [Display(Name = "Integration_Is_Active", ResourceType = typeof(MessageResource))]
        public bool? IsActive { get; set; }

        [Display(Name = "Integration_Last_Success_Date", ResourceType = typeof(MessageResource))]
        public DateTime? LastSuccessDate { get; set; }


    }
}
