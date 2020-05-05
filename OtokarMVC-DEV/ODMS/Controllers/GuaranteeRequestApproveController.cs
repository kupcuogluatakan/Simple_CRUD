using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.GuaranteeRequestApprove;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class GuaranteeRequestApproveController : ControllerBase
    {
        #region Member Varables

        private void SetDefaults()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.CategoryList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.GIFCategory).Data;
            ViewBag.WarrantyStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.WarrantyStatus).Data;
            ViewBag.DealerRegionList = new DealerBL().ListDealerRegions().Data;
            ViewBag.ProcessTypeList = CommonBL.ListProcessType(UserManager.UserInfo).Data;
            ViewBag.IndicatorTypeList = new AppointmentIndicatorSubCategoryBL().ListOfIndicatorTypeCodeAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.VehicleTypeList = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, "").Data;
            ViewBag.ModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
        }

        #endregion

        #region GuaranteeRequestApprove Index

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeRequestApprove.GuaranteeRequestApproveIndex)]
        [HttpGet]
        public ActionResult GuaranteeRequestApproveIndex(int? idVehicle, int? isEditable)
        {
           
            SetDefaults();
            var waranntyStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.WarrantyStatus).Data;
            waranntyStatusList.RemoveAll(x => x.Value == "0");
            waranntyStatusList.Where(x => x.Value == "1").FirstOrDefault().Selected = true;
            waranntyStatusList.Where(x => x.Value == "4").FirstOrDefault().Selected = true;

            ViewBag.WarrantyStatusList = waranntyStatusList;
            GuaranteeRequestApproveListModel model = new GuaranteeRequestApproveListModel();
            model.IdVehicle = idVehicle;
            model.IsEditable = isEditable;

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeRequestApprove.GuaranteeRequestApproveIndex, CommonValues.PermissionCodes.GuaranteeRequestApprove.GuaranteeRequestApproveIndex)]
        public ActionResult ListGuaranteeRequestApprove([DataSourceRequest] DataSourceRequest request, GuaranteeRequestApproveListModel model)
        {
            var guaranteeRequestApproveBo = new GuaranteeRequestApproveBL();

            var v = new GuaranteeRequestApproveListModel(request);

            v.IdDealer = model.IdDealer;
            v.IdUser = UserManager.UserInfo.UserId;
            v.StartDate = model.StartDate;
            v.EndDate = model.EndDate;
            v.WarrantyStatus = model.WarrantyStatus;
            v.IdVehicle = model.IdVehicle;
            v.IsEditable = model.IsEditable;
            v.CategoryId = model.CategoryId;
            v.ProcessType = model.ProcessType;
            v.DealerRegionId = model.DealerRegionId;
            v.ApproveEndDate = model.ApproveEndDate;
            v.ApproveStartDate = model.ApproveStartDate;
            v.WorkOrderId = model.WorkOrderId;
            v.WorkOrderDetailId = model.WorkOrderDetailId;
            v.ProcessType = model.ProcessType;
            v.VehicleVinNo = model.VehicleVinNo;
            v.ModelKodList = model.ModelKodList.AddSingleQuote();
            v.VehicleType = model.VehicleType.AddSingleQuote();
            var totalCnt = 0;
            var returnValue = guaranteeRequestApproveBo.ListGuaranteeRequestApprove(UserManager.UserInfo, v, out totalCnt).Data;

            int errCount = returnValue.Count(r => r.ErrorNo > 0);
            string errorMessage = string.Empty;
            if(errCount > 0)
            {
                errorMessage = returnValue.Select(d => d.ErrorMessage).First();
            }

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion
    }
}