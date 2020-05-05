using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Labour
{
    public class LabourAndLabourDetListModel
    {
        [Display(Name = "LabourAndLabourDet_LABOUR_CODE", ResourceType = typeof(MessageResource))]
        public string LABOUR_CODE { get; set; }
        [Display(Name = "LabourAndLabourDet_LABOUR_NAME", ResourceType = typeof(MessageResource))]
        public string LABOUR_NAME { get; set; }
        [Display(Name = "LabourAndLabourDet_LABOUR_SSID", ResourceType = typeof(MessageResource))]
        public string LABOUR_SSID { get; set; }
        [Display(Name = "LabourAndLabourDet_LABOUR_GROUP_DESC", ResourceType = typeof(MessageResource))]
        public string LABOUR_GROUP_DESC { get; set; }
        [Display(Name = "LabourAndLabourDet_LABOUR_SUBGROUP_DESC", ResourceType = typeof(MessageResource))]
        public string LABOUR_SUBGROUP_DESC { get; set; }
        [Display(Name = "LabourAndLabourDet_LABOUR_REPAIR_CODE", ResourceType = typeof(MessageResource))]
        public string LABOUR_REPAIR_CODE { get; set; }
        [Display(Name = "LabourAndLabourDet_LABOUR_TYPE_NAME", ResourceType = typeof(MessageResource))]
        public string LABOUR_TYPE_NAME { get; set; }
        [Display(Name = "LabourAndLabourDet_MODEL_KOD", ResourceType = typeof(MessageResource))]
        public string MODEL_KOD { get; set; }
        [Display(Name = "LabourAndLabourDet_DURATION", ResourceType = typeof(MessageResource))]
        public string DURATION { get; set; }
        [Display(Name = "LabourAndLabourDet_IS_ACTIVE", ResourceType = typeof(MessageResource))]
        public string IS_ACTIVE { get; set; }
        [Display(Name = "LabourAndLabourDet_MODEL_NAME", ResourceType = typeof(MessageResource))]
        public string MODEL_NAME { get; set; }
        [Display(Name = "LabourAndLabourDet_TYPE_NAME", ResourceType = typeof(MessageResource))]
        public string TYPE_NAME { get; set; }
        [Display(Name = "LabourAndLabourDet_ENGINE_TYPE", ResourceType = typeof(MessageResource))]
        public string ENGINE_TYPE { get; set; }
        [Display(Name = "LabourAndLabourDet_IS_ACTIVE_STRING", ResourceType = typeof(MessageResource))]
        public string IS_ACTIVE_STRING { get; set; }

    }
}
