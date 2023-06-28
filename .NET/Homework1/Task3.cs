namespace Homework1
{
    internal class Task3
    {// У Вас є подібний код, який повторюється і який можна записати значно компактніше.
        private int _size;
        private int[,,] _cube;
        private ThroughLines lines;
        public Task3(int[,,] cube)
        {
            _size = cube.GetLength(0);
            _cube = cube;
        }
        public Task3(int size = 3)
        {
            _size = size;
            _cube = new int[_size, _size, _size];
        }
        public int[,,] GenerateVoids()
        {
            Random random = new Random();
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    for (int k = 0; k < _size; k++)
                    {
                        _cube[i, j, k] = random.Next(2);
                    }
                }
            }
            return _cube;
        }
        public void FindThroughLinearHole_Print()
        {
            //vertical/horizontals perralels
            List<(int, int)> XZlines = new List<(int, int)>();//x,z - coords
            for (int x = 0; x < _size; x++)
            {
                for (int z = 0; z < _size; z++)
                {
                    XZlines.Add((x, z));
                    for (int y = 0; y < _size; y++)
                    {
                        if (_cube[x, y, z] == 0)
                        {
                            XZlines.Remove((x, z));
                            break;
                        }

                    }
                }
            }
            List<(int, int)> XYlines = new List<(int, int)>();//x,y - coords
            for (int x = 0; x < _size; x++)
            {
                for (int y = 0; y < _size; y++)
                {
                    XYlines.Add((x, y));
                    for (int z = 0; z < _size; z++)
                    {
                        if (_cube[x, y, z] == 0)
                        {
                            XYlines.Remove((x, y));
                            break;
                        }

                    }
                }
            }
            List<(int, int)> YZlines = new List<(int, int)>();//y,z - coords
            for (int y = 0; y < _size; y++)
            {
                for (int z = 0; z < _size; z++)
                {
                    YZlines.Add((y, z));
                    for (int x = 0; x < _size; x++)
                    {
                        if (_cube[x, y, z] == 0)
                        {
                            YZlines.Remove((y, z));
                            break;
                        }

                    }
                }
            }

            //Plane dizgonals
            //XYDiagonals
            //Top left to bottom right
            List<(int, int, int)> XYDiagonal = new List<(int, int, int)>();
            for (int z = 0; z < _size; z++)
            {
                for (int y = 0; y < _size; y++)
                {
                    (int, int, int) listCoord = (0, 0, 0);
                    for (int x = 0; x < _size; x++)
                    {

                        if (x == 0 || (x + y) % _size == 0)
                        {
                            XYDiagonal.Add((x, (x + y) % _size, z));
                            listCoord = (x, (x + y) % _size, z);
                        }
                        if (_cube[x, (x + y) % _size, z] == 0)
                        {
                            XYDiagonal.Remove(listCoord);
                            break;
                        }
                    }
                }
            }
            //Bottom left to top right
            List<(int, int, int)> XYDiagonal1 = new List<(int, int, int)>();
            for (int z = 0; z < _size; z++)
            {
                for (int x = _size - 1; x >= 0; x--)
                {
                    (int, int, int) listCoord = (0, 0, 0);
                    for (int y = 0; y < _size; y++)
                    {

                        if (((x - y) % _size + _size) % _size == _size - 1 || y == 0)
                        {
                            XYDiagonal1.Add((((x - y) % _size + _size) % _size, y, z));
                            listCoord = (((x - y) % _size + _size) % _size, y, z);
                        }
                        if (_cube[((x - y) % _size + _size) % _size, y, z] == 0)
                        {
                            XYDiagonal1.Remove(listCoord);
                            break;
                        }
                    }
                }
            }
            //XzDiagonals
            //Top left to bottom right
            List<(int, int, int)> XZDiagonal = new List<(int, int, int)>();
            for (int y = 0; y < _size; y++)
            {
                for (int z = 0; z < _size; z++)
                {
                    (int, int, int) listCoord = (0, 0, 0);
                    for (int x = 0; x < _size; x++)
                    {

                        if (x == 0 || (x + y) % _size == 0)
                        {
                            XZDiagonal.Add((x, (x + y) % _size, z));
                            listCoord = (x, (x + y) % _size, z);
                        }
                        if (_cube[x, (x + y) % _size, z] == 0)
                        {
                            XZDiagonal.Remove(listCoord);
                            break;
                        }
                    }
                }
            }
            //Bottom left to top right
            List<(int, int, int)> XZDiagonal1 = new List<(int, int, int)>();
            for (int y = 0; y < _size; y++)
            {
                for (int x = _size - 1; x >= 0; x--)
                {
                    (int, int, int) listCoord = (0, 0, 0);
                    for (int z = 0; z < _size; z++)
                    {

                        if (((x - y) % _size + _size) % _size == _size - 1 || y == 0)
                        {
                            XZDiagonal1.Add((((x - y) % _size + _size) % _size, y, z));
                            listCoord = (((x - y) % _size + _size) % _size, y, z);
                        }
                        if (_cube[((x - y) % _size + _size) % _size, y, z] == 0)
                        {
                            XZDiagonal1.Remove(listCoord);
                            break;
                        }
                    }
                }
            }

            //YZDiagonals
            //Top left to bottom right
            List<(int, int, int)> YZDiagonal = new List<(int, int, int)>();
            for (int x = 0; x < _size; x++)
            {
                for (int z = 0; z < _size; z++)
                {
                    (int, int, int) listCoord = (0, 0, 0);
                    for (int y = 0; y < _size; y++)
                    {

                        if (x == 0 || (x + y) % _size == 0)
                        {
                            YZDiagonal.Add((x, (x + y) % _size, z));
                            listCoord = (x, (x + y) % _size, z);
                        }
                        if (_cube[x, (x + y) % _size, z] == 0)
                        {
                            YZDiagonal.Remove(listCoord);
                            break;
                        }
                    }
                }
            }
            //Bottom left to top right
            List<(int, int, int)> YZDiagonal1 = new List<(int, int, int)>();
            for (int x = 0; x < _size; x++)
            {
                for (int y = _size - 1; y >= 0; y--)
                {
                    (int, int, int) listCoord = (0, 0, 0);
                    for (int z = 0; z < _size; z++)
                    {

                        if (((x - y) % _size + _size) % _size == _size - 1 || y == 0)
                        {
                            YZDiagonal1.Add((((x - y) % _size + _size) % _size, y, z));
                            listCoord = (((x - y) % _size + _size) % _size, y, z);
                        }
                        if (_cube[((x - y) % _size + _size) % _size, y, z] == 0)
                        {
                            YZDiagonal1.Remove(listCoord);
                            break;
                        }
                    }
                }
            }



            lines.XYlines = XYlines;
            lines.XZlines = XZlines;
            lines.YZlines = YZlines;

            lines.XYDiagonal = XYDiagonal;
            lines.XYDiagonal1 = XYDiagonal1;

            lines.XZDiagonal = XZDiagonal;
            lines.XZDiagonal1 = XZDiagonal1;

            lines.YZDiagonal = YZDiagonal;
            lines.YZDiagonal1 = YZDiagonal1;


        }

        public override string? ToString()
        {
            string? str = null;

            str = "\nOY parrallel through lines:\n";
            for (int i = 0; i < lines.XZlines.Count; i++)
                str += $"\tstart point ({lines.XZlines[i].Item1}, 0, {lines.XZlines[i].Item2}); end point ({lines.XZlines[i].Item1}, {_size - 1}, {lines.XZlines[i].Item2})\n";

            str += "\nOZ through lines:\n";
            for (int i = 0; i < lines.XYlines.Count; i++)
                str += $"\tstart point ({lines.XYlines[i].Item1}, {lines.XYlines[i].Item2}, 0); end point ({lines.XYlines[i].Item1}, {lines.XYlines[i].Item2}, {_size - 1})\n";

            str += "\nOX through lines:\n";
            for (int i = 0; i < lines.YZlines.Count; i++)
                str += $"\tstart point (0, {lines.YZlines[i].Item1}, {lines.YZlines[i].Item2}); end point ({_size - 1}, {lines.YZlines[i].Item1}, {lines.YZlines[i].Item2})\n";


            str += "\nXY panel diagonals parrallel through lines:\n";
            for (int i = 0; i < lines.XYDiagonal.Count; i++)
                str += $"\tstart point ({lines.XYDiagonal[i].Item1}, {lines.XYDiagonal[i].Item2}, {lines.XYDiagonal[i].Item3}); " +
                    $"end point ({_size - 1 - lines.XYDiagonal[i].Item2}, {_size - 1 - lines.XYDiagonal[i].Item1}, {lines.XYDiagonal[i].Item3})\n";
            for (int i = 0; i < lines.XYDiagonal1.Count; i++)
                str += $"\tstart point ({lines.XYDiagonal1[i].Item1}, {lines.XYDiagonal1[i].Item2}, {lines.XYDiagonal1[i].Item3}); " +
                    $"end point ({lines.XYDiagonal1[i].Item2}, {lines.XYDiagonal1[i].Item1}, {lines.XYDiagonal1[i].Item3})\n";

            str += "\nXZ panel diagonals parrallel through lines:\n";
            for (int i = 0; i < lines.XZDiagonal.Count; i++)
                str += $"\tstart point ({lines.XZDiagonal[i].Item1}, {lines.XZDiagonal[i].Item2}, {lines.XZDiagonal[i].Item3}); " +
                    $"end point ({_size - 1 - lines.XZDiagonal[i].Item3}, {lines.XZDiagonal[i].Item1}, {_size - 1 - lines.XZDiagonal[i].Item1})\n";
            for (int i = 0; i < lines.XZDiagonal1.Count; i++)
                str += $"\tstart point ({lines.XZDiagonal1[i].Item1}, {lines.XZDiagonal1[i].Item2}, {lines.XZDiagonal1[i].Item3}); " +
                    $"end point ({lines.XZDiagonal1[i].Item3}, {lines.XZDiagonal1[i].Item2}, {lines.XZDiagonal1[i].Item1})\n";

            str += "\nYZ panel diagonals parrallel through lines:\n";
            for (int i = 0; i < lines.YZDiagonal.Count; i++)
                str += $"\tstart point ({lines.YZDiagonal[i].Item1}, {lines.YZDiagonal[i].Item2}, {lines.YZDiagonal[i].Item3}); " +
                    $"end point ({lines.YZDiagonal[i].Item1}, {_size - 1 - lines.YZDiagonal[i].Item3}, {_size - 1 - lines.YZDiagonal[i].Item2})\n";
            for (int i = 0; i < lines.YZDiagonal1.Count; i++)
                str += $"\tstart point ({lines.YZDiagonal1[i].Item1}, {lines.YZDiagonal1[i].Item2}, {lines.YZDiagonal1[i].Item3}); " +
                    $"end point ({lines.YZDiagonal1[i].Item1}, {lines.YZDiagonal1[i].Item3}, {lines.YZDiagonal1[i].Item2})\n";
            return str;
        }

        struct ThroughLines
        {
            public List<(int, int)> XZlines;
            public List<(int, int)> XYlines;
            public List<(int, int)> YZlines;

            public List<(int, int, int)> XYDiagonal;
            public List<(int, int, int)> XYDiagonal1;

            public List<(int, int, int)> XZDiagonal;
            public List<(int, int, int)> XZDiagonal1;

            public List<(int, int, int)> YZDiagonal;
            public List<(int, int, int)> YZDiagonal1;
        }
    }

}
