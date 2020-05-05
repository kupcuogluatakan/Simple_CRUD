using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using ODMSCommon.Security;

namespace ODMSTerminal.Security
{
    public class UserPermissionManager
    {
        private readonly HttpSessionState _currentSessionState = HttpContext.Current.Session;

        public void InitializeCurrentSessionState()
        {
            if (UserManager.UserInfo == null)
                return;
            ClearCurrentSessionState();
            InitializeUserMenuSessionState();
            InitializeUserPermissionSessionState();
        }
        public List<PermissionInfo> UserPermissions
        {
            get
            {
                if (UserManager.UserInfo == null)
                    return new List<PermissionInfo>();

                if (_currentSessionState[ODMSCommon.CommonValues.UserPermissionsSessionKey] == null)
                {
                    InitializeUserPermissionSessionState();
                }

                return (List<PermissionInfo>)_currentSessionState[ODMSCommon.CommonValues.UserPermissionsSessionKey];
            }
        }

        public List<MenuItemInfo> UserMenu
        {
            get
            {
                if (UserManager.UserInfo == null)
                    return new List<MenuItemInfo>();

                if (_currentSessionState[ODMSCommon.CommonValues.UserMenuSessionKey] == null)
                {
                    InitializeUserMenuSessionState();
                }

                return (List<MenuItemInfo>)_currentSessionState[ODMSCommon.CommonValues.UserMenuSessionKey];
            }
        }

        private void InitializeUserPermissionSessionState()
        {
            var permissionBo = new ODMSBusiness.PermissionBL();
            _currentSessionState[ODMSCommon.CommonValues.UserPermissionsSessionKey] = permissionBo.GetUserPermissions(UserManager.UserInfo, UserManager.UserInfo.UserId);
        }

        public void InitializeUserMenuSessionState()
        {
            var menuBo = new ODMSBusiness.MenuBL();
            _currentSessionState[ODMSCommon.CommonValues.UserMenuSessionKey] = menuBo.GetUserMenu(UserManager.UserInfo, UserManager.UserInfo.UserId);
        }

        public void ClearCurrentSessionState()
        {
            if (_currentSessionState != null)
            {
                _currentSessionState.Remove(ODMSCommon.CommonValues.UserPermissionsSessionKey);
                _currentSessionState.Remove(ODMSCommon.CommonValues.UserMenuSessionKey);
            }
        }
    }
}