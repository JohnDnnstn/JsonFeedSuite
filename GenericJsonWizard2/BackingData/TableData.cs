using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.BackingData.ColumnMetadata;

namespace GenericJsonWizard.BackingData;

public abstract class TableData : IWizardData
{
    public string SchemaName { get; set; } = "";
    public string TableName { get; set; } = "";
    internal string QualifiedTableName => $"{SchemaName}.{TableName}";

    #region Columns the user has explicitly chosen
    /// <summary>Property containing the Column Metadata identifiers of all the columns the user has explicitly chosen
    /// These probably relate to Json Metadata but may also include BackfillIds from previously defined tables
    /// </summary>
    public virtual List<int> ChosenIdentifiers { get; set; } = [];

    #region Column Lists represented in different ways
    internal List<Metadata> ChosenMetadata { 
        get { return ChosenData.GetMetadataList(ChosenIdentifiers); } 
        set { ChosenIdentifiers = ChosenData.GetIdentifierList(value); }
    }
    internal List<object> ChosenObjects {
        get { return ChosenData.GetObjectList(ChosenIdentifiers); }
        set { ChosenIdentifiers = ChosenData.GetIdentifierList(value); }
    }
    #endregion
    #endregion

    #region Identity related properties
    public virtual bool HasId { get; set; } = false;
    public Identity Id { get; set; } = new("_id");
    #region
    internal string IdName { get { return Id.SqlName; } set { Id.SqlName = value; } }
    internal string IdType { get { return Id.SqlType; } set { Id.SqlType = value; } }
    internal string IdSerialName { get { return Id.SerialName; } set { Id.SerialName = value; } }
    #endregion
    #endregion

    #region Primary keys, logical and physical
    public virtual List<int> LogicalPK { get; set; } = [];
    public virtual List<int> PhysicalPK { get; set; } = [];
    #region Index names and alternative representations
    internal string PhysicalPkName { get { return $"{TableName}_pk"; } }
    internal string LogicalPkName { 
        get {
            if (Enumerable.SequenceEqual(LogicalPK, PhysicalPK)) { return PhysicalPkName; }
            return $"{TableName}_logical_pk"; 
        } 
    }
    internal List<Metadata> LogicalPkMetadata
    {
        get { return ChosenData.GetMetadataList(LogicalPK); }
        set { LogicalPK = ChosenData.GetIdentifierList(value); }
    }
    internal List<object> LogicalPkObjects { 
        get { return ChosenData.GetObjectList(LogicalPK); } 
        set { LogicalPK = ChosenData.GetIdentifierList(value); } 
    }
    internal List<object> PhysicalPkObjects { 
        get { return ChosenData.GetObjectList(PhysicalPK); } 
        set { PhysicalPK = ChosenData.GetIdentifierList(value); } 
    }
    #endregion
    #endregion

    /// <summary>Columns that the user might chose to be part of this table.
    /// This is a property so that I can feed it into the source list of a List-2-List Control
    /// However a List-2-List control doesn't save back the source list so no set is required.
    /// </summary>
    public virtual List<Metadata> AllCandidates { get { return ChosenData.GetAllCandidateMetadata(this); } }

    /// <summary>Constructor (may be used by Deserialiser)</summary>
    public TableData()
    {
        SchemaName = ChosenData.FeedDetails.SchemaName;
    }

    #region abstract methods
    /// <summary>Returns a list containing all of this Table's column's metadata
    /// Note: This is specific to each table type 
    /// In addition to any columns the user has explicitly chosen, it may contain others such as Id-related columns or description columns
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    /// <returns>A list containing all of this Table's column's metadata</returns>
    internal abstract List<Metadata> GetAllTableColumns();
    //{
    //    List<Metadata> answer = new(ChosenMetadata);
    //    if (HasId) { answer.Add(Id); }
    //    return answer;
    //}

    /// <summary>Returns a list containing all the BackFillId metadata for the table</summary>
    /// <returns>A list conatining all the BackFillId metadata for the table</returns>
    /// <exception cref="NotImplementedException"></exception>
    internal abstract List<BackfillId> GetBackfillIds();
    //{ 
    //    throw new NotImplementedException("Should be done in derived class"); 
    //}
    #endregion
}
