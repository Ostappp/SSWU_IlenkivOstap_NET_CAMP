namespace Homework7.Lights
{
    internal class SimpleLight : TrafficLights
    {

        public SimpleLight(string name, float redTimeSeconds, float greenTimeSeconds, ITrafficLight.State firstState = ITrafficLight.State.Off, float yellowTimeSeconds = 3f) : base(name, GetStateAndTime(redTimeSeconds, greenTimeSeconds, yellowTimeSeconds, firstState)) { }
        static Dictionary<ITrafficLight.State, float> GetStateAndTime(float redTimeSeconds, float greenTimeSeconds, float yellowTimeSeconds, ITrafficLight.State firstState)
        {
            if (redTimeSeconds <= 0)
                throw new ArgumentException("Timer for red light must bre grather than zero.");
            if (greenTimeSeconds <= 0)
                throw new ArgumentException("Timer for green light must bre grather than zero.");

            Dictionary<ITrafficLight.State, float> timeForState = new Dictionary<ITrafficLight.State, float>
            {
                { ITrafficLight.State.Red, redTimeSeconds },
                { ITrafficLight.State.RedYellow, yellowTimeSeconds },
                { ITrafficLight.State.Green, greenTimeSeconds },
                { ITrafficLight.State.Yellow, yellowTimeSeconds },
            };

            if (timeForState.ContainsKey(firstState) && firstState != ITrafficLight.State.Red && firstState != ITrafficLight.State.Off)
            {
                int startIndex = timeForState.Keys.ToList().IndexOf(firstState);
                Dictionary<ITrafficLight.State, float> res = new Dictionary<ITrafficLight.State, float>();
                for (int i = 0; i < timeForState.Count; i++)
                {
                    res.Add(timeForState.ElementAt((startIndex + i) % timeForState.Count).Key, timeForState.ElementAt((startIndex + i) % timeForState.Count).Value);
                }
                return res;
            }



            return timeForState;
        }
    }
}
