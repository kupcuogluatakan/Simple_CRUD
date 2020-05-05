using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODMS.Security.PasswordPolicyRules
{
    public class AllowSpecialCharRule : IPasswordRuleValidator
    {
        private readonly bool _allowSpecialChars;
        private readonly string _password;
        public bool AllowSpecialChars { get { return _allowSpecialChars;} }

        public AllowSpecialCharRule(bool allowSpecialChars,string password)
        {
            _allowSpecialChars = allowSpecialChars;
            _password = password;
        }

        public bool Validate()
        {
            if (_allowSpecialChars) return true;
            else
            {
                return CheckPasswordForSpecialChars();
            }
        }

        private bool CheckPasswordForSpecialChars()
        {
            return _password.Count(c=>!char.IsLetterOrDigit(c)) == 0;
        }
    }
}