namespace BusAllocatorApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            tableLayoutPanel1 = new TableLayoutPanel();
            button7 = new Button();
            button4 = new Button();
            button6 = new Button();
            button3 = new Button();
            label1 = new Label();
            panel1 = new Panel();
            dateTimePicker2 = new DateTimePicker();
            checkBox4 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            dateTimePicker1 = new DateTimePicker();
            button1 = new Button();
            button2 = new Button();
            button5 = new Button();
            dataGridView1 = new DataGridView();
            tableLayoutPanel2 = new TableLayoutPanel();
            button8 = new Button();
            button9 = new Button();
            button10 = new Button();
            textBox1 = new TextBox();
            label3 = new Label();
            label2 = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(button7, 2, 4);
            tableLayoutPanel1.Controls.Add(button4, 1, 4);
            tableLayoutPanel1.Controls.Add(button6, 2, 3);
            tableLayoutPanel1.Controls.Add(button3, 1, 3);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(button1, 1, 1);
            tableLayoutPanel1.Controls.Add(button2, 1, 2);
            tableLayoutPanel1.Controls.Add(button5, 2, 2);
            tableLayoutPanel1.Controls.Add(dataGridView1, 0, 6);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 7);
            tableLayoutPanel1.Controls.Add(textBox1, 0, 9);
            tableLayoutPanel1.Controls.Add(label3, 1, 8);
            tableLayoutPanel1.Controls.Add(label2, 1, 5);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 11;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(896, 583);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // button7
            // 
            button7.BackColor = SystemColors.ActiveCaptionText;
            button7.Dock = DockStyle.Left;
            button7.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button7.ForeColor = SystemColors.ControlLightLight;
            button7.Location = new Point(648, 166);
            button7.Name = "button7";
            button7.Size = new Size(245, 40);
            button7.TabIndex = 13;
            button7.Text = "Set Date";
            button7.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ControlText;
            button4.Dock = DockStyle.Fill;
            button4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button4.ForeColor = SystemColors.ControlLightLight;
            button4.Location = new Point(400, 166);
            button4.Name = "button4";
            button4.Size = new Size(242, 40);
            button4.TabIndex = 10;
            button4.Text = "Edit Date";
            button4.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            button6.BackColor = SystemColors.ControlText;
            button6.Dock = DockStyle.Left;
            button6.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button6.ForeColor = SystemColors.ControlLightLight;
            button6.Location = new Point(648, 120);
            button6.Name = "button6";
            button6.Size = new Size(245, 40);
            button6.TabIndex = 12;
            button6.Text = "Set Date";
            button6.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.ControlText;
            button3.Dock = DockStyle.Fill;
            button3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button3.ForeColor = SystemColors.ControlLightLight;
            button3.Location = new Point(400, 120);
            button3.Name = "button3";
            button3.Size = new Size(242, 40);
            button3.TabIndex = 9;
            button3.Text = "Edit Date";
            button3.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13F, FontStyle.Bold | FontStyle.Underline);
            label1.Location = new Point(51, 0);
            label1.Name = "label1";
            label1.Size = new Size(294, 25);
            label1.TabIndex = 6;
            label1.Text = "Allocator Requirements Checklist";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(dateTimePicker2);
            panel1.Controls.Add(checkBox4);
            panel1.Controls.Add(checkBox3);
            panel1.Controls.Add(checkBox2);
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(dateTimePicker1);
            panel1.Location = new Point(3, 28);
            panel1.Name = "panel1";
            tableLayoutPanel1.SetRowSpan(panel1, 4);
            panel1.Size = new Size(391, 178);
            panel1.TabIndex = 7;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Checked = false;
            dateTimePicker2.Enabled = false;
            dateTimePicker2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dateTimePicker2.Location = new Point(115, 141);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(268, 29);
            dateTimePicker2.TabIndex = 13;
            // 
            // checkBox4
            // 
            checkBox4.AutoCheck = false;
            checkBox4.AutoSize = true;
            checkBox4.Cursor = Cursors.No;
            checkBox4.FlatStyle = FlatStyle.Flat;
            checkBox4.Font = new Font("Segoe UI", 12F);
            checkBox4.Location = new Point(3, 145);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(116, 25);
            checkBox4.TabIndex = 3;
            checkBox4.Text = "Second Date:";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoCheck = false;
            checkBox3.AutoSize = true;
            checkBox3.Cursor = Cursors.No;
            checkBox3.FlatStyle = FlatStyle.Flat;
            checkBox3.Font = new Font("Segoe UI", 12F);
            checkBox3.Location = new Point(3, 99);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(95, 25);
            checkBox3.TabIndex = 2;
            checkBox3.Text = "First Date:";
            checkBox3.TextAlign = ContentAlignment.MiddleRight;
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoCheck = false;
            checkBox2.AutoSize = true;
            checkBox2.Cursor = Cursors.No;
            checkBox2.FlatStyle = FlatStyle.Flat;
            checkBox2.Font = new Font("Segoe UI", 12F);
            checkBox2.Location = new Point(3, 53);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(201, 25);
            checkBox2.TabIndex = 1;
            checkBox2.Text = "All Department Demands";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoCheck = false;
            checkBox1.AutoSize = true;
            checkBox1.Cursor = Cursors.No;
            checkBox1.FlatStyle = FlatStyle.Flat;
            checkBox1.Font = new Font("Segoe UI", 12F);
            checkBox1.Location = new Point(3, 7);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(254, 25);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "Bus Rates Spreadsheet Uploaded";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Checked = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dateTimePicker1.Location = new Point(115, 95);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(268, 29);
            dateTimePicker1.TabIndex = 12;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ControlText;
            tableLayoutPanel1.SetColumnSpan(button1, 2);
            button1.Dock = DockStyle.Left;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button1.ForeColor = SystemColors.ControlLightLight;
            button1.Location = new Point(400, 28);
            button1.Name = "button1";
            button1.Size = new Size(493, 40);
            button1.TabIndex = 0;
            button1.Text = "Upload Bus Rates Spreadsheet";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ControlText;
            button2.Dock = DockStyle.Fill;
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button2.ForeColor = SystemColors.ControlLightLight;
            button2.Location = new Point(400, 74);
            button2.Name = "button2";
            button2.Size = new Size(242, 40);
            button2.TabIndex = 8;
            button2.Text = "Upload Demand Spreadsheet";
            button2.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.ControlText;
            button5.Dock = DockStyle.Left;
            button5.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button5.ForeColor = SystemColors.ControlLightLight;
            button5.Location = new Point(648, 74);
            button5.Name = "button5";
            button5.Size = new Size(245, 40);
            button5.TabIndex = 11;
            button5.Text = "Check/Edit Demands";
            button5.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel1.SetColumnSpan(dataGridView1, 3);
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 237);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(1529, 48);
            dataGridView1.TabIndex = 14;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 3);
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(button8, 0, 0);
            tableLayoutPanel2.Controls.Add(button9, 1, 0);
            tableLayoutPanel2.Controls.Add(button10, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Left;
            tableLayoutPanel2.Location = new Point(3, 291);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(890, 44);
            tableLayoutPanel2.TabIndex = 16;
            // 
            // button8
            // 
            button8.BackColor = SystemColors.ControlText;
            button8.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button8.ForeColor = SystemColors.ControlLightLight;
            button8.Location = new Point(3, 3);
            button8.Name = "button8";
            button8.Size = new Size(202, 40);
            button8.TabIndex = 0;
            button8.Text = "Settings";
            button8.UseVisualStyleBackColor = false;
            // 
            // button9
            // 
            button9.BackColor = SystemColors.ControlText;
            button9.Dock = DockStyle.Fill;
            button9.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button9.ForeColor = SystemColors.ControlLightLight;
            button9.Location = new Point(211, 3);
            button9.Name = "button9";
            button9.Size = new Size(335, 41);
            button9.TabIndex = 1;
            button9.Text = "Generate Demands Spreadsheet";
            button9.UseVisualStyleBackColor = false;
            // 
            // button10
            // 
            button10.BackColor = SystemColors.ControlText;
            button10.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button10.ForeColor = SystemColors.ControlLightLight;
            button10.Location = new Point(552, 3);
            button10.Name = "button10";
            button10.Size = new Size(335, 41);
            button10.TabIndex = 2;
            button10.Text = "Generate Allocations Spreadsheet";
            button10.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            tableLayoutPanel1.SetColumnSpan(textBox1, 3);
            textBox1.Font = new Font("Segoe UI", 10F);
            textBox1.Location = new Point(3, 366);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(887, 200);
            textBox1.TabIndex = 1;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label3, 3);
            label3.Font = new Font("Segoe UI", 13F, FontStyle.Bold | FontStyle.Underline);
            label3.Location = new Point(713, 338);
            label3.Name = "label3";
            label3.Size = new Size(109, 25);
            label3.TabIndex = 0;
            label3.Text = "Output Log";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label2, 3);
            label2.Font = new Font("Segoe UI", 13F, FontStyle.Bold | FontStyle.Underline);
            label2.Location = new Point(662, 209);
            label2.Name = "label2";
            label2.Size = new Size(211, 25);
            label2.TabIndex = 15;
            label2.Text = "Total Rider Information";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1200, 781);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "MainForm";
            Text = "Bus Allocator";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button button1;
        private Label label1;
        private Panel panel1;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Button button2;
        private Button button4;
        private Button button5;
        private DateTimePicker dateTimePicker2;
        private DateTimePicker dateTimePicker1;
        private Button button3;
        private Button button6;
        private Button button7;
        private DataGridView dataGridView1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel2;
        private Button button8;
        private Button button9;
        private Button button10;
        private Label label3;
        private TextBox textBox1;
    }
}
