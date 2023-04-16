using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5.Task2
{
    internal abstract class ObjectWithSize
    {
        protected readonly double _sizeX;
        protected readonly double _sizeY;
        protected readonly double _sizeZ;
        public (double, double, double) Size { get => (_sizeX, _sizeY, _sizeZ); }
        public ObjectWithSize((double, double, double) size)
        {
            _sizeX = size.Item1;
            _sizeY = size.Item2;
            _sizeZ = size.Item3;
        }
    }
}
