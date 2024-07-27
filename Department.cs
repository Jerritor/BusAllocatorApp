using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusAllocatorApp
{
    internal class Department
    {
        public string name { get; set; }
        public bool isDataFilled { get; set; }
        public Dictionary<string, Dictionary<string, int>> demandData { get; set; } //time set 1:{route 1:demand 1, route 2: demand 2, etc.}, timeset 2:{route1,demand1...}, etc. 

        Department(string name)
        {
            this.name = name;
            isDataFilled = false;
            InstantiateEmptyDemandData();
        }

        Department(string name, bool isDataFilled)
        {
            this.name = name;
            this.isDataFilled = isDataFilled;
            InstantiateEmptyDemandData();
        }

        private void InstantiateEmptyDemandData()
        {
            demandData = new Dictionary<string, Dictionary<string, int> >();
        }
    }
}
