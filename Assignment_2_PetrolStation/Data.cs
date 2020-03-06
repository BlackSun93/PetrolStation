﻿using System.Collections.Generic;
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
            vehicles = new List<Vehicle>();

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
            Vehicle v = new Vehicle("diesel", 10*1800); //10 * 1800 millisecond fuel timer = 18 seconds
            vehicles.Add(v);
        }
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
                        blocked = false;
                    }
                }
                //no block check for pumps 0, 3 or 6 because there are no pumps before them

                //MAYBE PUT CHECKISBLOCKED() AS ITS OWN METHOD?

                // note: needs more logic here, don't just assign to first
                // available pump, but check for the last available pump
                if (p.IsAvailable() && !blocked)
                {
                    v = vehicles[0]; // get first vehicle
                    vehicles.RemoveAt(0); // remove vehicles from queue
                    p.AssignVehicle(v); // assign it to the pump
                   // p.FuelCounter(p.fuelSpeed);
                }
                if (!p.IsAvailable())
                {
                  //  p.FuelCounter();
                }

                if (vehicles.Count == 0) { break; }

            }
        }
    }
}
