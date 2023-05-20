using Homework9.KitchenData.Staff;

namespace Homework9.MenuElements
{
    internal class Pizza : IOffer
    {
        private readonly string _name;
        public string Name { get { return _name; } }

        private TimeOnly _prepearingTime;
        public TimeOnly PrepearingTime { get => _prepearingTime; }

        private string _description;
        public string Description { get => _description; }
        public Menu.OfferType OrderType { get => Menu.OfferType.Pizza; }


        public PizzaSize Size { get; private set; }

        private List<IStaff> _workedOnDish;
        public List<IStaff> WorkedOnDish { get => _workedOnDish; }

        public void AddCookToDish(IStaff staff)
        {
            _workedOnDish.Add(staff);
        }

        //private Dictionary<PrepearingStage, float> _prepearingStagesTime;
        //public Dictionary<PrepearingStage, float> PrepearingStagesTime { get => new(_prepearingStagesTime); }

        public Pizza(string name, string description,  PizzaSize size/*, Dictionary<PrepearingStage, float> prepearingStagesTimeInMinutes*/)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name can not be empty!");

            //if (!prepearingStagesTimeInMinutes.Any())
            //    throw new ArgumentNullException("Ingredients measuring can not be empty!");
            //foreach (float time in prepearingStagesTimeInMinutes.Values)
            //{
            //    if (time <= 0)
            //        throw new ArgumentException("time can not be less than 0");
            //}

            _name = name;

            if (string.IsNullOrEmpty(description))
                _description = string.Empty;
            else
                _description = description;
            //float totalTime = prepearingStagesTimeInMinutes.Values.Sum();
            //_prepearingTime = new TimeOnly((int)totalTime / 60, (int)totalTime - ((int)totalTime / 60));            

            //_prepearingStagesTime = prepearingStagesTimeInMinutes.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            
            Size = size;
            _prepearingTime = new TimeOnly(0, 20);
            _workedOnDish = new List<IStaff> { };
        }
        public enum PrepearingStage
        {
            PizzaBase,
            Filling,
            Baking
        }
        public enum PizzaSize
        {
            Small,
            Medium,
            Large,
            ExtraLarge
        }

    }
}
