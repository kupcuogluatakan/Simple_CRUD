using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness
{
    class BusinessVersionCache : IBusinessCache
    {
        /// <summary>
        /// Runtime da çalışılan cache key
        /// </summary>
        private string currentKey = string.Empty;

        public string OnEntry(MethodExecutionArgs args, Type VersionControlClass, string VersionControlMethod, int DurationMunite)
        {
            if (VersionControlClass == null || string.IsNullOrEmpty(VersionControlMethod))
            {
                throw new Exception("Versiyon kontrolü için VersionControlClass ve VersionControlMethod giriniz.");
            }

            Dictionary<string, object> version = new Dictionary<string, object>();

            var inst = Activator.CreateInstance(VersionControlClass);
            var classInfo = inst.GetType();
            var methodInfo = classInfo.GetMethod(VersionControlMethod);
            var model = methodInfo.Invoke(inst, null);
            if (model != null)
            {
                var modelPropList = model.GetType().GetProperties();
                modelPropList.ToList().ForEach(x =>
                {
                    var key = string.Format("{0}_{1}_{2}", classInfo.Name, VersionControlMethod, x.Name);
                    if (x.GetValue(model) != null)
                    {
                        var re = 0;
                        var isInt = int.TryParse(x.GetValue(model).ToString(), out re);

                        if (!version.ContainsKey(key) && ((isInt && re > 0) || !isInt))
                        {
                            version.Add(key, x.GetValue(model));
                        }
                    }
                });
            }

            var newKey = string.Empty;
            version.ToList().ForEach(x =>
            {
                newKey = string.Format("{0}_{1}_{2}", newKey, x.Key, x.Value);
            });

            return newKey;
        }
    }
}
