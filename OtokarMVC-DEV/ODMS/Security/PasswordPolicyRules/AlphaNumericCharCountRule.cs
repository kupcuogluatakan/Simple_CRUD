using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODMS.Security.PasswordPolicyRules
{
    public class AlphaNumericCharCountRule : IPasswordRuleValidator
    {
        private readonly int _allowedLength;
        private readonly string _password;
        public int MinLength { get { return _allowedLength; } }
        public AlphaNumericCharCountRule(int allowedLength,string password)
        {
            _allowedLength = allowedLength;
            _password = password;
        }

        public bool Validate()
        {
            return _allowedLength == 0 || _allowedLength <= AlpaNumericCharCount();
        }

        private int AlpaNumericCharCount()
        {
            return _password.Count(char.IsLetter);
        }
    }
}