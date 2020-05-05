using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODMS.Security.PasswordPolicyRules
{
    public class UpperCharCountRule : IPasswordRuleValidator
    {
        private readonly int _allowedUpperCharCount;
        private readonly string _password;
        public int MinUpperCharCount { get { return _allowedUpperCharCount; } }
        public UpperCharCountRule(int allowedUpperCharCount, string password)
        {
            _allowedUpperCharCount = allowedUpperCharCount;
            _password = password;
        }
        public bool Validate()
        {
            return _allowedUpperCharCount == 0 || _allowedUpperCharCount <= UpperCharCount();
        }

        private int UpperCharCount()
        {
            return _password.Count(char.IsUpper);
        }
    }
}