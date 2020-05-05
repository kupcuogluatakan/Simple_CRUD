using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMS.OtokarService;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using ODMSModel.PDIInvoice;

namespace ODMS.Controllers
{
    public class PDIInvoiceListController : ControllerBase
    {

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIInvoiceList.PDIInvoiceListIndex)]
        public ActionResult PDIInvoiceListIndex()
        {
            PDIInvoiceListModel model = new PDIInvoiceListModel();
            model.DealerId = UserManager.UserInfo.GetUserDealerId();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PDIInvoiceList.PDIInvoiceListIndex)]
        public JsonResult ListPDIInvoices([DataSourceRequest]DataSourceRequest request, PDIInvoiceListModel model)
        {
            List<PDIInvoiceListModel> rValue = new List<PDIInvoiceListModel>();
            int totalCount = 0;

            //if (General.IsTest)
            //    return Json(new
            //    {
            //        Data = rValue,
            //        Total = totalCount
            //    });

            var dealerId = UserManager.UserInfo.GetUserDealerId();
            var dealerBo = new DealerBL();

            if (dealerId != 0)
            {
                DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;
                using (var pssc = GetClient())
                {
                    string psUser = WebConfigurationManager.AppSettings["PSSCUser"];
                    string psPassword = WebConfigurationManager.AppSettings["PSSCPass"];

                    DataSet serviceList = pssc.ZDMS_REF_IZLEME(psUser, psPassword, "A", dealerModel.SSID);
                    DataTable dt = serviceList.Tables["t_Data1"];
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        rValue = (from DataRow e in dt.Rows
                                  select new PDIInvoiceListModel
                                  {
                                      RefNo = e["refno"].GetValue<long>(),
                                      Type = e["tip"].GetValue<string>(),
                                      OperationDateTime = e["islem_tarihi"].GetValue<DateTime>(),
                                      TotalLabourPrice = e["iscilik_toplam_tutari"].GetValue<decimal?>(),
                                      TotalPartPrice = e["parca_toplam_tutari"].GetValue<decimal?>(),
                                      TotalMaintenancePrice = e["kupon_bakim_toplam_tutari"].GetValue<decimal?>(),
                                      TotalPDIPrice = e["pdi_toplam_tutari"].GetValue<decimal?>(),
                                      VehicleGroupCode = e["arac_grup_kodu"].GetValue<string>()
                                  }).ToList<PDIInvoiceListModel>();
                    }
                }
            }
            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        public ActionResult PDIInvoiceDetailListIndex(string refNo)
        {
            PDIInvoiceDetailListModel model = new PDIInvoiceDetailListModel();
            model.RefNo = refNo.GetValue<long>();
            return View(model);
        }
        public JsonResult ListPDIInvoiceDetails([DataSourceRequest]DataSourceRequest request, PDIInvoiceDetailListModel model)
        {
            List<PDIInvoiceDetailListModel> rValue = new List<PDIInvoiceDetailListModel>();
            int totalCount = 0;

            //if (General.IsTest)
            //    return Json(new
            //    {
            //        Data = rValue,
            //        Total = totalCount
            //    });

            using (var pssc = GetClient())
            {
                string psUser = WebConfigurationManager.AppSettings["PSSCUser"];
                string psPassword = WebConfigurationManager.AppSettings["PSSCPass"];

                DataSet serviceList = pssc.ZDMS_REF_DET(psUser, psPassword, model.RefNo.ToString());
                DataTable dt = serviceList.Tables["t_Data1"];
                if (dt != null && dt.Rows.Count != 0)
                {
                    rValue = (from DataRow e in dt.Rows
                              select new PDIInvoiceDetailListModel
                              {
                                  RefNo = e["refno"].GetValue<long>(),
                                  VinNo = e["sasino"].GetValue<string>(),
                                  PDIRecordNo = e["gif_kayitno"].GetValue<string>(),
                                  MaintenanceDate = e["ariza_tr"].GetValue<DateTime?>(),
                                  OperationDate = e["islem_tr"].GetValue<DateTime?>(),
                                  TotalLabourPrice = e["iscilik_toplam_tutar"].GetValue<decimal?>(),
                                  TotalPartPrice = e["parca_toplam_tutar"].GetValue<decimal?>()
                              }).ToList<PDIInvoiceDetailListModel>();
                }
            }

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        public ActionResult PDIInvoiceLabourDetailListIndex(string refNo)
        {
            PDIInvoiceLabourDetailListModel model = new PDIInvoiceLabourDetailListModel();
            model.RefNo = refNo.GetValue<long>();
            return View(model);
        }
        public JsonResult ListPDIInvoiceLabourDetails([DataSourceRequest]DataSourceRequest request, PDIInvoiceLabourDetailListModel model)
        {
            List<PDIInvoiceLabourDetailListModel> rValue = new List<PDIInvoiceLabourDetailListModel>();
            int totalCount = 0;
            //if (General.IsTest)
            //    return Json(new
            //    {
            //        Data = rValue,
            //        Total = totalCount
            //    });

            using (var pssc = GetClient())
            {
                string psUser = WebConfigurationManager.AppSettings["PSSCUser"];
                string psPassword = WebConfigurationManager.AppSettings["PSSCPass"];

                DataSet serviceList = pssc.ZDMS_REF_DET_ISC(psUser, psPassword, model.RefNo.ToString());
                DataTable dt = serviceList.Tables["t_Data1"];
                if (dt != null && dt.Rows.Count != 0)
                {
                    rValue = (from DataRow e in dt.Rows
                              select new PDIInvoiceLabourDetailListModel
                              {
                                  RefNo = e["refno"].GetValue<long>(),
                                  VinNo = e["sasino"].GetValue<string>(),
                                  PDIRecordNo = e["gif_kayitno"].GetValue<string>(),
                                  MaintenanceDate = e["ariza_tr"].GetValue<DateTime?>(),
                                  MaintenanceCode = e["onarim_kodu"].GetValue<string>(),
                                  Price = e["tutar"].GetValue<decimal?>()
                              }).ToList<PDIInvoiceLabourDetailListModel>();
                }
            }

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        public ActionResult PDIInvoicePartDetailListIndex(string refNo)
        {
            PDIInvoicePartDetailListModel model = new PDIInvoicePartDetailListModel();
            model.RefNo = refNo.GetValue<long>();
            return View(model);
        }
        public JsonResult ListPDIInvoicePartDetails([DataSourceRequest]DataSourceRequest request, PDIInvoicePartDetailListModel model)
        {
            List<PDIInvoicePartDetailListModel> rValue = new List<PDIInvoicePartDetailListModel>();
            int totalCount = 0;
            //if (General.IsTest)
            //    return Json(new
            //    {
            //        Data = rValue,
            //        Total = totalCount
            //    });

            using (var pssc = GetClient())
            {
                string psUser = WebConfigurationManager.AppSettings["PSSCUser"];
                string psPassword = WebConfigurationManager.AppSettings["PSSCPass"];

                DataSet serviceList = pssc.ZDMS_REF_DET_PRC(psUser, psPassword, model.RefNo.ToString());
                DataTable dt = serviceList.Tables["t_Data1"];
                if (dt != null && dt.Rows.Count != 0)
                {
                    rValue = (from DataRow e in dt.Rows
                              select new PDIInvoicePartDetailListModel
                              {
                                  RefNo = e["refno"].GetValue<long>(),
                                  MaintenanceDate = e["ariza_tr"].GetValue<DateTime?>(),
                                  PDIRecordNo = e["gif_kayitno"].GetValue<string>(),
                                  PartCode = e["takilan_parca"].GetValue<string>(),
                                  Quantity = e["adet"].GetValue<long>(),
                                  UnitPrice = e["birim_fiyat"].GetValue<decimal?>()
                              }).ToList<PDIInvoicePartDetailListModel>();
                }
            }

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

    }
}
