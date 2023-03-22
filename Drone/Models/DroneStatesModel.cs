using System;
using System.Collections.Generic;

namespace DronesLoad.Models
{
    public partial class DroneStatesModel
    {
        public DroneStatesModel()
        {
            Drones = new HashSet<DronesModel>();
        }

        public int Id { get; set; }
        public string StateName { get; set; } = null!;

        public virtual ICollection<DronesModel> Drones { get; set; }
    }
}
