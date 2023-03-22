using DronesLoad.DB;
using DronesLoad.DB;
using DronesLoad.Interfaces;

namespace DronesLoad.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DronesDBContext _context;

		public IBaseRepository<DB.Drone> Drones { get; private set; }
		public IBaseRepository<DroneModel> DroneModels { get; private set; }
		public IBaseRepository<DroneState> DroneStates { get; private set; }
		public IBaseRepository<Medication> Medications { get; private set; }

		public UnitOfWork(DronesDBContext context)
		{
			_context = context;

			Drones = new BaseRepository<DB.Drone>(_context);
			DroneModels = new BaseRepository<DroneModel>(_context);
			DroneStates = new BaseRepository<DroneState>(_context);
			Medications = new BaseRepository<Medication>(_context);
		}


		public int Complete()
		{
			return _context.SaveChanges();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
