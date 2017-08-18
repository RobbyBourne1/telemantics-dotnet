using System;

namespace telemantics_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What is the VIN?:");
            var vinInput = int.Parse(Console.ReadLine());

            Console.WriteLine("What is the Odometer?:");
            var odometerInput = double.Parse(Console.ReadLine());

            Console.WriteLine("What is the consumption rate?:");
            var consumptionInput = double.Parse(Console.ReadLine());

            Console.WriteLine("What was the odometer last time oil was changed?:");
            var odometerLastOilChangeInput = double.Parse(Console.ReadLine());

            Console.WriteLine("What is the engine size?:");
            var engineSizeInput = double.Parse(Console.ReadLine());

            var newVehicle = new VehicleInfo(vinInput,odometerInput,consumptionInput,odometerLastOilChangeInput,engineSizeInput);
            Console.WriteLine($"{newVehicle}");

            new TelematicService().Report(newVehicle);

        }
    }
}
