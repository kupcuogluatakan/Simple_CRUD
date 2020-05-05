using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.Equipment
{
    public class EquipmentTypeListModel : BaseListWithPagingModel
    {
        public int EquipmentId { get; set; }

        [Display(Name = "EquipmentViewModel_Display_TypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EquipmentTypeName { get; set; }

        [Display(Name = "EquipmentViewModel_Display_TypeDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EquipmentTypeDesc { get; set; }

        public EquipmentTypeListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"EquipmentId", "EQUIPMENT_TYPE_ID"},
                     {"EquipmentTypeName", "EQUIPMENT_NAME"},
                     {"EquipmentTypeDesc", "DESCRIPTION"}
                 };
            SetMapper(dMapper);
        }

        public EquipmentTypeListModel() { }
    }
}
