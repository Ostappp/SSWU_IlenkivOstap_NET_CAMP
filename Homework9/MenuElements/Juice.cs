using Homework9.KitchenData.Staff;

namespace Homework9.MenuElements
{
    internal class Juice : IOffer
    {
        private readonly string _name;
        public string Name { get { return _name; } }

        private TimeOnly _prepearingTime;
        public TimeOnly PrepearingTime { get => _prepearingTime; }

        private string _description;
        public string Description { get => _description; }
        public Menu.OfferType OrderType { get => Menu.OfferType.Drink; }
        private List<IStaff> _workedOnDish;
        public List<IStaff> WorkedOnDish { get => _workedOnDish; }

        public void AddCookToDish(IStaff staff)
        {
            _workedOnDish.Add(staff);
        }
        public Juice(string name, TimeOnly preparationTime, string description)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name can not be empty!");
            _workedOnDish = new List<IStaff> { };

            _name = name;

            if (string.IsNullOrEmpty(description))
                _description = string.Empty;
            else
                _description = description;

            _prepearingTime = new TimeOnly(preparationTime.Hour, preparationTime.Minute, preparationTime.Second);

        }

    }
}
