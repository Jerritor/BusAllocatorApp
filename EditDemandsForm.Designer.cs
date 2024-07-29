namespace BusAllocatorApp
{
    partial class EditDemandsForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            labelDepartment = new Label();
            buttonFillEmpty = new Button();
            dataGridViewDemands = new DataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            buttonCancel = new Button();
            buttonApplyChanges = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDemands).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // labelDepartment
            // 
            labelDepartment.Anchor = AnchorStyles.Left;
            labelDepartment.AutoSize = true;
            labelDepartment.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelDepartment.Location = new Point(3, 12);
            labelDepartment.Name = "labelDepartment";
            labelDepartment.Size = new Size(265, 21);
            labelDepartment.TabIndex = 0;
            labelDepartment.Text = "Department: Sales and Marketing";
            labelDepartment.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // buttonFillEmpty
            // 
            buttonFillEmpty.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonFillEmpty.BackColor = SystemColors.ControlText;
            buttonFillEmpty.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonFillEmpty.ForeColor = SystemColors.ControlLightLight;
            buttonFillEmpty.Location = new Point(516, 3);
            buttonFillEmpty.Name = "buttonFillEmpty";
            buttonFillEmpty.Size = new Size(275, 40);
            buttonFillEmpty.TabIndex = 1;
            buttonFillEmpty.Text = "Fill empty values with 0";
            buttonFillEmpty.UseVisualStyleBackColor = false;
            buttonFillEmpty.Click += buttonFillEmpty_Click;
            // 
            // dataGridViewDemands
            // 
            dataGridViewDemands.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewDemands.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewDemands.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDemands.Location = new Point(3, 53);
            dataGridViewDemands.Name = "dataGridViewDemands";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridViewDemands.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewDemands.Size = new Size(794, 277);
            dataGridViewDemands.TabIndex = 2;
            dataGridViewDemands.CellValidating += dataGridViewDemands_CellValidating;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(dataGridViewDemands, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.Size = new Size(800, 383);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(labelDepartment, 0, 0);
            tableLayoutPanel2.Controls.Add(buttonFillEmpty, 1, 0);
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(794, 44);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(buttonCancel, 0, 0);
            tableLayoutPanel3.Controls.Add(buttonApplyChanges, 1, 0);
            tableLayoutPanel3.Location = new Point(3, 336);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(794, 44);
            tableLayoutPanel3.TabIndex = 3;
            // 
            // buttonCancel
            // 
            buttonCancel.BackColor = SystemColors.ActiveCaptionText;
            buttonCancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonCancel.ForeColor = SystemColors.ControlLightLight;
            buttonCancel.Location = new Point(3, 3);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 38);
            buttonCancel.TabIndex = 0;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonApplyChanges
            // 
            buttonApplyChanges.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonApplyChanges.BackColor = SystemColors.ActiveCaptionText;
            buttonApplyChanges.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonApplyChanges.ForeColor = SystemColors.ControlLightLight;
            buttonApplyChanges.Location = new Point(654, 3);
            buttonApplyChanges.Name = "buttonApplyChanges";
            buttonApplyChanges.Size = new Size(137, 38);
            buttonApplyChanges.TabIndex = 1;
            buttonApplyChanges.Text = "Apply Changes";
            buttonApplyChanges.UseVisualStyleBackColor = false;
            buttonApplyChanges.Click += buttonApplyChanges_Click;
            // 
            // EditDemandsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(800, 383);
            Controls.Add(tableLayoutPanel1);
            Name = "EditDemandsForm";
            Text = "Editing Demands";
            ((System.ComponentModel.ISupportInitialize)dataGridViewDemands).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelDepartment;
        private Button buttonFillEmpty;
        private DataGridView dataGridViewDemands;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Button buttonCancel;
        private Button buttonApplyChanges;
    }
}