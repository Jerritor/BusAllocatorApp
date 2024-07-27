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
        private List<Department> departments;

        public DeptsCheckForm(List<Department> depts)
        {
            InitializeComponent();

            this.departments = depts;
        }

        private int GetTotalDemand(Department department)
        {
            int totalDemand = 0;
            foreach (var timeSet in department.DemandData)
            {
                foreach (var route in timeSet.Value)
                {
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
                    Location = new Point(10, 5),
                    Width = 20
                };

                Label label = new Label
                {
                    Text = $"{department.Name} - Total Demand: {GetTotalDemand(department)}",
                    Location = new Point(40, 5),
                    Width = 300
                };

                Button editButton = new Button
                {
                    Text = "Edit",
                    Location = new Point(350, 0),
                    Width = 50
                };
                editButton.Click += (sender, e) => EditDepartment(department);

                departmentPanel.Controls.Add(checkBox);
                departmentPanel.Controls.Add(label);
                departmentPanel.Controls.Add(editButton);

                flowLayoutPanel1.Controls.Add(departmentPanel);
            }

            Button closeButton = new Button
            {
                Text = "Close Window",
                Width = 200,
                Margin = new Padding(5)
            };
            closeButton.Click += (sender, e) => this.Close();

            flowLayoutPanel1.Controls.Add(closeButton);
        }
    }
}
