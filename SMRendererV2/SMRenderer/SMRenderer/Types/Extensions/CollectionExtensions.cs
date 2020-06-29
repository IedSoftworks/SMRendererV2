using System.Collections.Generic;

namespace SMRenderer.Types.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<TSource>(this ICollection<TSource> collection, params TSource[] values)
        {
            foreach (TSource source in values) collection.Add(source);
        }

        public static void RemoveRange<TSource>(this ICollection<TSource> collection, params TSource[] values)
        {
            foreach (var source in values) collection.Remove(source);
        }
    }
}