namespace Homework9.Kitchen.Staff
{
    internal class Cook
    {
        private readonly string _name;
        public string Name { get { return _name; } }
        public CookProcess cookProcess { get; private set; }


        public void Cooking()
        {

        }




        public override string ToString()
        {
            return $"Name: {_name}. Is currently work: {cookProcess}";
        }
        public enum CookProcess
        {
            None,
            CakeCooking,
            PreparationIngridients,
            FillingFormation,
            Baking
        }
    }
}
