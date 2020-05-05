using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSData.Utility;
using ODMSModel.ListModel;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.SparePart;
using ODMSModel.StockCardPriceListModel;
using ODMSCommon.Security;
using ODMSModel.AppointmentIndicatorFailureCode;

namespace ODMSBusiness
{
    public class SparePartBL : BaseBusiness
    {
        private readonly SparePartData data = new SparePartData();
        public static ResponseModel<SelectListItem> GetSparePartClassCodeListForComboBox(UserInfo user)
        {
            var data = new SparePartData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetSparePartClassCodeListForComboBox(user);
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
        public static ResponseModel<SelectListItem> ListStockCardsAsSelectListItem(UserInfo user, bool? isOriginal)
        {
            var data = new SparePartData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListStockCardsAsSelectListItem(user, isOriginal);
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
        public ResponseModel<SparePartListModel> ListSpareParts(UserInfo user,SparePartListModel filter, out int totalCnt)
        {
            var data = new SparePartData();
            var response = new ResponseModel<SparePartListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSparePart(user,filter, out totalCnt);
                response.Total = totalCnt;
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
        public ResponseModel<SparePartIndexViewModel> GetSparePart(UserInfo user, SparePartIndexViewModel filter)
        {
            var data = new SparePartData();
            var response = new ResponseModel<SparePartIndexViewModel>();
            try
            {
                response.Model = data.GetSparePart(user, filter);
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
        public ResponseModel<SparePartIndexViewModel> GetSparePartFromTable(List<SparePartIndexViewModel> list)
        {
            var data = new SparePartData();
            var response = new ResponseModel<SparePartIndexViewModel>();
            try
            {
                response.Data = data.GetSparePartFromTable(list);
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
        public new ResponseModel<SparePartIndexViewModel> DMLSparePart(UserInfo user, SparePartIndexViewModel model)
        {
            var data = new SparePartData();
            var response = new ResponseModel<SparePartIndexViewModel>();
            try
            {
                data.DMLSparePart(user, model);
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

        public static ResponseModel<AutocompleteSearchListModel> ListSparePartAsAutoCompSearch(UserInfo user,string strSearch, string dealerId)
        {
            var data = new SparePartData();
            var response = new ResponseModel<AutocompleteSearchListModel>();
            try
            {
                response.Data = data.ListSparePartAsAutoCompSearch(user,strSearch, dealerId);
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
        /// Orjinal yada eşlenik seçeneklerini combo olarak döner.
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> ListPartTypes()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = MessageResource.Global_Display_Original, Value = "1" });
            list.Add(new SelectListItem { Text = MessageResource.SparePart_Display_NotOriginal, Value = "0" });
            return list;
        }
        public static ResponseModel<AutocompleteSearchListModel> ListOriginalSparePartAsAutoCompSearch(UserInfo user, string searchString)
        {
            var data = new SparePartData();
            var response = new ResponseModel<AutocompleteSearchListModel>();
            try
            {
                response.Data = data.ListOriginalSparePartAsAutoCompSearch(user, searchString);
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

        public static ResponseModel<AutocompleteSearchListModel> ListNotOriginalSparePartAsAutoCompSearch(UserInfo user, string searchString)
        {
            var data = new SparePartData();
            var response = new ResponseModel<AutocompleteSearchListModel>();
            try
            {
                response.Data = data.LisNotOriginalSparePartAsAutoCompSearch(user, searchString);
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

        public ResponseModel<SparePartSupplyDiscountRatioListModel> ListSparePartsSupplyDiscountRatios(UserInfo user, SparePartSupplyDiscountRatioListModel filter, out int totalCnt)
        {
            var data = new SparePartData();
            var response = new ResponseModel<SparePartSupplyDiscountRatioListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSparePartsSupplyDiscountRatios(user, filter, out totalCnt);
                response.Total = totalCnt;
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

        public static ResponseModel<SelectListItem> ListSSIDPriceListAsSelectListItem()
        {
            var data = new SparePartData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListSSIDPriceListAsSelectListItem();
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
        public ResponseModel<int> IsPartChanged(int partId, string partCode)
        {
            var data = new SparePartData();
            var response = new ResponseModel<int>();
            try
            {
                response.Model = data.IsPartChanged(partId, partCode);
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
        public ResponseModel<SparePartSplittingViewModel> ListSparePartsSplitting(long partId)
        {
            var data = new SparePartData();
            var response = new ResponseModel<SparePartSplittingViewModel>();
            try
            {
                response.Data = data.ListSparePartsSplitting(partId);
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
        public ServiceCallLogModel SaveSparePartSupplyDicsountRatio(UserInfo user, DataTable list)
        {
            var logModel = new ServiceCallLogModel() { IsSuccess = true, ErrorModel = new List<ServiceCallScheduleErrorListModel>() };
            List<SparePartSupplyDiscountRatioXMLModel> listModel = new List<SparePartSupplyDiscountRatioXMLModel>();
            SparePartData dal = new SparePartData();
            DealerSaleChannelData dscDal = new DealerSaleChannelData();
            List<SelectListItem> dealerSaleChannel = dscDal.ListDealerSaleChannelsAsSelectListItem();

            foreach (DataRow row in list.Rows)
            {
                /*
                <SS_ID>0008894931</SS_ID>  SS_ID
                <MATNR>T-15-066-AA</MATNR> ID_PART
                <KBETR>25.00</KBETR>   DISCOUNT_RATIO
                <DATAB>2014-01-15</DATAB> VALID_START_DATE
                <DATBI>9999-12-31</DATBI>  VALID_END_DATE
                <VKORG>5100</VKORG>      CHANNEL_CODE
                <CHNIND/>   IS_ACTIVE
                 */
                SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                SparePartSupplyDiscountRatioXMLModel iModel = new SparePartSupplyDiscountRatioXMLModel();
                iModel.SSID = row["SS_ID"].GetValue<string>();
                iModel.PartCode = row["MATNR"].GetValue<string>();
                if (string.IsNullOrEmpty(iModel.SSID))
                {
                    logModel.ErrorModel.Add(new ServiceCallScheduleErrorListModel()
                    {
                        Action = iModel.PartCode + " - " + MessageResource.SparePart_Display_PartCode,
                        Error = MessageResource.SparePartDiscountRatio_Warning_SSIDEmpty
                    });
                    logModel.IsSuccess = false;
                }
                if (string.IsNullOrEmpty(iModel.PartCode))
                {
                    logModel.ErrorModel.Add(new ServiceCallScheduleErrorListModel()
                    {
                        Action = iModel.SSID + " - " + MessageResource.SparePartDiscountRatio_Display_SSID,
                        Error = MessageResource.SparePartDiscountRatio_Warning_PartCodeEmpty
                    });
                    logModel.IsSuccess = false;
                }
                else
                {
                    spModel.PartCode = iModel.PartCode;
                    spModel = dal.GetSparePart(user, spModel);
                    if (spModel.PartId != 0)
                    {
                        iModel.PartId = spModel.PartId;
                    }
                    else
                    {
                        logModel.ErrorModel.Add(new ServiceCallScheduleErrorListModel()
                        {
                            Action = iModel.PartCode + " - " + MessageResource.SparePart_Display_PartCode,
                            Error = MessageResource.SparePartDiscountRatio_Warning_PartCodeNotFound
                        });
                        logModel.IsSuccess = false;
                    }
                }
                iModel.DiscountRatio = row["KBETR"].ToString().ToCommaString().GetValue<decimal>();
                if (iModel.DiscountRatio == 0)
                {
                    logModel.ErrorModel.Add(new ServiceCallScheduleErrorListModel()
                    {
                        Action = iModel.PartCode + " - " + MessageResource.SparePart_Display_PartCode,
                        Error = MessageResource.SparePartDiscountRatio_Warning_DiscountRatioEmpty
                    });
                    logModel.IsSuccess = false;
                }
                iModel.ValidStartDate = row["DATAB"].GetValue<DateTime>();
                if (iModel.ValidStartDate == null)
                {
                    logModel.ErrorModel.Add(new ServiceCallScheduleErrorListModel()
                    {
                        Action = iModel.PartCode + " - " + MessageResource.SparePart_Display_PartCode,
                        Error = MessageResource.SparePartDiscountRatio_Warning_ValidStartDateEmpty
                    });
                    logModel.IsSuccess = false;
                }
                iModel.ValidEndDate = row["DATBI"].GetValue<DateTime>();
                if (iModel.ValidEndDate == null)
                {
                    logModel.ErrorModel.Add(new ServiceCallScheduleErrorListModel()
                    {
                        Action = iModel.PartCode + " - " + MessageResource.SparePart_Display_PartCode,
                        Error = MessageResource.SparePartDiscountRatio_Warning_ValidEndDateEmpty
                    });
                    logModel.IsSuccess = false;
                }
                iModel.ChannelCode = row["VKORG"].GetValue<string>();
                if (string.IsNullOrEmpty(iModel.ChannelCode))
                {
                    logModel.ErrorModel.Add(new ServiceCallScheduleErrorListModel()
                    {
                        Action = iModel.PartCode + " - " + MessageResource.SparePart_Display_PartCode,
                        Error = MessageResource.SparePartDiscountRatio_Warning_ChannelCodeEmpty
                    });
                    logModel.IsSuccess = false;
                }
                else
                {
                    var control = (from d in dealerSaleChannel
                                   where d.Value == iModel.ChannelCode
                                   select d);
                    if (!control.Any())
                    {
                        logModel.ErrorModel.Add(new ServiceCallScheduleErrorListModel()
                        {
                            Action = iModel.ChannelCode + " - " + MessageResource.SparePartSupplyDiscountRatio_Display_ChannelCode,
                            Error = MessageResource.SparePartDiscountRatio_Warning_ChannelCodeNotFound
                        });
                        logModel.IsSuccess = false;
                    }
                }
                iModel.IsActive = String.IsNullOrEmpty(row["CHNIND"].GetValue<String>());
                listModel.Add(iModel);
            }

            if (logModel.IsSuccess)
            {
                dal.XMLtoDBSparePartSupplyDiscountRatio(listModel, logModel);
            }
            return logModel;
        }
        
        public ServiceCallLogModel XMLtoDBSparePart(DataTable listModel, DataTable langListModel)
        {
            var dal = new SparePartData();

            var model = dal.XMLtoDBSparePart(listModel);

            if (model.IsSuccess)
                dal.XMLtoDBSparePartLang(langListModel);

            return model;
        }

        public ResponseModel<ServiceCallLogModel> XMLtoDBSparePartPrice(DataTable dtPriceList)
        {
            var data = new SparePartData();
            var response = new ResponseModel<ServiceCallLogModel>();
            try
            {
                response.Model = data.XMLtoDBSparePartPrice(dtPriceList);
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


        public ResponseModel<bool> XMLtoDBSparePartSplit(List<SparePartSplitterXMLModel> listModel)
        {
            var data = new SparePartData();
            var response = new ResponseModel<bool>();
            try
            {
                data.DeleteSparePartSplitting();
                data.XMLtoDBSparePartSplit(listModel);
                response.Model = true;
                response.Message = MessageResource.Global_Display_Success;
                if (listModel.Any(x => x.ErrorNo > 0))
                    throw new Exception(listModel.FirstOrDefault().ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error,
                    MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;

        }

        public ResponseModel<int> GetFreeQuantity(int partId, int dealerId, int stockTypeId)
        {
            var data = new SparePartData();
            var response = new ResponseModel<int>();
            try
            {
                response.Model = data.GetFreeQuantity(partId, dealerId, stockTypeId);
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

        public ResponseModel<decimal> GetDiscountPrice(int partId, int dealerId, int stockTypeId)
        {
            var data = new SparePartData();
            var response = new ResponseModel<decimal>();
            try
            {
                response.Model = data.GetDiscountPrice(partId, dealerId, stockTypeId);
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
        public ResponseModel<decimal> GetCustomerPartDiscount(int? partId, int? dealerId, int? customerId, string actionType)
        {
            var data = new SparePartData();
            var response = new ResponseModel<decimal>();
            try
            {
                response.Model = data.GetCustomerPartDiscount(partId, dealerId, customerId, actionType);
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

        public static ResponseModel<AutocompleteSearchListModel> ListStockCardPartsAsAutoCompSearch(UserInfo user,string strSearch)
        {
            var data = new SparePartData();
            var response = new ResponseModel<AutocompleteSearchListModel>();
            try
            {
                response.Data = data.ListStockCardPartsAsAutoCompSearch(user,strSearch);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error,
                    MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<string> ListPriceListSSIDs()
        {
            var data = new SparePartData();
            var response = new ResponseModel<string>();
            try
            {
                response.Data = data.ListPriceListSSIDs();
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

        public ResponseModel<string> GetPriceListSsidByPriceListId(int priceListId)
        {
            var data = new SparePartData();
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.GetPriceListSsidByPriceListId(priceListId);
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

        public ResponseModel<SelectListItem> ListPartClassCodes(UserInfo user)
        {
            var data = new SparePartData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPartClassCodes(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error,
                    MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<decimal> UpdateSparePartUnserve(UserInfo user,int dealerId, long partId, int stockTypeId, decimal quantity, string transactionDesc, int transactionTypeId)
        {
            var data = new SparePartData();
            var response = new ResponseModel<decimal>();
            try
            {
                response.Model = data.UpdateSparePartUnserve(user,dealerId, partId, stockTypeId, quantity, transactionDesc, transactionTypeId);
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
    }
}

