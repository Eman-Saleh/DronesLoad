using System;
using System.Collections.Generic;

namespace DronesLoad.DB
{
    public partial class DroneModel
    {
        public DroneModel()
        {
            Drones = new HashSet<Drone>();
        }

        public int Id { get; set; }
        public string ModelName { get; set; } = null!;

        public virtual ICollection<Drone> Drones { get; set; }
    }
}
