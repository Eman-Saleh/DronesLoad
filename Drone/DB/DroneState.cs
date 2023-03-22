using System;
using System.Collections.Generic;

namespace DronesLoad.DB
{
    public partial class DroneState
    {
        public DroneState()
        {
            Drones = new HashSet<Drone>();
        }

        public int Id { get; set; }
        public string StateName { get; set; } = null!;

        public virtual ICollection<Drone> Drones { get; set; }
    }
}
