namespace GenericJsonWizard.BackingData.ColumnMetadata;

public class BackfillId : Metadata
{
    internal int SourceId { get; set; } = 0;
    internal TableData? Table { get; set; } = null;


    public override string SqlType
    {
        get
        {
            if (SourceId == 0 || !ChosenData.TryGetMetadata(SourceId, out Metadata meta))
            { return "INT"; }
            else
            { return meta.SqlType; }
        }
    }

    public string Alias() { return $"backfill{Identifier}"; }

    /// <summary>Constructor used by Persistence</summary>
    public BackfillId() : base() => Init();

    public BackfillId(string name) : base(name) => Init();

    public void Init()
    {
        IsBackfillId = true;
        Variety = MetadataVariety.BACKFILLID;
    }

    public override string ToString()
    {
        return SqlName;
    }
}
