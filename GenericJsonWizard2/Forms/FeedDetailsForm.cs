using GenericJsonSuite;
using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.EtlaToolbelt.Strings;

namespace GenericJsonWizard.Forms;

public partial class FeedDetailsForm : BaseWizardForm
{
    private bool _FormLoaded { get; set; } = false;
    private bool _FeedFullNameManuallyChanged { get; set; } = false;
    private bool _TargetTableManuallyChanged { get; set; } = false;

    public FeedDetailsForm(FeedDetailsData backingData)
    {
        InitializeComponent();
        TxtRubric.Text = "Please provide the feed details";

        Map = new FormMapper(backingData);
        Map.Add(TxtFilepath, nameof(backingData.Filepath));
        Map.Add(TxtFeedBaseName, nameof(backingData.FeedBaseName));
        Map.Add(TxtFeedFullName, nameof(backingData.FeedFullName));
        Map.Add(TxtTargetTable, nameof(backingData.TableName));
        Map.Add(TxtTargetSchema, nameof(backingData.SchemaName));
        Map.Add(TxtDatabase, nameof(backingData.DbName));
        Map.Add(RdoTableViaDir, nameof(backingData.RdoTableViaDir));
    }
    private void SetDbSchemaTable()
    {
        if (TxtFilepath.Text.IsBlack() && File.Exists(TxtFilepath.Text) && !_TargetTableManuallyChanged)
        {
            List<string> split = SplitFilepath();
            int ix = (RdoTableViaDir.Checked ? 1 : 0);
            if (ix < split.Count) { TxtTargetTable.Text = split[ix++]; }
            if (ix < split.Count) { TxtTargetSchema.Text = split[ix++]; }
            if (ix < split.Count) { TxtDatabase.Text = split[ix++]; }
            if (RdoTableViaFeed.Checked) { TxtTargetTable.Text = TxtFeedBaseName.Text; }
        }
    }

    private List<string> SplitFilepath()
    {
        List<string> answer = [Path.GetFileNameWithoutExtension(TxtFilepath.Text).MakeIdentifier()];
        string? dir = Path.GetDirectoryName(TxtFilepath.Text);
        string? root = Path.GetPathRoot(TxtFilepath.Text);
        while (dir != root && dir.IsBlack())
        {
            answer.Add(Path.GetFileName(dir).MakeIdentifier());
            dir = Path.GetDirectoryName(dir);
        }
        return answer;
    }

    private string GetFeedFullNameFallback()
    {
        return TxtFeedFullName.Text = TxtTargetSchema.Text + "." + TxtFeedBaseName.Text;
    }

    private string GetTargetTableFallback()
    {
        return TxtFeedBaseName.Text.MakeIdentifier();
    }

    private void FeedDetailsForm_Load(object sender, EventArgs e)
    {
        _FormLoaded = true;

        _TargetTableManuallyChanged = TxtTargetTable.Text.IsBlack() && !TxtTargetTable.Text.Equals(GetTargetTableFallback());
        _FeedFullNameManuallyChanged = TxtFeedFullName.Text.IsBlack() && !TxtFeedFullName.Text.Equals(GetFeedFullNameFallback());

        RdoTableViaFeed.Checked = !RdoTableViaDir.Checked;
    }

    private void BtnBrowse_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog browser = new())
        {
            browser.Multiselect = false;
            if (browser.ShowDialog() == DialogResult.OK)
            {
                TxtFilepath.Text = browser.FileName;
            }
        }

    }

    private void TxtFilepath_TextChanged(object sender, EventArgs e)
    {
        if (File.Exists(TxtFilepath.Text))
        {
            TxtFilepath.ForeColor = Color.Black;
            if (!_FormLoaded) { return; }

            TxtFeedBaseName.Text = Path.GetFileNameWithoutExtension(TxtFilepath.Text).MakeIdentifier();
            SetDbSchemaTable();
        }
    }

    private void TxtFeedBaseName_TextChanged(object sender, EventArgs e)
    {
        if (!_TargetTableManuallyChanged) { TxtTargetTable.Text = GetTargetTableFallback(); }
        if (!_FeedFullNameManuallyChanged) { TxtFeedFullName.Text = GetFeedFullNameFallback(); }
    }

    private void TxtFeedFullName_TextChanged(object sender, EventArgs e)
    {
        if (TxtFeedFullName.Focused) { _FeedFullNameManuallyChanged = true; }
    }

    private void TxtTargetTable_TextChanged(object sender, EventArgs e)
    {
        if (TxtTargetTable.Focused) { _TargetTableManuallyChanged = true; }
    }

    private void TxtTargetSchema_TextChanged(object sender, EventArgs e)
    {
        if (!_FeedFullNameManuallyChanged) { TxtFeedFullName.Text = GetFeedFullNameFallback(); }
    }

    private void RdoTableViaDir_CheckedChanged(object sender, EventArgs e)
    {
        SetDbSchemaTable();
    }

    private void RdoTableViaFeed_CheckedChanged(object sender, EventArgs e)
    {
        SetDbSchemaTable();
    }

    private void BtnResetFeedBaseName_Click(object sender, EventArgs e)
    {
        TxtFeedBaseName.Text = Path.GetFileNameWithoutExtension(TxtFilepath.Text).MakeIdentifier();
    }

    private void BtnResetFeedFullName_Click(object sender, EventArgs e)
    {
        TxtFeedFullName.Text = GetFeedFullNameFallback();
        _FeedFullNameManuallyChanged = false;
    }

    private void BtnResetTargetTable_Click(object sender, EventArgs e)
    {
        TxtTargetTable.Text = GetTargetTableFallback();
        _TargetTableManuallyChanged = false;
    }

}
