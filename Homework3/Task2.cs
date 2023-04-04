namespace Homework3
{
    internal class Task2
    {
        public int? SecondEntryOf(string usersText, string subString)
        {
            List<string> str = usersText.Split(' ').ToList();
            str = SeperateWordAndPunctual(str);
            int index = str.FindIndex(str.FindIndex(s => s == subString) + 1, s => s == subString);

            return index >= 0 ? index : null;
        }
        public int StartedOfUpperCharacter(string usersText)
        {
            List<string> str = usersText.Split(' ').ToList();
            str = SeperateWordAndPunctual(str);
            int counter = 0;

            str.ForEach(s => { if (char.IsUpper(s[0])) { counter += 1; } });
            return counter;
        }
        public string ReplaceWordsWithDoubleCharsBy(string usersText, string replacer)
        {
            List<string> str = usersText.Split(' ').ToList();
            str = SeperateWordAndPunctual(str);
            for (int i = 0; i < str.Count; i++)
            {
                if (HasDoubledChar(str[i]))
                    str[i] = replacer;
            }

            return string.Join(' ', str);
        }
        private bool HasDoubledChar(string str)
        {
            if (str == null || str.Length <= 1)
                return false;
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i - 1] == str[i])
                    return true;
            }
            return false;
        }
        private List<string> SeperateWordAndPunctual(List<string> strings)
        {
            List<string> strs = new List<string>();

            foreach (string str in strings)
            {
                string temp = string.Empty;
                for (int i = 0; i < str.Length; i++)
                {

                    if (!char.IsPunctuation(str[i]))
                        temp += str[i];

                    else
                    {
                        strs.Add(temp);
                        strs.Add(str[i].ToString());
                        temp = string.Empty;
                    }
                }
                strs.Add(temp);
            }

            return strs;
        }
    }
}
