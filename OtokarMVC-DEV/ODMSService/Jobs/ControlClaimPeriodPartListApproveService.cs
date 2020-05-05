using ODMSBusiness;
using ODMSModel.ServiceCallSchedule;

namespace ODMSService.ServiceClasses
{
    internal class ControlClaimPeriodPartListApproveService : ServiceBase
    {
        public ControlClaimPeriodPartListApproveService(ServiceScheduleModel model)
        {
        }

        public override void Execute()
        {
            var bl = new ClaimRecallPeriodBL();
            bl.ControlClaimPeriodPartListApprove();
        }
    }
}
