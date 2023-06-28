using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Homework1
{
    internal class Task1
    {
        static int[,] matrix = new int[,] { { 1 } };
        public static void GenerateSpiralMatrix(int sizeX = 8, int sizeY = 9,
            StartPoint point = StartPoint.BottomRight, Direction direction = Direction.ToRight)
        {
            Console.WriteLine("Task 1:\n\n");

            if (sizeX <= 0 || sizeY <= 0)
            {
                Console.WriteLine("Matrix must have at least one column and row!");
                return;
            }
            matrix = new int[sizeX, sizeY];
            int row = 0, col = 0;//starts from Top Left point
            int shiftX, shiftY;

            if (direction == Direction.ToDown || direction == Direction.ToUp)
            {
                //fills matrix from left to right
                shiftX = 1;
                shiftY = 0;
            }
            else
            {
                //fills matrix from up to down
                shiftX = 0;
                shiftY = 1;
            }

            for (int i = 1; i <= sizeX * sizeY; i++)
            {
                matrix[row, col] = i;
                if (matrix[(row + shiftX + sizeX) % sizeX, (col + shiftY + sizeY) % sizeY] != 0)
                {
                    if (direction == Direction.ToDown || direction == Direction.ToUp)
                        (shiftX, shiftY) = (-shiftY, shiftX);//turn in 90 degree
                    else
                        (shiftX, shiftY) = (shiftY, -shiftX);//turn in 90 degree
                }
                row += shiftX;
                col += shiftY;
            }


            if (!ChangePointAndDir(point, direction))
                Console.WriteLine("Impossible to generate a matrix with a given corner and direction!\nPlease change corner or direction");
            else
                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {
                        Console.Write($"{matrix[i, j]}\t");
                    }
                    Console.WriteLine("\n\n");
                }
        }
        static bool ChangePointAndDir(StartPoint point, Direction direction)
        {
            switch ((point, direction))
            {
                case (StartPoint.TopLeft, Direction.ToDown)://start point and dir
                    return true;
                case (StartPoint.TopLeft, Direction.ToRight)://start point and dir
                    return true;

                case (StartPoint.TopRight, Direction.ToDown)://good
                    Mirror(false);
                    return true;
                case (StartPoint.TopRight, Direction.ToLeft)://good
                    Mirror(false);
                    return true;

                case (StartPoint.BottomLeft, Direction.ToUp)://good
                    Mirror(true);
                    return true;
                case (StartPoint.BottomLeft, Direction.ToRight)://good
                    Mirror(true);
                    return true;

                case (StartPoint.BottomRight, Direction.ToUp)://good
                    Mirror(false);
                    Mirror(true);
                    return true;
                case (StartPoint.BottomRight, Direction.ToLeft)://good
                    Mirror(false);
                    Mirror(true);
                    return true;

                default:
                    return false;
            }
        }
        static void Mirror(bool byX)
        {
            int[,] tempMatrix = matrix.Clone() as int[,];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (byX)
                        matrix[i, j] = tempMatrix[matrix.GetLength(0) - i - 1, j];
                    else
                        matrix[i, j] = tempMatrix[i, matrix.GetLength(1) - j - 1];
                }
            }
        }

        public enum StartPoint
        {
            TopLeft, TopRight,
            BottomLeft, BottomRight,
        }
        public enum Direction
        {
            ToRight, ToLeft,
            ToUp, ToDown,
        }
    }
}
