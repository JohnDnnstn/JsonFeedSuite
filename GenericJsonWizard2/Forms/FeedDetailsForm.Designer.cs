namespace GenericJsonWizard.Forms
{
    partial class FeedDetailsForm
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
            label1 = new Label();
            BtnBrowse = new Button();
            TxtFilepath = new TextBox();
            label2 = new Label();
            TxtTargetTable = new TextBox();
            TxtTargetSchema = new TextBox();
            label3 = new Label();
            label4 = new Label();
            BtnResetTargetTable = new Button();
            RdoTableViaDir = new RadioButton();
            RdoTableViaFeed = new RadioButton();
            TxtDatabase = new TextBox();
            label5 = new Label();
            TxtFeedBaseName = new TextBox();
            label6 = new Label();
            TxtFeedFullName = new TextBox();
            BtnResetFeedFullName = new Button();
            BtnResetFeedBaseName = new Button();
            SuspendLayout();
            // 
            // BtnNext
            // 
            BtnNext.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(48, 74);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 100;
            label1.Text = "File name:";
            // 
            // BtnBrowse
            // 
            BtnBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnBrowse.Location = new Point(846, 70);
            BtnBrowse.Name = "BtnBrowse";
            BtnBrowse.Size = new Size(75, 23);
            BtnBrowse.TabIndex = 101;
            BtnBrowse.Text = "Browse";
            BtnBrowse.UseVisualStyleBackColor = true;
            BtnBrowse.Click += BtnBrowse_Click;
            // 
            // TxtFilepath
            // 
            TxtFilepath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtFilepath.Location = new Point(115, 71);
            TxtFilepath.Name = "TxtFilepath";
            TxtFilepath.Size = new Size(725, 23);
            TxtFilepath.TabIndex = 102;
            TxtFilepath.TextChanged += TxtFilepath_TextChanged;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(37, 352);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 103;
            label2.Text = "Target Table:";
            // 
            // TxtTargetTable
            // 
            TxtTargetTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TxtTargetTable.Location = new Point(115, 349);
            TxtTargetTable.Name = "TxtTargetTable";
            TxtTargetTable.Size = new Size(332, 23);
            TxtTargetTable.TabIndex = 104;
            TxtTargetTable.TextChanged += TxtTargetTable_TextChanged;
            // 
            // TxtTargetSchema
            // 
            TxtTargetSchema.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TxtTargetSchema.Location = new Point(115, 380);
            TxtTargetSchema.Name = "TxtTargetSchema";
            TxtTargetSchema.Size = new Size(332, 23);
            TxtTargetSchema.TabIndex = 105;
            TxtTargetSchema.TextChanged += TxtTargetSchema_TextChanged;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(22, 383);
            label3.Name = "label3";
            label3.Size = new Size(87, 15);
            label3.TabIndex = 106;
            label3.Text = "Target Schema:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(51, 413);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 107;
            label4.Text = "Database:";
            // 
            // BtnResetTargetTable
            // 
            BtnResetTargetTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BtnResetTargetTable.Location = new Point(452, 348);
            BtnResetTargetTable.Name = "BtnResetTargetTable";
            BtnResetTargetTable.Size = new Size(75, 23);
            BtnResetTargetTable.TabIndex = 108;
            BtnResetTargetTable.Text = "Reset";
            BtnResetTargetTable.UseVisualStyleBackColor = true;
            BtnResetTargetTable.Click += BtnResetTargetTable_Click;
            // 
            // RdoTableViaDir
            // 
            RdoTableViaDir.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            RdoTableViaDir.AutoSize = true;
            RdoTableViaDir.Checked = true;
            RdoTableViaDir.Location = new Point(533, 350);
            RdoTableViaDir.Name = "RdoTableViaDir";
            RdoTableViaDir.Size = new Size(189, 19);
            RdoTableViaDir.TabIndex = 109;
            RdoTableViaDir.TabStop = true;
            RdoTableViaDir.Text = "Table name  based on directory";
            RdoTableViaDir.UseVisualStyleBackColor = true;
            RdoTableViaDir.CheckedChanged += RdoTableViaDir_CheckedChanged;
            // 
            // RdoTableViaFeed
            // 
            RdoTableViaFeed.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            RdoTableViaFeed.AutoSize = true;
            RdoTableViaFeed.Location = new Point(728, 350);
            RdoTableViaFeed.Name = "RdoTableViaFeed";
            RdoTableViaFeed.Size = new Size(195, 19);
            RdoTableViaFeed.TabIndex = 110;
            RdoTableViaFeed.TabStop = true;
            RdoTableViaFeed.Text = "Table name based on feed name";
            RdoTableViaFeed.UseVisualStyleBackColor = true;
            RdoTableViaFeed.CheckedChanged += RdoTableViaFeed_CheckedChanged;
            // 
            // TxtDatabase
            // 
            TxtDatabase.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TxtDatabase.Location = new Point(115, 410);
            TxtDatabase.Name = "TxtDatabase";
            TxtDatabase.Size = new Size(332, 23);
            TxtDatabase.TabIndex = 111;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 121);
            label5.Name = "label5";
            label5.Size = new Size(97, 15);
            label5.TabIndex = 112;
            label5.Text = "Feed Base Name:";
            // 
            // TxtFeedBaseName
            // 
            TxtFeedBaseName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtFeedBaseName.Location = new Point(115, 118);
            TxtFeedBaseName.Name = "TxtFeedBaseName";
            TxtFeedBaseName.Size = new Size(412, 23);
            TxtFeedBaseName.TabIndex = 113;
            TxtFeedBaseName.TextChanged += TxtFeedBaseName_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(17, 152);
            label6.Name = "label6";
            label6.Size = new Size(92, 15);
            label6.TabIndex = 114;
            label6.Text = "Feed Full Name:";
            // 
            // TxtFeedFullName
            // 
            TxtFeedFullName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtFeedFullName.Location = new Point(115, 149);
            TxtFeedFullName.Name = "TxtFeedFullName";
            TxtFeedFullName.Size = new Size(607, 23);
            TxtFeedFullName.TabIndex = 115;
            TxtFeedFullName.TextChanged += TxtFeedFullName_TextChanged;
            // 
            // BtnResetFeedFullName
            // 
            BtnResetFeedFullName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnResetFeedFullName.Location = new Point(741, 148);
            BtnResetFeedFullName.Name = "BtnResetFeedFullName";
            BtnResetFeedFullName.Size = new Size(75, 23);
            BtnResetFeedFullName.TabIndex = 116;
            BtnResetFeedFullName.Text = "Reset";
            BtnResetFeedFullName.UseVisualStyleBackColor = true;
            BtnResetFeedFullName.Click += BtnResetFeedFullName_Click;
            // 
            // BtnResetFeedBaseName
            // 
            BtnResetFeedBaseName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnResetFeedBaseName.Location = new Point(533, 117);
            BtnResetFeedBaseName.Name = "BtnResetFeedBaseName";
            BtnResetFeedBaseName.Size = new Size(75, 23);
            BtnResetFeedBaseName.TabIndex = 117;
            BtnResetFeedBaseName.Text = "Reset";
            BtnResetFeedBaseName.UseVisualStyleBackColor = true;
            BtnResetFeedBaseName.Click += BtnResetFeedBaseName_Click;
            // 
            // FeedDetailsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(BtnResetFeedBaseName);
            Controls.Add(BtnResetFeedFullName);
            Controls.Add(TxtFeedFullName);
            Controls.Add(label6);
            Controls.Add(TxtFeedBaseName);
            Controls.Add(label5);
            Controls.Add(TxtDatabase);
            Controls.Add(RdoTableViaFeed);
            Controls.Add(RdoTableViaDir);
            Controls.Add(BtnResetTargetTable);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(TxtTargetSchema);
            Controls.Add(TxtTargetTable);
            Controls.Add(label2);
            Controls.Add(TxtFilepath);
            Controls.Add(BtnBrowse);
            Controls.Add(label1);
            Name = "FeedDetailsForm";
            Text = "FeedDetailsForm";
            Load += FeedDetailsForm_Load;
            Controls.SetChildIndex(TxtRubric, 0);
            Controls.SetChildIndex(BtnBack, 0);
            Controls.SetChildIndex(BtnNext, 0);
            Controls.SetChildIndex(label1, 0);
            Controls.SetChildIndex(BtnBrowse, 0);
            Controls.SetChildIndex(TxtFilepath, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(TxtTargetTable, 0);
            Controls.SetChildIndex(TxtTargetSchema, 0);
            Controls.SetChildIndex(label3, 0);
            Controls.SetChildIndex(label4, 0);
            Controls.SetChildIndex(BtnResetTargetTable, 0);
            Controls.SetChildIndex(RdoTableViaDir, 0);
            Controls.SetChildIndex(RdoTableViaFeed, 0);
            Controls.SetChildIndex(TxtDatabase, 0);
            Controls.SetChildIndex(label5, 0);
            Controls.SetChildIndex(TxtFeedBaseName, 0);
            Controls.SetChildIndex(label6, 0);
            Controls.SetChildIndex(TxtFeedFullName, 0);
            Controls.SetChildIndex(BtnResetFeedFullName, 0);
            Controls.SetChildIndex(BtnResetFeedBaseName, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button BtnBrowse;
        private TextBox TxtFilepath;
        private Label label2;
        private TextBox TxtTargetTable;
        private TextBox TxtTargetSchema;
        private Label label3;
        private Label label4;
        private Button BtnResetTargetTable;
        private RadioButton RdoTableViaDir;
        private RadioButton RdoTableViaFeed;
        private TextBox TxtDatabase;
        private Label label5;
        private TextBox TxtFeedBaseName;
        private Label label6;
        private TextBox TxtFeedFullName;
        private Button BtnResetFeedFullName;
        private Button BtnResetFeedBaseName;
    }
}