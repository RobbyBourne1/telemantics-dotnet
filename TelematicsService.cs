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

        public void DeSan(VehicleInfo vehicle)
        {
            string[] files = System.IO.Directory.GetFiles(".", "*.Json");
            ///LIST CREATION GOES HERE
            List<object> vehicleList = new List<object>();
            foreach (var item in files)
            {
                using (StreamReader file = File.OpenText(item))
                {
                    var vehicleInfoObj = JsonConvert.DeserializeObject<VehicleInfo>(file.ReadToEnd());
                    vehicleList.Add(vehicleInfoObj);
                }
            }
        }
    }
}