namespace Homework11.Task1
{
    internal static class QuickSort<T> where T : IComparable
    {
        
        static int PartitionFirst(T[] arr, int low, int high)
        {
            T pivot = arr[low];

            int i = low - 1;

            for (int j = low; j <= high; j++)
            {
                if (arr[j].CompareTo(pivot) <= 0)
                {
                    i++;
                    (arr[j], arr[i]) = (arr[i], arr[j]);
                }
            }

            (arr[low], arr[i]) = (arr[i], arr[low]);
            return i;
        }
        static IEnumerable<T> Sort(T[] arr, int low, int high/*, PivotSelect selector*/)
        {
            if (low < high)
            {
                int pi = PartitionFirst(arr, low, high);
                
                //if (selector == PivotSelect.First)
                //    pi = PartitionFirst(arr, low, high);
                //else if (PivotSelect.Random == selector)
                //    pi = PartitionRandom(arr, low, high);
                //else
                //    pi = PartitionMid(arr, low, high);

                Sort(arr, low, pi - 1/*, selector*/);
                Sort(arr, pi + 1, high/*, selector*/);
            }
            return new List<T>(arr);
        }
        public static IEnumerable<T> Sort(IEnumerable<T> arr/*, PivotSelect selector = PivotSelect.First*/)
        {
            return Sort(arr.ToArray(), 0, arr.Count() - 1/*, selector*/);
        }

        public enum PivotSelect
        {
            First,
            Random,
            Median
        }
    }
}
