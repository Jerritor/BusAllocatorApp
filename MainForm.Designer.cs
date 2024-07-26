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
            setSecondDateButton = new Button();
            editSecondDateButton = new Button();
            setFirstDateButton = new Button();
            editFirstDateButton = new Button();
            label1 = new Label();
            panel1 = new Panel();
            secondDatePicker = new DateTimePicker();
            secondDateCheckBox = new CheckBox();
            firstDateCheckBox = new CheckBox();
            deptsCheckBox = new CheckBox();
            busRateCheckBox = new CheckBox();
            firstDatePicker = new DateTimePicker();
            busRateButton = new Button();
            uploadDemandButton = new Button();
            checkEditDemandButton = new Button();
            dataGridView1 = new DataGridView();
            tableLayoutPanel2 = new TableLayoutPanel();
            settingsButton = new Button();
            generateDemandsButton = new Button();
            generateAllocationsButton = new Button();
            outputLog = new TextBox();
            label3 = new Label();
            label2 = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            folderBrowserDialog1 = new FolderBrowserDialog();
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
            tableLayoutPanel1.Controls.Add(setSecondDateButton, 2, 4);
            tableLayoutPanel1.Controls.Add(editSecondDateButton, 1, 4);
            tableLayoutPanel1.Controls.Add(setFirstDateButton, 2, 3);
            tableLayoutPanel1.Controls.Add(editFirstDateButton, 1, 3);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(busRateButton, 1, 1);
            tableLayoutPanel1.Controls.Add(uploadDemandButton, 1, 2);
            tableLayoutPanel1.Controls.Add(checkEditDemandButton, 2, 2);
            tableLayoutPanel1.Controls.Add(dataGridView1, 0, 6);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 7);
            tableLayoutPanel1.Controls.Add(outputLog, 0, 9);
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
            // setSecondDateButton
            // 
            setSecondDateButton.BackColor = SystemColors.ActiveCaptionText;
            setSecondDateButton.Dock = DockStyle.Left;
            setSecondDateButton.Enabled = false;
            setSecondDateButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            setSecondDateButton.ForeColor = SystemColors.ControlLightLight;
            setSecondDateButton.Location = new Point(648, 166);
            setSecondDateButton.Name = "setSecondDateButton";
            setSecondDateButton.Size = new Size(245, 40);
            setSecondDateButton.TabIndex = 13;
            setSecondDateButton.Text = "Set Date";
            setSecondDateButton.UseVisualStyleBackColor = false;
            setSecondDateButton.Visible = false;
            setSecondDateButton.Click += setSecondDateButton_Click;
            // 
            // editSecondDateButton
            // 
            editSecondDateButton.BackColor = SystemColors.ControlText;
            editSecondDateButton.Dock = DockStyle.Fill;
            editSecondDateButton.Enabled = false;
            editSecondDateButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            editSecondDateButton.ForeColor = SystemColors.ControlLightLight;
            editSecondDateButton.Location = new Point(400, 166);
            editSecondDateButton.Name = "editSecondDateButton";
            editSecondDateButton.Size = new Size(242, 40);
            editSecondDateButton.TabIndex = 10;
            editSecondDateButton.Text = "Edit Date";
            editSecondDateButton.UseVisualStyleBackColor = false;
            editSecondDateButton.Visible = false;
            editSecondDateButton.Click += editSecondDateButton_Click;
            // 
            // setFirstDateButton
            // 
            setFirstDateButton.BackColor = SystemColors.ControlText;
            setFirstDateButton.Dock = DockStyle.Left;
            setFirstDateButton.Enabled = false;
            setFirstDateButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            setFirstDateButton.ForeColor = SystemColors.ControlLightLight;
            setFirstDateButton.Location = new Point(648, 120);
            setFirstDateButton.Name = "setFirstDateButton";
            setFirstDateButton.Size = new Size(245, 40);
            setFirstDateButton.TabIndex = 12;
            setFirstDateButton.Text = "Set Date";
            setFirstDateButton.UseVisualStyleBackColor = false;
            setFirstDateButton.Visible = false;
            setFirstDateButton.Click += setFirstDateButton_Click;
            // 
            // editFirstDateButton
            // 
            editFirstDateButton.BackColor = SystemColors.ControlText;
            editFirstDateButton.Dock = DockStyle.Fill;
            editFirstDateButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            editFirstDateButton.ForeColor = SystemColors.ControlLightLight;
            editFirstDateButton.Location = new Point(400, 120);
            editFirstDateButton.Name = "editFirstDateButton";
            editFirstDateButton.Size = new Size(242, 40);
            editFirstDateButton.TabIndex = 9;
            editFirstDateButton.Text = "Edit Date";
            editFirstDateButton.UseVisualStyleBackColor = false;
            editFirstDateButton.Click += editFirstDateButton_Click;
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
            panel1.Controls.Add(secondDatePicker);
            panel1.Controls.Add(secondDateCheckBox);
            panel1.Controls.Add(firstDateCheckBox);
            panel1.Controls.Add(deptsCheckBox);
            panel1.Controls.Add(busRateCheckBox);
            panel1.Controls.Add(firstDatePicker);
            panel1.Location = new Point(3, 28);
            panel1.Name = "panel1";
            tableLayoutPanel1.SetRowSpan(panel1, 4);
            panel1.Size = new Size(391, 178);
            panel1.TabIndex = 7;
            // 
            // secondDatePicker
            // 
            secondDatePicker.Checked = false;
            secondDatePicker.CustomFormat = "";
            secondDatePicker.Enabled = false;
            secondDatePicker.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            secondDatePicker.Location = new Point(115, 145);
            secondDatePicker.Name = "secondDatePicker";
            secondDatePicker.Size = new Size(268, 29);
            secondDatePicker.TabIndex = 13;
            secondDatePicker.ValueChanged += secondDatePicker_ValueChanged;
            // 
            // secondDateCheckBox
            // 
            secondDateCheckBox.AutoCheck = false;
            secondDateCheckBox.AutoSize = true;
            secondDateCheckBox.Cursor = Cursors.No;
            secondDateCheckBox.FlatStyle = FlatStyle.Flat;
            secondDateCheckBox.Font = new Font("Segoe UI", 12F);
            secondDateCheckBox.Location = new Point(2, 145);
            secondDateCheckBox.Name = "secondDateCheckBox";
            secondDateCheckBox.Size = new Size(116, 25);
            secondDateCheckBox.TabIndex = 3;
            secondDateCheckBox.Text = "Second Date:";
            secondDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // firstDateCheckBox
            // 
            firstDateCheckBox.AutoCheck = false;
            firstDateCheckBox.AutoSize = true;
            firstDateCheckBox.Cursor = Cursors.No;
            firstDateCheckBox.FlatStyle = FlatStyle.Flat;
            firstDateCheckBox.Font = new Font("Segoe UI", 12F);
            firstDateCheckBox.Location = new Point(3, 99);
            firstDateCheckBox.Name = "firstDateCheckBox";
            firstDateCheckBox.Size = new Size(95, 25);
            firstDateCheckBox.TabIndex = 2;
            firstDateCheckBox.Text = "First Date:";
            firstDateCheckBox.TextAlign = ContentAlignment.MiddleRight;
            firstDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // deptsCheckBox
            // 
            deptsCheckBox.AutoCheck = false;
            deptsCheckBox.AutoSize = true;
            deptsCheckBox.Cursor = Cursors.No;
            deptsCheckBox.FlatStyle = FlatStyle.Flat;
            deptsCheckBox.Font = new Font("Segoe UI", 12F);
            deptsCheckBox.Location = new Point(3, 53);
            deptsCheckBox.Name = "deptsCheckBox";
            deptsCheckBox.Size = new Size(201, 25);
            deptsCheckBox.TabIndex = 1;
            deptsCheckBox.Text = "All Department Demands";
            deptsCheckBox.UseVisualStyleBackColor = true;
            // 
            // busRateCheckBox
            // 
            busRateCheckBox.AutoCheck = false;
            busRateCheckBox.AutoSize = true;
            busRateCheckBox.Cursor = Cursors.No;
            busRateCheckBox.FlatStyle = FlatStyle.Flat;
            busRateCheckBox.Font = new Font("Segoe UI", 12F);
            busRateCheckBox.Location = new Point(3, 7);
            busRateCheckBox.Name = "busRateCheckBox";
            busRateCheckBox.Size = new Size(254, 25);
            busRateCheckBox.TabIndex = 0;
            busRateCheckBox.Text = "Bus Rates Spreadsheet Uploaded";
            busRateCheckBox.UseVisualStyleBackColor = true;
            // 
            // firstDatePicker
            // 
            firstDatePicker.Checked = false;
            firstDatePicker.CustomFormat = "";
            firstDatePicker.Enabled = false;
            firstDatePicker.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            firstDatePicker.Location = new Point(115, 95);
            firstDatePicker.Name = "firstDatePicker";
            firstDatePicker.Size = new Size(268, 29);
            firstDatePicker.TabIndex = 12;
            // 
            // busRateButton
            // 
            busRateButton.BackColor = SystemColors.ControlText;
            tableLayoutPanel1.SetColumnSpan(busRateButton, 2);
            busRateButton.Dock = DockStyle.Left;
            busRateButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            busRateButton.ForeColor = SystemColors.ControlLightLight;
            busRateButton.Location = new Point(400, 28);
            busRateButton.Name = "busRateButton";
            busRateButton.Size = new Size(493, 40);
            busRateButton.TabIndex = 0;
            busRateButton.Text = "Upload Bus Rates Spreadsheet";
            busRateButton.UseVisualStyleBackColor = false;
            // 
            // uploadDemandButton
            // 
            uploadDemandButton.BackColor = SystemColors.ControlText;
            uploadDemandButton.Dock = DockStyle.Fill;
            uploadDemandButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            uploadDemandButton.ForeColor = SystemColors.ControlLightLight;
            uploadDemandButton.Location = new Point(400, 74);
            uploadDemandButton.Name = "uploadDemandButton";
            uploadDemandButton.Size = new Size(242, 40);
            uploadDemandButton.TabIndex = 8;
            uploadDemandButton.Text = "Upload Demand Spreadsheet";
            uploadDemandButton.UseVisualStyleBackColor = false;
            // 
            // checkEditDemandButton
            // 
            checkEditDemandButton.BackColor = SystemColors.ControlText;
            checkEditDemandButton.Dock = DockStyle.Left;
            checkEditDemandButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            checkEditDemandButton.ForeColor = SystemColors.ControlLightLight;
            checkEditDemandButton.Location = new Point(648, 74);
            checkEditDemandButton.Name = "checkEditDemandButton";
            checkEditDemandButton.Size = new Size(245, 40);
            checkEditDemandButton.TabIndex = 11;
            checkEditDemandButton.Text = "Check/Edit Demands";
            checkEditDemandButton.UseVisualStyleBackColor = false;
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
            tableLayoutPanel2.Controls.Add(settingsButton, 0, 0);
            tableLayoutPanel2.Controls.Add(generateDemandsButton, 1, 0);
            tableLayoutPanel2.Controls.Add(generateAllocationsButton, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Left;
            tableLayoutPanel2.Location = new Point(3, 291);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(890, 44);
            tableLayoutPanel2.TabIndex = 16;
            // 
            // settingsButton
            // 
            settingsButton.BackColor = SystemColors.ControlText;
            settingsButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            settingsButton.ForeColor = SystemColors.ControlLightLight;
            settingsButton.Location = new Point(3, 3);
            settingsButton.Name = "settingsButton";
            settingsButton.Size = new Size(202, 40);
            settingsButton.TabIndex = 0;
            settingsButton.Text = "Settings";
            settingsButton.UseVisualStyleBackColor = false;
            // 
            // generateDemandsButton
            // 
            generateDemandsButton.BackColor = SystemColors.ControlText;
            generateDemandsButton.Dock = DockStyle.Fill;
            generateDemandsButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            generateDemandsButton.ForeColor = SystemColors.ControlLightLight;
            generateDemandsButton.Location = new Point(211, 3);
            generateDemandsButton.Name = "generateDemandsButton";
            generateDemandsButton.Size = new Size(335, 41);
            generateDemandsButton.TabIndex = 1;
            generateDemandsButton.Text = "Generate Demands Spreadsheet";
            generateDemandsButton.UseVisualStyleBackColor = false;
            // 
            // generateAllocationsButton
            // 
            generateAllocationsButton.BackColor = SystemColors.ControlText;
            generateAllocationsButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            generateAllocationsButton.ForeColor = SystemColors.ControlLightLight;
            generateAllocationsButton.Location = new Point(552, 3);
            generateAllocationsButton.Name = "generateAllocationsButton";
            generateAllocationsButton.Size = new Size(335, 41);
            generateAllocationsButton.TabIndex = 2;
            generateAllocationsButton.Text = "Generate Allocations Spreadsheet";
            generateAllocationsButton.UseVisualStyleBackColor = false;
            generateAllocationsButton.Visible = false;
            // 
            // outputLog
            // 
            tableLayoutPanel1.SetColumnSpan(outputLog, 3);
            outputLog.Font = new Font("Segoe UI", 10F);
            outputLog.Location = new Point(3, 366);
            outputLog.Multiline = true;
            outputLog.Name = "outputLog";
            outputLog.ReadOnly = true;
            outputLog.ScrollBars = ScrollBars.Vertical;
            outputLog.Size = new Size(887, 200);
            outputLog.TabIndex = 1;
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
            ClientSize = new Size(1200, 777);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "MainForm";
            Text = "Bus Allocator";
            Load += MainForm_Load;
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
        private Button busRateButton;
        private Label label1;
        private Panel panel1;
        private CheckBox secondDateCheckBox;
        private CheckBox firstDateCheckBox;
        private CheckBox deptsCheckBox;
        private CheckBox busRateCheckBox;
        private Button uploadDemandButton;
        private Button editSecondDateButton;
        private Button checkEditDemandButton;
        private DateTimePicker secondDatePicker;
        private DateTimePicker firstDatePicker;
        private Button editFirstDateButton;
        private Button setFirstDateButton;
        private Button setSecondDateButton;
        private DataGridView dataGridView1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel2;
        private Button settingsButton;
        private Button generateDemandsButton;
        private Button generateAllocationsButton;
        private Label label3;
        private TextBox outputLog;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}
