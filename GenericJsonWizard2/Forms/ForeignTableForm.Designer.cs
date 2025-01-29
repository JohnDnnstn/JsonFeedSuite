using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonSuite.EtlaToolbelt.Wizards;

namespace GenericJsonWizard.Forms
{
    partial class ForeignTableForm : RepeatingWizardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForeignTableForm));
            label1 = new Label();
            TxtSchemaName = new TextBox();
            label2 = new Label();
            TxtTableName = new TextBox();
            label3 = new Label();
            TxtIdName = new TextBox();
            CmbIdType = new ComboBox();
            groupBox1 = new GroupBox();
            RdoNewValuesBad = new RadioButton();
            RdoAddNewValues = new RadioButton();
            panel1 = new Panel();
            L2lColumns = new List2List();
            ChkHasBackfilledId = new CheckBox();
            TxtBackfilledIdName = new TextBox();
            ChkLogicalFKOnly = new CheckBox();
            label4 = new Label();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // BtnNext
            // 
            BtnNext.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 114);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 100;
            label1.Text = "Schema";
            // 
            // TxtSchemaName
            // 
            TxtSchemaName.Location = new Point(67, 111);
            TxtSchemaName.Name = "TxtSchemaName";
            TxtSchemaName.Size = new Size(167, 23);
            TxtSchemaName.TabIndex = 101;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(240, 114);
            label2.Name = "label2";
            label2.Size = new Size(34, 15);
            label2.TabIndex = 102;
            label2.Text = "Table";
            // 
            // TxtTableName
            // 
            TxtTableName.Location = new Point(280, 111);
            TxtTableName.Name = "TxtTableName";
            TxtTableName.Size = new Size(174, 23);
            TxtTableName.TabIndex = 103;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 143);
            label3.Name = "label3";
            label3.Size = new Size(17, 15);
            label3.TabIndex = 104;
            label3.Text = "Id";
            // 
            // TxtIdName
            // 
            TxtIdName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtIdName.Location = new Point(35, 140);
            TxtIdName.Name = "TxtIdName";
            TxtIdName.Size = new Size(180, 23);
            TxtIdName.TabIndex = 105;
            // 
            // CmbIdType
            // 
            CmbIdType.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CmbIdType.FormattingEnabled = true;
            CmbIdType.Items.AddRange(new object[] { "SMALLINT", "INT", "BIGINT" });
            CmbIdType.Location = new Point(221, 140);
            CmbIdType.Name = "CmbIdType";
            CmbIdType.Size = new Size(86, 23);
            CmbIdType.TabIndex = 106;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(RdoNewValuesBad);
            groupBox1.Controls.Add(RdoAddNewValues);
            groupBox1.Location = new Point(12, 62);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(909, 39);
            groupBox1.TabIndex = 112;
            groupBox1.TabStop = false;
            groupBox1.Text = "Action when a new value doesnot already exist in the foreign table";
            // 
            // RdoNewValuesBad
            // 
            RdoNewValuesBad.AutoSize = true;
            RdoNewValuesBad.Location = new Point(228, 17);
            RdoNewValuesBad.Name = "RdoNewValuesBad";
            RdoNewValuesBad.Size = new Size(202, 19);
            RdoNewValuesBad.TabIndex = 1;
            RdoNewValuesBad.Text = "Treat as problem and exclude row";
            RdoNewValuesBad.UseVisualStyleBackColor = true;
            // 
            // RdoAddNewValues
            // 
            RdoAddNewValues.AutoSize = true;
            RdoAddNewValues.Checked = true;
            RdoAddNewValues.Location = new Point(6, 14);
            RdoAddNewValues.Name = "RdoAddNewValues";
            RdoAddNewValues.Size = new Size(187, 19);
            RdoAddNewValues.TabIndex = 0;
            RdoAddNewValues.TabStop = true;
            RdoAddNewValues.Text = "Add new value to foreign table";
            RdoAddNewValues.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(L2lColumns);
            panel1.Location = new Point(12, 179);
            panel1.Name = "panel1";
            panel1.Size = new Size(909, 293);
            panel1.TabIndex = 113;
            // 
            // L2lColumns
            // 
            L2lColumns.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            L2lColumns.ChosenItems = (System.Collections.IEnumerable)resources.GetObject("L2lColumns.ChosenItems");
            L2lColumns.Initialised = false;
            L2lColumns.IsValid = true;
            L2lColumns.Location = new Point(0, 0);
            L2lColumns.Name = "L2lColumns";
            L2lColumns.Size = new Size(909, 293);
            L2lColumns.TabIndex = 0;
            // 
            // ChkHasBackfilledId
            // 
            ChkHasBackfilledId.AutoSize = true;
            ChkHasBackfilledId.Location = new Point(635, 142);
            ChkHasBackfilledId.Name = "ChkHasBackfilledId";
            ChkHasBackfilledId.Size = new Size(113, 19);
            ChkHasBackfilledId.TabIndex = 114;
            ChkHasBackfilledId.Text = "Has backfilled id";
            ChkHasBackfilledId.UseVisualStyleBackColor = true;
            ChkHasBackfilledId.CheckedChanged += ChkHasBackfilledId_CheckedChanged;
            // 
            // TxtBackfilledIdName
            // 
            TxtBackfilledIdName.Location = new Point(754, 140);
            TxtBackfilledIdName.Name = "TxtBackfilledIdName";
            TxtBackfilledIdName.Size = new Size(167, 23);
            TxtBackfilledIdName.TabIndex = 115;
            // 
            // ChkLogicalFKOnly
            // 
            ChkLogicalFKOnly.AutoSize = true;
            ChkLogicalFKOnly.Location = new Point(801, 113);
            ChkLogicalFKOnly.Name = "ChkLogicalFKOnly";
            ChkLogicalFKOnly.Size = new Size(106, 19);
            ChkLogicalFKOnly.TabIndex = 116;
            ChkLogicalFKOnly.Text = "Logical FK only";
            ChkLogicalFKOnly.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 114);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 117;
            label4.Text = "Schema";
            // 
            // ForeignTableForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(label4);
            Controls.Add(ChkLogicalFKOnly);
            Controls.Add(TxtBackfilledIdName);
            Controls.Add(ChkHasBackfilledId);
            Controls.Add(panel1);
            Controls.Add(groupBox1);
            Controls.Add(CmbIdType);
            Controls.Add(TxtIdName);
            Controls.Add(label3);
            Controls.Add(TxtTableName);
            Controls.Add(label2);
            Controls.Add(TxtSchemaName);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ForeignTableForm";
            Text = "ForeignTableForm";
            Load += ForeignTableForm_Load;
            Controls.SetChildIndex(BtnRemove, 0);
            Controls.SetChildIndex(BtnNew, 0);
            Controls.SetChildIndex(TxtSchemaName, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(TxtTableName, 0);
            Controls.SetChildIndex(label3, 0);
            Controls.SetChildIndex(TxtIdName, 0);
            Controls.SetChildIndex(CmbIdType, 0);
            Controls.SetChildIndex(TxtRubric, 0);
            Controls.SetChildIndex(BtnBack, 0);
            Controls.SetChildIndex(BtnNext, 0);
            Controls.SetChildIndex(groupBox1, 0);
            Controls.SetChildIndex(panel1, 0);
            Controls.SetChildIndex(ChkHasBackfilledId, 0);
            Controls.SetChildIndex(TxtBackfilledIdName, 0);
            Controls.SetChildIndex(ChkLogicalFKOnly, 0);
            Controls.SetChildIndex(label4, 0);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox TxtSchemaName;
        private Label label2;
        private TextBox TxtTableName;
        private Label label3;
        private TextBox TxtIdName;
        private ComboBox CmbIdType;
        private GroupBox groupBox1;
        private RadioButton RdoNewValuesBad;
        private RadioButton RdoAddNewValues;
        private Panel panel1;
        private List2List L2lColumns;
        private CheckBox ChkHasBackfilledId;
        private TextBox TxtBackfilledIdName;
        private CheckBox ChkLogicalFKOnly;
        private Label label4;
    }
}