using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSBusiness.Business;
using ODMSCommon;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class VehicleHistoryController : ControllerBase
    {
        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleHistory.VehicleHistoryIndex)]
        [HttpGet]
        public ActionResult VehicleHistoryIndex()
        {
            var processTypeList = CommonBL.ListProcessType(UserManager.UserInfo).Data;
            var indicatorTypeList = new AppointmentIndicatorSubCategoryBL().ListOfIndicatorTypeCodeAsSelectListItem(UserManager.UserInfo).Data;
            //yol yardımı çıkarılmalı
            processTypeList.RemoveAll(x => x.Value == "PT_Y");
            //Satış öncesi çıkarılmalı
            indicatorTypeList.RemoveAll(x => x.Value == "IT_O");

            var bus = new DealerBL();
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.ProcessTypeCodeList = processTypeList;
            ViewBag.IndicatorTypeList = indicatorTypeList;
            ViewBag.CustomerIdList = CustomerBL.ListCustomerNameAndNoAsSelectListItem().Data;

            return View(new VehicleHistoryListModel());
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleHistory.VehicleHistoryIndex)]
        [HttpGet]
        public ActionResult VehicleHistoryDetailIndex(int id)
        {
            return View(new VehicleHistoryDetailListModel() { VehicleHistoryId = id });
        }


        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleHistory.VehicleHistoryIndex)]
        [HttpPost]
        public ActionResult ListVehicleHistoryIndex([DataSourceRequest] DataSourceRequest request, VehicleHistoryListModel model)
        {
            if (UserManager.UserInfo == null)
                return Json(new { Message = "Kullanıcı bilgisine ulaşılamadığı için yetki kontrolü yapılamadı!" });

            var reportsBo = new VehicleHistoryBL();
            var v = new VehicleHistoryListModel(request)
            {
                DealerName = model.DealerName.AddSingleQuote(),
                VinNo = model.VinNo,
                Plate = model.Plate,
                CustomerIds = model.CustomerIds.AddSingleQuote(),
                ProcessType = model.ProcessType.AddSingleQuote(),
                IndicatorType = model.IndicatorType.AddSingleQuote()
            };

            var totalCnt = 0;
            var returnValue = reportsBo.ListVehicleHistory(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleHistoryIndex)]
        public ActionResult ListVehicleHistoryDetailForVehicleHistoryScreen([DataSourceRequest] DataSourceRequest request, VehicleHistoryDetailListModel model)
        {
            var vehicleBo = new VehicleBL();
            var v = new VehicleHistoryDetailListModel(request);
            var totalCnt = 0;
            v.VehicleHistoryId = model.VehicleHistoryId;
            var returnValue = vehicleBo.ListVehicleHistoryDetails(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

    }
}