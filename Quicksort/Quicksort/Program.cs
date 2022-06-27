using BenchmarkDotNet.Running;

namespace SortComparison
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //const int length = 10;
            //var values = GenerateRandomValues(length);
            //DisplayCollection("Random values:", values);

            ////var sorting = new QuickSort();
            ////sorting.Sort(values, 0, length - 1);

            ////var sorting = new BubbleSort();
            ////sorting.Sort(values);

            //DisplayCollection("Sorted values:", values);

            //Console.ReadKey();

            BenchmarkRunner.Run<Benchy>();
            Console.ReadKey();
        }

        

        

        private static void DisplayCollection(string title, IEnumerable<int> values)
        {
            Console.WriteLine(title);
            Console.WriteLine(string.Join(",", values));
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