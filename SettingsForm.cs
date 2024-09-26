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
            }
        }

        //total depts mode radio button
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (totalModeRadioButton.Checked)
            {
                settings.ToggleDemandMode(2);
            }
        }
    }
}
