using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ODMSBusiness;
using ODMSCommon.Security;

namespace ODMS.Security.PasswordPolicyRules
{
    public class OldPasswordCheckRule : IPasswordRuleValidator
    {
        private readonly int _searchCount;
        private readonly string _newPassword;
        private readonly PasswordBL _passwordBl;

        public int SearchCount { get { return _searchCount; } }
        public OldPasswordCheckRule(int searchCount,string password)
        {
            _searchCount = searchCount;
            _newPassword = password;
            _passwordBl= new PasswordBL();
        }

        public bool Validate()
        { 
            if (_searchCount==0) return true;
            string hashedPassword = new PasswordSecurityProvider().GenerateHashedPassword(_newPassword);
            return !_passwordBl.PasswordExistsInLastNPasswords(_searchCount, hashedPassword, UserManager.UserInfo.UserId).Model;
        }
    }
}