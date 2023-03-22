

using DronesLoad.Interfaces;
using DronesLoad.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesLoad.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepository<DB.Drone> Drones { get; }
		public IBaseRepository<DroneModel> DroneModels { get; }
		public IBaseRepository<DroneState> DroneStates { get; }
		int Complete();
    }
}
