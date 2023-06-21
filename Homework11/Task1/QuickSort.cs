namespace Homework11.Task1
{
    internal static class QuickSort<T> where T : IComparable
    {
        static Random random = new Random();
        static int Median(T[] arr, int low, int high)
        {
            Dictionary<int, int> a = new Dictionary<int, int>();
            for (int i = low; i <= high; i++)
            {
                for (int j = low; j <= high; j++)
                {
                    if (arr[i].CompareTo(arr[j]) >= 0)
                        if (a.ContainsKey(i))
                            a[i]++;
                        else
                            a.Add(i, 1);
                }
            }
            double avr = a.Average(x => x.Value);
            int key = a.OrderBy(x => Math.Abs(x.Value - avr)).First().Key;
            return key;
        }
        
        static int Partition(T[] arr, int low, int high, PivotSelect selector)
        {
            if(selector == PivotSelect.Random)
            {
                int pivotIndex = random.Next(low, high + 1);
                (arr[low], arr[pivotIndex]) = (arr[pivotIndex], arr[low]);
            }
            else
            {
                int pivotIndex = Median(arr, low, high);
                (arr[low], arr[pivotIndex]) = (arr[pivotIndex], arr[low]);                
            }
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
        static IEnumerable<T> Sort(T[] arr, int low, int high, PivotSelect selector)
        {
            if (low < high)
            {
                int pi = Partition(arr, low, high, selector);

                Sort(arr, low, pi - 1, selector);
                Sort(arr, pi + 1, high, selector);
            }
            return new List<T>(arr);
        }
        public static IEnumerable<T> Sort(IEnumerable<T> arr, PivotSelect selector = PivotSelect.First)
        {
            return Sort(arr.ToArray(), 0, arr.Count() - 1, selector);
        }

        public enum PivotSelect
        {
            First,
            Random,
            Median
        }
    }
}
