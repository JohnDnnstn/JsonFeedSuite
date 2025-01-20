namespace GenericJsonWizard.Forms
{
    partial class ColumnDetailsForm
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            TxtJsonName = new TextBox();
            label2 = new Label();
            TxtDbName = new TextBox();
            label3 = new Label();
            TxtJsonType = new TextBox();
            label4 = new Label();
            TxtDbType = new TextBox();
            label5 = new Label();
            TxtDbJsonPath = new TextBox();
            ChkNullable = new CheckBox();
            label6 = new Label();
            TxtDefault = new TextBox();
            label7 = new Label();
            TxtFromNull = new TextBox();
            label8 = new Label();
            TxtToNull = new TextBox();
            label10 = new Label();
            TxtDescription = new TextBox();
            CmbStandardiser = new ComboBox();
            label9 = new Label();
            label11 = new Label();
            CmbBadWhenTrue = new ComboBox();
            TtpTips = new ToolTip(components);
            TxtOriginalPath = new TextBox();
            label12 = new Label();
            SuspendLayout();
            // 
            // BtnNext
            // 
            BtnNext.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 69);
            label1.Name = "label1";
            label1.Size = new Size(65, 15);
            label1.TabIndex = 100;
            label1.Text = "Json Name";
            // 
            // TxtJsonName
            // 
            TxtJsonName.Location = new Point(101, 66);
            TxtJsonName.Name = "TxtJsonName";
            TxtJsonName.ReadOnly = true;
            TxtJsonName.Size = new Size(286, 23);
            TxtJsonName.TabIndex = 101;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(412, 69);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 102;
            label2.Text = "Db Name";
            // 
            // TxtDbName
            // 
            TxtDbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtDbName.Location = new Point(475, 66);
            TxtDbName.Name = "TxtDbName";
            TxtDbName.Size = new Size(433, 23);
            TxtDbName.TabIndex = 103;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 101);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 104;
            label3.Text = "Json Type";
            // 
            // TxtJsonType
            // 
            TxtJsonType.Location = new Point(101, 98);
            TxtJsonType.Name = "TxtJsonType";
            TxtJsonType.ReadOnly = true;
            TxtJsonType.Size = new Size(286, 23);
            TxtJsonType.TabIndex = 105;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(412, 101);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 106;
            label4.Text = "Db Type";
            // 
            // TxtDbType
            // 
            TxtDbType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtDbType.Location = new Point(475, 98);
            TxtDbType.Name = "TxtDbType";
            TxtDbType.Size = new Size(433, 23);
            TxtDbType.TabIndex = 107;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 159);
            label5.Name = "label5";
            label5.Size = new Size(47, 15);
            label5.TabIndex = 108;
            label5.Text = "SqlPath";
            // 
            // TxtDbJsonPath
            // 
            TxtDbJsonPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtDbJsonPath.Location = new Point(101, 156);
            TxtDbJsonPath.Name = "TxtDbJsonPath";
            TxtDbJsonPath.ReadOnly = true;
            TxtDbJsonPath.Size = new Size(807, 23);
            TxtDbJsonPath.TabIndex = 109;
            // 
            // ChkNullable
            // 
            ChkNullable.AutoSize = true;
            ChkNullable.Location = new Point(12, 207);
            ChkNullable.Name = "ChkNullable";
            ChkNullable.Size = new Size(70, 19);
            ChkNullable.TabIndex = 110;
            ChkNullable.Text = "Nullable";
            ChkNullable.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(101, 208);
            label6.Name = "label6";
            label6.Size = new Size(45, 15);
            label6.TabIndex = 111;
            label6.Text = "Default";
            // 
            // TxtDefault
            // 
            TxtDefault.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtDefault.Location = new Point(152, 205);
            TxtDefault.Name = "TxtDefault";
            TxtDefault.Size = new Size(756, 23);
            TxtDefault.TabIndex = 112;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 240);
            label7.Name = "label7";
            label7.Size = new Size(60, 15);
            label7.TabIndex = 113;
            label7.Text = "From Null";
            // 
            // TxtFromNull
            // 
            TxtFromNull.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtFromNull.Location = new Point(101, 237);
            TxtFromNull.Name = "TxtFromNull";
            TxtFromNull.Size = new Size(807, 23);
            TxtFromNull.TabIndex = 114;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 274);
            label8.Name = "label8";
            label8.Size = new Size(44, 15);
            label8.TabIndex = 115;
            label8.Text = "To Null";
            // 
            // TxtToNull
            // 
            TxtToNull.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtToNull.Location = new Point(101, 271);
            TxtToNull.Name = "TxtToNull";
            TxtToNull.Size = new Size(807, 23);
            TxtToNull.TabIndex = 116;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(12, 306);
            label10.Name = "label10";
            label10.Size = new Size(67, 15);
            label10.TabIndex = 117;
            label10.Text = "Description";
            // 
            // TxtDescription
            // 
            TxtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TxtDescription.Location = new Point(101, 300);
            TxtDescription.Multiline = true;
            TxtDescription.Name = "TxtDescription";
            TxtDescription.ScrollBars = ScrollBars.Both;
            TxtDescription.Size = new Size(807, 122);
            TxtDescription.TabIndex = 118;
            // 
            // CmbStandardiser
            // 
            CmbStandardiser.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            CmbStandardiser.FormattingEnabled = true;
            CmbStandardiser.Items.AddRange(new object[] { "std_alphanumeric_only", "std_cph", "std_cph_as_int_to_cph", "std_cphh", "std_pascal_case" });
            CmbStandardiser.Location = new Point(101, 428);
            CmbStandardiser.Name = "CmbStandardiser";
            CmbStandardiser.Size = new Size(807, 23);
            CmbStandardiser.TabIndex = 119;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(4, 431);
            label9.Name = "label9";
            label9.Size = new Size(72, 15);
            label9.TabIndex = 120;
            label9.Text = "Standardiser";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(4, 464);
            label11.Name = "label11";
            label11.Size = new Size(83, 15);
            label11.TabIndex = 121;
            label11.Text = "Bad when true";
            // 
            // CmbBadWhenTrue
            // 
            CmbBadWhenTrue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            CmbBadWhenTrue.FormattingEnabled = true;
            CmbBadWhenTrue.Items.AddRange(new object[] { "bad_cattle_tag", "bad_cph", "bad_cphh", "bad_gb_cattle_eartag", "bad_postcode" });
            CmbBadWhenTrue.Location = new Point(101, 461);
            CmbBadWhenTrue.Name = "CmbBadWhenTrue";
            CmbBadWhenTrue.Size = new Size(807, 23);
            CmbBadWhenTrue.TabIndex = 122;
            // 
            // TxtOriginalPath
            // 
            TxtOriginalPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtOriginalPath.Location = new Point(101, 127);
            TxtOriginalPath.Name = "TxtOriginalPath";
            TxtOriginalPath.ReadOnly = true;
            TxtOriginalPath.Size = new Size(807, 23);
            TxtOriginalPath.TabIndex = 124;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(12, 130);
            label12.Name = "label12";
            label12.Size = new Size(73, 15);
            label12.TabIndex = 123;
            label12.Text = "OriginalPath";
            // 
            // ColumnDetailsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(TxtOriginalPath);
            Controls.Add(label12);
            Controls.Add(CmbBadWhenTrue);
            Controls.Add(label11);
            Controls.Add(label9);
            Controls.Add(CmbStandardiser);
            Controls.Add(TxtDescription);
            Controls.Add(label10);
            Controls.Add(TxtToNull);
            Controls.Add(label8);
            Controls.Add(TxtFromNull);
            Controls.Add(label7);
            Controls.Add(TxtDefault);
            Controls.Add(label6);
            Controls.Add(ChkNullable);
            Controls.Add(TxtDbJsonPath);
            Controls.Add(label5);
            Controls.Add(TxtDbType);
            Controls.Add(label4);
            Controls.Add(TxtJsonType);
            Controls.Add(label3);
            Controls.Add(TxtDbName);
            Controls.Add(label2);
            Controls.Add(TxtJsonName);
            Controls.Add(label1);
            Name = "ColumnDetailsForm";
            Text = "ColumnDetailsForm";
            Load += ColumnDetailsForm_Load;
            Controls.SetChildIndex(label1, 0);
            Controls.SetChildIndex(TxtJsonName, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(TxtDbName, 0);
            Controls.SetChildIndex(label3, 0);
            Controls.SetChildIndex(TxtJsonType, 0);
            Controls.SetChildIndex(label4, 0);
            Controls.SetChildIndex(TxtDbType, 0);
            Controls.SetChildIndex(label5, 0);
            Controls.SetChildIndex(TxtDbJsonPath, 0);
            Controls.SetChildIndex(ChkNullable, 0);
            Controls.SetChildIndex(label6, 0);
            Controls.SetChildIndex(TxtDefault, 0);
            Controls.SetChildIndex(label7, 0);
            Controls.SetChildIndex(TxtFromNull, 0);
            Controls.SetChildIndex(label8, 0);
            Controls.SetChildIndex(TxtToNull, 0);
            Controls.SetChildIndex(TxtRubric, 0);
            Controls.SetChildIndex(BtnBack, 0);
            Controls.SetChildIndex(BtnNext, 0);
            Controls.SetChildIndex(label10, 0);
            Controls.SetChildIndex(TxtDescription, 0);
            Controls.SetChildIndex(CmbStandardiser, 0);
            Controls.SetChildIndex(label9, 0);
            Controls.SetChildIndex(label11, 0);
            Controls.SetChildIndex(CmbBadWhenTrue, 0);
            Controls.SetChildIndex(label12, 0);
            Controls.SetChildIndex(TxtOriginalPath, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox TxtJsonName;
        private Label label2;
        private TextBox TxtDbName;
        private Label label3;
        private TextBox TxtJsonType;
        private Label label4;
        private TextBox TxtDbType;
        private Label label5;
        private TextBox TxtDbJsonPath;
        private CheckBox ChkNullable;
        private Label label6;
        private TextBox TxtDefault;
        private Label label7;
        private TextBox TxtFromNull;
        private Label label8;
        private TextBox TxtToNull;
        private Label label10;
        private TextBox TxtDescription;
        private ComboBox CmbStandardiser;
        private Label label9;
        private Label label11;
        private ComboBox CmbBadWhenTrue;
        private ToolTip TtpTips;
        private Label label12;
        private TextBox TxtOriginalPath;
    }
}