using GenericJsonSuite.EtlaToolbelt.Wizards;
using GenericJsonWizard.Forms;

namespace GenericJsonWizard.Wizards;

internal class MainWizard : BaseWizard
{
    public MainWizard()
    {
        Forms =
        [
                new FeedDetailsForm(ChosenData.FeedDetails),
                new ColumnsForm(ChosenData.ColumnsData),
                new AllTablesForm(),
                new ScriptForm(ChosenData.ScriptData),
        ];
    }
}
