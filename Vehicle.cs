using System;
using System.Timers;

namespace Assignment_2_PetrolStation
{
    class Vehicle
    {
        public string[] vehType = { "car", "van", "HGV" }; //array of potential vehicle types
        public string type;                                 //stores the vehicle's actual vehicle type
        public string[] fuelTypes = { "DSL", "LPG", "PET" }; //array of potential fuel types
        public string fuelType;                              //stores the vehicle's actual fuel type
        public double fuelTime;                                 // stores how long a vehicle will need to sit on a pump for
        public double fuelInTank; //used in constructor and is given a randomly generated amount between 0 - 25% of the max fuel size depending on the vehicle type
        public bool assignedToPump = false; //tracks if car is on a pump or not, used in patienceRunningOut function, a car's patience will tick down until it is assigned to a pump
        public int patience; // is randomly generated in the vehicle's constructor, used in patience running out and in the data class' patiencecheck.
        public int carID;
        public int maxFuel; // the size of a vehicle's fuel tank, the most fuel it can have
        public int pumpNumber; //tracks which pump the vehicle was serviced at

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
        public static int idOfLeftVeh;

        /// <summary>
        /// constructs a vehicle, gives it a vehicle type, a fuel type and a randomly generated amount of fuel in its tank based on that class of vehicle's maximum fuel amount
        /// </summary>
        public Vehicle()
        {
            patience = RandomNumberGen(1000, 2000); // generates a number for the patiencerunningout timer to work with
           // int genType = RandomNumberGen(0, 3); // generates a number and compares that number to an array of vehicle types
          /*  if (vehType[genType] == vehType[0])
            {
                maxFuel = 40;
                type = "Car";
                fuelType = fuelTypes[RandomNumberGen(0,3)]; // fuel type can be any of the 3 types
            }
            else if (vehType[genType] == vehType[1])
            {
                maxFuel = 60;
                type = "Van";
                fuelType = fuelTypes[RandomNumberGen(0,2)]; // fuel type can only be diesel of lpg
            }
            else // if random number was a 2 then generates a HGV
            {
                maxFuel = 150;
                type = "HGV";
                fuelType = fuelTypes[0];
            } */
            switch (RandomNumberGen(0, 3)) //when a vehicle is made, this piece of code will run and one of the 3 cases will be chosen (0, 1 or 2)
            {
                case 0:

                    maxFuel = 40;
                    type = "Car";
                    fuelType = fuelTypes[RandomNumberGen(0, 3)]; //cars can run on any of the fuel types
                    break;

                case 1:
                    maxFuel = 60;
                    type = "Van";
                    fuelType = fuelTypes[RandomNumberGen(0, 2)]; //vans can run on diesel and LPG
                    break;

                case 2:
                    maxFuel = 150;
                    type = "HGV";
                    fuelType = fuelTypes[0]; //HGVs only run on diesel
                    break;

                default:            //need a default statement, could just break here i guess?? 
                    maxFuel = 4;
                    type = "Car";
                    fuelType = "PET";
                    break;
            }
            //consider changing to a switch

            fuelInTank = RandomNumberGen(0, (maxFuel / 4)); //vehicles cant be created with more than 1/4 of their fuel tank full. this sets the amount of fuel in  
                                                            //the vehicle's tank as a random number from 0 (an empty fueltank) to 25% at most
            fuelTime = ((maxFuel - fuelInTank) / Pump.fuelSpeed ) * 1000; //find out how much fuel is needed to fill up the tank, / by fuel speed to calculate seconds needed on the pump, 
                                                                         // * by 1000 to convert from seconds to milliseconds for the timer
            carID = nextCarID++;

        }

        /// <summary>
        /// function that is driven by PateinceRunningOut tinmer function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SePatienceZero (object sender, ElapsedEventArgs e)
        {
            patience = 0; // once patienceRunningOut timer runs out, patience is set to 0 for Data.PatienceCheck which removes 0 patience vehicles from the waiting vehicle list.
        }
      
        /// <summary>
        /// Timer function, uses vehicle's randomly generated patience value as a timer and if the timer elapses, sets their pateince to 0
        /// </summary>
        public void PatienceRunningOut()
        {
            if (assignedToPump == false)
            {
                Timer timer = new Timer();
                timer.Interval = patience; 
                timer.AutoReset = false; //has to be false, this was true and meant each of these timers repeated, stacking the ++ effect
                timer.Elapsed += SePatienceZero; //sets patience to 0, used in data.PatienceCheck
                timer.Enabled = true;
                timer.Start();
            }
        }
    }  
}
