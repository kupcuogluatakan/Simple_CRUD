using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSModel.CampaignRequestOrders;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CampaignRequestOrdersController : ControllerBase
    {
        //
        // GET: /CampaignRequestOrders/

        #region CampaignRequestOrders Index
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CampaignRequestOrders.CampaignRequestOrdersIndex)]
        [HttpGet]
        //public ActionResult CampaignRequestOrdersIndex(string code = null)
        public ActionResult CampaignRequestOrdersIndex(int id = 0)
        {
            CampaignRequestOrdersModel cro = new CampaignRequestOrdersModel();
            if (id <= 0)
            {
                ViewBag.HideElement = true;
                return View();
            }
            cro.CampaignRId = id;
            ViewBag.HideElement = false;
                return PartialView(cro);
            }

           

        
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Role.RoleTypeIndex, ODMSCommon.CommonValues.PermissionCodes.CampaignRequestOrders.CampaignRequestOrdersIndex)]
        public ActionResult ListCampaignRequestOrders([DataSourceRequest] DataSourceRequest request, CampaignRequestOrdersListModel model)
        {
            CampaignRequestOrdersBL croBL = new CampaignRequestOrdersBL();
            CampaignRequestOrdersListModel crosListModel = new CampaignRequestOrdersListModel(request);
            int TotalCnt = 0;

            crosListModel.CampaignRId = model.CampaignRId;
            crosListModel.CampaignCode = model.CampaignCode;
            
            crosListModel.ModelName = model.ModelName;
            crosListModel.CampaignName = model.CampaignName;
            crosListModel.Quantity = model.Quantity;
            crosListModel.RequestNote = model.RequestNote;
            crosListModel.status = model.status;
            crosListModel.RequestDate = model.RequestDate;
            var rValue = croBL.ListCampaignRequestOrders(UserManager.UserInfo, crosListModel, out TotalCnt).Data;

            return Json(new
            {
                Data = rValue,
                Total = TotalCnt
            } );
    }
    
        #endregion

        #region CampaignRequestOrders Delete
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CampaignRequestOrders.CampaignRequestOrdersIndex, ODMSCommon.CommonValues.PermissionCodes.CampaignRequestOrders.CampaignRequestOrdersDelete)]
        public ActionResult DeleteCampaignRequestOrders(int id)
        {
            CampaignRequestOrdersModel croModel = new CampaignRequestOrdersModel() { CampaignRId = id };
            
            if (ModelState.IsValid)
            {
                ViewBag.HideElements = false;
                var boCro = new CampaignRequestOrdersBL();
                croModel.CommandType = CommonValues.DMLType.Delete;
                boCro.DeleteCampaignRequestOrders(croModel);
                CheckErrorForMessage(croModel, true);
                if (croModel.ErrorNo == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            }
            //croModel.CommandType = ODMSCommon.CommonValues.DMLType.Delete;
           
           
           return GenerateAsyncOperationResponse(AsynOperationStatus.Error, croModel.ErrorMessage);
        }
        #endregion
    }
}
