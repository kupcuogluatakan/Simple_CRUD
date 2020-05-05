using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Terminal.Common
{
    public interface IMediator
    {
        TResponse Request<TRequest, TResponse>(IRequest<TRequest, TResponse> request)
            where TRequest : IRequest<TRequest, TResponse>
            where TResponse : IResponse;

        void Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
