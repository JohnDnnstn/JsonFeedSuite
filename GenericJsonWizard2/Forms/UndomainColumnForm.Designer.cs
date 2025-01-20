namespace GenericJsonWizard.Forms
{
    partial class UndomainColumnForm
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
            TxtColumn = new TextBox();
            CmbDomain = new ComboBox();
            label2 = new Label();
            LblNewDomain = new Label();
            TxtNewDomain = new TextBox();
            SuspendLayout();
            // 
            // BtnNext
            // 
            BtnNext.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 72);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 100;
            label1.Text = "Column";
            // 
            // TxtColumn
            // 
            TxtColumn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtColumn.Location = new Point(68, 69);
            TxtColumn.Name = "TxtColumn";
            TxtColumn.Size = new Size(433, 23);
            TxtColumn.TabIndex = 101;
            // 
            // CmbDomain
            // 
            CmbDomain.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CmbDomain.FormattingEnabled = true;
            CmbDomain.Location = new Point(68, 115);
            CmbDomain.Name = "CmbDomain";
            CmbDomain.Size = new Size(433, 23);
            CmbDomain.TabIndex = 102;
            CmbDomain.SelectedIndexChanged += CmbDomain_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 118);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 103;
            label2.Text = "Domain";
            // 
            // LblNewDomain
            // 
            LblNewDomain.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            LblNewDomain.AutoSize = true;
            LblNewDomain.Location = new Point(539, 118);
            LblNewDomain.Name = "LblNewDomain";
            LblNewDomain.Size = new Size(76, 15);
            LblNewDomain.TabIndex = 104;
            LblNewDomain.Text = "New Domain";
            // 
            // TxtNewDomain
            // 
            TxtNewDomain.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TxtNewDomain.Location = new Point(621, 115);
            TxtNewDomain.Name = "TxtNewDomain";
            TxtNewDomain.Size = new Size(300, 23);
            TxtNewDomain.TabIndex = 105;
            // 
            // UndomainColumnForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(TxtNewDomain);
            Controls.Add(LblNewDomain);
            Controls.Add(label2);
            Controls.Add(CmbDomain);
            Controls.Add(TxtColumn);
            Controls.Add(label1);
            Name = "UndomainColumnForm";
            Text = "UndomainColumnForm";
            Load += UndomainColumnForm_Load;
            Controls.SetChildIndex(TxtRubric, 0);
            Controls.SetChildIndex(BtnBack, 0);
            Controls.SetChildIndex(BtnNext, 0);
            Controls.SetChildIndex(label1, 0);
            Controls.SetChildIndex(TxtColumn, 0);
            Controls.SetChildIndex(CmbDomain, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(LblNewDomain, 0);
            Controls.SetChildIndex(TxtNewDomain, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox TxtColumn;
        private ComboBox CmbDomain;
        private Label label2;
        private Label LblNewDomain;
        private TextBox TxtNewDomain;
    }
}