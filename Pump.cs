using System.Collections.Generic;
using System.Timers;
using System.Text;
using System.IO;
using System.Linq;
using System;

namespace Assignment_2_PetrolStation
{
    class Pump
    {
        public double dispensedFuel = 0; // keeps track of each dispension instance and how much fuel was dispensed in that one transaction
        public static double totalDispensedFuel = 0; // global variable to keep track of how much fuel all pumps have dispensed.
        public double totalPumpDisp = 0; // variable to keep track of each individual pump's dispensed fuel
        private readonly double fuelPrice = 1.20;
        public static int vehiclesServed = 0;
        public static double totalDisPet, totalDisDie, totalDisLPG; // variables to keep track of how much fuuel of each type have been dispensed
        public static double moneyTaken { get; set; }
        public Vehicle currentVehicle = null;
        public static double fuelSpeed = 1.5; //1.5L per second

        /// <summary>
        /// used in the data class to check if a pump can be assigned to. if currentVehicle is null then there isnt a vehicle on the pump, is referenced in display.cs
        /// so it has to be public
        /// </summary>
        /// <returns></returns>
        public bool IsAvailable()
        {
            // returns TRUE if currentVehicle is NULL, meaning available
            // returns FALSE if currentVehicle is NOT NULL, meaning busy
            return currentVehicle == null;
        }

        /// <summary>
        /// assigns a vehicle to a pump by assigning a value v to the pump's currentVehicle variable, waits for the vehicle's fueltime to elapse 
        /// before calling the releaseVehicle function. Is used in data.cs so it is public.
        /// </summary>
        /// <param name="v"></param> this is a vehicle object
        public void AssignVehicle(Vehicle v)
        {
            currentVehicle = v;

            Timer timer = new Timer
            {
                // timer.Interval = fuelSpeed;
                Interval = v.FuelTime,
                AutoReset = false // don't repeat
            };
            timer.Elapsed += ReleaseVehicle;
            timer.Enabled = true;
            timer.Start();
        }

        /// <summary>
        /// to get the pump amounts to show in the display class i had to have an accessable function to return the value of totalPumpDisp,
        /// which is a non-static amount of fuel the pump has dispensed
        /// </summary>
        /// <returns></returns>
        public double ShowFuel()
        {
            return totalPumpDisp;
        }
        
        /// <summary>
        /// writes the data from the vehicle object that was assigned to this pump to an output file., clears the vehicle object from the pump instance to allow another one to take its place
        /// </summary>
        /// <param name="sender"></param>
        /// this function is called from the timer in assignVehicle, hence the timer parameters
        /// <param name="e"></param>
        private void ReleaseVehicle(object sender, ElapsedEventArgs e)
        {
            AddFuelToTotal();
          
                using (StreamWriter sw = new StreamWriter("PetrolOutput.txt", append: true))
                {
 
                sw.WriteLine(string.Format("{0, -3}                {1, -3}         {2, -3}           {3, 3}           {4, 3}", Convert.ToString(currentVehicle.CarID), currentVehicle.type, currentVehicle.FuelType, dispensedFuel, ++currentVehicle.PumpNumber));
                    //writes the vehicle id, type, how much fuel it took and the pump it was asigned to (+1 or else pumps start at 0 instead of at 1)
                    //i could not find a way to get streamwriter to format the output in a such a way that each column always starts in the same place, it works for the first 3 columns but not for fuel and pump id
                }

            currentVehicle = null; //clears the pump
            vehiclesServed++;       
        }
       /// <summary>
       /// Calculates how much fuel was dispensed, updates global totalDispensedFuel variable and reports the pump's dispensed fuel for this transaction
       /// also calculates how much money has been taken since the application was opened.
       /// </summary>
        public void AddFuelToTotal()
        {
            dispensedFuel = ((currentVehicle.FuelTime * fuelSpeed) / 1000); //uses the vehicle object that was on the pump's time to fuel, * by the pump's fueling speed 
                                                                             //and adds it to the pump's record of litres dispensed, /1000 because fueltime is in milliseconds
                                                                             //so this brings it back to seconds.
            totalPumpDisp += dispensedFuel;     //adds the amount dispensed to the global variable that tracks all fuel dispensed across all pumps and all fueltypes

            switch (currentVehicle.FuelType)
            {
                case "PET":
                    totalDisPet += dispensedFuel;
                    break;

                case "DSL":
                    totalDisDie += dispensedFuel;
                    break;

                case "LPG":
                    totalDisLPG += dispensedFuel;
                    break;

                default:
                    break;

            }

           totalDispensedFuel = (totalDisLPG + totalDisDie + totalDisPet); //totals up all the global counters for the 3 fuel types, stores total amount of fuel dispensed
            moneyTaken = totalDispensedFuel * fuelPrice;
        }

    }
}
