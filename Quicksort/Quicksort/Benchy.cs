namespace SortComparison
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Engines;
    using SortingAlgorithms;

    [MemoryDiagnoser]
    //[SimpleJob(RunStrategy.ColdStart, launchCount: 20)]
    public class Benchy
    {
        //[Params(1000)]
        public int TotalValuesToBeSorted = 99999;

        private IList<int> _values;
        private QuickSort _quickSort;
        private BubbleSort _bubbleSort;

        [GlobalSetup]
        public void Setup()
        {
            _values = GenerateRandomValues(TotalValuesToBeSorted);
            _quickSort = new QuickSort();
            _bubbleSort = new BubbleSort();
        }

        [Benchmark]
        public void QuickSort()
        {
            _quickSort.Sort(_values, 0, TotalValuesToBeSorted - 1);
        }

        [Benchmark]
        public void BubbleSort()
        {
            _bubbleSort.Sort(_values);
        }

        private static IList<int> GenerateRandomValues(int length)
        {
            var random = new Random(length);
            var values = new List<int>(length);
            for (int i = 0; i < length; i++)
            {
                values.Add(random.Next(0, 99));
            }

            return values;
        }
    }
}