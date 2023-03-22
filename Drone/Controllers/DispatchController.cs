using DronesLoad.DB;
using DronesLoad.Interfaces;
using DronesLoad.Models;
using Microsoft.AspNetCore.Mvc;

namespace DronesLoad.Controllers
{
    [ApiController]
	[Route("api/[controller]/[action]")]
	
	public class DispatchController : ControllerBase
    {

		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<DispatchController> _logger;

        public DispatchController(ILogger<DispatchController> logger, IUnitOfWork  unitOfWork)
        {
            _logger = logger;
			_unitOfWork=unitOfWork;
        }
		[HttpPost]
		public async Task<IActionResult> RegisterADrone([FromBody] DronesModel drone)
		{
			try
			{
				//drone.DModel = droneModel;
				_unitOfWork.Drones.Add(mapDroneModelToDB(drone));
				_unitOfWork.Complete();

				return StatusCode(200);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.InnerException.Message);
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error Register a drone");

			}
		}
		[HttpPost]
		public async Task<IActionResult> LoadMedications([FromBody] List<MedicationModel> medicationModels)
		{
			try { 
			if(medicationModels .Count==0)
					return BadRequest("Fake Load");


				var drone = _unitOfWork.Drones.FindAll(a=> a.Id == medicationModels[0].DroneId,  new[] { "Medications" }).ToList().FirstOrDefault();
				//var states = _unitOfWork.DroneStates.GetAll();
				if (drone == null)
			{
				return BadRequest("Drone not exist");
			}

			//check drone state as it  allowed to load if in Idle or Loading State
			if (drone.StateId != 1 && drone.StateId != 2 )  
			{
				return BadRequest($"Drone State not allowed to load  ");
			}

			//check drone battery if less than 25%
			if (drone.BatteryCapacity < 25)
			{
				return BadRequest($"Drone Can't be loaded. Battery percentage = {drone.BatteryCapacity}%");
			}

			var DroneCapacity = drone.Medications.Count > 0 ? drone.Medications.Sum(x => x.Weight) : 0;
			_logger.LogInformation($"Current Drone Loaded Weight is {DroneCapacity }gram");

			//calculate total weight of new medications 
			var medicationsWeight = medicationModels.Sum(x => x.Weight);

			//calculate free space on drone
			if(drone.WeightLimit < (double)( medicationsWeight + DroneCapacity))
			{
				return BadRequest("Drone Can't be loaded. Drone exceed limit");
			}
			var WeightLeft = drone.WeightLimit - (double)(medicationsWeight + DroneCapacity);

			//this sets the state of the drone to loaded once it reaches its capacity or loading if there's still space left
			if ((drone.StateId == 1|| drone.StateId ==2) && WeightLeft > 0)
				drone.StateId = 2;
			else if ((drone.StateId == 1 || drone.StateId == 2) && WeightLeft == 0)
				drone.StateId = 3;
			var medication = new List<Medication>();
			foreach (var item in medicationModels)
			{
				medication.Add(mapMedicationModelToDB(item));
			}
			 _unitOfWork.Medications.AddRange(medication);
				drone.StateId = 3; // finish loading(Loaded ) can be move 
			_unitOfWork.Drones.Update(drone);
			 _unitOfWork.Complete();

			return Ok(medicationModels);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.InnerException.Message);
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error Load Medications ");
			}
		}
		[HttpGet]
		// check loaded medication item for a given drone
		public async Task<IActionResult> checkMedicationOfADrone(int droneID)
		{
			try {
			var drones = _unitOfWork.Drones.FindAll(x => x.Id == droneID && x.StateId==3, new[] { "Medications" }).FirstOrDefault();
			if (drones == null)// || drones.Count == 0)
			{
				return StatusCode(500, "No drones found.");
			}
			else
				return Ok(drones.Medications);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.InnerException.Message);
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error while check loaded medication item for a given drone");
			}
		}
		[HttpGet]
		//checking available drones for loading;
		public async Task<IActionResult> checkAvailableDrone()
		{
			try {
				var drones = _unitOfWork.Drones.FindAll(x =>  x.StateId == 1 || x.StateId==2);
				if (drones == null)// || drones.Count == 0)
				{
					_logger.LogInformation("No Idle or Loading Drones");
					return StatusCode(500, "No available drones .");
				}
				else
					return Ok(drones);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.InnerException.Message);
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error check Available Dron");
			}
		}
		[HttpGet]
		//check drone battery level for a given drone
		public async Task<IActionResult> getDroneBatteryLevel(int droneId)
		{
			try {
			var _drone = _unitOfWork.Drones.GetByID(droneId);
			if (_drone == null)
			{
				return StatusCode(500, "Wrong drone ID .");
			}
			else
				return Ok(_drone.BatteryCapacity);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.InnerException.Message);
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error check Drone Battery Level");
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetDrones()
		{
			try { 
			var drones =  _unitOfWork.Drones.GetAll();
			if (drones == null )
			{
				return StatusCode(500, "No drones found.");
			}
			return Ok(drones);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.InnerException.Message);
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error Get All Drones");
			}
		}
		private Medication mapMedicationModelToDB(MedicationModel medicationModel)
		{
			Medication medication = new Medication();
			medication.Id = medicationModel.Id;
			medication.Code = medicationModel.Code;
			medication.Weight = medicationModel.Weight;
			medication.Image = medicationModel.Image;
			medication.Name = medicationModel.Name;
			medication.DroneId = medicationModel.DroneId;
			return medication;
		}
		private Drone mapDroneModelToDB(DronesModel droneModel)
		{
			Drone drone = new Drone();
			drone.Id = droneModel.Id;
			drone.ModelId = droneModel.ModelId;
			drone.StateId = droneModel.StateId;
			drone.SerialNumber = droneModel.SerialNumber;
			drone.BatteryCapacity = droneModel.BatteryCapacity;
			drone.WeightLimit = droneModel.WeightLimit;
			return drone;
		}
	}
}