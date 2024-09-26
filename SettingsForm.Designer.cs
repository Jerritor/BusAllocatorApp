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
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(closeWindowButton, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.Size = new Size(511, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
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
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(505, 144);
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
            totalModeRadioButton.Location = new Point(3, 98);
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
            closeWindowButton.Location = new Point(3, 173);
            closeWindowButton.Name = "closeWindowButton";
            closeWindowButton.Size = new Size(505, 34);
            closeWindowButton.TabIndex = 1;
            closeWindowButton.Text = "Close Window";
            closeWindowButton.UseVisualStyleBackColor = true;
            closeWindowButton.Click += closeWindowButton_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(511, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "SettingsForm";
            Text = "Settings";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
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
    }
}