using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ODMSBusiness;
using ODMSCommon.Security;

namespace ODMS.Security.PasswordPolicyRules
{
    public class PasswordChangeDayCountRule : IPasswordRuleValidator
    {
        private readonly int _day;
        private readonly PasswordBL _passwordBl;
        public int Day { get { return _day; } }
        public PasswordChangeDayCountRule(int day)
        {
            _day = day;
           _passwordBl= new PasswordBL();
        }

        public bool Validate()
        {
            DateTime? lastPasswordChangeDay = _passwordBl.GetLastPasswordChangeDateForUser(UserManager.UserInfo.UserId).Model;
            if(lastPasswordChangeDay==null)
                return false;
            return (DateTime.Now - lastPasswordChangeDay.GetValueOrDefault()).Days < _day;
        }
    }
}