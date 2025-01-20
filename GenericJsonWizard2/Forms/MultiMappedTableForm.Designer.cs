namespace GenericJsonWizard.Forms
{
    partial class MultiMappedTableForm
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
            label2 = new Label();
            TxtSchema = new TextBox();
            label3 = new Label();
            TxtTable = new TextBox();
            ChkHasId = new CheckBox();
            TxtIdName = new TextBox();
            CmbIdType = new ComboBox();
            panel1 = new Panel();
            TabControl = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            BtnAddMapping = new Button();
            panel1.SuspendLayout();
            TabControl.SuspendLayout();
            SuspendLayout();
            // 
            // BtnNext
            // 
            BtnNext.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 64);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 106;
            label2.Text = "Schema";
            // 
            // TxtSchema
            // 
            TxtSchema.Location = new Point(66, 61);
            TxtSchema.Name = "TxtSchema";
            TxtSchema.Size = new Size(163, 23);
            TxtSchema.TabIndex = 107;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(235, 64);
            label3.Name = "label3";
            label3.Size = new Size(34, 15);
            label3.TabIndex = 108;
            label3.Text = "Table";
            // 
            // TxtTable
            // 
            TxtTable.Location = new Point(275, 60);
            TxtTable.Name = "TxtTable";
            TxtTable.Size = new Size(166, 23);
            TxtTable.TabIndex = 109;
            TxtTable.TextChanged += TxtTable_TextChanged;
            // 
            // ChkHasId
            // 
            ChkHasId.AutoSize = true;
            ChkHasId.Location = new Point(453, 63);
            ChkHasId.Name = "ChkHasId";
            ChkHasId.Size = new Size(59, 19);
            ChkHasId.TabIndex = 110;
            ChkHasId.Text = "Has Id";
            ChkHasId.UseVisualStyleBackColor = true;
            ChkHasId.CheckedChanged += ChkHasId_CheckedChanged;
            // 
            // TxtIdName
            // 
            TxtIdName.Location = new Point(518, 61);
            TxtIdName.Name = "TxtIdName";
            TxtIdName.Size = new Size(140, 23);
            TxtIdName.TabIndex = 111;
            // 
            // CmbIdType
            // 
            CmbIdType.FormattingEnabled = true;
            CmbIdType.Items.AddRange(new object[] { "SMALLINT", "INT", "BIGINT" });
            CmbIdType.Location = new Point(664, 62);
            CmbIdType.Name = "CmbIdType";
            CmbIdType.Size = new Size(121, 23);
            CmbIdType.TabIndex = 112;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(TabControl);
            panel1.Location = new Point(12, 117);
            panel1.Name = "panel1";
            panel1.Size = new Size(909, 349);
            panel1.TabIndex = 113;
            // 
            // TabControl
            // 
            TabControl.Controls.Add(tabPage1);
            TabControl.Controls.Add(tabPage2);
            TabControl.Location = new Point(5, 7);
            TabControl.Name = "TabControl";
            TabControl.SelectedIndex = 0;
            TabControl.Size = new Size(900, 366);
            TabControl.TabIndex = 0;
            TabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            TabControl.Selecting += TabControl_Selecting;
            TabControl.Deselecting += TabControl_Deselecting;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(892, 338);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(892, 338);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // BtnAddMapping
            // 
            BtnAddMapping.Location = new Point(801, 59);
            BtnAddMapping.Name = "BtnAddMapping";
            BtnAddMapping.Size = new Size(120, 23);
            BtnAddMapping.TabIndex = 114;
            BtnAddMapping.Text = "Add Mapping";
            BtnAddMapping.UseVisualStyleBackColor = true;
            BtnAddMapping.Click += BtnAddMapping_Click;
            // 
            // MultiMappedTableForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(BtnAddMapping);
            Controls.Add(panel1);
            Controls.Add(CmbIdType);
            Controls.Add(TxtIdName);
            Controls.Add(ChkHasId);
            Controls.Add(TxtTable);
            Controls.Add(label3);
            Controls.Add(TxtSchema);
            Controls.Add(label2);
            Name = "MultiMappedTableForm";
            Text = "Multi-Mapped Table";
            Load += MultiMappedTableForm_Load;
            Controls.SetChildIndex(TxtRubric, 0);
            Controls.SetChildIndex(BtnBack, 0);
            Controls.SetChildIndex(BtnNext, 0);
            Controls.SetChildIndex(BtnRemove, 0);
            Controls.SetChildIndex(BtnNew, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(TxtSchema, 0);
            Controls.SetChildIndex(label3, 0);
            Controls.SetChildIndex(TxtTable, 0);
            Controls.SetChildIndex(ChkHasId, 0);
            Controls.SetChildIndex(TxtIdName, 0);
            Controls.SetChildIndex(CmbIdType, 0);
            Controls.SetChildIndex(panel1, 0);
            Controls.SetChildIndex(BtnAddMapping, 0);
            panel1.ResumeLayout(false);
            TabControl.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private TextBox TxtSchema;
        private Label label3;
        private TextBox TxtTable;
        private CheckBox ChkHasId;
        private TextBox TxtIdName;
        private ComboBox CmbIdType;
        private Panel panel1;
        private TabControl TabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button BtnAddMapping;
    }
}