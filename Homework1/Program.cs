using System;

namespace Homework1
{
    internal class Program
    {//fkghsdlfhgfdkhg
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
            task3.FindThroughLinearHole();

            Console.WriteLine("\n\nPres any key to continue.\n\n");
            Console.ReadKey();
            decimal i = 3;
            Tensor tensor = new Tensor(i);
            int[] a = new int[3];
            Tensor tensorA = new Tensor(a);
            float[,] b = new float[3, 4];
            Tensor tensorB = new Tensor(b);
            long[][][] c = new long[3][][];
            Tensor tensorC = new Tensor(c);
            double[,][,,] d = new double[3,3][,,];
            Tensor tensorD = new Tensor(d); 
            double[,][,,] e = new double[3, 4][,,];
            Tensor tensorE = new Tensor(e);           
            Console.WriteLine($"`````````\nTask4\nDoes tesnorD has same size and type as tensorE: {tensorD.Compare(tensorE)}");

            Console.WriteLine("\n\nPres any key to end.");
            Console.ReadKey();

        }
    }
}
