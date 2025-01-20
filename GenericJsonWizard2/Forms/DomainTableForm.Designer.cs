namespace GenericJsonWizard.Forms
{
    partial class DomainTableForm
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
            TxtSchemaName = new TextBox();
            label3 = new Label();
            TxtTableName = new TextBox();
            TxtPermittedValues = new TextBox();
            panel1 = new Panel();
            TabColumns = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            label5 = new Label();
            label6 = new Label();
            TxtDomainColumnName = new TextBox();
            label7 = new Label();
            TxtDomainColumnType = new TextBox();
            ChkHasId = new CheckBox();
            LblIdName = new Label();
            TxtIdName = new TextBox();
            TxtIdType = new TextBox();
            ChkHasDescription = new CheckBox();
            BtnAddNew = new Button();
            TxtDescriptionColumn = new TextBox();
            ChkIsReadOnly = new CheckBox();
            panel1.SuspendLayout();
            TabColumns.SuspendLayout();
            SuspendLayout();
            // 
            // BtnNext
            // 
            BtnNext.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 65);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 105;
            label2.Text = "Schema";
            // 
            // TxtSchemaName
            // 
            TxtSchemaName.Location = new Point(67, 62);
            TxtSchemaName.Name = "TxtSchemaName";
            TxtSchemaName.Size = new Size(163, 23);
            TxtSchemaName.TabIndex = 106;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(236, 65);
            label3.Name = "label3";
            label3.Size = new Size(34, 15);
            label3.TabIndex = 107;
            label3.Text = "Table";
            // 
            // TxtTableName
            // 
            TxtTableName.Location = new Point(276, 62);
            TxtTableName.Name = "TxtTableName";
            TxtTableName.Size = new Size(166, 23);
            TxtTableName.TabIndex = 108;
            TxtTableName.TextChanged += TxtTableName_TextChanged;
            // 
            // TxtPermittedValues
            // 
            TxtPermittedValues.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            TxtPermittedValues.Location = new Point(765, 177);
            TxtPermittedValues.Multiline = true;
            TxtPermittedValues.Name = "TxtPermittedValues";
            TxtPermittedValues.Size = new Size(156, 239);
            TxtPermittedValues.TabIndex = 109;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(TabColumns);
            panel1.Location = new Point(12, 118);
            panel1.Name = "panel1";
            panel1.Size = new Size(747, 367);
            panel1.TabIndex = 110;
            // 
            // TabColumns
            // 
            TabColumns.Controls.Add(tabPage1);
            TabColumns.Controls.Add(tabPage2);
            TabColumns.Dock = DockStyle.Fill;
            TabColumns.Location = new Point(0, 0);
            TabColumns.Name = "TabColumns";
            TabColumns.SelectedIndex = 0;
            TabColumns.Size = new Size(747, 367);
            TabColumns.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(739, 339);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(739, 339);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(765, 159);
            label5.Name = "label5";
            label5.Size = new Size(95, 15);
            label5.TabIndex = 111;
            label5.Text = "Permitted Values";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(451, 65);
            label6.Name = "label6";
            label6.Size = new Size(50, 15);
            label6.TabIndex = 112;
            label6.Text = "Column";
            // 
            // TxtDomainColumnName
            // 
            TxtDomainColumnName.Location = new Point(507, 62);
            TxtDomainColumnName.Name = "TxtDomainColumnName";
            TxtDomainColumnName.Size = new Size(140, 23);
            TxtDomainColumnName.TabIndex = 113;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(653, 65);
            label7.Name = "label7";
            label7.Size = new Size(31, 15);
            label7.TabIndex = 114;
            label7.Text = "Type";
            // 
            // TxtDomainColumnType
            // 
            TxtDomainColumnType.Location = new Point(688, 62);
            TxtDomainColumnType.Name = "TxtDomainColumnType";
            TxtDomainColumnType.Size = new Size(100, 23);
            TxtDomainColumnType.TabIndex = 115;
            // 
            // ChkHasId
            // 
            ChkHasId.AutoSize = true;
            ChkHasId.Location = new Point(12, 91);
            ChkHasId.Name = "ChkHasId";
            ChkHasId.Size = new Size(59, 19);
            ChkHasId.TabIndex = 116;
            ChkHasId.Text = "Has Id";
            ChkHasId.UseVisualStyleBackColor = true;
            ChkHasId.CheckedChanged += ChkHasId_CheckedChanged;
            // 
            // LblIdName
            // 
            LblIdName.AutoSize = true;
            LblIdName.Location = new Point(67, 92);
            LblIdName.Name = "LblIdName";
            LblIdName.Size = new Size(52, 15);
            LblIdName.TabIndex = 117;
            LblIdName.Text = "Id Name";
            // 
            // TxtIdName
            // 
            TxtIdName.Location = new Point(125, 89);
            TxtIdName.Name = "TxtIdName";
            TxtIdName.Size = new Size(145, 23);
            TxtIdName.TabIndex = 118;
            // 
            // TxtIdType
            // 
            TxtIdType.Location = new Point(276, 89);
            TxtIdType.Name = "TxtIdType";
            TxtIdType.Size = new Size(66, 23);
            TxtIdType.TabIndex = 120;
            // 
            // ChkHasDescription
            // 
            ChkHasDescription.AutoSize = true;
            ChkHasDescription.Location = new Point(507, 91);
            ChkHasDescription.Name = "ChkHasDescription";
            ChkHasDescription.Size = new Size(137, 19);
            ChkHasDescription.TabIndex = 121;
            ChkHasDescription.Text = "Has Description Field";
            ChkHasDescription.UseVisualStyleBackColor = true;
            ChkHasDescription.CheckedChanged += ChkHasDescription_CheckedChanged;
            // 
            // BtnAddNew
            // 
            BtnAddNew.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnAddNew.Enabled = false;
            BtnAddNew.Location = new Point(765, 118);
            BtnAddNew.Name = "BtnAddNew";
            BtnAddNew.Size = new Size(145, 38);
            BtnAddNew.TabIndex = 122;
            BtnAddNew.Text = "Add New Domained Column to this Domain";
            BtnAddNew.UseVisualStyleBackColor = true;
            BtnAddNew.Visible = false;
            // 
            // TxtDescriptionColumn
            // 
            TxtDescriptionColumn.Enabled = false;
            TxtDescriptionColumn.Location = new Point(650, 87);
            TxtDescriptionColumn.Name = "TxtDescriptionColumn";
            TxtDescriptionColumn.Size = new Size(156, 23);
            TxtDescriptionColumn.TabIndex = 123;
            TxtDescriptionColumn.Visible = false;
            // 
            // ChkIsReadOnly
            // 
            ChkIsReadOnly.AutoSize = true;
            ChkIsReadOnly.Location = new Point(844, 64);
            ChkIsReadOnly.Name = "ChkIsReadOnly";
            ChkIsReadOnly.Size = new Size(77, 19);
            ChkIsReadOnly.TabIndex = 124;
            ChkIsReadOnly.Text = "ReadOnly";
            ChkIsReadOnly.UseVisualStyleBackColor = true;
            // 
            // DomainTableForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(ChkIsReadOnly);
            Controls.Add(TxtDescriptionColumn);
            Controls.Add(BtnAddNew);
            Controls.Add(ChkHasDescription);
            Controls.Add(TxtIdType);
            Controls.Add(TxtIdName);
            Controls.Add(LblIdName);
            Controls.Add(ChkHasId);
            Controls.Add(TxtDomainColumnType);
            Controls.Add(label7);
            Controls.Add(TxtDomainColumnName);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(panel1);
            Controls.Add(TxtPermittedValues);
            Controls.Add(TxtTableName);
            Controls.Add(label3);
            Controls.Add(TxtSchemaName);
            Controls.Add(label2);
            Name = "DomainTableForm";
            Text = "DomainTableForm";
            Load += DomainTableForm_Load;
            Controls.SetChildIndex(TxtRubric, 0);
            Controls.SetChildIndex(BtnBack, 0);
            Controls.SetChildIndex(BtnNext, 0);
            Controls.SetChildIndex(BtnRemove, 0);
            Controls.SetChildIndex(BtnNew, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(TxtSchemaName, 0);
            Controls.SetChildIndex(label3, 0);
            Controls.SetChildIndex(TxtTableName, 0);
            Controls.SetChildIndex(TxtPermittedValues, 0);
            Controls.SetChildIndex(panel1, 0);
            Controls.SetChildIndex(label5, 0);
            Controls.SetChildIndex(label6, 0);
            Controls.SetChildIndex(TxtDomainColumnName, 0);
            Controls.SetChildIndex(label7, 0);
            Controls.SetChildIndex(TxtDomainColumnType, 0);
            Controls.SetChildIndex(ChkHasId, 0);
            Controls.SetChildIndex(LblIdName, 0);
            Controls.SetChildIndex(TxtIdName, 0);
            Controls.SetChildIndex(TxtIdType, 0);
            Controls.SetChildIndex(ChkHasDescription, 0);
            Controls.SetChildIndex(BtnAddNew, 0);
            Controls.SetChildIndex(TxtDescriptionColumn, 0);
            Controls.SetChildIndex(ChkIsReadOnly, 0);
            panel1.ResumeLayout(false);
            TabColumns.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private TextBox TxtSchemaName;
        private Label label3;
        private TextBox TxtTableName;
        private TextBox TxtPermittedValues;
        private Panel panel1;
        private TabControl TabColumns;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label5;
        private Label label6;
        private TextBox TxtDomainColumnName;
        private Label label7;
        private TextBox TxtDomainColumnType;
        private CheckBox ChkHasId;
        private Label LblIdName;
        private TextBox TxtIdName;
        private TextBox TxtIdType;
        private CheckBox ChkHasDescription;
        private Button BtnAddNew;
        private TextBox TxtDescriptionColumn;
        private CheckBox ChkIsReadOnly;
    }
}