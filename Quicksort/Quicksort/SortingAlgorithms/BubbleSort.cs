namespace SortComparison.SortingAlgorithms
{
    public class BubbleSort
    {
        // implement bubble sort algorithm
        public void Sort(IList<int> values)
        {
            if (values == null || values.Count == 0)
                return;

            for (int i = 0; i < values.Count - 2; i++)
            {
                for (int j = 0; j < values.Count - 1 - i; j++)
                {
                    if (values[j] > values[j + 1])
                    {
                        int temp = values[j];
                        values[j] = values[j + 1];
                        values[j + 1] = temp;
                    }
                }
            }
        }
    }
}