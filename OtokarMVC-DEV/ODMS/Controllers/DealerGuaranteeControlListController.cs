using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.DealerGuaranteeControl;
using ODMS.Filters;
using Perm = ODMSCommon.CommonValues.PermissionCodes.DealerGuaranteeControlList;
using ODMSModel.GuaranteeRequestApproveDetail;

namespace ODMS.Controllers
{
    public class DealerGuaranteeControlListController: ControllerBase
    {
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
        [AuthorizationFilter(Perm.Index)]
        public ActionResult Index()
        {
            SetDefaults();
            return View();
        }
        [HttpPost]
        [AuthorizationFilter(Perm.Index)]
        public ActionResult List([DataSourceRequest] DataSourceRequest request,DealerGuaranteeControlListModel model)
        {
            DealerGuaranteeControlListModel filterModel = new DealerGuaranteeControlListModel(request);
            filterModel.DealerId = UserManager.UserInfo.GetUserDealerId();
            filterModel.WarrantyStatus = model.WarrantyStatus;
            filterModel.WorkOrderId = model.WorkOrderId;
            filterModel.WorkOrderDetailId = model.WorkOrderDetailId;
            filterModel.CategoryId = model.CategoryId;
            filterModel.StartDate = model.StartDate;
            filterModel.EndDate = model.EndDate;
            filterModel.ApproveStartDate = model.ApproveStartDate;
            filterModel.ApproveEndDate = model.ApproveEndDate;
            filterModel.IndicatorType = model.IndicatorType;
            filterModel.ProcessType = model.ProcessType;
            filterModel.VehicleType = model.VehicleType;
            filterModel.ModelKodList = model.ModelKodList;
            var totalCnt = 0;
            var returnValue = new DealerGuaranteeControlBL().ListGuaranteeRequests(UserManager.UserInfo, filterModel, out totalCnt).Data;
            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [AuthorizationFilter(Perm.Index)]
        public ActionResult Details(long guaranteeId,int guaranteeSeq)
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
            model.IsEditable =  false;
            return View( model);
           
        }

    }
}