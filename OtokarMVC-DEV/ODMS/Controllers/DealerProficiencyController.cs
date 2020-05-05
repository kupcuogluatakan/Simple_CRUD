using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Proficiency;
using System.Linq;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DealerProficiencyController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Proficiency.ProficiencyIndex)]
        public ActionResult DealerProficiencyIndex(int id = 0)
        {
            ViewBag.DealerId = id;
            return PartialView();
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsIndex, CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsCreate)]
        public ActionResult DealerProficiencyCreate(int dealerId)
        {
            ViewBag.DealerId = dealerId;
            ViewBag.ProficiencyList = ProficiencyBL.ListProficiesAsSelectListItem(UserManager.UserInfo).Data;
            return View(new ProficiencyDetailModel() { DealerId = dealerId });
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsIndex, CommonValues.PermissionCodes.WorkorderInvoicePayments.WorkorderInvoicePaymentsCreate)]
        public ActionResult DealerProficiencyCreate(int dealerId, string proficiencyCode)
        {
            var model = new ProficiencyDetailModel
            {
                DealerId = dealerId,
                ProficiencyCode = proficiencyCode,
                CommandType = CommonValues.DMLType.Insert
            };
            var bo = new ProficiencyBL();
            ViewBag.ProficiencyList = ProficiencyBL.ListProficiesAsSelectListItem(UserManager.UserInfo).Data;
            if (!string.IsNullOrEmpty(proficiencyCode))
            {
                int totalCount = 0;
                ProficiencyListModel listModel = new ProficiencyListModel();
                listModel.DealerId = dealerId;
                List<ProficiencyListModel> list = bo.ListProficienciesOfDealer(UserManager.UserInfo, listModel, out totalCount).Data;
                var control = (from e in list.AsEnumerable()
                               where e.ProficiencyCode == proficiencyCode
                               select e);
                if (control.Any())
                {
                    SetMessage(MessageResource.DealerProficiency_Warning_ProficiencycodeExists, CommonValues.MessageSeverity.Fail);
                    return View(model);
                }
                else
                {
                    bo.DMLDealerProficiency(UserManager.UserInfo, model);
                    CheckErrorForMessage(model, true);
                    ModelState.Clear();
                    SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                }
            }
            else
            {
                SetMessage(MessageResource.DealerProficiency_Display_ProficiencyCodeError, CommonValues.MessageSeverity.Fail);
                return View(model);
            }

            return View(new ProficiencyDetailModel() { DealerId = dealerId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Proficiency.ProficiencyIndex)]
        public ActionResult DealerProficiencyDelete(int dealerId, string proficiencyCode)
        {
            var bo = new ProficiencyBL();
            var model = new ProficiencyDetailModel { DealerId = dealerId, ProficiencyCode = proficiencyCode, CommandType = CommonValues.DMLType.Delete };
            bo.DMLDealerProficiency(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Proficiency.ProficiencyIndex)]
        public ActionResult ListProficienciesOfDealer([DataSourceRequest] DataSourceRequest request, int dealerId)
        {
            int totalCount;

            var bo = new ProficiencyBL();
            var model = new ProficiencyListModel(request) { DealerId = dealerId };
            var result = bo.ListProficienciesOfDealer(UserManager.UserInfo, model, out totalCount).Data;

            return Json(new
            {
                Data = result,
                Total = totalCount
            });
        }

    }
}
