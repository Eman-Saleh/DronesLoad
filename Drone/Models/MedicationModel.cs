using AutoMapper;
using DronesLoad.DB;
using System;
using System.Collections.Generic;

namespace DronesLoad.Models
{
    public partial class MedicationModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Weight { get; set; }
        public string? Code { get; set; }
        public string? Image { get; set; }
        public int? DroneId { get; set; }

        public virtual DronesModel? DroneModel { get; set; }
    }
	public class MedicationProfile : Profile
	{
		public MedicationProfile()
		{
			CreateMap<Medication, MedicationModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ForMember(dest => dest.Id, opt => opt.Ignore());
			CreateMap<MedicationModel, Medication>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
		}
	}
}
