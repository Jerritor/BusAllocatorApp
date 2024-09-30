using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace BusAllocatorApp
{
    public partial class EditDemandsForm : Form
    {
        private Department department;
        private List<TimeSet> timeSets;


        private bool promptShown = false; // Flag to ensure the prompt appears only once

        public EditDemandsForm(Department department, List<TimeSet> timeSets)
        {
            InitializeComponent();
            this.department = department;
            this.timeSets = timeSets;

            SetupDemandsPanel();
            //CheckForEmptyCells();
        }

        private void SetupDemandsPanel()
        {
            dataGridViewDemands.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

            labelDepartment.Text = $"Department: {department.Name}";

            LoadDemands();

            dataGridViewDemands.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            dataGridViewDemands.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewDemands.RowHeadersVisible = true; // Ensure row headers are visible
        }

        private void LoadDemands()
        {
            // Create a mapping from TimeSetKey ("16:00:00_True") to formatted time strings ("OUT 4:00PM")
            var timeSetKeyMap = timeSets.ToDictionary(
                ts => $"{ts.Time.ToString(@"hh\:mm\:ss")}_{ts.IsOutgoing}",
                ts => ts.GetFormattedTimeINOUT());

            var formattedTimeSetKeys = timeSetKeyMap.Values.ToList();

            // Get list of routes
            var routes = department.DemandData.Values.SelectMany(d => d.Keys).Distinct().ToList();

            // Debug: Check the retrieved time sets and routes
            Debug.WriteLine("Formatted Time Sets: " + string.Join(", ", formattedTimeSetKeys));
            Debug.WriteLine("Routes: " + string.Join(", ", routes));

            dataGridViewDemands.ColumnCount = formattedTimeSetKeys.Count;
            dataGridViewDemands.RowCount = routes.Count;

            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle
            {
                Font = new Font(dataGridViewDemands.Font, FontStyle.Bold)
            };

            for (int i = 0; i < formattedTimeSetKeys.Count; i++)
            {
                var formattedTimeSetKey = formattedTimeSetKeys[i];
                dataGridViewDemands.Columns[i].Name = formattedTimeSetKey;
                dataGridViewDemands.Columns[i].HeaderCell.Style = headerStyle;
                dataGridViewDemands.Columns[i].HeaderText = formattedTimeSetKey;

                // Debug: Check the formatted time for columns
                Debug.WriteLine($"Column {i}: {formattedTimeSetKey}");
            }

            for (int rowIndex = 0; rowIndex < routes.Count; rowIndex++)
            {
                var route = routes[rowIndex];
                dataGridViewDemands.Rows[rowIndex].HeaderCell.Value = route;
                dataGridViewDemands.Rows[rowIndex].HeaderCell.Style = headerStyle;

                for (int colIndex = 0; colIndex < formattedTimeSetKeys.Count; colIndex++)
                {
                    var formattedTimeSetKey = formattedTimeSetKeys[colIndex];

                    // Convert the formatted key back to original key format
                    var originalKey = timeSetKeyMap.FirstOrDefault(kvp => kvp.Value == formattedTimeSetKey).Key;

                    if (!string.IsNullOrEmpty(originalKey))
                    {
                        if (department.DemandData.ContainsKey(originalKey) && department.DemandData[originalKey].ContainsKey(route))
                        {
                            var demand = department.DemandData[originalKey][route];
                            dataGridViewDemands.Rows[rowIndex].Cells[colIndex].Value = demand.HasValue ? demand.Value.ToString() : "";
                        }
                        else
                        {
                            dataGridViewDemands.Rows[rowIndex].Cells[colIndex].Value = "";
                        }
                    }
                    else
                    {
                        dataGridViewDemands.Rows[rowIndex].Cells[colIndex].Value = "";
                    }
                }
            }

            dataGridViewDemands.RowHeadersDefaultCellStyle = headerStyle;
        }


        private void buttonFillEmpty_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewDemands.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()))
                    {
                        cell.Value = "0";
                    }
                }
            }
        }

        private void buttonApplyChanges_Click(object sender, EventArgs e)
        {
            if (CheckForEmptyCells() && !promptShown)
            {
                promptShown = true;
                DialogResult result = MessageBox.Show("There are empty cells. Do you want to proceed with empty cells?", "Empty Cells Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    promptShown = false;
                    return; // Cancel saving if the user chooses No
                }
            }

            // Create a mapping from TimeSetKey ("16:00:00_True") to formatted time strings ("OUT 4:00PM")
            var timeSetKeyMap = timeSets.ToDictionary(
                ts => $"{ts.Time.ToString(@"hh\:mm\:ss")}_{ts.IsOutgoing}",
                ts => ts.GetFormattedTimeINOUT());

            // No longer need to clear old keys without suffix as all keys now include the suffix
            // department.DemandData.Clear();

            foreach (DataGridViewRow row in dataGridViewDemands.Rows)
            {
                var route = row.HeaderCell.Value.ToString();
                for (int i = 0; i < dataGridViewDemands.Columns.Count; i++)
                {
                    var formattedTimeSetKey = dataGridViewDemands.Columns[i].Name;

                    // Convert the formatted key back to original key format
                    var originalKey = timeSetKeyMap.FirstOrDefault(kvp => kvp.Value == formattedTimeSetKey).Key;

                    // Debug: Check if originalKey is found
                    Debug.WriteLine($"FormattedTimeSetKey: {formattedTimeSetKey}, OriginalKey: {originalKey}");

                    if (!string.IsNullOrEmpty(originalKey))
                    {
                        // Instantiate a new dictionary for the time key if it doesn't exist
                        if (!department.DemandData.ContainsKey(originalKey))
                        {
                            department.DemandData[originalKey] = new Dictionary<string, int?>();
                        }

                        var cellValue = row.Cells[i].Value?.ToString();

                        // Try to save demand per cell -- saves null otherwise
                        if (int.TryParse(cellValue, out int demand))
                        {
                            department.DemandData[originalKey][route] = demand;
                            Debug.WriteLine($"Saved Demand: {demand} for Route: {route}, OriginalKey: {originalKey}");
                        }
                        else
                        {
                            department.DemandData[originalKey][route] = null;
                            Debug.WriteLine($"Saved Demand: null for Route: {route}, OriginalKey: {originalKey}");
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"Error: originalKey is null for formattedTimeSetKey: {formattedTimeSetKey}");
                    }
                }
            }

            // Debug: Print the saved demand data
            foreach (var key in department.DemandData.Keys)
            {
                foreach (var route in department.DemandData[key].Keys)
                {
                    var demand = department.DemandData[key][route];
                    Debug.WriteLine($"Saved Data - Key: {key}, Route: {route}, Demand: {demand}");
                }
            }

            department.IsDataFilled = true;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dataGridViewDemands_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Check if the cell value is numeric, non-negative or empty
            if (dataGridViewDemands.Columns[e.ColumnIndex].Name != "Route")
            {
                string cellValue = Convert.ToString(e.FormattedValue);
                if (!string.IsNullOrEmpty(cellValue))
                {
                    if (!int.TryParse(cellValue, out int result) || result < 0)
                    {
                        e.Cancel = true;
                        MessageBox.Show("Please enter a non-negative whole number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private bool CheckForEmptyCells()
        {
            foreach (DataGridViewRow row in dataGridViewDemands.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()))
                    {
                        return true; // Return true if an empty cell is found
                    }
                }
            }
            return false;
        }

        //paste excel data functionality
        private void PasteClipboardData()
        {
            if (Clipboard.ContainsText())
            {
                string clipboardText = Clipboard.GetText();
                string[] lines = clipboardText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                int rowIndex = dataGridViewDemands.CurrentCell.RowIndex;
                int colIndex = dataGridViewDemands.CurrentCell.ColumnIndex;

                foreach (var line in lines)
                {
                    if (rowIndex >= dataGridViewDemands.RowCount || colIndex >= dataGridViewDemands.ColumnCount)
                        break;

                    string[] cells = line.Split('\t');
                    for (int i = 0; i < cells.Length; i++)
                    {
                        if (colIndex + i < dataGridViewDemands.ColumnCount)
                        {
                            dataGridViewDemands[colIndex + i, rowIndex].Value = cells[i];
                        }
                    }
                    rowIndex++;
                }
            }
        }

        private void dataGridViewDemands_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PasteClipboardData();
                e.Handled = true;
            }
        }
    }

}
