using System.Timers;
using Homework8.Task1.LightAbstraction;

namespace Homework8.Task1
{
    internal class RoadIntersection
    {
        private readonly string _name;
        public string Name { get => new(_name); }
        protected List<TrafficLights> _lights;

        protected System.Timers.Timer _timer;
        float timeOfSimulation = 0;
        readonly bool _isLocalTimer;
        public RoadIntersection(string name, List<TrafficLights> lights, float iterationSeconds = 1f)
        {
            if (iterationSeconds <= 0)
                throw new ArgumentException("Tick count must be grather than zero.");

            _name = new(name);
            _lights = new List<TrafficLights>(lights);
            _timer = new System.Timers.Timer(iterationSeconds * 1000);
            _timer.AutoReset = true;

            _isLocalTimer = true;
        }
        public RoadIntersection(string name, List<TrafficLights> lights, System.Timers.Timer timer)
        {
            _name = new(name);
            _lights = new List<TrafficLights>(lights);
            _timer = timer;

            _isLocalTimer = false;
        }
        /// <summary>
        /// Enables all traffic lights and subscribes to timer.
        /// </summary>
        public virtual void Start()
        {
            foreach (TrafficLights light in _lights)
            {
                light.Enable();
            }

            _timer.Elapsed -= TimerIvent;
            _timer.Elapsed += TimerIvent;

            _timer.Start();
        }
        /// <summary>
        /// Unsubscribes from timer and if timer belongs to object stops it.
        /// </summary>
        public virtual void Stop()
        {
            _timer.Elapsed -= TimerIvent;
            if (_isLocalTimer)
            {
                _timer.Stop();
                _timer.Dispose();
            }
        }
        protected void TimerIvent(object? sender, ElapsedEventArgs e)
        {
            timeOfSimulation += (float)(_timer.Interval / 1000.0);
            Console.WriteLine($"{this}\nt = {timeOfSimulation} s.");
            foreach (var light in _lights)
            {
                light.SpendTime((float)(_timer.Interval / 1000.0));
                Console.WriteLine(light);
            }
            Console.WriteLine("\n\n");
        }
        public override string ToString()
        {
            return $"Intersection's name: {Name}. Update time: {_timer.Interval / 1000.0}s. Traffic lights count: {_lights.Count}.";
        }
    }
}
