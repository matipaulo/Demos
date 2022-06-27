namespace SortComparison.SortingAlgorithms
{
    using Extensions;

    public class QuickSort
    {
        public void Sort(IList<int> values, int start, int end)
        {
            if (start >= end)
                return;

            int index = Partition(values, start, end);

            Sort(values, start, index - 1);
            Sort(values, index + 1, end);
        }

        private static int Partition(IList<int> values, int start, int end)
        {
            int pivotValue = values.ElementAt(end);
            int pivotIndex = start;

            for (int i = start; i < end; i++)
            {
                if (values.ElementAt(i) < pivotValue)
                {
                    values.Swap(i, pivotIndex);
                    pivotIndex++;
                }
            }

            values.Swap(pivotIndex, end);

            return pivotIndex;
        }
    }
}