namespace Homework8.Task1.LightAbstraction
{
    public interface ITrafficLight
    {
        public State CurrentState { get; }
        public List<Dir> Directions { get; }

        public enum State
        {
            Off,
            Red,
            RedYellow,
            Green,
            Yellow,
            None

        }
        public enum Dir
        {
            None,
            Forward,
            Left,
            Right,
        }
    }
}
