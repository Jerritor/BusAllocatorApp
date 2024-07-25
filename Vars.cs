using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusAllocatorApp
{
    public class Vars
    {
        //Empty object instantiation

        //List of Routes
        public static List<string>? solo_routes;
        public static List<Tuple<string, string>>? hybrid_routes;

        //List of Departments
        public static List<string>? departments;

        //First Day and Second Day
        public static DateTime? firstDay;
        public static DateTime? secondDay;

        //List of Dictionaries of Time Sets
        public static List<Dictionary<string, object>>? timeSets;

        //Dictionary of Rates
        public static Dictionary<string, double>? costSmallBus;
        public static Dictionary<string, double>? costLargeBus;
        public static Dictionary<(string, string), double>? costSmallHybridRoute;
        public static Dictionary<(string, string), double>? costLargeHybridRoute;

        //Dictionary of buffer capacities
        public static Dictionary<object, int>? bufferCurrentSmall;
        public static Dictionary<object, int>? bufferCurrentLarge;

        //Bus Max Capacities
        public static int capacitySmallBus;
        public static int capacityLargeBus;
    }
}
