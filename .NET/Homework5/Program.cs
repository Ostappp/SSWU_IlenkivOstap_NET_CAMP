using Homework5.Task2;
using System.Globalization;
Ostap	Ilenkiv			90	15	20	95	85	95	100	110,5. Вітаю Вас. Ви в другому турі.
namespace Homework5
{
    internal class Program
    {
        static void Main()
        {

            //Task1
            Task1 garden1 = new Task1(50);
            Task1 garden2 = new Task1(75);
            Console.WriteLine($"First garden's fence length: {garden1.FenceLength}");
            Console.WriteLine($"Second garden's fence length: {garden2.FenceLength}");

            Console.WriteLine($"Is first garden's fence longer than second garden: {garden1 > garden2}");
            List<(float, float)> treeInGarden = new List<(float, float)>
                {
                    (-1, 1 ),               (1, 1),
                    (-1, 0 ),   (0, 0),     (1, 0),
                    (-1, -1),               (1, -1),
                };
            Console.WriteLine($"{new Task1(treeInGarden).FenceLength}\n\n");
            treeInGarden = new List<(float, float)>
                {
                    (-1, 1 ),
                    (-1, 0 ),   (0, 0),     (1, 0),
                    (-1, -1),   (0, -1),    (1, -1),
                };
            Console.WriteLine($"{new Task1(treeInGarden).FenceLength}\n\n");
            treeInGarden = new List<(float, float)>
                {
                    (-1, 1 ),
                    (-1, 0 ),   (0, 0),
                    (-1, -1),   (0, -1),     (1, -1),
                };
            Console.WriteLine($"{new Task1(treeInGarden).FenceLength}\n\n");
            treeInGarden = new List<(float, float)>
                {
                    (-1, 1 ),
                                (0, 0),
                    (-1, -1),               (1, -1),
                };
            Console.WriteLine($"{new Task1(treeInGarden).FenceLength}\n\n");
            treeInGarden = new List<(float, float)>
                {
                                (0, 0),
                    (-1, -1),               (1, -1),
                };
            Console.WriteLine($"{new Task1(treeInGarden).FenceLength}\n\n"); treeInGarden = new List<(float, float)>
                {
                    (-1, 1),                (1, 1),
                                (0, 0),
                    (-1, -1),               (1, -1),
                };
            Console.WriteLine($"{new Task1(treeInGarden).FenceLength}\n\n");
            treeInGarden = new List<(float, float)>
                {
                                            (1, 1),
                    (-1, 0 ),   (0, 0),     (1, 0),
                    (-1, -1),               (1, -1),
                };
            Console.WriteLine($"{new Task1(treeInGarden).FenceLength}\n\n");
            treeInGarden = new List<(float, float)>
                {
                    (-1, 1 ),
                    (-1, 0 ),   (0, 0),
                                (0, -1),     (1, -1),
                };
            Console.WriteLine($"{new Task1(treeInGarden).FenceLength}\n\n");
            




            //Task2

            SuperUser user = new SuperUser();
            Store store = new Store(user, "Metro", "Some Address");
            user.AddStore(store);
            string managersCommands = File.ReadAllText("..\\..\\..\\Task2\\StoreConfig.txt");
            //user.AddObjToManage(store);
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("uk-UA");
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            user.ManageObject(store, managersCommands, out string report);
            Console.WriteLine($"\n\n{report}");
            CLI<SuperUser> cli = new CLI<SuperUser>(user);
            cli.OpenCLI();



        }
    }
}
