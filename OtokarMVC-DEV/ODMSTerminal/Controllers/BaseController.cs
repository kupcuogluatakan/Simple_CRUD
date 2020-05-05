using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using NLog;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.ViewModel;

namespace ODMSTerminal.Controllers
{
    public class BaseController : Controller
    {
        protected enum AsynOperationStatus
        {
            Error = 0,
            Success = 1
        }

        public BaseController()
        {
            ClearTempData();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            GlobalDiagnosticsContext.Set("SessionId", filterContext.HttpContext.Session?.SessionID);
            GlobalDiagnosticsContext.Set("RequestId", Guid.NewGuid().ToString());
            GlobalDiagnosticsContext.Set("Controller", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName);
            GlobalDiagnosticsContext.Set("Action", filterContext.ActionDescriptor.ActionName);
            base.OnActionExecuting(filterContext);
        }

        public void ClearTempData()
        {
            TempData.Remove("MessageToShow");
            TempData.Remove("IsError");
        }

        public bool CheckErrorForMessage(ODMSModel.ModelBase targetModel, bool showSuccessAsDefault)
        {
            var retVal = new MessageHolderViewModel();
            if (targetModel.ErrorNo > 0)
            {
                retVal.ResultMessage = targetModel.ErrorMessage;
                retVal.IsError = true;
            }
            else if (showSuccessAsDefault)
            {
                retVal.ResultMessage = MessageResource.Global_Display_Success;
                retVal.IsError = false;
            }
            TempData["MessageToShow"] = retVal.ResultMessage;
            TempData["IsError"] = retVal.IsError;
            //ViewBag.MessageToShow = retVal;
            return retVal.IsError;

        }
        public bool CheckMultilanguage(string isValidText)
        {
            if (string.IsNullOrEmpty(isValidText) || isValidText == "0")
            {
                TempData["MessageToShow"] = "Çoklu dil içeriğinde, Türkçe boş olamaz. Lütfen Türkçe içeriği doldurup tekrar deneyin.";
                TempData["IsError"] = true;
                return false;
            }
            else
                return true;

        }
        public void SetMessage(string errorMessage, CommonValues.MessageSeverity severity)
        {
            TempData["MessageToShow"] = errorMessage;
            TempData["IsError"] = severity == CommonValues.MessageSeverity.Fail;

            if (TempData[CommonValues.DownloadFileIdCookieKey] == null) return;
            TempData["Message"] = MessageResource.Global_Display_ErrorMessage;
        }

        protected void LoadMultiLanguageFromTempData(MultiLanguageModel MultiLanguageModel)
        {
            MultiLanguageModel = TempData["MLModel"] == null ? new MultiLanguageModel() : (MultiLanguageModel)TempData["MLModel"];
        }

        protected JsonResult GenerateAsyncOperationResponse(AsynOperationStatus status, string message)
        {
            return Json(new
            {
                Status = (int)status,
                Message = message
            });
        }
        public new RedirectToRouteResult RedirectToAction(string action, string controller)
        {
            return base.RedirectToAction(action, controller);
        }
        protected virtual T ParseModelFromRequestInputStream<T>() where T : class
        {
            Request.InputStream.Seek(0, SeekOrigin.Begin);
            string jsonString = new StreamReader(Request.InputStream).ReadToEnd();
            {
                var serializer = new JavaScriptSerializer();
                return (T)serializer.Deserialize(jsonString, typeof(T));
            }
        }
        [AllowAnonymous]
        [Route("error")]
        public ActionResult Error()
        {
            return View();
        }
    }
}