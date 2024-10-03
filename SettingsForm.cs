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
        private bool isInitializing = true; // Flag to indicate initialization

        Settings settings;
        MainForm mainForm;

        public SettingsForm(Settings settings, MainForm mainForm)
        {
            InitializeComponent();
            this.settings = settings;
            this.mainForm = mainForm;

            SetRadioButtonsBasedOnMode();
            SetIncompleteAllocsCheckBox();
            isInitializing = false; //sets initializing to complete
        }

        #region RadioButtons

        /// <summary>
        /// Sets the RadioButtons based on the current demand mode.
        /// </summary>
        private void SetRadioButtonsBasedOnMode()
        {
            int demandMode = settings.vars.GetDemandMode();

            if (demandMode == 1) //indiv dept mode
            {
                deptsModeRadioButton.Checked = true;
                setVisualModeToIndivDepts();
            }
            else if (demandMode == 2) //total demand mode
            {
                setVisualModeToTotal();
                totalModeRadioButton.Checked = true;
            }
            else
            {
                // Handle unknown mode by defaulting to Individual Departments Mode
                deptsModeRadioButton.Checked = true;
            }
        }

        private void setVisualModeToIndivDepts()
        {
            incompleteAllocsCheckBox.Enabled = true;
            incompleteAllocsCheckBox.Visible = true;

            modeDescriptionLabel.Text = "In this mode, you can upload and manage separate demand files for each department individually.\n" +
                                            "You must use the standard department template for each spreadsheet.";
            deptsModeRadioButton.ForeColor = Color.Black;
            totalModeRadioButton.ForeColor = Color.DimGray;
            mainForm.checkEditDemandButton.Visible = true;
            mainForm.WriteLine("Demand Mode changed to 'Individual Departments Mode'. " +
                "You can now upload multiple department spreadsheets or manually edit demands.");
        }

        private void setVisualModeToTotal()
        {
            incompleteAllocsCheckBox.Enabled = false;
            incompleteAllocsCheckBox.Visible = false;

            modeDescriptionLabel.Text = "In this mode, you can upload and manage a single spreadsheet containing the total demand for all departments combined.\n" +
                                            "You must use the standard total demand spreadsheet with only one sheet.";
            deptsModeRadioButton.ForeColor = Color.DimGray;
            totalModeRadioButton.ForeColor = Color.Black;
            mainForm.checkEditDemandButton.Visible = false;
            mainForm.WriteLine("Demand Mode changed to 'Total Demand Mode'. " +
                "You can now upload a single total demand spreadsheet. Demands cannot be manually edited.");
        }

        //individual depts mode radio button
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (isInitializing) return; // Exit if the form is still initializing

            if (deptsModeRadioButton.Checked)
            {
                setVisualModeToIndivDepts();
                settings.ToggleDemandMode(1); //Updates mode flag

                SetIncompleteAllocsCheckBox();
            }
        }

        //total depts mode radio button
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (isInitializing) return; // Exit if the form is still initializing

            if (totalModeRadioButton.Checked)
            {
                setVisualModeToTotal();
                settings.ToggleDemandMode(2); //Updates mode flag

                SetIncompleteAllocsCheckBox();
            }
        }

        #endregion

        private void closeWindowButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Incomplete Allocations Checkbox
        /// <summary>
        /// Sets the incompleteAllocsCheckBox based on the current setting.
        /// </summary>
        private void SetIncompleteAllocsCheckBox()
        {
            // Only set the checkbox state if in Individual Department Mode
            int demandMode = settings.vars.GetDemandMode();

            if (demandMode == 1) incompleteAllocsCheckBox.Checked = settings.vars.canAllocateWithIncompeleteDepts;
            else incompleteAllocsCheckBox.Checked = false; // Default or irrelevant in Total Demand Mode so resets it
        }

        private void incompleteAllocsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (isInitializing) return; //exit if the form is still initializing

            //2nd parameter is if debug mode is on
            settings.SetIncompleteAllocs(incompleteAllocsCheckBox.Checked, true);

            settings.ClearDemandData();
        }

        #endregion
    }
}
