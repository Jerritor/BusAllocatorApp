namespace BusAllocatorApp
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            modeDescriptionLabel = new Label();
            deptsModeRadioButton = new RadioButton();
            totalModeRadioButton = new RadioButton();
            closeWindowButton = new Button();
            incompleteAllocsCheckBox = new CheckBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            label2 = new Label();
            label3 = new Label();
            this.allocationFolderTextBox = new TextBox();
            this.allocationFolderUploadButton = new Button();
            tableLayoutPanel4 = new TableLayoutPanel();
            allocationFileNameTextBox = new TextBox();
            label5 = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            bufferSizesButton = new Button();
            busRateButton = new Button();
            label4 = new Label();
            advancedSettingsButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(closeWindowButton, 0, 6);
            tableLayoutPanel1.Controls.Add(incompleteAllocsCheckBox, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 3);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 0, 4);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel5, 0, 5);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel6, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.Size = new Size(511, 471);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.BackColor = SystemColors.ControlLightLight;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(modeDescriptionLabel, 1, 1);
            tableLayoutPanel2.Controls.Add(deptsModeRadioButton, 0, 1);
            tableLayoutPanel2.Controls.Add(totalModeRadioButton, 0, 2);
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(505, 134);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(label1, 2);
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label1.Location = new Point(117, 0);
            label1.Name = "label1";
            label1.Size = new Size(271, 30);
            label1.TabIndex = 1;
            label1.Text = "Spreadsheet Import Mode";
            // 
            // modeDescriptionLabel
            // 
            modeDescriptionLabel.AutoSize = true;
            modeDescriptionLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            modeDescriptionLabel.Location = new Point(255, 30);
            modeDescriptionLabel.Name = "modeDescriptionLabel";
            tableLayoutPanel2.SetRowSpan(modeDescriptionLabel, 2);
            modeDescriptionLabel.Size = new Size(245, 85);
            modeDescriptionLabel.TabIndex = 2;
            modeDescriptionLabel.Text = "In this mode, you can upload and manage separate demand files for each department individually.\r\nYou must use the standard department template for each spreadsheet.";
            // 
            // deptsModeRadioButton
            // 
            deptsModeRadioButton.AutoSize = true;
            deptsModeRadioButton.Checked = true;
            deptsModeRadioButton.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            deptsModeRadioButton.Location = new Point(3, 33);
            deptsModeRadioButton.Name = "deptsModeRadioButton";
            deptsModeRadioButton.Size = new Size(246, 25);
            deptsModeRadioButton.TabIndex = 1;
            deptsModeRadioButton.TabStop = true;
            deptsModeRadioButton.Text = "Individual Departments Mode";
            deptsModeRadioButton.UseVisualStyleBackColor = true;
            deptsModeRadioButton.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // totalModeRadioButton
            // 
            totalModeRadioButton.AutoSize = true;
            totalModeRadioButton.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            totalModeRadioButton.ForeColor = Color.DimGray;
            totalModeRadioButton.Location = new Point(3, 85);
            totalModeRadioButton.Name = "totalModeRadioButton";
            totalModeRadioButton.Size = new Size(176, 25);
            totalModeRadioButton.TabIndex = 3;
            totalModeRadioButton.Text = "Total Demand Mode";
            totalModeRadioButton.UseVisualStyleBackColor = true;
            totalModeRadioButton.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // closeWindowButton
            // 
            closeWindowButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            closeWindowButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            closeWindowButton.Location = new Point(3, 438);
            closeWindowButton.Name = "closeWindowButton";
            closeWindowButton.Size = new Size(505, 34);
            closeWindowButton.TabIndex = 1;
            closeWindowButton.Text = "Close Window";
            closeWindowButton.UseVisualStyleBackColor = true;
            closeWindowButton.Click += closeWindowButton_Click;
            // 
            // incompleteAllocsCheckBox
            // 
            incompleteAllocsCheckBox.AutoSize = true;
            incompleteAllocsCheckBox.Location = new Point(3, 143);
            incompleteAllocsCheckBox.Name = "incompleteAllocsCheckBox";
            incompleteAllocsCheckBox.Size = new Size(83, 19);
            incompleteAllocsCheckBox.TabIndex = 2;
            incompleteAllocsCheckBox.Text = "checkBox1";
            incompleteAllocsCheckBox.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel3.Controls.Add(label2, 0, 0);
            tableLayoutPanel3.Controls.Add(label3, 0, 1);
            tableLayoutPanel3.Controls.Add(this.allocationFolderTextBox, 1, 1);
            tableLayoutPanel3.Controls.Add(this.allocationFolderUploadButton, 2, 1);
            tableLayoutPanel3.Location = new Point(3, 218);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(505, 94);
            tableLayoutPanel3.TabIndex = 5;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            tableLayoutPanel3.SetColumnSpan(label2, 3);
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(499, 15);
            label2.TabIndex = 0;
            label2.Text = "Output Folder Selection";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 15);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 1;
            label3.Text = "label3";
            // 
            // allocationFolderTextBox
            // 
            this.allocationFolderTextBox.Location = new Point(171, 18);
            this.allocationFolderTextBox.Name = "allocationFolderTextBox";
            this.allocationFolderTextBox.Size = new Size(100, 23);
            this.allocationFolderTextBox.TabIndex = 3;
            // 
            // allocationFolderUploadButton
            // 
            this.allocationFolderUploadButton.Location = new Point(339, 18);
            this.allocationFolderUploadButton.Name = "allocationFolderUploadButton";
            this.allocationFolderUploadButton.Size = new Size(75, 23);
            this.allocationFolderUploadButton.TabIndex = 4;
            this.allocationFolderUploadButton.Text = "button1";
            this.allocationFolderUploadButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(allocationFileNameTextBox, 1, 0);
            tableLayoutPanel4.Controls.Add(label5, 0, 0);
            tableLayoutPanel4.Location = new Point(3, 318);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(505, 74);
            tableLayoutPanel4.TabIndex = 6;
            // 
            // allocationFileNameTextBox
            // 
            allocationFileNameTextBox.Location = new Point(255, 3);
            allocationFileNameTextBox.Name = "allocationFileNameTextBox";
            allocationFileNameTextBox.Size = new Size(100, 23);
            allocationFileNameTextBox.TabIndex = 0;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 0);
            label5.Name = "label5";
            label5.Size = new Size(38, 15);
            label5.TabIndex = 2;
            label5.Text = "label5";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Controls.Add(label4, 0, 0);
            tableLayoutPanel5.Controls.Add(advancedSettingsButton, 1, 0);
            tableLayoutPanel5.Location = new Point(3, 398);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(200, 34);
            tableLayoutPanel5.TabIndex = 7;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 2;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Controls.Add(bufferSizesButton, 0, 0);
            tableLayoutPanel6.Controls.Add(busRateButton, 1, 0);
            tableLayoutPanel6.Location = new Point(3, 168);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(200, 44);
            tableLayoutPanel6.TabIndex = 8;
            // 
            // bufferSizesButton
            // 
            bufferSizesButton.Location = new Point(3, 3);
            bufferSizesButton.Name = "bufferSizesButton";
            bufferSizesButton.Size = new Size(75, 23);
            bufferSizesButton.TabIndex = 3;
            bufferSizesButton.Text = "button1";
            bufferSizesButton.UseVisualStyleBackColor = true;
            // 
            // busRateButton
            // 
            busRateButton.Location = new Point(103, 3);
            busRateButton.Name = "busRateButton";
            busRateButton.Size = new Size(75, 23);
            busRateButton.TabIndex = 4;
            busRateButton.Text = "button2";
            busRateButton.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 0);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 0;
            label4.Text = "label4";
            // 
            // advancedSettingsButton
            // 
            advancedSettingsButton.Location = new Point(103, 3);
            advancedSettingsButton.Name = "advancedSettingsButton";
            advancedSettingsButton.Size = new Size(75, 23);
            advancedSettingsButton.TabIndex = 1;
            advancedSettingsButton.Text = "button2";
            advancedSettingsButton.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(511, 471);
            Controls.Add(tableLayoutPanel1);
            Name = "SettingsForm";
            Text = "Settings";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private RadioButton deptsModeRadioButton;
        private RadioButton totalModeRadioButton;
        private Label modeDescriptionLabel;
        private Button closeWindowButton;
        private CheckBox incompleteAllocsCheckBox;
        private Button bufferSizesButton;
        private Button busRateButton;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel6;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private Button button2;
        private TextBox allocationFileNameTextBox;
        private TextBox textBox4;
        private Label label5;
        private Label label4;
        private Button advancedSettingsButton;
        private Label label6;
    }
}