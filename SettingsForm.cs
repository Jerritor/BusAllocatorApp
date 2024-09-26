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
    public partial class SettingsForm : Form
    {
        Settings settings;
        MainForm mainForm;

        public SettingsForm(Settings settings, MainForm mainForm)
        {
            InitializeComponent();
            this.settings = settings;
            this.mainForm = mainForm;
        }

        //individual depts mode radio button
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (deptsModeRadioButton.Checked)
            {
                settings.ToggleDemandMode(1);
                modeDescriptionLabel.Text = "In this mode, you can upload and manage separate demand files for each department individually.\n" +
                                            "You must use the standard department template for each spreadsheet.";
                deptsModeRadioButton.ForeColor = Color.Black;
                totalModeRadioButton.ForeColor = Color.DimGray;
                mainForm.WriteLine("Demand Mode changed to 'Individual Departments Mode'. " +
                    "You can now upload multiple department spreadsheets or manually edit demands.");
            }
        }

        //total depts mode radio button
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (totalModeRadioButton.Checked)
            {
                settings.ToggleDemandMode(2);
                modeDescriptionLabel.Text = "In this mode, you can upload and manage a single spreadsheet containing the total demand for all departments combined.\n" +
                                            "You must use the standard total demand spreadsheet with only one sheet.";
                deptsModeRadioButton.ForeColor = Color.DimGray;
                totalModeRadioButton.ForeColor = Color.Black;
                mainForm.WriteLine("Demand Mode changed to 'Total Demand Mode'. " +
                    "You can now upload a single total demand spreadsheet. Demands cannot be manually edited.");
            }
        }

        private void closeWindowButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
