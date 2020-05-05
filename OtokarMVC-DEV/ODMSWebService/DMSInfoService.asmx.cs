using System;
using System.Data;
using System.Web.Services;
using ODMSBusiness;
using ODMSModel.Reports;
using System.Collections.Generic;
using ODMSWebService.Model;
using System.Linq;

namespace ODMSWebService
{
    /// <summary>
    /// Summary description for GetInfoFromDms
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DMSInfoService : System.Web.Services.WebService
    {

        [WebMethod]
        public DataSet VehicleHistoryInfo(string userName, string pass, string vinNo)
        {
            var bl = new VehicleBL();
            string un = System.Web.Configuration.WebConfigurationManager.AppSettings["WSUser"];
            string ps = System.Web.Configuration.WebConfigurationManager.AppSettings["WSPass"];
            DataSet ds = new DataSet();

            if (userName == un && pass == ps)
            {
                if (!string.IsNullOrEmpty(vinNo))
                {
                    ds = bl.ListVehicleHistoryForService(vinNo).Model;
                }
                else
                {
                    ds = GetErrorTable("Şasi no boş veya yanlış", 2);
                }
            }
            else
            {
                ds = GetErrorTable("Kullanıcı adı veya şifre yanlış", 1);
            }

            return ds;
        }


        [WebMethod]
        public InvoiceWebServiceResult InvoiceList(string userName, string password, string startDate, string endDate, string customerId, string invoiceNo = null)
        {

            DateTime sDate;
            DateTime eDate;
            DateTime.TryParse(startDate, out sDate);
            DateTime.TryParse(endDate, out eDate);
            long custId;
            if (!long.TryParse(customerId, out custId))
            {
                custId = 0;
            }
            var result = new ReportsBL().GetInvoicesAsXml(userName, password, sDate, eDate, custId, invoiceNo).Model;
            return result;
        }


        static DataSet GetErrorTable(string errorMessage, int errorNo)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            table.TableName = "tblVehicleCustomerInfo";
            table.Columns.Add("ErrorNo", typeof(int));
            table.Columns.Add("ErrorMessage", typeof(string));
            table.Columns.Add("ErrorDate", typeof(DateTime));

            table.Rows.Add(errorNo, errorMessage, DateTime.Now);
            ds.Tables.Add(table);

            return ds;
        }


        [WebMethod]
        public ResponseModel<Country> GetCountryList(string userName, string password, string languageCode)
        {
            var model = new ResponseModel<Country>() { IsSuccess = true };
            var business = new CountryBL();
            string un = System.Web.Configuration.WebConfigurationManager.AppSettings["WSUser"];
            string ps = System.Web.Configuration.WebConfigurationManager.AppSettings["WSPass"];

            if (userName == un && password == ps)
            {
                if (string.IsNullOrEmpty(languageCode))
                {
                    model.IsSuccess = false;
                    model.ErrorCode = 1;
                    model.ErrorMessage = "Dil kodu zorunlu parametredir.";
                    return model;
                }

                try
                {
                    var list = business.GetCountryList(languageCode).Data;
                    if (list.Count > 0)
                    {
                        model.Data = list.Select(x => new Country()
                        {
                            CityId = x.CityId,
                            CityName = x.CityName,
                            CountryId = x.CountryId,
                            CountryName = x.CountryName,
                            TownId = x.TownId,
                            TownName = x.TownName
                        }).ToList();
                    }
                }
                catch (Exception ex)
                {
                    model.IsSuccess = false;
                    model.ErrorCode = 2;
                    model.ErrorMessage = string.Format("Beklenmeyen hata oluştu.{0}", ex.Message);
                }
            }
            else
            {
                model.IsSuccess = false;
                model.ErrorCode = 1;
                model.ErrorMessage = "Hatalı kullanıcı adı yada şifre";
            }

            return model;
        }


        /// <summary>
        /// Kullanılmıyor test için eklendi.
        /// Silinebilir
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public ResponseModel<Country> GetCountryList_Duration(string userName, string password, string languageCode)
        {
            var model = new ResponseModel<Country>() { IsSuccess = true };
            var business = new CountryBL();
            string un = System.Web.Configuration.WebConfigurationManager.AppSettings["WSUser"];
            string ps = System.Web.Configuration.WebConfigurationManager.AppSettings["WSPass"];

            if (userName == un && password == ps)
            {
                if (string.IsNullOrEmpty(languageCode))
                {
                    model.IsSuccess = false;
                    model.ErrorCode = 1;
                    model.ErrorMessage = "Dil kodu zorunlu parametredir.";
                    return model;
                }

                try
                {
                    var list = business.GetCountryList_Duration(languageCode).Data;
                    if (list.Count > 0)
                    {
                        model.Data = list.Select(x => new Country()
                        {
                            CityId = x.CityId,
                            CityName = x.CityName,
                            CountryId = x.CountryId,
                            CountryName = x.CountryName,
                            TownId = x.TownId,
                            TownName = x.TownName
                        }).ToList();
                    }
                }
                catch (Exception ex)
                {
                    model.IsSuccess = false;
                    model.ErrorCode = 2;
                    model.ErrorMessage = string.Format("Beklenmeyen hata oluştu.{0}", ex.Message);
                }
            }
            else
            {
                model.IsSuccess = false;
                model.ErrorCode = 1;
                model.ErrorMessage = "Hatalı kullanıcı adı yada şifre";
            }

            return model;
        }

        [WebMethod]
        public ResponseModel<Dealer> GetDealerList(string userName, string password, string languageCode)
        {
            var model = new ResponseModel<Dealer>() { IsSuccess = true };
            var business = new DealerBL();
            string un = System.Web.Configuration.WebConfigurationManager.AppSettings["WSUser"];
            string ps = System.Web.Configuration.WebConfigurationManager.AppSettings["WSPass"];

            if (userName == un && password == ps)
            {
                if (string.IsNullOrEmpty(languageCode))
                {
                    model.IsSuccess = false;
                    model.ErrorCode = 1;
                    model.ErrorMessage = "Dil kodu zorunlu parametredir.";
                    return model;
                }

                try
                {
                    var list = business.GetDealerList(languageCode).Data;
                    if (list.Count > 0)
                    {
                        model.Data = list.Select(x => new Dealer()
                        {
                            DealerId = x.DealerId,
                            DealerName = x.Name,
                            Address = x.Address,
                            GroupName = x.GroupName,
                            CityId = x.CityId,
                            City = x.City,
                            CountryId = x.CountryId,
                            Country = x.Country,
                            TownId = x.TownId,
                            Town = x.Town,
                            Latitude = x.Latitude,
                            Longitute = x.Longitute
                        }).ToList();
                    }
                }
                catch (Exception ex)
                {
                    model.IsSuccess = false;
                    model.ErrorCode = 2;
                    model.ErrorMessage = string.Format("Beklenmeyen hata oluştu.{0}", ex.Message);
                }
            }
            else
            {
                model.IsSuccess = false;
                model.ErrorCode = 1;
                model.ErrorMessage = "Hatalı kullanıcı adı yada şifre";
            }

            return model;
        }

        [WebMethod]
        public ResponseModel<Vehicle> GetVehicleList(string userName, string password)
        {
            var model = new ResponseModel<Vehicle>() { IsSuccess = true };
            var business = new VehicleBL();
            string un = System.Web.Configuration.WebConfigurationManager.AppSettings["WSUser"];
            string ps = System.Web.Configuration.WebConfigurationManager.AppSettings["WSPass"];

            if (userName == un && password == ps)
            {
                try
                {
                    var list = business.ListVehicles().Data;
                    if (list.Count > 0)
                    {
                        model.Data = list.Select(x => new Vehicle()
                        {
                            VehicleId = x.VehicleId,
                            VinNo = x.VinNo,
                            ModelCode = x.ModelCode,
                            ModelName = x.ModelName,
                            ModelYear = x.ModelYear,
                            GroupCode = x.GroupCode,
                            GroupName = x.GroupName,
                            TypeCode = x.TypeCode,
                            TypeName = x.TypeName,
                            CustomerNameSurname = x.CustomerName
                        }).ToList();
                    }
                }
                catch (Exception ex)
                {
                    model.IsSuccess = false;
                    model.ErrorCode = 2;
                    model.ErrorMessage = string.Format("Beklenmeyen hata oluştu.{0}", ex.Message);
                }
            }
            else
            {
                model.IsSuccess = false;
                model.ErrorCode = 1;
                model.ErrorMessage = "Hatalı kullanıcı adı yada şifre";
            }

            return model;
        }

        [WebMethod]
        public ResponseModel<VehicleMaint> GetVehicleMaintList(string userName, string password, string languageCode, string vinNo)
        {
            var model = new ResponseModel<VehicleMaint>() { IsSuccess = true };
            var business = new VehicleBL();
            string un = System.Web.Configuration.WebConfigurationManager.AppSettings["WSUser"];
            string ps = System.Web.Configuration.WebConfigurationManager.AppSettings["WSPass"];

            if (userName == un && password == ps)
            {
                if (string.IsNullOrEmpty(languageCode) || string.IsNullOrEmpty(vinNo))
                {
                    model.IsSuccess = false;
                    model.ErrorCode = 1;
                    model.ErrorMessage = "Dil kodu ve Şasi no zorunlu parametredir.";
                    return model;
                }

                try
                {
                    var list = business.ListVehicleWorkOrderMaint(vinNo, languageCode).Data;
                    if (list.Count > 0)
                    {
                        model.Data = list.Select(x => new VehicleMaint()
                        {
                            VinNo = x.VinNo,
                            WorkOrderId = x.WorkOrderId,
                            WarrantStartDate = x.WarrantStartDate,
                            WarrantyEndDate = x.WarrantyEndDate,
                            WorkOrderCreateDate = x.WorkOrderCreateDate,
                            VehicleLeaveDate = x.VehicleLeaveDate,
                            DealerId = x.DealerId,
                            DealerName = x.DealerName,
                            MaintName = x.MaintName
                        }).ToList();
                    }
                }
                catch (Exception ex)
                {
                    model.IsSuccess = false;
                    model.ErrorCode = 2;
                    model.ErrorMessage = string.Format("Beklenmeyen hata oluştu.{0}", ex.Message);
                }
            }
            else
            {
                model.IsSuccess = false;
                model.ErrorCode = 1;
                model.ErrorMessage = "Hatalı kullanıcı adı yada şifre";
            }

            return model;
        }

        /// <summary>
        /// Kullanılmıyor test için eklendi.
        /// Silinebilir
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public ResponseModel<CacheModelList> GetCacheList(string userName, string password)
        {
            var model = new ResponseModel<CacheModelList>() { IsSuccess = true };
            if (userName == "sysadmin" && password == "1234")
            {
                try
                {
                    var list = BusinessCache.CacheList;
                    if (list.Count > 0)
                    {
                        model.Data = new List<CacheModelList>();
                        model.Data.AddRange(list.Select(x => new CacheModelList()
                        {
                            Name = x.Key,
                            Count = (x.Value as IEnumerable<object>).Count().ToString()
                        }).ToList());

                        list = BusinessCache.DurationCacheList;
                        model.Data.AddRange(list.Select(x => new CacheModelList()
                        {
                            Name = x.Key,
                            Count = ((DateTime)x.Value).ToString()
                        }).ToList());
                    }
                }
                catch (Exception ex)
                {
                    model.IsSuccess = false;
                    model.ErrorCode = 2;
                    model.ErrorMessage = string.Format("Beklenmeyen hata oluştu.{0}", ex.Message);
                }
            }
            else
            {
                model.IsSuccess = false;
                model.ErrorCode = 1;
                model.ErrorMessage = "Hatalı kullanıcı adı yada şifre";
            }

            return model;
        }
    }
}
