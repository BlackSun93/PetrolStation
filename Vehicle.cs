using System;
using System.Timers;

namespace Assignment_2_PetrolStation
{
    class Vehicle
    {
        private readonly string[] vehType = { "car", "van", "HGV" }; //array of potential vehicle types, readonly because no new types are added anywhere
        public string type;                                 //stores the vehicle's actual vehicle type
        private readonly string[] fuelTypes = { "DSL", "LPG", "PET" }; //array of potential fuel types
        public string FuelType { get; set; }   //stores the vehicle's actual fuel type, public because accessed in display
        public double FuelTime { get; set; }   // stores how long a vehicle will need to sit on a pump for, public because accessed in Pump
        public double fuelInTank; //used in constructor and is given a randomly generated amount between 0 - 25% of the max fuel size depending on the vehicle type
        public bool assignedToPump = false; //tracks if car is on a pump or not, used in patienceRunningOut function, a car's patience will tick down until it is assigned to a pump
        public int Patience { get; set; } // is randomly generated in the vehicle's constructor, used in patience running out and in the data class' patiencecheck.
        public int CarID { get; set; }  //the ID of a generated vehicle
        private int MaxFuel { get; set; } // the size of a vehicle's fuel tank, the most fuel it can have
        public int PumpNumber { get; set; } //tracks which pump the vehicle was serviced at. public as also used in Data and Pump

        public static Random number = new Random();
        public static int RandomNumberGen (int min, int max)   //https://stackoverflow.com/questions/767999/random-number-generator-only-generating-one-random-number
        {
            lock (number)
            {
                return (number.Next(min, max));               //random number generator, used to generate vehicles of different types, fueltypes and with random amounts of fuel
            }                                                  //already in the tank  
        }
		public static int nextCarID = 0;

        public static int VehiclesLeftNoFuel = 0;

        /// <summary>
        /// constructs a vehicle, gives it a vehicle type, a fuel type and a randomly generated amount of fuel in its tank based on that class of vehicle's maximum fuel amount
        /// </summary>
        public Vehicle()
        {
            Patience = RandomNumberGen(1000, 2000); // generates a number for the patiencerunningout timer to work with
 
            switch (RandomNumberGen(0, 3)) //when a vehicle is made, this piece of code will run and one of the 3 cases will be chosen (0, 1 or 2)
            {
                case 0:

                    MaxFuel = 40;
                    type = "Car";
                    FuelType = fuelTypes[RandomNumberGen(0, 3)]; //cars can run on any of the fuel types
                    break;

                case 1:
                    MaxFuel = 60;
                    type = "Van";
                    FuelType = fuelTypes[RandomNumberGen(0, 2)]; //vans can run on diesel and LPG
                    break;

                case 2:
                    MaxFuel = 150;
                    type = "HGV";
                    FuelType = fuelTypes[0]; //HGVs only run on diesel
                    break;

                default:           
                  
                    break;
            }
            //consider changing to a switch

            fuelInTank = RandomNumberGen(0, (MaxFuel / 4));                //vehicles cant be created with more than 1/4 of their fuel tank full. this sets the amount of fuel in  
                                                                           //the vehicle's tank as a random number from 0 (an empty fueltank) to 25% at most
            FuelTime = ((MaxFuel - fuelInTank) / Pump.fuelSpeed ) * 1000; //find out how much fuel is needed to fill up the tank, / by fuel speed to calculate seconds needed on the pump, 
                                                                         // * by 1000 to convert from seconds to milliseconds for the 
            CarID = nextCarID++;                                           // AssignVehicle timer

        }

        /// <summary>
        /// function that is driven by PateinceRunningOut tinmer function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SePatienceZero (object sender, ElapsedEventArgs e)
        {
            Patience = 0; // once patienceRunningOut timer runs out, patience is set to 0 for Data.PatienceCheck which removes 0 patience vehicles from the waiting vehicle list.
        }
      
        /// <summary>
        /// Timer function, uses vehicle's randomly generated patience value as a timer and if the timer elapses, sets their pateince to 0 for cleanup
        /// by data.patienceCheck
        /// </summary>
        public void PatienceRunningOut()
        {
            if (assignedToPump == false)
            {
                Timer timer = new Timer
                {
                    Interval = Patience,
                    AutoReset = false //has to be false, this was true and meant each of these timers repeated, stacking the ++ effect
                };
                timer.Elapsed += SePatienceZero; //sets patience to 0, used in data.PatienceCheck
                timer.Enabled = true;
                timer.Start();
            }
        }
    }  
}
