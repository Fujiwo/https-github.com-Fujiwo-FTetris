using System;
using System.Collections.Generic;
using System.Linq;

namespace FTetris.Model
{
    public static class EnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            foreach (var item in @this)
                action(item);
        }

        public static void ForEach<T>(this IEnumerable<T> @this, Action<int, T> action)
        {
            var index = 0;
            foreach (var item in @this)
                action(index++, item);
        }

        public static bool IsEven<T, S>(this IEnumerable<T> @this, Func<T, S> predicate)
        {
            try {
                var value = predicate(@this.First());
                return @this.All(item => predicate(item).Equals(value));
            } catch {
                return true;
            }
        }
    }
}
