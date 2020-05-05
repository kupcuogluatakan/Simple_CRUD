using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Core;
using ODMSCommon;
using ODMSModel.ViewModel;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel;
using ODMSModel.ListModel;
using NLog;
using System.IO;
using System.Web.Script.Serialization;
using System.Net;
using System.Web.Configuration;
using ODMS.OtokarService;

namespace ODMS.Controllers
{
    [RequireHttps]
    public abstract class ControllerBase : Controller
    {
        protected enum AsynOperationStatus
        {
            Error = 0,
            Success = 1
        }

        protected ControllerBase()
        {
            ClearTempData();
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            foreach (var item in filterContext.ActionParameters)
            {
                var filterObj = (item.Value as BaseListWithPagingModel);
                if (filterObj != null && filterObj.FirstCall == 1)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                        filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                        filterContext.Result = new JsonResult
                        {
                            Data = null
                            //JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                }
            }

            try
            {
                GlobalDiagnosticsContext.Set("SessionId", filterContext.HttpContext?.Session?.SessionID);
            }
            catch
            {
                GlobalDiagnosticsContext.Set("SessionId", "Taner");
            }

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
        //public bool CheckErrorForMessageTemp(ODMSModel.ModelBase targetModel, bool showSuccessAsDefault)
        //{
        //    var retVal = new MessageHolderViewModel();
        //    if (targetModel.ErrorNo > 0)
        //    {
        //        retVal.ResultMessage = targetModel.ErrorMessage; //globalize
        //        retVal.IsError = true;
        //        TempData["RoleViewState"] = targetModel;
        //    }
        //    else if (showSuccessAsDefault)
        //    { 
        //        retVal.ResultMessage = CommonUtility.GetResourceValue("Global_Display_Success");
        //        retVal.IsError = false;
        //    }
        //    TempData["MessageToShow"] = retVal.ResultMessage;
        //    TempData["IsError"] = retVal.IsError;
        //    //ViewBag.MessageToShow = retVal;
        //    return retVal.IsError;
        //}

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
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding,
            JsonRequestBehavior behavior)
        {
            return new JsonNetResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public ActionResult Error()
        {
            return View("Error");
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
        //private dynamic GetClient()
        //{
        //    if (WebConfigurationManager.AppSettings.GetValues("ServiceReferance")[0] == "Test")
        //        return new ProjectServiceSoapClient("ProjectServiceSoap");
        //    return new tr.com.otokar.service.ProjectServiceSoapClient();
        //}
        protected dynamic GetClient()
        {
            if (WebConfigurationManager.AppSettings.GetValues("ServiceReferance")[0] == "Test")
                return new ProjectServiceSoapClient();
            return new tr.com.otokar.service.ProjectServiceSoapClient();
        }
    }
}