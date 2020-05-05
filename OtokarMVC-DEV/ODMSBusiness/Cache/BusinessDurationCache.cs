using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness
{
    class BusinessDurationCache : IBusinessCache
    {
        public string OnEntry(MethodExecutionArgs args, Type VersionControlClass, string VersionControlMethod, int DurationMunite)
        {
            if (DurationMunite == 0)
            {
                throw new Exception("Cache yapmak için DurationMunite giriniz.");
            }

            return string.Format("{0}_{1}", args.Instance.ToString(), args.Method.Name);
        }
    }
}
