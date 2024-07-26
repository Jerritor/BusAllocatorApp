using System.Data;
using System.Windows.Forms;

namespace BusAllocatorApp
{
    public partial class MainForm : Form
    {
        private Vars vars;

        private bool isSecondDateSet = false;

        public MainForm()
        {
            InitializeComponent();

            //
            LoadData();
            ResizeFormToFitTableLayoutPanel();

            vars = new Vars(this);
        }

        private void LoadData()
        {
            // Create a DataTable
            DataTable table = new DataTable();

            // Add columns to the DataTable
            table.Columns.Add("Location", typeof(string));
            table.Columns.Add("OUT 4:00PM", typeof(int));
            table.Columns.Add("OUT 6:00PM", typeof(int));
            table.Columns.Add("OUT 7:00PM", typeof(int));
            table.Columns.Add("IN 7:00PM", typeof(int));
            table.Columns.Add("IN 10:00PM", typeof(int));
            table.Columns.Add("OUT 4:00AM", typeof(int));
            table.Columns.Add("OUT 7:00AM", typeof(int));
            table.Columns.Add("IN 7:00AM", typeof(int));

            // Add rows to the DataTable
            table.Rows.Add("Alabang", 3, 12, 8, 6, 0, 0, 6, 27);
            table.Rows.Add("Balibago", 85, 45, 135, 83, 0, 0, 83, 306);
            table.Rows.Add("Binan", 22, 2, 40, 23, 0, 0, 23, 88);
            table.Rows.Add("Carmona", 6, 7, 20, 6, 0, 0, 6, 40);
            table.Rows.Add("Cabuyao", 19, 15, 23, 13, 0, 0, 13, 69);
            table.Rows.Add("Calamba", 17, 46, 35, 25, 0, 0, 25, 110);


            // Set the DataSource of the DataGridView
            dataGridView1.DataSource = table;

            // Alternatively, you can create DataGridView columns and bind them to DataTable columns
            // This step is optional if you already set AutoGenerateColumns to true
            /**
            foreach (DataColumn column in table.Columns)
            {
                DataGridViewTextBoxColumn dgvColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = column.ColumnName,
                    Name = column.ColumnName,
                    HeaderText = column.ColumnName
                };
                dataGridView1.Columns.Add(dgvColumn);
            }
            **/

            ResizeDataGridView();
            FormatDataGridView();
        }

        private void ResizeDataGridView()
        {
            // Calculate the required size
            int totalWidth = dataGridView1.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
            int totalHeight = dataGridView1.Rows.GetRowsHeight(DataGridViewElementStates.Visible);// + dataGridView1.ColumnHeadersHeight;

            // Adjust the size of DataGridView
            dataGridView1.ClientSize = new Size(totalWidth, totalHeight);
        }

        private void FormatDataGridView()
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

        private void ResizeFormToFitTableLayoutPanel()
        {
            // Calculate the preferred size of the TableLayoutPanel
            Size preferredSize = tableLayoutPanel1.PreferredSize;

            // Set the size of the form to fit the TableLayoutPanel
            this.ClientSize = new Size(preferredSize.Width, preferredSize.Height);
        }

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

        //DATE PICKERS CODE
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

        private void secondDatePicker_ValueChanged(object sender, EventArgs e)
        {
            CheckSecondDate();
        }

        private void setSecondDateButton_Click(object sender, EventArgs e)
        {
            if (CheckSecondDate() )
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
    }
}
