using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportService
{
    public class UserCredentials:System.Web.Services.Protocols.SoapHeader
    {
        public int userId;
        public string password;
        public bool returnValue;
    }
}