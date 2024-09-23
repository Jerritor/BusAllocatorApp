using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using ExcelDataReader;
using System.Data;
using System.Diagnostics;


namespace BusAllocatorApp
{
    public class Vars
    {
        private MainForm mainForm;
        public IO io;

        //Constructor
        public Vars(MainForm form)
        {
            mainForm = form;
            io = new IO(this, mainForm);

            io.LoadConfig();

            //Load vars from JSON
            //FirstLoadRoutesTimeSetsDepartments();
        }

        //CONFIG INFO
        public string configFile = "config.cfg";
        public string ratesPath { get; set; }

        public string totalDemandFilePath { get; set; }


        //--Empty object instantiation--
        //List of Routes
        public List<string>? solo_routes { get; set; }
        public List<Tuple<string, string>>? hybrid_routes { get; set; }

        //List of Department Names
        public List<string>? deptNames { get; set; }

        //First Day and Second Day
        public DateTime? firstDay { get; set; }
        public DateTime? secondDay { get; set; }

        //List of of Time Sets
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
        //this one is used if the setting is individual departments and demands
        public List<Department>? deptsAndDemands { get; private set; }
        //this one is used if the setting is total demands
        public Department? totalDemands { get; private set; }


        //Demands Completion Flags - 1 if the demands are ready for generating spreadsheets.
        public enum CompletionState
        {
            Uninitialized,
            Incompleted, //initialized but not filled
            Completed //filled
        }
        //tracks deptsAndDemands state
        //is initially on by default as default mode is individual dept demand mode
        CompletionState IsDeptsAndDemandsCompleted { get; set; } = CompletionState.Incompleted;
        //tracks totalDemands state
        //is initially off by default as default mode is not all dept demand mode
        CompletionState IsTotalDemandsCompleted { get; set; } = CompletionState.Uninitialized;

        #region Variable Instantiation
        public void InstantiateVars()
        {
            InstantiateSoloRoutes();
            InstantiateHybridRoutes();
            InstantiateDeptNames();
            InstantiateDates();
            InstantiateTimeSets();
            InstantiateRates();
            InstantiateBuffers();
            InstantiateBusCapacities();
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
        #endregion

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
                mainForm.WriteLine($"{i + 1}: {timeSets[i]}");
            }

            /**
            foreach (var ts in timeSets)
            {
                mainForm.WriteLine(ts.ToString());
            }**/
        }

        public void PrintAllDepartments()
        {
            foreach (var department in deptsAndDemands)
            {
                mainForm.WriteLine(department.ToString());
            }
        }

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

        public void LoadDepartments(bool isInitialLoad = false)
        {
            LoadDepartmentNames();

            //initialize the departments
            deptsAndDemands = deptNames.Select(deptname => new Department(deptname)).ToList();

            //initialize the department demands
            UpdateDepartmentsWithRoutesAndTimeSets(isInitialLoad);

            //printing department names
            mainForm.WriteLine(StringListToString(deptNames));
            //printing departments object list
            PrintAllDepartments();
        }

        public void LoadJSONFiles()
        {
            LoadRoutes();
            LoadTimeSets();
            LoadDepartments(true); //this also calls LoadDepartmentNames(), true means set all demand values to null
        }
        #endregion

        #region IO Saving to JSON
        public void SaveDepartments()
        {
            io.SaveDepartmentNames(deptsAndDemands.Select(d => d.Name).ToList()); //Save Departments to JSON
        }
        #endregion

        #region Department List & Department Object Updating
        //Initialize the department demands
        /**
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
        **/

        // Update demand for a specific time set and route
        public void UpdateDepartmentDemand(string departmentName, string timeSet, string route, int demand)
        {
            var department = deptsAndDemands.FirstOrDefault(d => d.Name == departmentName);
            if (department != null)
            {
                if (!department.DemandData.ContainsKey(timeSet))
                {
                    department.DemandData[timeSet] = new Dictionary<string, int?>();
                }
                department.DemandData[timeSet][route] = demand;
                department.IsDataFilled = true;
            }
        }

        // Overload to update a full time set of demands
        public void UpdateDepartmentDemand(string departmentName, string timeSet, Dictionary<string, int> demands)
        {
            var department = deptsAndDemands.FirstOrDefault(d => d.Name == departmentName);
            if (department != null)
            {
                if (!department.DemandData.ContainsKey(timeSet))
                {
                    department.DemandData[timeSet] = new Dictionary<string, int?>();
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
            var department = deptsAndDemands.FirstOrDefault(d => d.Name == departmentName);
            if (department != null)
            {
                foreach (var timeSet in allDemands)
                {
                    if (!department.DemandData.ContainsKey(timeSet.Key))
                    {
                        department.DemandData[timeSet.Key] = new Dictionary<string, int?>();
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
            foreach (var dept in deptsAndDemands)
            {
                UpdateDepartmentWithRoutesAndTimeSets(dept, initializeDemands);
            }
        }

        private void UpdateDepartmentWithRoutesAndTimeSets(Department department, bool initializeDemands)
        {
            var updatedDemandData = new Dictionary<string, Dictionary<string, int?>>();

            //Iterate over TimeSets in order
            foreach (var timeSet in timeSets)
            {
                string timeSetKey = timeSet.Time.ToString();
                if (!updatedDemandData.ContainsKey(timeSetKey))
                {
                    updatedDemandData[timeSetKey] = new Dictionary<string, int?>();
                }
                //Update demands for solo routes
                foreach (var route in solo_routes)
                {
                    if (!updatedDemandData[timeSetKey].ContainsKey(route))
                    {
                        int? initialDemand = initializeDemands ? null : (department.DemandData.ContainsKey(timeSetKey) && department.DemandData[timeSetKey].ContainsKey(route) ? department.DemandData[timeSetKey][route] : 0);
                        updatedDemandData[timeSetKey][route] = initialDemand;
                    }
                }
                /** Removed because hybrid routes shouldnt be per department
                //Update demands for hybrid routes
                foreach (var hybridRoute in hybrid_routes)
                {
                    string hybridRouteKey = $"{hybridRoute.Item1}-{hybridRoute.Item2}";
                    if (!updatedDemandData[timeSetKey].ContainsKey(hybridRouteKey))
                    {
                        int? initialDemand = initializeDemands ? null : (department.DemandData.ContainsKey(timeSetKey) && department.DemandData[timeSetKey].ContainsKey(hybridRouteKey) ? department.DemandData[timeSetKey][hybridRouteKey] : 0);
                        updatedDemandData[timeSetKey][hybridRouteKey] = initialDemand;
                    }
                }
                **/
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

        #region Total Department Demand Spreadsheet Processing
        public void ProcessTotalDemandSpreadsheet()
        {
            try
            {
                if (!File.Exists(totalDemandFilePath))
                {
                    MessageBox.Show($"The file '{totalDemandFilePath}' does not exist.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Register the code page provider for ExcelDataReader
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                using (var stream = File.Open(totalDemandFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();

                        // Ensure there's exactly one sheet in the Excel file
                        if (result.Tables.Count != 1)
                        {
                            throw new Exception("The spreadsheet can only have one sheet.");
                        }

                        var table = result.Tables[0];
                        string sheetName = table.TableName;

                        // Find the 'TOTAL' column index (assuming header is at the 7th row, index 6)
                        int totalColumnIndex = -1;
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            var columnName = table.Rows[6][i]?.ToString().Trim();
                            if (columnName != null && columnName.Equals("TOTAL", StringComparison.OrdinalIgnoreCase))
                            {
                                totalColumnIndex = i;
                                break;
                            }
                        }

                        if (totalColumnIndex == -1)
                        {
                            throw new Exception($"No 'TOTAL' column found in the sheet '{sheetName}'.");
                        }

                        // Define the shifts and their corresponding starting row numbers (1-based indexing)
                        // Adjust these row numbers based on your actual spreadsheet structure
                        Dictionary<string, int> shiftStartRows = new Dictionary<string, int>
                        {
                            { "OUT 4:00AM", 9 },    // Excel row 10
                            { "IN 7:00AM", 19 },    // Excel row 20
                            { "OUT 7:00AM", 29 },   // Excel row 30
                            { "OUT 4:00PM", 39 },   // Excel row 40
                            { "OUT 6:00PM", 49 },   // Excel row 50
                            { "IN 7:00PM", 59 },    // Excel row 60
                            { "OUT 7:00PM", 69 },   // Excel row 70
                            { "IN 10:00PM", 79 }    // Excel row 80
                        };

                        // Map TimeSet shifts to the shift names in the Excel file
                        Dictionary<string, string> timeSetShiftMap = new Dictionary<string, string>();
                        foreach (var timeSet in timeSets)
                        {
                            string shiftName = timeSet.GetFormattedTimeINOUT(); // e.g., "OUT 4:00AM"
                            if (shiftStartRows.ContainsKey(shiftName))
                            {
                                timeSetShiftMap[shiftName] = shiftName;
                            }
                            else
                            {
                                // Handle exact match failures by removing ":00" if necessary
                                string shiftKey = shiftName.Replace(":00", "");
                                if (shiftStartRows.ContainsKey(shiftKey))
                                {
                                    timeSetShiftMap[shiftName] = shiftKey;
                                }
                                else
                                {
                                    // Shift not found, notify the user and skip
                                    MessageBox.Show($"Shift '{shiftName}' not found in the predefined shifts.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    continue;
                                }
                            }
                        }

                        // Instantiate the Department object with Name set to "Total"
                        totalDemands = new Department("Total");

                        bool hasErrors = false; // Flag to track actual data reading errors

                        // Iterate through each TimeSet that matches shifts in the Excel file
                        foreach (var timeSet in timeSets)
                        {
                            string shiftName = timeSet.GetFormattedTimeINOUT(); // e.g., "IN 7:00AM"

                            if (!timeSetShiftMap.TryGetValue(shiftName, out string excelShiftName))
                            {
                                // Shift not found, skip processing
                                continue;
                            }

                            int shiftStartRow = shiftStartRows[excelShiftName]; // Excel row number (1-based)
                            int dataRowIndex = shiftStartRow - 1; // Convert to 0-based index

                            // Initialize the dictionary for this shift if not already present
                            if (!totalDemands.DemandData.ContainsKey(shiftName))
                            {
                                totalDemands.DemandData[shiftName] = new Dictionary<string, int?>();
                            }

                            // Build a route-to-row index mapping for this shift
                            Dictionary<string, int> routeRowMapping = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                            // Read routes and their corresponding rows
                            int currentRowIndex = dataRowIndex;
                            while (currentRowIndex < table.Rows.Count)
                            {
                                var routeCell = table.Rows[currentRowIndex][1]; // Assuming route names are in the second column (index 1)
                                string routeName = routeCell?.ToString().Trim();

                                // Check for empty route name or end of shift section
                                if (string.IsNullOrEmpty(routeName) || routeName.Equals("TOTAL", StringComparison.OrdinalIgnoreCase))
                                {
                                    break;
                                }

                                // If the route is not in solo_routes, skip it (ignore deprecated routes)
                                if (!solo_routes.Contains(routeName, StringComparer.OrdinalIgnoreCase))
                                {
                                    currentRowIndex++;
                                    continue;
                                }

                                // Map the route name to the row index
                                routeRowMapping[routeName] = currentRowIndex;

                                currentRowIndex++;
                            }

                            // For each route in solo_routes, get the demand value if present
                            foreach (var route in solo_routes)
                            {
                                int demand = 0; // Default to zero demand

                                if (routeRowMapping.TryGetValue(route, out int routeRowIndex))
                                {
                                    // Get the 'TOTAL' value from the corresponding row
                                    var row = table.Rows[routeRowIndex];
                                    object totalValue = row[totalColumnIndex];

                                    if (totalValue == null || string.IsNullOrWhiteSpace(totalValue.ToString()))
                                    {
                                        // Empty cell, treat as zero demand
                                        demand = 0;
                                    }
                                    else if (int.TryParse(totalValue.ToString(), out int parsedTotal))
                                    {
                                        // Successfully parsed the total value
                                        demand = parsedTotal;
                                    }
                                    else
                                    {
                                        // Parsing failed, this is an error
                                        demand = 0;
                                        hasErrors = true;
                                    }
                                }
                                else
                                {
                                    // Route not found in the shift, demand remains zero
                                    // This is expected for deprecated or missing routes
                                }

                                // Assign the demand value
                                totalDemands.DemandData[shiftName][route] = demand;
                            }

                            // Optionally, handle 'TOTAL' row if needed
                            // If you have a separate 'TOTAL' row per shift, implement here
                        }

                        // Determine if all data fields are filled without errors
                        if (!hasErrors)
                        {
                            totalDemands.IsDataFilled = true;
                            MessageBox.Show("Total demand sheet processed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("There were some errors reading the data. Please check the Excel file for invalid entries.", "Data Read Errors", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            totalDemands.IsDataFilled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading the Excel file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Debug Parser Function
        public void OutputDemandsToDebugConsole()
        {
            if (totalDemands == null || totalDemands.DemandData == null)
            {
                Debug.WriteLine("No demand data available.");
                return;
            }

            foreach (var shiftEntry in totalDemands.DemandData)
            {
                string shiftName = shiftEntry.Key;
                var routeDemands = shiftEntry.Value;

                Debug.WriteLine($"Shift: {shiftName}");
                foreach (var routeEntry in routeDemands)
                {
                    string routeName = routeEntry.Key;
                    int? demand = routeEntry.Value;
                    Debug.WriteLine($"  Route: {routeName}, Demand: {demand}");
                }
            }
        }

        #endregion

    }
}