using System.Collections.Generic;
using System.Timers;

namespace Assignment_2_PetrolStation
{
    class Data
    {
        private static Timer timer;
        public static List<Vehicle> vehicles;
        public static List<Pump> pumps;

        public static void Initialise() {
            InitialisePumps();
            InitialiseVehicles();
        }

        private static void InitialiseVehicles()
        {
            vehicles = new List<Vehicle>();   //how to remove vehicles with 0 patience from vehicles list? they have IDs but that doesnt relate to their position in the queue

                // https://msdn.microsoft.com/en-us/library/system.timers.timer(v=vs.71).aspx
                timer = new Timer();
                timer.Interval = 1500;
                timer.AutoReset = true; // keep repeating every 1.5 seconds
                timer.Elapsed += CreateVehicle; //every time 1500 elapses, make a new vehicle
                timer.Enabled = true;
                timer.Start();
            
        }

        private static void CreateVehicle(object sender, ElapsedEventArgs e)
        {
            // queue limit
            // diesel 
            if (vehicles.Count < 1) //if there are more than 1 vehicle waiting, when CreateVehicle is called, it will not generate  vehicle or add it to the list of vehicles waiting
            {
                Vehicle v = new Vehicle("diesel", 10 * 1800); //10 * 1800 millisecond fuel timer = 18 seconds
               
                vehicles.Add(v);
                v.PatienceRunningOut(); //starts the vehicle's patience timer
            }
            
           
        }
        
    
    //either have a counter counting down, or have a IfNotAssigned() function that will -1 off a vehicle's patience, when patience hits 0, car is removed from vehicle list

        private static void InitialisePumps()
        {
            pumps = new List<Pump>();

            Pump p;

            for (int i = 0; i < 9; i++)
            {
                p = new Pump("diesel");
                pumps.Add(p);
            }
            
        }

       /* public static bool CheckIfBlocked()
        {
            bool blocked = true; //if this is true then there is a vehicle on a pump ahead of the currently checked pump, blocking it from having another vehicle park on it.

           // if (vehicles.Count == 0) { return; }

            for (int i = 8; i >= 0; i--) //checks pumps starting from the pump at the last index in the pump list
            {
                int j = i;
                p = pumps[i];
                // if current pump %3 == 2 (so pumps number 2, 5 and 8 - i.e the last pumps on each row)
                if (i % 3 == 2)
                {
                    if (pumps[i - 1].IsAvailable() && pumps[i - 2].IsAvailable()) //checks the two pumps before it dont have a vehicle on it
                    {
                        blocked = false;
                    }
                }
                else if (i % 3 == 1) //if middle pumps or pumps 1, 4 or 7
                {
                    if (pumps[i - 1].IsAvailable())
                    {
                        blocked = false;                 //no block check for pumps 0, 3 or 6 because there are no pumps before them
                    }
                }
                //MAYBE PUT CHECKISBLOCKED() AS ITS OWN METHOD?
            }
            return blocked;
        } */
        

            public static void AssignVehicleToPump()
        {
            Vehicle v;
            Pump p;
          bool blocked = true; //if this is true then there is a vehicle on a pump ahead of the currently checked pump, blocking it from having another vehicle park on it.

            if (vehicles.Count == 0) { return; }

            for (int i = 8; i >= 0; i--) //checks pumps starting from the pump at the last index in the pump list
            {
                int j = i;
                p = pumps[i];
                // if current pump %3 == 2 (so pumps number 2, 5 and 8 - i.e the last pumps on each row)
                if (i % 3 == 2)
                {
                    if (pumps[i-1].IsAvailable() && pumps[i-2].IsAvailable()) //checks the two pumps before it dont have a vehicle on it
                    {
                        blocked = false;
                    }
                }
                else if (i % 3 == 1) //if middle pumps or pumps 1, 4 or 7
                {
                    if (pumps[i-1].IsAvailable())
                    {
                        blocked = false;                 //no block check for pumps 0, 3 or 6 because there are no pumps before them
                    }
                }
                //MAYBE PUT CHECKISBLOCKED() AS ITS OWN METHOD?
               
              
                if (p.IsAvailable() && !blocked)
                {
                   v = vehicles[0]; // get first vehicle
                   vehicles.RemoveAt(0); // remove vehicles from queue
                   p.AssignVehicle(v); // assign it to the pump
                  // p.FuelCounter(); //track how much fuel is dispensed
                   v.assignedToPump = true;
                }

              //  if (vehicles[0].assignedToPump == false)
                {
                   // vehicles[0].PatienceRunningOut();
                }
              

                if (vehicles.Count == 0) { break; }

            }
        }
    }

}
