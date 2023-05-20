using static Homework9.Kitchen.Staff.Cook;

namespace Homework9.Kitchen.Recipes
{
    internal interface IRecipe
    {
        public Dictionary<CookProcess, float> cookTime { get; }
    }
}
