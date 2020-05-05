using System.Collections.Generic;

namespace ODMSModel.GuaranteeRequestApproveDetail
{
    public class GuaranteePartsLabourViewModel:ModelBase
    {
        public List<GuaranteePartsListModel> ListModelParts { get; set; }
        public List<GuaranteeLaboursListModel> ListModelLabour { get; set; }
    }
}
