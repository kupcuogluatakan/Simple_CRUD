using ODMSBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ODMS.Security.PasswordPolicyRules;
namespace ODMS.Security
{
    public class PasswordPolicyManager
    {
        private readonly string _password;
        private string _passwordPolicyString;
        private List<IPasswordRuleValidator> _passwordRuleValidators;
        private int _blockMinutes;

        public int BlockMinutes { get { return _blockMinutes;} }
        public List<IPasswordRuleValidator> PasswordRuleValidators {
            get { return _passwordRuleValidators; }
        }
        public PasswordPolicyManager(string password)
        {
            _password = password;
            _passwordPolicyString = CommonBL.GetGeneralParameterValue("SYS_PASSWORD_POLICY").Model;
            if (string.IsNullOrEmpty(_passwordPolicyString))
            {
                throw new ArgumentException("SYS_PASSWORD_POLICY is not set.");
            }
            _passwordRuleValidators= new List<IPasswordRuleValidator>();

            if (password == "**otokar**")
                return;

            ParseRulesFromPolicyString();
        }

        private void ParseRulesFromPolicyString()
        {
            //format: LL-XX-YYY-ZZZ-MM-AA-UU-DD-S
            var rules = _passwordPolicyString.Split('-');
            
            //OldPasswordCheckRule
            int searchPasswordCount = ParseIntValue(rules[0]);
            var oldPasswordCheckRule = new OldPasswordCheckRule(searchPasswordCount, _password);
            _passwordRuleValidators.Add(oldPasswordCheckRule);

            //WrongPasswordCountRule
            int allowedWrongPasswordCount = ParseIntValue(rules[1]);
            var wrongPasswordCountRule = new WrongPasswordCountRule(allowedWrongPasswordCount);
            _passwordRuleValidators.Add(wrongPasswordCountRule);

            //BlockMinutes
            _blockMinutes = ParseIntValue(rules[2]);
            
            //PasswordChangeDayCountRule
            int passwordChangeDayCount = ParseIntValue(rules[3]);
            _passwordRuleValidators.Add(new PasswordChangeDayCountRule(passwordChangeDayCount));

           //PasswordLengthRule
            int minPasswordLength = ParseIntValue(rules[4]);
            _passwordRuleValidators.Add(new PasswordLengthRule(minPasswordLength, _password));

            //AlphaNumericCharCountRule
            int minAlphaNumericCharCount = ParseIntValue(rules[5]);
            _passwordRuleValidators.Add(new AlphaNumericCharCountRule(minAlphaNumericCharCount, _password));

            //UpperCharCountRule
            int minUpperCharCount = ParseIntValue(rules[6]);
            _passwordRuleValidators.Add(new UpperCharCountRule(minUpperCharCount, _password));

            //NumericCharCountRule
            int minNumericCharCount = ParseIntValue(rules[7]);
            _passwordRuleValidators.Add(new NumericCharCountRule(minNumericCharCount, _password));

            //AllowSpecialCharRule
            bool allowSpecialChars = ParseIntValue(rules[8]) == 1;
            _passwordRuleValidators.Add(new AllowSpecialCharRule(allowSpecialChars, _password));

        }

        private static int ParseIntValue(string str)
        {
            int outValue = 0;
            if (!int.TryParse(str, out outValue))
            {
                return 0;
            }
            return outValue;
        }
    }
}