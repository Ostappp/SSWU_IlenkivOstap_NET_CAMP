using System.Text;

namespace Homework4
{
    internal class Program
    {
        const string TASK2_TEXT_PATH = @"..\..\..\Task2_Text.txt";
        const string TASK3_INPUT_PATH = @"..\..\..\Task3_Data.txt";
        static readonly string[] TASK3_OUTPUT_PATH = new string[]
        {
            @"..\..\..\Task3_Outputs\TotalReport.txt",
            @"..\..\..\Task3_Outputs\SingleFlatReport.txt",
            @"..\..\..\Task3_Outputs\TotalSpending.txt",
            @"..\..\..\Task3_Outputs\TimeFromLastReport.txt",
        };
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

            Console.WriteLine("\n\nTask 3:\n---------------------------");
            Task3 t3 = new Task3(ReadData(TASK3_INPUT_PATH));
            WriteDataLines(TASK3_OUTPUT_PATH[0], t3.GetTotalReport());
            WriteDataLines(TASK3_OUTPUT_PATH[1], new string[] { t3.GetReportByApartment(30) });
            Console.WriteLine($"Найбільший борг за електроенергію при ціні {1.44:c2} у {t3.GetSurnameWithHighestArrears(1.44)}");
            if (t3.GetAppartmentNumWithoutElectricityUse().Any())
                Console.WriteLine($"Номер квартири де не використовувалась електроенергія: {t3.GetAppartmentNumWithoutElectricityUse()}.");
            else
                Console.WriteLine("Немає квартир, що не використовували електроенергію протягом кврталу");
            WriteDataLines(TASK3_OUTPUT_PATH[2], t3.TotalSpending(1.44));
            WriteDataLines(TASK3_OUTPUT_PATH[3], t3.TimeFromLastMeterReading());
        }
        static string ReadData(string path) => File.ReadAllText(path, Encoding.UTF8/*CodePagesEncodingProvider.Instance.GetEncoding(1251)*/);
        static void WriteDataLines(string path, IEnumerable<string> lines) => File.WriteAllLines(path, lines, Encoding.UTF8);
    }
}