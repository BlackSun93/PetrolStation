using System;
namespace Assignment_2_PetrolStation
{
    public class Display
    {
		public static void DrawVehicles()
		{
			Vehicle v;

			Console.WriteLine("Vehicles Queue:");

			for (int i = 0; i < Data.vehicles.Count; i++)
			{
				v = Data.vehicles[i];
				Console.Write("#{0} Fuel Type: {1} | ", v.carID, v.fuelType);
			}
		}

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

                // modulus -> remainder of a division operation
                // 0 % 3 => 0 (0 / 3 = 0 R=0)
                // 1 % 3 => 1 (1 / 3 = 0 R=1)
                // 2 % 3 => 2 (2 / 3 = 0 R=2)
                // 3 % 3 => 0 (3 / 3 = 1 R=0)
                // 4 % 3 => 1 (4 / 3 = 1 R=1)
                // 5 % 3 => 2 (5 / 3 = 1 R=2)
                // 6 % 3 => 0 (6 / 3 = 2 R=0)
                // ...
				if (i % 3 == 2) { Console.WriteLine(); }
             
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            //Data.pumps[8].FuelCounter();
            Console.WriteLine("Test counter for how many vehicles have left due to getting too impatient: {0}", Vehicle.VehiclesLeftNoFuel);
            Console.WriteLine("Pump 1 has dispensed : {0}           Pump 2 has dispensed: {1}           Pump 3 has dispensed: {2} ", Data.pumps[0].showFuel(), Data.pumps[1].showFuel(), Data.pumps[2].showFuel());
            Console.WriteLine("Pump 4 has dispensed : {0}           Pump 5 has dispensed: {1}           Pump 6 has dispensed: {2} ", Data.pumps[3].showFuel(), Data.pumps[4].showFuel(), Data.pumps[5].showFuel());
            Console.WriteLine("Pump 7 has dispensed : {0}           Pump 8 has dispensed: {1}           Pump 9 has dispensed: {2} ", Data.pumps[6].showFuel(), Data.pumps[7].showFuel(), Data.pumps[8].showFuel());
            Console.WriteLine("Total vehicles served: {0}", Pump.vehiclesServed);
            Console.WriteLine("Total amout of fuel dispensed: {0}", Pump.totalDispensedFuel);
            Console.WriteLine("Total amout of money made: {0}", Pump.moneyTaken);
            Console.WriteLine("Attendant's commission is: {0}", (Pump.moneyTaken / 100) );
            //Console.WriteLine("Total vehicles left without fuel: {0}", Pump.vehiclesLeft);

        }
    }
}
