using GenericJsonWizard.BackingData.ColumnMetadata;

namespace GenericJsonWizard.BackingData;

public class ForeignTableData : TableData
{
    #region BackfillId related properties
    public bool HasBackfillId { get; set; } = true;
    public BackfillId BackfillId { get; set; } = new("_id");

    #region Exposed BackfillId properties
    internal string BackfillIdName { get => BackfillId.SqlName; set => BackfillId.SqlName = value; }
    internal string BackfillIdType { get => BackfillId.SqlType; set => BackfillId.SqlType = value; }
    internal int? BackfillIdSource { get => BackfillId.SourceId; set => BackfillId.SourceId = value; }
    #endregion
    #endregion

    public override List<int> LogicalPK { get => ChosenIdentifiers; set { } }
    public override List<int> PhysicalPK { get => [Id.Identifier]; set { } }

    public ForeignTableData() : base() 
    {
        HasId = true;
    }

    internal override List<Metadata> GetAllTableColumns()
    {
        List<Metadata> answer = new(ChosenMetadata);
        if (HasId) { answer.Add(Id); }
        return answer;
    }

    internal override List<BackfillId> GetBackfillIds()
    {
        List<BackfillId> answer = [];
        if (HasBackfillId) { answer.Add(BackfillId); }
        return answer;
    }
}
