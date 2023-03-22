using DronesLoad.DB;
using DronesLoad.Interfaces;
using DronesLoad.Models;
using Microsoft.AspNetCore.Mvc;

namespace DronesLoad.Controllers
{
    [ApiController]
	[Route("api/[controller]")]
	
	public class DispatchController : ControllerBase
    {
		//    private static readonly string[] Summaries = new[]
		//    {
		//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		//};

		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<DispatchController> _logger;

        public DispatchController(ILogger<DispatchController> logger, IUnitOfWork  unitOfWork)
        {
            _logger = logger;
			_unitOfWork=unitOfWork;
        }
		[HttpPost]
		public async Task<IActionResult> RegisterADrone( DronesModel drone)
		{
			//drone.DModel = droneModel;
			 _unitOfWork.Drones.Add(mapDroneModelToDB(drone));
			 _unitOfWork.Complete();

			return StatusCode(200);
		}

		private Drone mapDroneModelToDB(DronesModel drone)
		{
			throw new NotImplementedException();
		}

		[HttpGet]
		public async Task<IActionResult> GetDrones()
		{
			var drones =  _unitOfWork.Drones.GetAll();
			if (drones == null)// || drones.Count == 0)
			{
				return StatusCode(500, "No drones found.");
			}
			return Ok(drones);
		}
		[HttpGet]
		public async Task<IActionResult> GetDroneBatteryLevel(int id)
		{
			var drone =  _unitOfWork.Drones.GetByID(id);
			return Ok(drone.BatteryCapacity);
		}
	}
}