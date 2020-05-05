using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ODMSCommon.Logging;
using ODMSTerminal.Infrastructure.MvcActionFilters;

namespace ODMSTerminal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LocalizationFilter(),1);
            filters.Add(new ExceptionFilter(new Loggable()),2);
        }
    }
}