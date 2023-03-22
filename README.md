# Drone App
has many API
**RegisterADrone** POST => https://localhost:7299/api/Dispatch/RegisterADrone
take Drone model in json 
{
  "id": 0,
  "serialNumber": "987456",
  "modelId": 1,
  "stateId": 2,
  "weightLimit": 350,
  "batteryCapacity": 95
}
**LoadMedications** POST=>https://localhost:7299/api/Dispatch/LoadMedications
take lit of medication in JSON
[
  {
    "id": 0,
    "name": "Panadol",
    "weight": 50,
    "code": "test",
    "image": "123456",
    "droneId": 9
  },
  {
    "id": 0,
    "name": "Panadol2",
    "weight": 50,
    "code": "test",
    "image": "123456",
    "droneId": 9
  }
]
and also Get APIs
checkMedicationOfADrone,checkAvailableDrone,getDroneBatteryLevel,GetAll Drones

the DB script file exit in DB Folder it will create DB and tables with default data
/*******************************************************************************************/
the project has Serilog save logs in the folder of project it also has docker windows enviroment 
/******************************************************************************************/
