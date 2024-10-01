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
            Initialized, //initialized but not demand data not completed
            Completed //demands filled
        }
        //tracks deptsAndDemands state
        //is initially on by default as default mode is individual dept demand mode
        public CompletionState IsDeptsAndDemandsCompleted { get; set; } = CompletionState.Initialized;
        //tracks totalDemands state
        //is initially off by default as default mode is not all dept demand mode
        public CompletionState IsTotalDemandsCompleted { get; set; } = CompletionState.Uninitialized;

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
            deptsAndDemands = deptNames.Select(deptname => new Department(deptname,this)).ToList();

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
        public void UpdateDepartmentsWithRoutesAndTimeSets(bool initializeDemands = false)
        {
            foreach (var dept in deptsAndDemands)
            {
                UpdateDepartmentWithRoutesAndTimeSets(dept, initializeDemands);
            }
        }

        private void UpdateDepartmentWithRoutesAndTimeSets(Department department, bool initializeDemands)
        {
            var updatedDemandData = new Dictionary<string, Dictionary<string, int?>>();

            // Iterate over TimeSets in order
            foreach (var timeSet in timeSets)
            {
                // Include the _True or _False suffix based on IsOutgoing
                string timeSetKey = $"{timeSet.Time.ToString(@"hh\:mm\:ss")}_{timeSet.IsOutgoing}";
                if (!updatedDemandData.ContainsKey(timeSetKey))
                {
                    updatedDemandData[timeSetKey] = new Dictionary<string, int?>();
                }

                // Update demands for solo routes
                foreach (var route in solo_routes)
                {
                    if (!updatedDemandData[timeSetKey].ContainsKey(route))
                    {
                        int? initialDemand = initializeDemands
                            ? null
                            : (department.DemandData.ContainsKey(timeSetKey) && department.DemandData[timeSetKey].ContainsKey(route)
                                ? department.DemandData[timeSetKey][route]
                                : 0);
                        updatedDemandData[timeSetKey][route] = initialDemand;
                    }
                }

                /**
                // If you have hybrid routes, ensure they are handled similarly
                foreach (var hybridRoute in hybrid_routes)
                {
                    string hybridRouteKey = $"{hybridRoute.Item1}-{hybridRoute.Item2}";
                    if (!updatedDemandData[timeSetKey].ContainsKey(hybridRouteKey))
                    {
                        int? initialDemand = initializeDemands 
                            ? null 
                            : (department.DemandData.ContainsKey(timeSetKey) && department.DemandData[timeSetKey].ContainsKey(hybridRouteKey) 
                                ? department.DemandData[timeSetKey][hybridRouteKey] 
                                : 0);
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
                        totalDemands = new Department("Total",this);

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

        public void ClearTotalDemandsData()
        {
            if (totalDemands != null)
            {
                totalDemands.ClearDemandData();
            }
            mainForm.WriteLine("Cleared all demand data from Total Demand Mode.");
        }

        #endregion

        #region Individual Department Demand Spreadsheet Processing
        public void ClearDeptsAndDemandsData()
        {
            if (deptsAndDemands != null)
            {
                foreach (Department dept in deptsAndDemands)
                {
                    dept.ClearDemandData();
                }
            }
            mainForm.WriteLine("Cleared all demand data from Individual Departments Mode.");
        }

        public bool ProcessIndivDeptSpreadsheet(string filePath, bool isDebug = false)
        {
            // Register the code page provider to handle different encodings
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            if (isDebug) Debug.WriteLine($"Starting to process spreadsheet: {filePath}");

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();

                    if (result.Tables.Count == 0)
                    {
                        if (isDebug)Debug.WriteLine("No worksheets found in the Excel file.");
                        return false;
                    }

                    var table = result.Tables[0];

                    if (isDebug) Debug.WriteLine($"Processing worksheet: {table.TableName}");

                    // Read the department name from cell B1 (Row 0, Column 1)
                    string departmentName = table.Rows.Count > 0 && table.Columns.Count > 1
                        ? table.Rows[0][1]?.ToString().Trim()
                        : string.Empty;

                    if (isDebug) Debug.WriteLine($"Extracted Department Name: '{departmentName}' from cell B1.");

                    if (string.IsNullOrEmpty(departmentName))
                    {
                        if (isDebug)Debug.WriteLine("Department name is empty. Cannot process spreadsheet.");
                        return false;
                    }

                    // Find the Department object in deptsAndDemands
                    var department = deptsAndDemands.FirstOrDefault(d => d.Name.Equals(departmentName, StringComparison.OrdinalIgnoreCase));

                    if (department == null)
                    {
                        if (isDebug) Debug.WriteLine($"Department '{departmentName}' not found in deptsAndDemands.");
                        return false;
                    }

                    if (isDebug) Debug.WriteLine($"Found Department: {department.Name}");

                    try
                    {
                        // Read route names from cells D5:D10 (Rows 4 to 9, Column 3)
                        List<string> routeNamesFromSpreadsheet = new List<string>();

                        for (int row = 4; row <= 9; row++) // Rows 5 to 10 inclusive
                        {
                            if (row >= table.Rows.Count)
                            {
                                if (isDebug) Debug.WriteLine($"Row {row + 1} is out of range. Skipping.");
                                continue;
                            }

                            string routeName = table.Rows[row][3]?.ToString().Trim(); // Column D is index 3

                            if (isDebug) Debug.WriteLine($"Extracted Route Name from cell D{row + 1}: '{routeName}'");

                            if (!string.IsNullOrEmpty(routeName))
                            {
                                routeNamesFromSpreadsheet.Add(routeName);
                            }
                        }

                        if (isDebug) Debug.WriteLine($"Total Routes Extracted: {routeNamesFromSpreadsheet.Count}");

                        // Get the list of routes from department.DemandData
                        var departmentRoutes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        foreach (var timeSet in department.DemandData.Values)
                        {
                            foreach (var route in timeSet.Keys)
                            {
                                departmentRoutes.Add(route);
                            }
                            break; // Assuming all TimeSets have the same routes
                        }

                        if (isDebug) Debug.WriteLine($"Department '{department.Name}' has {departmentRoutes.Count} routes.");

                        // Create a mapping from route names in the spreadsheet to department routes
                        var routeMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                        foreach (var routeName in routeNamesFromSpreadsheet)
                        {
                            if (departmentRoutes.Contains(routeName))
                            {
                                routeMapping[routeName] = routeName;
                                if (isDebug) Debug.WriteLine($"Route '{routeName}' mapped successfully.");
                            }
                            else
                            {
                                if (isDebug) Debug.WriteLine($"Route '{routeName}' from spreadsheet not found in department routes. Skipping.");
                                // Route from spreadsheet not found in department routes; skip
                            }
                        }

                        if (isDebug) Debug.WriteLine($"Total Routes Mapped: {routeMapping.Count}");

                        // Define the specific TimeSets and their corresponding columns
                        var timeAllocations = new Dictionary<string, int>
                {
                    {"OUT 4:00PM", 4},  // Column E (index 4)
                    {"OUT 6:00PM", 5},  // Column F (index 5)
                    {"OUT 7:00PM", 6},  // Column G (index 6)
                    {"IN 7:00PM", 7},   // Column H (index 7)
                    {"IN 10:00PM", 8},  // Column I (index 8)
                    {"OUT 4:00AM", 10}, // Column K (index 10)
                    {"OUT 7:00AM", 11}, // Column L (index 11)
                    {"IN 7:00AM", 12},  // Column M (index 12)
                };

                        if (isDebug)
                        {
                            Debug.WriteLine("Defined Time Allocations and their corresponding columns:");
                            foreach (var ta in timeAllocations)
                            {
                                Debug.WriteLine($"        '{ta.Key}' => Column {ta.Value + 1}");
                            }
                        }

                        // Create a mapping from time strings to TimeSet keys used in department.DemandData
                        var timeSetKeyMapping = new Dictionary<string, string>();

                        foreach (var timeAllocation in timeAllocations)
                        {
                            string timeString = timeAllocation.Key; // e.g., "OUT 4:00PM"
                            int column = timeAllocation.Value;

                            // Extract the "OUT" or "IN" part
                            string inOut = timeString.Split(' ')[0]; // "OUT" or "IN"

                            // Extract the time part
                            string timePart = timeString.Split(' ')[1]; // "4:00PM"

                            // Parse timePart into DateTime
                            if (!DateTime.TryParseExact(timePart, "h:mmtt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
                            {
                                if (isDebug) Debug.WriteLine($"Failed to parse time '{timePart}' from time string '{timeString}'. Skipping.");
                                continue;
                            }

                            TimeSpan timeSpan = dateTime.TimeOfDay;
                            bool isOutgoing = inOut.Equals("OUT", StringComparison.OrdinalIgnoreCase);
                            string timeSetKey = $"{timeSpan.ToString(@"hh\:mm\:ss")}_{isOutgoing}";

                            // Add to the mapping
                            if (!timeSetKeyMapping.ContainsKey(timeString))
                            {
                                timeSetKeyMapping[timeString] = timeSetKey;
                                if (isDebug) Debug.WriteLine($"Mapped Time String '{timeString}' to TimeSetKey '{timeSetKey}'");
                            }
                        }

                        // Now iterate over each time allocation using the TimeSetKey
                        foreach (var timeAllocation in timeAllocations)
                        {
                            string timeString = timeAllocation.Key; // e.g., "OUT 4:00PM"
                            int column = timeAllocation.Value;

                            if (!timeSetKeyMapping.TryGetValue(timeString, out string timeSetKey))
                            {
                                if (isDebug)  Debug.WriteLine($"TimeSetKey not found for time string '{timeString}'. Skipping.");
                                continue;
                            }

                            if (isDebug)
                            {
                                Debug.WriteLine($"Processing TimeSet: '{timeSetKey}' (from '{timeString}') in Column {column + 1}");
                            }

                            // Check if the TimeSet exists in department.DemandData
                            if (!department.DemandData.ContainsKey(timeSetKey))
                            {
                                if (isDebug) Debug.WriteLine($"TimeSet '{timeSetKey}' not found in department.DemandData. Skipping.");
                                continue;
                            }

                            // Iterate over the routes from the spreadsheet
                            for (int i = 0; i < routeNamesFromSpreadsheet.Count; i++)
                            {
                                string routeNameFromSpreadsheet = routeNamesFromSpreadsheet[i];

                                // Check if this route is mapped to a department route
                                if (!routeMapping.ContainsKey(routeNameFromSpreadsheet))
                                {
                                    if (isDebug) Debug.WriteLine($"Route '{routeNameFromSpreadsheet}' is not mapped to department routes. Skipping.");
                                    continue;
                                }

                                string departmentRouteName = routeMapping[routeNameFromSpreadsheet];

                                int row = 4 + i; // Since route names are from rows 5 to 10 (0-based index 4 to 9)

                                if (row >= table.Rows.Count)
                                {
                                    if (isDebug) Debug.WriteLine($"Row {row + 1} is out of range for demands. Skipping.");
                                    continue;
                                }

                                if (column >= table.Columns.Count)
                                {
                                    if (isDebug) Debug.WriteLine($"Column {column + 1} is out of range for demands. Skipping.");
                                    continue;
                                }

                                object demandObj = table.Rows[row][column];
                                string demandText = demandObj?.ToString().Trim();

                                if (isDebug) Debug.WriteLine($"Reading Demand from cell ({row + 1}, {column + 1}): '{demandText}'");

                                int demand = 0;
                                if (string.IsNullOrEmpty(demandText))
                                {
                                    demand = 0;
                                    if (isDebug) Debug.WriteLine($"Demand is empty. Treated as 0.");
                                }
                                else if (!int.TryParse(demandText, out demand) || demand < 0)
                                {
                                    string errorMsg = $"Invalid demand value at cell (Row {row + 1}, Column {column + 1}): '{demandText}'";
                                    if (isDebug) Debug.WriteLine($"{errorMsg}");
                                    throw new Exception(errorMsg);
                                }

                                // Update the department's DemandData
                                department.DemandData[timeSetKey][departmentRouteName] = demand;

                                if (isDebug)  Debug.WriteLine($"Updated DemandData: TimeSet='{timeSetKey}', Route='{departmentRouteName}', Demand={demand}");
                            }
                        }

                        department.IsDataFilled = true;

                        if (isDebug) Debug.WriteLine($"Successfully processed department '{department.Name}'. IsDataFilled set to true.");

                        return true;
                    }
                    catch (Exception ex)
                    {
                        if (isDebug) Debug.WriteLine($"Exception occurred while processing spreadsheet: {ex.Message}");
                        department.IsDataFilled = false;
                        return false;
                    }
                }
            }
        }

        #endregion

        #region Demand Mode State Retrieval
        //1 = individual department mode, 2 = total department mode
        public int GetDemandMode()
        {
            if (IsTotalDemandsCompleted != CompletionState.Uninitialized)
            {
                // Total Department mode is active
                return 2;
            }
            else if (IsDeptsAndDemandsCompleted != CompletionState.Uninitialized)
            {
                // Individual Department mode is active
                return 1;
            }
            else
            {
                // Neither mode is properly initialized
                throw new InvalidOperationException("Demand mode is not properly initialized.");
            }
        }
        #endregion

        #region DataGridView Handling

        public void ClearAllDemandsInDataGridView()
        {
            /**
            // Ensure the DataGridView has data
            if (mainForm.dataGridView1.DataSource == null)
            {
                MessageBox.Show("There is no data to clear.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }**/

            /**
            // Loop through each row in the DataGridView
            foreach (DataGridViewRow row in mainForm.dataGridView1.Rows)
            {
                // Skip the "Location" column (index 0), which contains route names
                for (int col = 1; col < mainForm.dataGridView1.Columns.Count; col++)
                {
                    // Set all demand cells (columns from index 1 onward) to empty
                    row.Cells[col].Value = DBNull.Value; // Sets the cell to empty
                }
            }
            **/

            //mainForm.table.Clear();

            foreach (DataRow row in mainForm.table.Rows)
            {
                // Skip the first column (routes) and clear only the demand columns
                for (int col = 1; col < mainForm.table.Columns.Count; col++)
                {
                    // Clear the demand data (set to DBNull)
                    row[col] = DBNull.Value;
                }
            }


            // Optionally, refresh the DataGridView to reflect the changes immediately
            mainForm.dataGridView1.Refresh();
        }

        public void InitializeEmptyDataGridView()
        {
            DataTable table = mainForm.table;
            DataGridView dataGridView1 = mainForm.dataGridView1;

            // Add the "Location" column
            table.Columns.Add("Location", typeof(string));

            // Extract unique shift names from timeSets using GetFormattedTimeINOUT()
            var shifts = timeSets
                            .Select(ts => ts.GetFormattedTimeINOUT())
                            .Distinct()
                            .OrderBy(s => s)
                            .ToList();

            // Add a column for each shift
            foreach (var shift in shifts)
            {
                table.Columns.Add(shift, typeof(int));
            }

            // Add a row for each route with empty demand values
            foreach (var route in solo_routes)
            {
                DataRow newRow = table.NewRow();
                newRow["Location"] = route;

                // Initialize all shift demand values to DBNull (empty)
                foreach (var shift in shifts)
                {
                    newRow[shift] = DBNull.Value; // Sets the cell to empty
                                                  // Alternatively, use 0 if you prefer zero instead of empty:
                                                  // newRow[shift] = 0;
                }

                table.Rows.Add(newRow);
            }

            // Set the DataSource of the DataGridView
            dataGridView1.DataSource = table;

            // Optionally, adjust column headers for better readability
            dataGridView1.Columns["Location"].HeaderText = "Location";

            // Optional: Adjust column widths for better visibility
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            // Format the DataGridView (optional)
            mainForm.ResizeDataGridView();
            mainForm.FormatDataGridView();
            mainForm.ResizeFormToFitTableLayoutPanel();
        }




        #endregion

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

        public void OutputDemandModeToDebugConsole()
        {
            Debug.WriteLine("deptsMode = " + IsDeptsAndDemandsCompleted);
            Debug.WriteLine("totalMode = " + IsTotalDemandsCompleted + "\n");
        }

    }
}