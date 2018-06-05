﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Crawler.Api
{
    internal static class Extensions
    {
        public static void SetAsNonRemovable(this ObjectCache cache, string key, object value)
        {
            cache.Set(key, value, new CacheItemPolicy() { Priority = CacheItemPriority.NotRemovable });
        }

        public static IEnumerable<TResult> SafeSelect<T, TResult>(this IEnumerable<T> source, Func<T, TResult> predicate) where TResult : class
        {
            return source.Select<T, TResult>(i =>
            {
                try
                {
                    return predicate(i);
                }
                catch
                {
                }
                return null;
            }).Where(i => i != null);

        }
    }
}
