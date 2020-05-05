using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.PickingList.Dtos;
using ODMSCommon.Exception;
using ODMSModel.WorkOrderPicking;
using ODMSCommon.Security;

namespace ODMSBusiness.Terminal.PickingList.Handlers
{
    public class PickingCompleteCommandHandler:IHandleCommand<PickingCompleteCommand>
    {
        private readonly WorkOrderPickingDetailBL _bus;

        public PickingCompleteCommandHandler(WorkOrderPickingDetailBL bus)
        {
            _bus = bus;
        }

        public void Handle(PickingCompleteCommand command)
        {
            var model = new WorkOrderPickingViewModel {WorkOrderPickingId = command.PickingId};
            _bus.CompleteWorkOrderPicking(UserManager.UserInfo, model);
            if (model.ErrorNo > 0)
                throw new ODMSException(model.ErrorMessage);
        }
    }
}
