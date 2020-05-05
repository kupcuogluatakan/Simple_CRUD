using System.Web.Mvc;
using ODMSBusiness.Terminal.Common;

namespace ODMSTerminal.Infrastructure.MvcActionFilters
{
    public class TransactionalAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionalAttribute()
        {
            _unitOfWork = new UnitOfWork();

        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (filterContext.Exception == null || filterContext.ExceptionHandled)
            {
                _unitOfWork.Complete();
            }

            _unitOfWork.Dispose();

        }
    }
}