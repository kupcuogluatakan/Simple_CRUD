using System.Security.Principal;

namespace ODMSCommon.Security
{
        public class ODMSUserPrincipal : IPrincipal
        {
            public ODMSUserPrincipal(IIdentity identity)
            {
                Identity = identity;
            }

            public IIdentity Identity
            {
                get;
                private set;
            }

            public UserInfo UserInfo { get; set; }

            public bool IsInRole(string role)
            {
                return true;
            }
        }
    
}
