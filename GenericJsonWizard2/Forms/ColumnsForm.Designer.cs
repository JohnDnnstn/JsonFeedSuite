namespace GenericJsonWizard.Forms
{
    partial class ColumnsForm
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
            BtnAnalyse = new Button();
            panel1 = new Panel();
            GrdColumns = new DataGridView();
            BtnRevert = new Button();
            ChkShowJsonEntities = new CheckBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GrdColumns).BeginInit();
            SuspendLayout();
            // 
            // BtnNext
            // 
            BtnNext.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            // 
            // BtnAnalyse
            // 
            BtnAnalyse.Location = new Point(12, 97);
            BtnAnalyse.Name = "BtnAnalyse";
            BtnAnalyse.Size = new Size(75, 23);
            BtnAnalyse.TabIndex = 100;
            BtnAnalyse.Text = "Analyse";
            BtnAnalyse.UseVisualStyleBackColor = true;
            BtnAnalyse.Click += BtnAnalyse_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(GrdColumns);
            panel1.Location = new Point(12, 126);
            panel1.Name = "panel1";
            panel1.Size = new Size(909, 359);
            panel1.TabIndex = 101;
            // 
            // GrdColumns
            // 
            GrdColumns.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GrdColumns.Dock = DockStyle.Fill;
            GrdColumns.Location = new Point(0, 0);
            GrdColumns.Name = "GrdColumns";
            GrdColumns.Size = new Size(909, 359);
            GrdColumns.TabIndex = 0;
            GrdColumns.CellDoubleClick += GrdColumns_CellDoubleClick;
            GrdColumns.UserAddedRow += GrdColumns_UserAddedRow;
            // 
            // BtnRevert
            // 
            BtnRevert.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnRevert.Enabled = false;
            BtnRevert.Location = new Point(846, 97);
            BtnRevert.Name = "BtnRevert";
            BtnRevert.Size = new Size(75, 23);
            BtnRevert.TabIndex = 102;
            BtnRevert.Text = "Revert";
            BtnRevert.UseVisualStyleBackColor = true;
            BtnRevert.Visible = false;
            BtnRevert.Click += BtnRevert_Click;
            // 
            // ChkShowJsonEntities
            // 
            ChkShowJsonEntities.AutoSize = true;
            ChkShowJsonEntities.Location = new Point(799, 72);
            ChkShowJsonEntities.Name = "ChkShowJsonEntities";
            ChkShowJsonEntities.Size = new Size(122, 19);
            ChkShowJsonEntities.TabIndex = 103;
            ChkShowJsonEntities.Text = "Show Json Entities";
            ChkShowJsonEntities.UseVisualStyleBackColor = true;
            ChkShowJsonEntities.CheckedChanged += ChkShowJsonEntities_CheckedChanged;
            // 
            // ColumnsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(ChkShowJsonEntities);
            Controls.Add(BtnRevert);
            Controls.Add(panel1);
            Controls.Add(BtnAnalyse);
            Name = "ColumnsForm";
            Text = "Columns";
            Load += ColumnsForm_Load;
            Controls.SetChildIndex(TxtRubric, 0);
            Controls.SetChildIndex(BtnBack, 0);
            Controls.SetChildIndex(BtnNext, 0);
            Controls.SetChildIndex(BtnAnalyse, 0);
            Controls.SetChildIndex(panel1, 0);
            Controls.SetChildIndex(BtnRevert, 0);
            Controls.SetChildIndex(ChkShowJsonEntities, 0);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GrdColumns).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnAnalyse;
        private Panel panel1;
        private Button BtnRevert;
        private DataGridView GrdColumns;
        private CheckBox ChkShowJsonEntities;
    }
}