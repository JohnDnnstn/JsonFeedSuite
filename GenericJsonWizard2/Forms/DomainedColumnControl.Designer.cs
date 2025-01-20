namespace GenericJsonWizard.Forms
{
    partial class DomainedColumnControl
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
            TxtBackfillIdName = new TextBox();
            LblBackfillIdName = new Label();
            ChkBackillId = new CheckBox();
            panel1 = new Panel();
            BtnRemoveDomainedColumn = new Button();
            LstColumns = new ListBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // TxtBackfillIdName
            // 
            TxtBackfillIdName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            TxtBackfillIdName.Location = new Point(196, 241);
            TxtBackfillIdName.Name = "TxtBackfillIdName";
            TxtBackfillIdName.Size = new Size(154, 23);
            TxtBackfillIdName.TabIndex = 7;
            // 
            // LblBackfillIdName
            // 
            LblBackfillIdName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            LblBackfillIdName.AutoSize = true;
            LblBackfillIdName.Location = new Point(84, 244);
            LblBackfillIdName.Name = "LblBackfillIdName";
            LblBackfillIdName.Size = new Size(106, 15);
            LblBackfillIdName.TabIndex = 6;
            LblBackfillIdName.Text = "Backfilled Id Name";
            // 
            // ChkBackillId
            // 
            ChkBackillId.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ChkBackillId.AutoSize = true;
            ChkBackillId.Location = new Point(4, 243);
            ChkBackillId.Name = "ChkBackillId";
            ChkBackillId.Size = new Size(77, 19);
            ChkBackillId.TabIndex = 5;
            ChkBackillId.Text = "Backfill Id";
            ChkBackillId.UseVisualStyleBackColor = true;
            ChkBackillId.CheckedChanged += ChkBackillId_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(BtnRemoveDomainedColumn);
            panel1.Controls.Add(LstColumns);
            panel1.Controls.Add(LblBackfillIdName);
            panel1.Controls.Add(TxtBackfillIdName);
            panel1.Controls.Add(ChkBackillId);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(606, 270);
            panel1.TabIndex = 8;
            // 
            // BtnRemoveDomainedColumn
            // 
            BtnRemoveDomainedColumn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnRemoveDomainedColumn.Location = new Point(515, 8);
            BtnRemoveDomainedColumn.Name = "BtnRemoveDomainedColumn";
            BtnRemoveDomainedColumn.Size = new Size(75, 102);
            BtnRemoveDomainedColumn.TabIndex = 10;
            BtnRemoveDomainedColumn.Text = "Remove this Domained Column from this Domain";
            BtnRemoveDomainedColumn.UseVisualStyleBackColor = true;
            BtnRemoveDomainedColumn.Click += BtnRemove_Click;
            // 
            // LstColumns
            // 
            LstColumns.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LstColumns.FormattingEnabled = true;
            LstColumns.ItemHeight = 15;
            LstColumns.Location = new Point(3, 8);
            LstColumns.Name = "LstColumns";
            LstColumns.Size = new Size(506, 229);
            LstColumns.TabIndex = 8;
            // 
            // DomainedColumnControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "DomainedColumnControl";
            Size = new Size(606, 270);
            Load += DomainedColumnControl_Load;
            Leave += DomainedColumnControl_Leave;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label LblBackfillIdName;
        private CheckBox ChkBackillId;
        private Panel panel1;
        private ListBox LstColumns;
        private Button BtnRemoveDomainedColumn;
        internal TextBox TxtBackfillIdName;
    }
}
