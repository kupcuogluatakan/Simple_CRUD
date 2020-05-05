using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ODMS.Core;
using ODMS.Filters;
using ODMSCommon;
using System.Threading.Tasks;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ExcelExporterController : ControllerBase
    {
        public ActionResult Export(ExcelExportDto dto)
        {

            if (dto.Controller == "ExcelExporter" && dto.Action == "Export")
                return Content("Sayfa döngüsü yapılamaz!");

            var routeData = new RouteData();
            routeData.Values["controller"] = dto.Controller;
            routeData.Values["action"] = dto.Action;
            foreach (var key in Request.Form.AllKeys)
            {
                routeData.Values[key] = Request.Form[key];
            }

            var root = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });

            var content = string.Empty;

            try
            {
                var request1 = (HttpWebRequest)WebRequest.Create(root + Url.Action(dto.Action, dto.Controller));
                var json1 = JsonConvert.SerializeObject(routeData.Values);
                var data1 = new UTF8Encoding().GetBytes(json1);
                var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                var netCookie = Request.Cookies["ASP.NET_SessionId"];
                var languageCookie = Request.Cookies["ODMSApplicationLang"];
                request1.Headers.Add(FormsAuthentication.FormsCookieName, authCookie == null ? "" : authCookie.Value);
                request1.Headers.Add("ODMSApplicationLang", languageCookie == null ? "" : languageCookie.Value);
                request1.Headers.Add("ASP.NET_SessionId", netCookie == null ? "" : netCookie.Value);
                request1.Headers.Add("UseExcelExport", "1");
                request1.ContentType = "application/json";
                request1.ContentLength = data1.Length;
                request1.Method = "POST";
                request1.GetRequestStream().Write(data1, 0, data1.Length);
                request1.Timeout = int.MaxValue;
                request1.AllowWriteStreamBuffering = false;
                //request1.ReadWriteTimeout = int.MaxValue;
                request1.KeepAlive = true;
                using (HttpWebResponse response = (HttpWebResponse)request1.GetResponse())
                {
                    WebHeaderCollection headers = response.Headers;
                    using (Stream answer = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(answer, Encoding.UTF8);
                        content = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.Timeout)
            {
                return Content("İstek timeout oldu!");
            }

            try
            {
                if (!string.IsNullOrEmpty(content))
                {

                    object obj = Activator.CreateInstance("ODMSModel", dto.ListType).Unwrap();
                    List<FilterDto> filters = null;
                    if (routeData.Values.Keys.Contains("Filters"))
                        filters = JsonConvert.DeserializeObject<List<FilterDto>>(routeData.Values["Filters"].ToString());

                    var propertiesArr = dto.Properties[0].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    var header = new List<string>();
                    foreach (var prop in propertiesArr)
                    {
                        var attr =
                               obj.GetType()
                                   .GetProperty(prop)
                                   .GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                        if (attr != null)
                        {


                            header.Add(CommonUtility.GetResourceValue(attr.Name));
                        }
                        else
                        {
                            header.Add(prop);
                        }
                    }

                    if (filters != null && filters.Any())
                    {
                        header.AddRange(filters.Select(c => c.FilterName).ToList());
                    }


                    var rows = new List<List<Tuple<object, string>>>();

                    foreach (JObject item in ((dynamic)((JObject)(JsonConvert.DeserializeObject(content)))).Data)
                    {

                        var row = propertiesArr.Select(prop => new Tuple<object, string>(item.GetValue(prop) as object, prop)).ToList();

                        //yemin ediyom böyle bir saçmalık yok
                        if (filters != null && filters.Any())
                        {
                            row.AddRange(filters.Select(c => new Tuple<object, string>(c.FilterValue, c.FilterName)).ToList());
                        }


                        rows.Add(row);

                    }
                    var fileName = string.IsNullOrEmpty(dto.ReportName) ? dto.Controller : dto.ReportName;
                    var excelBytes = new ExcelHelper().GenerateExcel(header, rows, filters, reportName: fileName, reportObject: obj);
                    return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
                }
            }
            catch (Exception ex)
            {
                return Content(string.Format("{0} - {1} ", "excel oluşumdan patladı!", ex.Message));
            }

            return Content("");
        }


        //public static Stream GetRequestStreamWithTimeout(this WebRequest request, int? millisecondsTimeout = null)
        //{
        //    return AsyncToSyncWithTimeout(
        //        request.BeginGetRequestStream,
        //        request.EndGetRequestStream,
        //        millisecondsTimeout ?? request.Timeout);
        //}

        //public static WebResponse GetResponseWithTimeout(this HttpWebRequest request, int? millisecondsTimeout = null)
        //{
        //    return AsyncToSyncWithTimeout(
        //        request.BeginGetResponse,
        //        request.EndGetResponse,
        //        millisecondsTimeout ?? request.Timeout);
        //}

        //private static T AsyncToSyncWithTimeout<T>(Func<AsyncCallback, object, IAsyncResult> begin,
        //Func<IAsyncResult, T> end,
        //int millisecondsTimeout)
        //{
        //    var iar = begin(null, null);
        //    if (!iar.AsyncWaitHandle.WaitOne(millisecondsTimeout))
        //    {
        //        var ex = new TimeoutException();
        //        throw new WebException(ex.Message, ex, WebExceptionStatus.Timeout, null);
        //    }
        //    return end(iar);
        //}

    }
}
