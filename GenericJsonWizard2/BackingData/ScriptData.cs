using GenericJsonSuite.EtlaToolbelt.Forms;

namespace GenericJsonWizard.BackingData;

public class ScriptData : IWizardData
{
    public bool HasCustomPreTransfer { get; set; } = false;
    public string CustomPreTransfer { get; set; } = "";
    public string PreTransferScript { get; set; } = "";
    public bool HasStandardTransfer { get; set; } = true;
    public bool HasCustomPostTransfer { get; set; } = false;
    public string CustomPostTransfer { get; set; } = "";
    public string PostTransferScript { get; set; } = "";

    public string ScriptPrefix { get; set; } = "01_";
    public bool AddTimings { get; set; } = true;
    public bool HasDataSourcesTable { get; set; } = true;

    public string AdditionalStagingScript { get; set; } = "";
    public string AdditionalTargetScript { get; set; } = "";

    internal string FeedFilepath => ChosenData.FeedDetails.Filepath;
    internal string StagingSchema => ChosenData.StagingSchema;
    internal string TargetSchema => ChosenData.FeedDetails.SchemaName;
    internal string FeedBaseName => ChosenData.FeedDetails.FeedBaseName;
}
