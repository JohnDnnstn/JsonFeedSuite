using GenericJsonSuite.EtlaToolbelt.Forms;

namespace GenericJsonWizard.BackingData;

public class UndomainColumnData : IWizardData
{
    public static readonly string NewDomain = "New Domain Table";
    public static readonly string NoDomain = "No Longer tied to any Domain";

    public required DomainedColumn DomainedColumn { get; set; }

    public string ColumnName { get { return DomainedColumn?.UnderlyingColumn?.SqlName ?? ""; } set { DomainedColumn.UnderlyingColumn.SqlName = value; } }

    public string Target { get; set; } = "";

    public string NewDomainName { get; set; } = "";
}
