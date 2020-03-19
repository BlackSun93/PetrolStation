using System;
using System.Timers;

namespace Assignment_2_PetrolStation
{
    class Program
    {
        //
        static void Main(string[] args)
        {
            Data.Initialise();

            Timer timer = new Timer();
            timer.Interval = 150;
            timer.AutoReset = true; 
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

        }
    }
}
