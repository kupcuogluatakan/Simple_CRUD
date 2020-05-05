using DocumentFormat.OpenXml.Drawing;
using ODMS.Filters;
using ODMSBusiness;
using ODMSBusiness.Business;
using ODMSBusiness.Reports;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel;
using ODMSModel.AppointmentDetails;
using ODMSModel.AppointmentDetailsLabours;
using ODMSModel.AppointmentDetailsParts;
using ODMSModel.Common;
using ODMSModel.Customer;
using ODMSModel.CustomerDiscount;
using ODMSModel.Dealer;
using ODMSModel.Fleet;
using ODMSModel.PeriodicMaintControlList;
using ODMSModel.Proposal;
using ODMSModel.ProposalCard;
using ODMSModel.SparePart;
using ODMSModel.SparePartSale;
using ODMSModel.SparePartSaleDetail;
using ODMSModel.StockCard;
using ODMSModel.StockTypeDetail;
using ODMSModel.Vehicle;
using ODMSModel.VehicleBodywork;
using ODMSModel.VehicleBodyWorkProposal;
using ODMSModel.WorkOrderCard;
using ODMSModel.WorkOrderPicking;
using ODMSModel.WorkOrderPickingDetail;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Permission = ODMSCommon.CommonValues.PermissionCodes.ProposalCard;
namespace ODMS.Controllers
{
    public class Proposal_CardController : ControllerBase
    {
        /// <summary
        /// 
        /// </summary>
        /// <param name="id">Proposal</param>
        /// <returns></returns> 
        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public ActionResult ProposalCardIndex(long id = 0, int seq = 0)
        {
            ViewBag.YesNoList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.YesNo).Data;
            ViewBag.HideElements = false;
            if (id == 0)
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                ViewBag.HideElements = true;
            }
            var bl = new ProposalCardBL();
            var model = bl.GetProposalCard(UserManager.UserInfo, id, seq).Model;
            if (model.TechnicalDesc != null)
                model.TechnicialDescList = model.TechnicalDesc.Split(new[] { "/*,/" }, StringSplitOptions.None);

            if (model.Customer == null || model.Vehicle == null)
            {
                ViewBag.HideElements = true;
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View(model);
            }
            var statsList = bl.ListProposalStats(UserManager.UserInfo).Data;
            ViewBag.ProposalStatsList = model.ProposalStatManualChangeAllow
                ? statsList.Where(c => c.Selected).ToList()
                : statsList;
            if ((UserManager.UserInfo.IsDealer && UserManager.UserInfo.GetUserDealerId() != model.DealerId))
            {
                ViewBag.HideElements = true;
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View(model);
            }

            if (model.ProposalStatId.In(1, 4, 5, 6, 7, 8) || model.IsConvert)
            {
                return View("_ReadOnlyProposalCardIndex", model);
            }

            ViewBag.CampaignCheckList = model.Vehicle.IsCampaignApplicable ? bl.GetCampaignCheckList(UserManager.UserInfo, id).Data.Where(c => c.IsMust).ToList() : null;


            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public ActionResult Print()
        {

            var model = (Session["PrintProposal"] as ProposalCardModel);
            if (model == null)
            {
                return RedirectToAction("NoAuthorization", "SystemAdmin");
            }

            var id = model.ProposalId;
            var seq = model.ProposalSeq;

            Session["PrintProposal"] = null;

            var user = UserManager.UserInfo;
            //HQ user
            if (user.IsDealer)
            {
                bool checkDealer = new ProposalCardBL().CheckProposalDealer(id, user.DealerID).Model;
                if (!checkDealer)
                {
                    return RedirectToAction("NoAuthorization", "SystemAdmin");
                }
            }
            //Teklifin statüsü Onay Bekleyen Teklif Durumuna Getirilir.
            //if (new ProposalCardBL().GetProposalCard(UserManager.UserInfo, id, seq).Model.ProposalStatId == -1)
            //{
            //    new ProposalCardBL().ChangeProposalStat(UserManager.UserInfo, id, 1, seq);
            //}

            var proposal = ReportManager.GetReport(ReportType.Proposal, id, seq);
            if (proposal != null && proposal.Length > 0)
            {
                // 21.11.2017 testinde Gürdal bey'in isteği ile kapatıldı.
                //var proposalDetail = ReportManager.GetReport(ReportType.ProposalDetail, id, seq);
                //using (var second = new MemoryStream(proposalDetail))
                using (var first = new MemoryStream(proposal))
                using (PdfDocument targetDoc = new PdfDocument())
                {
                    using (PdfDocument pdfDoc = PdfReader.Open(first, PdfDocumentOpenMode.Import))
                    {
                        for (int i = 0; i < pdfDoc.PageCount; i++)
                        {
                            targetDoc.AddPage(pdfDoc.Pages[i]);
                        }
                    }
                    // 21.11.2017 testinde Gürdal bey'in isteği ile kapatıldı.
                    //using (PdfDocument pdfDoc = PdfReader.Open(second, PdfDocumentOpenMode.Import))
                    //{
                    //    for (int i = 0; i < pdfDoc.PageCount; i++)
                    //    {
                    //        Paragraph paragraph = new Paragraph();
                    //        targetDoc.AddPage(pdfDoc.Pages[i]);
                    //    }
                    //}
                    ViewBag.HideElements = true;
                    var ms = new MemoryStream();
                    targetDoc.Save(ms, false);

                    PdfDocument outputDocument = new PdfDocument();
                    using (var last = new MemoryStream(ms.ToArray()))
                    using (PdfDocument targetsDoc = new PdfDocument())
                    {
                        PdfDocument inputDocument = PdfReader.Open(last, PdfDocumentOpenMode.Import);
                        // Show consecutive pages facing. Requires Acrobat 5 or higher.
                        outputDocument.PageLayout = PdfPageLayout.TwoColumnLeft;

                        XFont font = new XFont("Verdana", 10, XFontStyle.Regular);
                        XStringFormat format = new XStringFormat();
                        format.Alignment = XStringAlignment.Center;
                        format.LineAlignment = XLineAlignment.Far;
                        XGraphics gfx;
                        XRect box;
                        int count = inputDocument.PageCount;
                        for (int idx = 0; idx < count; idx++)
                        {
                            PdfPage page1 = inputDocument.PageCount > idx ?
                              inputDocument.Pages[idx] : new PdfPage();
                            page1 = outputDocument.AddPage(page1);
                            gfx = XGraphics.FromPdfPage(page1);
                            box = page1.MediaBox.ToXRect();
                            box.Inflate(0, -10);
                            gfx.DrawString(String.Format("Sayfa No : {0} / {1}", (idx + 1).ToString(), count.ToString()), font, XBrushes.Black, box, format);
                        }

                    }
                    var ms2 = new MemoryStream();
                    outputDocument.Save(ms2, false);
                    return File(ms2.ToArray(), MimeMapping.GetMimeMapping("a.pdf"), String.Format("{0}_Nolu_Teklif_Dökümü.pdf", id));
                }
            }

            SetMessage(MessageResource.ErrorProposalReport, CommonValues.MessageSeverity.Fail);
            return ProposalCardIndex(id, seq);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public ActionResult ChangeProposalStatForPrint(long id, int seq)
        {
            var user = UserManager.UserInfo;
            var prop = new ProposalCardModel();
            //HQ user
            if (user.IsDealer)
            {
                bool checkDealer = new ProposalCardBL().CheckProposalDealer(id, user.DealerID).Model;
                if (!checkDealer)
                {
                    return RedirectToAction("NoAuthorization", "SystemAdmin");
                }
            }
            //Teklifin statüsü Onay Bekleyen Teklif Durumuna Getirilir.
            if (new ProposalCardBL().GetProposalCard(UserManager.UserInfo, id, seq).Model.ProposalStatId == -1)
            {
                var model = new ProposalCardBL().ChangeProposalStat(UserManager.UserInfo, id, 1, seq);
                if (model.Model.ErrorNo == 0)
                {
                    prop.ProposalId = id;
                    prop.ProposalSeq = seq;
                    Session["PrintProposal"] = prop;
                    return Json(new { Result = true, Message = MessageResource.Global_Display_Success });
                }
                else
                {
                    return Json(new { Result = false, Message = model.Model.ErrorMessage });
                }
            }
            prop.ProposalId = id;
            prop.ProposalSeq = seq;
            Session["PrintProposal"] = prop;
            return Json(new { Result = true, Message = MessageResource.Global_Display_Success });
        }


        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public ActionResult GetProposalDetails(long ProposalId, int vehicleId, bool vehicleLeave = false, int seq = 0)
        {
            //var model = new ProposalCardBL().GetProposalCardDetails(UserManager.UserInfo, ProposalId, seq);
            //ViewBag.VehicleId = vehicleId;
            //ViewBag.IsVehicleLeft = vehicleLeave;

            //return View("_ProposalDetails", model);
            var bl = new ProposalCardBL();
            var model = bl.GetProposalCard(UserManager.UserInfo, ProposalId, seq).Model;
            if (model.TechnicalDesc != null)
                model.TechnicialDescList = model.TechnicalDesc.Split(new[] { "/*,/" }, StringSplitOptions.None);

            if (model.Customer == null || model.Vehicle == null)
            {
                ViewBag.HideElements = true;
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View("_ProposalDetails", model);
            }
            var statsList = bl.ListProposalStats(UserManager.UserInfo).Data;
            ViewBag.ProposalStatsList = model.ProposalStatManualChangeAllow
                ? statsList.Where(c => c.Selected).ToList()
                : statsList;
            if ((UserManager.UserInfo.IsDealer && UserManager.UserInfo.GetUserDealerId() != model.DealerId))
            {
                ViewBag.HideElements = true;
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View("_ProposalDetails", model);
            }

            if (model.ProposalStatId.In(1, 4, 5, 6, 7) || model.IsConvert)
            {
                return View("_ReadOnlyProposalCardIndex", model);
            }

            ViewBag.CampaignCheckList = model.Vehicle.IsCampaignApplicable ? bl.GetCampaignCheckList(UserManager.UserInfo, ProposalId).Data.Where(c => c.IsMust).ToList() : null;
            ViewBag.VehicleId = vehicleId;
            ViewBag.IsVehicleLeft = vehicleLeave;

            return View("_ProposalDetails", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult UpdateVehicleKM(long id, int seq, long? km, bool isHourMaint = false, int hour = 0)
        {
            ViewBag.ProposalId = id;
            ViewBag.ProposalSeq = seq;
            ViewBag.VehicleKM = km;
            ViewBag.IsHourMaint = isHourMaint;
            ViewBag.Hour = hour;
            return View("_UpdateVehicleKM");
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardCancel)]
        public ActionResult CancelProposal(long id,byte seq = 0)
        {
            return View("_CancelProposal", new ProposalCancelModel() { ProposalId = id, ProposalSeq = seq });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardCancel)]
        public ActionResult CancelProposal(ProposalCancelModel model)
        {
            var proposalBL = new ProposalCardBL();

            var c = proposalBL.CancelProposal(UserManager.UserInfo, model);

            if (model.ErrorNo == 1)
            {
                return Json(new { Result = false, Message = model.ErrorMessage });
            }
            return Json(new { Result = true, Message = MessageResource.Global_Display_Success });
        }


        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult UpdateVehiclePlate(long id, string plate)
        {
            ViewBag.ProposalId = id;
            ViewBag.Plate = plate;

            return View("_UpdateVehiclePlate");
        }
        private void SetDefaultCombo()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.CountryList = CommonBL.ListCountries(UserManager.UserInfo).Data;
            //ViewBag.BodyworkList = CommonBL.ListBodyworks();
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
        }
        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddProposalDetail(long? id, int seq)
        {
            //id=>ProposalId
            ViewBag.Seq = seq;
            if (id.HasValue && id.GetValueOrDefault(0) > 0)
            {
                ViewBag.ProposalId = id;
                //var campaignCheckList = new ProposalCardBL().GetCampaignCheckList(UserManager.UserInfo, id ?? 0).Data.Where(c => c.IsMust);


                //if (campaignCheckList.Any(c => c.IsMust))
                //{
                //    ViewBag.HideElements = true;
                //    SetMessage(MessageResource.WorkOrderCard_Display_MandatoryCampaignsMustAdd, CommonValues.MessageSeverity.Fail);
                //}
                //else
                //{
                    ////deny reason check
                    //var model = new ProposalCardBL().CheckForOtherCampaigns(id ?? 0).Model;
                    //if (model.ErrorNo > 0)
                    //{
                    //    ViewBag.HideElements = true;
                    //    CheckErrorForMessage(model, false);
                    //}
                    //else
                    //{

                        ViewBag.HideElements = false;
                        //ViewBag.MainCategoryList =AppointmentIndicatorMainCategoryBL.ListAppointmentIndicatorMainCategories(true);
                        var bo = new ProposalCardBL();
                        ViewBag.FailureCodeList = bo.ListFailureCodes(UserManager.UserInfo).Data;
                        ViewBag.IndicatorTypes = bo.ListIndicatorTypes(UserManager.UserInfo).Data;
                    //}
                //}
            }
            else
            {
                ViewBag.HideElements = true;
            }
            return View("_AddProposalDetail");
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddProposalDetail(AppointmentDetailsViewModel model)
        {
            //ViewBag.HideElements = false;
            if (model.AppointmentId > 0 && model.SubCategoryId > 0)
            {
                //ViewBag.HideElements = true;
                new ProposalCardBL().AddProposalDetail(UserManager.UserInfo, model);
                if (model.ErrorNo == 1)
                {
                    return Json(new { Result = false, Message = model.ErrorMessage });
                }
                return Json(new { Result = true, Message = MessageResource.Global_Display_Success });
            }
            return Json(new { Result = false, Message = MessageResource.WorkOrderCard_Display_SubCategoryError });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddProposalPart(long? id, long proposalDetailId)
        {
            ViewBag.ProposalId = id;
            ViewBag.ProposalDetailId = proposalDetailId;
            ViewBag.LastLevelChecked = false;
            return View("_AddProposalPart");
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddProposalPart(AppointmentDetailsPartsViewModel model, bool LastLevelChecked)
        {
            if (ModelState.IsValid)
            {
                var bl = new ProposalCardBL();
                if (LastLevelChecked)
                {
                    model.PartId = bl.GetLastLevelPartId(model.PartId).Model;
                    bl.AddProposalPart(UserManager.UserInfo, model);
                    CheckErrorForMessage(model, true);
                    return RedirectToAction("AddProposalPart", new { id = model.Id, ProposalDetailId = model.AppointIndicId });
                }
                else
                {
                    var anyLastLevelPart = bl.CheckPartLastLevel(model.AppointIndicId, model.PartId.ToString()).Model;
                    if (anyLastLevelPart.HasValue && anyLastLevelPart == true)
                    {
                        ViewBag.ProposalId = model.Id;
                        ViewBag.ProposalDetailId = model.AppointIndicId;
                        ViewBag.LastLevelChecked = true;
                        ViewBag.ShowConfirmForLastLvl = true;
                        return View("_AddProposalPart", model);
                    }
                    else
                    {
                        bl.AddProposalPart(UserManager.UserInfo, model);
                        CheckErrorForMessage(model, true);
                        return RedirectToAction("AddProposalPart",
                            new { id = model.Id, ProposalDetailId = model.AppointIndicId });
                    }
                }

            }
            ViewBag.ProposalId = model.Id;
            ViewBag.ProposalDetailId = model.AppointIndicId;
            ViewBag.LastLevelChecked = false;
            return View("_AddProposalPart");
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public ActionResult GetLabourData(long labourId, int? vehicleId)
        {
            var data = new ProposalCardBL().GetLabourData(labourId, vehicleId).Model;
            return data == null ? Json(new LabourDataModel { Editable = true, Duration = 100 }) : Json(data);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddProposalLabour(long? id, int ProposalDetailId, int vehicleId)
        {
            ViewBag.HideElements = id <= 0;

            var indicator = new ProposalCardBL().GetDetailData(UserManager.UserInfo, id ?? 0, ProposalDetailId).Model;
            ViewBag.VehicleId = vehicleId;
            ViewBag.Indicator = indicator;
            ViewBag.HideElements = indicator.AppointmentId <= 0;
            return View("_AddProposalLabour", new AppointmentDetailsLaboursViewModel { AppointmentIndicatorId = ProposalDetailId, AppointmentId = id ?? 0 });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddProposalLabour(AppointmentDetailsLaboursViewModel model, int vehicleId)
        {
            if (ModelState.IsValid)
            {
                new ProposalCardBL().AddProposalLabour(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
            }
            var indicator = new ProposalCardBL().GetDetailData(UserManager.UserInfo, model.AppointmentId, model.AppointmentIndicatorId).Model;
            ViewBag.Indicator = indicator;
            ViewBag.VehicleId = vehicleId;
            return View("_AddProposalLabour", model.ErrorNo == 0 ? new AppointmentDetailsLaboursViewModel() : model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddProposalMaint(long? id, long proposalDetailID, int seq, bool hideElement = false)
        {
            ViewBag.HideElements = false;
            var bl = new ProposalCardBL();
            //var campaignCheckList = bl.GetCampaignCheckList(UserManager.UserInfo, id ?? 0).Data.Where(c => c.IsMust);

            //if (campaignCheckList.Any(c => c.IsMust))
            //{
            //    ViewBag.HideElements = true;
            //    SetMessage(MessageResource.WorkOrderCard_Display_MandatoryCampaignsMustAdd, CommonValues.MessageSeverity.Fail);
            //}

            var model = new ProposalMaintenanceModel
            {
                ProposalId = id ?? 0,
                ProposalDetailId = proposalDetailID,
                ProposalSeq = seq
            };           

            var list = bl.GetMaintenance(UserManager.UserInfo, model).Data;

            //if (model.MaintenanceId == 0)
            //{
            //    SetMessage(MessageResource.WorkOrderCard_Display_PeriodicMeintenanceNotFound,
            //        CommonValues.MessageSeverity.Fail);
            //    ViewBag.HideElements = true;
            //}
            if (hideElement)
                ViewBag.HideElements = true;

            return View("_AddProposalMaintenance", list);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddProposalMaint(ProposalMaintenanceModel model)
        {
            var bl = new ProposalCardBL();

            bl.AddProposalMaint(UserManager.UserInfo, model);
            if (model.ErrorNo > 0)
                return Json(new { Message = model.ErrorMessage, Result = false });
            else
                return Json(new { Message = MessageResource.Global_Display_Success, Result = true });

        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult CancelProposalDetail(long? id, long proposalDetailId, int seq)
        {
            ViewBag.HideElements = false;
            var model = new ProposalDetailCancelModel
            {
                ProposalId = id ?? 0,
                ProposalDetailId = proposalDetailId,
                ProposalSeq = seq
            };
            return View("_CancelProposalDetail", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddDetailDiscount(long? id, long proposalDetailId, string type, long itemId)
        {
            var model = new ProposalCardBL().GetProposalDetailItemDataForDiscount(id ?? 0, proposalDetailId, type, itemId);
            return View("_AddDetailDiscount", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddDetailQuantity(long? id, long proposalDetailId, string type, long itemId)
        {
            var model = new ProposalCardBL().GetQuantityData(UserManager.UserInfo, id ?? 0, proposalDetailId, type, itemId).Model;
            return View("_AddDetailQuantity", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult ChangePriceList(long? id, long ProposalDetailId, string type, long itemId, DateTime date)
        {
            var model = new ProposalChangePriceListModel
            {
                ProposalId = id ?? 0,
                ProposalDetailId = ProposalDetailId,
                Type = type,
                ProposalDate = date,
                ItemId = itemId
            };
            return View("_ChangePriceList", model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult ChangePriceList(ProposalChangePriceListModel model)
        {
            if (ModelState.IsValid)
            {

                new ProposalCardBL().ChangePriceList(UserManager.UserInfo, model);
                if (model.ErrorNo == 0)
                {
                    return Json(new { Message = MessageResource.Global_Display_Success, Result = true });
                }
                else
                {
                    return Json(new { Message = model.ErrorMessage, Result = false });
                }
            }

            model.ErrorMessage = MessageResource.Err_Generic_Unexpected;
            return Json(new { Message = model.ErrorMessage, Result = false });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult UpdateQuantity(ProposalQuantityDataModel model)
        {
            if (ModelState.IsValid)
            {

                new ProposalCardBL().UpdateQuantity(UserManager.UserInfo, model);
                if (model.ErrorNo == 0)
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
                }
            }
            if (model.Quantity == 0)
            {
                model.ErrorMessage = string.Format(MessageResource.WorkOrderCard_Validation_EnterQuantity);
            }
            return Json(new { Message = model.ErrorMessage, Result = false });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult RemoveItem(ProposalMaintenanceQuantityDataModel model)
        {
            if (ModelState.IsValid)
            {

                new ProposalCardBL().RemoveMaintenanceItem(UserManager.UserInfo, model);
                if (model.ErrorNo == 0)
                {
                    return Json(new { Message = MessageResource.Global_Display_Success, Result = true });
                }
                else
                {
                    return Json(new { Message = model.ErrorMessage, Result = false });
                }
            }

            model.ErrorMessage = MessageResource.Err_Generic_Unexpected;
            return Json(new { Message = model.ErrorMessage, Result = false });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult ChangePart(ProposalMaintenanceQuantityDataModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.NewPartId) || model.NewPartId == "0")
                    return Json(new { Message = MessageResource.Err_Generic_Unexpected, Result = false });
                //model.ErrorMessage = MessageResource.Err_Generic_Unexpected;
                new ProposalCardBL().ChangePart(UserManager.UserInfo, model);
                if (model.ErrorNo == 0)
                {
                    return Json(new { Message = MessageResource.Global_Display_Success, Result = true });
                }
                else
                {
                    return Json(new { Message = model.ErrorMessage, Result = false });
                }
            }

            model.ErrorMessage = MessageResource.Err_Generic_Unexpected;
            return Json(new { Message = model.ErrorMessage, Result = false });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public JsonResult CancelDetail(ProposalDetailCancelModel model)
        {
            if (ModelState.IsValid)
            {

                new ProposalCardBL().CancelDetail(UserManager.UserInfo, model);
                if (model.ErrorNo == 0)
                {
                    return Json(new { Message = MessageResource.Global_Display_Success, Result = true });
                }
            }
            if (model.CancelReason == null)
            {
                model.ErrorMessage = string.Format(MessageResource.WorkOrderCard_Validation_EnterCancelReason);
            }
            else if (model.CancelReason.Length > 500)
                model.ErrorMessage = string.Format(MessageResource.Validation_Length, 500);

            return Json(new { Message = model.ErrorMessage, Result = false });

        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public JsonResult AddDiscount(ProposalDiscountModel model)
        {
            if (ModelState.IsValid)
            {

                new ProposalCardBL().AddDiscount(UserManager.UserInfo, model);
                if (model.ErrorNo == 0)
                {
                    return Json(new { Message = MessageResource.Global_Display_Success, Result = true });
                }
                return Json(new { Message = model.ErrorMessage, Result = false });


            }

            if (model.DiscountType == ODMSModel.ProposalCard.DiscountType.Money && model.DiscountPrice < 0)
            {
                model.ErrorMessage = MessageResource.WorkOrderCard_Validation_EnterDiscountPrice;
            }
            else if (model.DiscountType == ODMSModel.ProposalCard.DiscountType.Percentage && model.DiscountRatio < 0)
                model.ErrorMessage = MessageResource.WorkOrderCard_Validation_EnterDiscountRatio;
            else
                model.ErrorMessage = MessageResource.Err_Generic_Unexpected;

            return Json(new { Message = model.ErrorMessage, Result = false });

        }

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public JsonResult UpdateVehicleKM(string id, int seq, bool isHourMaint, long km = 0, int hour = 0)
        {
            long newId;
            if (long.TryParse(id, out newId) == false)
                return Json(new { Message = MessageResource.Error_DB_NoRecordFound, Result = false });
            if (isHourMaint && hour == 0)
                return Json(new { Message = MessageResource.WorkOrderCard_Validation_EnterVehicleHour, Result = false });

            int ErrorNo;
            string ErrorMessage;

            long? newKM = new ProposalCardBL().UpdateVehicleKM(UserManager.UserInfo, newId, seq, km, isHourMaint, hour, out ErrorNo, out ErrorMessage).Model;

            if (ErrorNo == 0)
            {
                return Json(new { Message = MessageResource.Global_Display_Success, Result = true, NewKM = newKM });
            }
            return Json(new { Message = ErrorMessage, Result = false });

        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public JsonResult UpdateVehiclePlate(string id, string plate)
        {
            long newId;
            if (long.TryParse(id, out newId) == false)
                return Json(new { Message = MessageResource.Error_DB_NoRecordFound, Result = false });

            plate = plate.ToUpper();
            plate = plate.Trim().Replace(" ", "").Replace("-", "");

            if (String.IsNullOrEmpty(plate))
                return Json(new { Message = MessageResource.WorkOrderCard_Validation_EnterVehiclePlate, Result = false });

            int ErrorNo;
            string ErrorMessage;

            string _plate = new ProposalCardBL().UpdateVehiclePlate(UserManager.UserInfo, newId, plate, out ErrorNo, out ErrorMessage).Model;

            if (ErrorNo == 0)
            {
                return Json(new { Message = MessageResource.Global_Display_Success, Result = true, plate = _plate });

            }
            return Json(new { Message = ErrorMessage, Result = false });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult EditMeintenanceQuantity(long? id, long ProposalDetailId, string type, long itemId, int maintId)
        {
            var model = new ProposalCardBL().GetMaintenanceQuantityData(UserManager.UserInfo, id ?? 0, ProposalDetailId, type, itemId, maintId).Model;
            return View("_EditMeintenanceQuantity", model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public ActionResult ListAlternateParts(long partId = 0, int maintId = 0, long ProposalDetailId = 0)
        {
            return partId == 0 || maintId == 0 ? Json(null) : Json(new ProposalCardBL().ListAlternateParts(UserManager.UserInfo, partId, maintId, ProposalDetailId).Data);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddCampaign(long id, string campaignCode)/*ProposalId*/
        {
            var model = new ProposalCardBL().GetCampaignData(UserManager.UserInfo, id).Model;
            model.ProposalId = id;
            if (!string.IsNullOrEmpty(campaignCode) && model != null)
            {
                if (model.Campaigns.Any())
                {
                    var campaignItem = model.Campaigns.SingleOrDefault(c => c.CampaignCode == campaignCode);
                    if (campaignItem != null)
                    {
                        model.Campaigns.RemoveAll(c => c.CampaignCode != campaignCode);
                    }
                }
            }

            return View("_AddCampaign", model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddCampaign(ProposalCampaignModel model)
        {
            //custom validation
            if (model.ProposalId == 0 || model.Campaigns == null || string.IsNullOrEmpty(model.Campaigns.First().CampaignCode))
                return Json(new { Message = MessageResource.Err_Generic_Unexpected, Result = false });

            new ProposalCardBL().AddCampaign(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return Json(new { Message = MessageResource.Global_Display_Success, Result = true });

            return Json(new { Message = model.ErrorMessage, Result = false });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public ActionResult GetCampaignDetail(string campaignCode, string type, long id = 0)
        {
            if (string.IsNullOrEmpty(campaignCode) || string.IsNullOrEmpty(type) || !(type == "LABOUR" || type == "PART" || type == "DOCUMENT"))
            {
                return new ContentResult() { Content = MessageResource.Error_DB_NoRecordFound, ContentType = "text/html" };
            }
            switch (type)
            {
                case "LABOUR":
                    return View("_CampaignDetailLabour", new ProposalCardBL().GetCampaignLabours(UserManager.UserInfo, campaignCode, id).Data);
                case "PART":
                    return View("_CampaignDetailPart", new ProposalCardBL().GetCampaignParts(UserManager.UserInfo, campaignCode).Data);
                case "DOCUMENT":
                    return View("_CampaignDetailDocument", new ProposalCardBL().GetCampaignDocuments(UserManager.UserInfo, campaignCode).Data);
                default:
                    return new ContentResult()
                    {
                        Content = MessageResource.Error_DB_NoRecordFound,
                        ContentType = "text/html"
                    };
            }
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public ActionResult VehicleNotes(long id = 0)
        {
            ViewBag.ProposalId = id;
            return View("_VehicleNotes");
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public ActionResult GetVehicleNote(int id, long ProposalId)
        {
            return View("_VehicleNoteDetail", new ProposalCardBL().GetVehicleNote(UserManager.UserInfo, id, ProposalId).Model);
        }

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //[HttpPost]
        //[AuthorizationFilter(Permission.ProposalCardIndex)]
        //public ActionResult GetVehicleNotePopup(int id, long ProposalId)
        //{
        //    return View("_VehicleNoteDetail", new ProposalCardBL().GetVehicleNotePopup(id, ProposalId));
        //}

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult SaveVehicleNote(long ProposalId, string note)
        {
            if (ProposalId == 0 || string.IsNullOrEmpty(note) || note.Length == 0 || note.Length > 500)
                return Json(new { Message = MessageResource.Err_Generic_Unexpected, Result = false });

            var model = new ProposalVehicleNoteModel
            {
                ProposalId = ProposalId,
                Note = note
            };

            new ProposalCardBL().AddVehicleNote(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return Json(new { Message = MessageResource.WorkOrderCard_Message_NoteSendToApproval, Result = true });

            return Json(new { Message = model.ErrorMessage, Result = false });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public JsonResult SearchParts(string searchText)
        {
            var list = SparePartBL.ListSparePartAsAutoCompSearch(UserManager.UserInfo, searchText, null).Data.Select(x => new ComboBoxModel { Text = string.Format("{0} / {1}", x.Column1, x.Column2), Value = x.Id });
            return Json(list);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult UpdateFailureCode(long id, long ProposalDetailId, string code)/*ProposalId*/
        {
            ViewBag.ProposalId = id;
            ViewBag.ProposalDetailId = ProposalDetailId;
            ViewBag.FailureCode = !string.IsNullOrEmpty(code) && code.Contains("_")
                ? code.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries).Last()
                : string.Empty;
            ViewBag.FailureCodeList = new ProposalCardBL().ListFailureCodes(UserManager.UserInfo).Data;
            return View("_UpdateFailureCode");
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult FailureCodeUpdate(long ProposalId, long ProposalDetailId, string failureCode)
        {
            if (ProposalId == 0 || ProposalDetailId == 0) /*|| string.IsNullOrEmpty(failureCode))*/
                return Json(new { Message = MessageResource.Err_Generic_Unexpected, Result = false });

            int errorNo;
            string errorMessage;

            new ProposalCardBL().UpdateFailureCode(UserManager.UserInfo, ProposalId, ProposalDetailId, failureCode, out errorNo, out errorMessage);

            if (errorNo == 0)
                return Json(new { Message = MessageResource.Global_Display_Success, Result = true });

            return Json(new { Message = errorMessage, Result = false });
        }


        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult UpdateDuration(ProposalQuantityDataModel model)
        {
            if (ModelState.IsValid)
            {

                new ProposalCardBL().UpdateDuration(UserManager.UserInfo, model);
                if (model.ErrorNo == 0)
                {
                    return Json(new { Message = MessageResource.Global_Display_Success, Result = true });
                }
            }
            if (model.Quantity == 0)
            {
                model.ErrorMessage = string.Format(MessageResource.WorkOrderCard_Validation_EnterdDuration);
            }
            return Json(new { Message = model.ErrorMessage, Result = false });
        }


        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public ActionResult ListDetailProcessTypes(string id, long? ProposalId, int seq)
        {
            return string.IsNullOrEmpty(id) || ProposalId.GetValueOrDefault(0) == 0
                ? Json(null)
                : Json(new ProposalCardBL().ListDetailProcessTypes(UserManager.UserInfo, id, ProposalId.GetValueOrDefault(), seq).Data);
        }



        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddTechnicalDetail(long id, long ProposalDetailId)
        {
            ViewBag.DetailId = ProposalDetailId;
            var model = new ProposalCardBL().GetProposalDetailDescription(ProposalDetailId).Model;
            return PartialView("_AddTechnicalDetail", model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddTechnicalDetail(long ProposalDetailId, string note)
        {
            var model = new ProposalCardBL().UpdateProposalDetailDescription(UserManager.UserInfo, ProposalDetailId, note).Model;
            return
                Json(new
                {
                    Result = model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success
                });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult UpdateContactInfo(long id)
        {
            ViewBag.Id = id;
            var model = new ProposalCardBL().GetProposalContactInfo(id).Model;
            return PartialView("_UpdateContactInfo", model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult UpdateContactInfo(long id, string note)
        {
            var model = new ProposalCardBL().UpdateProposalContactInfo(UserManager.UserInfo, id, note).Model;
            return
                Json(new
                {
                    Result = model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success
                });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult UpdateCampaignDenyReason(long id, string note)
        {
            var model = new ProposalCardBL().UpdateCampaignDenyReason(UserManager.UserInfo, id, note).Model;
            return
                Json(new
                {
                    Result = model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success
                });
        }



        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult UpdateCampaignDenyReason(long id)
        {
            ViewBag.Id = id;
            var model = new ProposalCardBL().GetProposalCampaignDenyReason(id).Model;
            return PartialView("_UpdateCampaignDenyReason", model);
        }



        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult ReturnParts(long id, long ProposalDetailId)
        {
            ViewBag.DetailId = ProposalDetailId;
            var model = new ProposalCardBL().ListDetailPartReturnItems(UserManager.UserInfo, ProposalDetailId).Data;
            return View("_PartsReturn", model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult ReturnParts(List<PartReturnModel> list, long ProposalDetailId)
        {
            var model = new ProposalCardBL().ReturnDetailParts(UserManager.UserInfo, list, ProposalDetailId).Data;
            return
                Json(new
                {
                    Result =
                        model.Any(c => c.ErrorNo > 0) ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.Any(c => c.ErrorNo > 0) ? model.FirstOrDefault().ErrorMessage : MessageResource.Global_Display_Success
                });
        }


        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult PickDetailParts(long id, long ProposalDetailId)
        {
            var model = new ProposalCardBL().PickDetailParts(UserManager.UserInfo, id, ProposalDetailId).Model;
            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
                });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult SaveProposalStat(long ProposalId, int ProposalStatId, int seq)
        {
            var model = new ProposalCardBL().ChangeProposalStat(UserManager.UserInfo, ProposalId, ProposalStatId, seq).Model;
            CheckErrorForMessage(model, true);
            return RedirectToAction("ProposalCardIndex", new { id = ProposalId });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult CreateCampaignRequest(long ProposalDetailId)
        {
            var model = new ProposalCardBL().CreateCampaignRequest(UserManager.UserInfo, ProposalDetailId).Model;
            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
                });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult CampaignRequestDetails(long ProposalDetailId, long campaignRequestId)
        {
            var campaignRequest = new ProposalCampaignRequestViewModel { IdCampaignRequest = campaignRequestId };

            var model = new ProposalCardBL().ListCampaignRequestDetails(UserManager.UserInfo, ProposalDetailId, campaignRequest).Data;

            ViewBag.CampaignRequest = campaignRequest;
            return PartialView("_CampaignRequestDetails", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult UpdateProcessType(long ProposalDetailId)
        {
            ViewBag.DetailId = ProposalDetailId;
            var model = new ProposalCardBL().GetProcessTypeData(UserManager.UserInfo, ProposalDetailId).Model;
            return PartialView("_ChangeProcessType", model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult UpdateProcessType(long ProposalDetailId, string ProcessType, bool confirmed)
        {
            ViewBag.DetailId = ProposalDetailId;
            var model = new ProposalCardBL().UpdateProcessType(UserManager.UserInfo, ProposalDetailId, ProcessType, confirmed).Model;
            return Json(new { ErrorNo = model.ErrorNo, Message = model.ErrorNo == 0 ? MessageResource.Global_Display_Success : model.ErrorMessage });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult CheckVehicleLeave(long id)
        {
            var vehicleLeaveDate = new ProposalCardBL().GetVehicleLeaveDate(proposalId: id).Model;
            return Json(new { VehicleLeaveDate = vehicleLeaveDate }, "application/json");

        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult CheckVehicleLeaveStep2(long id)
        {
            var isValid = new ProposalCardBL().CheckVehicleLeaveMandatoryFields(UserManager.UserInfo, proposalId: id).Model;
            return Json(new { IsValid = isValid }, "application/json");

        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult MustRemovedParts(long id)
        {
            var list = new ProposalCardBL().ListMustRemovedParts(UserManager.UserInfo, proposalId: id).Data;
            return PartialView("_CheckVehicleLeave", list);

        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult PartRemovalInfo(long ProposalDetailId, long partId)
        {
            var bl = new ProposalCardBL();
            var dto = bl.GetPartRemovalDto(UserManager.UserInfo, ProposalDetailId, partId).Model;
            ViewBag.RemovablePartList = bl.ListRemovableParts(UserManager.UserInfo, partId).Data;
            return PartialView("_PartRemoval", dto);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult UpdateRemovedPartInfo(ProposalPartRemovalDto dto)
        {
            if (ModelState.IsValid)
            {
                var model = new ProposalCardBL().UpdateRemovalInfo(UserManager.UserInfo, dto).Model;
                return
                    Json(
                        new
                        {
                            ErrorNo = model.ErrorNo,
                            Message = model.ErrorNo == 0 ? MessageResource.Global_Display_Success : model.ErrorMessage
                        });
            }
            return
                   Json(
                       new
                       {
                           ErrorNo = 1,
                           Message = MessageResource.Err_Generic_Unexpected
                       });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddPdiPackage(ProposalAddPdiPackageModel model)
        {
            new ProposalCardBL().AddPdiPackage(UserManager.UserInfo, model);
            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
                });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult UpdatePdiPackage(ProposalAddPdiPackageModel model)
        {
            new ProposalCardBL().UpdatePdiPackage(UserManager.UserInfo, model);
            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
                });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public ActionResult GetProposalVehicleLeave(long id)
        {

            var user = UserManager.UserInfo;
            if (user.IsDealer)
            {
                bool checkDealer = new ProposalCardBL().CheckProposalDealer(id, user.GetUserDealerId()).Model;
                if (!checkDealer)
                {
                    SetMessage(MessageResource.ErrorVehicleLeaveInvoice, CommonValues.MessageSeverity.Fail);
                    return ProposalCardIndex(id);
                }
            }
            var stream = ReportManager.GetReport(ReportType.VehicleLeaveReport, id);
            if (stream != null && stream.Length > 0)
            {
                var filename = string.Format("Araç_Teslimat_Fişi_{0}.pdf", id);
                return File(stream, "application.pdf", filename);
            }
            else
            {
                SetMessage(MessageResource.ErrorVehicleLeaveReport, CommonValues.MessageSeverity.Fail);
                return ProposalCardIndex(id);
            }
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddPdiPackage(long id)
        {
            var model = new ProposalAddPdiPackageModel { ProposalId = id };
            return PartialView("_AddPdiPackage", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult UpdatePdiPackage(long id)
        {
            var model = new ProposalCardBL().GetPdiPackageData(id).Model;
            return PartialView("_UpdatePdiPackage", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult OpenPdiPackageResults(long id)
        {
            return PartialView("_PdiResults", id);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult GetPdiPackageResults(long id)
        {
            var bo = new ProposalCardBL();
            ViewBag.Id = id;
            ViewBag.IsControlled = bo.GetPdiVehicleIsControlled(id).Model;

            return PartialView("_PdiResultItems", bo.ListPdiResultItems(UserManager.UserInfo, id));
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult OpenPdiPackageResult(long id, string controlCode)
        {

            var tuple = new ProposalCardBL().GetPdiResultData(UserManager.UserInfo, id, controlCode);
            ViewBag.ControlCode = controlCode;
            ViewBag.ControlName = tuple.Item2;
            ViewBag.PartList = tuple.Item3;
            ViewBag.BreakDownList = tuple.Item4;
            ViewBag.ResultList = tuple.Item5;
            return PartialView("_PdiResultItemEdit", new ProposalPdiResultModel { ProposalId = id, ControlCode = controlCode });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult SavePdiResult(ProposalPdiResultModel dto)
        {
            if (ModelState.IsValid)
            {
                var model = new ProposalCardBL().SavePdiResult(UserManager.UserInfo, dto, "U").Model;
                return
                    Json(
                        new
                        {
                            Result = model.ErrorNo == 0,
                            Message = model.ErrorNo == 0 ? MessageResource.Global_Display_Success : model.ErrorMessage
                        });
            }
            return
                   Json(
                       new
                       {
                           Result = false,
                           Message = MessageResource.Err_Generic_Unexpected
                       });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult ClearPdiResult(ProposalPdiResultModel dto)
        {
            var model = new ProposalCardBL().SavePdiResult(UserManager.UserInfo, dto, "C").Model;
            return
                Json(
                    new
                    {
                        Result = model.ErrorNo == 0,
                        Message = model.ErrorNo == 0 ? MessageResource.Global_Display_Success : model.ErrorMessage
                    });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult PdiSendToApproval(long id)
        {
            var model = new ProposalCardBL().PdiSendToApproval(UserManager.UserInfo, id).Model;
            return
                Json(
                    new
                    {
                        Result = model.ErrorNo == 0,
                        Message = model.ErrorNo == 0 ? MessageResource.Global_Display_Success : model.ErrorMessage
                    });
        }

        //[HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult GetPdiPackageDetails(long id)
        {
            ViewBag.Id = id;
            return PartialView("_PdiDetails", new ProposalCardBL().GetPdiPackageDetails(UserManager.UserInfo, id).Model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult PrintPdiPackageDetails(long id)
        {
            return PartialView("_PdiPrint", id);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult DeleteDetailItem(long? id, long ProposalDetailId, string type, long itemId)
        {
            var model = new ProposalCardBL().DeleteDetailItem(UserManager.UserInfo, id ?? 0, ProposalDetailId, type, itemId).Model;
            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
                });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult PrintProposalInvoice(long id, long invoiceId, ProposalInvoicePrintType printType)
        {
            var user = UserManager.UserInfo;
            var data = new ProposalInvoicesBL().GetProposalInvoice(UserManager.UserInfo, invoiceId).Model;
            if (data == null)
            {
                SetMessage(MessageResource.ErrorVehicleLeaveInvoice, CommonValues.MessageSeverity.Fail);
                return ProposalCardIndex(id);
            }
            if (id == 0)
            {
                if (data.ProposalId > 0)
                    id = data.ProposalId;
                else
                {
                    if (!string.IsNullOrEmpty(data.ProposalIds))
                    {
                        id = int.Parse(data.ProposalIds.Split(',').First());
                    }
                }
            }

            if (user.IsDealer)
            {
                bool checkDealer = new ProposalCardBL().CheckProposalDealer(id, user.GetUserDealerId(), invoiceId).Model;
                if (!checkDealer)
                {
                    SetMessage(MessageResource.ErrorVehicleLeaveInvoice, CommonValues.MessageSeverity.Fail);
                    return ProposalCardIndex(id);
                }
            }


            var invType = (ProposalInvoiceType)0;
            if (data.InvoiceTypeId == 1) invType = ProposalInvoiceType.Dokumlu;
            if (data.InvoiceTypeId == 2) invType = ProposalInvoiceType.UcKirilimli;
            if (data.InvoiceTypeId == 3) invType = ProposalInvoiceType.Ozel;
            //var invoice = InvoiceFactory.Create(invType, invoiceId, data.HasWitholding);
            var stream = ReportManager.GetReport(ReportType.VehicleInvoiceReport, invType, invoiceId, data.HasWitholding,
                printType);
            if (stream != null && stream.Length > 0)
            {
                string filename = string.Empty;

                switch (printType)
                {
                    case ProposalInvoicePrintType.Printed:
                        filename = string.Format(MessageResource.ProposalInvoice_Report_Invoice, invoiceId);
                        break;
                    case ProposalInvoicePrintType.Transcript:
                        filename = string.Format(MessageResource.ProposalInvoice_Report_InvoiceTranscript, invoiceId);
                        break;
                    case ProposalInvoicePrintType.Proforma:
                        filename = string.Format(MessageResource.ProposalInvoice_Report_InvoiceProforma, invoiceId);
                        break;
                    case ProposalInvoicePrintType.ProformaExcel:
                        filename = string.Format(MessageResource.ProformaInvoice_Report_InvoiceProformaExcel, invoiceId);
                        break;
                    case ProposalInvoicePrintType.ProposalAndProforma:
                        var ProposalReport = ReportManager.GetReport(ReportType.ProposalPrintFirstPart, id);
                        using (var second = new MemoryStream(stream))
                        using (var first = new MemoryStream(ProposalReport))
                        using (PdfDocument targetDoc = new PdfDocument())
                        {
                            using (PdfDocument pdfDoc = PdfReader.Open(first, PdfDocumentOpenMode.Import))
                            {
                                for (int i = 0; i < pdfDoc.PageCount; i++)
                                {
                                    targetDoc.AddPage(pdfDoc.Pages[i]);
                                }
                            }
                            using (PdfDocument pdfDoc = PdfReader.Open(second, PdfDocumentOpenMode.Import))
                            {
                                for (int i = 0; i < pdfDoc.PageCount; i++)
                                {
                                    targetDoc.AddPage(pdfDoc.Pages[i]);
                                }
                            }
                            var ms = new MemoryStream();
                            targetDoc.Save(ms, false);
                            return File(ms.ToArray(), MimeMapping.GetMimeMapping("a.pdf"), String.Format("Döküm_ve_Fatura_{0}.pdf", id));

                        }
                }
                return File(stream, MimeMapping.GetMimeMapping(filename), filename);
            }
            SetMessage(MessageResource.ErrorVehicleLeaveInvoice, CommonValues.MessageSeverity.Fail);
            return ProposalCardIndex(id);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult GetVehicleHistoryToolTipContent(int id)
        {
            var content = new ProposalCardBL().GetVehicleHistoryToolTipContent(vehicleId: id);
            return Content(content, "html", Encoding.UTF8);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult VehicleDetails(int id)
        {
            var model = new VehicleIndexViewModel { VehicleId = id };
            new VehicleBL().GetVehicle(UserManager.UserInfo, model);
            return PartialView("_VehicleInfo", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult OpenPeriodicMaintControlList(int id)/*Vehicle Type Id*/
        {
            var total = 0;
            var list =
                new PeriodicMaintControlListBL().ListPeriodicMaintControlList(UserManager.UserInfo,
                    new PeriodicMaintControlListListModel() { IdType = id, LanguageCustom = UserManager.LanguageCode },
                    out total).Data;
            return PartialView("_PeriodicMaintControlList", list);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public ActionResult ApplicableFleet(int id = 0)/*fleetId*/
        {
            if (id > 0)
            {
                var model = new FleetViewModel { IdFleet = id };
                new FleetBL().GetFleet(UserManager.UserInfo, model);
                return PartialView("_FleetInfo", model);
            }
            return new HttpNotFoundResult();
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public ActionResult AddHourMaint(long id, int vehicleId)
        {
            ViewBag.MaintList = new ProposalCardBL().ListVehicleHourMaints(UserManager.UserInfo, vehicleId).Data;
            return View("_HourMaint", id);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddHourMaintenance(long ProposalId, int MaintId)
        {
            var model = new ProposalMaintenanceModel
            {
                MaintenanceId = MaintId,
                ProposalId = ProposalId
            };

            new ProposalCardBL().AddProposalMaint(UserManager.UserInfo, model);

            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
                });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult CompletePdiVehicleResult(long id) /*ProposalId*/
        {
            var model = new ProposalCardBL().CompletePdiVehicleResult(UserManager.UserInfo, id).Model;
            return
               Json(new
               {
                   Result =
                       model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                   Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
               });

        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult PickingCancellation(long id) /*ProposalId*/
        {
            ViewBag.Id = id;
            var model = new ProposalCardBL().ListPickingsForCancellation(UserManager.UserInfo, id).Data;
            return PartialView("_PickingCancellation", model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult CancelPicking(long pickingId, long ProposalId)
        {
            new ProposalCardBL().CancelPicking(UserManager.UserInfo, pickingId, ProposalId);
            return RedirectToAction("PickingCancellation", new { id = ProposalId });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.CancelVehicleLeave)]
        public ActionResult CancelVehicleLeave(long id)
        {
            return PartialView("_CancelVehicleLeave", id);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.CancelVehicleLeave)]
        public ActionResult CancelVehicleLeave(long ProposalId, string reason)
        {
            var model = new ProposalCardBL().CancelVehicleLeave(UserManager.UserInfo, ProposalId, reason).Model;
            return
               Json(new
               {
                   Result =
                       model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                   Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
               });
        }

        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult ApproveProposal(long id, int seq)
        {
            var model = new ProposalCardBL().ChangeProposalStat(UserManager.UserInfo, id, 4, seq).Model;

            if (model.ErrorNo == 0)
                return Json(new { Message = MessageResource.Global_Display_Success, Result = true });

            return Json(new { Message = model.ErrorMessage, Result = false });
        }

        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult ProposalRevision(int id, int seq)
        {
            var model = new ProposalCardBL().ProposalRevision(UserManager.UserInfo, id, seq).Model;
            return RedirectToAction("ProposalCardIndex", new { id = model.ProposalId, seq = model.ProposalSeq });

        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public ActionResult DealerNotes(long id = 0, int seq = 0)
        {
            ViewBag.ProposalId = id;
            ViewBag.Seq = seq;
            return View("_DealerNotes");
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public ActionResult VehicleNotesProposal(long id = 0, int seq = 0)
        {
            ViewBag.ProposalId = id;
            ViewBag.Seq = seq;
            return View("_VehicleNotesProposal");
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex)]
        public ActionResult GetDealerNote(int id, long proposalId, int seq)
        {
            return View("_DealerNoteDetail", new ProposalCardBL().GetDealerNote(UserManager.UserInfo, id, proposalId, seq).Model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult SaveDealerNote(long ProposalId, int seq, string note)
        {
            if (ProposalId == 0 || string.IsNullOrEmpty(note) || note.Length == 0 || note.Length > 500)
                return Json(new { Message = MessageResource.Err_Generic_Unexpected, Result = false });
            var model = new ProposalVehicleNoteModel
            {
                ProposalId = ProposalId,
                ProposalSeq = seq,
                Note = note
            };

            new ProposalCardBL().AddDealerNote(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return Json(new { Message = MessageResource.Global_Display_Success, Result = true });

            return Json(new { Message = model.ErrorMessage, Result = false });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult CreateSparePartSale(int id, int seq)
        {
            var model = new ProposalCardBL().GetProposalCard(UserManager.UserInfo, id, seq).Model;
            using (var ts = new TransactionScope())
            {
                var master = CreateMaster(model);
                if (master.ErrorNo > 0)
                    return Json(new { Message = master.ErrorMessage, Result = false });
                else
                {
                    var details = CreateParts(model, master.SparePartSaleId);
                    if (details.ErrorNo > 0)
                    {
                        CheckErrorForMessage(model, false);
                        return Json(new { Message = MessageResource.Global_Display_Error, Result = false });
                    }
                }
                new ProposalCardBL().ChangeProposalStat(UserManager.UserInfo, id, 7, seq);
                new ProposalCardBL().UpdateProposalSparePartId(id, master.SparePartSaleId, seq);
                ts.Complete();
            }
            return Json(new { Message = MessageResource.Global_Display_Success, Result = true });
        }

        private SparePartSaleViewModel CreateMaster(ProposalCardModel sourceModel)
        {
            SparePartSaleViewModel model = new SparePartSaleViewModel();
            model.CustomerId = sourceModel.Customer.CustomerId;
            model.CustomerTypeId = sourceModel.Customer.CustomerTypeId;
            model.VehicleId = sourceModel.Vehicle.VehicleId;
            CustomerIndexViewModel customerModel = new CustomerIndexViewModel();
            customerModel.CustomerId = model.CustomerId;

            CustomerBL customerBo = new CustomerBL();
            customerBo.GetCustomer(UserManager.UserInfo, customerModel);

            model.CustomerTypeId = customerModel.CustomerTypeId;
            DealerBL dealerBo = new DealerBL();
            DealerViewModel dModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;

            var bo = new SparePartSaleBL();
            VehicleIndexViewModel vModel = new VehicleIndexViewModel();
            vModel.VehicleId = model.VehicleId.GetValue<int>();

            VehicleBL vBo = new VehicleBL();
            vBo.GetVehicle(UserManager.UserInfo, vModel);

            model.VatExclude = vModel.VatExcludeType;
            model.PriceListId = vModel.IdPriceList;
            model.StockTypeId = 1;
            model.SaleDate = DateTime.Now;
            model.CommandType = CommonValues.DMLType.Insert;
            model.IsReturn = false;
            model.SaleStatusLookVal = ((int)CommonValues.SparePartSaleStatus.NewRecord).ToString();

            bo.DMLSparePartSale(UserManager.UserInfo, model);
            return model;
        }

        private SparePartSaleDetailDetailModel CreateParts(ProposalCardModel sourceModel, int partSaleId)
        {
            foreach (var item in sourceModel.Details.Where(x => x.Type == "PART"))
            {
                var model = GetValues(item.Id.ToString(), partSaleId.ToString(), "1");
                model.SparePartId = item.Id;
                model.PartSaleId = partSaleId;
                model.StatusId = 1;
                model.PlanQuantity = item.Quantity;
                var bo = new SparePartSaleDetailBL();
                CommonBL cbo = new CommonBL();
                DealerBL dealerBo = new DealerBL();
                SparePartBL spBo = new SparePartBL();


                if (model.SparePartId != null)
                {
                    SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                    spModel.PartId = model.SparePartId.GetValue<int>();

                    spBo.GetSparePart(UserManager.UserInfo, spModel);
                    model.SparePartName = spModel.PartCode + CommonValues.Slash + spModel.AdminDesc;

                    StockCardBL scBo = new StockCardBL();
                    model.DealerPrice = scBo.GetDealerPriceByDealerAndPart(spModel.PartId,
                                                                           UserManager.UserInfo.GetUserDealerId()).Model;

                    model.ListPrice = cbo.GetPriceByDealerPartVehicleAndType(spModel.PartId, 0,
                                                                             UserManager.UserInfo.GetUserDealerId(),
                                                                             CommonValues.ListPriceLabel).Model;

                    DealerViewModel dealerViewModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
                    CountryVatRatioBL vatRatioBo = new CountryVatRatioBL();
                    model.VatRatio = vatRatioBo.GetVatRatioByPartAndCountry(spModel.PartId, dealerViewModel.Country).Model;
                }
                if (model.DiscountPrice > model.ListPrice)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.SparePartSaleDetail_Warning_DiscountPriceListPriceControl;
                    return model;
                }

                int totalCount = 0;
                SparePartSaleDetailListModel lModel = new SparePartSaleDetailListModel();
                lModel.PartSaleId = model.PartSaleId;
                SparePartSaleDetailBL spdBo = new SparePartSaleDetailBL();
                List<SparePartSaleDetailListModel> detailList = spdBo.ListSparePartSaleDetails(UserManager.UserInfo, lModel, out totalCount).Data;

                if (totalCount != 0)
                {
                    var control = (from s in detailList.AsEnumerable()
                                   where s.SparePartId == model.SparePartId
                                   select s);
                    if (control.Any())
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.SparePartSaleDetail_Warning_SamePartExists;
                        return model;
                    }
                }

                DealerViewModel dealerModel = dealerBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
                model.CurrencyCode = dealerModel.CurrencyCode;
                model.CommandType = CommonValues.DMLType.Insert;

                bo.DMLSparePartSaleDetail(UserManager.UserInfo, model);
                if (model.ErrorNo != 0)
                {
                    return model;
                }
                model = new SparePartSaleDetailDetailModel
                {
                    PartSaleId = model.PartSaleId,
                    StatusId = (int)CommonValues.SparePartSaleDetailStatus.NoCollectOrder
                };

                return model;
            }
            return null;
        }
        public SparePartSaleDetailDetailModel GetValues(string partId, string partSaleId, string stockTypeId)
        {
            decimal shipmentQuantity = 0;
            int pickingQuantity = 0;
            int totalCount = 0;
            string currencyCode = string.Empty;
            string unitName = string.Empty;
            decimal profitMarginRatio = 0;
            decimal vatRatio = 0;
            decimal listPrice = 0;
            decimal dealerPrice = 0;
            decimal discountPrice = 0;
            decimal freeQuantity = 0;
            decimal discountRatio = 0;
            decimal stockQuantity = 0;
            int dealerId = UserManager.UserInfo.GetUserDealerId();

            if (!string.IsNullOrEmpty(partId))
            {
                SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                spModel.PartId = partId.GetValue<int>();

                SparePartBL spBo = new SparePartBL();
                spBo.GetSparePart(UserManager.UserInfo, spModel);
                shipmentQuantity = spModel.ShipQuantity.GetValue<decimal>();
                unitName = spModel.UnitName;

                if (!string.IsNullOrEmpty(stockTypeId))
                {
                    discountPrice = spBo.GetDiscountPrice(partId.GetValue<int>(), dealerId, stockTypeId.GetValue<int>()).Model;
                    freeQuantity = spBo.GetFreeQuantity(partId.GetValue<int>(), dealerId, stockTypeId.GetValue<int>()).Model;
                }

                StockTypeDetailListModel stdListModel = new StockTypeDetailListModel();
                stdListModel.IdPart = spModel.PartId;
                stdListModel.IdStockType = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.StockType).Model.GetValue<int>();
                stdListModel.IdDealer = dealerId;

                StockTypeDetailBL stdBo = new StockTypeDetailBL();
                List<StockTypeDetailListModel> stockList = stdBo.ListStockTypeDetail(UserManager.UserInfo, stdListModel, out totalCount).Data;

                if (totalCount != 0)
                {
                    stockQuantity = (from s in stockList.AsEnumerable()
                                     select s.StockQuantity).Sum()
                                                            .GetValue
                        <decimal>();
                }

                StockCardViewModel scModel = new StockCardViewModel();
                scModel.PartId = partId.GetValue<int>();
                scModel.DealerId = dealerId;

                StockCardBL scBo = new StockCardBL();
                scModel = scBo.GetStockCard(UserManager.UserInfo, scModel).Model;

                dealerPrice = scBo.GetDealerPriceByDealerAndPart(partId.GetValue<int>(), dealerId).Model;
                profitMarginRatio = scModel.ProfitMarginRatio.GetValue<decimal>();

                DealerBL dealerBo = new DealerBL();
                DealerViewModel dealerViewModel = dealerBo.GetDealer(UserManager.UserInfo, dealerId).Model;
                currencyCode = dealerViewModel.CurrencyCode;

                CountryVatRatioBL vatRatioBo = new CountryVatRatioBL();
                vatRatio = vatRatioBo.GetVatRatioByPartAndCountry(partId.GetValue<int>(), dealerViewModel.Country).Model;

                CommonBL bo = new CommonBL();
                listPrice = bo.GetPriceByDealerPartVehicleAndType(partId.GetValue<int>(), 0, dealerId, CommonValues.ListPriceLabel).Model;

                if (!string.IsNullOrEmpty(partSaleId))
                {
                    WorkOrderPickingListModel woModel = new WorkOrderPickingListModel();
                    woModel.PartSaleId = partSaleId.GetValue<int>();

                    WorkOrderPickingBL woBo = new WorkOrderPickingBL();
                    List<WorkOrderPickingListModel> woList = woBo.ListWorkOrderPicking(UserManager.UserInfo, woModel, out totalCount).Data;

                    if (totalCount != 0)
                    {
                        WorkOrderPickingDetailListModel wodModel = new WorkOrderPickingDetailListModel();
                        wodModel.WOPMstId = woList.ElementAt(0).WorkOrderPickingId;
                        WorkOrderPickingDetailBL wodBo = new WorkOrderPickingDetailBL();

                        List<WorkOrderPickingDetailListModel> detailList = wodBo.ListWorkOrderPickingDetail(UserManager.UserInfo, wodModel, out totalCount).Data;
                        if (totalCount != 0)
                        {
                            pickingQuantity = detailList.ElementAt(0).PickQuantity.GetValue<int>();
                        }
                    }

                    SparePartSaleBL spsBo = new SparePartSaleBL();
                    SparePartSaleViewModel spsModel = spsBo.GetSparePartSale(UserManager.UserInfo, partSaleId.GetValue<int>()).Model;

                    int customerId = spsModel.CustomerId;
                    CustomerDiscountListModel cdModel = new CustomerDiscountListModel();
                    cdModel.IdCustomer = customerId;
                    cdModel.IdDealer = UserManager.UserInfo.GetUserDealerId();
                    CustomerDiscountBL cdBo = new CustomerDiscountBL();

                    List<CustomerDiscountListModel> cdList = cdBo.ListCustomerDiscount(UserManager.UserInfo, cdModel, out totalCount).Data;
                    if (totalCount != 0)
                    {
                        discountRatio = cdList.ElementAt(0).PartDiscountRatio.GetValue<decimal>();
                    }
                }
            }
            else
            {
                return null;
            }
            return new SparePartSaleDetailDetailModel
            {
                CurrencyCode = currencyCode,
                PlanQuantity = pickingQuantity,
                ShipmentQuantity = shipmentQuantity,
                ProfitMarginRatio = profitMarginRatio,
                VatRatio = vatRatio,
                ListPrice = listPrice,
                DealerPrice = dealerPrice,
                DiscountPrice = discountPrice,
                DiscountRatio = discountRatio,
                StockQuantity = stockQuantity
            };
        }

        [HttpPost]
        public ActionResult SaveGeneralInfos(GeneralInfo model)
        {
            new ProposalCardBL().SaveGeneralInfo(UserManager.UserInfo, model);
            return RedirectToAction("ProposalCardIndex", routeValues: new { id = model.ProposalId, seq = model.ProposalSeq });
        }

        [HttpPost]
        public ActionResult SaveTechnicianInfo(GeneralInfo model)
        {
            new ProposalCardBL().SaveTechnicalInfo(UserManager.UserInfo, model);
            return RedirectToAction("ProposalCardIndex", routeValues: new { id = model.ProposalId, seq = model.ProposalSeq });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddLabourPrice(long id, long workOrderDetailId, string type, long itemId)
        {

            var model = new AddLabourPrice
            {
                WorkOrderId = id,
                WorkOrderDetailId = workOrderDetailId,
                Type = type,
                ItemId = itemId,
                UnitPrice = new ProposalCardBL().GetProposalDetailItemDataForDiscount(id, workOrderDetailId, type, itemId).ListPrice
            };
            return View("_AddLabourPrice", model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalCardIndex, Permission.ProposalCardUpdate)]
        public ActionResult AddLabourPrice(AddLabourPrice model)
        {
            if (ModelState.IsValid)
            {

                new ProposalCardBL().AddLabourPrice(UserManager.UserInfo, model);
                if (model.ErrorNo == 0)
                {
                    return Json(new { Message = MessageResource.Global_Display_Success, Result = true });
                }
                else
                {
                    return Json(new { Message = model.ErrorMessage, Result = false });
                }
            }

            model.ErrorMessage = MessageResource.Err_Generic_Unexpected;
            return Json(new { Message = model.ErrorMessage, Result = false });
        }
    }
}