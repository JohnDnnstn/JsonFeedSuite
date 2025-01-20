using GenericJsonWizard.BackingData.ColumnMetadata;
using static GenericJsonWizard.ChosenData;

namespace GenericJsonWizard.BackingData;

public class OtherTableData : TableData
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
    public override List<int> PhysicalPK { 
        get 
        {
            switch (PKVariety)
            {
                case PKVariety.ID:
                    return [Id.Identifier];
                case PKVariety.SAME:
                    return LogicalPK;
                case PKVariety.DIFFERENT:
                    return DifferentPhysicalPk;
                default:
                    throw new Exception($"Unexpected value '{PKVariety}'of PKVariety in table {TableName}");
            }
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
