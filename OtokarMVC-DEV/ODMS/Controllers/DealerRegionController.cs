using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Dealer;
using ODMSModel.ListModel;
using ODMSModel.DealerRegion;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DealerRegionController : ControllerBase
    {
        #region DealerRegion Index

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerRegion.DealerRegionIndex)]
        [HttpGet]
        public ActionResult DealerRegionIndex()
        {
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerRegion.DealerRegionIndex, CommonValues.PermissionCodes.DealerRegion.DealerRegionDetails)]
        public ActionResult ListDealerRegion([DataSourceRequest] DataSourceRequest request, DealerRegionListModel model)
        {
            var dealerRegionBo = new DealerRegionBL();
            var v = new DealerRegionListModel(request);
            var totalCnt = 0;
            v.DealerRegionName = model.DealerRegionName;
            var returnValue = dealerRegionBo.ListDealerRegions(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region DealerRegion Create
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerRegion.DealerRegionIndex, CommonValues.PermissionCodes.DealerRegion.DealerRegionCreate)]
        [HttpGet]
        public ActionResult DealerRegionCreate()
        {
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerRegion.DealerRegionIndex, CommonValues.PermissionCodes.DealerRegion.DealerRegionCreate)]
        [HttpPost]
        public ActionResult DealerRegionCreate(DealerRegionIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var dealerRegionBo = new DealerRegionBL();

            if (ModelState.IsValid)
            {
                viewModel.CommandType = viewModel.DealerRegionId > 0
                                            ? CommonValues.DMLType.Update
                                            : CommonValues.DMLType.Insert;
                dealerRegionBo.DMLDealerRegion(UserManager.UserInfo, viewModel);

                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();
                return View();
            }
            return View(viewModel);
        }

        #endregion

        #region DealerRegion Update
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerRegion.DealerRegionIndex, CommonValues.PermissionCodes.DealerRegion.DealerRegionUpdate)]
        [HttpGet]
        public ActionResult DealerRegionUpdate(int id = 0)
        {
            var v = new DealerRegionIndexViewModel();
            if (id > 0)
            {
                var dealerRegionBo = new DealerRegionBL();
                v.DealerRegionId = id;
                dealerRegionBo.GetDealerRegion(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerRegion.DealerRegionIndex, CommonValues.PermissionCodes.DealerRegion.DealerRegionUpdate)]
        [HttpPost]
        public ActionResult DealerRegionUpdate(DealerRegionIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var dealerRegionBo = new DealerRegionBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = viewModel.DealerRegionId > 0
                                            ? CommonValues.DMLType.Update
                                            : CommonValues.DMLType.Insert;
                dealerRegionBo.DMLDealerRegion(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region DealerRegion Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerRegion.DealerRegionIndex, CommonValues.PermissionCodes.DealerRegion.DealerRegionDelete)]
        public ActionResult DeleteDealerRegion(int dealerRegionId)
        {
            /*TFS NO : 27628 OYA 17.12.2014 Bayi bölge silinmek istenildiğinde dealer tablosu kontrol edilir. dealer tablosunda o kayıt var ise 
            " Silmek istediğiniz bölgeye ait Servis/Bayi kaydı bulunmaktadır. Kayıt silinemez." uyarısı verilir.*/
            int totalCount = 0;
            DealerBL dBo = new DealerBL();
            DealerListModel dModel = new DealerListModel();
            dModel.DealerRegionId = dealerRegionId;
            dBo.ListDealers(UserManager.UserInfo, dModel, out totalCount);
            if (totalCount > 0)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.DealerRegion_Warning_DealerExists);
            }
            else
            {
                DealerRegionIndexViewModel viewModel = new DealerRegionIndexViewModel()
                {
                    DealerRegionId = dealerRegionId
                };
                var dealerRegionBo = new DealerRegionBL();
                viewModel.CommandType = viewModel.DealerRegionId > 0 ? CommonValues.DMLType.Delete : string.Empty;
                dealerRegionBo.DMLDealerRegion(UserManager.UserInfo, viewModel);

                ModelState.Clear();
                if (viewModel.ErrorNo == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
            }
        }
        #endregion

        #region DealerRegion Details
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerRegion.DealerRegionIndex, CommonValues.PermissionCodes.DealerRegion.DealerRegionDetails)]
        [HttpGet]
        public ActionResult DealerRegionDetails(int id = 0)
        {
            var v = new DealerRegionIndexViewModel();
            var dealerRegionBo = new DealerRegionBL();

            v.DealerRegionId = id;
            dealerRegionBo.GetDealerRegion(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
    }
}
