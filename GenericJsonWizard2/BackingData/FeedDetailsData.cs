using GenericJsonSuite.EtlaToolbelt.Forms;

namespace GenericJsonWizard.BackingData;

public class FeedDetailsData : IWizardData
{
    public string Filepath { get; set; } = "";
    public string FeedBaseName { get; set; } = "";
    public string FeedFullName { get; set; } = "";
    public string TableName { get; set; } = "";
    public string SchemaName { get; set; } = "";
    public string DbName { get; set; } = "";
    public bool RdoTableViaDir { get; set; }

}
