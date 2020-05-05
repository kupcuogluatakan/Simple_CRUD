using System;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.GeneralParameter;
using Permission=ODMSCommon.CommonValues.PermissionCodes.GeneralParameter;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class GeneralParameterController : ControllerBase
    {
        #region GeneralParameter Index

        [HttpGet]
        [AuthorizationFilter(Permission.GeneralParameterIndex)]
        public ActionResult GeneralParameterIndex()
        {
            return View();
        }
       

        [HttpPost]
        [AuthorizationFilter(Permission.GeneralParameterIndex)]
        public ActionResult ListGeneralParameters([DataSourceRequest]DataSourceRequest request)
        {
            var bus = new GeneralParameterBL();
            var model = new GeneralParameterListModel(request);
            var totalCnt = 0;
            var returnValue = bus.ListGeneralParameters(model, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion


        #region General Parameter Update

        [HttpGet]
        [AuthorizationFilter(Permission.GeneralParameterIndex, Permission.GeneralParameterUpdate)]
        public ActionResult GeneralParameterUpdate(string id)
        {
            return View(new GeneralParameterBL().GetGeneralParameter(id).Model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.GeneralParameterIndex, Permission.GeneralParameterUpdate)]
        [ValidateAntiForgeryToken]
        public ActionResult GeneralParameterUpdate(GeneralParameterViewModel model,bool? BoolValue, DateTime? DateValue)
        {
            if (ModelState.IsValid == false)
                return View(model);
            if (model.Value == null)
            {
                switch (model.Type)
                {
                    case "B":
                        model.Value = BoolValue.ToString();
                        break;
                    case "D":
                        if (DateValue.HasValue == false)
                        {
                            SetMessage(MessageResource.GeneralParameter_Warning_Date, CommonValues.MessageSeverity.Fail);
                            return View(model);
                        }
                        model.Value = DateValue.Value.ToShortDateString();
                        break;
                    default:
                        if (string.IsNullOrEmpty(model.Value))
                        {
                            SetMessage(MessageResource.GeneralParameter_Warning_Value, CommonValues.MessageSeverity.Fail);
                            return View(model);
                        }
                        break;
                }
            }
            

            var bus = new GeneralParameterBL();
            model.CommandType = CommonValues.DMLType.Update;
            bus.DMLGeneralParameter(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            return View(model);
        }

        #endregion

    }
}
