namespace ODMSBusiness.Terminal.Common
{
    using System.Transactions;
    public class UnitOfWork:IUnitOfWork
    {
        private readonly TransactionScope _scope;

        public UnitOfWork()
        {
            _scope= new TransactionScope(TransactionScopeOption.RequiresNew);
        }
        public void Complete()
        {
            _scope.Complete();
        }

        public void Dispose()
        {
            _scope.Dispose();   
        }
    }
}
