namespace GenericJsonWizard.Forms
{
    partial class ScriptForm
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
            groupBox1 = new GroupBox();
            BtnPostTransfer = new Button();
            BtnPreTransfer = new Button();
            TxtCustomPostTransfer = new TextBox();
            TxtCustomPreTransfer = new TextBox();
            ChkCustomPostTransfer = new CheckBox();
            ChkStandard = new CheckBox();
            ChkCustomPreTransfer = new CheckBox();
            groupBox2 = new GroupBox();
            BtnDataSources = new Button();
            ChkDataSources = new CheckBox();
            ChkSkipGeneration = new CheckBox();
            BtnRoles = new Button();
            ChkTimings = new CheckBox();
            TxtStagingSchemaFile = new TextBox();
            label2 = new Label();
            TxtTargetSchemaFile = new TextBox();
            label1 = new Label();
            BtnOtherScripts = new Button();
            TxtScriptPrefix = new TextBox();
            BtnRenameScripts = new Button();
            BtnGenerateScript = new Button();
            groupBox3 = new GroupBox();
            BtnSaveNow = new Button();
            ChkSaveChoices = new CheckBox();
            BtnArgs = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // BtnNext
            // 
            BtnNext.Enabled = false;
            BtnNext.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            BtnNext.Click += BtnNext_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(BtnPostTransfer);
            groupBox1.Controls.Add(BtnPreTransfer);
            groupBox1.Controls.Add(TxtCustomPostTransfer);
            groupBox1.Controls.Add(TxtCustomPreTransfer);
            groupBox1.Controls.Add(ChkCustomPostTransfer);
            groupBox1.Controls.Add(ChkStandard);
            groupBox1.Controls.Add(ChkCustomPreTransfer);
            groupBox1.Location = new Point(14, 66);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(907, 112);
            groupBox1.TabIndex = 100;
            groupBox1.TabStop = false;
            groupBox1.Text = "Mechanisms for transferring data from Staging table to Target table";
            // 
            // BtnPostTransfer
            // 
            BtnPostTransfer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnPostTransfer.Enabled = false;
            BtnPostTransfer.Location = new Point(792, 85);
            BtnPostTransfer.Name = "BtnPostTransfer";
            BtnPostTransfer.Size = new Size(100, 23);
            BtnPostTransfer.TabIndex = 6;
            BtnPostTransfer.Text = "Script post-Trn";
            BtnPostTransfer.UseVisualStyleBackColor = true;
            // 
            // BtnPreTransfer
            // 
            BtnPreTransfer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnPreTransfer.Enabled = false;
            BtnPreTransfer.Location = new Point(792, 19);
            BtnPreTransfer.Name = "BtnPreTransfer";
            BtnPreTransfer.Size = new Size(100, 23);
            BtnPreTransfer.TabIndex = 5;
            BtnPreTransfer.Text = "Script pre-Trn";
            BtnPreTransfer.UseVisualStyleBackColor = true;
            // 
            // TxtCustomPostTransfer
            // 
            TxtCustomPostTransfer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtCustomPostTransfer.Location = new Point(179, 86);
            TxtCustomPostTransfer.Name = "TxtCustomPostTransfer";
            TxtCustomPostTransfer.Size = new Size(606, 23);
            TxtCustomPostTransfer.TabIndex = 4;
            // 
            // TxtCustomPreTransfer
            // 
            TxtCustomPreTransfer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtCustomPreTransfer.Location = new Point(184, 20);
            TxtCustomPreTransfer.Name = "TxtCustomPreTransfer";
            TxtCustomPreTransfer.Size = new Size(601, 23);
            TxtCustomPreTransfer.TabIndex = 3;
            // 
            // ChkCustomPostTransfer
            // 
            ChkCustomPostTransfer.AutoSize = true;
            ChkCustomPostTransfer.Location = new Point(9, 88);
            ChkCustomPostTransfer.Name = "ChkCustomPostTransfer";
            ChkCustomPostTransfer.Size = new Size(160, 19);
            ChkCustomPostTransfer.TabIndex = 2;
            ChkCustomPostTransfer.Text = "Custom post-Targets SQL";
            ChkCustomPostTransfer.UseVisualStyleBackColor = true;
            ChkCustomPostTransfer.CheckedChanged += ChkCustomPostTransfer_CheckedChanged;
            // 
            // ChkStandard
            // 
            ChkStandard.AutoSize = true;
            ChkStandard.Location = new Point(9, 55);
            ChkStandard.Name = "ChkStandard";
            ChkStandard.Size = new Size(301, 19);
            ChkStandard.TabIndex = 1;
            ChkStandard.Text = "Standard Transfer Mechanism from staging to target";
            ChkStandard.UseVisualStyleBackColor = true;
            // 
            // ChkCustomPreTransfer
            // 
            ChkCustomPreTransfer.AutoSize = true;
            ChkCustomPreTransfer.Location = new Point(9, 22);
            ChkCustomPreTransfer.Name = "ChkCustomPreTransfer";
            ChkCustomPreTransfer.Size = new Size(154, 19);
            ChkCustomPreTransfer.TabIndex = 0;
            ChkCustomPreTransfer.Text = "Custom pre-Targets SQL";
            ChkCustomPreTransfer.UseVisualStyleBackColor = true;
            ChkCustomPreTransfer.CheckedChanged += ChkCustomPreTransfer_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(BtnDataSources);
            groupBox2.Controls.Add(ChkDataSources);
            groupBox2.Controls.Add(ChkSkipGeneration);
            groupBox2.Controls.Add(BtnRoles);
            groupBox2.Controls.Add(ChkTimings);
            groupBox2.Controls.Add(TxtStagingSchemaFile);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(TxtTargetSchemaFile);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(BtnOtherScripts);
            groupBox2.Controls.Add(TxtScriptPrefix);
            groupBox2.Controls.Add(BtnRenameScripts);
            groupBox2.Controls.Add(BtnGenerateScript);
            groupBox2.Location = new Point(12, 200);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(907, 181);
            groupBox2.TabIndex = 101;
            groupBox2.TabStop = false;
            groupBox2.Text = "Script Generation";
            // 
            // BtnDataSources
            // 
            BtnDataSources.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnDataSources.Enabled = false;
            BtnDataSources.Location = new Point(794, 149);
            BtnDataSources.Name = "BtnDataSources";
            BtnDataSources.Size = new Size(100, 24);
            BtnDataSources.TabIndex = 12;
            BtnDataSources.Text = "DataSources";
            BtnDataSources.UseVisualStyleBackColor = true;
            // 
            // ChkDataSources
            // 
            ChkDataSources.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ChkDataSources.AutoSize = true;
            ChkDataSources.Location = new Point(664, 153);
            ChkDataSources.Name = "ChkDataSources";
            ChkDataSources.Size = new Size(93, 19);
            ChkDataSources.TabIndex = 11;
            ChkDataSources.Text = "Data sources";
            ChkDataSources.UseVisualStyleBackColor = true;
            // 
            // ChkSkipGeneration
            // 
            ChkSkipGeneration.AutoSize = true;
            ChkSkipGeneration.Location = new Point(11, 156);
            ChkSkipGeneration.Name = "ChkSkipGeneration";
            ChkSkipGeneration.Size = new Size(209, 19);
            ChkSkipGeneration.TabIndex = 10;
            ChkSkipGeneration.Text = "Skip the SQL script generation step";
            ChkSkipGeneration.UseVisualStyleBackColor = true;
            // 
            // BtnRoles
            // 
            BtnRoles.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnRoles.Enabled = false;
            BtnRoles.Location = new Point(794, 114);
            BtnRoles.Name = "BtnRoles";
            BtnRoles.Size = new Size(100, 24);
            BtnRoles.TabIndex = 9;
            BtnRoles.Text = "Roles";
            BtnRoles.UseVisualStyleBackColor = true;
            // 
            // ChkTimings
            // 
            ChkTimings.AutoSize = true;
            ChkTimings.Location = new Point(11, 119);
            ChkTimings.Name = "ChkTimings";
            ChkTimings.Size = new Size(300, 19);
            ChkTimings.TabIndex = 8;
            ChkTimings.Text = "Add Timing instrumentation in the Process function";
            ChkTimings.UseVisualStyleBackColor = true;
            // 
            // TxtStagingSchemaFile
            // 
            TxtStagingSchemaFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtStagingSchemaFile.Location = new Point(184, 85);
            TxtStagingSchemaFile.Name = "TxtStagingSchemaFile";
            TxtStagingSchemaFile.Size = new Size(718, 23);
            TxtStagingSchemaFile.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(53, 89);
            label2.Name = "label2";
            label2.Size = new Size(113, 15);
            label2.TabIndex = 6;
            label2.Text = "Staging Schema File";
            // 
            // TxtTargetSchemaFile
            // 
            TxtTargetSchemaFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtTargetSchemaFile.Location = new Point(184, 58);
            TxtTargetSchemaFile.Name = "TxtTargetSchemaFile";
            TxtTargetSchemaFile.Size = new Size(718, 23);
            TxtTargetSchemaFile.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(63, 61);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 4;
            label1.Text = "Target Schema File";
            // 
            // BtnOtherScripts
            // 
            BtnOtherScripts.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnOtherScripts.Location = new Point(794, 22);
            BtnOtherScripts.Name = "BtnOtherScripts";
            BtnOtherScripts.Size = new Size(100, 27);
            BtnOtherScripts.TabIndex = 3;
            BtnOtherScripts.Text = "Other Scripts";
            BtnOtherScripts.UseVisualStyleBackColor = true;
            BtnOtherScripts.Click += BtnOtherScripts_Click;
            // 
            // TxtScriptPrefix
            // 
            TxtScriptPrefix.Enabled = false;
            TxtScriptPrefix.Location = new Point(421, 25);
            TxtScriptPrefix.Name = "TxtScriptPrefix";
            TxtScriptPrefix.Size = new Size(30, 23);
            TxtScriptPrefix.TabIndex = 2;
            TxtScriptPrefix.Text = "01_";
            TxtScriptPrefix.Visible = false;
            // 
            // BtnRenameScripts
            // 
            BtnRenameScripts.Enabled = false;
            BtnRenameScripts.Location = new Point(315, 23);
            BtnRenameScripts.Name = "BtnRenameScripts";
            BtnRenameScripts.Size = new Size(100, 25);
            BtnRenameScripts.TabIndex = 1;
            BtnRenameScripts.Text = "Rename Scripts";
            BtnRenameScripts.UseVisualStyleBackColor = true;
            BtnRenameScripts.Visible = false;
            BtnRenameScripts.Click += BtnRenameScripts_Click;
            // 
            // BtnGenerateScript
            // 
            BtnGenerateScript.Location = new Point(8, 22);
            BtnGenerateScript.Name = "BtnGenerateScript";
            BtnGenerateScript.Size = new Size(169, 27);
            BtnGenerateScript.TabIndex = 0;
            BtnGenerateScript.Text = "Generate Script";
            BtnGenerateScript.UseVisualStyleBackColor = true;
            BtnGenerateScript.Click += BtnGenerateScript_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(BtnSaveNow);
            groupBox3.Controls.Add(ChkSaveChoices);
            groupBox3.Location = new Point(14, 387);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(499, 52);
            groupBox3.TabIndex = 102;
            groupBox3.TabStop = false;
            groupBox3.Text = "Do you want to save your choices for this feed (saved when you click the Finished button)?";
            // 
            // BtnSaveNow
            // 
            BtnSaveNow.Location = new Point(182, 16);
            BtnSaveNow.Name = "BtnSaveNow";
            BtnSaveNow.Size = new Size(88, 27);
            BtnSaveNow.TabIndex = 1;
            BtnSaveNow.Text = "Save Now";
            BtnSaveNow.UseVisualStyleBackColor = true;
            BtnSaveNow.Click += BtnSaveNow_Click;
            // 
            // ChkSaveChoices
            // 
            ChkSaveChoices.AutoSize = true;
            ChkSaveChoices.Checked = true;
            ChkSaveChoices.CheckState = CheckState.Checked;
            ChkSaveChoices.Location = new Point(9, 21);
            ChkSaveChoices.Name = "ChkSaveChoices";
            ChkSaveChoices.Size = new Size(95, 19);
            ChkSaveChoices.TabIndex = 0;
            ChkSaveChoices.Text = "Save Choices";
            ChkSaveChoices.UseVisualStyleBackColor = true;
            // 
            // BtnArgs
            // 
            BtnArgs.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnArgs.Enabled = false;
            BtnArgs.Location = new Point(806, 404);
            BtnArgs.Name = "BtnArgs";
            BtnArgs.Size = new Size(100, 25);
            BtnArgs.TabIndex = 103;
            BtnArgs.Text = "Args";
            BtnArgs.UseVisualStyleBackColor = true;
            // 
            // ScriptForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(BtnArgs);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Location = new Point(12, 200);
            Name = "ScriptForm";
            Text = "ScriptForm";
            Controls.SetChildIndex(TxtRubric, 0);
            Controls.SetChildIndex(BtnBack, 0);
            Controls.SetChildIndex(BtnNext, 0);
            Controls.SetChildIndex(groupBox1, 0);
            Controls.SetChildIndex(groupBox2, 0);
            Controls.SetChildIndex(groupBox3, 0);
            Controls.SetChildIndex(BtnArgs, 0);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private CheckBox ChkCustomPostTransfer;
        private CheckBox ChkStandard;
        private CheckBox ChkCustomPreTransfer;
        private TextBox TxtCustomPostTransfer;
        private TextBox TxtCustomPreTransfer;
        private Button BtnPostTransfer;
        private Button BtnPreTransfer;
        private GroupBox groupBox2;
        private Button BtnRenameScripts;
        private Button BtnGenerateScript;
        private TextBox TxtTargetSchemaFile;
        private Label label1;
        private Button BtnOtherScripts;
        private TextBox TxtScriptPrefix;
        private CheckBox ChkTimings;
        private TextBox TxtStagingSchemaFile;
        private Label label2;
        private CheckBox ChkDataSources;
        private CheckBox ChkSkipGeneration;
        private Button BtnRoles;
        private Button BtnDataSources;
        private GroupBox groupBox3;
        private CheckBox ChkSaveChoices;
        private Button BtnSaveNow;
        private Button BtnArgs;
    }
}