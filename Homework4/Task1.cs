namespace Homework4
{
    internal class Task1
    {// Ці параметри краще робити зовнішніми. Наприклад, якщо потрібно розв'язати цю ж задачу але з квадратними дужками, а клас вже буде закритий, буде прикро...
        readonly private List<char> _sentenceEnd = new List<char> { '.', '!', '?' };
        readonly private List<char> _brackets = new List<char> { '(', ')' };
        readonly private List<string> _textList;
        readonly private int _stringMaxLenght;
        public Task1(string text, int stringMaxLenght = 80, List<char>? sentenceEnd = null, List<char>? brackets = null)
        {
            if (sentenceEnd != null)
                _sentenceEnd = sentenceEnd;
            if (brackets != null)
                _brackets = brackets;

            _stringMaxLenght = stringMaxLenght;
            _textList = ConvertTextToList(text);
        }
        public Task1(IEnumerable<string> text, List<char>? sentenceEnd = null, List<char>? brackets = null)
        {
            if (sentenceEnd != null)
                _sentenceEnd = sentenceEnd;
            if (brackets != null)
                _brackets = brackets;
            if (text.Any())
            {
                _textList = text.ToList();                
            }
            else
            {
                _textList = ConvertTextToList("");
            }
            _stringMaxLenght = _textList.MaxBy(str => str.Length).Length;

        }
        private List<string> ConvertTextToList(string text)
        {
            if (text.Length <= 2)
                return new List<string> { text };
            // Чому ділим лише по пропусках?
            List<string> words = text.Split(' ').ToList();

            List<string> strings = new List<string> { string.Empty };
            int stringLenght = 0;
            words.ForEach(word =>
            {
                if (_stringMaxLenght >= stringLenght + word.Length)
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
        public IEnumerable<string> GetSentencesInBrackets()
        {
            //key is the start of sentance, value - end
            //(int,int) -> item1 - line index, item2 - character index in line
            List<((int, int), (int, int))> allNestedSentences = new List<((int, int), (int, int))>();

            //key - line index, value - char index
            List<(int, int)> bracketIndex = new List<(int, int)>();
            for (int i = 0; i < _textList.Count; i++)
            {
                var indexes = GetBracketsIndexes(_textList[i]);
                if (indexes != null)
                {
                    foreach (var item in indexes)
                    {
                        bracketIndex.Add((i, item));
                    }
                }

            }
//Ви кілька разів проходите текст. можна все зробити за 1 прохід.
            for (int i = 0; i < bracketIndex.Count; i += 2)
            {
                List<(int, int)> localSentencesStart = GetSentencesStartIndexes(_textList.GetRange(bracketIndex[i].Item1, bracketIndex[i + 1].Item1 - bracketIndex[i].Item1 + 1),
                                                                    bracketIndex[i].Item2, bracketIndex[i + 1].Item2, bracketIndex[i].Item1);
                List<(int, int)> localSentencesEnd = new List<(int, int)>();
                for (int j = 1; j < localSentencesStart.Count; j++)
                {
                    var endSentence = localSentencesStart[j];
                    if (endSentence.Item2 == 0)
                        endSentence = (endSentence.Item1 - 1, _textList[endSentence.Item1 - 1].Length - 1);
                    else
                        endSentence = (endSentence.Item1, endSentence.Item2 - 1);

                    localSentencesEnd.Add((endSentence.Item1, endSentence.Item2));
                }
                localSentencesEnd.Add((bracketIndex[i + 1].Item1, bracketIndex[i + 1].Item2 - 1));
                for (int j = 0; j < localSentencesStart.Count; j++)
                {
                    var sentenceStart = localSentencesStart[j];
                    var sentenceEnd = localSentencesEnd[j];

                    allNestedSentences.Add(((sentenceStart.Item1, sentenceStart.Item2), (sentenceEnd.Item1, sentenceEnd.Item2)));
                }
            }

            return GetSentences(allNestedSentences, _textList).Select(x => x.TrimStart());

        }
        private List<int>? GetBracketsIndexes(string line)
        {
            List<int> bracketsIndexes = new List<int>
            {
                line.IndexOfAny(_brackets.ToArray())
            };

            if (bracketsIndexes.Count > 0)
            {
                int index = bracketsIndexes[0];
                while (index >= 0 && index + 1 < line.Length)
                {
                    bracketsIndexes.Add(line.IndexOfAny(_brackets.ToArray(), index + 1));
                    index = bracketsIndexes.Last();
                }

            }
            bracketsIndexes.RemoveAll(x => x == -1);
            return bracketsIndexes.Count > 0 ? bracketsIndexes : null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lines">Lines that contain one open and close brackets</param>
        /// <param name="openBracketIndex">index of open bracket</param>
        /// <param name="closeBracketIndex">index of close bracket in last line</param>
        /// <param name="openBracketLineIndex">index of line with open bracket in global text list</param>
        /// <returns>List where first item is line index in global text and second item is index of first letter of sentence</returns>
        private List<(int, int)> GetSentencesStartIndexes(List<string> lines, int openBracketIndex, int closeBracketIndex, int openBracketLineIndex)
        {
            //key - line index, value - char index
            List<(int, int)> sentanceStartIndex = new List<(int, int)>();
            int startChar = openBracketIndex;
            for (int i = 0; i < lines.Count - 1; i++)
            {

                while (startChar != -1)
                {

                    if (startChar + 1 < lines[i].Length)
                    {
                        sentanceStartIndex.Add((openBracketLineIndex + i, startChar + 1));
                        startChar += 1;
                    }
                    startChar = lines[i].IndexOfAny(_sentenceEnd.ToArray(), startChar);
                }
                startChar = lines[i + 1].IndexOfAny(_sentenceEnd.ToArray(), 0);
            }
            while (startChar != -1)
            {

                if (startChar + 1 < lines.Last().Length)
                {
                    sentanceStartIndex.Add((openBracketLineIndex + lines.Count - 1, startChar + 1));
                    startChar += 1;
                }
                startChar = lines.Last().IndexOfAny(_sentenceEnd.ToArray(), startChar);
            }

            return sentanceStartIndex.Except(
                    sentanceStartIndex.Where(x => x.Item1 == lines.Count - 1 + openBracketLineIndex)
                    .Where(x => x.Item2 >= closeBracketIndex).ToList())
                .ToList();
        }
        private List<string> GetSentences(List<((int, int), (int, int))> sentencesStartEndIndexes, List<string> text)
        {
            var sentences = new List<string>();
            int numerator = 0;
            foreach (var indexes in sentencesStartEndIndexes)
            {
                //на скільки пам'ятаю, казали що конкатинацію можна використовувати коли речееня вже є знайдені і потрібно лише сформувати єдине ціле

                //if sentence has start and end in same line
                if (indexes.Item1.Item1 == indexes.Item2.Item1)
                    sentences.Add(string.Concat(text[indexes.Item1.Item1]
                        .Where((x, i) => i >= indexes.Item1.Item2 && i <= indexes.Item2.Item2)));
                else
                {
                    //add sentence part in first line
                    sentences.Add(string.Concat(text[indexes.Item1.Item1]
                        .Where((x, i) => i >= indexes.Item1.Item2 && i < text[indexes.Item1.Item1].Length)));

                    //add sentence part between first and last line
                    sentences[numerator] += string.Concat(text.Where((t, index) => index > indexes.Item1.Item1 && index < indexes.Item2.Item1)
                        .Select(x => x));

                    //add sentence part in last line
                    sentences[numerator] += string.Concat(text[indexes.Item2.Item1]
                        .Where((x, i) => i >= 0 && i <= indexes.Item2.Item2).Select(x => x));
                }
                numerator++;
            }

            return sentences;
        }

    }
}
