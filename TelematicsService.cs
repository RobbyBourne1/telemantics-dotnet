using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace telemantics_dotnet
{
    class TelematicService
    {
        JsonSerializer serializer = new JsonSerializer();
        public void Report(VehicleInfo vehicleInfo)
        {
            using (var writer = new StreamWriter(File.Open($"{vehicleInfo.VIN}.Json", FileMode.OpenOrCreate)))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(writer, vehicleInfo);
            }
        }
    }
}