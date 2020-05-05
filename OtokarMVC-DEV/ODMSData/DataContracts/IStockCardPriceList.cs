using ODMSCommon.Security;

namespace ODMSData.DataContracts
{
    public interface IStockCardPriceList<T>
    {        
        T Get(UserInfo user,T model);

        T Select(UserInfo user, T model);       
    }
}
