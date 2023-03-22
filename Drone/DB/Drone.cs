using System;
using System.Collections.Generic;

namespace DronesLoad.DB
{
    public partial class Drone
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; } = null!;
        public int? ModelId { get; set; }
        public int? StateId { get; set; }
        public int? WeightLimit { get; set; }
        public decimal? BatteryCapacity { get; set; }

        public virtual DroneModel? Model { get; set; }
        public virtual DroneState? State { get; set; }
    }
}
