using System.Collections.Generic;
using System.Timers;
using System.Text;
using System.IO;
using System.Linq;
using System;

namespace Assignment_2_PetrolStation
{
    class Program
    {
        private static int refreshSpeed = 1400; //sets the refresh rate of the program to 1.4 seconds
        
        static void Main(string[] args)
        {
            //When the program first runs, creates a new output file to keep track of all the transactions, creates it with some headings to make it easier to read
            using (StreamWriter sw = new StreamWriter("PetrolOutput.txt", append: true))     
                //https://stackoverflow.com/questions/7306214/append-lines-to-a-file-using-a-streamwriter
            {
                sw.WriteLine("VehicleID    VehicleType    FuelType    FuelDispensed    Pump#");
            }

            //Data.vehGen = Vehicle.RandomNumberGen(1500, 2200);
            Data.Initialise();

            Timer timer = new Timer
            {
                Interval = refreshSpeed,
                AutoReset = true
            };
            timer.Elapsed += RunProgramLoop;
            timer.Enabled = true;
            timer.Start();

            Console.ReadLine();
        }

        static void RunProgramLoop(object sender, ElapsedEventArgs e)
        {
            Data.vehGen = Vehicle.RandomNumberGen(1500, 2200);      //gives vehGen a value
            if (Data.vehGenInProg == false)
            {
                Data.vehGenInProg = true;
                Data.CreateVehTimer();     //starts the timer for vehicle creation
            }
           
            Console.Clear();
            Display.DrawVehicles();   //draws vehicle list to the screen
            Console.WriteLine();
            Console.WriteLine();
            
            Data.AssignVehicleToPump();    //drives function to assign vehicles                     
            Display.DrawPumps();    //draws pump status as well as the counters
            Data.PatienceCheck();   //checks to see if any vehicles have 0 patience in the vehicle queue
        }
    }
}
