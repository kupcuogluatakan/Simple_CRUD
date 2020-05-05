using ODMSBusiness;
using ODMSModel.ServiceCallSchedule;

namespace ODMSService.ServiceClasses
{
    internal class ControlMrpService : ServiceBase
    {
        public ControlMrpService(ServiceScheduleModel model)
        {
        }
        public override void Execute()
        {
            var bl = new PurchaseOrderSuggestionDetailBL();
            bl.ControlMrP();
        }
    }
}
