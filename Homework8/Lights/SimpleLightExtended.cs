using System.Text;

namespace Homework8.Lights
{
    internal class SimpleLightExtended : SimpleLight
    {
        private List<LightExtension> _extentions;
        public List<ITrafficLight> Extensions { get => _extentions.Cast<ITrafficLight>().ToList(); }

        public SimpleLightExtended(string name, SimpleLight original, List<LightExtension> extensions)
            : base(name, original)
        {
            if (extensions.Any())
                _extentions = extensions;
            else
                throw new ArgumentException("List of extensions can't be empty");

        }

        public SimpleLightExtended(string name, List<LightExtension> extensions, float redTimeSeconds, float greenTimeSeconds,
            IEnumerable<ITrafficLight.Dir>? lightDirs = null, ITrafficLight.State firstState = ITrafficLight.State.Off, float yellowTimeSeconds = 3f)
            : base(name, redTimeSeconds, greenTimeSeconds, firstState, lightDirs, yellowTimeSeconds)
        {
            if (extensions.Any())
                _extentions = extensions;
            else
                throw new ArgumentException("List of extensions can't be empty");

        }
        public override void Enable()
        {
            base.Enable();
            foreach (var light in _extentions)
                light.Enable();
        }

        public override void Disable()
        {
            foreach (var light in _extentions)
                light.Disable();

        }
        public override void SpendTime(float time)
        {
            base.SpendTime(time);
            foreach (LightExtension extension in _extentions)
            {
                extension.SpendTime(time);
            }
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(base.ToString() + "\n\tExtensions:");
            foreach (var light in _extentions)
                builder.Append("\n\t\t" + light.ToString());
            return builder.ToString();
        }
    }
}
