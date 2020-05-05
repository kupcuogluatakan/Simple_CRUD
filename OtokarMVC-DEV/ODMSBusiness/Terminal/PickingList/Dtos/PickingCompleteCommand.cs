using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.PickingList.Dtos
{
    public class PickingCompleteCommand:ICommand
    {
        public long PickingId { get; }
        public int DealerId { get; }
        public int OperatingUser { get; }
        public string LanguageCode { get; }

        public PickingCompleteCommand(long pickingId,int dealerId,int operatingUser,string languageCode)
        {
            PickingId = pickingId;
            DealerId = dealerId;
            OperatingUser = operatingUser;
            LanguageCode = languageCode;
        }
    }
}
