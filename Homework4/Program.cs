namespace Homework4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            string text = "Дано текст в дужках (Речення перше! Речення друге) і ще один текст у дужках (Третє речення. І ще одне речення?)";
            Task1 t1 = new Task1(text);
            Console.WriteLine(string.Join('\n', t1.GetSentencesInBrackets()) + '\n');

            text = "some text without brackets (text. inside brackets!) and (some more text? inside brackets)";
            t1 = new Task1(text);
            Console.WriteLine(string.Join('\n', t1.GetSentencesInBrackets()));
        }
    }
}