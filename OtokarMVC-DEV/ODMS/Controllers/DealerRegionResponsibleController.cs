using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.DealerRegionResponsible;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DealerRegionResponsibleController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerRegionResponsible.DealerRegionResponsibleIndex)]
        public ActionResult DealerRegionResponsibleIndex()
        {
            var bo = new DealerRegionResponsibleBL();
            var model = bo.GetDealerRegionResponsibleIndexModel().Model;
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerRegionResponsible.DealerRegionResponsibleIndex, CommonValues.PermissionCodes.DealerRegionResponsible.DealerRegionResponsibleCreate)]
        public ActionResult DealerRegionResponsibleCreate()
        {
            ViewBag.DealerRegionList = DealerRegionResponsibleBL.GetDealerRegionList().Data;
            ViewBag.UserList = DealerRegionResponsibleBL.GetUserList().Data;
            var model = new DealerRegionResponsibleDetailModel {IsActive = true};
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerRegionResponsible.DealerRegionResponsibleIndex, CommonValues.PermissionCodes.DealerRegionResponsible.DealerRegionResponsibleCreate)]
        public ActionResult DealerRegionResponsibleCreate(DealerRegionResponsibleDetailModel model)
        {
            ViewBag.DealerRegionList = DealerRegionResponsibleBL.GetDealerRegionList().Data;
            ViewBag.UserList = DealerRegionResponsibleBL.GetUserList().Data;
            if (ModelState.IsValid)
            {
                var bo = new DealerRegionResponsibleBL();
                DealerRegionResponsibleDetailModel modelExists = new DealerRegionResponsibleDetailModel();
                modelExists.DealerRegionId = model.DealerRegionId;
                modelExists.UserId = model.UserId;
                modelExists = bo.GetDealerRegionResponsible(modelExists).Model;
                if (modelExists.DealerRegionName == null)
                {
                    model.CommandType = CommonValues.DMLType.Insert;
                    bo.DMLDealerRegionResponsible(UserManager.UserInfo, model);
                    CheckErrorForMessage(model, true);
                    ModelState.Clear();
                    var newModel = new DealerRegionResponsibleDetailModel { IsActive = true };
                    return View(newModel);
                }
                else
                {
                    SetMessage(MessageResource.DealerRegionResponsible_Warning_DataExists, CommonValues.MessageSeverity.Fail);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerRegionResponsible.DealerRegionResponsibleIndex, CommonValues.PermissionCodes.DealerRegionResponsible.DealerRegionResponsibleDelete)]
        public ActionResult DealerRegionResponsibleDelete(int dealerRegionId, int userId)
        {
            ViewBag.HideElements = false;

            var bo = new DealerRegionResponsibleBL();
            var model = new DealerRegionResponsibleDetailModel
                {
                    DealerRegionId = dealerRegionId,
                    UserId = userId,
                    CommandType = CommonValues.DMLType.Delete
                };
            bo.DMLDealerRegionResponsible(UserManager.UserInfo, model);
            //CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerRegionResponsible.DealerRegionResponsibleIndex, CommonValues.PermissionCodes.DealerRegionResponsible.DealerRegionResponsibleDetails)]
        public ActionResult DealerRegionResponsibleDetails(int dealerRegionId, int userId)
        {
            var referenceModel = new DealerRegionResponsibleDetailModel { DealerRegionId = dealerRegionId, UserId = userId};
            var bo = new DealerRegionResponsibleBL();

            var model = bo.GetDealerRegionResponsible(referenceModel).Model;

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerRegionResponsible.DealerRegionResponsibleIndex, CommonValues.PermissionCodes.DealerRegionResponsible.DealerRegionResponsibleDetails)]
        public ActionResult ListDealerRegionResponsibles([DataSourceRequest]DataSourceRequest request, DealerRegionResponsibleListModel model)
        {
            var bo = new DealerRegionResponsibleBL();
            var referenceModel = new DealerRegionResponsibleListModel(request) { DealerRegionId = model.DealerRegionId, Name = model.Name, Surname = model.Surname, Phone = model.Phone, Email = model.Email };
            int totalCnt;
            var returnValue = bo.ListDealerRegionResponsibles(referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
    }
}
