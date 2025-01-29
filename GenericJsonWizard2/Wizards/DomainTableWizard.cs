using GenericJsonSuite.EtlaToolbelt.Wizards;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.Forms;

namespace GenericJsonWizard.Wizards;

internal class DomainTableWizard : RepeatingWizard<DomainTableForm, DomainTableData>
{
    public DomainTableWizard() : base(ChosenData.DomainTables) { }

    public override DomainTableForm CreateForm(DomainTableData item)
    {
        return new DomainTableForm(item);
    }
}
