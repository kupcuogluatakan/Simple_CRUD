using System;
using System.Web.Security;


namespace ODMSCommon.Security
{
    public class ODMSMembershipProvider : MembershipProvider
    {
        public override bool ValidateUser(string username, string password)
        {
            //if (username == "admin")
            //{
            //    var uInfo = new UserInfo
            //    {
            //        UserName = "admin",
            //        UserId = 1,
            //        Email = "admin@admin.com",
            //        Permissions =
            //            new List<PermissionInfo>
            //                    {
            //                        new PermissionInfo {PermissionCode = "Placeholder", PermissionName = "Yetki Adı"}
            //                    }
            //    };

            //    var testModel = new MenuInfo();
            //    var menuItem = new MenuItemInfo();
            //    menuItem.MenuItemId = 1;
            //    menuItem.ActionName = "Index";
            //    menuItem.ControllerName = "Home";
            //    menuItem.Text = "HomeIndex";
            //    testModel.MenuItems.Add(menuItem);
            //    var childMenuItem = new MenuItemInfo();
            //    childMenuItem.MenuItemId = 2;
            //    childMenuItem.ActionName = "About";
            //    childMenuItem.ControllerName = "Home";
            //    childMenuItem.Text = "HomeAbout";
            //    testModel.MenuItems[0].Children.Add(childMenuItem);


            //    var aboutChildMenuItem = new MenuItemInfo();
            //    aboutChildMenuItem.MenuItemId = 3;
            //    aboutChildMenuItem.Text = "AboutChild";
            //    aboutChildMenuItem.ActionName = "AboutChild";
            //    aboutChildMenuItem.ControllerName = "Home";
            //    testModel.MenuItems[0].Children[0].Children.Add(aboutChildMenuItem);
            //    uInfo.MenuItems = testModel.MenuItems;

            //    HttpContext.Current.Items.Add("UserInfo", uInfo);

            //}
            //DB Authentication logic
            return username == "admin";
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            if (UserManager.UserInfo != null)

                return new MembershipUser("MyMembershipProvider", username,
                                          UserManager.UserInfo.UserId, UserManager.UserInfo.Email, null,
                                          null, true, false, DateTime.MinValue, DateTime.MinValue,
                                          DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);


            return null;

        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
                                                  bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
                                                             string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }


        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}