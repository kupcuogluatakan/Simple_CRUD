using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.ClaimDismantledParts;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ClaimDismantledPartsController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.FirmActionList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.ClaimPartAction).Data;
        }
        public JsonResult GetFirmActions()
        {
            var returnValue = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.ClaimPartAction).Data;

            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }

        #region ClaimDismantledParts Index

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimDismantledParts.ClaimDismantledPartsIndex)]
        [HttpGet]
        public ActionResult ClaimDismantledPartsIndex(int? id)
        {
            SetDefaults();
            ClaimDismantledPartsListModel model = new ClaimDismantledPartsListModel();
            if (id != null)
            {
                model.ClaimWaybillId = id.GetValue<int>();
            }

            var waybillModel = new ClaimWaybillViewModel { ClaimWaybillId = model.ClaimWaybillId };
            var partBo = new ClaimDismantledPartsBL();
            partBo.GetClaimWaybill(waybillModel);
            ViewBag.AcceptUser = waybillModel.AcceptUser;
            ViewBag.AcceptDate = waybillModel.AcceptDate;

            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimDismantledParts.ClaimDismantledPartsIndex)]
        public ActionResult ListClaimDismantledParts([DataSourceRequest] DataSourceRequest request, ClaimDismantledPartsListModel model)
        {
            var claimDismantledPartsBo = new ClaimDismantledPartsBL();
            var v = new ClaimDismantledPartsListModel(request);
            var totalCnt = 0;
            v.ClaimWaybillId = model.ClaimWaybillId;
            var returnValue = claimDismantledPartsBo.ListClaimDismantledPartss(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        [HttpPost]
        public ActionResult ClaimDismantledPartComplete(int claimWaybillId, string idList)
        {
            bool isValid = true;
            string errMsg = string.Empty;

            ClaimDismantledPartsViewModel resultViewModel = new ClaimDismantledPartsViewModel();
            ClaimDismantledPartsBL resultBl = new ClaimDismantledPartsBL();

            int totalCount = 0;
            var listModel = new ClaimDismantledPartsListModel
            {
                ClaimWaybillId = claimWaybillId
            };
            var list = resultBl.ListClaimDismantledPartss(UserManager.UserInfo, listModel, out totalCount).Data;

            var control = (from r in list.AsEnumerable()
                           where !idList.Contains(r.ClaimDismantledPartId.ToString())
                           select r);
            foreach (var claimDismantledPartsListModel in control)
            {
                resultViewModel.ClaimDismantledPartId = claimDismantledPartsListModel.ClaimDismantledPartId;
                resultBl.GetClaimDismantledParts(UserManager.UserInfo, resultViewModel);
                resultViewModel.FirmActionId = (int)CommonValues.FirmAction.Lost;
                resultViewModel.FirmActionDate = DateTime.Now;
                resultViewModel.CommandType = CommonValues.DMLType.Update;
                resultBl.DMLClaimDismantledParts(UserManager.UserInfo, resultViewModel);
                if (resultViewModel.ErrorNo > 0)
                {
                    isValid = false;
                    errMsg = resultViewModel.ErrorMessage;
                    break;
                }
            }

            if (isValid)
            {
                ClaimDismantledPartsBL partBo = new ClaimDismantledPartsBL();
                ClaimWaybillViewModel waybillModel = new ClaimWaybillViewModel();
                waybillModel.ClaimWaybillId = claimWaybillId;
                partBo.GetClaimWaybill(waybillModel);
                waybillModel.AcceptUser = UserManager.UserInfo.FirstName + " " + UserManager.UserInfo.LastName;
                waybillModel.AcceptDate = DateTime.Now;
                waybillModel.CommandType = CommonValues.DMLType.Update;
                partBo.DMLClaimWaybill(UserManager.UserInfo, waybillModel);
                if (waybillModel.ErrorNo > 0)
                    errMsg = waybillModel.ErrorMessage;
            }
            return Json(errMsg);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountResult.CycleCountResultSave)]
        [HttpPost]
        public ActionResult ClaimDismantledPartsSave([DataSourceRequest] DataSourceRequest request,
                                                 [Bind(Prefix = "models")] IEnumerable<ClaimDismantledPartsListModel>
                                                     claimDismantledPartsList)
        {
            int claimWaybillId = (claimDismantledPartsList.ElementAt(0) as ClaimDismantledPartsListModel).ClaimWaybillId;
            SaveResults(claimDismantledPartsList, claimWaybillId);

            ClaimDismantledPartsListModel model = new ClaimDismantledPartsListModel();
            model.ClaimWaybillId = claimWaybillId;

            return ListClaimDismantledParts(request, model);
        }
        private static void SaveResults(IEnumerable<ClaimDismantledPartsListModel> claimDismantledPartsList, int claimWaybillId)
        {
            List<int> idList = new List<int>();
            ClaimDismantledPartsBL resultBl = new ClaimDismantledPartsBL();
            ClaimDismantledPartsViewModel resultViewModel = new ClaimDismantledPartsViewModel();

            foreach (var result in claimDismantledPartsList)
            {
                resultViewModel.ClaimDismantledPartId = result.ClaimDismantledPartId;
                resultBl.GetClaimDismantledParts(UserManager.UserInfo, resultViewModel);

                resultViewModel.FirmActionExplanation = result.FirmActionExplanation;
                resultViewModel.FirmActionId = result.FirmActionId.GetValue<int>();
                resultViewModel.FirmActionDate = DateTime.Now;
                resultViewModel.CommandType = CommonValues.DMLType.Update;
                resultBl.DMLClaimDismantledParts(UserManager.UserInfo, resultViewModel);

                idList.Add(result.ClaimDismantledPartId);
            }
        }

        #endregion
    }
}
