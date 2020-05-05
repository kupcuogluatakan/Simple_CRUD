namespace ODMSBusiness.Terminal.Common
{
    public interface IHandleRequest<TRequest, TResponse>
         where TRequest : IRequest<TRequest, TResponse>
         where TResponse : IResponse
    {
        TResponse Handle(TRequest request);
    }
}
