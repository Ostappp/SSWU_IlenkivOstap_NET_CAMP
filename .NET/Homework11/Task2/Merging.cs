using Homework11.Task1;
using System.IO;

namespace Homework11.Task2
{
    internal class Merging
    {
        static void Sort_Merge(string file1, string file2, string output)
        {
            StreamReader reader1 = new StreamReader(file1);
            StreamReader reader2 = new StreamReader(file2);

            StreamWriter writer = new StreamWriter(output);

            int num1 = int.Parse(reader1.ReadLine());
            int num2 = int.Parse(reader2.ReadLine());


            while (!reader1.EndOfStream && !reader2.EndOfStream)
            {
                if (num1 < num2)
                {
                    writer.WriteLine(num1);

                    num1 = int.Parse(reader1.ReadLine());
                }
                else
                {
                    writer.WriteLine(num2);

                    num2 = int.Parse(reader2.ReadLine());
                }
            }
            if (num1 < num2)
            {
                writer.WriteLine(num1);
                writer.WriteLine(num2);
            }
            else
            {
                writer.WriteLine(num2);
                writer.WriteLine(num1);
            }
            if (reader1.EndOfStream)
            {
                //writer.WriteLine(num2);
                while (!reader2.EndOfStream)
                {
                    writer.WriteLine(reader2.ReadLine());
                }
            }
            else
            {
                //writer.WriteLine(num1);
                while (!reader1.EndOfStream)
                {
                    writer.WriteLine(reader1.ReadLine());
                }
            }
            reader1.Close();
            reader2.Close();
            writer.Close();
        }
        public static void Sort(string dataPath, string outputPath, int count)
        {
            string temp1 = Path.Combine(Path.GetDirectoryName(outputPath), "temp1.txt");
            string temp2 = Path.Combine(Path.GetDirectoryName(outputPath), "temp2.txt");
            StreamReader reader = new StreamReader(dataPath);
            StreamWriter writer = new StreamWriter(temp1);
            int[] _arr = new int[count];
            for (int i = 0; i < _arr.Length && !reader.EndOfStream; i++)
            {
                _arr[i] = int.Parse(reader.ReadLine());
            }
            _arr = QuickSort<int>.Sort(_arr).ToArray();


            for (int i = 0; i < _arr.Length && !reader.EndOfStream; i++)
            {
                writer.WriteLine(_arr[i]);
                _arr[i] = int.Parse(reader.ReadLine());
            }
            writer.Close();
            writer = new StreamWriter(temp2);
            _arr = QuickSort<int>.Sort(_arr).ToArray();
            for (int i = 0; i < _arr.Length; i++)
            {
                writer.WriteLine(_arr[i]);
            }
            writer.Dispose();
            writer.Close();
            reader.Dispose();
            reader.Close();

            Sort_Merge(temp1, temp2, outputPath);
        }
        public static void SortManyFiles(string dataPath, string outputPath, int arrMaxSize)
        {
            int lines = 0;
            StreamReader reader = new StreamReader(dataPath);
            StreamWriter writer;
            while (!reader.EndOfStream)
            {
                reader.ReadLine();
                lines++;
            }
            int parts = (int)Math.Ceiling((double)lines / arrMaxSize);
            reader.Close();
            reader = new StreamReader(dataPath);
            string[] tmps = new string[parts];
            for (int i = 0; i < parts; i++)
            {
                tmps[i] = Path.Combine(Path.GetDirectoryName(dataPath), "tmp", $"temp{i}.txt");
            }

            foreach (string filePath in Directory.GetFiles(Path.GetDirectoryName(tmps[0])))
                File.Delete(filePath);

            for (int i = 0; i < parts; i++)
            {
                writer = new StreamWriter(tmps[i]);
                int[] _arr;
                if (i == parts - 2)
                {
                    if (lines - (arrMaxSize * (i + 1)) == 1)
                        _arr = new int[arrMaxSize - 1];
                    else
                        _arr = new int[arrMaxSize];
                }
                else if (i == parts - 1)
                {
                    if (lines - (arrMaxSize * (i + 1)) == 1)
                        _arr = new int[lines - (arrMaxSize * i) + 1];
                    else
                        _arr = new int[lines - (arrMaxSize * i)];
                }
                else
                    _arr = new int[arrMaxSize];


                for (int j = 0; j < _arr.Length && !reader.EndOfStream; j++)
                {
                    _arr[j] = int.Parse(reader.ReadLine());
                }
                _arr = QuickSort<int>.Sort(_arr).ToArray();


                for (int j = 0; j < _arr.Length; j++)
                {
                    writer.WriteLine(_arr[j]);
                }
                writer.Close();
            }
            reader.Close();
            for (int i = 1; i < parts; i++)
            {
                Sort_Merge(tmps[0], tmps[i], outputPath);
                reader = new StreamReader(outputPath);
                writer = new StreamWriter(tmps[0]);
                while (!reader.EndOfStream)
                {
                    writer.WriteLine(reader.ReadLine());
                }
                reader.Close();
                writer.Close();
            }

            //foreach (string filePath in Directory.GetFiles(Path.GetDirectoryName(tmps[0])))
            //    File.Delete(filePath);
        }
    }
}
