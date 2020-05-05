using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using FluentValidation.Attributes;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.LabourDuration
{
    [Validator(typeof(LabourDurationDetailModelValidator))]
    public class LabourDurationDetailModel : ModelBase
    {
        public int? LabourId { get; set; }

        public List<string> VehicleEngineTypeIdList { get; set; }

        [Display(Name = "Labour_Display_Code", ResourceType = typeof(MessageResource))]
        public string LabourCode { get; set; }

        [Display(Name = "LabourDuration_Display_Labour", ResourceType = typeof(MessageResource))]
        public string LabourName { get; set; }

        [Display(Name = "LabourDuration_Display_LabourDesc", ResourceType = typeof(MessageResource))]
        public string LabourDesc { get; set; }
        
        public string VehicleModelId { get; set; }
        
        public int? VehicleTypeId { get; set; }

        public string VehicleCode { get; set; }

        [Display(Name = "Vehicle_Display_EngineType", ResourceType = typeof(MessageResource))]
        public string EngineType { get; set; }

        [Display(Name = "LabourDuration_Display_VehicleModelName", ResourceType = typeof(MessageResource))]
        public string VehicleModelName { get; set; }
        
        [Display(Name = "LabourDuration_Display_VehicleTypeName", ResourceType = typeof(MessageResource))]
        public string VehicleTypeName { get; set; }
        
        [Display(Name = "LabourDuration_Display_Duration", ResourceType = typeof(MessageResource))]
        public int Duration { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveString
        {
            get
            {
                if (IsActive)
                    return MessageResource.Global_Display_Active;
                return MessageResource.Global_Display_Passive;
            }
        }

        [Display(Name = "LabourDuration_Display_Duration", ResourceType = typeof(MessageResource))]
        public string DurationString
        {
            get { return Convert.ToString(Duration, CultureInfo.InvariantCulture); }
            set
            {
                int val;
                var result = int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out val);
                if (result)
                    Duration = val;
            }
        }

        [Display(Name = "LabourDuration_Display_Labour", ResourceType = typeof(MessageResource))]
        public string LabourIdString
        {
            get { return LabourId == null ? string.Empty : LabourId.GetValue<string>(); }
            set { int val;
                var result = int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out val);
                LabourId = result ? (int?)val : null;
            }
        }

        [Display(Name = "LabourDuration_Display_VehicleTypeName", ResourceType = typeof(MessageResource))]
        public string VehicleTypeIdString
        {
            get { return VehicleTypeId == null ? string.Empty : VehicleTypeId.GetValue<string>(); }
            set
            {
                int val;
                var result = int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out val);
                VehicleTypeId = result ? (int?)val : null;
            }
        }

    }
}
