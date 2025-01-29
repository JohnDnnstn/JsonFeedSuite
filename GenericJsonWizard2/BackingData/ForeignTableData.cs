using GenericJsonWizard.BackingData.ColumnMetadata;
using GenericJsonWizard.EtlaToolbelt.Wizards;

namespace GenericJsonWizard.BackingData;

public class ForeignTableData : TableData, IFormDataWithCreateMethod<ForeignTableData, string>
{
    #region BackfillId related properties
    public bool HasBackfillId { get; set; } = true;
    public BackfillId BackfillId { get; set; } = new("_id");

    #region Exposed BackfillId properties
    internal string BackfillIdName { get => BackfillId.SqlName; set => BackfillId.SqlName = value; }
    internal string BackfillIdType { get => BackfillId.SqlType; set => BackfillId.SqlType = value; }
    internal int BackfillIdSource { get => BackfillId.SourceId; set => BackfillId.SourceId = value; }
    #endregion
    #endregion

    #region Primary Key related properties
    public override List<int> LogicalPK { get => ChosenIdentifiers; set { } }
    public override List<int> PhysicalPK { get => [Id.Identifier]; set { } }
    #endregion

    #region Constructors and Factory
    /// <summary>Constructor used by Persistence</summary>
    public ForeignTableData() : base() { }

    /// <summary>Constructor used by Repeating Wizard via the Factory</summary>
    /// <param name="tableName">Initial table name; will be ""</param>
    public ForeignTableData(string tableName) : base(tableName)
    {
        HasId = true;
    }

    public static ForeignTableData Create(string tableName) {  return new(tableName); }
    #endregion

    internal override List<Metadata> GetAllTableColumns()
    {
        List<Metadata> answer = new(ChosenMetadata);
        if (HasId) { answer.Add(Id); }
        return answer;
    }

    internal override List<BackfillId> GetBackfillIds()
    {
        List<BackfillId> answer = [];
        if (HasBackfillId) 
        {
            BackfillId.SourceId = Id.Identifier;
            BackfillId.Table = this;
            answer.Add(BackfillId); 
        }
        return answer;
    }
}
