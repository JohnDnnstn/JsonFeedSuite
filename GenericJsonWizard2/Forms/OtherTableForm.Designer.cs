using GenericJsonSuite.EtlaToolbelt.Forms;

namespace GenericJsonWizard.Forms
{
    partial class OtherTableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OtherTableForm));
            label2 = new Label();
            TxtSchemaName = new TextBox();
            label3 = new Label();
            TxtTableName = new TextBox();
            ChkStructureOnly = new CheckBox();
            panel1 = new Panel();
            TabDefinitionPages = new TabControl();
            TpgColumns = new TabPage();
            L2lColumns = new List2List();
            TpgLogicalPK = new TabPage();
            L2lLogicalPK = new List2List();
            TpgPhysicalPK = new TabPage();
            TxtBackfillId = new TextBox();
            ChkBackfillId = new CheckBox();
            CmbIdType = new ComboBox();
            TxtIdName = new TextBox();
            LblId = new Label();
            LstPhysicalPK = new ListBox();
            panel2 = new Panel();
            L2lPhysicalPK = new List2List();
            groupBox1 = new GroupBox();
            RdoPhysicalPkId = new RadioButton();
            RdoPhysicalPkDifferent = new RadioButton();
            RdoPhysicalPkSame = new RadioButton();
            RdoPhysicalPkNone = new RadioButton();
            TpgLogicalDuplicates = new TabPage();
            TpgWarnings = new TabPage();
            TxtWarnings = new TextBox();
            panel1.SuspendLayout();
            TabDefinitionPages.SuspendLayout();
            TpgColumns.SuspendLayout();
            TpgLogicalPK.SuspendLayout();
            TpgPhysicalPK.SuspendLayout();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            TpgWarnings.SuspendLayout();
            SuspendLayout();
            // 
            // BtnNext
            // 
            BtnNext.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 69);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 105;
            label2.Text = "Schema";
            // 
            // TxtSchemaName
            // 
            TxtSchemaName.Location = new Point(67, 66);
            TxtSchemaName.Name = "TxtSchemaName";
            TxtSchemaName.Size = new Size(177, 23);
            TxtSchemaName.TabIndex = 106;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(250, 69);
            label3.Name = "label3";
            label3.Size = new Size(34, 15);
            label3.TabIndex = 107;
            label3.Text = "Table";
            // 
            // TxtTableName
            // 
            TxtTableName.Location = new Point(290, 66);
            TxtTableName.Name = "TxtTableName";
            TxtTableName.Size = new Size(164, 23);
            TxtTableName.TabIndex = 108;
            // 
            // ChkStructureOnly
            // 
            ChkStructureOnly.AutoSize = true;
            ChkStructureOnly.Location = new Point(460, 68);
            ChkStructureOnly.Name = "ChkStructureOnly";
            ChkStructureOnly.Size = new Size(99, 19);
            ChkStructureOnly.TabIndex = 109;
            ChkStructureOnly.Text = "StructureOnly";
            ChkStructureOnly.UseVisualStyleBackColor = true;
            ChkStructureOnly.CheckedChanged += ChkStructureOnly_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(TabDefinitionPages);
            panel1.Location = new Point(12, 126);
            panel1.Name = "panel1";
            panel1.Size = new Size(909, 359);
            panel1.TabIndex = 112;
            // 
            // TabDefinitionPages
            // 
            TabDefinitionPages.Controls.Add(TpgColumns);
            TabDefinitionPages.Controls.Add(TpgLogicalPK);
            TabDefinitionPages.Controls.Add(TpgPhysicalPK);
            TabDefinitionPages.Controls.Add(TpgLogicalDuplicates);
            TabDefinitionPages.Controls.Add(TpgWarnings);
            TabDefinitionPages.Dock = DockStyle.Fill;
            TabDefinitionPages.Location = new Point(0, 0);
            TabDefinitionPages.Name = "TabDefinitionPages";
            TabDefinitionPages.SelectedIndex = 0;
            TabDefinitionPages.Size = new Size(909, 359);
            TabDefinitionPages.TabIndex = 0;
            // 
            // TpgColumns
            // 
            TpgColumns.Controls.Add(L2lColumns);
            TpgColumns.Location = new Point(4, 24);
            TpgColumns.Name = "TpgColumns";
            TpgColumns.Padding = new Padding(3);
            TpgColumns.Size = new Size(901, 331);
            TpgColumns.TabIndex = 0;
            TpgColumns.Text = "Columns";
            TpgColumns.UseVisualStyleBackColor = true;
            // 
            // L2lColumns
            // 
            L2lColumns.ChosenItems = (System.Collections.IEnumerable)resources.GetObject("L2lColumns.ChosenItems");
            L2lColumns.Dock = DockStyle.Fill;
            L2lColumns.Initialised = false;
            L2lColumns.IsValid = true;
            L2lColumns.Location = new Point(3, 3);
            L2lColumns.Name = "L2lColumns";
            L2lColumns.Size = new Size(895, 325);
            L2lColumns.TabIndex = 0;
            L2lColumns.ListsChanged += L2lColumns_ListsChanged;
            // 
            // TpgLogicalPK
            // 
            TpgLogicalPK.Controls.Add(L2lLogicalPK);
            TpgLogicalPK.Location = new Point(4, 24);
            TpgLogicalPK.Name = "TpgLogicalPK";
            TpgLogicalPK.Padding = new Padding(3);
            TpgLogicalPK.Size = new Size(901, 331);
            TpgLogicalPK.TabIndex = 1;
            TpgLogicalPK.Text = "Logical Primary Key";
            TpgLogicalPK.UseVisualStyleBackColor = true;
            // 
            // L2lLogicalPK
            // 
            L2lLogicalPK.ChosenItems = (System.Collections.IEnumerable)resources.GetObject("L2lLogicalPK.ChosenItems");
            L2lLogicalPK.Dock = DockStyle.Fill;
            L2lLogicalPK.Initialised = false;
            L2lLogicalPK.IsValid = true;
            L2lLogicalPK.Location = new Point(3, 3);
            L2lLogicalPK.Name = "L2lLogicalPK";
            L2lLogicalPK.Size = new Size(895, 325);
            L2lLogicalPK.TabIndex = 0;
            L2lLogicalPK.ListsChanged += L2lLogicalPK_ListsChanged;
            // 
            // TpgPhysicalPK
            // 
            TpgPhysicalPK.Controls.Add(TxtBackfillId);
            TpgPhysicalPK.Controls.Add(ChkBackfillId);
            TpgPhysicalPK.Controls.Add(CmbIdType);
            TpgPhysicalPK.Controls.Add(TxtIdName);
            TpgPhysicalPK.Controls.Add(LblId);
            TpgPhysicalPK.Controls.Add(LstPhysicalPK);
            TpgPhysicalPK.Controls.Add(panel2);
            TpgPhysicalPK.Controls.Add(groupBox1);
            TpgPhysicalPK.Location = new Point(4, 24);
            TpgPhysicalPK.Name = "TpgPhysicalPK";
            TpgPhysicalPK.Padding = new Padding(3);
            TpgPhysicalPK.Size = new Size(901, 331);
            TpgPhysicalPK.TabIndex = 2;
            TpgPhysicalPK.Text = "Physical Primary Key";
            TpgPhysicalPK.UseVisualStyleBackColor = true;
            // 
            // TxtBackfillId
            // 
            TxtBackfillId.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            TxtBackfillId.Location = new Point(675, 304);
            TxtBackfillId.Name = "TxtBackfillId";
            TxtBackfillId.Size = new Size(176, 23);
            TxtBackfillId.TabIndex = 7;
            // 
            // ChkBackfillId
            // 
            ChkBackfillId.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ChkBackfillId.AutoSize = true;
            ChkBackfillId.Location = new Point(540, 306);
            ChkBackfillId.Name = "ChkBackfillId";
            ChkBackfillId.Size = new Size(129, 19);
            ChkBackfillId.TabIndex = 6;
            ChkBackfillId.Text = "Generates BackfillId";
            ChkBackfillId.UseVisualStyleBackColor = true;
            ChkBackfillId.CheckedChanged += ChkBackfillId_CheckedChanged;
            // 
            // CmbIdType
            // 
            CmbIdType.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CmbIdType.FormattingEnabled = true;
            CmbIdType.Items.AddRange(new object[] { "SMALLINT", "INT", "BIGINT" });
            CmbIdType.Location = new Point(258, 304);
            CmbIdType.Name = "CmbIdType";
            CmbIdType.Size = new Size(121, 23);
            CmbIdType.TabIndex = 5;
            // 
            // TxtIdName
            // 
            TxtIdName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            TxtIdName.Location = new Point(29, 304);
            TxtIdName.Name = "TxtIdName";
            TxtIdName.Size = new Size(223, 23);
            TxtIdName.TabIndex = 4;
            // 
            // LblId
            // 
            LblId.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            LblId.AutoSize = true;
            LblId.Location = new Point(6, 307);
            LblId.Name = "LblId";
            LblId.Size = new Size(17, 15);
            LblId.TabIndex = 3;
            LblId.Text = "Id";
            // 
            // LstPhysicalPK
            // 
            LstPhysicalPK.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            LstPhysicalPK.FormattingEnabled = true;
            LstPhysicalPK.ItemHeight = 15;
            LstPhysicalPK.Location = new Point(6, 139);
            LstPhysicalPK.Name = "LstPhysicalPK";
            LstPhysicalPK.Size = new Size(160, 154);
            LstPhysicalPK.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Controls.Add(L2lPhysicalPK);
            panel2.Location = new Point(172, 6);
            panel2.Name = "panel2";
            panel2.Size = new Size(723, 298);
            panel2.TabIndex = 1;
            // 
            // L2lPhysicalPK
            // 
            L2lPhysicalPK.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            L2lPhysicalPK.ChosenItems = (System.Collections.IEnumerable)resources.GetObject("L2lPhysicalPK.ChosenItems");
            L2lPhysicalPK.Initialised = false;
            L2lPhysicalPK.IsValid = true;
            L2lPhysicalPK.Location = new Point(0, 0);
            L2lPhysicalPK.Name = "L2lPhysicalPK";
            L2lPhysicalPK.Size = new Size(723, 298);
            L2lPhysicalPK.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(RdoPhysicalPkId);
            groupBox1.Controls.Add(RdoPhysicalPkDifferent);
            groupBox1.Controls.Add(RdoPhysicalPkSame);
            groupBox1.Controls.Add(RdoPhysicalPkNone);
            groupBox1.Location = new Point(6, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(160, 127);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Physical Primary Key";
            // 
            // RdoPhysicalPkId
            // 
            RdoPhysicalPkId.AutoSize = true;
            RdoPhysicalPkId.Checked = true;
            RdoPhysicalPkId.Location = new Point(6, 100);
            RdoPhysicalPkId.Name = "RdoPhysicalPkId";
            RdoPhysicalPkId.Size = new Size(65, 19);
            RdoPhysicalPkId.TabIndex = 3;
            RdoPhysicalPkId.TabStop = true;
            RdoPhysicalPkId.Text = "Identity";
            RdoPhysicalPkId.UseVisualStyleBackColor = true;
            RdoPhysicalPkId.CheckedChanged += RdoPhysicalPk_CheckedChanged;
            // 
            // RdoPhysicalPkDifferent
            // 
            RdoPhysicalPkDifferent.AutoSize = true;
            RdoPhysicalPkDifferent.Location = new Point(6, 74);
            RdoPhysicalPkDifferent.Name = "RdoPhysicalPkDifferent";
            RdoPhysicalPkDifferent.Size = new Size(143, 19);
            RdoPhysicalPkDifferent.TabIndex = 2;
            RdoPhysicalPkDifferent.Text = "Different to Logical PK";
            RdoPhysicalPkDifferent.UseVisualStyleBackColor = true;
            RdoPhysicalPkDifferent.CheckedChanged += RdoPhysicalPk_CheckedChanged;
            // 
            // RdoPhysicalPkSame
            // 
            RdoPhysicalPkSame.AutoSize = true;
            RdoPhysicalPkSame.Location = new Point(6, 48);
            RdoPhysicalPkSame.Name = "RdoPhysicalPkSame";
            RdoPhysicalPkSame.Size = new Size(126, 19);
            RdoPhysicalPkSame.TabIndex = 1;
            RdoPhysicalPkSame.Text = "Same as Logical PK";
            RdoPhysicalPkSame.UseVisualStyleBackColor = true;
            RdoPhysicalPkSame.CheckedChanged += RdoPhysicalPk_CheckedChanged;
            // 
            // RdoPhysicalPkNone
            // 
            RdoPhysicalPkNone.AutoSize = true;
            RdoPhysicalPkNone.Enabled = false;
            RdoPhysicalPkNone.Location = new Point(6, 22);
            RdoPhysicalPkNone.Name = "RdoPhysicalPkNone";
            RdoPhysicalPkNone.Size = new Size(54, 19);
            RdoPhysicalPkNone.TabIndex = 0;
            RdoPhysicalPkNone.Text = "None";
            RdoPhysicalPkNone.UseVisualStyleBackColor = true;
            RdoPhysicalPkNone.CheckedChanged += RdoPhysicalPk_CheckedChanged;
            // 
            // TpgLogicalDuplicates
            // 
            TpgLogicalDuplicates.Location = new Point(4, 24);
            TpgLogicalDuplicates.Name = "TpgLogicalDuplicates";
            TpgLogicalDuplicates.Padding = new Padding(3);
            TpgLogicalDuplicates.Size = new Size(901, 331);
            TpgLogicalDuplicates.TabIndex = 3;
            TpgLogicalDuplicates.Text = "Logical Duplicates";
            TpgLogicalDuplicates.UseVisualStyleBackColor = true;
            // 
            // TpgWarnings
            // 
            TpgWarnings.Controls.Add(TxtWarnings);
            TpgWarnings.Location = new Point(4, 24);
            TpgWarnings.Name = "TpgWarnings";
            TpgWarnings.Padding = new Padding(3);
            TpgWarnings.Size = new Size(901, 331);
            TpgWarnings.TabIndex = 4;
            TpgWarnings.Text = "Warnings";
            TpgWarnings.UseVisualStyleBackColor = true;
            // 
            // TxtWarnings
            // 
            TxtWarnings.Dock = DockStyle.Fill;
            TxtWarnings.Location = new Point(3, 3);
            TxtWarnings.Multiline = true;
            TxtWarnings.Name = "TxtWarnings";
            TxtWarnings.Size = new Size(895, 325);
            TxtWarnings.TabIndex = 0;
            // 
            // OtherTableForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(panel1);
            Controls.Add(ChkStructureOnly);
            Controls.Add(TxtTableName);
            Controls.Add(label3);
            Controls.Add(TxtSchemaName);
            Controls.Add(label2);
            Name = "OtherTableForm";
            Text = "OtherTableForm";
            Load += OtherTableForm_Load;
            Controls.SetChildIndex(TxtRubric, 0);
            Controls.SetChildIndex(BtnBack, 0);
            Controls.SetChildIndex(BtnNext, 0);
            Controls.SetChildIndex(BtnRemove, 0);
            Controls.SetChildIndex(BtnNew, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(TxtSchemaName, 0);
            Controls.SetChildIndex(label3, 0);
            Controls.SetChildIndex(TxtTableName, 0);
            Controls.SetChildIndex(ChkStructureOnly, 0);
            Controls.SetChildIndex(panel1, 0);
            panel1.ResumeLayout(false);
            TabDefinitionPages.ResumeLayout(false);
            TpgColumns.ResumeLayout(false);
            TpgLogicalPK.ResumeLayout(false);
            TpgPhysicalPK.ResumeLayout(false);
            TpgPhysicalPK.PerformLayout();
            panel2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            TpgWarnings.ResumeLayout(false);
            TpgWarnings.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private TextBox TxtSchemaName;
        private Label label3;
        private TextBox TxtTableName;
        private CheckBox ChkStructureOnly;
        private Panel panel1;
        private TabControl TabDefinitionPages;
        private TabPage TpgColumns;
        private TabPage TpgLogicalPK;
        private List2List L2lColumns;
        private List2List L2lLogicalPK;
        private TabPage TpgPhysicalPK;
        private GroupBox groupBox1;
        private RadioButton RdoPhysicalPkDifferent;
        private RadioButton RdoPhysicalPkSame;
        private RadioButton RdoPhysicalPkNone;
        private RadioButton RdoPhysicalPkId;
        private ListBox LstPhysicalPK;
        private Panel panel2;
        private List2List L2lPhysicalPK;
        private ComboBox CmbIdType;
        private TextBox TxtIdName;
        private Label LblId;
        private TabPage TpgLogicalDuplicates;
        private TabPage TpgWarnings;
        private TextBox TxtWarnings;
        private CheckBox ChkBackfillId;
        private TextBox TxtBackfillId;
    }
}