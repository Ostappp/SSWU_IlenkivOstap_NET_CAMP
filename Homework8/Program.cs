using Homework8.Lights;

namespace Homework8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            System.Timers.Timer timer = new System.Timers.Timer(1000);
            
            SimpleLight Light1 = new SimpleLight("1.1", 3f, 3f, ITrafficLight.State.Off, new List<ITrafficLight.Dir> { ITrafficLight.Dir.Forward, ITrafficLight.Dir.Left });
            LightExtension ExtendedLight1 = new LightExtension("1.2.1", new List<ITrafficLight.Dir> { ITrafficLight.Dir.Right }, 9f, 3f, ITrafficLight.State.Green);
            SimpleLight Light2 = new SimpleLight("1.2", Light1);

            SimpleLight Light3 = new SimpleLight("2.1", Light1);
            LightExtension ExtendedLight2 = new LightExtension("2.2.1", ExtendedLight1);
            SimpleLight Light4 = new SimpleLight("2.2", Light1);

            SimpleLight Light5 = new SimpleLight("3", 3f, 3f, ITrafficLight.State.Green);
            SimpleLight Light6 = new SimpleLight("4", 3f, 3f);

            SyncLights GroupA1 = new SyncLights("A1", new List<TrafficLights> { Light1, Light2, Light4, Light3 }, Light1);
            SyncLights GroupA2 = new SyncLights("A2", new List<TrafficLights> { ExtendedLight1, ExtendedLight2 }, ExtendedLight1);
            SyncLights GroupB = new SyncLights("B", new List<TrafficLights> { Light5, Light6 }, Light5);
            
            RoadIntersection intersection1 = new RoadIntersection("Road Intersection 1", new List<TrafficLights> { GroupA1, GroupA2, GroupB }, timer);




            SimpleLight Light7 = new SimpleLight("5.1", 3f, 3f, ITrafficLight.State.Green, new List<ITrafficLight.Dir> { ITrafficLight.Dir.Left });
            SimpleLight Light8 = new SimpleLight("5.2", Light7, new List<ITrafficLight.Dir> { ITrafficLight.Dir.Right });
            SimpleLight Light9 = new SimpleLight("6.1", 3f, 3f, ITrafficLight.State.Red, new List<ITrafficLight.Dir> { ITrafficLight.Dir.Forward });
            SimpleLight Light10 = new SimpleLight("6.2", Light9);

            SyncLights GroupC = new SyncLights("C", new List<TrafficLights> { Light7, Light8 }, Light7);
            SyncLights GroupD = new SyncLights("D", new List<TrafficLights> { Light9, Light10 }, Light9);

            RoadIntersection intersection2 = new RoadIntersection("Road Intersection 2", new List<TrafficLights> { GroupC, GroupD }, timer);

            intersection1.Start();
            intersection2.Start();
            Console.ReadLine();

        }
    }
}