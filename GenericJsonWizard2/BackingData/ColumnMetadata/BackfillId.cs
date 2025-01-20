namespace GenericJsonWizard.BackingData.ColumnMetadata;

public class BackfillId : Metadata
{
    public int? SourceId { get; set; } = null;

    public override string SqlType
    {
        get
        {
            if (SourceId == null) { return "INT"; } else { return ChosenData.GetMetadata(SourceId.Value).SqlType; }
        }
    }

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
