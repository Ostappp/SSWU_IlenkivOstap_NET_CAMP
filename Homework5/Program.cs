namespace Homework5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {//Task1
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
            }

        }
    }
}