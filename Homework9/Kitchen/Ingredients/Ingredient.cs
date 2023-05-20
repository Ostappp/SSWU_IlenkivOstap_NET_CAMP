using Homework9.Kitchen.Staff;

namespace Homework9.Kitchen.Ingredients
{
    internal abstract class Ingredient
    {
        string _name;
        Cook.CookProcess _requiredProccess;
        uint count;
        float timePer100;
        
    }
}
