using System;
using System.Timers;

namespace Assignment_2_PetrolStation
{
    class Pump
    {
        public double dispensedFuel = 0;
        public double fuelPrice = 1.20;

        public static int vehiclesServed = 0;
        public static double publicDF = 0;
        public static double totalDispensedFuel = 0;
        public static double moneyTaken;

        public Vehicle currentVehicle = null;
        public string fuelType;
        public double fuelSpeed = 1.5; //1.5L per second

        public Pump(string ftp)
        {
            fuelType = ftp;
        }

        public bool IsAvailable()
        {
            // returns TRUE if currentVehicle is NULL, meaning available
            // returns FALSE if currentVehicle is NOT NULL, meaning busy
            return currentVehicle == null;
        }

        public void AssignVehicle(Vehicle v)
        {
            currentVehicle = v;

            Timer timer = new Timer();
           // timer.Interval = fuelSpeed;
            timer.Interval = v.fuelTime;
            timer.AutoReset = false; // don't repeat
            timer.Elapsed += ReleaseVehicle;
            timer.Enabled = true;
            timer.Start();
        }
        /* public void FuelCounter()
        {
            Timer timer = new Timer();
            timer.Interval = 1050;
            timer.AutoReset = false; //has to be false, this was true and meant each of these timers repeated, stacking the ++ effect
            timer.Elapsed += TimedAddFuel;
            timer.Enabled = true;
            timer.Start();
            //totalDispensedFuel += dispensedFuel;
        }
        */

       
        public double showFuel()
        {
            return dispensedFuel;
        }
        

        public void ReleaseVehicle(object sender, ElapsedEventArgs e)
        {
            AddFuelToTotal();
           currentVehicle = null;
            // record transaction
            vehiclesServed++;
        }
       
        public void AddFuelToTotal()
        {
            dispensedFuel += ((currentVehicle.fuelTime * fuelSpeed) / 1000); //uses the vehicle object that was on the pump's time to fuel, * by the pump's fueling speed and adds it to the pump's record of litres dispensed, /1000 to get rid of excess 0s
            totalDispensedFuel += dispensedFuel;
            moneyTaken = totalDispensedFuel * fuelPrice;
        }

    }
}
