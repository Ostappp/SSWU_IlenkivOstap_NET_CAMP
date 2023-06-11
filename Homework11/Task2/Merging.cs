using Homework11.Task1;

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
            if (!reader1.EndOfStream)
            {
                writer.WriteLine(num2);
                while (!reader2.EndOfStream)
                {
                    writer.WriteLine(reader2.ReadLine());
                }
            }
            else
            {
                writer.WriteLine(num1);
                while (!reader1.EndOfStream)
                {
                    writer.WriteLine(reader1.ReadLine());
                }
            }
            reader1.Dispose();
            reader1.Close();
            reader2.Dispose();
            reader2.Close();
            writer.Dispose();
            writer.Close();
        }
        public static void Sort(string path, int count)
        {
            string temp1 = Path.Combine(Path.GetDirectoryName(path), "temp1.txt");
            string temp2 = Path.Combine(Path.GetDirectoryName(path), "temp2.txt");
            StreamReader reader = new StreamReader(path);
            StreamWriter writer = new StreamWriter(temp1);
            int[]_arr = new int[count];
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

            Sort_Merge(temp1, temp2,path);
        }

    }
}
