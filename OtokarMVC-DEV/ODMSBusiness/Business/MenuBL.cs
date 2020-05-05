using System.Collections.Generic;
using System.Linq;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.ListModel;
using ODMSModel.Menu;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class MenuBL : BaseBusiness
    {
        private readonly MenuData data = new MenuData();

        public ResponseModel<MenuListModel> ListMenu(UserInfo user, MenuListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<MenuListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListMenu(user, filter, out totalCnt);
                response.Total = totalCnt;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<MenuIndexViewModel> GetMenu(UserInfo user, MenuIndexViewModel filter)
        {
            var response = new ResponseModel<MenuIndexViewModel>();
            try
            {
                response.Model = data.GetMenu(user, filter);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<MenuItemInfo> GetUserMenu(UserInfo user, int userId)
        {
            var response = new ResponseModel<MenuItemInfo>();
            try
            {
                var menuInfoRaw = data.GetUserMenu(user, userId);
                var boFavoriteMenu = new FavoriteScreenData();
                var favoriteMenuInfoRaw = boFavoriteMenu.GetUserFavoriteMenu(user, userId);
                menuInfoRaw.MenuItems.AddRange(favoriteMenuInfoRaw.MenuItems);
                var menuInfoProcessed = HandleMenuItemHierarchy(menuInfoRaw);

                response.Data = menuInfoProcessed.MenuItems;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        private MenuInfo HandleMenuItemHierarchy(MenuInfo menuInfo)
        {
            var processedMenuInfo = new MenuInfo(); 
            var processedList = processedMenuInfo.MenuItems;
            var rawList = menuInfo.MenuItems;
            //0. seviyedeki itemları ekliyoruz
            processedList.AddRange(menuInfo.MenuItems.Where(v => v.MenuItemParentId == null));
            foreach (var item in processedList)
            {
                AddChildren(item, rawList);
            }
            processedMenuInfo.MenuItems = processedList;
            return processedMenuInfo;
        }

        private void AddChildren(MenuItemInfo menuItem, List<MenuItemInfo> referenceList)
        {
            menuItem.Children = new List<MenuItemInfo>();
            menuItem.Children.AddRange(referenceList.Where(v => v.MenuItemParentId == menuItem.MenuItemId));
            if (menuItem.Children.Count > 0)
                foreach (var childMenuItem in menuItem.Children)
                    AddChildren(childMenuItem, referenceList);
        }

        public ResponseModel<MenuIndexViewModel> DMLMenu(UserInfo user, MenuIndexViewModel model)
        {
            var response = new ResponseModel<MenuIndexViewModel>();
            try
            {
                data.DMLMenu(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }


        public ResponseModel<SelectListItem> GetDefinedSubMenuList(UserInfo user, int id)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetDefinedSubMenuList(user, id);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<SelectListItem> GetSubMenuList(UserInfo user, int id)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetSubMenuList(user, id);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<MenuPlacementViewModel> DMLMenuPlacement(UserInfo user, MenuPlacementViewModel model)
        {
            var response = new ResponseModel<MenuPlacementViewModel>();
            try
            {
                data.DMLMenuPlacement(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }


    }
}
