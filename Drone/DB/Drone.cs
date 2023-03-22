using System;
using System.Collections.Generic;

namespace DronesLoad.DB
{
    public partial class Drone
    {
        public Drone()
        {
            Medications = new HashSet<Medication>();
        }

        public int Id { get; set; }
        public string SerialNumber { get; set; } = null!;
        public int? ModelId { get; set; }
        public int? StateId { get; set; }
        public double? WeightLimit { get; set; }
        public double? BatteryCapacity { get; set; }

        public virtual DroneModel? Model { get; set; }
        public virtual DroneState? State { get; set; }
        public virtual ICollection<Medication> Medications { get; set; }
    }
}
