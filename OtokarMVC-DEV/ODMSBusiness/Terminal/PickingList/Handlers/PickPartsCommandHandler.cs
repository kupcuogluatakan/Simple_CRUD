using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.PickingList.Dtos;
using ODMSCommon;
using ODMSCommon.Exception;
using ODMSModel.WorkOrderPickingDetail;
using ODMSCommon.Security;

namespace ODMSBusiness.Terminal.PickingList.Handlers
{
    public class PickPartsCommandHandler:IHandleCommand<PickPartsCommand>
    {
        private readonly WorkOrderPickingDetailBL _bus;

        public PickPartsCommandHandler(WorkOrderPickingDetailBL bus)
        {
            _bus = bus;
        }

        public void Handle(PickPartsCommand command)
        {
            var model = new WOPDetSubViewModel();
            model.ListSubModel= new List<WOPDetSubListModel>();
            int total;
            var resultList = _bus.ListWorkOrderPickingDetailSub(UserManager.UserInfo, new WOPDetSubListModel()
            {
                WOPDetId = command.Model.PickingDetailId 
            }, out total).Data;

            foreach (var item in command.Model.StockRackList.Where(c=>c.PickQuantity>0))
            {

                int resultId = -1;
                var result=resultList?.FirstOrDefault(c => c.WOPDetId == command.Model.PickingDetailId && c.Value==item.RackId.ToString());
                if (result != null)
                    resultId = result.ResultId;

                model.ListSubModel.Add(new WOPDetSubListModel()
                {
                    ResultId =resultId,
                    Quantity = item.PickQuantity,
                    WOPDetId = command.Model.PickingDetailId,
                    Value = item.RackId.ToString(),

                });
            }

            _bus.DMLWOPDetSub(UserManager.UserInfo, model);

            if (model.ErrorNo > 0)
                throw new ODMSException(model.ErrorMessage);

        }
    }
}
