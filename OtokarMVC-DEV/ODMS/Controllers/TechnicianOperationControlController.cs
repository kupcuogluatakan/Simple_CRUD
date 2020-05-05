using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.TechnicianOperationControl;
using System;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class TechnicianOperationControlController : ControllerBase
    {
        private readonly TechnicianOperationControlBL _technicianOperationControlService = new TechnicianOperationControlBL();

        [HttpGet]
        public ActionResult TechnicianOperationControlIndex()
        {
            var viewModel = new TechnicianOperationViewModel();

            if (Session[CommonValues.TechnicionCookieKey] != null)
            {
                viewModel.UserId = Session[CommonValues.TechnicionCookieKey].GetValue<int>();
                viewModel = _technicianOperationControlService.Get(UserManager.UserInfo,viewModel).Model;
            }          

            return PartialView(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TechnicianOperationControlCreate(TechnicianOperationViewModel model)
        {
            if (Session[CommonValues.TechnicionCookieKey] != null)
            {
                model.UserId = Session[CommonValues.TechnicionCookieKey].GetValue<int>();
                
                var checkInDate = _technicianOperationControlService.Get(UserManager.UserInfo, model).Model.CheckInDate;
                if (checkInDate != null)
                    model.CheckInDate = checkInDate.Value.ToString("yyyy-MM-dd HH:mm:ss").GetValue<DateTime>();

                model.DealerId = UserManager.UserInfo.GetUserDealerId();
                model.CommandType = model.ProcessType == ProcessType.CheckIn
                                        ? CommonValues.DMLType.Insert
                                        : CommonValues.DMLType.Update;

                _technicianOperationControlService.Insert(UserManager.UserInfo,model);              
            }

            return Json(model);            
        }

        [HttpPost]
        public ActionResult GetData([DataSourceRequest]DataSourceRequest request)
        {
            var referenceModel = new TechnicianOperationListModel(request);

            if (Session[CommonValues.TechnicionCookieKey] != null)
                referenceModel.UserId = Session[CommonValues.TechnicionCookieKey].GetValue<int>();

            int totalCnt = 0;
            
            return Json(new
            {
                Data = _technicianOperationControlService.List(UserManager.UserInfo,referenceModel, out totalCnt).Data,
                Total = totalCnt
            });
        }
    }
}
