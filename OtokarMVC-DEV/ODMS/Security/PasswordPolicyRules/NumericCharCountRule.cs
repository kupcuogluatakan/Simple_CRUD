using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODMS.Security.PasswordPolicyRules
{
    public class NumericCharCountRule : IPasswordRuleValidator
    {
        private readonly int _minNumericCharCount;
        private readonly string _password;
        public int MinNumericCharCount { get { return _minNumericCharCount; } }
        public NumericCharCountRule(int minNumericCharCount,string password)
        {
            _minNumericCharCount = minNumericCharCount;
            _password = password;
        }
        public bool Validate()
        {
            return _minNumericCharCount == 0 || _minNumericCharCount <= NumericCharCount();
        }
        private int NumericCharCount()
        {
            return _password.Count(char.IsNumber);
        }
    }
}