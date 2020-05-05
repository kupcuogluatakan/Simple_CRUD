using System.Security.Principal;

namespace ODMSCommon.Security
{
    public class ODMSUserIdentity : IIdentity
    {
        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public string email { get; set; }

        // public ODMSUserIdentity(string userName)
        //{
        //    //Assign the incoming user name to the current one and clear the roles collection
        //    var user = SecurityHelper.GetUserDetail(userName);
        //    userName = userName;
        //    fullName = user.FullName;

        //    roles.Clear();
        //    authenticated = true;
        //    roles.AddRange(SecurityHelper.GetUserPermissions(userName));
        //}

        // public ODMSUserIdentity(int userId)
        //{
        //    //Assign the incoming user name to the current one and clear the roles collection
        //    var user = SecurityHelper.GetUserDetail(userId);
        //    userName = user.UserName;
        //    fullName = user.FullName;
        //    roles.Clear();
        //    authenticated = true;
        //    /* Retrive the list of all authorized Tasks and Operations from NetSqlAzMan database
        //     * and persist it with the roles arraylist collection
        //     */
        //    roles.AddRange(SecurityHelper.GetUserPermissions(userName));
        //}

    }
}