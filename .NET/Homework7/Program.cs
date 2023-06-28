using Homework7.Lights;

namespace Homework7
{
    internal class Program
    {
        static void Main(string[] args)
        {// Хороша ідея працювати з групами світлофорів, але вона не до кінця реалізована, оскільки Ви не маєте змоги виконувати на рівні клієнтського класу одну дію над всією групою. Для цього треба було створити клас. а не просто список.
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            SimpleLight NorthSouth = new SimpleLight("Північ-південь", 3f, 3f);
            SimpleLight SouthNorth = new SimpleLight("Південь-північ", 3f, 3f);
            SyncLights NorthSouthPair = new SyncLights("Вертикальні", new List<TrafficLights>() { NorthSouth, SouthNorth }, NorthSouth);

            SimpleLight WestEast = new SimpleLight("Захід-схід", 3f, 3f, ITrafficLight.State.Green);
            SimpleLight EastWest = new SimpleLight("Схід-захід", 3f, 3f, ITrafficLight.State.Green);
            SyncLights WestEastPair = new SyncLights("Горизонатльні", new List<TrafficLights>() { WestEast, EastWest }, WestEast);
            Simulator s = new Simulator(new List<TrafficLights> { NorthSouthPair, WestEastPair },2,20);
            s.Start();
            Console.ReadLine(); 
            
        }
    }
}
