namespace Homework3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task2 task2 = new Task2();
            string usersText = "some word and another word";
            int? indexOfSecondEntrance = task2.SecondEntryOf(usersText, "word");
            Console.WriteLine(indexOfSecondEntrance == null ? "null" : indexOfSecondEntrance);

            usersText = "some word and another";
            indexOfSecondEntrance = task2.SecondEntryOf(usersText, "word");
            Console.WriteLine(indexOfSecondEntrance == null ? "null" : indexOfSecondEntrance);

            usersText = "Some word And another Word";
            Console.WriteLine(task2.StartedOfUpperCharacter(usersText));

            usersText = "Some wordd And another Wordd";
            usersText = task2.ReplaceWordsWithDoubleCharsBy(usersText,"replacer");
            Console.WriteLine(usersText);
        }
    }
}