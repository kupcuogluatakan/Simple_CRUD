using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Resources;
using ODMSModel.Bank;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class BankController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Bank.BankIndex)]
        public ActionResult BankIndex()
        {
            return View();

        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Bank.BankIndex, ODMSCommon.CommonValues.PermissionCodes.Bank.BankCreate)]
        public ActionResult BankCreate()
        {
            return View();
        }

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Bank.BankIndex, ODMSCommon.CommonValues.PermissionCodes.Bank.BankCreate)]
        public ActionResult BankCreate(BankDetailModel model)
        {
            if (ModelState.IsValid)
            {
                var bo = new BankBL();
                model.CommandType = model.Id > 0 ? ODMSCommon.CommonValues.DMLType.Update : ODMSCommon.CommonValues.DMLType.Insert;
                bo.DMLBank(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();
                return View();
            }
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Bank.BankIndex, ODMSCommon.CommonValues.PermissionCodes.Bank.BankUpdate)]
        public ActionResult BankUpdate(int id = 0)
        {
            var referenceModel = new BankDetailModel();
            if (id > 0)
            {
                var bo = new BankBL();
                referenceModel.Id = id;
                referenceModel = bo.GetBank(referenceModel).Model;
            }
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Bank.BankIndex, ODMSCommon.CommonValues.PermissionCodes.Bank.BankUpdate)]
        public ActionResult BankUpdate(BankDetailModel viewModel)
        {
            var bo = new BankBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = viewModel.Id > 0 ? ODMSCommon.CommonValues.DMLType.Update : ODMSCommon.CommonValues.DMLType.Insert;
                bo.DMLBank(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Bank.BankIndex, ODMSCommon.CommonValues.PermissionCodes.Bank.BankDelete)]
        public ActionResult BankDelete(BankDetailModel model)
        {
            ViewBag.HideElements = false;

            var bo = new BankBL();
            model.CommandType = model.Id > 0 ? ODMSCommon.CommonValues.DMLType.Delete : string.Empty;
            bo.DMLBank(UserManager.UserInfo, model);
            //this.CheckErrorForMessage(model, true);
            this.ModelState.Clear();

            if (model.ErrorNo == 0)
                return this.GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return this.GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Bank.BankIndex, ODMSCommon.CommonValues.PermissionCodes.Bank.BankDetails)]
        public ActionResult BankDetails(int id = 0)
        {
            var referenceModel = new BankDetailModel { Id = id };
            var bo = new BankBL();

            var model = bo.GetBank(referenceModel).Model;

            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Bank.BankIndex, ODMSCommon.CommonValues.PermissionCodes.Bank.BankDetails)]
        public ActionResult ListBanks([DataSourceRequest]DataSourceRequest request, BankListModel model)
        {
            var bo = new BankBL();
            var referenceModel = new BankListModel(request) { Code = model.Code, Name = model.Name };
            int totalCnt;
            var returnValue = bo.ListBanks(referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
    }
}
