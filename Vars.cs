using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusAllocatorApp
{
    public class Vars
    {
        private MainForm mainForm;
        IO io;

        //Constructor
        public Vars(MainForm form)
        {
            mainForm = form;
            io = new IO(this, mainForm);

            InstantiateVars();
            io.GenerateJSONFiles();
        }

        //--Empty object instantiation--
        //List of Routes
        public List<string>? solo_routes { get; set; }
        public List<Tuple<string, string>>? hybrid_routes { get; set; }

        //List of Departments
        public List<string>? departments { get; set; }

        //First Day and Second Day
        public DateTime? firstDay { get; set; }
        public DateTime? secondDay { get; set; }

        //List of Dictionaries of Time Sets
        public List<Dictionary<string, object>>? timeSets { get; set; }

        //Dictionary of Rates
        public Dictionary<string, double>? costSmallBus { get; set; }
        public Dictionary<string, double>? costLargeBus { get; set; }
        public Dictionary<(string, string), double>? costSmallHybridRoute { get; set; }
        public Dictionary<(string, string), double>? costLargeHybridRoute { get; set; }

        //Dictionary of buffer capacities
        public Dictionary<object, int>? bufferCurrentSmall { get; set; }
        public Dictionary<object, int>? bufferCurrentLarge { get; set; }

        //Bus Max Capacities
        public int capacitySmallBus { get; set; }
        public int capacityLargeBus { get; set; }

        private void InstantiateVars()
        {
            // Instantiate solo_routes list
            solo_routes = new List<string>
            {
                "ALABANG", "BINAN", "BALIBAGO", "CARMONA", "CABUYAO", "CALAMBA"
            };
            mainForm.WriteLine("Instantiated solo_routes.");

            // Instantiate hybrid_routes list
            hybrid_routes = new List<Tuple<string, string>>
            {
                Tuple.Create("ALABANG", "CARMONA"),
                Tuple.Create("BINAN", "CARMONA"),
                Tuple.Create("CALAMBA", "CABUYAO")
            };
            mainForm.WriteLine("Instantiated hybrid_routes.");

            departments = new List<string>
            {
                "3M", "ASTI", "IE", "ICTC", "JCM", "VISHAY", "OKURA", "NIDEC", "R&D / NPI", "TIP", "SUMITRONICS",
                "MERCHANT", "GLOBAL SKYWARE", "GLOBAL INVACOM", "ANALOG", "ENGG", "QA", "PCMC/SCM", "WAREHOUSE",
                "FACILITIES", "TQM", "IT", "INTERNAL AUDITOR", "FINANCE", "HR", "SECURITY", "TOPSEARCH", "CANTEEN",
                "OFFISTE", "ERTI", "CREOTEC", "ESPI", "Sales and Marketing"
            };
            mainForm.WriteLine("Instantiated departments.");

            firstDay = new DateTime(2024, 2, 26); // Monday, Feb 26
            secondDay = new DateTime(2024, 2, 27); // Tuesday, Feb 27
            mainForm.WriteLine("Instantiated firstDay & secondDay.");

            timeSets = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object> { { "id", 1 }, { "isOutgoing", true }, { "Time", "4:00PM" }, { "isFirstDay", true }, { "isOutModel", true } },
                new Dictionary<string, object> { { "id", 2 }, { "isOutgoing", true }, { "Time", "6:00PM" }, { "isFirstDay", true }, { "isOutModel", true } },
                new Dictionary<string, object> { { "id", 3 }, { "isOutgoing", true }, { "Time", "7:00PM" }, { "isFirstDay", true }, { "isOutModel", true } },
                new Dictionary<string, object> { { "id", 4 }, { "isOutgoing", false }, { "Time", "7:00PM" }, { "isFirstDay", true }, { "isOutModel", false } },
                new Dictionary<string, object> { { "id", 5 }, { "isOutgoing", false }, { "Time", "10:00PM" }, { "isFirstDay", true }, { "isOutModel", false } },
                new Dictionary<string, object> { { "id", 6 }, { "isOutgoing", true }, { "Time", "4:00AM" }, { "isFirstDay", false }, { "isOutModel", true } },
                new Dictionary<string, object> { { "id", 7 }, { "isOutgoing", true }, { "Time", "7:00AM" }, { "isFirstDay", false }, { "isOutModel", true } },
                new Dictionary<string, object> { { "id", 8 }, { "isOutgoing", false }, { "Time", "7:00AM" }, { "isFirstDay", false }, { "isOutModel", false } }
            };
            mainForm.WriteLine("Instantiated timeSets.");

            costSmallBus = new Dictionary<string, double>
            {
                { "ALABANG", 1423.5 },
                { "BINAN", 586.5 },
                { "BALIBAGO", 1038.0 },
                { "CABUYAO", 586.5 },
                { "CALAMBA", 1300.5 }
            };
            mainForm.WriteLine("Instantiated costSmallBus.");

            costLargeBus = new Dictionary<string, double>
            {
                { "ALABANG", 3072 },
                { "BINAN", 2460 },
                { "BALIBAGO", 1881 },
                { "CARMONA", 1881 },
                { "CABUYAO", 1935 },
                { "CALAMBA", 2127 }
            };
            mainForm.WriteLine("Instantiated costLargeBus.");

            costSmallHybridRoute = new Dictionary<(string, string), double>
            {
                { ("ALABANG", "CARMONA"), 1518.0 },
                { ("BINAN", "CARMONA"), 630.0 },
                { ("CALAMBA", "CABUYAO"), 630.0 }
            };
            mainForm.WriteLine("Instantiated costSmallHybridRoute.");

            costLargeHybridRoute = new Dictionary<(string, string), double>
            {
                { ("ALABANG", "CARMONA"), 3270 },
                { ("BINAN", "CARMONA"), 2550 },
                { ("CALAMBA", "CABUYAO"), 2625 }
            };
            mainForm.WriteLine("Instantiated costLargeHybridRoute.");

            bufferCurrentSmall = new Dictionary<object, int>
            {
                { "ALABANG", 0 },
                { "BINAN", 0 },
                { "CARMONA", 0 },
                { "BALIBAGO", 0 },
                { "CABUYAO", 0 },
                { "CALAMBA", 0 },
                { ("ALABANG", "CARMONA"), 0 },
                { ("BINAN", "CARMONA"), 0 },
                { ("CALAMBA", "CABUYAO"), 0 }
            };
            mainForm.WriteLine("Instantiated bufferCurrentSmall.");

            bufferCurrentLarge = new Dictionary<object, int>
            {
                { "ALABANG", 3 },
                { "BINAN", 3 },
                { "CARMONA", 3 },
                { "BALIBAGO", 3 },
                { "CABUYAO", 3 },
                { "CALAMBA", 3 },
                { ("ALABANG", "CARMONA"), 3 },
                { ("BINAN", "CARMONA"), 3 },
                { ("CALAMBA", "CABUYAO"), 3 }
            };
            mainForm.WriteLine("Instantiated bufferCurrentLarge.");

            capacitySmallBus = 18;
            capacityLargeBus = 56;
            mainForm.WriteLine("Instantiated capacitySmallBus & capacityLargeBus.");
        }

        /**
        void mainformWriteLine(string msg)
        {
            mainForm.WriteLine(msg);
        }
        */
    }
}
