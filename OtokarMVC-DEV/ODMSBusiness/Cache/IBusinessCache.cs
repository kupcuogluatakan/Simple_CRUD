using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness
{
    public interface IBusinessCache
    {
        string OnEntry(MethodExecutionArgs args, Type VersionControlClass, string VersionControlMethod, int DurationMunite);
    }
}
