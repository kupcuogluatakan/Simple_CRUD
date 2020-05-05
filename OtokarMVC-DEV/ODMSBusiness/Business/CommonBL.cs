using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSData.Utility;
using ODMSModel.Common;
using ODMSModel.StockBlock;
using ODMSCommon.Security;
using ODMSModel.Dealer;

namespace ODMSBusiness
{
    public class CommonBL : BaseBusiness
    {
        private readonly CommonData data = new CommonData();

        public static ResponseModel<SelectListItem> ListGroupTypeValueInt(bool isCampaignItem = true)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                var group = new List<SelectListItem>();
                if (isCampaignItem)
                {
                    group = new List<SelectListItem>
                    {
                        new SelectListItem {Value = "1", Text = "Servise Göre"},
                        new SelectListItem {Value = "2", Text = "Modele Göre"},
                        new SelectListItem {Value = "3", Text = "Araç Tipine Göre"},
                        new SelectListItem {Value = "4", Text = "Bölgeye Göre"},
                        new SelectListItem {Value ="5",Text = "Kampanya ya Göre"}
                    };
                }
                else
                {
                    group = new List<SelectListItem>
                    {
                        new SelectListItem {Value = "1", Text = "Servise Göre"},
                        new SelectListItem {Value = "2", Text = "Modele Göre"},
                        new SelectListItem {Value = "3", Text = "Araç Tipine Göre"},
                        new SelectListItem {Value = "4", Text = "Bölgeye Göre"}
                    };
                }
                response.Data = group;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;


        }

        public static ResponseModel<SelectListItem> ListLookup(UserInfo user, string lookupKey, string languageCode = "")
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new CommonData();
            try
            {
                response.Data = data.ListLookup(user, lookupKey, languageCode);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Zamanı SelectListItem tipi listesi şeklinde döndürür.
        /// </summary>
        /// <returns></returns>
        public static ResponseModel<SelectListItem> ListOfhours()
        {
            var response = new ResponseModel<SelectListItem>();

            try
            {
                var statusList = new List<SelectListItem>();
                for (int i = 0; i <= 23; i++)
                {
                    for (int j = 0; j <= 59; j++)
                    {
                        string tm = ((i<10)?("0"+i):(""+i)) + ":" + ((j < 10) ? ("0" + j) : ("" + j));
                        statusList.Add(new SelectListItem { Value = tm, Text = tm });
                    }
                }

                response.Data = statusList;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }



            return response;
        }

        public static ResponseModel<SelectListItem> ListStatus()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                var statusList = new List<SelectListItem>
                {
                    new SelectListItem {Value = "1", Text = MessageResource.Global_Display_Active},
                    new SelectListItem {Value = "0", Text = MessageResource.Global_Display_Passive}
                };

                response.Data = statusList;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;

        }
        public static ResponseModel<SelectListItem> ListStatusAll(int selected)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                var statusList = new List<SelectListItem>
                {
                    new SelectListItem {Value = "1", Text = MessageResource.Global_Display_Yes,Selected = selected==1},
                    new SelectListItem {Value = "0", Text = MessageResource.Global_Display_No,Selected = selected==0},
                    new SelectListItem {Value = "2", Text = MessageResource.Global_Display_All,Selected = selected==2}
                };
                response.Data = statusList;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public static ResponseModel<SelectListItem> ListAcceptStatus()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                var acceptStatusList = new List<SelectListItem>
                {
                    new SelectListItem {Value = "1", Text = MessageResource.Global_Display_Received},
                    new SelectListItem {Value = "0", Text = MessageResource.Global_Display_NotReceived}
                };
                response.Data = acceptStatusList;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListStatusOfWork()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                var statusOfWorkList = new List<SelectListItem>
                {
                    new SelectListItem {Value = "0", Text = MessageResource.WorkHour_StatusOfWork_NotWorks},
                    new SelectListItem {Value = "1", Text = MessageResource.WorkHour_StatusOfWork_Works}
                };
                response.Data = statusOfWorkList;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListSupplyType()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                var supplyTypeList = new List<SelectListItem>
                {
                    new SelectListItem {Value = "1", Text = "Otokar"},
                    new SelectListItem {Value = "0", Text = "Servis"}
                };
                response.Data = supplyTypeList;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public static ResponseModel<string> GetNewID()
        {
            var data = new CommonData();
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.GetNewID();
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public static ResponseModel<SelectListItem> ListYesNoValueInt()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                var statusList = new List<SelectListItem>
                {
                    new SelectListItem {Value = "1", Text = MessageResource.Global_Display_Yes},
                    new SelectListItem {Value = "0", Text = MessageResource.Global_Display_No}
                };
                response.Data = statusList;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListYesNoValueIntWithAll(int selected)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                var statusList = new List<SelectListItem>
                {
                    new SelectListItem {Value = "1", Text = MessageResource.Global_Display_Yes,Selected = selected==1},
                    new SelectListItem {Value = "0", Text = MessageResource.Global_Display_No,Selected = selected==0},
                    new SelectListItem {Value = "2", Text = MessageResource.Global_Display_All,Selected = selected==2}
                };
                response.Data = statusList;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListYesNo()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                var yesnoList = new List<SelectListItem>
                {
                    new SelectListItem {Value = "True", Text = MessageResource.Global_Display_Yes},
                    new SelectListItem {Value = "False", Text = MessageResource.Global_Display_No}
                };
                response.Data = yesnoList;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListStatusBool()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                var yesnoList = new List<SelectListItem>
                {
                    new SelectListItem {Value = "True", Text = MessageResource.Global_Display_Active},
                    new SelectListItem {Value = "False", Text = MessageResource.Global_Display_Passive}
                };
                response.Data = yesnoList;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public static ResponseModel<SelectListItem> ListReturnPicking()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                var returnpickingList = new List<SelectListItem>
                {
                    new SelectListItem {Value = "1", Text = MessageResource.Global_Display_Return},
                    new SelectListItem {Value = "0", Text = MessageResource.Global_Display_Picking}
                };
                response.Data = returnpickingList;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public static ResponseModel<decimal?> GetCountryVatRatio(int? countryId)
        {
            var data = new CommonData();
            var response = new ResponseModel<decimal?>();
            try
            {
                response.Model = data.GetCountryVatRatio(countryId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public static ResponseModel<SelectListItem> ListVatRatio()
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListVatRatio();
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public static ResponseModel<SelectListItem> ListCountries(UserInfo user)
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetCountryListForComboBox(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public static ResponseModel<SelectListItem> ListBodyWorks(UserInfo user)
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetBodyworkListForComboBox(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public static ResponseModel<SelectListItem> ListCities(UserInfo user, int countryId)
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetCityListForComboBox(user, countryId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public static ResponseModel<SelectListItem> ListTowns(UserInfo user, int cityId)
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetTownListForComboBox(user, cityId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public static ResponseModel<SelectListItem> ListRacks(UserInfo user, int? warehouseId)
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetRackListForComboBox(user, warehouseId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public static ResponseModel<SelectListItem> ListWarrantyStatus()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                var warrantyStatusList = new List<SelectListItem>
                {
                    new SelectListItem {Value = "1", Text = MessageResource.Vehicle_Display_Warrantied},
                    new SelectListItem {Value = "0", Text = MessageResource.Vehicle_Display_OutOfWarranty}
                };
                response.Data = warrantyStatusList;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public static ResponseModel<SelectListItem> ListStockTypes(UserInfo user)
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetStockTypeListForComboBox(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListPo(UserInfo user)
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPo(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public static ResponseModel<string> GetGeneralParameterValue(string key)
        {
            var data = new CommonData();
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.GetGeneralParameterValue(key);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public ResponseModel<decimal> GetPriceByDealerPartVehicleAndType(int partId, int? vehicleId, int dealerId, string type)
        {
            var response = new ResponseModel<decimal>();
            try
            {
                response.Model = data.GetPriceByDealerPartVehicleAndType(partId, vehicleId, dealerId, type);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListDealer(UserInfo user)
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListDealer(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<DealerViewModel> GetDealer(UserInfo user)
        {
            var data = new CommonData();
            var response = new ResponseModel<DealerViewModel>();
            try
            {
                response.Data = data.GetDealer(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListAllDealerWihSelectListItems()
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListAllDealerWihSelectListItems();
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListUserByDealerId(int? dealerId, bool? isTechnician)
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListUserByDealerId(dealerId, isTechnician);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListCurrencies(UserInfo user)
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetCurrencyListForComboBox(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListAppIndcFailureCode(UserInfo user)
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListAppIndcFailureCode(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListGuaranteeAuthorityNeedAsSelectListItem()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                var guaranteeAuthorityNeedList = new List<SelectListItem>
                {
                    new SelectListItem {Value = "True", Text = MessageResource.Global_Display_Yes},
                    new SelectListItem {Value = "False", Text = MessageResource.Global_Display_No}
                };
                response.Data = guaranteeAuthorityNeedList;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<string> SendDbMail(string to, string subject, string body)
        {
            var data = new CommonData();
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.SendDBMail(to, subject, body);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public static ResponseModel<SelectListItem> ListSelectedStockTypes(UserInfo user)
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetSelectedStockTypes(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<SelectListItem> ListStockTypes(UserInfo user, long? idPart, int? idDealer)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListStockTypes(user, idPart, idDealer);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<StockBlockViewModel> GetBlockQuantity(UserInfo user, long? idPart, int? idStockType, int? idDealer)
        {
            var response = new ResponseModel<StockBlockViewModel>();
            try
            {
                response.Model = data.GetBlockQuantity(user, idPart, idStockType, idDealer);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListBalanceType()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                var balanceTypeList = new List<SelectListItem>
                {
                    new SelectListItem {Value = "0", Text = MessageResource.StockBlock_Display_NoBalance},
                    new SelectListItem {Value = "1", Text = MessageResource.StockBlock_Display_Balance}
                };
                response.Data = balanceTypeList;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        /// <summary>
        /// returns active suppliers with given dealer id
        /// </summary>
        /// <returns></returns>
        public ResponseModel<SelectListItem> ListSuppliersByDealerId(int dealerId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListDealerSuppliers(dealerId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListAllLabour(UserInfo user)
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListAllLabour(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }


        public static ResponseModel<SelectListItem> ListConfirmedUser()
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListConfirmedUser();
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }
        public static ResponseModel<SelectListItem> ListProcessType(UserInfo user)
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListProcessType(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListRacksByPartWareHouse(UserInfo user, int warehouseId, long partId)
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListRacksByPartWareHouse(user, warehouseId, partId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListPeriodicMaintLang(UserInfo user)
        {
            var data = new CommonData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPeriodicMaintLang(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<StockTransactionViewModel> DMLStockTransaction(UserInfo user, StockTransactionViewModel model)
        {
            var response = new ResponseModel<StockTransactionViewModel>();
            try
            {
                data.DMLStockTransaction(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public static ResponseModel<bool> CheckDealer(int dealerid, long id, string type)
        {
            var data = new CommonData();
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = data.CheckDealer(dealerid, id, type);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;

        }

        //public static dynamic ListLookup(object ınvoiceType)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
