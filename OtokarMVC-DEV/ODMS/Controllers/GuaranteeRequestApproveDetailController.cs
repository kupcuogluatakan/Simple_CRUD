using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.GuaranteeRequestApproveDetail;
using System.Text.RegularExpressions;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.PartCostService;
using System.Web.Configuration;
using ODMSBusiness.Business;
using System.Data;
using ODMS.OtokarService;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    public class GuaranteeRequestApproveDetailController : ControllerBase
    {
        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeRequestApprove.GuaranteeRequestApproveIndex)]
        public ActionResult GuaranteeRequestApproveDetailIndex(int guaranteeId, int guaranteeSeq, int isEditable)
        {
            ViewBag.AllowQuantityEdit = 1;
            var bl = new GuaranteeRequestApproveDetailBL();
            bl.UpdatePricesOnOpen(UserManager.UserInfo, guaranteeId, guaranteeSeq);

            var model = new GRADMstViewModel()
            {
                GuaranteeId = guaranteeId,
                GuaranteeSeq = guaranteeSeq
            };

            bl.GetGuaranteeInfo(UserManager.UserInfo, model);
            model.IsEditable = isEditable != 0;
            if ((model.WarrantyStatus == 5 || model.WarrantyStatus == 6))
                model.IsEditable = false;


            return View(model);
        }

        public ActionResult ListGuaranteeParts(GuaranteePartsListModel hModel)
        {
            var bl = new GuaranteeRequestApproveDetailBL();
            var model = new GuaranteePartsListModel()
            {
                GuaranteeId = hModel.GuaranteeId,
                GuaranteeSeq = hModel.GuaranteeSeq
            };

            int totalCount = 0;
            var rValue = bl.ListGuaranteeParts(UserManager.UserInfo, model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        public ActionResult ListGuaranteeLabours(GuaranteeLaboursListModel hModel)
        {
            var bl = new GuaranteeRequestApproveDetailBL();
            var model = new GuaranteeLaboursListModel()
            {
                GuaranteeId = hModel.GuaranteeId,
                GuaranteeSeq = hModel.GuaranteeSeq
            };

            int totalCount = 0;
            var rValue = bl.ListGuaranteeLabours(UserManager.UserInfo, model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        public JsonResult ListRemovalPart(int PartId)
        {
            var bl = new GuaranteeRequestApproveDetailBL();

            return Json(bl.ListRemovalPart(UserManager.UserInfo, PartId).Data);
        }

        public ActionResult ListGuaranteeDescriptionHistory(GuaranteeDescriptionHistoryListModel hModel)
        {
            var bl = new GuaranteeRequestApproveDetailBL();
            var model = new GuaranteeDescriptionHistoryListModel()
            {
                GuaranteeId = hModel.GuaranteeId
            };
            int totalCount = 0;
            var rValue = bl.ListGuaranteeDescriptionHistory(model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        [HttpPost]
        public JsonResult SaveGuaranteeParts()
        {
            var bl = new GuaranteeRequestApproveDetailBL();
            var resolveRequest = HttpContext.Request;
            var model = new GuaranteePartsLabourViewModel();
            var listModel = new List<GuaranteePartsListModel>();

            resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);

            string jsonString = new StreamReader(resolveRequest.InputStream).ReadToEnd();
            {
                var serializer = new JavaScriptSerializer();
                listModel = (List<GuaranteePartsListModel>)serializer.Deserialize(jsonString, typeof(List<GuaranteePartsListModel>));
            }

            foreach (var item in listModel)
            {
                var regex = new Regex(@"^[a-zA-Z0-9\s,]*$");
                if (!string.IsNullOrEmpty(item.DisSerialNo))
                {

                    if (item.DisSerialNo.Length > 15 || !regex.IsMatch(item.DisSerialNo))
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Validation_DisSerialNo);
                    }
                }
                if (!string.IsNullOrEmpty(item.SerialNo))
                {
                    if (item.SerialNo.Length > 15 || !regex.IsMatch(item.SerialNo))
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Validation_PartSerialNo);
                    }
                }
            }

            model.ListModelParts = listModel;
            bl.DMLSaveGuaranteeParts(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpPost]
        public JsonResult SaveGuaranteeLabours()
        {
            var bl = new GuaranteeRequestApproveDetailBL();
            var resolveRequest = HttpContext.Request;
            var model = new GuaranteePartsLabourViewModel();
            var listModel = new List<GuaranteeLaboursListModel>();

            resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);

            string jsonString = new StreamReader(resolveRequest.InputStream).ReadToEnd();
            {
                var serializer = new JavaScriptSerializer();
                listModel = (List<GuaranteeLaboursListModel>)serializer.Deserialize(jsonString, typeof(List<GuaranteeLaboursListModel>));
            }

            model.ListModelLabour = listModel;
            bl.DMLSaveGuaranteeLabour(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        public JsonResult ListLabour()
        {
            return Json(CommonBL.ListAllLabour(UserManager.UserInfo).Data);
        }

        //Int64 id, int seq, int type, string desc, string category
        //, List<GuaranteeLaboursListModel> labourList, List<GuaranteePartsListModel> partList
        [HttpPost]
        public ActionResult GuaranteeApprove()
        {

            var resolveRequest = HttpContext.Request;
            var listModel = new GuaranteeCompleteViewModel();
            resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);
            string jsonString = new StreamReader(resolveRequest.InputStream).ReadToEnd();
            {
                var serializer = new JavaScriptSerializer();
                listModel = (GuaranteeCompleteViewModel)serializer.Deserialize(jsonString, typeof(GuaranteeCompleteViewModel));
            }

            var blPartsLabour = new GuaranteeRequestApproveDetailBL();

            var partLabourModel = new GuaranteePartsLabourViewModel();
            partLabourModel.ListModelParts = listModel.partList;
            partLabourModel.ListModelLabour = listModel.labourList;

            var saveParts = false;
            blPartsLabour.DMLSaveGuaranteeParts(UserManager.UserInfo, partLabourModel);
            saveParts = partLabourModel.ErrorNo == 0;

            var saveLabour = false;
            partLabourModel.ErrorNo = 0;
            blPartsLabour.DMLSaveGuaranteeLabour(UserManager.UserInfo, partLabourModel);
            saveLabour = partLabourModel.ErrorNo == 0;

            //parça ve işçilik kaydedildiyse işleme devm edilsin.
            if (saveLabour && saveParts)
            {
                var bl = new GuaranteeRequestApproveDetailBL();

                var model = new GRADMstViewModel()
                {
                    GuaranteeId = listModel.Id,
                    GuaranteeSeq = listModel.seq,
                    WarrantyStatus = listModel.type,
                    ConfirmDesc = listModel.desc
                };

                var category = listModel.category;

                if (string.IsNullOrEmpty(category))
                    model.CategoryId = null;
                else
                    model.CategoryId = CommonUtility.ParseEnum<GRADMstViewModel.Category>(category);

                bl.CompleteGuaranteeApprove(UserManager.UserInfo, model);

                if (model.ErrorNo == 0)
                {
                    if (!General.IsTest)
                    {
                        #region GarantiServisi
                        var callBl = new ServiceCallScheduleBL();
                        var logModel = new ServiceCallLogModel();
                        var serviceModel = new List<PartCostServiceModel>();

                        var requestResult = true;

                        for (int i = 0; i < 3; i++)
                        {
                            #region sys garanti servisi 
                            try
                            {
                                using (var pssc = GetClient())
                                {
                                    string psUser = WebConfigurationManager.AppSettings["PSSCUser"];//"OTOKAR_SYS";
                                    string psPassword = WebConfigurationManager.AppSettings["PSSCPass"];//"sY!kAr07";

                                    PartCostServiceBL Servicebl = new PartCostServiceBL();
                                    List<PartCostServiceModel> list = Servicebl.GetPart(listModel.Id, listModel.seq).Data;
                                    if (list.Count > 0)
                                    {
                                        foreach (var item in list)
                                        {
                                            string stockType = (item.StockType == 2) ? "B" : (item.StockType == 3) ? "K" : "";

                                            string date =
                                            item.ApproveDate.Year.ToString() +
                                            ((item.ApproveDate.Month.ToString().Length == 2) ? item.ApproveDate.Month.ToString() : "0" +
                                            item.ApproveDate.Month.ToString()) + ((item.ApproveDate.Day.ToString().Length == 2) ? item.ApproveDate.Day.ToString() : "0" +
                                            item.ApproveDate.Day.ToString());

                                            DataSet rValue = pssc.ZDMS_KMP_PRC_MLYT(psUser, psPassword, date, item.PartCode, stockType);
                                            DataTable dt = new DataTable();
                                            dt = rValue.Tables["Table1"];
                                            dt.AsEnumerable().Select(row => item.Avg_Cost = row.Field<decimal>("ORT_MALIYET"));
                                            var temp = dt.Rows[0].Field<string>("ORT_MALIYET");
                                            item.Avg_Cost = Convert.ToDecimal(temp.Replace('.', ','));
                                        }
                                        Servicebl.SetPartCostVAlue(list);
                                    }

                                }
                                requestResult = true;
                            }
                            catch (Exception ex)
                            {
                                requestResult = false;

                            }
                            #endregion

                            if (requestResult)
                                break;
                        }

                        //sys servisinden hata geldi ise
                        if (!requestResult)
                            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, "Garanti Servisi çalışma esnasında hata oluşmuştur.");

                        #endregion
                    }
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
                }
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
            }
            else
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Err_Generic_Unexpected);
        }

        public ActionResult GetPartialGifHistory(Int64 id, int seq)
        {
            var model = new GRADMstViewModel()
            {
                GuaranteeId = id,
                GuaranteeSeq = seq
            };
            return PartialView("PartialGifHistory", model);
        }

        public ActionResult ListGRADGifHistory([DataSourceRequest]DataSourceRequest request, GRADGifHistoryModel cModel)
        {
            var bl = new GuaranteeRequestApproveDetailBL();
            int totalCount = 0;
            var model = new GRADGifHistoryModel(request)
            {
                GuaranteeId = cModel.GuaranteeId,
                GuaranteeSeq = cModel.GuaranteeSeq
            };

            var rValue = bl.ListGRADGifHistory(UserManager.UserInfo, model, out totalCount).Data;


            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        public ActionResult ListGRADGifHistoryDet([DataSourceRequest]DataSourceRequest request, Int64 guaranteeId)
        {
            var bl = new GuaranteeRequestApproveDetailBL();
            int totalCount = 0;

            var rValue = bl.ListGRADGifHistoryDet(UserManager.UserInfo, guaranteeId, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });

        }

        [HttpPost]
        public ActionResult GuaranteeCancel(string id, string ssidGuarantee, int guaranteeSeq, int warrantyStat)
        {
            var bl = new GuaranteeRequestApproveDetailBL();

            var model = new GRADMstViewModel
            {

                GuaranteeId = Convert.ToInt64(id),
                GifInfo = new GifModel { GifNo = ssidGuarantee },
                GuaranteeSeq = guaranteeSeq,
                WarrantyStatus = warrantyStat

            };

            bl.GetGuaranteeInfo(UserManager.UserInfo, model);

            if (model.WarrantyStatus != warrantyStat)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.DB_Record_Has_Changed);
            }


            if (model.WarrantyStatus == (int)GRADMstViewModel.WarrantyStatusType.AuthorityApproved)
            {

                model.WarrantyStatus = (int)GRADMstViewModel.WarrantyStatusType.NewRecord;
            }

            if (model.WarrantyStatus == (int)GRADMstViewModel.WarrantyStatusType.Approved)
            {
                if (model.GifInfo.IsPerm == false)
                {
                    model.WarrantyStatus = (int)GRADMstViewModel.WarrantyStatusType.NewRecord;
                }
                else
                {
                    model.WarrantyStatus = (int)GRADMstViewModel.WarrantyStatusType.AuthorityApproved;
                }
            }

            bl.CompleteGuaranteeCancel(UserManager.UserInfo, model);

            return model.ErrorNo == 0 ?
                GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success) :
                GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpPost]
        public ActionResult GuaranteeUpdateDescription(Int64 id, string desc)
        {
            var bl = new GuaranteeRequestApproveDetailBL();

            var model = new GRADMstViewModel()
            {
                GuaranteeId = id,
                ConfirmDesc = desc
            };

            bl.GuaranteeUpdateDescription(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpGet]
        public ActionResult GuaranteeRequestApproveDetailPartInfo(long id)
        {
            ViewBag.id = id;
            return View();
        }
        public ActionResult ListGuaranteeRequestApproveDetailPartInfo([DataSourceRequest] DataSourceRequest request, long id)
        {
            var returnModel = new GuaranteeRequestApproveDetailBL().GetPartInfos(id).Data;
            return Json(new
            {
                Data = returnModel
            });
        }

    }
}
