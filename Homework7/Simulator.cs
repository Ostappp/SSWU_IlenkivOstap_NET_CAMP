using System.Timers;

namespace Homework7
{
    internal class Simulator
    {
        System.Timers.Timer _timer;
        float _iterationSeconds;
        List<TrafficLights> _lights;
        float timeOfSimulation = 0;
        float _startRecordTime;
        float _stopRecordTime;
        public Simulator(List<TrafficLights> lights, float iterationSeconds = 1f, float stopRecordTime = 0, float startRecordTime = 0)
        {
            if (iterationSeconds <= 0)
                throw new ArgumentException("Tick count must be grather than zero.");

            _lights = new List<TrafficLights>(lights);
            _iterationSeconds = iterationSeconds;
            _timer = new System.Timers.Timer(_iterationSeconds * 1000);

            if (_startRecordTime > _stopRecordTime)
                (startRecordTime, stopRecordTime) = (stopRecordTime, startRecordTime);

            _startRecordTime = startRecordTime;
            _stopRecordTime = stopRecordTime;
        }
        public void Start()
        {
            foreach (TrafficLights light in _lights)
            {
                light.Enable();
            }
            _timer.AutoReset = true;

            _timer.Elapsed += TimerIvent;
            _timer.Start();
            Console.WriteLine($"Iterations: {_iterationSeconds}c");
            Console.WriteLine("Enter any key to stop simulation.");
            if(_stopRecordTime > 0)
                Console.WriteLine($"Time for recording: {_startRecordTime} - {_stopRecordTime}c");
            Console.ReadLine();
            _timer.Stop();
            
        }
        void TimerIvent(object? sender, ElapsedEventArgs e)
        {
            timeOfSimulation += _iterationSeconds;
            if (_stopRecordTime > 0)
            {
                if (timeOfSimulation <= _stopRecordTime && timeOfSimulation >= _startRecordTime)
                {
                    Console.WriteLine($"t = {timeOfSimulation} с");
                    foreach (var light in _lights)
                    {
                        light.SpendTime(_iterationSeconds);
                        Console.WriteLine(light);
                    }
                }
            }

            //else
            //{
            //    timeOfSimulation += _iterationSeconds;
            //    Console.WriteLine($"t = {timeOfSimulation} с");
            //    foreach (var light in _lights)
            //    {
            //        light.SpendTime(_iterationSeconds);
            //        Console.WriteLine(light);
            //    }
            //}
        }
    }
}
