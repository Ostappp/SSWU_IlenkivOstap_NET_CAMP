using System;

namespace Homework1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task1.GenerateSpiralMatrix(3, 4, Task1.StartPoint.BottomLeft, Task1.Direction.ToRight);

            Console.WriteLine("\n\nPres any key to continue.\n\n");
            Console.ReadKey();
            Task2 task2 = new Task2();
            task2.GetIndexesAndLenght();

            Console.WriteLine("\n\nPres any key to continue.\n\n");
            Console.ReadKey();
            Task3 task3 = new Task3();
            task3.GenerateVoids();
            task3.FindThroughLinearHole_Print();
            Console.WriteLine($"\n``````````````\nTask 3:\n{task3}");

            Console.WriteLine("\n\nPres any key to continue.\n\n");
            Console.ReadKey();

            Console.WriteLine($"`````````\nTask4\n");
            decimal i = 3;
            Tensor tensor = new Tensor(i);
            int[][][][] a = new int[2][][][]
            {
                new int[2][][] {
                    new int[2][] {
                        new int[] {1, 2},
                        new int[] {3, 4}
                    },
                    new int[1][] {
                        new int[] {5, 6}
                    }
                },
                new int[1][][] {
                    new int[2][] {
                        new int[] {7, 8},
                        new int[] {9, 10}
                    }
                }
            };
            Tensor tensorA = new Tensor(a);
            Console.WriteLine($"Tensor 1 data: {tensor}");
            Console.WriteLine($"Tensor 2 data: {tensorA}");
            

            Console.WriteLine("\n\nPres any key to end.");
            Console.ReadKey();

        }
    }
}