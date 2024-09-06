using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusAllocatorApp
{
    public partial class DeptsCheckForm : Form
    {
        private Vars vars;
        private List<Department> departments;
        private List<TimeSet> timeSets;

        public DeptsCheckForm(Vars vars)
        {
            InitializeComponent();
            this.vars = vars;
            this.departments = vars.deptsAndDemands;
            this.timeSets = vars.timeSets;


            CreateDepartmentList();
            

            //Form Closing Event
            //this.FormClosing += DeptsCheckForm_FormClosing;
        }

        //Also checks/unchecks the boxG
        private int? GetTotalDemandAndCheckboxes(Department department)
        {
            int total = 0;

            foreach (var timeSetKey in department.DemandData.Keys)
            {
                foreach (var route in department.DemandData[timeSetKey].Keys)
                {
                    var demand = department.DemandData[timeSetKey][route];
                    if (!demand.HasValue)
                    {
                        return null;
                    }
                    total += demand.Value;
                }
            }
            return total;
        }

        private void EditDepartment(Department department)
        {
            var editForm = new EditDemandsForm(department, timeSets);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                // Handle any updates if needed, e.g., refreshing the list
                CreateDepartmentList();
            }

            // Open edit form or handle editing logic here
            //MessageBox.Show($"Editing department: {department.Name}");
        }

        private void CreateDepartmentList()
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var dept in departments)
            {
                int? totalDemand = GetTotalDemandAndCheckboxes(dept);
                dept.IsDataFilled = totalDemand.HasValue;
                
                Panel departmentPanel = new Panel
                {
                    Width = flowLayoutPanel1.ClientSize.Width - 20,
                    Height = 40,
                    Margin = new Padding(5),
                    AutoSize = true
                };
                string demandText = totalDemand.HasValue ? totalDemand.Value.ToString() : "Incomplete";

                CheckBox checkBox = new CheckBox
                {
                    Text = $"{dept.Name} - Total Demand: {demandText}",
                    Checked = dept.IsDataFilled,
                    Location = new Point(10, 10),
                    AutoSize = true,
                    AutoCheck = false
                };
                Button editButton = new Button
                {
                    Text = "Edit",
                    Width = 50,
                    Height = 25
                };
                editButton.Click += (sender, e) => EditDepartment(dept);

                // Add controls to the Panel
                departmentPanel.Controls.Add(checkBox);
                //departmentPanel.Controls.Add(label);
                departmentPanel.Controls.Add(editButton);

                // Position the editButton explicitly to the right of the panel
                editButton.Location = new Point(departmentPanel.Width - editButton.Width - 10, 10);

                // Ensure the label width does not overlap with the editButton
                //label.Width = editButton.Location.X - label.Location.X - 10;
                flowLayoutPanel1.Controls.Add(departmentPanel);
            }
            flowLayoutPanel1.Controls.Add(closeButton);
        }

        private void DeptsCheckForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Handle any cleanup or actions needed before the form closes
            // For example, you can prompt the user to save changes or confirm closing
            DialogResult result = MessageBox.Show("Are you sure you want to close?", "Confirm Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true; // Cancel the closing event if the user chooses No
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
