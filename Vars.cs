using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

            io.LoadConfig();

            InstantiateVars();

            //Generate JSON Files -- comment out after testing
            GenerateJSONFiles();

            //Load vars from JSON
            FirstLoadRoutesTimeSetsDepartments();
        }

        //CONFIG INFO
        public string configFile = "config.cfg";
        public string ratesPath { get; set; }

        //--Empty object instantiation--
        //List of Routes
        public List<string>? solo_routes { get; set; }
        public List<Tuple<string, string>>? hybrid_routes { get; set; }

        //List of Departments
        public List<string>? deptNames { get; set; }

        //First Day and Second Day
        public DateTime? firstDay { get; set; }
        public DateTime? secondDay { get; set; }

        //List of Dictionaries of Time Sets
        public List<TimeSet>? timeSets { get; set; }
        //public List<Dictionary<string, object>>? timeSets { get; set; }

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

        //Departments List and Demands Memory
        public List<Department> departments { get; private set; }

        private void InstantiateVars()
        {
            //InstantiateSoloRoutes();
            //InstantiateHybridRoutes();
            //InstantiateDeptNames();
            //InstantiateDates();
            //InstantiateTimeSets();
            //InstantiateRates();
            //InstantiateBuffers();
            //InstantiateBusCapacities();
        }

        private void InstantiateSoloRoutes()
        {
            // Instantiate solo_routes list
            solo_routes = new List<string>
            {
                "ALABANG", "BINAN", "BALIBAGO", "CARMONA", "CABUYAO", "CALAMBA"
            };
            mainForm.WriteLine("Instantiated solo_routes.");
        }

        private void InstantiateHybridRoutes()
        {
            // Instantiate hybrid_routes list
            hybrid_routes = new List<Tuple<string, string>>
            {
                Tuple.Create("ALABANG", "CARMONA"),
                Tuple.Create("BINAN", "CARMONA"),
                Tuple.Create("CALAMBA", "CABUYAO")
            };
            mainForm.WriteLine("Instantiated hybrid_routes.");
        }

        private void InstantiateDeptNames()
        {
            deptNames = new List<string>
            {
                "3M", "ASTI", "IE", "ICTC", "JCM", "VISHAY", "OKURA", "NIDEC", "R&D / NPI", "TIP", "SUMITRONICS",
                "MERCHANT", "GLOBAL SKYWARE", "GLOBAL INVACOM", "ANALOG", "ENGG", "QA", "PCMC/SCM", "WAREHOUSE",
                "FACILITIES", "TQM", "IT", "INTERNAL AUDITOR", "FINANCE", "HR", "SECURITY", "TOPSEARCH", "CANTEEN",
                "OFFISTE", "ERTI", "CREOTEC", "ESPI", "Sales and Marketing"
            };
            mainForm.WriteLine("Instantiated departments.");
        }

        private void InstantiateDates()
        {
            firstDay = new DateTime(2024, 2, 26); // Monday, Feb 26
            secondDay = new DateTime(2024, 2, 27); // Tuesday, Feb 27
            mainForm.WriteLine("Instantiated firstDay & secondDay.");
        }

        private void InstantiateTimeSets()
        {
            /** OLD WAY
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
            };**/

            timeSets = new List<TimeSet>
            {
                new TimeSet(true, "4:00PM", true),
                new TimeSet(true, "6:00PM",true),
                new TimeSet(true, "7:00PM",true),
                new TimeSet(false, "7:00PM",true),
                new TimeSet(false, "10:00PM",true),
                new TimeSet(true, "4:00AM",false),
                new TimeSet(true, "7:00AM",false),
                new TimeSet(false, "7:00AM",false)
            };

            mainForm.WriteLine("Instantiated timeSets.");
        }

        private void InstantiateRates()
        {
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
        }

        private void InstantiateBuffers()
        {
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
        }

        private void InstantiateBusCapacities()
        {
            capacitySmallBus = 18;
            capacityLargeBus = 56;
            mainForm.WriteLine("Instantiated capacitySmallBus & capacityLargeBus.");
        }



        #region To_String Functions
        string StringListToString(List<string> s)
        {
            return String.Join(",", s);
        }

        string StringListOfTuplesToString(List<Tuple<string, string>>? tuplelist)
        {
            return String.Join(",", tuplelist.Select(pair => $"{pair.Item1}:{pair.Item2}"));
        }

        public void PrintAllTimeSets()
        {
            for (int i = 0; i < timeSets.Count; i++)
            {
                mainForm.WriteLine($"{i+1}: {timeSets[i]}");
            }

            /**
            foreach (var ts in timeSets)
            {
                mainForm.WriteLine(ts.ToString());
            }**/
        }

        public void PrintAllDepartments()
        {
            foreach (var department in departments)
            {
                mainForm.WriteLine(department.ToString());
            }
        }

        #endregion

        #region IO Functions
        public void CreateDefaultConfig() { io.CreateDefaultConfig(); }
        public void UploadRatesSheet() { io.UploadRatesSheet(); }

        public void GenerateJSONFiles() { io.GenerateJSONFiles(); }

        #endregion

        #region IO Loading JSON Files
        private void LoadRoutes()
        {
            (solo_routes, hybrid_routes) = io.LoadRoutes();

            //printing
            mainForm.WriteLine(StringListToString(solo_routes));
            mainForm.WriteLine(StringListOfTuplesToString(hybrid_routes));


        }

        private void LoadTimeSets()
        {
            timeSets = io.LoadTimeSets();

            PrintAllTimeSets();
            //mainForm.WriteLine(StringListToString(timeSets));
        }

        private void LoadDepartmentNames()
        {
            deptNames = io.LoadDeptNames();
        }

        private void LoadDepartments(bool isInitialLoad = false)
        {
            //initialize the departments
            departments = deptNames.Select(deptname => new Department(deptname)).ToList();

            //initialize the department demands
            UpdateDepartmentsWithRoutesAndTimeSets(isInitialLoad);

            //printing department names
            mainForm.WriteLine(StringListToString(deptNames));
            //printing departments object list
            PrintAllDepartments();
        }

        private void FirstLoadRoutesTimeSetsDepartments()
        {
            LoadRoutes();
            LoadTimeSets();
            LoadDepartmentNames();
            LoadDepartments(true); //this also calls LoadDepartmentNames()
        }
        #endregion

        #region IO Saving to JSON
        public void SaveDepartments()
        {
            io.SaveDepartmentNames(departments.Select(d => d.Name).ToList()); //Save Departments to JSON
        }
        #endregion

        #region Department List & Department Object Updating
        //Initialize the department demands
        private void InitializeDepartmentsDemands()
        {
            foreach (var department in departments)
            {
                InitializeDepartmentDemands(department);
            }
        }
        private void InitializeDepartmentDemands(Department department)
        {
            foreach (var timeSet in timeSets)
            {
                string timeSetKey = timeSet.Time.ToString();
                if (!department.DemandData.ContainsKey(timeSetKey))
                {
                    department.DemandData[timeSetKey] = new Dictionary<string, int>();
                }

                foreach (var route in solo_routes)
                {
                    if (!department.DemandData[timeSetKey].ContainsKey(route))
                    {
                        department.DemandData[timeSetKey][route] = -1; // Initialize with -1 demand
                    }
                }

                foreach (var hybridRoute in hybrid_routes)
                {
                    string hybridRouteKey = $"{hybridRoute.Item1}-{hybridRoute.Item2}";
                    if (!department.DemandData[timeSetKey].ContainsKey(hybridRouteKey))
                    {
                        department.DemandData[timeSetKey][hybridRouteKey] = -1; // Initialize with -1 demand
                    }
                }
            }
        }



        // Update demand for a specific time set and route
        public void UpdateDepartmentDemand(string departmentName, string timeSet, string route, int demand)
        {
            var department = departments.FirstOrDefault(d => d.Name == departmentName);
            if (department != null)
            {
                if (!department.DemandData.ContainsKey(timeSet))
                {
                    department.DemandData[timeSet] = new Dictionary<string, int>();
                }
                department.DemandData[timeSet][route] = demand;
                department.IsDataFilled = true;
            }
        }

        // Overload to update a full time set of demands
        public void UpdateDepartmentDemand(string departmentName, string timeSet, Dictionary<string, int> demands)
        {
            var department = departments.FirstOrDefault(d => d.Name == departmentName);
            if (department != null)
            {
                if (!department.DemandData.ContainsKey(timeSet))
                {
                    department.DemandData[timeSet] = new Dictionary<string, int>();
                }

                foreach (var demand in demands)
                {
                    department.DemandData[timeSet][demand.Key] = demand.Value;
                }

                department.IsDataFilled = true;
            }
        }

        // Overload to update all time sets of demands
        public void UpdateDepartmentDemand(string departmentName, Dictionary<string, Dictionary<string, int>> allDemands)
        {
            var department = departments.FirstOrDefault(d => d.Name == departmentName);
            if (department != null)
            {
                foreach (var timeSet in allDemands)
                {
                    if (!department.DemandData.ContainsKey(timeSet.Key))
                    {
                        department.DemandData[timeSet.Key] = new Dictionary<string, int>();
                    }

                    foreach (var demand in timeSet.Value)
                    {
                        department.DemandData[timeSet.Key][demand.Key] = demand.Value;
                    }
                }

                department.IsDataFilled = true;
            }
        }


        //Used to Update Departments list, specifically based on the routes and time sets lists
        //if initializeDemands is true, all demands will be set to -1
        private void UpdateDepartmentsWithRoutesAndTimeSets(bool initializeDemands = false)
        {
            foreach(var dept in departments)
            {
                UpdateDepartmentWithRoutesAndTimeSets(dept, initializeDemands);
            }
        }

        private void UpdateDepartmentWithRoutesAndTimeSets(Department department, bool initializeDemands)
        {
            var updatedDemandData = new Dictionary<string, Dictionary<string, int>>();

            //Iterate over TimeSets in order
            foreach (var timeSet in timeSets)
            {
                string timeSetKey = timeSet.Time.ToString();
                if (!updatedDemandData.ContainsKey(timeSetKey))
                {
                    updatedDemandData[timeSetKey] = new Dictionary<string, int>();
                }
                //Update demands for solo routes
                foreach (var route in solo_routes)
                {
                    if (!updatedDemandData[timeSetKey].ContainsKey(route))
                    {
                        int initialDemand = initializeDemands ? -1 : (department.DemandData.ContainsKey(timeSetKey) && department.DemandData[timeSetKey].ContainsKey(route) ? department.DemandData[timeSetKey][route] : 0);
                        updatedDemandData[timeSetKey][route] = initialDemand;
                    }
                }
                //Update demands for hybrid routes
                foreach (var hybridRoute in hybrid_routes)
                {
                    string hybridRouteKey = $"{hybridRoute.Item1}-{hybridRoute.Item2}";
                    if (!updatedDemandData[timeSetKey].ContainsKey(hybridRouteKey))
                    {
                        int initialDemand = initializeDemands ? -1 : (department.DemandData.ContainsKey(timeSetKey) && department.DemandData[timeSetKey].ContainsKey(hybridRouteKey) ? department.DemandData[timeSetKey][hybridRouteKey] : 0);
                        updatedDemandData[timeSetKey][hybridRouteKey] = initialDemand;
                    }
                }
            }
            department.DemandData = updatedDemandData;
        }

        #endregion

        #region Updating Non-Department Lists
        public void UpdateRoutes(List<string> newRoutes)
        {
            solo_routes = newRoutes;
            UpdateDepartmentsWithRoutesAndTimeSets(false);
        }

        public void UpdateHybridRoutes(List<Tuple<string, string>> newHybridRoutes)
        {
            hybrid_routes = newHybridRoutes;
            UpdateDepartmentsWithRoutesAndTimeSets(false);
        }

        public void UpdateTimeSets(List<TimeSet> newTimeSets)
        {
            timeSets = newTimeSets;
            UpdateDepartmentsWithRoutesAndTimeSets(false);
        }
        #endregion
    }
}
