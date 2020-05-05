using System;

namespace ODMSBusiness.Terminal.Common
{
    public interface IUnitOfWork:IDisposable
    {
        void Complete();
    }
}
