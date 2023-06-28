namespace Homework2
{
    internal class Simulator
    {Якщо є багато користувачів, то симулятор має керувати їх потребою в воді і моментом включення. Для цього слід створити відповідні методи...
        List<User> users = new List<User>();//заповнити список корстувачів методом або створити конструктор
        Pump waterTowerFiller = new Pump(1500);
        Pump waterTowerTaker = new Pump();
        public void Simulate(float source = 25000/*води у джерелі яке наповнює вежу*/)
        {
            WaterTower waterTower = new WaterTower(waterTowerFiller, waterTowerTaker);

            ///цикл, який виконуватиметься або доки в source є вода, або доки в usersPump є юзери
            ///якщо waterTowerFiller.IsOn рівне true 
            ///     waterTowerFiller.IsOn присвоюємо !StopTakeWater
            ///інакше
            ///     waterTowerFiller.IsOn присвоюємо StartTakeWater
            ///     
            /// 
            ///Виконуємо функцію waterTower.TakeWater 
            //Користувачі, які беруть воду по лінієчці хіба є в Китаї).
            ///для кожного користувача беремо воду
            ///тобто для кожного виконуємо :
            ///     якщо User.NeedWater()
            ///         User.IncreaseWater(waterTower.GiveWater())
            ///     інакше
            ///         видаляємо користувача
            ///
        }
    }
}
