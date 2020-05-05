namespace ODMSBusiness.Terminal.Common
{
    public interface IHandleCommand<TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}
