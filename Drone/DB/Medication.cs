using System;
using System.Collections.Generic;

namespace DronesLoad.DB
{
    public partial class Medication
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Weight { get; set; }
        public string? Code { get; set; }
        public string? Image { get; set; }
        public int? DroneId { get; set; }

        public virtual Drone? Drone { get; set; }
    }
}
