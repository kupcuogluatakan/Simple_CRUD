using ODMSService.Helpers;
using ODMSService.Report;
using System.Collections.Generic;
using System;
using ODMSBusiness;
using ODMSModel.Reports;
using System.IO;
using System.Configuration;
using System.ServiceProcess;

namespace ODMSService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if (!DEBUG)
            var servicesToRun = new ServiceBase[]
            {
                new OtokarDataServ()
            };
            ServiceBase.Run(servicesToRun);
#else
            var service = new OtokarDataServ();
            
#endif
        }
    }
}
