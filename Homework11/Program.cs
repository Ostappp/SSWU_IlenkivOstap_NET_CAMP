using Homework11.Task1;
using Homework11.Task2;
namespace Homework11
{
    internal class Program
    {
        public const string dataPath = "..\\..\\..\\Task2\\Data.txt";
        public const string outputPath = "..\\..\\..\\Task2\\Result.txt";
        static void Main(string[] args)
        {
            Random rnd = new Random();
            //Task1
            int[] list = new int[32];// { 14, 2, 1, 8, 6, 3, 5, 4, 10, 7, 9, 11, 13, 12};

            for (int i = 0; i < list.Length; i++)
            {
                list[i] = rnd.Next(100);
            }
            Console.WriteLine(string.Join(", ", QuickSort<int>.Sort(list, QuickSort<int>.PivotSelect.Median)));

            //Task2
            List<string> data = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                data.Add(rnd.Next(1000).ToString());
            }
            WriteDataLines(data.ToArray());

            Merging.SortManyFiles(dataPath, outputPath, 30);
        }
        public static void WriteDataLines(string[] text, bool append = false, string path = dataPath)
        {
            using StreamWriter outputFile = new(path, append);
            foreach (var line in text)
            {
                outputFile.WriteLine(line);
            }

        }
    }
}