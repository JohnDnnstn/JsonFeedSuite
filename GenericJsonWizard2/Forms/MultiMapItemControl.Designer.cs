namespace GenericJsonWizard.Forms
{
    partial class MultiMapItemControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            GrdMapping = new DataGridView();
            RtnRemoveMapping = new Button();
            ChkBackfillId = new CheckBox();
            TxtBackfillIdName = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GrdMapping).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(GrdMapping);
            panel1.Location = new Point(0, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(774, 275);
            panel1.TabIndex = 0;
            // 
            // GrdMapping
            // 
            GrdMapping.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GrdMapping.Dock = DockStyle.Fill;
            GrdMapping.Location = new Point(0, 0);
            GrdMapping.Name = "GrdMapping";
            GrdMapping.Size = new Size(774, 275);
            GrdMapping.TabIndex = 0;
            GrdMapping.CellFormatting += GrdMapping_CellFormatting;
            GrdMapping.CellParsing += GrdMapping_CellParsing;
            GrdMapping.DataError += GrdMapping_DataError;
            // 
            // RtnRemoveMapping
            // 
            RtnRemoveMapping.Location = new Point(777, 3);
            RtnRemoveMapping.Name = "RtnRemoveMapping";
            RtnRemoveMapping.Size = new Size(100, 42);
            RtnRemoveMapping.TabIndex = 2;
            RtnRemoveMapping.Text = "Remove this Mapping";
            RtnRemoveMapping.UseVisualStyleBackColor = true;
            RtnRemoveMapping.Click += BtnRemoveMapping_Click;
            // 
            // ChkBackfillId
            // 
            ChkBackfillId.AutoSize = true;
            ChkBackfillId.Location = new Point(3, 284);
            ChkBackfillId.Name = "ChkBackfillId";
            ChkBackfillId.Size = new Size(100, 19);
            ChkBackfillId.TabIndex = 3;
            ChkBackfillId.Text = "Has Backfill Id";
            ChkBackfillId.UseVisualStyleBackColor = true;
            ChkBackfillId.CheckedChanged += ChkBackfillId_CheckedChanged;
            // 
            // TxtBackfillIdName
            // 
            TxtBackfillIdName.Location = new Point(109, 282);
            TxtBackfillIdName.Name = "TxtBackfillIdName";
            TxtBackfillIdName.Size = new Size(224, 23);
            TxtBackfillIdName.TabIndex = 4;
            // 
            // MultiMapItemControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(TxtBackfillIdName);
            Controls.Add(ChkBackfillId);
            Controls.Add(RtnRemoveMapping);
            Controls.Add(panel1);
            Name = "MultiMapItemControl";
            Size = new Size(880, 320);
            Load += MultiMapItemControl_Load;
            Leave += MultiMapItemControl_Leave;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GrdMapping).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private DataGridView GrdMapping;
        private Button RtnRemoveMapping;
        private CheckBox ChkBackfillId;
        private TextBox TxtBackfillIdName;
    }
}
