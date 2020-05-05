using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODMS.Security.PasswordPolicyRules
{
    public class PasswordLengthRule : IPasswordRuleValidator
    {
        private readonly int _minLength;
        private readonly string _password;
        public int PasswordMinLength { get { return _minLength; } }
        public PasswordLengthRule(int minLength,string password)
        {
            _minLength = minLength;
            _password = password;
        }

        public bool Validate()
        {
            return _minLength==0 || _minLength<=_password.Length;
        }
    }
}