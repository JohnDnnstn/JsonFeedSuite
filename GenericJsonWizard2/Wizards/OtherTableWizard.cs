using GenericJsonSuite.EtlaToolbelt.Wizards;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.Forms;

namespace GenericJsonWizard.Wizards;

public class OtherTableWizard : RepeatingWizard<OtherTableForm, OtherTableData>
{
    public OtherTableWizard() : base(ChosenData.OtherTables) { }

    public override OtherTableForm CreateForm(OtherTableData item)
    {
        return new OtherTableForm(item);
    }
}
