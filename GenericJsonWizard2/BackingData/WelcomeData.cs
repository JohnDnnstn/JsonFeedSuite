using GenericJsonSuite.EtlaToolbelt.Forms;

namespace GenericJsonWizard.BackingData;

public class WelcomeData : IWizardData
{
    /// <summary>The environment</summary>
    public string Env { get; set; } = "TEST";

    public bool CreateNewFeedFromJsonSchema { get; set; } = true;
    public bool CreateNewFeedFromJson { get; set; } = false;
    public bool AmendExistingFeed { get; set; } = false;

    public string FeedName { get; set; } = "";
}
