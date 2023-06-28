using System.Drawing;

namespace Homework6
{// сумарний бал - 92
    internal class Program
    {
        static void Main(string[] args)
        {
            //Task 1
            Console.WriteLine("``````````````````\nTask1:\n");

            Task1 task1 = new Task1(10);
            Console.WriteLine(task1);

            foreach (int i in task1)
            {
                Console.Write(i + " ");
            }

            //Task 2
            Console.WriteLine("\n\n``````````````````\nTask2:\n");

            int[] arr = { 1, 2, 5 }, arr1 = { 7, -2, 3 };
            //List<int> newArr = Task2_3.Task2(new List<int[]> { arr, arr1 }).ToList();
            foreach (int i in Task2_3.Task2(new List<int[]> { arr, arr1 }))
            {
                Console.Write(i + " ");
            }

            //Task 3
            Console.WriteLine("\n\n``````````````````\nTask3:\n");

            string text = "text: one, two,  three, three, one, four, text";
            foreach (string word in Task2_3.Task3(text))
            {
                Console.WriteLine(word);
            }
        }
    }
}
