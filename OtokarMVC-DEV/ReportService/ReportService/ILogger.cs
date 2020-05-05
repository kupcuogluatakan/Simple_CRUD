using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportService
{
    public interface ILogger
    {
        void Log(string message, string methodParameters, out int i);
    }
}
