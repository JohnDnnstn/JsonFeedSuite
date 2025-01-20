using GenericJsonSuite.EtlaToolbelt.Wizards;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.Forms;

namespace GenericJsonWizard.Wizards;

internal class MultiMapTableWizard : RepeatingWizard<MultiMappedTableForm, XMultiMapTableData>
{
    public MultiMapTableWizard() : base(ChosenData.MultiMapTables) { }

    public override MultiMappedTableForm CreateForm(XMultiMapTableData item)
    {
        return new MultiMappedTableForm(item);
    }
}
