namespace GenericJsonWizard.Forms;

partial class WelcomeForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));
        groupBox1 = new GroupBox();
        RdoCreateNewFromJson = new RadioButton();
        CmbFeeds = new ComboBox();
        RdoAmendFeed = new RadioButton();
        RdoNewFromJsonSchema = new RadioButton();
        groupBox1.SuspendLayout();
        SuspendLayout();
        // 
        // BtnNext
        // 
        BtnNext.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
        BtnNext.Click += BtnNext_Click;
        // 
        // groupBox1
        // 
        groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        groupBox1.Controls.Add(RdoCreateNewFromJson);
        groupBox1.Controls.Add(CmbFeeds);
        groupBox1.Controls.Add(RdoAmendFeed);
        groupBox1.Controls.Add(RdoNewFromJsonSchema);
        groupBox1.Location = new Point(12, 62);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(909, 230);
        groupBox1.TabIndex = 100;
        groupBox1.TabStop = false;
        groupBox1.Text = "Choose what you want to do";
        // 
        // RdoCreateNewFromJson
        // 
        RdoCreateNewFromJson.AutoSize = true;
        RdoCreateNewFromJson.Enabled = false;
        RdoCreateNewFromJson.Location = new Point(6, 66);
        RdoCreateNewFromJson.Name = "RdoCreateNewFromJson";
        RdoCreateNewFromJson.Size = new Size(170, 19);
        RdoCreateNewFromJson.TabIndex = 3;
        RdoCreateNewFromJson.TabStop = true;
        RdoCreateNewFromJson.Text = "Create new feed from JSON";
        RdoCreateNewFromJson.UseVisualStyleBackColor = true;
        // 
        // CmbFeeds
        // 
        CmbFeeds.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        CmbFeeds.FormattingEnabled = true;
        CmbFeeds.Location = new Point(146, 109);
        CmbFeeds.Name = "CmbFeeds";
        CmbFeeds.Size = new Size(734, 23);
        CmbFeeds.TabIndex = 2;
        // 
        // RdoAmendFeed
        // 
        RdoAmendFeed.AutoSize = true;
        RdoAmendFeed.Location = new Point(6, 110);
        RdoAmendFeed.Name = "RdoAmendFeed";
        RdoAmendFeed.Size = new Size(134, 19);
        RdoAmendFeed.TabIndex = 1;
        RdoAmendFeed.TabStop = true;
        RdoAmendFeed.Text = "Amend existing feed";
        RdoAmendFeed.UseVisualStyleBackColor = true;
        RdoAmendFeed.CheckedChanged += RdoAmendFeed_CheckedChanged;
        // 
        // RdoNewFromJsonSchema
        // 
        RdoNewFromJsonSchema.AutoSize = true;
        RdoNewFromJsonSchema.Location = new Point(6, 22);
        RdoNewFromJsonSchema.Name = "RdoNewFromJsonSchema";
        RdoNewFromJsonSchema.Size = new Size(215, 19);
        RdoNewFromJsonSchema.TabIndex = 0;
        RdoNewFromJsonSchema.TabStop = true;
        RdoNewFromJsonSchema.Text = "Create new feed from JSON Schema";
        RdoNewFromJsonSchema.UseVisualStyleBackColor = true;
        // 
        // WelcomeForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(933, 519);
        Controls.Add(groupBox1);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "WelcomeForm";
        Text = "Welcome";
        Load += WelcomeForm_Load;
        Controls.SetChildIndex(TxtRubric, 0);
        Controls.SetChildIndex(BtnBack, 0);
        Controls.SetChildIndex(BtnNext, 0);
        Controls.SetChildIndex(groupBox1, 0);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private GroupBox groupBox1;
    private RadioButton RdoAmendFeed;
    private RadioButton RdoNewFromJsonSchema;
    private RadioButton RdoCreateNewFromJson;
    private ComboBox CmbFeeds;
}
