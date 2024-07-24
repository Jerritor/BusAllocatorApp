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
            checklisttableLayoutPanel = new TableLayoutPanel();
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
            checklisttableLayoutPanel.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // checklisttableLayoutPanel
            // 
            checklisttableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            checklisttableLayoutPanel.ColumnCount = 3;
            checklisttableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            checklisttableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            checklisttableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 252F));
            checklisttableLayoutPanel.Controls.Add(button7, 2, 4);
            checklisttableLayoutPanel.Controls.Add(button4, 1, 4);
            checklisttableLayoutPanel.Controls.Add(button6, 2, 3);
            checklisttableLayoutPanel.Controls.Add(button3, 1, 3);
            checklisttableLayoutPanel.Controls.Add(label1, 0, 0);
            checklisttableLayoutPanel.Controls.Add(panel1, 0, 1);
            checklisttableLayoutPanel.Controls.Add(button1, 1, 1);
            checklisttableLayoutPanel.Controls.Add(button2, 1, 2);
            checklisttableLayoutPanel.Controls.Add(button5, 2, 2);
            checklisttableLayoutPanel.Location = new Point(12, 12);
            checklisttableLayoutPanel.Name = "checklisttableLayoutPanel";
            checklisttableLayoutPanel.RowCount = 6;
            checklisttableLayoutPanel.RowStyles.Add(new RowStyle());
            checklisttableLayoutPanel.RowStyles.Add(new RowStyle());
            checklisttableLayoutPanel.RowStyles.Add(new RowStyle());
            checklisttableLayoutPanel.RowStyles.Add(new RowStyle());
            checklisttableLayoutPanel.RowStyles.Add(new RowStyle());
            checklisttableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            checklisttableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            checklisttableLayoutPanel.Size = new Size(832, 496);
            checklisttableLayoutPanel.TabIndex = 0;
            // 
            // button7
            // 
            button7.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button7.Location = new Point(648, 166);
            button7.Name = "button7";
            button7.Size = new Size(181, 40);
            button7.TabIndex = 13;
            button7.Text = "Set Date";
            button7.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button4.Location = new Point(400, 166);
            button4.Name = "button4";
            button4.Size = new Size(242, 40);
            button4.TabIndex = 10;
            button4.Text = "Edit Date";
            button4.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button6.Location = new Point(648, 120);
            button6.Name = "button6";
            button6.Size = new Size(181, 40);
            button6.TabIndex = 12;
            button6.Text = "Set Date";
            button6.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.None;
            button3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button3.Location = new Point(400, 120);
            button3.Name = "button3";
            button3.Size = new Size(242, 40);
            button3.TabIndex = 9;
            button3.Text = "Edit Date";
            button3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
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
            checklisttableLayoutPanel.SetRowSpan(panel1, 4);
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
            dateTimePicker1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dateTimePicker1.Location = new Point(115, 95);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(268, 29);
            dateTimePicker1.TabIndex = 12;
            // 
            // button1
            // 
            checklisttableLayoutPanel.SetColumnSpan(button1, 2);
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button1.Location = new Point(400, 28);
            button1.Name = "button1";
            button1.Size = new Size(429, 40);
            button1.TabIndex = 0;
            button1.Text = "Upload Bus Rates Spreadsheet";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.None;
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button2.Location = new Point(400, 74);
            button2.Name = "button2";
            button2.Size = new Size(242, 40);
            button2.TabIndex = 8;
            button2.Text = "Upload Demand Spreadsheet";
            button2.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Anchor = AnchorStyles.Left;
            button5.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button5.Location = new Point(648, 74);
            button5.Name = "button5";
            button5.Size = new Size(181, 40);
            button5.TabIndex = 11;
            button5.Text = "Check/Edit Demands";
            button5.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 800);
            Controls.Add(checklisttableLayoutPanel);
            Name = "MainForm";
            Text = "Bus Allocator";
            checklisttableLayoutPanel.ResumeLayout(false);
            checklisttableLayoutPanel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel checklisttableLayoutPanel;
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
    }
}
