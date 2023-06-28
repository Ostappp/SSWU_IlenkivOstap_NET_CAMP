namespace Homework7
{
    internal interface ITrafficLight
    {
        public State CurrentState { get; }
        private State SetTate { set => throw new NotImplementedException(); }
        public void SpendTime(float seconds);
        public void Enable();
        public void Disable();
        public enum State
        {
            Off,
            Red,
            RedYellow,
            Green,
            Yellow,

        }
    }
}
