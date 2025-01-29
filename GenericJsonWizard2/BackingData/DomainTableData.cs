using GenericJsonWizard.BackingData.ColumnMetadata;
using GenericJsonWizard.EtlaToolbelt.Strings;
using GenericJsonWizard.EtlaToolbelt.Wizards;
using System.Text;

namespace GenericJsonWizard.BackingData;

public class DomainTableData : TableData, IFormDataWithCreateMethod<DomainTableData, string>
{
    #region Properties
    /// <summary>The column that will exist in the Domain table</summary>
    public DomainColumn DomainColumn { get; set; }
    #region Exposed Domain column properties.  Required to allow mapping to associated form
    internal string DomainColumnSqlName { get { return DomainColumn.SqlName; } set { DomainColumn.SqlName = value; } }
    internal string DomainColumnSqlType { get { return DomainColumn.SqlType; } set { DomainColumn.SqlType = value; } }
    #endregion

    /// <summary>The source JsonColumns that should be in this Domain, and - in each case - info about any BackfillId</summary>
    public List<DomainedColumn> DomainedColumns { get; set; } = [];

    public bool IsReadOnly { get; set; } = false;
    public HashSet<object> PermittedValues { get; set; } = [];
    internal string PermittedValuesAsString { get { return GetPermittedValuesAsString(); } }

    public bool HasDescription { get; set; } = false;
    public Metadata DescriptionColumn { get; set; } = new("description") { SqlType = "text" };
    public string DescriptionName { get => DescriptionColumn.SqlName; set => DescriptionColumn.SqlName = value; }
    #endregion

    #region The Logical PK will always be the Domain column.  The Physical is either an Id if added, or the same as the logical
    public override List<int> LogicalPK { get { return [DomainColumn.Identifier]; } set { } }
    public override List<int> PhysicalPK { get { if (HasId) { return [Id.Identifier]; } else { return [DomainColumn.Identifier]; } } set { } }
    #endregion

    #region Constructors and Factory
    public static DomainTableData Create(string arg) => new(arg);

    /// <summary>Constructor used by Persistence ONLY</summary>
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public DomainTableData() { }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    /// <summary>Constructor used when Repeating Wizard creates new instance</summary>
    /// <param name="tableName">Initial value of table name; will usually be ""</param>
    public DomainTableData(string tableName)  : base(tableName)  
    {
        DomainColumn = new(TableName.GetSingular());
    }

    /// <summary>Constructor used by JsonSchemaAnalyser
    /// Called when the Json Schema Analyser finds a node with an explicit set of permitted values
    /// The resulting table name will be based on the first such node encountered withthat set of permitted values
    /// </summary>
    /// <param name="firstDomainedColumn">First json node encountered with this set of permitted values</param>
    /// <param name="preferredTableName"></param>
    public DomainTableData(JsonColumn firstDomainedColumn, string? preferredTableName) : base("")
    {
        TableName = preferredTableName ?? firstDomainedColumn.SqlName.GetPlural();
        DomainColumn = new(TableName.GetSingular())
        {
            SqlType = firstDomainedColumn.SqlType
        };

        DomainedColumn domained = new(firstDomainedColumn);
        DomainedColumns.Add(domained);

        IdName = $"{TableName.GetSingular()}_id";
    }
    #endregion

    public override List<int> ChosenIdentifiers
    {
        get
        {
            List<int> answer = [];
            answer.Add(DomainColumn.Identifier);
            if (HasDescription) { answer.Add(DescriptionColumn.Identifier); }
            return answer;
        }
    }

    public List<string> GetAllCandidateNames()
    {
        List<string> answer = [];
        foreach (var meta in AllCandidates)
        {
            var temp = meta.SqlName;
            answer.Add(temp);
        }
        return answer;
    }

    private string GetPermittedValuesAsString()
    {
        StringBuilder answer = new();
        foreach (var val in PermittedValues)
        {
            if (val != null) { answer.AppendLine(val.ToString()); }
        }
        return answer.ToString();
    }

    #region Implementations of the TableData's abstract methods
    internal override List<Metadata> GetAllTableColumns()
    {
        List<Metadata> answer = [];
        answer.Add(DomainColumn ?? Metadata.Zero);
        if (HasDescription) { answer.Add(DescriptionColumn); }
        if (HasId) { answer.Add(Id); }
        return answer;
    }

    internal override List<BackfillId> GetBackfillIds()
    {
        List<BackfillId> answer = [];
        if (HasId)
        {
            foreach (DomainedColumn domained in DomainedColumns)
            {
                if (domained.HasBackfillId) 
                {
                    domained.BackfillId.SourceId = Id.Identifier;
                    domained.BackfillId.Table = this;
                    answer.Add(domained.BackfillId); 
                }
            }
        }
        return answer;
    }

    #endregion
}
