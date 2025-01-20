namespace GenericJsonWizard.Forms
{
    partial class AllTablesForm
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
            LblDomainsCount = new Label();
            BtnDomains = new Button();
            groupBox2 = new GroupBox();
            LblForeignCount = new Label();
            BtnForeigns = new Button();
            groupBox3 = new GroupBox();
            LblOtherCount = new Label();
            BtnOthers = new Button();
            groupBox4 = new GroupBox();
            LblMultiMappedCount = new Label();
            BtnMultiMapped = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // BtnBack
            // 
            BtnBack.Location = new Point(12, 422);
            // 
            // BtnNext
            // 
            BtnNext.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            BtnNext.Location = new Point(713, 422);
            // 
            // TxtRubric
            // 
            TxtRubric.Size = new Size(776, 54);
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(LblDomainsCount);
            groupBox1.Controls.Add(BtnDomains);
            groupBox1.Location = new Point(12, 79);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(776, 59);
            groupBox1.TabIndex = 100;
            groupBox1.TabStop = false;
            groupBox1.Text = "Domain Tables";
            // 
            // LblDomainsCount
            // 
            LblDomainsCount.AutoSize = true;
            LblDomainsCount.Enabled = false;
            LblDomainsCount.Location = new Point(91, 26);
            LblDomainsCount.Name = "LblDomainsCount";
            LblDomainsCount.Size = new Size(13, 15);
            LblDomainsCount.TabIndex = 1;
            LblDomainsCount.Text = "0";
            // 
            // BtnDomains
            // 
            BtnDomains.Location = new Point(6, 22);
            BtnDomains.Name = "BtnDomains";
            BtnDomains.Size = new Size(75, 23);
            BtnDomains.TabIndex = 0;
            BtnDomains.Text = "Define";
            BtnDomains.UseVisualStyleBackColor = true;
            BtnDomains.Click += BtnDomains_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(LblForeignCount);
            groupBox2.Controls.Add(BtnForeigns);
            groupBox2.Location = new Point(12, 151);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(776, 59);
            groupBox2.TabIndex = 101;
            groupBox2.TabStop = false;
            groupBox2.Text = "Foreign Tables";
            // 
            // LblForeignCount
            // 
            LblForeignCount.AutoSize = true;
            LblForeignCount.Enabled = false;
            LblForeignCount.Location = new Point(91, 26);
            LblForeignCount.Name = "LblForeignCount";
            LblForeignCount.Size = new Size(13, 15);
            LblForeignCount.TabIndex = 1;
            LblForeignCount.Text = "0";
            // 
            // BtnForeigns
            // 
            BtnForeigns.Location = new Point(6, 22);
            BtnForeigns.Name = "BtnForeigns";
            BtnForeigns.Size = new Size(75, 23);
            BtnForeigns.TabIndex = 0;
            BtnForeigns.Text = "Define";
            BtnForeigns.UseVisualStyleBackColor = true;
            BtnForeigns.Click += BtnForeigns_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(LblOtherCount);
            groupBox3.Controls.Add(BtnOthers);
            groupBox3.Location = new Point(12, 295);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(776, 59);
            groupBox3.TabIndex = 102;
            groupBox3.TabStop = false;
            groupBox3.Text = "Other Tables";
            // 
            // LblOtherCount
            // 
            LblOtherCount.AutoSize = true;
            LblOtherCount.Enabled = false;
            LblOtherCount.Location = new Point(91, 26);
            LblOtherCount.Name = "LblOtherCount";
            LblOtherCount.Size = new Size(13, 15);
            LblOtherCount.TabIndex = 1;
            LblOtherCount.Text = "0";
            // 
            // BtnOthers
            // 
            BtnOthers.Location = new Point(6, 22);
            BtnOthers.Name = "BtnOthers";
            BtnOthers.Size = new Size(75, 23);
            BtnOthers.TabIndex = 0;
            BtnOthers.Text = "Define";
            BtnOthers.UseVisualStyleBackColor = true;
            BtnOthers.Click += BtnOthers_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(LblMultiMappedCount);
            groupBox4.Controls.Add(BtnMultiMapped);
            groupBox4.Location = new Point(12, 223);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(776, 59);
            groupBox4.TabIndex = 103;
            groupBox4.TabStop = false;
            groupBox4.Text = "Multi-Mapped Tables";
            // 
            // LblMultiMappedCount
            // 
            LblMultiMappedCount.AutoSize = true;
            LblMultiMappedCount.Enabled = false;
            LblMultiMappedCount.Location = new Point(91, 26);
            LblMultiMappedCount.Name = "LblMultiMappedCount";
            LblMultiMappedCount.Size = new Size(13, 15);
            LblMultiMappedCount.TabIndex = 1;
            LblMultiMappedCount.Text = "0";
            // 
            // BtnMultiMapped
            // 
            BtnMultiMapped.Location = new Point(6, 22);
            BtnMultiMapped.Name = "BtnMultiMapped";
            BtnMultiMapped.Size = new Size(75, 23);
            BtnMultiMapped.TabIndex = 0;
            BtnMultiMapped.Text = "Define";
            BtnMultiMapped.UseVisualStyleBackColor = true;
            BtnMultiMapped.Click += BtnMultiMapped_Click;
            // 
            // AllTablesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "AllTablesForm";
            Text = "Secondary Tables Form";
            Load += SecondaryTablesForm_Load;
            Controls.SetChildIndex(TxtRubric, 0);
            Controls.SetChildIndex(BtnBack, 0);
            Controls.SetChildIndex(BtnNext, 0);
            Controls.SetChildIndex(groupBox1, 0);
            Controls.SetChildIndex(groupBox2, 0);
            Controls.SetChildIndex(groupBox3, 0);
            Controls.SetChildIndex(groupBox4, 0);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Button BtnDomains;
        private Label LblDomainsCount;
        private GroupBox groupBox2;
        private Label LblForeignCount;
        private Button BtnForeigns;
        private GroupBox groupBox3;
        private Label LblOtherCount;
        private Button BtnOthers;
        private GroupBox groupBox4;
        private Button BtnMultiMapped;
        private Label LblMultiMappedCount;
    }
}