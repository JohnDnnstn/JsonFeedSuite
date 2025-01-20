namespace GenericJsonWizard.BackingData.ColumnMetadata;

public class MultiMapColumn : Metadata
{
    internal new static MultiMapColumn Zero = new MultiMapColumn() { Identifier = 0 };

    /// <summary>Constructor for use by persistence and Repeating Wizard</summary>
    public MultiMapColumn() : base() { Init(); }

    public MultiMapColumn(string name) : base(name) { Init(); }

    private void Init()
    {
        Variety = MetadataVariety.MULTIMAP_COLUMN;
    }

    public override string ToString()
    {
        return SqlName;
    }
}
