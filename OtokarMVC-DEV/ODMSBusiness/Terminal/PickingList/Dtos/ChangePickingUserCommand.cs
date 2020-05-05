using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.PickingList.Dtos
{
    public class ChangePickingUserCommand:ICommand
    {
        public long PickingId { get; }
        public int DealerId { get; }
        public int UserId { get; }

        public ChangePickingUserCommand(long pickingId,int dealerId,int userId)
        {
            PickingId = pickingId;
            DealerId = dealerId;
            UserId = userId;
        }
    }
}
