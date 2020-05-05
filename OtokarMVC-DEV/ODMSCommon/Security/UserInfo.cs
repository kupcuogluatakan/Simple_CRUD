using System;
using System.Collections.Generic;
using System.Text;

namespace ODMSCommon.Security
{

    [Serializable]
    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int DealerID { get; set; }
        public int ActiveDealerId { get; set; }
        public bool IsDealer { get; set; }
        public string LanguageCode { get; set; }
        public bool IsPasswordSet { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool RememberMe { get; set; }
        public bool IsTechnician { get; set; }
        public bool HasSystemRole { get; set; }
        //public List<PermissionInfo> Permissions { get; set; }
        //public List<MenuItemInfo> MenuItems { get; set; }
        public List<int> Roles { get; set; }

        public string SessionId { get; set; }
        public DateTime? SessionExpireDate { get; set; }
        public string FullName
        {
            get
            {
                return
                    new StringBuilder().Append(FirstName)
                        .Append(CommonValues.EmptySpace)
                        .Append(string.IsNullOrEmpty(MiddleName) ? string.Empty : MiddleName)
                        .Append(CommonValues.EmptySpace)
                        .Append(LastName)
                        .ToString();
            }
        }

        public bool IsBlocked { get; set; }
        public int PasswordChangeSequence { get; set; }

        public UserInfo()
        {
            Roles = new List<int>();
        }
        public int GetUserDealerId()
        {
            return DealerID == 0 ? ActiveDealerId : DealerID;
        }
    }
}


