using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.Wizards;

namespace GenericJsonWizard.Forms;

public partial class WelcomeForm : BaseWizardForm
{
    public WelcomeForm(WelcomeData backingData)
    {
        InitializeComponent();
        TxtRubric.Text = "Application to create SQL scripts for loading JSON data";

        BtnBack.Visible = BtnBack.Enabled = false;

        // Associate form controls with the backing data object
        Map = new FormMapper(backingData);
        Map.Add(RdoNewFromJsonSchema, nameof(backingData.CreateNewFeedFromJsonSchema));
        Map.Add(RdoCreateNewFromJson, nameof(backingData.CreateNewFeedFromJson));
        Map.Add(RdoAmendFeed, nameof(backingData.AmendExistingFeed));
    }

    private void LoadFeeds()
    {
        var temp = new List<string>(ChosenData.FeedList);
        temp.Reverse();
        CmbFeeds.Items.Clear();
        CmbFeeds.Items.AddRange([.. temp]);
    }

    private void SetVisibility()
    {
        CmbFeeds.Visible = CmbFeeds.Enabled = RdoAmendFeed.Checked;
    }

    private void WelcomeForm_Load(object sender, EventArgs e)
    {
        LoadFeeds();
        SetVisibility();
    }

    private void RdoAmendFeed_CheckedChanged(object sender, EventArgs e)
    {
        SetVisibility();
    }

    private void BtnNext_Click(object sender, EventArgs e)
    {
        if (IsInvalid(out string? msg))
        {
            MessageBox.Show(msg);
            DialogResult = DialogResult.None;
            return;
        }

        if (RdoAmendFeed.Checked) { ChosenData.LoadFeedData(CmbFeeds.Text); }

        Hide();

        var wizard = new MainWizard();
        var btn = wizard.Show(this);
        DialogResult = (DialogResult)btn;
    }
}
