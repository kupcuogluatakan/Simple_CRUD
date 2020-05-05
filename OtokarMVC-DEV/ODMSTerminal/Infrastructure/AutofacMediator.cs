using Newtonsoft.Json;
using NLog;
using ODMSCommon.Logging;

namespace ODMSTerminal.Infrastructure
{
    using Autofac;
    using ODMSBusiness.Terminal.Common;

    public class AutofacMediator : IMediator
    {
        private readonly ILifetimeScope _lifetimeScope;
        private readonly Loggable _logger;

        public AutofacMediator(ILifetimeScope lifetimeScope, Loggable logger)
        {
            _lifetimeScope = lifetimeScope;
            _logger = logger;
        }

        public TResponse Request<TRequest, TResponse>(IRequest<TRequest, TResponse> request)
            where TRequest : IRequest<TRequest, TResponse>
            where TResponse : IResponse
        {
            var handler = _lifetimeScope.Resolve<IHandleRequest<TRequest, TResponse>>();
            _logger.InfoAsync(JsonConvert.SerializeObject((TRequest)request));
            var response = handler.Handle((TRequest)request);
            _logger.InfoAsync(JsonConvert.SerializeObject(response));
            return response;
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _lifetimeScope.Resolve<IHandleCommand<TCommand>>();
            _logger.InfoAsync(JsonConvert.SerializeObject(command));
            handler.Handle(command);
        }
    }
}