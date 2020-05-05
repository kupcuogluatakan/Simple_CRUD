using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System.Collections.Generic;

namespace ODMSModel.GuaranteeAuthorityGroupVehicleModels
{
    public class GuaranteeAuthorityGroupVehicleListModel : BaseListWithPagingModel
    {
        public GuaranteeAuthorityGroupVehicleListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"IdGrpoup", "ID_GROUP"},
                    {"ModelKod","MODEL_KOD"}
                };
            SetMapper(dMapper);
        }

        public GuaranteeAuthorityGroupVehicleListModel()
        {
        }
    
        public int? IdGroup { get; set; }

        public string ModelKod { get; set; }

        public string ModelName { get; set; }
    }
}
