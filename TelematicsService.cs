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
            using (var writer = new StreamWriter(File.Open($"{vehicleInfo.VIN}.json", FileMode.OpenOrCreate)))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(writer, vehicleInfo);
            }

        }

        public void GenerateHTML(VehicleInfo vehicle)
        {
            string[] files = System.IO.Directory.GetFiles(".", searchPattern: "*.json");
            ///LIST CREATION GOES HERE
            List<object> vehicleList = new List<object>();
            var totalOdometer = 0d;
            var totalConsumption = 0d;
            var totalLastOilChange = 0d;
            var totalEngineSize = 0d;

            var itemTemplate = @"<table align='center' border='1'>
          <tr>
              <th>VIN</th><th>Odometer (miles)</th><th>Consumption (gallons)</th><th>Last Oil Change</th><th>Engine Size (liters)</th>
          </tr>
          <tr>
              <td align='center'>{0}</td><td align='center'>{1}</td><td align='center'>{2}</td><td align='center'>{3}</td><td align='center'>{4}</td>
          </tr>
      </table>";

            var tableHTML = string.Empty;
            foreach (var item in files)
            {
                using (StreamReader file = File.OpenText(item))
                {
                    var vehicleInfoNew = JsonConvert.DeserializeObject<VehicleInfo>(file.ReadToEnd());
                    totalOdometer += vehicleInfoNew.Odometer;
                    totalConsumption += vehicleInfoNew.Consumption;
                    totalLastOilChange += vehicleInfoNew.OdometerLastOilChange;
                    totalEngineSize += vehicleInfoNew.EngineSizeL;
                    tableHTML += string.Format($"{itemTemplate}", vehicleInfoNew.VIN, vehicleInfoNew.Odometer, vehicleInfoNew.Consumption, vehicleInfoNew.OdometerLastOilChange, vehicleInfoNew.EngineSizeL);
                    vehicleList.Add(vehicleInfoNew);
                }
            }

            var odometerAverage = totalOdometer / vehicleList.Count;
            var consumptionAverage = totalConsumption / vehicleList.Count;
            var oilChangeAverage = totalLastOilChange / vehicleList.Count;
            var engineSizeAverage = totalEngineSize / vehicleList.Count;
            string html = $@"<html>
    <title>Vehicle Telematics Dashboard</title>
    <body>
      <h1 align='center'>Averages for # vehicles</h1>
      <table align='center'>
          <tr>
              <th>Odometer (miles) |</th><th>Consumption (gallons) |</th><th>Last Oil Change |</th><th>Engine Size (liters)</th>
          </tr>
          <tr>
              <td align='center'>{odometerAverage: 0.0}</td><td align='center'>{consumptionAverage}</td><td align='center'>{oilChangeAverage}</td align='center'><td align='center'>{engineSizeAverage}</td>
          </tr>
      </table>
      <h1 align='center'>History</h1>
      {tableHTML}
    </body>
  </html>";
            using (var writer = new StreamWriter(File.Open($"Dashboard.html", FileMode.OpenOrCreate)))
            {

                writer.WriteLine(html);
            }
        }
    }
}
