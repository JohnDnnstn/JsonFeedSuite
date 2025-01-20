namespace GenericJsonWizard.BackingData.ColumnMetadata;

public class DomainColumn : Metadata
{
    /// <summary>Constructor used by Persistence ONLY</summary>
    public DomainColumn() : base() => Init();

    public DomainColumn(string name) : base(name) => Init();

    private void Init()
    {
        Nullable = false;
        Variety = MetadataVariety.DOMAIN_COLUMN;
    }

    public override string ToString()
    {
        return SqlName;
    }
}
