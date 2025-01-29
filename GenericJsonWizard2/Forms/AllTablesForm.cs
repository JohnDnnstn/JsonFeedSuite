using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.Wizards;

namespace GenericJsonWizard.Forms;

public partial class AllTablesForm : BaseWizardForm
{
    private static int DomainCount => ChosenData.DomainTables.Count;
    private static int ForeignCount => ChosenData.ForeignTables.Count;
    private static int MultiMapCount => ChosenData.MultiMapTables.Count;
    private static int OtherCount => ChosenData.TargetTables.Count;

    public AllTablesForm()
    {
        InitializeComponent();
        TxtRubric.Text = "Define any secondary tables";
    }

    private void SecondaryTablesForm_Load(object sender, EventArgs e)
    {
        UpdateCounts();
    }


    private void BtnDomains_Click(object sender, EventArgs e)
    {
        var wizard = new DomainTableWizard();
        var result = wizard.Show(this);
        if (result == FormResult.EXIT) { DialogResult = DialogResult.Cancel; }
        UpdateCounts();
    }

    private void BtnForeigns_Click(object sender, EventArgs e)
    {
        var wizard = new ForeignTableWizard();
        var result = wizard.Show(this);
        if (result == FormResult.EXIT) { DialogResult = DialogResult.Cancel; }
        UpdateCounts();
    }

    private void BtnMultiMapped_Click(object sender, EventArgs e)
    {
        var wizard = new MultiMapTableWizard();
        var result = wizard.Show(this);
        if (result == FormResult.EXIT) { DialogResult = DialogResult.Cancel; }
        UpdateCounts();
    }

    private void BtnOthers_Click(object sender, EventArgs e)
    {
        var wizard = new TargetTableWizard();
        var result = wizard.Show(this);
        if (result == FormResult.EXIT) { DialogResult = DialogResult.Cancel; }
        LblOtherCount.Text = $"({OtherCount})";
    }

    private void UpdateCounts()
    {
        LblDomainsCount.Text = $"({DomainCount})";
        LblForeignCount.Text = $"({ForeignCount})";
        LblMultiMappedCount.Text = $"({MultiMapCount})";
        LblOtherCount.Text = $"({OtherCount})";
    }

}
