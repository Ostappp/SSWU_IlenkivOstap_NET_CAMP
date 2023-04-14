using System.Text;

namespace Homework4
{
    internal class Program
    {
        const string TASK2_TEXT_PATH = @"..\..\..\Task2_Text.txt";
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("Task 1:\n---------------------------");
            string text = "Дано текст в дужках (Речення перше! Речення друге) і ще один текст у дужках (Третє речення. І ще одне речення?)";
            Task1 t1 = new Task1(text);
            Console.WriteLine($"Текст: {text}\n");
            Console.WriteLine(string.Join('\n', t1.GetSentencesInBrackets()) + '\n');

            text = "some text without brackets (text. inside brackets!) and (some more text? inside brackets)";
            t1 = new Task1(text);
            Console.WriteLine($"Текст: {text}\n");
            Console.WriteLine(string.Join('\n', t1.GetSentencesInBrackets()));

            
            Console.WriteLine("\n\nTask 2:\n---------------------------");
            Task2 t2 = new Task2(ReadData(TASK2_TEXT_PATH));
            List<string> correctMails = t2.GetCorrectMails(out List<string> wrongMails);
            Console.WriteLine("Correct mails:");
            foreach (string mail in correctMails)
            {
                Console.WriteLine($"\t{mail}");
            }
            Console.WriteLine("\nWrong mails:");
            foreach (string mail in wrongMails)
            {
                Console.WriteLine($"\t{mail}");
            }
        }
        public static string ReadData(string path) => File.ReadAllText(path, CodePagesEncodingProvider.Instance.GetEncoding(1251));
    }
}