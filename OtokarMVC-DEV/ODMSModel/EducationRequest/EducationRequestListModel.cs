using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.EducationRequest
{
    public class EducationRequestListModel : BaseListWithPagingModel
    {
        public long Id { get; set; }
        
        [Display(Name = "Education_Display_Code", ResourceType = typeof(MessageResource))]
        public string EducationCode { get; set; }

        [Display(Name = "Education_Display_Name", ResourceType = typeof(MessageResource))]
        public string EducationName { get; set; }

        public int WorkerId { get; set; }

        [Display(Name = "Global_Display_WorkerName", ResourceType = typeof(MessageResource))]
        public string WorkerName { get; set; }

        [Display(Name = "EducationRequest_Display_CreateDate", ResourceType = typeof(MessageResource))]
        public DateTime CreateDate { get; set; }

        [Display(Name = "EducationRequest_Display_CreateDate", ResourceType = typeof(MessageResource))]
        public string CreateDateString
        {
            get { return CreateDate.ToShortDateString(); }
        }

        public EducationRequestListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"Id", "ID_EDUCATION_REQUEST"},
                    {"EducationCode", "EDUCATION_CODE"},
                    {"EducationName", "EDUCATION_NAME"},
                    {"WorkerId", "ID_DMS_USER"},
                    {"WorkerName", "WORKER_NAME"},
                    {"CreateDate", "CREATE_DATE"},
                };
            SetMapper(dMapper);

        }

        public EducationRequestListModel()
        {
            
        }
    }
}
