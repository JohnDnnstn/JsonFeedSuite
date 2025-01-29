using GenericJsonSuite.EtlaToolbelt.Wizards;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.Forms;

namespace GenericJsonWizard.Wizards;

public class TargetTableWizard : RepeatingWizard<TargetTableForm, TargetTableData>
{
    public TargetTableWizard() : base(ChosenData.TargetTables) { }

    public override TargetTableForm CreateForm(TargetTableData item)
    {
        return new TargetTableForm(item);
    }
}
