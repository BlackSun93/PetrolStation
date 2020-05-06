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
				else { Console.Write("BUSY"); }     //would ideally write the vehicle's ID and fuel type and potentially how many seconds its going to be on the pump for?
				Console.Write(" | ");               //but would have to be a static vehicle object, currently vehicle data is taken from the queue list, then moved to pump.currentvehicle 
				if (i % 3 == 2) { Console.WriteLine(); } //this cant be static because there are potentially 9 different pump.currentvehicles, so i would have to take the vehicle object
                                                //move it somewhere (probably to a list of successfully fuelled vehicles) then reference the pump ID it was asigned to with the pump
            }                               //index number and then read its data fields from there, gross.
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Total vehicles served: {0}", Pump.vehiclesServed);           //counter 4 reqs
            Console.WriteLine("Amount of vehicles have left without fueling: {0}       Its ID was: {1}", Vehicle.VehiclesLeftNoFuel, Data.showRemovedId); //counter 5 reqs
            Console.WriteLine("");
         
            Console.WriteLine("Pump 1 has dispensed : {0}           Pump 2 has dispensed: {1}           Pump 3 has dispensed: {2} ", Data.pumps[0].ShowFuel(), Data.pumps[1].ShowFuel(), Data.pumps[2].ShowFuel());
            Console.WriteLine("Pump 4 has dispensed : {0}           Pump 5 has dispensed: {1}           Pump 6 has dispensed: {2} ", Data.pumps[3].ShowFuel(), Data.pumps[4].ShowFuel(), Data.pumps[5].ShowFuel());
            Console.WriteLine("Pump 7 has dispensed : {0}           Pump 8 has dispensed: {1}           Pump 9 has dispensed: {2} ", Data.pumps[6].ShowFuel(), Data.pumps[7].ShowFuel(), Data.pumps[8].ShowFuel());
            Console.WriteLine("");

            Console.WriteLine("PETROL dispensed: {0}L       DIESEL dispensed: {1}L      LPG dispensed: {2}L", Pump.totalDisPet, Pump.totalDisDie, Pump.totalDisLPG);
            Console.WriteLine("Total fuel dispensed :  {0}", Pump.totalDispensedFuel);      //satifies counter 1 reqs
            Console.WriteLine("Total amout of money made: ${0}", Pump.moneyTaken);          //satisfies counter 2
            Console.WriteLine("Attendant's commission is: ${0}", (Pump.moneyTaken / 100) ); //counter 3 reqs

            
        }
    }
}
