using System.Data;
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
            if (vars.totalDemands == null || vars.totalDemands.DemandData == null)
            {
                MessageBox.Show("No demand data available to display.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Add "Location" column
            //table.Columns.Add("Location", typeof(string));

            // Get all unique shifts from DemandData and order them as desired
            var shifts = vars.totalDemands.DemandData.Keys.OrderBy(s => s).ToList();

            // Add a column for each shift
            /**
            foreach (var shift in shifts)
            {
                table.Columns.Add(shift, typeof(int));
            }
            **/

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
            if (isSecondDateSet && !CheckFirstDate()) { return; }

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

            firstDateCheckBox.Checked = true;

            vars.firstDay = firstDatePicker.Value.Date;
            string formattedDate = firstDatePicker.Value.ToLongDateString();
            WriteLine("First date selected: " + formattedDate);
            //WriteLine(vars.firstDay.Value.ToString());
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

        private bool CheckFirstDate()
        {
            if (isSecondDateSet && (secondDatePicker.Value.Date <= firstDatePicker.Value.Date))
            {
                firstDateCheckBox.Checked = false;

                MessageBox.Show("The first date must be before the second date.",
                    "Invalid First Date",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
            else { return true; }
        }

        private bool CheckSecondDate()
        {
            if (secondDatePicker.Value.Date <= firstDatePicker.Value.Date)
            {
                secondDateCheckBox.Checked = false;

                MessageBox.Show("The second date must be after the first date.",
                    "Invalid Second Date",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
            else { return true; }
        }

        private void firstDatePicker_ValueChanged(object sender, EventArgs e)
        {
            CheckFirstDate();
        }

        private void secondDatePicker_ValueChanged(object sender, EventArgs e)
        {
            CheckSecondDate();
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

                secondDateCheckBox.Checked = true;

                isSecondDateSet = true;

                vars.secondDay = secondDatePicker.Value.Date;
                string formattedDate = secondDatePicker.Value.ToLongDateString();
                WriteLine("Second date selected: " + formattedDate);
                //WriteLine(vars.secondDay.Value.ToString());
            }
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
                }
                else
                {
                    ShowRatesChangedPopup();
                }
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
                vars.io.UploadRatesSheet();
            }
            else if (result == DialogResult.No)
            {
                MessageBox.Show("Rates confirmed. Proceeding with loading rates.");
                busRateCheckBox.Checked = true;
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
                    // Process the total demand spreadsheet
                    vars.ProcessTotalDemandSpreadsheet();

                    // Check if the total demands data was filled successfully
                    if (vars.totalDemands.IsDataFilled)
                    {
                        settings.SetDemandModeToComplete();
                        MessageBox.Show("Demand data was successfully filled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateDataGrid();
                    }
                    else MessageBox.Show("Demand data could not be completely filled. Please check for any empty fields in the Excel file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        bool isDataFilled = vars.ProcessIndivDeptSpreadsheet(filePath,true);
                        if (isDataFilled)
                        {
                            WriteLine($"Demand data was successfully filled for file: {Path.GetFileName(filePath)}");

                            //Disabled because grid should be updated only when all demands are set or when checkbox in settings is checked
                            //UpdateDataGrid(); // Update the grid after each successful upload
                        }
                        else
                        {
                            string fileDemandErrorMsg = $"Demand data could not be completely filled for file: {Path.GetFileName(filePath)}. Please check for any empty fields.";
                            WriteLine(fileDemandErrorMsg);
                            MessageBox.Show(fileDemandErrorMsg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            SettingsForm settingsForm = new SettingsForm(settings, this);

            settingsForm.ShowDialog();
        }
        #endregion

        #region Clear Demand Data Button
        private void clearDemandDataButton_Click(object sender, EventArgs e)
        {
            settings.ClearDemandData();
        }
        #endregion
    }
}
