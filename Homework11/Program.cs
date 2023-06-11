using Homework11.Task1;
using Homework11.Task2;
namespace Homework11
{
    internal class Program
    {
        public const string outputPath = "..\\..\\..\\Task2\\Data.txt";
        static void Main(string[] args)
        {
            //Task1
            int[] list = { 14, 2, 1, 8, 6, 3, 5, 4, 10, 7, 9, 11, 13, 12};
            Console.WriteLine(string.Join(", ", QuickSort<int>.Sort(list)));

            //Task2
            Random rnd = new Random();
            List<string> data = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                data.Add(rnd.Next(100).ToString());
            }
            WriteDataLines(data.ToArray());

            Merging.Sort(outputPath, 50);
        }
        public static void WriteDataLines(string[] text, bool append = false, string path = outputPath)
        {
            using StreamWriter outputFile = new(path, append);
            foreach (var line in text)
            {
                outputFile.WriteLine(line);
            }

        }
    }
}