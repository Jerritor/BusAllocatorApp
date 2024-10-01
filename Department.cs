using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusAllocatorApp
{
    public class Department
    {
        //Vars reference
        Vars vars;

        #region Fields
        public string Name { get; set; }
        public bool IsDataFilled { get; set; }

        //[time set n: [route n: demand n, ... ], ...]
        //ie. time set 1:{route 1:demand 1, route 2: demand 2, etc.}, timeset 2:{route1,demand1...}, etc.
        public Dictionary<string, Dictionary<string, int?>> DemandData { get; set; }
        #endregion

        //CONSTRUCTORS
        public Department(string name, Vars v)
        {
            this.Name = name;
            IsDataFilled = false;
            InstantiateEmptyDemandData();
            this.vars = v;
        }

        public Department(string name, bool isDataFilled)
        {
            this.Name = name;
            this.IsDataFilled = isDataFilled;
            InstantiateEmptyDemandData();
        }


        private void InstantiateEmptyDemandData()
        {
            //Old way
            DemandData = new Dictionary<string, Dictionary<string, int?> >();
        }

        // In Department Class
        public void ClearDemandData()
        {
            DemandData.Clear();
            IsDataFilled = false;

            // Re-initialize DemandData with correct keys
            foreach (var timeSet in vars.timeSets)
            {
                string timeSetKey = $"{timeSet.Time.ToString(@"hh\:mm\:ss")}_{timeSet.IsOutgoing}";
                DemandData[timeSetKey] = new Dictionary<string, int?>();
                foreach (var route in vars.solo_routes)
                {
                    DemandData[timeSetKey][route] = null; //= 0// or any initial value
                }
            }
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
