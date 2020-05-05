using ODMSModel.Dealer;
using PostSharp.Aspects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness
{
    [Serializable]
    public class BusinessCache : OnMethodBoundaryAspect
    {
        public BusinessCache()
        {

        }

        /// <summary>
        /// Cache tipi
        /// </summary>
        public enum BusinessCacheType
        {
            Duration = 0,
            Version = 1
        }

        public BusinessCacheType CurrentCacheType
        {
            get
            {
                if (DurationMunite > 0)
                    return BusinessCacheType.Duration;
                else
                    return BusinessCacheType.Version;
            }
        }

        /// <summary>
        /// Cache Nesnesi
        /// </summary>
        private static Dictionary<string, object> _cache;
        public static Dictionary<string, object> CacheList
        {
            get
            {
                if (_cache == null)
                    _cache = new Dictionary<string, object>();
                return _cache;
            }
            set
            {
                _cache = value;
            }
        }

        /// <summary>
        /// Duration Cache Nesnesi
        /// </summary>
        private static Dictionary<string, object> _durationCache;
        public static Dictionary<string, object> DurationCacheList
        {
            get
            {
                if (_durationCache == null)
                    _durationCache = new Dictionary<string, object>();
                return _durationCache;
            }
            set
            {
                _durationCache = value;
            }
        }

        /// <summary>
        /// Versiyon kontrolü yapılacak nesne
        /// </summary>
        public Type VersionControlClass { get; set; }

        /// <summary>
        /// Versiyon kontrolü yapılacak method
        /// </summary>
        public string VersionControlMethod { get; set; }

        /// <summary>
        /// Cache yapılacak süre
        /// </summary>
        public int DurationMunite { get; set; }

        /// <summary>
        /// Runtime da çalışılan cache key
        /// </summary>
        string currentKey = string.Empty;

        /// <summary>
        /// Methoda girdiğinde cache kontrolü yapılır
        /// Cache yeni atılacak ise key oluşturulur
        /// </summary>
        /// <param name="args">Class ve Method parametreleri</param>
        public override void OnEntry(MethodExecutionArgs args)
        {
            Dictionary<string, object> version = new Dictionary<string, object>();

            object currentValue = null;
            IBusinessCache cache;
            //Versiyon cache yada Duration cache ten birini kullanınız.
            switch (CurrentCacheType)
            {
                case BusinessCacheType.Duration:
                    cache = new BusinessDurationCache();
                    currentKey = cache.OnEntry(args, VersionControlClass, VersionControlMethod, DurationMunite);
                    currentValue = DateTime.Now.AddMinutes(DurationMunite);
                    break;
                case BusinessCacheType.Version:
                    cache = new BusinessVersionCache();
                    currentKey = cache.OnEntry(args, VersionControlClass, VersionControlMethod, DurationMunite);
                    break;
                default:
                    break;
            }

            lock (currentKey)
            {
                if (CacheList.ContainsKey(currentKey))
                {
                    switch (CurrentCacheType)
                    {
                        case BusinessCacheType.Version:
                            args.ReturnValue = CacheList[currentKey];
                            args.FlowBehavior = FlowBehavior.Return;
                            return;
                            break;
                        case BusinessCacheType.Duration:
                            //Cache'te varsa cache'ten al yoksa ekle
                            if (Convert.ToDateTime(DurationCacheList[currentKey]) > DateTime.Now)
                            {
                                //Cache'ten al
                                args.ReturnValue = CacheList[currentKey];
                                args.FlowBehavior = FlowBehavior.Return;
                            }
                            else
                            {
                                //Cache'e ekleme yapılır.

                                //key ile cachte data var ise kaldırılır.
                                if (DurationCacheList.ContainsKey(currentKey))
                                    DurationCacheList.Remove(currentKey);
                                if (CacheList.ContainsKey(currentKey))
                                    CacheList.Remove(currentKey);

                                DurationCacheList.Add(currentKey, currentValue);
                                CacheList.Add(currentKey, currentValue);
                                args.FlowBehavior = FlowBehavior.Continue;
                            }
                            return;
                            break;
                    }
                    args.ReturnValue = CacheList[currentKey];
                    args.FlowBehavior = FlowBehavior.Return;
                    return;
                }
                else
                {
                    //key ile cachte data var ise kaldırılır.
                    if (DurationCacheList.ContainsKey(currentKey))
                        DurationCacheList.Remove(currentKey);
                    if (CacheList.ContainsKey(currentKey))
                        CacheList.Remove(currentKey);

                    //Cache'e ekleme yapılır.
                    switch (CurrentCacheType)
                    {
                        case BusinessCacheType.Duration:
                            DurationCacheList.Add(currentKey, currentValue);
                            CacheList.Add(currentKey, currentValue);
                            break;
                        case BusinessCacheType.Version:
                            CacheList.Add(currentKey, currentValue);
                            break;
                    }

                    args.FlowBehavior = FlowBehavior.Continue;
                    return;
                }
            }
        }

        /// <summary>
        /// Method çalıştıktan sonra sonucunu cache'e atar.
        /// </summary>
        /// <param name="args">Method return result nesnesi</param>
        public override void OnExit(MethodExecutionArgs args)
        {
            lock (currentKey)
            {
                if (!string.IsNullOrEmpty(currentKey))
                {
                    var list = args.ReturnValue;
                    CacheList[currentKey] = list;
                }
            }
            base.OnExit(args);
        }
    }
}
