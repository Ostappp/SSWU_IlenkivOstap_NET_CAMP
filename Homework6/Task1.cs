using System.Collections;
using System.Text;

namespace Homework6
{
    internal class Task1 : IEnumerable
    {
        private int[,] _matrix;
        public Task1(int[,] matrix)
        {//Черговий раз одна і та ж помилка!!! Я вже втомилась про неї писати і казати!!!
            _matrix = matrix;
        }
        public Task1(uint size = 6)
        {
            _matrix = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    _matrix[i, j] = (int)((i * (size - 1)) + i + j);
                }
            }
        }

        public IEnumerator GetEnumerator()
        {// такий підхід може бути тільки для квадратних матриць. Тому слід здійснювати перевірку
            int sizeX = _matrix.GetLength(0);
            int sizeY = _matrix.GetLength(1);
            int index = 0;
            bool isUp = false;
            int x = 0, y = 0;
            while (index < sizeX * sizeY)
            {

                yield return _matrix[x, y];
                index++;
                if (isUp)
                {// Не оптимально на кожному елементі перепитувати, чи це не кінець лінії вітки.
                    if (y == sizeY - 1)
                    {
                        x++;
                        isUp = false;
                    }
                    else if (x == 0)
                    {
                        y++;
                        isUp = false;
                    }
                    else
                        (x, y) = Shift((x, y), isUp);


                }
                else
                {
                    if (x == sizeX - 1)
                    {
                        y++;
                        isUp = true;
                    }
                    else if (y == 0)
                    {
                        x++;
                        isUp = true;
                    }
                    else
                        (x, y) = Shift((x, y), isUp);
                }

            }
        }

        public override string? ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    sb.Append(_matrix[i, j] + "\t");
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }

        (int, int) Shift((int, int) index, bool isUp) => isUp ? (index.Item1 - 1, index.Item2 + 1) : (index.Item1 + 1, index.Item2 - 1);
    }
}
/*
1   2   3   4   5   6
7   8   9   10  11  12
13  14  15  16  17  18
19  20  21  22  23  24  
25  26  27  28  29  30


1   3   4   10  11
2   5   9   12  19
6   8   13  18  20
7   14  17  21  24
15  16  22  23  25

1   3   4   10  11  21  22  36  37
2   5   9   12  20  23  35  38
6   8   13  19  24  34  39
7   14  18  25  33  40
15  17  26  32  41
16  27  31  42
28  30  43
29  44
45

0   1   2   3   4   5   6   7   8   9   10
1
2
3
4
5
6
7
8
9
10
*/
