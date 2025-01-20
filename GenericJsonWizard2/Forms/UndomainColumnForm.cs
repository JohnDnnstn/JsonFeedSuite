using GenericJsonSuite;
using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.EtlaToolbelt.Strings;

namespace GenericJsonWizard.Forms;

public partial class UndomainColumnForm : BaseWizardForm
{
    private UndomainColumnData _BackingData { get; set; }

    public UndomainColumnForm(UndomainColumnData backingData)
    {
        InitializeComponent();
        TxtRubric.Text = $"What are your instructions for this column?";

        _BackingData = backingData;

        Map = new FormMapper(backingData);
        Map.Add(TxtColumn, nameof(backingData.ColumnName));
        Map.Add(TxtNewDomain, nameof(backingData.NewDomainName));
    }

    private void UndomainColumnForm_Load(object sender, EventArgs e)
    {
        CmbDomain.Items.Clear();
        CmbDomain.Items.Add(UndomainColumnData.NewDomain);
        CmbDomain.Items.Add(UndomainColumnData.NoDomain);
        foreach (var domain in ChosenData.DomainTables)
        {
            CmbDomain.Items.Add(domain.TableName);
        }
    }

    private void CmbDomain_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool isNewDomain = (CmbDomain.Text == UndomainColumnData.NewDomain);
        LblNewDomain.Visible = LblNewDomain.Enabled = isNewDomain;
        TxtNewDomain.Visible = TxtNewDomain.Enabled = isNewDomain;


        if (CmbDomain.SelectedIndex == -1) { return; }

        if (isNewDomain) 
        {
            if (TxtNewDomain.Text.IsWhite()) { TxtNewDomain.Text = TxtColumn.Text.GetPlural(); }
            _BackingData.Target = UndomainColumnData.NewDomain;
            _BackingData.NewDomainName = TxtNewDomain.Text;
            return; 
        }

        _BackingData.Target = CmbDomain.Text;
        _BackingData.NewDomainName = "";
    }
}
