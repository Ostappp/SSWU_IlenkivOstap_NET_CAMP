namespace Homework6
{
    internal class Task2_3
    {// не враховано ефективність yield
        public static IEnumerable<int> Task2(List<int[]> arrays)
        {
            List<int> resArr = new List<int>();
            foreach (int[] arr in arrays)
            {
                resArr.AddRange(arr);
            }
            resArr.Sort();
            for (int i = 0; i < resArr.Count; i++)
            {
                yield return resArr[i];
            }
        }
        public static IEnumerable<string> Task3(string text, string seperators = " ,./?!'\"<>[]{};:+-*/\\")
        {// Щодо сепараторів char.IsPunctuation() не підходить?
            // множина - це набір унікальних елементів...
            List<string> uniqueWords = text.Split(seperators.ToArray(), StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
            for (int i = 0; i < uniqueWords.Count; i++)
            {
                while (uniqueWords.LastIndexOf(uniqueWords[i]) != i)
                    uniqueWords.RemoveAt(uniqueWords.LastIndexOf(uniqueWords[i]));
            }
            for (int i = 0; i < uniqueWords.Count; i++)
            {
                yield return uniqueWords[i];
            }
        }
    }
}
