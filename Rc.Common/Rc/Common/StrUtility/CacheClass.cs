namespace Rc.Common.StrUtility
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web;
    using System.Web.Caching;

    public class CacheClass
    {
        public static void AddCache(string CacheName, object CacheValue)
        {
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                current.Cache.Insert(CacheName, CacheValue, null, DateTime.Now.AddYears(100), Cache.NoSlidingExpiration);
            }
        }

        public static void AddCache(string CacheName, DateTime dt, object CacheValue)
        {
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                current.Cache.Insert(CacheName, CacheValue, null, dt, Cache.NoSlidingExpiration);
            }
        }

        public static void DeleteCache(string cacheName)
        {
            HttpContext current = HttpContext.Current;
            if ((current != null) && (current.Cache[cacheName] != null))
            {
                current.Cache.Remove(cacheName);
            }
        }

        public static object GetCache(string cacheName)
        {
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                return current.Cache[cacheName];
            }
            return null;
        }

        public static List<string> GetCacheKeyAll()
        {
            Cache cache = HttpRuntime.Cache;
            List<string> list = new List<string>();
            IDictionaryEnumerator enumerator = cache.GetEnumerator();
            if (enumerator != null)
            {
                while (enumerator.MoveNext())
                {
                    list.Add(enumerator.Key.ToString());
                }
            }
            return list;
        }

        public static List<string> GetCacheKeyByPrefix(string prefix)
        {
            List<string> list = new List<string>();
            List<string> cacheKeyAll = GetCacheKeyAll();
            for (int i = 0; i < cacheKeyAll.Count; i++)
            {
                if (cacheKeyAll[i].ToString().IndexOf(prefix) == 0)
                {
                    list.Add(cacheKeyAll[i]);
                }
            }
            return list;
        }

        public static bool GetIfEnabledCache()
        {
            string str = ConfigurationManager.AppSettings["IsEnableDataCache"];
            bool flag = false;
            if (string.IsNullOrEmpty(str) || (!str.ToLower().Equals("true") && !str.ToLower().Equals("false")))
            {
                return flag;
            }
            return bool.Parse(str);
        }
    }
}

