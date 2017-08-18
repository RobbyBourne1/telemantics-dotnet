using System;

namespace telemantics_dotnet
{
    public class VehicleInfo
    {
        public int VIN { get; set; }
        public double Odometer { get; set; }
        public double Consumption { get; set; }
        public double OdometerLastOilChange { get; set; } 
        public double EngineSizeL { get; set; }

        public VehicleInfo(int vin, double odometer, double consumption, double oilChange, double enengineSize)
        {
            VIN = vin;
            Odometer = odometer;
            Consumption = consumption;
            OdometerLastOilChange = oilChange;
            EngineSizeL = enengineSize;
        }

        public override string ToString()
        {
            return $"VIN:{VIN}, Odometer:{Odometer}, Consumption:{Consumption},Last Oil Change Odometer Reading:{OdometerLastOilChange}, Engine Size:{EngineSizeL}";
        }
    }
}