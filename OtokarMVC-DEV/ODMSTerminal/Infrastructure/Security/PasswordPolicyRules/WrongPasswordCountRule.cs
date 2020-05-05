using ODMSBusiness;
using ODMSCommon.Security;

namespace ODMSTerminal.Infrastructure.Security.PasswordPolicyRules
{
    public class WrongPasswordCountRule : IPasswordRuleValidator
    {
        private readonly int _allowedWrongPasswordInputCount;
        private readonly PasswordBL _passwordBl;

        public int AllowedWrongPasswordInputCount { get {return _allowedWrongPasswordInputCount;} }
        public WrongPasswordCountRule(int allowedWrongPasswordInputCount)
        {
            _allowedWrongPasswordInputCount = allowedWrongPasswordInputCount;
            _passwordBl= new PasswordBL();
        }

        public bool Validate()
        {
            int wrongPasswordInputCount = _passwordBl.WrongPasswordInputCountForUser(UserManager.UserInfo.UserId).Model;
            return wrongPasswordInputCount < _allowedWrongPasswordInputCount;
        }
    }
}