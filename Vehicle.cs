using System;
using System.Timers;

namespace Assignment_2_PetrolStation
{
    class Vehicle
    {
        public string fuelType;
        public double fuelTime;
        public bool assignedToPump = false; //tracks if car is on a pump or not
        public int patience;
        
        public static Random number = new Random();
        public int RandomNumberGen (int min, int max)
        {
            lock (number)
            {
                return (number.Next(min, max));
            }
        }
		public static int nextCarID = 0;
		public int carID;
        public static int VehiclesLeftNoFuel = 0;
        public static int idOfLeftVeh;
        public Vehicle(string ftp, double ftm)
        {
            fuelType = ftp;
            fuelTime = ftm;
			carID = nextCarID++;
            //patience = RandomNumberGen(5000, 15000); //vehicle will wait between 5 and 15 seconds to fuel
            patience = 15000;
        }

        public void PatienceMinus (object sender, ElapsedEventArgs e)
        {
            patience--;
        }
      
      
        public void PatienceRunningOut()
        {
            if (assignedToPump == false)
            {
                Timer timer = new Timer();
                timer.Interval = patience; //should set this timer to elapse every
                timer.AutoReset = false; //has to be false, this was true and meant each of these timers repeated, stacking the ++ effect
                timer.Elapsed += RemoveFromList;
                timer.Enabled = true;
                timer.Start();
            }
        }
        public void RemoveFromList(object sender, ElapsedEventArgs e)
        {
            Data.vehicles.RemoveAt(0);
            VehiclesLeftNoFuel++;
            idOfLeftVeh = carID;
        }
        
    }
}
