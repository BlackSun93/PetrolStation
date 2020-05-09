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
            Console.Clear();
            Display.DrawVehicles();
            Console.WriteLine();
            Console.WriteLine();
            
            Data.AssignVehicleToPump();
            Display.DrawPumps();
            Data.PatienceCheck();
        }
    }
}
