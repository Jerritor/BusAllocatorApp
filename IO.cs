using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Data;
using System.IO;

using ExcelDataReader;
using System.Net.Http.Json;

namespace BusAllocatorApp
{
    public class TupleKeyConverter : JsonConverter<(string, string)>
    {
        public override (string, string) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var tupleString = reader.GetString();
            var parts = tupleString.Split('-');
            return (parts[0], parts[1]);
        }

        public override void Write(Utf8JsonWriter writer, (string, string) value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"{value.Item1}-{value.Item2}");
        }
    }
    public class IO
    {
        private Vars vars;
        private MainForm mainform;

        private readonly string dataFolder = "data";

        private readonly string deptsFileName = "depts.json";
        private readonly string routesFileName = "routes.json";
        private readonly string timeSetsFileName = "time_sets.json";
        private readonly string ratesFileName = "rates.json";
        private readonly string buffersFileName = "buffers.json";
        private readonly string busCapacitiesFileName = "bus_capacities.json";

        //Constructor
        public IO(Vars v, MainForm f)
        {
            vars = v;
            mainform = f;
        }

        //Utility Methods
        public void GenerateJSONFiles()
        {
            SaveBusCapacities();
            SaveBuffers();
            SaveRates();
            SaveDepartmentNames();
            SaveTimeSets();
            SaveRoutes();
        }

        string GetPathInDataFolder(string file_name)
        {
            return Path.Combine(dataFolder, file_name);
        }

        #region CONFIG FILE
        public void LoadConfig()
        {
            if (File.Exists(vars.configFile))
            {
                var lines = File.ReadAllLines(vars.configFile);
                foreach (var line in lines)
                {
                    if (line.StartsWith("rate_path=", StringComparison.OrdinalIgnoreCase))
                    {
                        vars.ratesPath = line.Substring("rate_path=".Length).Trim();
                    }
                    // Add additional config options here as needed
                }
            }
        }

        //sets the config option named 'optionName' to the data 'optionData'
        public void SetConfigOption(string optionName, string optionData)
        {
            try
            {
                if (!File.Exists(vars.configFile))
                {
                    throw new FileNotFoundException("Configuration file not found.");
                }

                var lines = File.ReadAllLines(vars.configFile);
                bool optionFound = false;

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith($"{optionName}=", StringComparison.OrdinalIgnoreCase))
                    {
                        lines[i] = $"{optionName}={optionData}";
                        optionFound = true;
                        break;
                    }
                }

                if (!optionFound)
                {
                    using (StreamWriter writer = new StreamWriter(vars.configFile, true))
                    {
                        writer.WriteLine($"{optionName}={optionData}");
                    }
                }
                else
                {
                    File.WriteAllLines(vars.configFile, lines);
                }

                Console.WriteLine($"Configuration option '{optionName}' set successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting configuration option: {ex.Message}");
            }
        }

        public void CreateConfigFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(vars.configFile))
                {
                    writer.WriteLine("rate_path=");
                    // Add additional default config options here as needed
                    //writer.WriteLine("another_option=");
                    //writer.WriteLine("yet_another_option=");
                }

                Console.WriteLine("Default configuration file created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating default configuration file: {ex.Message}");
            }
        }

        public void CheckAndCreateVarsFolderAndFiles()
        {
            if (!Directory.Exists(dataFolder))
            {
                try
                {
                    // Check if the 'data' folder exists, and create it if it doesn't
                    Directory.CreateDirectory(dataFolder);
                    Console.WriteLine("Data folder created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating data folder: {ex.Message}.\nTry creating the data folder yourself in the project directory.");
                    MessageBox.Show($"Error creating data folder: {ex.Message}. Try creating the data folder yourself in the project directory or contact IM.",
                                    "Cannot create data folder.",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }

            if (Directory.Exists(dataFolder))
            {
                bool isEmpty = Directory.GetFiles(dataFolder).Length == 0 &&
                               Directory.GetDirectories(dataFolder).Length == 0;

                if (isEmpty)
                {
                    //Ask if user wants to make dummy JSON Files
                    try
                    {
                        DialogResult result = MessageBox.Show("Do you want to use default data? You can edit all data later in the settings.\n" +
                                "If you are not sure, click 'Yes'.\n\n" +
                                "Click 'Yes' to use default data.\n" +
                                "Click 'No' if you want to manually set all data in the settings yourself.",
                            "Check Rates",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            //Set system vars to dummy values
                            vars.InstantiateVars();
                            //Generate JSON Files -- comment out after testing
                            GenerateJSONFiles();
                            MessageBox.Show($"Default data used and saved into the '{dataFolder}' folder.");
                        }
                        else if (result == DialogResult.No)
                        {
                            MessageBox.Show("No default data was generated. Modify all settings manually.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error generating default files: {e.Message}.");
                    }
                }
                else //If data folder has json files, load them.
                {
                    vars.LoadJSONFiles();
                }
            }
        }

        #endregion

        #region FILE UPLOADING
        public void UploadRatesSheet()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                vars.ratesPath = openFileDialog.FileName;
                SetConfigOption("rate_path", vars.ratesPath);

                vars.EnableBusRateCheckBox();
                vars.CheckSetModeCompletionState();

                //mainform.WriteLine("Rates sheet uploaded succesfully! " + vars.ratesPath);



                // Logic to handle the file upload
                //MessageBox.Show("Rates sheet uploaded successfully!");
                // Proceed with loading rates
            }
            else
            {
                vars.DisableBusRateCheckBox();
                vars.CheckSetModeCompletionState();
            }
        }

        public void UploadTotalDemandSheet()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                vars.totalDemandFilePath = openFileDialog.FileName;
            }
        }

        public List<string> UploadIndivDeptSpreadsheet()
        {
            // Create a file dialog to allow the user to upload department spreadsheets
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true, // Allow multiple file selection
                Filter = "Excel Files|*.xlsx;*.xls", // Only allow Excel files
                Title = "Select Department Demand Files"
            };

            // If the user selects files and clicks OK
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Return the list of selected file paths
                return openFileDialog.FileNames.ToList();
            }
            else
            {
                // If the user cancels or doesn't select any files, return null
                return null;
            }
        }



        #endregion

        #region DATA LOADING FROM JSON

        //Bus Rates Spreadsheet Reader
        private void ReadRatesSheet(string path)
        {
            vars.costSmallBus = new Dictionary<string, double>();
            vars.costLargeBus = new Dictionary<string, double>();
            vars.costSmallHybridRoute = new Dictionary<(string, string), double>();
            vars.costLargeHybridRoute = new Dictionary<(string, string), double>();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    DataTable costTable = result.Tables[0];

                    foreach (DataRow row in costTable.Rows)
                    {
                        string route = row["ROUTE"].ToString();
                        double totalLarge = Convert.ToDouble(row["TOTAL"]);
                        double totalSmall = Convert.ToDouble(row["TOTAL.1"]);

                        if (route.Contains("VIA"))
                        {
                            var routeParts = route.Split(new string[] { " VIA " }, StringSplitOptions.None);
                            var routeTuple = (routeParts[1], routeParts[0]);
                            vars.costLargeHybridRoute[routeTuple] = totalLarge;
                            vars.costSmallHybridRoute[routeTuple] = totalSmall;
                        }
                        else
                        {
                            vars.costLargeBus[route] = totalLarge;
                            vars.costSmallBus[route] = totalSmall;
                        }
                    }
                }
            }
        }

        //Load Routes
        public (List<string> soloRoutes, List<Tuple<string, string>> hybridRoutes) LoadRoutes()
        {
            string routesFilePath = GetPathInDataFolder(routesFileName);

            if (!File.Exists(routesFilePath))
            {
                return (new List<string>(), new List<Tuple<string, string>>());
            }

            string json = File.ReadAllText(routesFilePath);
            using (JsonDocument document = JsonDocument.Parse(json))
            {
                JsonElement root = document.RootElement;

                var soloRoutes = JsonSerializer.Deserialize<List<string>>(root.GetProperty("solo_routes").GetRawText());

                var hybridRoutes = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(root.GetProperty("hybrid_routes").GetRawText())
                                   .Select(item => Tuple.Create(item["Item1"], item["Item2"])).ToList();

                return (soloRoutes, hybridRoutes);
            }
        }
        
        //Load Time Sets
        public List<TimeSet> LoadTimeSets()
        {
            //Loading TimeSets from JSON
            string timeSetsFilePath = GetPathInDataFolder(timeSetsFileName);
            if (!File.Exists(timeSetsFilePath))
            {
                return new List<TimeSet>();
            }

            string json = File.ReadAllText(timeSetsFilePath);
            var timeSetDTOs = JsonSerializer.Deserialize<List<TimeSetDTO>>(json);

            var timeSets = timeSetDTOs.Select(dto => new TimeSet
            {
                IsOutgoing = dto.IsOutgoing,
                Time = TimeSet.ParseTimeFromAMPM(dto.Time.ToString()),
                IsFirstDay = dto.IsFirstDay,
                IsOutModel = dto.IsOutModel
            }).ToList();

            return timeSets;
        }

        //Load Departments
        public List<string> LoadDeptNames()
        {
            //Loading Dept names from JSON
            string deptsPath = GetPathInDataFolder(deptsFileName);
            if (!File.Exists(deptsPath))
            {
                mainform.WriteLine($"{deptsPath} does not exist.");
                return new List<string>();
            }
            else
            {
                string json = File.ReadAllText(deptsPath);
                //mainform.WriteLine($"{deptsPath} loaded from json file.");
                return JsonSerializer.Deserialize<List<string>>(json);
            }
        }

        #endregion

        #region DATA SAVING TO JSON

        //Convert List of Routes to JSON
        public void SaveRoutes()
        {
            var routesData = new
            {
                solo_routes = vars.solo_routes,
                hybrid_routes = vars.hybrid_routes
            };

            string json = JsonSerializer.Serialize(routesData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(GetPathInDataFolder(routesFileName), json);

            mainform.WriteLine("Converted Routes to JSON.");
        }

        // Convert List of Dictionaries of Time Sets to JSON
        public void SaveTimeSets()
        {
            var timeSetDTOs = vars.timeSets.Select(ts => new
            {
                IsOutgoing = ts.IsOutgoing,
                Time = ts.GetFormattedTime(),
                IsFirstDay = ts.IsFirstDay,
                IsOutModel = ts.IsOutModel
            }).ToList();

            string json = JsonSerializer.Serialize(timeSetDTOs, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(GetPathInDataFolder(timeSetsFileName), json);

            mainform.WriteLine("Converted Time Sets to JSON.");

            /** OLD WAY
            string json = JsonSerializer.Serialize(vars.timeSets, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(GetPathInDataFolder(timeSetsFileName), json);

            mainform.WriteLine("Converted Time Sets to JSON."); **/
        }

        // Convert List of Departments to JSON
        public void SaveDepartmentNames()
        {
            SaveDeptNamesPrivate(vars.deptNames);
        }

        public void SaveDepartmentNames(List<string> deptnames)
        {
            SaveDeptNamesPrivate(deptnames);
        }

        private void SaveDeptNamesPrivate(List<string> deptnames)
        {
            string json = JsonSerializer.Serialize(deptnames, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(GetPathInDataFolder(deptsFileName), json);

            mainform.WriteLine("Converted Departments to JSON.");
        }

        // Convert Dictionary of Rates to JSON
        public void SaveRates()
        {
            RatesData ratesData = new RatesData
            {
                costSmallBus = vars.costSmallBus,
                costLargeBus = vars.costLargeBus,
                costSmallHybridRoute = vars.costSmallHybridRoute,
                costLargeHybridRoute = vars.costLargeHybridRoute
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new TupleKeyConverter() }
            };

            string json = JsonSerializer.Serialize(ratesData, options);
            File.WriteAllText(GetPathInDataFolder(ratesFileName), json);

            mainform.WriteLine("Converted Bus Rates to JSON.");
        }

        private class RatesData
        {
            public required Dictionary<string, double> costSmallBus { get; set; }
            public required Dictionary<string, double> costLargeBus { get; set; }

            [JsonConverter(typeof(DictionaryTupleDoubleConverter))]
            public required Dictionary<(string, string), double> costSmallHybridRoute { get; set; }

            [JsonConverter(typeof(DictionaryTupleDoubleConverter))]
            public required Dictionary<(string, string), double> costLargeHybridRoute { get; set; }
        }

        private class DictionaryTupleDoubleConverter : JsonConverter<Dictionary<(string, string), double>>
        {
            public override Dictionary<(string, string), double> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, Dictionary<(string, string), double> value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                foreach (var kvp in value)
                {
                    writer.WritePropertyName($"{kvp.Key.Item1}-{kvp.Key.Item2}");
                    writer.WriteNumberValue(kvp.Value);
                }
                writer.WriteEndObject();
            }
        }

        // Convert Dictionary of Buffer Capacities to JSON
        public void SaveBuffers()
        {
            var buffersData = new BuffersData
            {
                bufferCurrentSmall = vars.bufferCurrentSmall,
                bufferCurrentLarge = vars.bufferCurrentLarge
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new TupleKeyConverter() }
            };

            string json = JsonSerializer.Serialize(buffersData, options);
            File.WriteAllText(GetPathInDataFolder(buffersFileName), json);

            mainform.WriteLine("Converted Buffer Capacities to JSON.");
        }

        private class BuffersData
        {
            [JsonConverter(typeof(DictionaryObjectIntConverter))]
            public required Dictionary<object, int> bufferCurrentSmall { get; set; }

            [JsonConverter(typeof(DictionaryObjectIntConverter))]
            public required Dictionary<object, int> bufferCurrentLarge { get; set; }
        }

        private class DictionaryObjectIntConverter : JsonConverter<Dictionary<object, int>>
        {
            public override Dictionary<object, int> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, Dictionary<object, int> value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                foreach (var kvp in value)
                {
                    if (kvp.Key is string)
                    {
                        writer.WritePropertyName((string)kvp.Key);
                        writer.WriteNumberValue(kvp.Value);
                    }
                    else if (kvp.Key is ValueTuple<string, string> tuple)
                    {
                        writer.WritePropertyName($"{tuple.Item1}-{tuple.Item2}");
                        writer.WriteNumberValue(kvp.Value);
                    }
                }
                writer.WriteEndObject();
            }
        }

        // Convert Bus Max Capacities to JSON
        public void SaveBusCapacities()
        {
            var capacitiesData = new
            {
                capacitySmallBus = vars.capacitySmallBus,
                capacityLargeBus = vars.capacityLargeBus
            };

            string json = JsonSerializer.Serialize(capacitiesData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(GetPathInDataFolder(busCapacitiesFileName), json);

            mainform.WriteLine("Converted Bus Capacities to JSON.");
        }

        #endregion
    }
}
