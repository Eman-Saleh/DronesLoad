using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DronesLoad.Models
{
    public partial class DronesModel
    {
        public int Id { get; set; }
		[Required]
        
		[StringLength(maximumLength: 100, ErrorMessage = "Serial Number is required with length less than 100")]
		public string SerialNumber { get; set; } = null!;
        public int? ModelId { get; set; }
        public int? StateId { get; set; }
		[Required]
		[Range(0, 500)]
		public double? WeightLimit { get; set; }
		[Required]
		[Range(0,100.00)]
        public double? BatteryCapacity { get; set; }

        public virtual DModelWeight? DModel { get; set; }
        public virtual DroneStatesModel? State { get; set; }
    }
}
