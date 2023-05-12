using System.Text;

namespace Homework8.Lights
{
    internal class SyncLights : TrafficLights
    {
        private List<TrafficLights> _syncLights;
        public SyncLights(string nameForLightComplex, IEnumerable<TrafficLights> lightsToSync, TrafficLights baseLight) : base(nameForLightComplex, baseLight)
        {
            if (!lightsToSync.Any())
                throw new ArgumentException("List of traffic lights to syncronize can't be empty"); 
            if (baseLight == null)
                throw new ArgumentException("Base traffic light can't be null");

            _syncLights = new List<TrafficLights>();
            foreach (var light in lightsToSync)
            {
                if (light.GetType() == baseLight.GetType())
                    _syncLights.Add(light);
            }
            foreach (var light in _syncLights)
                light.StateSetter(baseLight.CurrentState);

            ChangingState += ChangeState;
        }

        public override void Enable()
        {
            base.Enable();
            foreach (var light in _syncLights)
                light.Enable();
        }

        public override void Disable()
        {
            base.Disable();
            foreach (var light in _syncLights)
                light.Disable();

        }
        public override void SpendTime(float seconds)
        {
            base.SpendTime(seconds);
            foreach (var light in _syncLights)
                light.SpendTime(seconds);
        }
        private void ChangeState()
        {
            foreach (var light in _syncLights)
                light.StateSetter(CurrentState);
        }
        public override string? ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Group name: {Name}");
            foreach (var light in _syncLights)
                builder.AppendLine("\t" + light.ToString().Replace("\n","\n\t"));
            return builder.ToString();
        }


    }
}
