namespace Homework8.LightAbstraction
{
    public abstract class TrafficLights : ITrafficLight
    {
        protected string _name;
        public string Name { get => _name; }
        protected ITrafficLight.State _state;
        public ITrafficLight.State CurrentState { get => _state; }

        protected List<ITrafficLight.Dir> _direction;
        public List<ITrafficLight.Dir> Directions { get => new List<ITrafficLight.Dir>(_direction); }

        public delegate void TrafficLightsDelegate();
        public event TrafficLightsDelegate ChangingState;

        protected readonly Dictionary<ITrafficLight.State, float> _timeForState;
        protected float _timeToChangeState;


        protected TrafficLights(string name, Dictionary<ITrafficLight.State, float> timeForState, IEnumerable<ITrafficLight.Dir>? lightDirs = null)
        {
            if (name == string.Empty)
                throw new ArgumentException("Input thaffic light's name.");
            if (timeForState.Count < 1)
                throw new ArgumentException("Traffic light must contain lights.");

            if (lightDirs == null || lightDirs.Contains(ITrafficLight.Dir.None))
                _direction = new List<ITrafficLight.Dir> { ITrafficLight.Dir.None };
            else
                _direction = new List<ITrafficLight.Dir>(lightDirs);


            _name = new(name);
            _timeForState = new Dictionary<ITrafficLight.State, float>(timeForState);

            _state = ITrafficLight.State.Off;
        }
        protected TrafficLights(string name, TrafficLights original, IEnumerable<ITrafficLight.Dir>? lightDirs = null)
        {
            if (name == string.Empty)
                throw new ArgumentException("Input thaffic light's name.");
            if (lightDirs == null)
            {
                if (original._direction.Contains(ITrafficLight.Dir.None))
                    _direction = new List<ITrafficLight.Dir> { ITrafficLight.Dir.None };
                else
                    _direction = new List<ITrafficLight.Dir>(original._direction);
            }
            else
            {
                if (lightDirs.Contains(ITrafficLight.Dir.None))
                    _direction = new List<ITrafficLight.Dir> { ITrafficLight.Dir.None };
                else
                    _direction = new List<ITrafficLight.Dir>(lightDirs);
            }


            _name = new(name);
            _timeForState = new Dictionary<ITrafficLight.State, float>(original._timeForState);

            _state = ITrafficLight.State.Off;
        }
        public virtual void Enable()
        {
            if (_state == ITrafficLight.State.Off)
            {
                _state = _timeForState.ElementAt(0).Key;
                _timeToChangeState = _timeForState[_state];
            }

        }
        public virtual void Disable() => _state = ITrafficLight.State.Off;
        private void DecreaseTimer(float secondfs)
        {
            _timeToChangeState -= secondfs;
            if (_timeToChangeState <= 0)
                ChangeState();
        }
        public virtual void SpendTime(float seconds)
        {
            if (_state != ITrafficLight.State.Off && seconds > 0)
                DecreaseTimer(seconds);
        }
        private void ChangeState()
        {
            int currIndex = ((_timeForState.Keys.ToList().IndexOf(_state) - 1) % _timeForState.Count + _timeForState.Count) % _timeForState.Count;
            while (_timeToChangeState <= 0)
            {
                currIndex = (currIndex + 1) % _timeForState.Count;
                _state = _timeForState.ElementAt((currIndex + 1) % _timeForState.Count).Key;
                _timeToChangeState += _timeForState[_state];
            }

            ChangingState?.Invoke();

        }
        protected internal void StateSetter(ITrafficLight.State state)
        {
            if (_timeForState.ContainsKey(state))
            {
                _state = state;

                _timeToChangeState = _timeForState[_state];
            }
        }

        public override string? ToString()
        {
            return $"Name: {_name}; type: {GetType().Name}; directions: {string.Join(", ", _direction)}; state: {_state}";
        }

    }
}
