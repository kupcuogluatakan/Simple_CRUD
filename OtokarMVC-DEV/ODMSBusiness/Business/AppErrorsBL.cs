using System;
using System.Collections.Generic;
using ODMSData;
using ODMSModel.Announcement;
using ODMSModel.ListModel;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;

namespace ODMSBusiness
{
    public class AppErrorsBL : BaseBusiness
    {

        public static async Task<ResponseModel<AppErrorViewModel>> Add(string error, MethodBase method)
        {
            var response = await AddAsync(error, method, string.Empty);
            return response;
        }

        public static async Task<ResponseModel<AppErrorViewModel>> Add(string error, MethodBase method, string executeSql)
        {
            var response = await AddAsync(error, method, executeSql);
            return response;
        }

        private static Task<ResponseModel<AppErrorViewModel>> AddAsync(string error, MethodBase method, string executeSql)
        {
            return Task.Run(() =>
            {

                var data = new AppErrorsData();

                var model = new AppErrorViewModel();
                model.CommandType = "I";
                model.Source = General.Source;
                if (method != null)
                {
                    string methodName = method.Name;
                    string className = method.ReflectedType.Name;

                    model.BusinessName = className;
                    model.MethodName = methodName;
                }
                if (!string.IsNullOrEmpty(executeSql))
                    model.DebugParameters = executeSql;
                if (UserManager.UserInfo != null)
                    model.UserCode = UserManager.UserInfo.UserName;
                model.ErrorDesc = error;
                model.ErrorTime = DateTime.Now;

                var response = new ResponseModel<AppErrorViewModel>();
                try
                {
                    data.Add(model);
                    response.Model = model;
                    response.Message = MessageResource.Global_Display_Success;
                }
                catch (System.Exception ex)
                {
                    File.AppendAllText(string.Format(@"{0}Errors\{1}.txt", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyyMMdd")), string.Format("{0} {1}", ex, DateTime.Now.ToString()) + Environment.NewLine);

                    response.IsSuccess = false;
                    response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
                }
                return response;
            });
        }

    }
}
