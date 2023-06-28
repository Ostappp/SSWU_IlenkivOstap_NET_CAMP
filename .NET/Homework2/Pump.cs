namespace Homework2
{
    internal class Pump
    {
        private float power;
        private bool isOn;
        public bool IsOn { get => isOn; }
        public Pump(float power = 100)
        {
            this.power = power;
            isOn = true;
        }
        public void ChangeState() => isOn = !isOn;
        
        public float TransferWater(ref float sourceTake)
        {
            if(isOn)
            {
                ///забираємо з джерела воду
            }
            return 0;
        }

        public override string? ToString()
        {
            return $"Water volume, that pump can trnsfer per tick: {power}.";
        }
    }
}
