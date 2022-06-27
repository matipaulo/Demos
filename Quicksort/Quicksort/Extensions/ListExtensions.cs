namespace SortComparison.Extensions
{
    using System.Collections.Generic;

    public static class ListExtensions
    {
        public static void Swap(this IList<int> values, int i, int j)
        {
            (values[i], values[j]) = (values[j], values[i]);
        }
    }
}
