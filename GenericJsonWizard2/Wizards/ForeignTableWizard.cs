using GenericJsonSuite.EtlaToolbelt.Wizards;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.Forms;

namespace GenericJsonWizard.Wizards;

internal class ForeignTableWizard : RepeatingWizard<ForeignTableForm, ForeignTableData>
{
    public ForeignTableWizard() : base(ChosenData.ForeignTables) { }

    public override ForeignTableForm CreateForm(ForeignTableData item)
    {
        return new ForeignTableForm(item);
    }
}
