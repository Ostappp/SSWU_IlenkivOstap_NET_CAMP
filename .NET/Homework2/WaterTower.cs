namespace Homework2
{
    internal class WaterTower
    {
        private readonly float _waterVolumeMax;
        private float _waterVolume;
        private Pump _tap;//видача води
        private Pump _pump;//прийняття води

        public WaterTower(Pump _tap, Pump _pump, float waterVolumeMax = 10000)
        {
            _waterVolumeMax = waterVolumeMax;
            this._tap = _tap;
            this._pump = _pump;
        }
        public bool StartTakeWater() => _waterVolume == 0;
        public bool StopTakeWater() => _waterVolume == _waterVolumeMax;
        public float TakeWater(ref float source)
        {
            ///_tap наповнє башню водою з source
            return 0;
        }
        public float GiveWater()
        {
            ///з _waterToAdd додаємо всю можливу воду до _waterVolume
            return 0;
        }

        public override string? ToString()
        {
            return $"Curent water level: {_waterVolume}.\t"+
                $"Max water volume: {_waterVolumeMax}.\t"+
                $"Pump that works for filling water source: {_pump}.\t"+
                $"Pump that works for take water from source: {_tap}.";
        }
    }
}
