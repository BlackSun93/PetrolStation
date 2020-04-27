using System.Collections.Generic;
using System.Timers;
using System.Text;
using System.IO;
using System.Linq;

namespace Assignment_2_PetrolStation
{
    class Data
    {
        private static Timer timer;
        public static List<Vehicle> vehicles;
        public static List<Pump> pumps;
        public static int showRemovedId;        //tracks the ID of vehicles that left without refueling
        public static int pumpRemember;         //variable that moves with the for loop counter to assign the vehicle.pumpnumber variable a value
                                                //allows the vehicle to remember what pump it was fueled by

        public static void Initialise() {
            InitialisePumps();
            InitialiseVehicles();
        }

        private static void InitialiseVehicles()
        {
            vehicles = new List<Vehicle>();   

                // https://msdn.microsoft.com/en-us/library/system.timers.timer(v=vs.71).aspx
                timer = new Timer();
                timer.Interval = Vehicle.RandomNumberGen(1500, 2200);
                timer.AutoReset = true; // keep repeating every 1.5 seconds
                timer.Elapsed += CreateVehicle; //every time 1500 elapses, make a new vehicle
                timer.Enabled = true;
                timer.Start();
            
        }

        private static void CreateVehicle(object sender, ElapsedEventArgs e)
        {

            if (vehicles.Count < 5) //if there are more than 5 vehicle waiting, when CreateVehicle is called, it will not generate  vehicle or add it to the list of vehicles waiting
                                    //in practice vehicles have such little patience that there is usually 2 vehicles in the queue at most
            {
                Vehicle v = new Vehicle();

                vehicles.Add(v);
                v.PatienceRunningOut(); //starts the vehicle's patience timer
            }
        }
            /// <summary>
            /// looks at the waiting vehicles and removes any that have 0 patience from having to wait
            /// </summary>
             public static void PatienceCheck()
             {
                foreach (Vehicle veh in vehicles)
                {
                    if (veh.patience <= 0)
                    {
                        int toRemove;
                        toRemove = vehicles.IndexOf(veh);
                        showRemovedId = veh.carID;          //some logic to transfer the ID of a vehicle to be removed elsewhere, was used so i could debug
                                                            //but I think its a nice feature
                        vehicles.RemoveAt(toRemove);
                        Vehicle.VehiclesLeftNoFuel++;
                    }
                }
             }
           /// <summary>
           /// initialises 9 pumps
           /// </summary>
        private static void InitialisePumps()
        {
            pumps = new List<Pump>();

            Pump p;

            for (int i = 0; i < 9; i++)
            {
                p = new Pump();
                pumps.Add(p);
            }
            
        }

        /// <summary>
        /// This fuction handles the logic behind which pump a vehicle can be asssigned to, assigning a vehicle to a pump is really taking the vehicle
        /// out of the vehicles list, making the pump its been assigned to busy for the amount of time it takes to fuel and recording the data when the vehicle
        /// leaves the pump
        /// </summary>
            public static void AssignVehicleToPump()
        { 
            Vehicle v;
            Pump p;
            bool blocked = true; //if this is true then there is a vehicle on a pump ahead of the currently checked pump, blocking it from having another vehicle park on it.

            if (vehicles.Count == 0) { return; }

            for (int i = 8; i >= 0; i--) //checks pumps starting from the pump at the last index in the pump list
            {
                pumpRemember = i;
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
               
                if (p.IsAvailable() && !blocked)
                {
                   v = vehicles[0]; // get first vehicle
                   vehicles.RemoveAt(0); // remove vehicles from queue
                   p.AssignVehicle(v); // assign it to the pump
                   v.assignedToPump = true;
                   v.pumpNumber = pumpRemember;
                }

                if (vehicles.Count == 0) { break; }

            }
        }


    }

}
