using GenericJsonSuite;
using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.BackingData;

namespace GenericJsonWizard.Forms;

public partial class ScriptForm : BaseWizardForm
{
    private ScriptData _BackingData { get; set; }

    public ScriptForm(ScriptData backingData)
    {
        InitializeComponent();
        TxtRubric.Text = "Almost finished... Do you need custom SQL to transfer data from staging to target (a function is recommended), and do you want to skip the steps to generate the SQL script or save your choices?";

        _BackingData = backingData;

        // Associate form controls with the backing data object
        Map = new FormMapper(backingData);
        Map.Add(ChkCustomPreTransfer, nameof(backingData.HasCustomPreTransfer));
        Map.Add(TxtCustomPreTransfer, nameof(backingData.CustomPreTransfer));
        Map.Add(ChkStandard, nameof(backingData.HasStandardTransfer));
        Map.Add(ChkCustomPostTransfer, nameof(backingData.HasCustomPostTransfer));
        Map.Add(TxtCustomPostTransfer, nameof(backingData.CustomPostTransfer));
        Map.Add(TxtScriptPrefix, nameof(backingData.ScriptPrefix));
        Map.Add(ChkTimings, nameof(backingData.AddTimings));

        // Prime all the CheckBoxes and RadioButtons that manage the visibility of other controls
        ChkCustomPreTransfer_CheckedChanged();
        ChkCustomPostTransfer_CheckedChanged();
    }

    private void ScriptForm_Load(object sender, EventArgs e)
    {
        // Things that need resetting each time the form is loaded
        BtnRenameScripts.Visible = BtnRenameScripts.Enabled = false;
        TxtScriptPrefix.Visible = TxtScriptPrefix.Enabled = false;

        // Things with specific initial states that might change between creation in the wizard and loading
        ChkDataSources.Checked = _BackingData.HasDataSourcesTable;
        ChkDataSources.Enabled = false;
        BtnDataSources.Visible = BtnDataSources.Enabled = ChkDataSources.Checked;

    }

    private void ChkCustomPreTransfer_CheckedChanged(object? sender = null, EventArgs? e = null)
    {
        TxtCustomPreTransfer.Visible = TxtCustomPreTransfer.Enabled = ChkCustomPreTransfer.Checked;
        if (TxtCustomPreTransfer.Text.IsWhite())
        {
            TxtCustomPreTransfer.Text = $"PERFORM {_BackingData.StagingSchema}.{_BackingData.TargetSchema}_{_BackingData.FeedBaseName}_pre_transfer(_load_id,_args);";
        }
        BtnPreTransfer.Visible = BtnPreTransfer.Enabled = ChkCustomPreTransfer.Checked;
    }

    private void ChkCustomPostTransfer_CheckedChanged(object? sender = null, EventArgs? e = null)
    {
        TxtCustomPostTransfer.Visible = TxtCustomPostTransfer.Enabled = ChkCustomPostTransfer.Checked;
        if (TxtCustomPostTransfer.Text.IsWhite())
        {
            TxtCustomPostTransfer.Text = $"PERFORM {_BackingData.StagingSchema}.{_BackingData.TargetSchema}_{_BackingData.FeedBaseName}_post_transfer(_load_id,_args);";
        }
        BtnPostTransfer.Visible = BtnPostTransfer.Enabled = ChkCustomPostTransfer.Checked;

    }
    private void BtnGenerateScript_Click(object sender, EventArgs e)
    {
        using (var browser = new SaveFileDialog())
        {
            browser.Title = "Choose Generated Target Schema Script File Name";
            browser.Filter = "SQL|*.sql|Any|*.*";
            browser.InitialDirectory = Path.GetDirectoryName(_BackingData.FeedFilepath);
            browser.FileName = _BackingData.FeedBaseName + DateTime.Now.ToString("_yyyy-MM-dd_HHmm") + "_target_schema" + ".sql";
            if (browser.ShowDialog() == DialogResult.OK)
            {
                TxtTargetSchemaFile.Text = browser.FileName;
                var dir = Path.GetDirectoryName(browser.FileName);

                browser.Title = "Choose Generated Staging Schema Script File Name";
                browser.Filter = "SQL|*.sql|Any|*.*";
                browser.InitialDirectory = dir;
                browser.FileName = _BackingData.FeedBaseName + DateTime.Now.ToString("_yyyy-MM-dd_HHmm") + "_staging_schema" + ".sql";
                if (browser.ShowDialog() == DialogResult.OK)
                {
                    TxtStagingSchemaFile.Text = browser.FileName;

                    SaveFormStateToBackingData();

                    var script = Boilerplate.GenerateTargetSql();
                    File.WriteAllText(TxtTargetSchemaFile.Text, script);
                    script = Boilerplate.GenerateStagingSql();
                    File.WriteAllText(TxtStagingSchemaFile.Text, script);

                    BtnNext.Enabled = true;
                    ChkSkipGeneration.Visible = ChkSkipGeneration.Enabled = false;

                    BtnRenameScripts.Enabled = BtnRenameScripts.Visible = true;
                    TxtScriptPrefix.Enabled = TxtScriptPrefix.Visible = true;

                    BtnNext.Visible = BtnNext.Enabled = true;
                }
            }
        }

    }

    private void BtnRenameScripts_Click(object sender, EventArgs e)
    {
        var newTargetName = "1" + TxtScriptPrefix.Text + ChosenData.FeedDetails.FeedBaseName + "_target_schema.sql";
        var targetDir = Path.GetDirectoryName(TxtTargetSchemaFile.Text) ?? "";
        var newTargetPath = Path.Combine(targetDir, newTargetName);
        var targetBackup = Path.ChangeExtension(newTargetPath, ".bak");
        if (File.Exists(targetBackup)) { File.Delete(targetBackup); }
        if (File.Exists(newTargetPath)) { File.Move(newTargetPath, targetBackup); }
        File.Move(TxtTargetSchemaFile.Text, newTargetPath);
        TxtTargetSchemaFile.Text = newTargetPath;

        var newStagingName = "2" + TxtScriptPrefix.Text + ChosenData.FeedDetails.FeedBaseName + "_staging_schema.sql";
        var stagingDir = Path.GetDirectoryName(TxtStagingSchemaFile.Text) ?? "";
        var newStagingPath = Path.Combine(stagingDir, newStagingName);
        var stagingBackup = Path.ChangeExtension(newStagingPath, ".bak");
        if (File.Exists(stagingBackup)) { File.Delete(stagingBackup); }
        if (File.Exists(newStagingPath)) { File.Move(newStagingPath, stagingBackup); }
        File.Move(TxtStagingSchemaFile.Text, newStagingPath);
        TxtStagingSchemaFile.Text = newStagingPath;

        BtnRenameScripts.Enabled = BtnRenameScripts.Visible = false;
        TxtScriptPrefix.Enabled = TxtScriptPrefix.Visible = false;
    }

    private void BtnSaveNow_Click(object sender, EventArgs e)
    {
        SaveFormStateToBackingData();
        ChosenData.SaveFeedData();
        BtnNext.Visible = BtnNext.Enabled = true;
    }

    private void BtnNext_Click(object sender, EventArgs e)
    {
        if (ChkSaveChoices.Checked)
        {
            SaveFormStateToBackingData();
            ChosenData.SaveFeedData();
            if (!ChosenData.FeedList.Contains(ChosenData.FeedDetails.FeedFullName))
            {
                ChosenData.FeedList.Add(ChosenData.FeedDetails.FeedFullName);
            }
            ChosenData.SaveFeedList();
        }
    }

    private void BtnOtherScripts_Click(object sender, EventArgs e)
    {
        //using (var form = new OtherScriptsForm())
        //{
        //    var result = BaseWizard.ShowSingleton(form, this);
        //    if (result == FormResult.EXIT) { DialogResult = (DialogResult)result; }
        //}
    }
}
