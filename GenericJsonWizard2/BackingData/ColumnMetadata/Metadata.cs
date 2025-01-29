namespace GenericJsonWizard.BackingData.ColumnMetadata;

public class Metadata
{
    public enum MetadataVariety { UNKNOWN, SYSTEM, JSON, USER_DEFINED, ID, BACKFILLID, DOMAIN_COLUMN, MULTIMAP_COLUMN };

    #region static properties
    internal static readonly Metadata Zero = new() { Identifier = 0 };
    internal static int NextIdentifier { get; set; } = 1000;
    #endregion

    // temp!!
    //public static List<Metadata> All = [];

    public int Identifier { get; set; } = -1;
    public string SqlName { get; set; } = "";
    public virtual string SqlType { get; set; } = "";
    public bool Nullable { get; set; } = true;
    public virtual string Default { get; set; } = "";

    public bool IsId { get; set; } = false;
    public bool IsBackfillId { get; set; } = false;

    public string Description { get; set; } = "";

    public MetadataVariety Variety { get; set; } = MetadataVariety.UNKNOWN;

    /// <summary>Constructor used by Persistence ONLY</summary>
    public Metadata() { }

    public Metadata(string sqlName)
    {
        Identifier = ++NextIdentifier;
        SqlName = sqlName;
        //All.Add(this);
    }

    public override string ToString()
    {
        return SqlName;
    }
}
