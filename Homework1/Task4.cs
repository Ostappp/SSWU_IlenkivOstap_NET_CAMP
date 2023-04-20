
using System.Xml.Linq;
// Молодець!
namespace Homework1
{
    internal class Tensor
    {
        //припустимо що прийматимемо лише цілочисельні типи
        private object _data;
        private Type _type;
        public Tensor(object data)
        {
            _data = data;
            _type = data.GetType();
        }
        void IterateArray(Array array, int[] indices, int dimension, ref string str)
        {
            if (dimension == array.Rank)
            {
                // Access the element at the current indices
                if (indices.Length > 1)
                {
                    Array element = (Array)array.GetValue(indices);
                    if (array.GetValue(0).GetType().IsArray)
                    {

                        IterateArray(element, new int[indices.Rank], 0, ref str);
                    }

                }
                else
                {
                    if (array.GetValue(0).GetType().IsArray)
                    {
                        Array element = (Array)array.GetValue(indices);
                        IterateArray(element, new int[indices.Rank], 0, ref str);
                    }
                    else
                        str += $"{array.GetValue(indices)}\t";
                }


            }
            else
            {
                for (int i = 0; i < array.GetLength(dimension); i++)
                {
                    indices[dimension] = i;
                    IterateArray(array, indices, dimension + 1, ref str);
                }
            }
        }
        public override string? ToString()
        {
            string? str = null;
            if (_data != null)
            {
                if (_type.IsArray)
                {
                    Array arr = (Array)_data;
                    IterateArray(arr, new int[arr.Rank], 0, ref str);
                }
                else
                    str = $"{Convert.ChangeType(_data, _type)}";
            }

            return str;
        }

    }
}
