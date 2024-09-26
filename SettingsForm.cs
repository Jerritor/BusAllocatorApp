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

        public SettingsForm(Settings settings)
        {
            InitializeComponent();
            this.settings = settings;
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
            }
        }

        //total depts mode radio button
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (totalModeRadioButton.Checked)
            {
                settings.ToggleDemandMode(2);
                modeDescriptionLabel.Text = "In this mode, you can upload and manage a single file containing the total demand for all departments combined.\n" +
                                            "You must use the standard spreadsheet with only one sheet.";
                deptsModeRadioButton.ForeColor = Color.DimGray;
                totalModeRadioButton.ForeColor = Color.Black;
            }
        }

        private void closeWindowButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
