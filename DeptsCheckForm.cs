﻿using System;
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
        private List<Department> departments;

        public DeptsCheckForm(List<Department> depts)
        {
            InitializeComponent();
            this.departments = depts;

            CreateDepartmentList();

            //Form Closing Event
            //this.FormClosing += DeptsCheckForm_FormClosing;
        }

        private int? GetTotalDemand(Department department)
        {
            int? totalDemand = 0;
            foreach (var timeSet in department.DemandData)
            {
                foreach (var route in timeSet.Value)
                {
                    //returns null if any demand value is null
                    if (!route.Value.HasValue)
                    {
                        return null;
                    }
                    totalDemand += route.Value;
                }
            }
            return totalDemand;
        }

        private void EditDepartment(Department department)
        {
            // Open edit form or handle editing logic here
            MessageBox.Show($"Editing department: {department.Name}");
        }

        private void CreateDepartmentList()
        {
            flowLayoutPanel1.Controls.Clear();

            /**
            foreach (var department in departments)
            {
                Panel departmentPanel = new Panel
                {
                    Width = flowLayoutPanel1.ClientSize.Width - 20,
                    Height = 30,
                    Margin = new Padding(5)
                };

                CheckBox checkBox = new CheckBox
                {
                    Checked = department.IsDataFilled,
                    AutoCheck = false,
                    Location = new Point(10, 5),
                    Width = 20
                };
                
                Button editButton = new Button
                {
                    Text = "Edit",
                    //Location = new Point(flowLayoutPanel1.ClientSize.Width - 80, 0), //new Point(350, 0),
                    Width = 50,
                    Anchor = AnchorStyles.Right
                };

                Label label = new Label
                {
                    Text = $"{department.Name} - Total Demand: {GetTotalDemand(department)}",
                    Location = new Point(40, 5),
                    Width = 300,
                    AutoSize = true
                };

                
                editButton.Click += (sender, e) => EditDepartment(department);

                departmentPanel.Controls.Add(checkBox);
                departmentPanel.Controls.Add(label);
                departmentPanel.Controls.Add(editButton);

                flowLayoutPanel1.Controls.Add(departmentPanel);
            }**/
            foreach (var department in departments)
            {
                Panel departmentPanel = new Panel
                {
                    Width = flowLayoutPanel1.ClientSize.Width - 20,
                    Height = 40,
                    Margin = new Padding(5),
                    AutoSize = true
                };

                int? totalDemand = GetTotalDemand(department);
                string demandText = totalDemand.HasValue ? totalDemand.Value.ToString() : "Incomplete";

                CheckBox checkBox = new CheckBox
                {
                    Text = $"{department.Name} - Total Demand: {demandText}",
                    Checked = department.IsDataFilled,
                    Location = new Point(10, 10),
                    AutoSize = true,
                    AutoCheck = false
                };       
                /**
                Label label = new Label
                {
                    Text = $"{department.Name} - Total Demand: {GetTotalDemand(department)}",
                    Location = new Point(40, 10),
                    AutoSize = true
                };**/
                Button editButton = new Button
                {
                    Text = "Edit",
                    Width = 50,
                    Height = 25
                };
                editButton.Click += (sender, e) => EditDepartment(department);

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
