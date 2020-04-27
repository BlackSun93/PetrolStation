using System;
namespace Assignment_2_PetrolStation
{
    public class Display
    {

        /// <summary>
        /// uses the vehicles array in Data to look at queued vehicles, outputs its fuel type, vehicle type and ID
        /// </summary>
        public static void DrawVehicles()
		{
			Vehicle v;

			Console.WriteLine("Vehicles in Queue:");

			for (int i = 0; i < Data.vehicles.Count; i++)
			{
				v = Data.vehicles[i];
				Console.Write("#{0} Fuel Type: {1}      Vehicle Type: {2} | ", v.carID, v.fuelType, v.type);
			}
		}

        /// <summary>
        /// Draws the pumps to the screen, i wanted it to say the vehicle ID or type or something instead of busy
        /// </summary>
		public static void DrawPumps()
		{
			Pump p;

			Console.WriteLine("Pumps Status:");

			for (int i = 0; i < 9; i++)
			{
				p = Data.pumps[i];

				Console.Write("#{0} ", i + 1);
				if (p.IsAvailable()) { Console.Write("FREE"); }
				else { Console.Write("BUSY"); }
				Console.Write(" | ");
				if (i % 3 == 2) { Console.WriteLine(); }
             
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Total vehicles served: {0}", Pump.vehiclesServed);           //counter 4 reqs
            Console.WriteLine("Amount of vehicles have left without fueling: {0}       Its ID was: {1}", Vehicle.VehiclesLeftNoFuel, Data.showRemovedId); //counter 5 reqs
            Console.WriteLine("");
         
            Console.WriteLine("Pump 1 has dispensed : {0}           Pump 2 has dispensed: {1}           Pump 3 has dispensed: {2} ", Data.pumps[0].showFuel(), Data.pumps[1].showFuel(), Data.pumps[2].showFuel());
            Console.WriteLine("Pump 4 has dispensed : {0}           Pump 5 has dispensed: {1}           Pump 6 has dispensed: {2} ", Data.pumps[3].showFuel(), Data.pumps[4].showFuel(), Data.pumps[5].showFuel());
            Console.WriteLine("Pump 7 has dispensed : {0}           Pump 8 has dispensed: {1}           Pump 9 has dispensed: {2} ", Data.pumps[6].showFuel(), Data.pumps[7].showFuel(), Data.pumps[8].showFuel());
            Console.WriteLine("");

            Console.WriteLine("PETROL dispensed: {0}L       DIESEL dispensed: {1}L      LPG dispensed: {2}L", Pump.totalDisPet, Pump.totalDisDie, Pump.totalDisLPG);
            Console.WriteLine("Total fuel dispensed :  {0}", Pump.totalDispensedFuel);      //satifies counter 1 reqs
            Console.WriteLine("Total amout of money made: ${0}", Pump.moneyTaken);          //satisfies counter 2
            Console.WriteLine("Attendant's commission is: ${0}", (Pump.moneyTaken / 100) ); //counter 3 reqs
        }
    }
}
