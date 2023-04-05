using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace Homework4
{
    internal class Task1
    {
        readonly List<char> sentenceEnd = new List<char>() { '.', ',', '!', '?' };

        List<string> textList;
        int stringMaxLenght;
        public Task1(string text, int stringMaxLenght = 80)
        {
            this.stringMaxLenght = stringMaxLenght;
            textList = ConvertTextToList(text);
        }
        public Task1(IEnumerable<string> text)
        {
            textList = text.ToList();
            stringMaxLenght = textList.MaxBy(str => str.Length).Length;
        }
        private List<string> ConvertTextToList(string text)
        {
            List<string> words = text.Split(' ').ToList();

            List<string> strings = new List<string>();
            strings.Add(string.Empty);
            int stringLenght = 0;
            words.ForEach(word =>
            {
                if (stringMaxLenght >= stringLenght + word.Length)
                {
                    strings[strings.Count() - 1] += word + ' ';
                    stringLenght += word.Length + 1;
                }
                else
                {
                    strings.Add(word + ' ');
                    stringLenght = word.Length + 1;
                }
            });
            return strings;
        }
        public void GetSentencesInBrackets()
        {
            List<List<string>> allNestedSentences = new List<List<string>>();
            List<string> tempSentences = new List<string>();
            tempSentences.Add(string.Empty);
            bool insideBrackets = false;

            for (int i = 0; i < textList.Count; i++)
            {
                if (!insideBrackets)
                {
                    if (textList[i].Contains('('))
                    {
                        List<string> words = textList[i].Split(' ').ToList();

                        int cycleEnd = 0;
                        int cycleStart = 0;
                    FindSentances://перепровірити повторне проходження
                        if (textList[i].Contains(')'))
                            cycleEnd = words.FindIndex(cycleStart, w => w.Contains(')'));
                        else
                        {
                            cycleEnd = words.Count - 1;

                        }
                        if (cycleEnd == -1)
                            insideBrackets = true;

                        cycleStart = words.FindIndex(cycleEnd, w => w.Contains('('));
                        if (cycleStart != -1)
                        {
                            for (int j = words.FindIndex(w => w.Contains('(')); j <= cycleEnd; j++)
                            {
                                tempSentences.Add(words[j]);
                            }
                            allNestedSentences.Add(tempSentences);
                            tempSentences = new List<string>();
                            tempSentences.Add(string.Empty);
                            goto FindSentances;
                        }
                    }
                }
                else
                {
                    if (textList[i].Contains(')'))
                    {

                    }
                    else
                    {
                        textList[i].Split(' ').ToList().ForEach(w => tempSentences.Add(w));
                    }
                }

            }
        }
    }
}
