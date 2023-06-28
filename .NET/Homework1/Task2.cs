namespace Homework1
{
    internal class Task2
    {
        private int _sizeX;
        private int _sizeY;
        private int[,] _matrix;
        public Task2(int sizeX = 10, int sizeY = 10)
        {

            _sizeX = sizeX;
            _sizeY = sizeY;
            _matrix = new int[_sizeX, _sizeY];

            Random rnd = new Random();
            for (int i = 0; i < _sizeX; i++)
            {
                for (int j = 0; j < _sizeY; j++)
                {
                    _matrix[i, j] = rnd.Next(0, 17);
                }
            }

        }
        public Task2(int[,] matrix)
        {
            _matrix = matrix;
            _sizeX = matrix.GetLength(0);
            _sizeY = matrix.GetLength(1);
        }

        public void GetIndexesAndLenght()
        {
            Console.WriteLine("```````````\nTask 2:\n\n");
            if (_sizeX <= 0 || _sizeY <= 0)
                Console.WriteLine("Matrix must have at least one column and row!");
            else
            {
                for (int i = 0; i < _sizeX; i++)
                {
                    for (int j = 0; j < _sizeY; j++)
                    {
                        Console.Write($"{_matrix[i, j]}\t");
                    }
                    Console.WriteLine("\n\n");
                }

                (int, int, int) data = (0, 0, 1);//line/start/count
                for (int i = 0; i < _sizeX; i++)
                {
                    (int, int) localData = (0, 0);//start/count
                    for (int j = 1; j < _sizeY; j++)
                    {
                        if (_matrix[i, j] == _matrix[i, j - 1])
                        {
                            localData = (localData.Item1, localData.Item2 + 1);
                            if (localData.Item2 > data.Item3)
                                data = (i, localData.Item1, localData.Item2);
                        }
                        else
                            localData = (j, 1);
                    }
                }
                if (data == (0, 0, 1))
                    Console.WriteLine("All lines has same lenght!");
                else
                    Console.WriteLine($"The longest line is the line with colour {_matrix[data.Item1, data.Item2]} in row with index {data.Item1}. Its lenght is {data.Item3}. " +
                        $"Index of start position is {data.Item2} and index of end position is {data.Item2 + data.Item3 - 1}!");

            }
        }
    }
}
