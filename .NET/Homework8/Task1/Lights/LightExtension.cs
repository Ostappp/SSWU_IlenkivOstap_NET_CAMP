using Homework8.Task1.LightAbstraction;

namespace Homework8.Task1.Lights
{
    internal class LightExtension : TrafficLights
    {
        public LightExtension(string name, IEnumerable<ITrafficLight.Dir> lightDirections, float OFFtime, float ONtime, ITrafficLight.State firstState = ITrafficLight.State.Off) : base(name, BaseSettings(ONtime, OFFtime, firstState), lightDirections)
        {

        }
        public LightExtension(string name, LightExtension original, IEnumerable<ITrafficLight.Dir>? lightDirections = null) : base(name, original, lightDirections)
        {

        }
        static Dictionary<ITrafficLight.State, float> BaseSettings(float ONtime, float OFFtime, ITrafficLight.State firstState)
        {
            if (firstState == ITrafficLight.State.Green)
                return new Dictionary<ITrafficLight.State, float>()
                {
                    { ITrafficLight.State.Green, ONtime },
                    { ITrafficLight.State.None, OFFtime }
                };
            else
                return new Dictionary<ITrafficLight.State, float>()
                {
                    { ITrafficLight.State.None, OFFtime },
                    { ITrafficLight.State.Green, ONtime }
                };
        }
    }
}
