using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSBusiness.Reports;
using ODMSBusiness.WorkOrder;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel;
using ODMSModel.AppointmentDetails;
using ODMSModel.AppointmentDetailsLabours;
using ODMSModel.AppointmentDetailsParts;
using ODMSModel.CampaignRequest;
using ODMSModel.Common;
using ODMSModel.Fleet;
using ODMSModel.PeriodicMaintControlList;
using ODMSModel.Vehicle;
using ODMSModel.WorkOrderCard;
using Permission = ODMSCommon.CommonValues.PermissionCodes.WorkOrderCard;
using ODMSModel.VehicleBodywork;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class WorkOrderCardController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">WorkOrderId</param>
        /// <returns></returns> 
        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult WorkOrderCardIndex(long id = 0)
        {
            ViewBag.HideElements = false;
            if (id == 0)
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                ViewBag.HideElements = true;
            }
            var bl = new WorkOrderCardBL();
            var blResponse = bl.GetWorkOrderCard(UserManager.UserInfo, id);
            //bölünen parça recursion döngüsüne girdi ise 
            if (blResponse.Message.Contains("The maximum recursion"))
            {
                SetMessage(MessageResource.WorkOrderCard_Display_Recursive_ForPart, CommonValues.MessageSeverity.Fail);
                return RedirectToAction("WorkOrderIndex", "WorkOrder");
            }

            var model = blResponse.Model;
            if (model.IsCentralDealer)
                return RedirectToAction("CSIndex", new { id = model.WorkOderId });

            if (model.Customer == null || model.Vehicle == null)
            {
                ViewBag.HideElements = true;
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View(model);
            }
            var statsList = bl.ListWorkOrderStats(UserManager.UserInfo).Data;
            ViewBag.WorkOrderStatsList = model.WorkOrderStatManualChangeAllow
                ? statsList.Where(c => c.Selected).ToList()
                : statsList;
            if ((!UserManager.UserInfo.IsDealer && UserManager.UserInfo.ActiveDealerId != model.DealerId) || model.WorkOrderStatId.In(4, 3))
            {
                return View("_ReadOnlyWorkOrderCardIndex", model);
            }

            ViewBag.CampaignCheckList = model.Vehicle.IsCampaignApplicable ? bl.GetCampaignCheckList(UserManager.UserInfo, id).Data.Where(c => c.IsMust).ToList() : null;


            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult CSIndex(long id = 0)
        {
            ViewBag.HideElements = false;
            if (id == 0)
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                ViewBag.HideElements = true;
            }
            var bl = new WorkOrderCardBL();
            var model = bl.GetWorkOrderCard(UserManager.UserInfo, id).Model;
            if (!model.IsCentralDealer)
                return RedirectToAction("WorkOrderCardIndex", new { id = model.WorkOderId });

            if (model.Customer == null || model.Vehicle == null)
            {
                ViewBag.HideElements = true;
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View(model);
            }
            var statsList = bl.ListWorkOrderStats(UserManager.UserInfo).Data;
            ViewBag.WorkOrderStatsList = model.WorkOrderStatManualChangeAllow
                ? statsList.Where(c => c.Selected).ToList()
                : statsList;
            if ((!UserManager.UserInfo.IsDealer && UserManager.UserInfo.ActiveDealerId != model.DealerId) || model.WorkOrderStatId.In(4, 3))
            {
                return View("_ReadOnlyWorkOrderCardIndex", model);
            }

            ViewBag.CampaignCheckList = model.Vehicle.IsCampaignApplicable ? bl.GetCampaignCheckList(UserManager.UserInfo, id).Data.Where(c => c.IsMust).ToList() : null;


            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult Print(long id)
        {
            var user = UserManager.UserInfo;
            //HQ user
            if (user.IsDealer)
            {
                bool checkDealer = new WorkOrderCardBL().CheckWorkOrderDealer(id, user.DealerID).Model;
                if (!checkDealer)
                {
                    return RedirectToAction("NoAuthorization", "SystemAdmin");
                }
            }
            var stream = ReportManager.GetReport(ReportType.WorkOrderReport, id);
            if (stream != null && stream.Length > 0)
            {
                var filename = String.Concat(id, "_Nolu_İş_Emri_Dökümü.pdf");
                return File(stream, "application.pdf", filename);
            }
            SetMessage(MessageResource.ErrorWorkOrderCardReport, CommonValues.MessageSeverity.Fail);
            return WorkOrderCardIndex(id);
        }


        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult GetWorkOrderDetails(long workOrderId, int vehicleId, bool vehicleLeave = false)
        {
            WorkOrderCardBL bl = new WorkOrderCardBL();
            var model = bl.GetWorkOrderCardDetails(UserManager.UserInfo, workOrderId).Model;
            model = bl.GetWorkOrderCard(UserManager.UserInfo, workOrderId).Model;
            ViewBag.VehicleId = vehicleId;
            ViewBag.IsVehicleLeft = vehicleLeave;
            return View("_WorkOrderDetails", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateVehicleKM(long id, long? km, int fromUpdateBtn, bool isHourMaint = false, int hour = 0)
        {
            ViewBag.WorkOrderId = id;
            ViewBag.VehicleKM = km;
            ViewBag.IsHourMaint = isHourMaint;
            ViewBag.Hour = hour;
            ViewBag.FromUpdateBtn = fromUpdateBtn;
            return View("_UpdateVehicleKM");
        }
        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateVehiclePlate(long id, string plate)
        {
            ViewBag.WorkOrderId = id;
            ViewBag.Plate = plate;

            return View("_UpdateVehiclePlate");
        }



        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateVehicleBodyWork(int id, string vinNo, Int64 woNo)
        {
            var bl = new WorkOrderBL();
            var model = new VehicleBodyworkViewModel()
            {
                VehicleId = id,
                VehicleVinNo = vinNo,
                WorkOrderId = woNo,
                DealerId = UserManager.UserInfo.GetUserDealerId()
            };
            bl.GetBodyworkFromVehicle(model);

            SetDefaultCombo();


            return View("_UpdateVehicleBodyWork", model);
        }

        private void SetDefaultCombo()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.CountryList = CommonBL.ListCountries(UserManager.UserInfo).Data;
            ViewBag.BodyworkList = CommonBL.ListBodyWorks(UserManager.UserInfo).Data;
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateVehicleBodyWork(VehicleBodyworkViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_UpdateVehicleBodyWork", model);

            var bl = new WorkOrderBL();

            bl.DMLBodyWorkForWorkOrder(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            SetDefaultCombo();

            return View("_UpdateVehicleBodyWork", model);
        }


        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddWorkOrderDetail(long? id)
        {
            //id=>WorkOrderId
            if (id.HasValue && id.GetValueOrDefault(0) > 0)
            {
                ViewBag.WorkOrderId = id;
                var campaignCheckList = new WorkOrderCardBL().GetCampaignCheckList(UserManager.UserInfo, id ?? 0).Data.Where(c => c.IsMust);

                var bl = new WorkOrderCardBL();
                var wo = bl.GetWorkOrderCard(UserManager.UserInfo, id ?? 0);

                ViewBag.ExpiredWarranty = (wo?.Model?.Vehicle?.WarrantyEndKilometer ?? 0) > 0 && ((wo?.Model?.Vehicle?.WarrantyEndKilometer ?? 0) < (wo?.Model?.Vehicle?.VehicleKilometer));
                ViewBag.ExpiredWarrantyMessage = string.Format(MessageResource.WorkOrderCard_Message_ExpiredVehicleWarranty, (wo?.Model?.Vehicle?.WarrantyEndKilometer ?? 0));

                if (campaignCheckList.Any(c => c.IsMust))
                {
                    ViewBag.HideElements = true;
                    SetMessage(MessageResource.WorkOrderCard_Display_MandatoryCampaignsMustAdd,
                        CommonValues.MessageSeverity.Fail);
                }
                else
                {
                    //deny reason check
                    var model = new WorkOrderCardBL().CheckForOtherCampaigns(id ?? 0).Model;
                    if (model.ErrorNo > 0)
                    {
                        ViewBag.HideElements = true;
                        CheckErrorForMessage(model, false);
                    }
                    else
                    {

                        ViewBag.HideElements = false;
                        //ViewBag.MainCategoryList =AppointmentIndicatorMainCategoryBL.ListAppointmentIndicatorMainCategories(true);
                        var bo = new WorkOrderCardBL();
                        var failureCodeList = bo.ListFailureCodes(UserManager.UserInfo).Data;

                        // 185482  nolu çağrıya istinaden, belirti eklerken arıza kodu olarak alttaki seçeneklerin seçileMEMESİ istendi - ibrahim
                        // Ø(S – BAKIM PAKETİ) – BAKIM PAKETİ (19)
                        // Ø(SO - SATIŞ ÖNCESİ) – SATIŞ ÖNCESİ (29)
                        // Ø(Z - KAMPANYA) - KAMPANYA (27)
                        // Ø(SK - KUPON BAKIMI) - KUPON BAKIMI (20)
                        var exceptionList = new List<int>() { 19, 29, 27, 20 };
                        ViewBag.FailureCodeList = failureCodeList.Where(x => !exceptionList.Contains(x.Value)).ToList();

                        ViewBag.IndicatorTypes = bo.ListIndicatorTypes(UserManager.UserInfo).Data;
                    }
                }
            }
            else
            {
                ViewBag.HideElements = true;
            }
            return View("_AddWorkOrderDetail");
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddWorkOrderDetail(AppointmentDetailsViewModel model)
        {
            //ViewBag.HideElements = false;
            if (model.AppointmentId > 0 && model.SubCategoryId > 0)
            {
                //ViewBag.HideElements = true;
                new WorkOrderCardBL().AddWorkOrderDetail(UserManager.UserInfo, model);
                if (model.ErrorNo == 1)
                {
                    return Json(new { Result = false, Message = model.ErrorMessage });
                }
                return Json(new { Result = true, Message = MessageResource.Global_Display_Success });
            }
            return Json(new { Result = false, Message = MessageResource.WorkOrderCard_Display_SubCategoryError });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddWorkOrderPart(long? id, long workOrderDetailId, string message, bool isError = false)
        {
            if (!string.IsNullOrEmpty(message))
            {
                TempData["MessageToShow"] = message;
                TempData["IsError"] = isError;
            }

            ViewBag.WorkOrderId = id;
            ViewBag.WorkOrderDetailId = workOrderDetailId;
            ViewBag.LastLevelChecked = false;
            return View("_AddWorkOrderPart");
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult GetLabourData(long labourId, int? vehicleId)
        {
            var data = new WorkOrderCardBL().GetLabourData(labourId, vehicleId).Model;
            return data == null ? Json(new LabourDataModel { Editable = true, Duration = 100 }) : Json(data);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardAddDetailItem)]
        public ActionResult AddWorkOrderPart(AppointmentDetailsPartsViewModel model, bool LastLevelChecked)
        {
            var bl = new WorkOrderCardBL();
            var isError = true;
            if (model.IsManuel == 1)
            {
                var barcodeList = model.BarcodeList.Replace(" ", "").Split(new string[] { "\r\n" }, StringSplitOptions.None);
                var barcodeList1 = barcodeList.GroupBy(x => x).Select(x => new { key = x.Key, count = x.Count() }).ToList();
                var message = new StringBuilder();
                foreach (var part in barcodeList1)
                {
                    if (string.IsNullOrEmpty(part.key))
                        continue;

                    var partId = bl.GetPart(UserManager.UserInfo, 0, part.key).Model.PartId;

                    if (partId == 0)
                    {
                        isError = true;
                        message.Append(string.Format("{0} {1}", part.key, "Parça bulunamadı!"));
                        continue;
                    }

                    if (LastLevelChecked)
                    {
                        var lastLevelPartId = bl.GetLastLevelPartId(partId).Model;
                        model.SelectedPartId = partId;
                        model.PartId = lastLevelPartId;
                        model.Quantity = part.count;
                        bl.AddWorkOrderPart(UserManager.UserInfo, model);
                        if (model.ErrorNo == 1)
                        {
                            isError = false;
                            message.Append(string.Format("{0} {1}", part.key, model.ErrorMessage));
                        }
                        else
                        {
                            message.Append(model.ErrorMessage);
                        }
                    }
                    else
                    {
                        var anyLastLevelPart = bl.CheckPartLastLevel(model.AppointIndicId, partId.ToString()).Model;
                        if (!anyLastLevelPart.HasValue || (anyLastLevelPart.HasValue && anyLastLevelPart == false))
                        {
                            model.PartId = partId;
                            model.Quantity = part.count;
                            bl.AddWorkOrderPart(UserManager.UserInfo, model);
                            if (model.ErrorNo == 1)
                            {
                                isError = false;
                                message.Append(string.Format("{0} {1}", part.key, model.ErrorMessage));
                            }
                            else
                            {
                                message.Append(model.ErrorMessage);
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(message.ToString()))
                {
                    message.Append(MessageResource.Global_Display_Success);
                }

                return RedirectToAction("AddWorkOrderPart", new { id = model.Id, workOrderDetailId = model.AppointIndicId, message = message.ToString(), isSuccess = isError });
            }
            else
            {
                if (LastLevelChecked)
                {
                    var lastLevelPartId = bl.GetLastLevelPartId(model.PartId).Model;
                    model.SelectedPartId = model.PartId;
                    model.PartId = lastLevelPartId;
                    bl.AddWorkOrderPart(UserManager.UserInfo, model);
                    CheckErrorForMessage(model, true);
                    return RedirectToAction("AddWorkOrderPart", new { id = model.Id, workOrderDetailId = model.AppointIndicId });
                }
                else
                {
                    var anyLastLevelPart = bl.CheckPartLastLevel(model.AppointIndicId, model.PartId.ToString()).Model;
                    if (anyLastLevelPart.HasValue && anyLastLevelPart == true)
                    {
                        ViewBag.WorkOrderId = model.Id;
                        ViewBag.WorkOrderDetailId = model.AppointIndicId;
                        ViewBag.LastLevelChecked = true;
                        ViewBag.ShowConfirmForLastLvl = true;
                        return View("_AddWorkOrderPart", model);
                    }
                    else
                    {
                        bl.AddWorkOrderPart(UserManager.UserInfo, model);
                        CheckErrorForMessage(model, true);
                        return RedirectToAction("AddWorkOrderPart", new { id = model.Id, workOrderDetailId = model.AppointIndicId });
                    }
                }
            }

            ViewBag.WorkOrderId = model.Id;
            ViewBag.WorkOrderDetailId = model.AppointIndicId;
            ViewBag.LastLevelChecked = false;
            return View("_AddWorkOrderPart");
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddWorkOrderLabour(long? id, int workOrderDetailId, int vehicleId)
        {
            ViewBag.HideElements = id <= 0;

            var indicator = new WorkOrderCardBL().GetDetailData(UserManager.UserInfo, id ?? 0, workOrderDetailId).Model;
            ViewBag.VehicleId = vehicleId;
            ViewBag.Indicator = indicator;
            ViewBag.HideElements = indicator.AppointmentId <= 0;
            return View("_AddWorkOrderLabour", new AppointmentDetailsLaboursViewModel { AppointmentIndicatorId = workOrderDetailId, AppointmentId = id ?? 0 });
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardAddDetailItem)]
        public ActionResult AddWorkOrderLabour(AppointmentDetailsLaboursViewModel model, int vehicleId)
        {
            if (ModelState.IsValid)
            {
                new WorkOrderCardBL().AddWorkOrderLabour(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
            }
            var indicator = new WorkOrderCardBL().GetDetailData(UserManager.UserInfo, model.AppointmentId, model.AppointmentIndicatorId).Model;
            ViewBag.Indicator = indicator;
            ViewBag.VehicleId = vehicleId;
            return View("_AddWorkOrderLabour", model.ErrorNo == 0 ? new AppointmentDetailsLaboursViewModel() : model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddWorkOrderMaint(long? id, long workOrderDetailId)
        {
            ViewBag.HideElements = false;
            var bl = new WorkOrderCardBL();
            var campaignCheckList = bl.GetCampaignCheckList(UserManager.UserInfo, id ?? 0).Data.Where(c => c.IsMust);

            var model = new WorkOrderMaintenanceModel
            {
                WorkOrderId = id ?? 0,
                WorkOrderDetailId = workOrderDetailId
            };
            if (campaignCheckList.Any(c => c.IsMust))
            {
                ViewBag.HideElements = true;
                SetMessage(MessageResource.WorkOrderCard_Display_MandatoryCampaignsMustAdd,
                    CommonValues.MessageSeverity.Fail);
            }
            else
            {


                bl.GetMaintenance(UserManager.UserInfo, model);

                if (model.MaintenanceId == 0)
                {
                    SetMessage(MessageResource.WorkOrderCard_Display_PeriodicMeintenanceNotFound,
                        CommonValues.MessageSeverity.Fail);
                    ViewBag.HideElements = true;
                }
            }
            return View("_AddWorkOrderMaintenance", model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddWorkOrderMaint(WorkOrderMaintenanceModel model)
        {
            var bl = new WorkOrderCardBL();

            bl.AddWorkOrderMaint(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ViewBag.HideElements = true;
            return View("_AddWorkOrderMaintenance", model);

        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult CancelWorkOrderDetail(long? id, long workOrderDetailId)
        {
            ViewBag.HideElements = false;
            var model = new WorkOrderDetailCancelModel
            {
                WorkOrderId = id ?? 0,
                WorkOrderDetailId = workOrderDetailId
            };
            return View("_CancelWorkOrderDetail", model);
        }
        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardCancel)]
        public ActionResult CancelWorkOrder(long? id, int vehicleId)
        {
            ViewBag.HideElements = false;
            var model = new WorkOrderCancelModel
            {
                WorkOrderId = id ?? 0,
                VehicleId = vehicleId
            };
            return View("_CancelWorkOrder", model);
        }
        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateCustomerNote(long? id)
        {
            ViewBag.HideElements = false;
            var bl = new WorkOrderCardBL();
            string rNote = bl.GetCustomerNote(id.GetValue<long>()).Model;
            var model = new WorkOrderCustomerNoteUpdateModel
            {
                WorkOrderId = id ?? 0,
                Note = rNote
            };
            return View("_UpdateCustomerNote", model);
        }


        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public JsonResult UpdateNote(WorkOrderCustomerNoteUpdateModel model)
        {
            if (ModelState.IsValid)
            {

                new WorkOrderCardBL().UpdateCustomerNote(model);
                if (model.ErrorNo == 0)
                {
                    return Json(new { Message = MessageResource.Global_Display_Success, Result = true });
                }
            }
            if (model.Note == null)
            {
                model.ErrorMessage = string.Format(MessageResource.WorkOrderCard_Validation_EnterCustomer_Note);
            }
            else if (model.Note.Length > 500)
                model.ErrorMessage = string.Format(MessageResource.Validation_Length, 500);

            return Json(new { Message = model.ErrorMessage, Result = false });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult Action()
        {
            return View();
        }


        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddDetailDiscount(long? id, long workOrderDetailId, string type, long itemId)
        {
            var model = new WorkOrderCardBL().GetWorkOrderDetailItemDataForDiscount(id ?? 0, workOrderDetailId, type, itemId).Model;
            return View("_AddDetailDiscount", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddDetailQuantity(long? id, long workOrderDetailId, string type, long itemId)
        {
            var model = new WorkOrderCardBL().GetQuantityData(UserManager.UserInfo, id ?? 0, workOrderDetailId, type, itemId).Model;
            return View("_AddDetailQuantity", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult ChangePriceList(long? id, long workOrderDetailId, string type, long itemId, DateTime date)
        {
            var model = new ChangePriceListModel
            {
                WorkOrderId = id ?? 0,
                WorkOrderDetailId = workOrderDetailId,
                Type = type,
                WorkOrderDate = date,
                ItemId = itemId
            };
            return View("_ChangePriceList", model);
        }


        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddLabourPrice(long id, long workOrderDetailId, string type, long itemId)
        {

            var model = new AddLabourPrice
            {
                WorkOrderId = id,
                WorkOrderDetailId = workOrderDetailId,
                Type = type,
                ItemId = itemId,
                UnitPrice = new WorkOrderCardBL().GetWorkOrderDetailItemDataForDiscount(id, workOrderDetailId, type, itemId).Model.ListPrice
            };
            return View("_AddLabourPrice", model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddLabourPrice(AddLabourPrice model)
        {
            if (ModelState.IsValid)
            {

                new WorkOrderCardBL().AddLabourPrice(UserManager.UserInfo, model);
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
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult ChangePriceList(ChangePriceListModel model)
        {
            if (ModelState.IsValid)
            {

                new WorkOrderCardBL().ChangePriceList(UserManager.UserInfo, model);
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
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateQuantity(WorkOrderQuantityDataModel model)
        {
            if (ModelState.IsValid)
            {

                new WorkOrderCardBL().UpdateQuantity(UserManager.UserInfo, model);
                if (model.ErrorNo == 0)
                {
                    return Json(new { Message = MessageResource.Global_Display_Success, Result = true });
                }
            }
            if (model.Quantity == 0)
            {
                model.ErrorMessage = string.Format(MessageResource.WorkOrderCard_Validation_EnterQuantity);
            }
            return Json(new { Message = model.ErrorMessage, Result = false });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult RemoveItem(WorkOrderMaintenanceQuantityDataModel model)
        {
            if (ModelState.IsValid)
            {

                new WorkOrderCardBL().RemoveMaintenanceItem(UserManager.UserInfo, model);
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
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult ChangePart(WorkOrderMaintenanceQuantityDataModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.NewPartId) || model.NewPartId == "0")
                    return Json(new { Message = MessageResource.Err_Generic_Unexpected, Result = false });
                //model.ErrorMessage = MessageResource.Err_Generic_Unexpected;
                new WorkOrderCardBL().ChangePart(UserManager.UserInfo, model);
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
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public JsonResult CancelDetail(WorkOrderDetailCancelModel model)
        {
            if (ModelState.IsValid)
            {

                new WorkOrderCardBL().CancelDetail(UserManager.UserInfo, model);
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
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public JsonResult AddDiscount(WorkOrderDiscountModel model)
        {
            if (ModelState.IsValid)
            {

                new WorkOrderCardBL().AddDiscount(UserManager.UserInfo, model);
                if (model.ErrorNo == 0)
                {
                    return Json(new { Message = MessageResource.Global_Display_Success, Result = true });
                }
                return Json(new { Message = model.ErrorMessage, Result = false });


            }
            if (model.DiscountType == DiscountType.Money && model.DiscountPrice < 0)
            {
                model.ErrorMessage = MessageResource.WorkOrderCard_Validation_EnterDiscountPrice;
            }
            else if (model.DiscountType == DiscountType.Percentage && model.DiscountRatio < 0)
                model.ErrorMessage = MessageResource.WorkOrderCard_Validation_EnterDiscountRatio;
            else
                model.ErrorMessage = MessageResource.Err_Generic_Unexpected;

            return Json(new { Message = model.ErrorMessage, Result = false });

        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public JsonResult UpdateVehicleKM(string id, int fromUpdateBtn, bool isHourMaint, long km = 0, int hour = 0)
        {
            long newId;
            if (long.TryParse(id, out newId) == false)
                return Json(new { Message = MessageResource.Error_DB_NoRecordFound, Result = false });
            if (isHourMaint && hour == 0)
                return Json(new { Message = MessageResource.WorkOrderCard_Validation_EnterVehicleHour, Result = false });
            int ErrorNo;
            string ErrorMessage;
            long? newKM = new WorkOrderCardBL().UpdateVehicleKM(UserManager.UserInfo, newId, km, isHourMaint, hour, fromUpdateBtn, out ErrorNo, out ErrorMessage).Model;
            if (ErrorNo == 0)
            {
                TempData["kmUpdate"] = "1";
                return Json(new { Message = MessageResource.Global_Display_Success, Result = true, NewKM = newKM });
            }
            return Json(new { Message = ErrorMessage, Result = false });

        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public JsonResult UpdateVehiclePlate(string id, string plate)
        {
            long newId;
            int isDuplicate; //Araç plakası unique olmalı. 
            if (long.TryParse(id, out newId) == false)
                return Json(new { Message = MessageResource.Error_DB_NoRecordFound, Result = false });

            plate = plate.ToUpper();
            plate = plate.Trim().Replace(" ", "").Replace("-", "");

            if (String.IsNullOrEmpty(plate))
                return Json(new { Message = MessageResource.WorkOrderCard_Validation_EnterVehiclePlate, Result = false });
            isDuplicate = new WorkOrderCardBL().GetVehiclePlate(id, plate).Model;
            if (isDuplicate > 0)
            {
                return Json(new { Message = MessageResource.Vehicle_Plate_Must_Be_Unique, Result = false });
            }
            int ErrorNo;
            string ErrorMessage;
            string _plate = new WorkOrderCardBL().UpdateVehiclePlate(UserManager.UserInfo, newId, plate, out ErrorNo, out ErrorMessage).Model;
            if (ErrorNo == 0)
            {
                return Json(new { Message = MessageResource.Global_Display_Success, Result = true, plate = _plate });
            }
            return Json(new { Message = ErrorMessage, Result = false });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult EditMeintenanceQuantity(long? id, long workOrderDetailId, string type, long itemId, int maintId)
        {
            var model = new WorkOrderCardBL().GetMaintenanceQuantityData(UserManager.UserInfo, id ?? 0, workOrderDetailId, type, itemId, maintId).Model;
            return View("_EditMeintenanceQuantity", model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult ListAlternateParts(long partId = 0, int maintId = 0, long workOrderDetailId = 0)
        {
            if (partId == 0 || maintId == 0)
                return Json(null);

            var obj = new WorkOrderCardBL().ListAlternateParts(UserManager.UserInfo, partId, maintId, workOrderDetailId).Data;
            return Json(obj);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddCampaign(long id, string campaignCode)/*WorkOrderId*/
        {
            var WOCBL = new WorkOrderCardBL();

            var model = WOCBL.GetCampaignData(UserManager.UserInfo, id).Model;
            model.WorkOrderId = id;
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

            var orderCard = WOCBL.GetWorkOrderCard(UserManager.UserInfo, id).Model;

            List<string> camps = new List<string>();
            if (!String.IsNullOrEmpty(orderCard.DeniedCampaignCodes))
            {
                camps.AddRange(orderCard.DeniedCampaignCodes.Split(',').ToList());
            }

            if (!String.IsNullOrEmpty(orderCard.DeniedCampaignServiceCodes))
            {
                camps.AddRange(orderCard.DeniedCampaignServiceCodes.Split(',').ToList());
            }

            ViewBag.selectedCamps = string.Join(",", camps.Distinct()); //orderCard.DeniedCampaignCodes;


            return View("_AddCampaign", model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddCampaign(WorkOrderCampaignModel model)
        {
            //custom validation
            if (model.WorkOrderId == 0 || model.Campaigns == null || string.IsNullOrEmpty(model.Campaigns.First().CampaignCode))
                return Json(new { Message = MessageResource.Err_Generic_Unexpected, Result = false });
            new WorkOrderCardBL().AddCampaign(UserManager.UserInfo, model);
            if (model.ErrorNo == 0)
                return Json(new { Message = MessageResource.Global_Display_Success, Result = true });

            return Json(new { Message = model.ErrorMessage, Result = false });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult GetCampaignDetail(string campaignCode, string type, long id = 0)
        {
            if (string.IsNullOrEmpty(campaignCode) || string.IsNullOrEmpty(type) || !(type == "LABOUR" || type == "PART" || type == "DOCUMENT"))
            {
                return new ContentResult() { Content = MessageResource.Error_DB_NoRecordFound, ContentType = "text/html" };
            }
            switch (type)
            {
                case "LABOUR":
                    return View("_CampaignDetailLabour", new WorkOrderCardBL().GetCampaignLabours(UserManager.UserInfo, campaignCode, id).Data);
                case "PART":
                    return View("_CampaignDetailPart", new WorkOrderCardBL().GetCampaignParts(UserManager.UserInfo, campaignCode).Data);
                case "DOCUMENT":
                    return View("_CampaignDetailDocument", new WorkOrderCardBL().GetCampaignDocuments(UserManager.UserInfo, campaignCode).Data);
                default:
                    return new ContentResult()
                    {
                        Content = MessageResource.Error_DB_NoRecordFound,
                        ContentType = "text/html"
                    };
            }
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult VehicleNotes(long id = 0)
        {
            ViewBag.WorkOrderId = id;
            return View("_VehicleNotes");
        }
        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult VehicleNotesPopup(long id = 0)
        {
            ViewBag.WorkOrderId = id;
            return View("_VehicleNotesPopup");
        }
        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult DealerNotes(long id = 0)
        {
            ViewBag.WorkOrderId = id;
            return View("_DealerNotes");
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult GetDealerNote(int id, long workOrderId)
        {
            return View("_DealerNoteDetail", new WorkOrderCardBL().GetDealerNote(UserManager.UserInfo, id, workOrderId).Model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult GetVehicleNote(int id, long workOrderId)
        {
            return View("_VehicleNoteDetail", new WorkOrderCardBL().GetVehicleNote(UserManager.UserInfo, id, workOrderId).Model);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult GetVehicleNotePopup(int id, long workOrderId)
        {
            return View("_VehicleNoteDetail", new WorkOrderCardBL().GetVehicleNotePopup(UserManager.UserInfo, id, workOrderId).Model);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult SaveDealerNote(long workOrderId, string note)
        {
            if (workOrderId == 0 || string.IsNullOrEmpty(note) || note.Length == 0 || note.Length > 500)
                return Json(new { Message = MessageResource.Err_Generic_Unexpected, Result = false });
            var model = new WorkOrderVehicleNoteModel
            {
                WorkOrderId = workOrderId,
                Note = note
            };

            new WorkOrderCardBL().AddDealerNote(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return Json(new { Message = MessageResource.Global_Display_Success, Result = true });

            return Json(new { Message = model.ErrorMessage, Result = false });
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult SaveVehicleNote(long workOrderId, string note)
        {
            if (workOrderId == 0 || string.IsNullOrEmpty(note) || note.Length == 0 || note.Length > 500)
                return Json(new { Message = MessageResource.Err_Generic_Unexpected, Result = false });
            var model = new WorkOrderVehicleNoteModel
            {
                WorkOrderId = workOrderId,
                Note = note
            };

            new WorkOrderCardBL().AddVehicleNote(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return Json(new { Message = MessageResource.WorkOrderCard_Message_NoteSendToApproval, Result = true });

            return Json(new { Message = model.ErrorMessage, Result = false });
        }
        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult ClaimWarranty(long id, long workOrderDetailId)
        {
            return View("_ClaimWarranty", new WorkOrderCardBL().GetWarrantData(UserManager.UserInfo, id, workOrderDetailId).Model);

        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public JsonResult SearchParts(string searchText)
        {
            var list = SparePartBL.ListSparePartAsAutoCompSearch(UserManager.UserInfo, searchText, null).Data.Select(x => new ComboBoxModel { Text = string.Format("{0} / {1}", x.Column1, x.Column2), Value = x.Id });
            return Json(list);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateFailureCode(long id, long workOrderDetailId, string code)/*WorkOrderId*/
        {
            ViewBag.WorkOrderId = id;
            ViewBag.WorkOrderDetailId = workOrderDetailId;
            ViewBag.FailureCode = !string.IsNullOrEmpty(code) && code.Contains("_")
                ? code.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries).Last()
                : string.Empty;
            ViewBag.FailureCodeList = new WorkOrderCardBL().ListFailureCodes(UserManager.UserInfo).Data;
            return View("_UpdateFailureCode");
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult FailureCodeUpdate(long workOrderId, long workOrderDetailId, string failureCode)
        {
            if (workOrderId == 0 || workOrderDetailId == 0) /*|| string.IsNullOrEmpty(failureCode))*/
                return Json(new { Message = MessageResource.Err_Generic_Unexpected, Result = false });
            int errorNo;
            string errorMessage;
            new WorkOrderCardBL().UpdateFailureCode(UserManager.UserInfo, workOrderId, workOrderDetailId, failureCode, out errorNo, out errorMessage);
            if (errorNo == 0)
                return Json(new { Message = MessageResource.Global_Display_Success, Result = true });

            return Json(new { Message = errorMessage, Result = false });
        }


        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateDuration(WorkOrderQuantityDataModel model)
        {
            if (ModelState.IsValid)
            {

                new WorkOrderCardBL().UpdateDuration(UserManager.UserInfo, model);
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
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult ListDetailProcessTypes(string id, long? workOrderId)
        {
            return string.IsNullOrEmpty(id) || workOrderId.GetValueOrDefault(0) == 0
                ? Json(null)
                : Json(new WorkOrderCardBL().ListDetailProcessTypes(UserManager.UserInfo, id, workOrderId.GetValueOrDefault()).Data);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult ReservDetailParts(long id, long workOrderDetailId)
        {
            ViewBag.DetailId = workOrderDetailId;
            var model = new WorkOrderCardBL().GetPartReservationData(UserManager.UserInfo, workOrderDetailId).Model;
            return View("_ReservDetailParts", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddTechnicalDetail(long id, long workOrderDetailId)
        {
            ViewBag.DetailId = workOrderDetailId;
            var model = new WorkOrderCardBL().GetWorkOrderDetailDescription(workOrderDetailId).Model;
            return PartialView("_AddTechnicalDetail", model);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddTechnicalDetail(long workOrderDetailId, string note)
        {
            var model = new WorkOrderCardBL().UpdateWorkOrderDetailDescription(UserManager.UserInfo, workOrderDetailId, note).Model;
            return
                Json(new
                {
                    Result = model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success
                });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateContactInfo(long id)
        {
            ViewBag.Id = id;
            var model = new WorkOrderCardBL().GetWorkOrderContactInfo(id).Model;
            return PartialView("_UpdateContactInfo", model);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateContactInfo(long id, string note)
        {
            var model = new WorkOrderCardBL().UpdateWorkOrderContactInfo(UserManager.UserInfo, id, note).Model;
            return
                Json(new
                {
                    Result = model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success
                });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateCampaignDenyReason(long id, string note, string deniedCamps)
        {
            var model = new WorkOrderCardBL().UpdateCampaignDenyReason(UserManager.UserInfo, id, note, deniedCamps.TrimStart(',')).Model;
            return
                Json(new
                {
                    Result = model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success
                });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateDeniedCampaigns(long id, string deniedCamps)
        {
            var model = new WorkOrderCardBL().UpdateDeniedCampaigns(UserManager.UserInfo, id, deniedCamps.TrimStart(',')).Model;
            return
                Json(new
                {
                    Result = model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success
                });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult CancelCampaignRejections(long id)
        {
            var model = new WorkOrderCardBL().CancelCampaignRejections(UserManager.UserInfo, id).Model;
            return
                Json(new
                {
                    Result = model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success
                });
        }


        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateCampaignDenyDealerReason(long id, string note, string deniedCamps)
        {
            var model = new WorkOrderCardBL().UpdateCampaignDenyDealerReason(UserManager.UserInfo, id, note, deniedCamps.TrimStart(',')).Model;
            return
                Json(new
                {
                    Result = model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success
                });
        }


        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateCampaignDenyReason(long id, string deniedCamps)
        {
            ViewBag.Id = id;
            ViewBag.selectedCamps = deniedCamps;
            var model = new WorkOrderCardBL().GetWorkOrderCampaignDenyReason(id).Model;
            return PartialView("_UpdateCampaignDenyReason", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateCampaignDenyDealerReason(long id, string deniedCamps)
        {
            ViewBag.Id = id;
            ViewBag.selectedCamps = deniedCamps;
            var model = new WorkOrderCardBL().GetWorkOrderCampaignDenyDealerReason(id).Model;
            return PartialView("_UpdateCampaignDenyDealerReason", model);
        }


        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult ReturnParts(long id, long workOrderDetailId)
        {
            ViewBag.DetailId = workOrderDetailId;
            var model = new WorkOrderCardBL().ListDetailPartReturnItems(UserManager.UserInfo, workOrderDetailId).Data;
            return View("_PartsReturn", model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult ReturnParts(List<PartReturnModel> list, long workOrderDetailId)
        {
            var model = new WorkOrderCardBL().ReturnDetailParts(UserManager.UserInfo, list, workOrderDetailId).Data;
            return
                Json(new
                {
                    Result =
                        model.Any(c => c.ErrorNo > 0) ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.Any(c => c.ErrorNo > 0) ? model.FirstOrDefault().ErrorMessage : MessageResource.Global_Display_Success
                });
        }

        //[HttpPost]
        //[AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult ReturnPart_ForWebTest(PartReturnModel part, long workOrderDetailId)
        {
            var list = new List<PartReturnModel>();
            list.Add(part);
            var model = new WorkOrderCardBL().ReturnDetailParts(UserManager.UserInfo, list, workOrderDetailId).Data;
            return
                Json(new
                {
                    Result =
                        model.Any(c => c.ErrorNo > 0) ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.Any(c => c.ErrorNo > 0) ? model.FirstOrDefault().ErrorMessage : MessageResource.Global_Display_Success,

                }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult ReservDetailParts(string ProcessType, long workOrderDetailId)
        {
            List<PartReservationInfo> list;
            var model = new WorkOrderCardBL().ReservDetailParts(UserManager.UserInfo, workOrderDetailId, ProcessType, out list).Model;
            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
                    List = list
                });
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult PickDetailParts(long id, long workOrderDetailId)
        {
            var model = new WorkOrderCardBL().PickDetailParts(UserManager.UserInfo, id, workOrderDetailId).Model;
            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
                });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult SaveWorkOrderStat(long WorkOderId, int WorkOrderStatId)
        {
            ModelBase model;
            int workOrderStatusId = new WorkOrderBL().GetWorkOrder(UserManager.UserInfo, WorkOderId).Model.WorkOrderStatusId;
            if (workOrderStatusId.In(4, 3))
            {
                model = new ModelBase()
                {
                    ErrorNo = 1,
                    ErrorMessage = MessageResource.Err_Generic_WorkOrderCardStatusChangeCardAlreadyClosedError
                };

            }
            else
            {
                model = new WorkOrderCardBL().ChangeWorkOrderStat(UserManager.UserInfo, WorkOderId, WorkOrderStatId).Model;
            }

            CheckErrorForMessage(model, true);
            return RedirectToAction("WorkOrderCardIndex", new { id = WorkOderId });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult CreateCampaignRequest(long workOrderDetailId)
        {
            var model = new WorkOrderCardBL().CreateCampaignRequest(UserManager.UserInfo, workOrderDetailId).Model;
            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
                });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult CampaignRequestDetails(long workOrderDetailId, long campaignRequestId)
        {
            var campaignRequest = new CampaignRequestViewModel { IdCampaignRequest = campaignRequestId };
            var model = new WorkOrderCardBL().ListCampaignRequestDetails(UserManager.UserInfo, workOrderDetailId, campaignRequest).Data;
            ViewBag.CampaignRequest = campaignRequest;
            return PartialView("_CampaignRequestDetails", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateProcessType(long workOrderDetailId)
        {
            ViewBag.DetailId = workOrderDetailId;
            var model = new WorkOrderCardBL().GetProcessTypeData(UserManager.UserInfo, workOrderDetailId).Model;
            return PartialView("_ChangeProcessType", model);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateProcessType(long workOrderDetailId, string ProcessType, bool confirmed)
        {
            ViewBag.DetailId = workOrderDetailId;
            var model = new WorkOrderCardBL().UpdateProcessType(UserManager.UserInfo, workOrderDetailId, ProcessType, confirmed).Model;
            return Json(new { ErrorNo = model.ErrorNo, Message = model.ErrorNo == 0 ? MessageResource.Global_Display_Success : model.ErrorMessage });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult CheckVehicleLeave(long id)
        {
            var vehicleLeaveDate = new WorkOrderCardBL().GetVehicleLeaveDate(workOrderId: id).Model;
            return Json(new { VehicleLeaveDate = vehicleLeaveDate }, "application/json");

        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult CheckVehicleLeaveStep2(long id)
        {
            var isValid = new WorkOrderCardBL().CheckVehicleLeaveMandatoryFields(UserManager.UserInfo, workOrderId: id).Model;
            return Json(new { IsValid = isValid }, "application/json");

        }
        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult MustRemovedParts(long id)
        {
            var list = new WorkOrderCardBL().ListMustRemovedParts(UserManager.UserInfo, workOrderId: id).Data;
            return PartialView("_CheckVehicleLeave", list);

        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult PartRemovalInfo(long workOrderDetailId, long partId)
        {
            var bl = new WorkOrderCardBL();
            var dto = bl.GetPartRemovalDto(UserManager.UserInfo, workOrderDetailId, partId).Model;
            ViewBag.RemovablePartList = bl.ListRemovableParts(UserManager.UserInfo, partId).Data;
            return PartialView("_PartRemoval", dto);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdateRemovedPartInfo(PartRemovalDto dto)
        {
            if (ModelState.IsValid)
            {
                var model = new WorkOrderCardBL().UpdateRemovalInfo(UserManager.UserInfo, dto).Model;
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
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddPdiPackage(AddPdiPackageModel model)
        {
            new WorkOrderCardBL().AddPdiPackage(UserManager.UserInfo, model);
            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
                });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdatePdiPackage(AddPdiPackageModel model)
        {
            new WorkOrderCardBL().UpdatePdiPackage(UserManager.UserInfo, model);
            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
                });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult SendToWarrantyPreApproval(long id, long workOrderDetailId)
        {
            ViewBag.Id = workOrderDetailId;
            ViewBag.Action = "SendToWarrantyPreApproval";
            return PartialView("_WarrantyRequestNote", new WorkOrderCardBL().GetGuaranteeRequestDescription(workOrderDetailId).Model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult SendToWarrantyPreApproval(long id, string requestDescription)
        {
            var model = new WorkOrderCardBL().SendToWarrantyPreApproval(UserManager.UserInfo, workOrderDetailId: id, requestDescription: requestDescription).Model;
            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message =
                        model.ErrorNo > 0
                            ? model.ErrorMessage
                            : MessageResource.Global_Display_Success + " " + MessageResource.Global_Display_Id + ":" +
                              id.ToString(),
                });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult SendToWarrantyApproval(long workOrderDetailId)
        {
            var model = new WorkOrderCardBL().SendToWarrantyApproval(UserManager.UserInfo, workOrderDetailId).Model;
            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message =
                        model.ErrorNo > 0
                            ? model.ErrorMessage
                            : MessageResource.Global_Display_Success + " " + MessageResource.Global_Display_Id + ":" +
                              workOrderDetailId.ToString(),
                });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult GetWorkOrderVehicleLeave(long id)
        {

            var user = UserManager.UserInfo;
            if (user.IsDealer)
            {
                bool checkDealer = new WorkOrderCardBL().CheckWorkOrderDealer(id, user.GetUserDealerId()).Model;
                if (!checkDealer)
                {
                    SetMessage(MessageResource.ErrorVehicleLeaveInvoice, CommonValues.MessageSeverity.Fail);
                    return WorkOrderCardIndex(id);
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
                SetMessage(MessageResource.ErrorVehicleLeaveInvoice, CommonValues.MessageSeverity.Fail);
                return WorkOrderCardIndex(id);
            }
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddPdiPackage(long id)
        {
            var model = new AddPdiPackageModel { WorkOrderId = id };
            return PartialView("_AddPdiPackage", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult UpdatePdiPackage(long id)
        {
            var model = new WorkOrderCardBL().GetPdiPackageData(id).Model;
            return PartialView("_UpdatePdiPackage", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult OpenPdiPackageResults(long id)
        {
            return PartialView("_PdiResults", id);
        }
        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult GetPdiPackageResults(long id)
        {
            var bo = new WorkOrderCardBL();
            ViewBag.Id = id;
            ViewBag.IsControlled = bo.GetPdiVehicleIsControlled(id).Model;

            return PartialView("_PdiResultItems", bo.ListPdiResultItems(UserManager.UserInfo, id).Data);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult OpenPdiPackageResult(long id, string controlCode)
        {

            var tuple = new WorkOrderCardBL().GetPdiResultData(UserManager.UserInfo, id, controlCode).Model;
            ViewBag.ControlCode = controlCode;
            ViewBag.ControlName = tuple.Item2;
            ViewBag.PartList = tuple.Item3;
            ViewBag.BreakDownList = tuple.Item4;
            ViewBag.ResultList = tuple.Item5;
            return PartialView("_PdiResultItemEdit", new PdiResultModel { WorkOrderId = id, ControlCode = controlCode });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult SavePdiResult(PdiResultModel dto)
        {
            if (ModelState.IsValid)
            {
                var model = new WorkOrderCardBL().SavePdiResult(UserManager.UserInfo, dto, "U").Model;
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
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult ClearPdiResult(PdiResultModel dto)
        {
            var model = new WorkOrderCardBL().SavePdiResult(UserManager.UserInfo, dto, "C").Model;
            return
                Json(
                    new
                    {
                        Result = model.ErrorNo == 0,
                        Message = model.ErrorNo == 0 ? MessageResource.Global_Display_Success : model.ErrorMessage
                    });
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult PdiSendToApproval(long id)
        {
            var model = new WorkOrderCardBL().PdiSendToApproval(UserManager.UserInfo, id).Model;
            return
                Json(
                    new
                    {
                        Result = model.ErrorNo == 0,
                        Message = model.ErrorNo == 0 ? MessageResource.Global_Display_Success : model.ErrorMessage
                    });
        }

        //[HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult GetPdiPackageDetails(long id)
        {
            ViewBag.Id = id;
            return PartialView("_PdiDetails", new WorkOrderCardBL().GetPdiPackageDetails(UserManager.UserInfo, id).Model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult PrintPdiPackageDetails(long id)
        {
            return PartialView("_PdiPrint", id);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult PrintLabels(long workOrderDetailId, bool printAll = true)
        {
            ModelBase model = new WorkOrderCardBL().PrintLabels(UserManager.UserInfo, workOrderDetailId, printAll).Model;
            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
                });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardDeleteDetailItem)]
        public ActionResult DeleteDetailItem(long? id, long workOrderDetailId, string type, long itemId)
        {
            var model = new WorkOrderCardBL().DeleteDetailItem(UserManager.UserInfo, id ?? 0, workOrderDetailId, type, itemId).Model;
            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
                });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult PrintWorkOrderInvoice(long id, long invoiceId, InvoicePrintType printType)
        {
            var user = UserManager.UserInfo;
            var data = new WorkOrderInvoicesBL().GetWorkOrderInvoices(UserManager.UserInfo, invoiceId).Model;
            if (data == null)
            {
                SetMessage(MessageResource.ErrorVehicleLeaveInvoice, CommonValues.MessageSeverity.Fail);
                return WorkOrderCardIndex(id);
            }
            if (id == 0)
            {
                if (data.WorkOrderId > 0)
                    id = data.WorkOrderId;
                else
                {
                    if (!string.IsNullOrEmpty(data.WorkOrderIds))
                    {
                        id = int.Parse(data.WorkOrderIds.Split(',').First());
                    }
                }
            }

            if (user.IsDealer)
            {
                bool checkDealer = new WorkOrderCardBL().CheckWorkOrderDealer(id, user.GetUserDealerId(), invoiceId).Model;
                if (!checkDealer)
                {
                    SetMessage(MessageResource.ErrorVehicleLeaveInvoice, CommonValues.MessageSeverity.Fail);
                    return WorkOrderCardIndex(id);
                }
            }


            var invType = (InvoiceType)0;
            if (data.InvoiceTypeId == 1) invType = InvoiceType.Dokumlu;
            if (data.InvoiceTypeId == 2) invType = InvoiceType.UcKirilimli;
            if (data.InvoiceTypeId == 3) invType = InvoiceType.Ozel;
            //var invoice = InvoiceFactory.Create(invType, invoiceId, data.HasWitholding);
            var stream = ReportManager.GetReport(ReportType.VehicleInvoiceReport, invType, invoiceId, data.HasWitholding,
                printType);
            if (stream != null && stream.Length > 0)
            {
                string filename = string.Empty;

                switch (printType)
                {
                    case InvoicePrintType.Printed:
                        filename = string.Format(MessageResource.WorkOrderInvoice_Report_Invoice, id);
                        break;
                    case InvoicePrintType.Transcript:
                        filename = string.Format(MessageResource.WorkOrderInvoice_Report_InvoiceTranscript, id);
                        break;
                    case InvoicePrintType.Proforma:
                        filename = string.Format(MessageResource.WorkOrderInvoice_Report_InvoiceProforma, id);
                        break;
                    case InvoicePrintType.ProformaExcel:
                        filename = string.Format(MessageResource.WorkOrderInvoice_Report_InvoiceProformaExcel, id);
                        break;
                    case InvoicePrintType.WorkOrderAndProforma:
                        var workOrderReport = ReportManager.GetReport(ReportType.WorkOrderPrintFirstPart, id);
                        using (var second = new MemoryStream(stream))
                        using (var first = new MemoryStream(workOrderReport))
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
            return WorkOrderCardIndex(id);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult GetVehicleHistoryToolTipContent(int id)
        {
            var content = new WorkOrderCardBL().GetVehicleHistoryToolTipContent(vehicleId: id).Model;
            return Content(content, "html", Encoding.UTF8);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult VehicleDetails(int id)
        {
            var model = new VehicleIndexViewModel { VehicleId = id };
            new VehicleBL().GetVehicle(UserManager.UserInfo, model);
            return PartialView("_VehicleInfo", model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult OpenPeriodicMaintControlList(int id)/*Vehicle Type Id*/
        {
            var total = 0;
            var list =
                new PeriodicMaintControlListBL().ListPeriodicMaintControlList(UserManager.UserInfo, new PeriodicMaintControlListListModel() { IdType = id, LanguageCustom = UserManager.LanguageCode },
                    out total).Data;
            return PartialView("_PeriodicMaintControlList", list);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
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
        [AuthorizationFilter(Permission.WorkOrderCardIndex)]
        public ActionResult AddHourMaint(long id, int vehicleId)
        {
            ViewBag.MaintList = new WorkOrderCardBL().ListVehicleHourMaints(UserManager.UserInfo, vehicleId).Data;
            return View("_HourMaint", id);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult AddHourMaintenance(long WorkOrderId, int MaintId)
        {
            var model = new WorkOrderMaintenanceModel
            {
                MaintenanceId = MaintId,
                WorkOrderId = WorkOrderId
            };
            new WorkOrderCardBL().AddWorkOrderMaint(UserManager.UserInfo, model);
            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
                });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult CompletePdiVehicleResult(long id) /*WorkOrderId*/
        {
            var model = new WorkOrderCardBL().CompletePdiVehicleResult(UserManager.UserInfo, id).Model;
            return
               Json(new
               {
                   Result =
                       model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                   Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
               });


        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult PickingCancellation(long id) /*WorkOrderId*/
        {
            ViewBag.Id = id;
            var model = new WorkOrderCardBL().ListPickingsForCancellation(UserManager.UserInfo, id).Data;
            return PartialView("_PickingCancellation", model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.WorkOrderCardUpdate)]
        public ActionResult CancelPicking(long pickingId, long workOrderId)
        {
            new WorkOrderCardBL().CancelPicking(UserManager.UserInfo, pickingId, workOrderId);
            return RedirectToAction("PickingCancellation", new { id = workOrderId });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.CancelVehicleLeave)]
        public ActionResult CancelVehicleLeave(long id)
        {
            return PartialView("_CancelVehicleLeave", id);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderCardIndex, Permission.CancelVehicleLeave)]
        public ActionResult CancelVehicleLeave(long workOrderId, string reason)
        {
            var model = new WorkOrderCardBL().CancelVehicleLeave(UserManager.UserInfo, workOrderId, reason).Model;
            return
               Json(new
               {
                   Result =
                       model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                   Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
               });


        }



    }
}
