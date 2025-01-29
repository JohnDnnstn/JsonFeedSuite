namespace GenericJsonWizard.BackingData.ColumnMetadata;

public class SystemColumn : JsonColumn
{
    private static int NextSystemIdentifier = 1;

    public int SystemIdentifier { get; set; } = -1;

    public SystemColumn() : base() { Init(); }
    public SystemColumn(string name) : base(name)
    {
        Init();
        SystemIdentifier = NextSystemIdentifier++;
    }

    private void Init()
    {
        Variety = MetadataVariety.SYSTEM;
    }

    internal override string GetJsonPathInOriginal()
    {
        return $"$System[{SystemIdentifier}]";
    }

    internal string GetSelectClause()
    {
        if (Standardiser.Contains('('))
        {
            return $"{Standardiser} as {SqlName}";
        }
        throw new Exception($"No valid way to populate system column '{SqlName}'");
    }
}
