using GenericJsonWizard.BackingData.ColumnMetadata;
using GenericJsonWizard.EtlaToolbelt.Wizards;
using static GenericJsonWizard.ChosenData;

namespace GenericJsonWizard.BackingData;

public class TargetTableData : TableData, IFormDataWithCreateMethod<TargetTableData, string>
{
    public bool IsStructureOnly { get; set; } = false;
    public PKVariety PKVariety { get; set; }

    #region Id related
    public override bool HasId { get { return PKVariety == PKVariety.ID; } set { } }
    #endregion

    #region BackfillId related properties
    public bool HasBackfillId { get; set; } = true;
    public BackfillId BackfillId { get; set; } = new("_id");

    #region Exposed BackfillId properties
    internal string BackfillIdName { get => BackfillId.SqlName; set => BackfillId.SqlName = value; }
    internal string BackfillIdType { get => BackfillId.SqlType; set => BackfillId.SqlType = value; }
    #endregion
    #endregion

    #region Physical Primary Key related
    public override List<int> PhysicalPK
    {
        get
        {
            return PKVariety switch
            {
                PKVariety.ID => [Id.Identifier],
                PKVariety.SAME => LogicalPK,
                PKVariety.DIFFERENT => DifferentPhysicalPk,
                _ => throw new Exception($"Unexpected value '{PKVariety}'of PKVariety in table {TableName}"),
            };
        }
        set { }
    }

    public List<int> DifferentPhysicalPk { get; set; } = [];
    #region Alternative representations of the Different PK
    internal List<Metadata> DifferentPhysicalPkMetadata
    {
        get { return ChosenData.GetMetadataList(DifferentPhysicalPk); }
        set { DifferentPhysicalPk = ChosenData.GetIdentifierList(value); }
    }
    internal List<object> DifferentPhysicalPkObjects
    {
        get { return ChosenData.GetObjectList(DifferentPhysicalPk); }
        set { DifferentPhysicalPk = ChosenData.GetIdentifierList(value); }
    }
    #endregion
    #endregion

    public TargetTableData() : base() { }
    public TargetTableData(string tableName) : base(tableName) { }
    public static TargetTableData Create(string tableName)
    {
        return new(tableName);
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
        if (HasBackfillId) 
        {
            BackfillId.SourceId = Id.Identifier;
            BackfillId.Table = this;
            answer.Add(BackfillId); 
        }
        return answer;
    }

}
