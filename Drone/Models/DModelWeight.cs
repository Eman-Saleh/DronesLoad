using AutoMapper;
using DronesLoad.DB;
using System;
using System.Collections.Generic;

namespace DronesLoad.Models
{
    public partial class DModelWeight
    {
        public DModelWeight()
        {
            Drones = new HashSet<DronesModel>();
        }

        public int Id { get; set; }
        public string ModelName { get; set; } = null!;

        public virtual ICollection<DronesModel> Drones { get; set; }
    }

}
