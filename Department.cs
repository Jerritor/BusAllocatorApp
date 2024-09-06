using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusAllocatorApp
{
    public class Department
    {
        public string Name { get; set; }
        public bool IsDataFilled { get; set; }

        //[time set n: [route n: demand n, ... ], ...]
        //ie. time set 1:{route 1:demand 1, route 2: demand 2, etc.}, timeset 2:{route1,demand1...}, etc.
        public Dictionary<string, Dictionary<string, int?>> DemandData { get; set; }  

        public Department(string name)
        {
            this.Name = name;
            IsDataFilled = false;
            InstantiateEmptyDemandData();
        }

        public Department(string name, bool isDataFilled)
        {
            this.Name = name;
            this.IsDataFilled = isDataFilled;
            InstantiateEmptyDemandData();
        }

        private void InstantiateEmptyDemandData()
        {
            DemandData = new Dictionary<string, Dictionary<string, int?> >();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Department Name: {Name}");
            sb.AppendLine($"Is Data Filled: {IsDataFilled}");
            sb.AppendLine("Demand Data:");

            foreach (var timeSet in DemandData)
            {
                sb.AppendLine($"  Time Set: {timeSet.Key}");
                foreach (var route in timeSet.Value)
                {
                    sb.AppendLine($"    Route: {route.Key}, Demand: {route.Value?.ToString() ?? "N/A"}");
                }
            }

            return sb.ToString();
        }
    }
}
