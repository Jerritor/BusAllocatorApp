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

        public EditDemandsForm(Department department, List<TimeSet> timeSets)
        {
            InitializeComponent();
            this.department = department;
            this.timeSets = timeSets;

            SetupDemandsPanel();
        }

        private void SetupDemandsPanel()
        {
            dataGridViewDemands.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

            labelDepartment.Text = $"Department: {department.Name}";

            LoadDemands();

            dataGridViewDemands.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            //dataGridViewDemands.Row

            dataGridViewDemands.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewDemands.RowHeadersVisible = true; // Ensure row headers are visible
        }

        private void LoadDemands()
        {
            // Create a dictionary to map unique keys (time + isOutgoing) to formatted time strings
            var timeSetKeyMap = timeSets.ToDictionary(
                ts => $"{ts.Time.ToString(@"hh\:mm\:ss")}_{ts.IsOutgoing}",
                ts => ts.GetFormattedTimeINOUT());

            var formattedTimeSetKeys = timeSetKeyMap.Values.ToList();
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
                    var originalTimeKey = originalKey.Split('_')[0];

                    var demand = department.DemandData.ContainsKey(originalTimeKey) && department.DemandData[originalTimeKey].ContainsKey(route) ? department.DemandData[originalTimeKey][route] : null;

                    dataGridViewDemands.Rows[rowIndex].Cells[colIndex].Value = demand.HasValue ? demand.Value.ToString() : "";
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
            var timeSets = department.DemandData.Keys.ToList();

            foreach (DataGridViewRow row in dataGridViewDemands.Rows)
            {
                var route = row.Cells[0].Value.ToString();
                for (int i = 1; i < dataGridViewDemands.Columns.Count; i++)
                {
                    var timeSet = dataGridViewDemands.Columns[i].Name;
                    if (!department.DemandData.ContainsKey(timeSet))
                    {
                        department.DemandData[timeSet] = new Dictionary<string, int?>();
                    }

                    var cellValue = row.Cells[i].Value?.ToString();
                    if (int.TryParse(cellValue, out int demand))
                    {
                        department.DemandData[timeSet][route] = demand;
                    }
                    else
                    {
                        department.DemandData[timeSet][route] = null;
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
