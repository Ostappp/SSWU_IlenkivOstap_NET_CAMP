using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{
    internal class User
    {
        private readonly float _savedWaterMax;
        private float _savedWater;
        private float _neededWater;
        private readonly float _waterUsagePerTick;

        private float SavedWater
        {
            set
            {
                if (value >= 0)
                    if (value <= _savedWaterMax)
                        _savedWater = value;
                    else
                        _savedWater = _savedWaterMax;
            }
        }
        public User(float savedWaterMax = 5, float savedWater = 0, float neededWater = 15, float waterUsge = 2)
        {
            _savedWaterMax = savedWaterMax;
            _savedWater = savedWater;
            _neededWater = neededWater;
            _waterUsagePerTick = waterUsge;
        }
        public void IncreaseSource(float volume)
        {
            ///додаємо до значення _savedWater значення volume
            ///значення _savedWater встановлювати через SavedWater
        }
        public void Use()
        {
            ///якщо _neededWater не рівне нулю, 
            ///беремо воду з _sawedWater у розмірі _waterUsagePerTick або менше, якщо води у _savedWater не достатньо, або _neededWater менше за _waterUsagePerTick

        }
        public bool NeedWater() => _neededWater > 0;

        public override string? ToString()
        {
            return $"Need water volume: {_neededWater}.\tWater level in storage: {_savedWater}.\t" +
                $"Max water level in storage: {_savedWaterMax}.\t" +
                $"Water user can use per tick/second: {_waterUsagePerTick}.";
        }
    }
}
