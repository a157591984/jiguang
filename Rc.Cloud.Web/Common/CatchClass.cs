using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Collections;

namespace Rc.Cloud.Web.Common
{
    public class CacheClass
    {
        /// <summary>
        /// 功能:向缓存中添加数据
        /// 时间:2012-04-06
        /// </summary>
        /// <param name="CacheName"></param>
        /// <param name="CacheValue"></param>
        /// <param name="tag"></param>
        public static void AddCache(string CacheName, object CacheValue)
        {
            System.Web.HttpContext.Current.Cache.Insert(CacheName, CacheValue, null, DateTime.Now.AddYears(100), System.Web.Caching.Cache.NoSlidingExpiration);
        }
        /// <summary>
        /// 功能:向缓存中添加数据
        /// 时间:2012-04-06
        /// </summary>
        /// <param name="CacheName"></param>
        /// <param name="CacheValue"></param>
        /// <param name="tag"></param>
        public static void AddCache(string CacheName, DateTime dt, object CacheValue)
        {
            System.Web.HttpContext.Current.Cache.Insert(CacheName, CacheValue, null, dt, System.Web.Caching.Cache.NoSlidingExpiration);
        }
        /// <summary>
        /// 功能：取出缓存中的值
        /// 时间:2012-04-06
        /// </summary>
        /// <param name="cacheName"></param>
        /// <returns></returns>
        public static Object GetCache(string cacheName)
        {
            return System.Web.HttpContext.Current.Cache[cacheName];
        }
        /// <summary>
        /// 功能：清除缓存中的值
        /// 时间:2012-04-06
        /// </summary>
        /// <param name="cacheName"></param>
        /// <returns></returns>
        public static void DeleteCache(string cacheName)
        {
            System.Web.HttpContext.Current.Cache.Remove(cacheName);
        }
        /// <summary>
        /// 跟据缓存Key值前缀得到相应的缓存Key值
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static List<string> GetCacheKeyByPrefix(string prefix)
        {
            List<string> listTemp = new List<string>();
            List<string> arr = GetCacheKeyAll();
            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i].ToString().IndexOf(prefix) == 0)
                {
                    listTemp.Add(arr[i]);
                }
            }

            return listTemp;
        }
        /// <summary>
        /// 得到所有缓存Key值
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCacheKeyAll()
        {

            Cache _cache = HttpRuntime.Cache;//缓存
            List<string> listTemp = new List<string>();
            IDictionaryEnumerator enumerator = _cache.GetEnumerator();
            if (enumerator != null)
            {
                while (enumerator.MoveNext())
                {
                    listTemp.Add(enumerator.Key.ToString());
                }
            }
            return listTemp;
        }
    }
}