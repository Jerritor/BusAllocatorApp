using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Security;
using System.Windows.Forms;

namespace BusAllocatorApp
{
    public partial class MainForm : Form
    {
        Vars vars;
        Settings settings;

        public DataTable table;

        private bool isSecondDateSet = false;

        public MainForm()
        {
            InitializeComponent();

            vars = new Vars(this);
            settings = new Settings(vars);

            //File validation
            CheckConfigFileAndRatesPath();
            vars.io.CheckAndCreateVarsFolderAndFiles();

            //UpdateDataGrid();

            if (vars.deptsAndDemands == null || vars.deptsAndDemands.Count == 0)
            {
                vars.LoadDepartments(true);
            }

            //Initialize empty demands table
            table = new DataTable();
            dataGridView1.DataSource = table;
            vars.InitializeEmptyDataGridView();

            //DEBUG FUNCTIONS
            vars.OutputDemandModeToDebugConsole();


            //EVERYTHING AFTER HERE IS ON FORM LOAD

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            vars.SyncMainFormCheckboxes();
            //Enable below to create JSON files
            //vars.GenerateJSONFiles();
        }

        #region Total Riders Data Grid
        public void UpdateDataGrid()
        {
            UpdateTotalDemandDisplay();

            //Formatting
            ResizeDataGridView();
            FormatDataGridView();
            ResizeFormToFitTableLayoutPanel();
        }
        public void ResizeDataGridView()
        {
            // Calculate the required size
            int totalWidth = dataGridView1.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
            int totalHeight = dataGridView1.Rows.GetRowsHeight(DataGridViewElementStates.Visible);// + dataGridView1.ColumnHeadersHeight;

            // Adjust the size of DataGridView
            dataGridView1.ClientSize = new Size(totalWidth, totalHeight);
        }

        public void FormatDataGridView()
        {
            // Disable sorting on all columns
            dataGridView1.ColumnAdded += (sender, e) =>
            {
                e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            };

            // Set a specific column's cells as bold
            dataGridView1.CellFormatting += (sender, e) =>
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Location")
                {
                    e.CellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                }
            };
        }

        public void ResizeFormToFitTableLayoutPanel()
        {
            // Calculate the preferred size of the TableLayoutPanel
            Size preferredSize = tableLayoutPanel1.PreferredSize;

            // Set the size of the form to fit the TableLayoutPanel
            this.ClientSize = new Size(preferredSize.Width, preferredSize.Height);
        }

        public void UpdateTotalDemandDisplay()
        {
            // Determine the current demand mode: 1 = Individual Department Mode, 2 = Total Demand Mode
            int demandMode = vars.GetDemandMode();

            // This will hold the demand data to display
            Dictionary<string, Dictionary<string, int?>> displayDemandData = null;

            // This will hold the formatted shift names for column headers
            List<string> shiftsFormatted = new List<string>();

            // This will map formatted shift names back to raw time keys (only used in Individual Department Mode)
            Dictionary<string, string> formattedToRawShiftMap = new Dictionary<string, string>();

            if (demandMode == 2)
            {
                // --- Total Demand Mode ---

                if (vars.totalDemands == null || vars.totalDemands.DemandData == null)
                {
                    MessageBox.Show("No demand data available to display.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Use the total demands data directly
                displayDemandData = vars.totalDemands.DemandData;

                // Get all shifts (formatted strings) and sort them
                shiftsFormatted = displayDemandData.Keys.OrderBy(s => s).ToList(); // Assuming shifts are already formatted strings
            }
            else if (demandMode == 1)
            {
                // --- Individual Department Mode ---

                // Aggregate demands from all departments
                displayDemandData = new Dictionary<string, Dictionary<string, int?>>();

                foreach (var department in vars.deptsAndDemands)
                {
                    if (!department.IsDataFilled)
                    {
                        // Optionally skip departments without data
                        continue;
                    }

                    foreach (var timeKey in department.DemandData.Keys)
                    {
                        if (!displayDemandData.ContainsKey(timeKey))
                        {
                            displayDemandData[timeKey] = new Dictionary<string, int?>();
                        }

                        foreach (var route in department.DemandData[timeKey].Keys)
                        {
                            int? demandValue = department.DemandData[timeKey][route];
                            if (demandValue.HasValue)
                            {
                                if (!displayDemandData[timeKey].ContainsKey(route))
                                {
                                    displayDemandData[timeKey][route] = 0;
                                }
                                displayDemandData[timeKey][route] += demandValue.Value;
                            }
                        }
                    }
                }

                if (displayDemandData.Count == 0)
                {
                    MessageBox.Show("No demand data available to display.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Build a mapping from raw time keys ("HH:mm:ss_True/False") to formatted shift names ("OUT 4:00PM")
                var rawToFormattedShiftMap = vars.timeSets.ToDictionary(
                    ts => $"{ts.Time.ToString(@"hh\:mm\:ss")}_{ts.IsOutgoing}",
                    ts => ts.GetFormattedTimeINOUT());

                // Invert the mapping for easy lookup (formatted to raw)
                formattedToRawShiftMap = rawToFormattedShiftMap.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

                // Now, prepare the formatted shifts list in a sorted order
                shiftsFormatted = displayDemandData.Keys
                    .Where(k => rawToFormattedShiftMap.ContainsKey(k))
                    .Select(k => rawToFormattedShiftMap[k])
                    .OrderBy(s => s)
                    .ToList();
            }
            else
            {
                // Unknown demand mode
                MessageBox.Show("Unknown demand mode selected. Please contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // --- Preparing the Display Table ---

            // Clear existing columns and rows
            table.Columns.Clear();
            table.Rows.Clear();

            // Add "Location" column
            table.Columns.Add("Location", typeof(string));

            // Add a column for each shift
            foreach (var shift in shiftsFormatted)
            {
                table.Columns.Add(shift, typeof(int));
            }

            // Populate the DataTable with solo_routes
            foreach (var route in vars.solo_routes)
            {
                DataRow newRow = table.NewRow();
                newRow["Location"] = route;

                for (int i = 0; i < shiftsFormatted.Count; i++)
                {
                    string shift = shiftsFormatted[i];
                    string rawTimeKey = demandMode == 2 ? shift : (formattedToRawShiftMap.ContainsKey(shift) ? formattedToRawShiftMap[shift] : "");

                    if (demandMode == 2)
                    {
                        // Total Demand Mode
                        if (displayDemandData.ContainsKey(shift) && displayDemandData[shift].ContainsKey(route))
                        {
                            newRow[shift] = displayDemandData[shift][route];
                        }
                        else
                        {
                            // If for some reason the route is missing, default to 0
                            newRow[shift] = 0;
                        }
                    }
                    else if (demandMode == 1)
                    {
                        // Individual Department Mode
                        if (!string.IsNullOrEmpty(rawTimeKey) && displayDemandData.ContainsKey(rawTimeKey) && displayDemandData[rawTimeKey].ContainsKey(route))
                        {
                            newRow[shift] = displayDemandData[rawTimeKey][route];
                        }
                        else
                        {
                            // If the route is missing for this shift, default to 0
                            newRow[shift] = 0;
                        }
                    }
                }

                table.Rows.Add(newRow);
            }

            /**
            // Determine the current demand mode: 1 = Individual Department Mode, 2 = Total Demand Mode
            int demandMode = vars.GetDemandMode();

            // This will hold the demand data to display
            Dictionary<string, Dictionary<string, int?>> demandData = null;

            // Total Demand Mode
            if (demandMode == 2)
            {
                
                if (vars.totalDemands == null || vars.totalDemands.DemandData == null)
                {
                    MessageBox.Show("No demand data available to display.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                demandData = vars.totalDemands.DemandData;
            }
            // Individual Department Mode
            else if (demandMode == 1)
            {
                // Aggregate demands from all departments
                demandData = new Dictionary<string, Dictionary<string, int?>>();

                foreach (var department in vars.deptsAndDemands)
                {
                    if (!department.IsDataFilled)
                    {
                        // Optionally skip departments without data
                        continue;
                    }

                    foreach (var timeKey in department.DemandData.Keys)
                    {
                        if (!demandData.ContainsKey(timeKey))
                        {
                            demandData[timeKey] = new Dictionary<string, int?>();
                        }

                        foreach (var route in department.DemandData[timeKey].Keys)
                        {
                            int? demandValue = department.DemandData[timeKey][route];
                            if (demandValue.HasValue)
                            {
                                if (!demandData[timeKey].ContainsKey(route))
                                {
                                    demandData[timeKey][route] = 0;
                                }
                                demandData[timeKey][route] += demandValue.Value;
                            }
                        }
                    }
                }

                if (demandData.Count == 0)
                {
                    MessageBox.Show("No demand data available to display.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Unknown demand mode selected. Please contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Now, demandData contains the aggregated demands for the current mode.

            // Get all unique shifts (time keys) from DemandData and order them
            var shifts = demandData.Keys.OrderBy(s => s).ToList();

            // Clear existing columns and rows in the table
            table.Columns.Clear();
            table.Rows.Clear();

            // Add "Location" column
            table.Columns.Add("Location", typeof(string));

            // Add a column for each shift
            foreach (var shift in shifts)
            {
                table.Columns.Add(shift, typeof(int));
            }

            // Populate the DataTable with solo_routes
            foreach (var route in vars.solo_routes)
            {
                DataRow newRow = table.NewRow();
                newRow["Location"] = route;

                foreach (var shift in shifts)
                {
                    if (demandData[shift].ContainsKey(route))
                    {
                        newRow[shift] = demandData[shift][route];
                    }
                    else
                    {
                        // If the route is missing for this shift, default to 0
                        newRow[shift] = 0;
                    }
                }

                table.Rows.Add(newRow);
            }
            **/ //2
            /**
            if (vars.totalDemands == null || vars.totalDemands.DemandData == null)
            {
                MessageBox.Show("No demand data available to display.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //clear current rows
            table.Rows.Clear();

            // Populate the DataTable with solo_routes
            foreach (var route in vars.solo_routes)
            {
                DataRow newRow = table.NewRow();
                newRow["Location"] = route;

                foreach (var shift in shifts)
                {
                    if (vars.totalDemands.DemandData[shift].ContainsKey(route))
                    {
                        newRow[shift] = vars.totalDemands.DemandData[shift][route];
                    }
                    else
                    {
                        // If for some reason the route is missing, default to 0
                        newRow[shift] = 0;
                    }
                }

                table.Rows.Add(newRow);
            }
            **/ //1
        }


        #endregion
        public void WriteLine(string message)
        {
            if (outputLog.InvokeRequired)
            {
                outputLog.Invoke(new Action(() => WriteLine(message)));
            }
            else
            {
                outputLog.AppendText(message + Environment.NewLine);

                //Scroll to bottom code
                /**
                outputLog.SelectionStart = outputLog.Text.Length;
                outputLog.SelectionLength = 0;
                outputLog.ScrollToCaret();
                outputLog.Refresh(); **/
            }
        }

        #region DATE PICKERS
        private bool CheckFirstDate(out bool result)
        {
            if (isSecondDateSet && (secondDatePicker.Value.Date <= firstDatePicker.Value.Date))
            {
                MessageBox.Show("The first date must be before the second date.",
                    "Invalid First Date",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                vars.DisableFirstDateCheckBox();

                result = false;
                return false;
            }
            else
            {
                vars.EnableFirstDateCheckBox();
                result = true;
                return true;
            }
        }

        private bool CheckSecondDate()
        {
            if (secondDatePicker.Value.Date <= firstDatePicker.Value.Date)
            {
                MessageBox.Show("The second date must be after the first date.",
                    "Invalid Second Date",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                vars.DisableSecondDateCheckBox();
                return false;
            }
            else
            {
                vars.EnableSecondDateCheckBox();
                return true;
            }
        }

        private void editFirstDateButton_Click(object sender, EventArgs e)
        {
            firstDatePicker.Enabled = true;

            editFirstDateButton.Enabled = false;
            editFirstDateButton.Visible = false;

            editSecondDateButton.Enabled = false;
            editSecondDateButton.Visible = false;

            setFirstDateButton.Visible = true;
            setFirstDateButton.Enabled = true;
        }

        private void setFirstDateButton_Click(object sender, EventArgs e)
        {
            bool isFirstDate = false;
            //bool result = false;
            //if (isSecondDateSet && !CheckFirstDate(out result)) return;
            //else isFirstDate = result;

            if (isSecondDateSet)
            {
                // If the second date is set, validate the first date against it
                bool checkResult = CheckFirstDate(out bool result);
                if (!checkResult)
                {
                    // If validation fails, exit the method
                    return;
                }
                isFirstDate = result; // Assign the result from CheckFirstDate
            }
            else
            {
                vars.EnableFirstDateCheckBox();
                // If the second date is not set, setting the first date is always valid
                isFirstDate = true;
            }


            if (isFirstDate)
            {
                firstDatePicker.Enabled = false;
                setFirstDateButton.Enabled = false;
                editFirstDateButton.Enabled = true;
                editSecondDateButton.Enabled = true;
                firstDatePicker.Enabled = false;

                setFirstDateButton.Enabled = false;
                setFirstDateButton.Visible = false;

                editFirstDateButton.Enabled = true;
                editFirstDateButton.Visible = true;

                editSecondDateButton.Enabled = true;
                editSecondDateButton.Visible = true;

                vars.firstDay = firstDatePicker.Value.Date;
                string formattedDate = firstDatePicker.Value.ToLongDateString();
                WriteLine("First date selected: " + formattedDate);
                //WriteLine(vars.firstDay.Value.ToString());
            }
            vars.CheckSetModeCompletionState(true);
        }
        private void setSecondDateButton_Click(object sender, EventArgs e)
        {
            if (CheckSecondDate())
            {
                secondDatePicker.Enabled = false;

                editFirstDateButton.Enabled = true;
                editFirstDateButton.Visible = true;

                setSecondDateButton.Enabled = false;
                setSecondDateButton.Visible = false;

                editSecondDateButton.Enabled = true;
                editSecondDateButton.Visible = true;

                isSecondDateSet = true;

                vars.secondDay = secondDatePicker.Value.Date;
                string formattedDate = secondDatePicker.Value.ToLongDateString();
                WriteLine("Second date selected: " + formattedDate);
                //WriteLine(vars.secondDay.Value.ToString());
            }
            vars.CheckSetModeCompletionState(true);
        }
        private void editSecondDateButton_Click(object sender, EventArgs e)
        {
            secondDatePicker.Enabled = true;

            editFirstDateButton.Enabled = false;
            editFirstDateButton.Visible = false;

            editSecondDateButton.Enabled = false;
            editSecondDateButton.Visible = false;

            setSecondDateButton.Enabled = true;
            setSecondDateButton.Visible = true;
        }
        private void firstDatePicker_ValueChanged(object sender, EventArgs e)
        {
            //'out _' discards the outputed value
            CheckFirstDate(out _);
            vars.CheckSetModeCompletionState(true);
        }
        private void secondDatePicker_ValueChanged(object sender, EventArgs e)
        {
            CheckSecondDate();
            vars.CheckSetModeCompletionState(true);
        }
        #endregion

        #region Bus Rate Spreadsheet On Start Popup
        private void CheckConfigFileAndRatesPath()
        {
            if (!File.Exists(vars.configFile))
            {
                MessageBox.Show("Configuration file not found. A new 'config.cfg' will be created in the application directory.",
                                "Configuration File Missing",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                vars.io.CreateConfigFile();
            }
            else //if config file doesnt exist
            {
                if (string.IsNullOrWhiteSpace(vars.ratesPath)) //if rates file name doesnt exist in the config
                {
                    MessageBox.Show("Rates file not found in the configuration file or it is empty. Please upload a Bus Rates spreadsheet or manually set the rates in the settings.",
                                    "Rates Path Missing",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    vars.DisableBusRateCheckBox();
                    //vars.CheckSetModeCompletionState();
                }
                else
                {
                    ShowRatesChangedPopup();
                }

                //TODO: ADD MORE IFELSE STATEMENTS TO CHECK OTHER CONFIG OPTIONS HERE
            }
        }

        private void ShowRatesChangedPopup()
        {
            DialogResult result = MessageBox.Show("Have the bus rates changed since your last update?\n\nClick 'Yes' to upload new rates.\nClick 'No' if the rates are up-to-date.",
                                                  "Check Rates",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                vars.InstantiateRates(); //replace this later

                //TODO: ACTIVATE THIS WHEN ITS READY
                //vars.io.UploadRatesSheet();
            }
            else if (result == DialogResult.No)
            {
                MessageBox.Show("Rates confirmed. Proceeding with loading rates.");
                vars.InstantiateVars(); //replace this later

                vars.EnableBusRateCheckBox(); //put this in the logic code below once implemented
                //vars.CheckSetModeCompletionState(); //this too

                // Proceed with loading rates
            }
        }

        #endregion

        #region BUS RATES BUTTON AND CODE
        private void busRateButton_Click(object sender, EventArgs e)
        {
            vars.io.UploadRatesSheet();
        }
        #endregion

        #region DEMANDS BUTTONS AND CODE
        private void checkEditDemandButton_Click(object sender, EventArgs e)
        {
            DeptsCheckForm deptsCheckForm = new DeptsCheckForm(vars);
            deptsCheckForm.ShowDialog();
        }

        private void uploadDemandButton_Click(object sender, EventArgs e)
        {
            // Determine the current demand mode: 1 = individual departments mode, 2 = total demand mode
            int demandMode = vars.GetDemandMode();

            // Total Demand Mode
            if (demandMode == 2)
            {
                vars.io.UploadTotalDemandSheet();

                if (!string.IsNullOrEmpty(vars.totalDemandFilePath))
                {
                    try
                    {
                        // Process the total demand spreadsheet
                        vars.ProcessTotalDemandSpreadsheet();

                        if (vars.totalDemands != null)
                        {
                            // Check if the total demands data was filled successfully
                            if (vars.totalDemands.IsDataFilled)
                            {
                                vars.SetDemandModeToComplete();
                                WriteLine("Demand data was successfully filled!");
                                UpdateDataGrid();
                            }
                            else
                            {
                                MessageBox.Show("Demand data could not be completely filled. Please check for any empty fields in the Excel file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("An error occurred: Demand data could not be processed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle the specific exception thrown by ProcessTotalDemandSpreadsheet
                        MessageBox.Show($"An error occurred while processing the spreadsheet: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else MessageBox.Show("No file selected. Please upload a valid demand sheet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Individual Department Mode
            else if (demandMode == 1)
            {
                // Upload department spreadsheet(s)
                List<string> selectedFilePaths = vars.io.UploadIndivDeptSpreadsheet();

                // Check if any files were selected
                if (selectedFilePaths != null && selectedFilePaths.Any())
                {
                    foreach (string filePath in selectedFilePaths)
                    {
                        // Process each file immediately
                        bool isDataFilled = vars.ProcessIndivDeptSpreadsheet(filePath, true);

                        if (isDataFilled) WriteLine($"Demand data was successfully filled for file: {Path.GetFileName(filePath)}");
                        else
                        {
                            string fileDemandErrorMsg = $"Demand data could not be completely filled for file: {Path.GetFileName(filePath)}. Please check for any empty fields.";
                            WriteLine(fileDemandErrorMsg);
                            MessageBox.Show(fileDemandErrorMsg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    //////////FILE PROCESSING DONE HERE


                    // After processing all files
                    int totalDepartments = vars.deptsAndDemands.Count;
                    int filledDepartments = vars.deptsAndDemands.Count(dept => dept.IsDataFilled);

                    //if incomplete demands checkbox is enabled in settings AND there is at least 1 filled department
                    if (vars.canAllocateWithIncompleteDepts)
                    {
                        if (filledDepartments > 0)
                        {
                            // Some departments have data filled, and allocations can proceed with incomplete departments
                            vars.SetDemandModeToComplete();

                            string incompleteDeptsModeIsCompletedMsg = $"Demand data was successfully filled for {filledDepartments} out of {totalDepartments} departments.";
                            MessageBox.Show(incompleteDeptsModeIsCompletedMsg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            WriteLine(incompleteDeptsModeIsCompletedMsg);

                            UpdateDataGrid();
                        }
                        else
                        {
                            WriteLine("No departments have been completed yet.");
                            vars.SetDemandModeToIncomplete(vars.GetDemandModeObject());
                        }
                    }
                    //If incomplete demands checkbox is disabled
                    else
                    {
                        //all departments have data filled
                        if (filledDepartments == totalDepartments)
                        {
                            vars.SetDemandModeToComplete();
                            MessageBox.Show("All demand data was successfully filled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            UpdateDataGrid();
                        }
                        else
                        {
                            // Not enough data to make allocations
                            WriteLine($"Not all departments' demands are completed yet. Keep uploading department spreadsheets or allocate more manually.");
                            vars.SetDemandModeToIncomplete(vars.GetDemandModeObject());
                        }
                    }
                }
                else MessageBox.Show("No files selected. Please upload valid department demand sheets.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("Unknown demand mode selected. Please contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //vars.OutputDemandsToDebugConsole();
        }
        #endregion

        #region Settings Button
        private void settingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(settings, this, vars);

            settingsForm.ShowDialog();
        }
        #endregion

        #region Clear Demand Data Button
        private void clearDemandDataButton_Click(object sender, EventArgs e)
        {
            vars.ClearDemandData();
        }
        #endregion

        private void generateAllocationsButton_Click(object sender, EventArgs e)
        {
            BusAllocator allocator = new BusAllocator(vars, vars.GetDemandMode());
        }
    }
}
