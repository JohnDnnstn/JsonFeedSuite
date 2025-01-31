using GenericJsonSuite;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.BackingData.ColumnMetadata;
using GenericJsonWizard.EtlaToolbelt.Scripts;
using GenericJsonWizard.Models.ColumnMetadata;
using System.Text;
using System.Text.Json;

namespace GenericJsonWizard.Models;

internal class TableModel : Script
{
    private TableData _Data { get; set; }

    private readonly List<MetadataModel> _AllColumns = [];

    private readonly List<string> _Cols = [];
    private readonly List<string> _Selection = [];
    private readonly HashSet<JsonColumn> _AllParents = [];
    private bool _IsFirstFromClause { get; set; }
    int _BackfillCount;

    public TableModel(TableData data)
    {
        _Data = data;
        foreach (Metadata meta in data.GetAllTableColumns())
        {
            MetadataModel model = new(meta);
            _AllColumns.Add(model);
        }
    }

    #region Table definition parts
    public virtual string Defn(bool isLogged = true, int indent = 0)
    {
        Indent = indent;
        DefnStart();
        Scr("(", false, 1);
        bool first = true;
        foreach (MetadataModel meta in _AllColumns)
        {
            if (first) { first = false; } else { Scr(",", false); }
            Scr($"{meta.Defn()}");
        }
        AddPhysicalPKDefn();
        Scr(");", -2);
        Scr(AddLogicalPkDefn());
        AddRoleRelatedAmendments();
        return ReturnAndClear();
    }

    protected virtual void DefnStart(bool isLogged = true)
    {
        Scr();
        Scr($"-- DROP TABLE IF EXISTS {_Data.QualifiedTableName} /* CASCADE */;");
        Scr();
        Scr($"CREATE {(!isLogged ? "UNLOGGED " : "")}TABLE IF NOT EXISTS {_Data.QualifiedTableName}", 1);
    }

    private void AddPhysicalPKDefn()
    {
        if (_Data.PhysicalPK.Count > 0)
        {
            Scr(",", false);
            Scr("PRIMARY KEY(");
            bool first = true;
            foreach (int identifier in _Data.PhysicalPK)
            {
                if (first) { first = false; } else { Scr(", ", false); }
                if (ChosenData.TryGetMetadata(identifier, out Metadata col)) { Scr(col.SqlName, false); }
                else { throw new Exception($"Failed to get metadata for identifier {identifier}"); }
            }
            Scr(")", false);
        }
        else { throw new Exception("No physical PK identifiers!"); }
    }

    protected virtual string AddLogicalPkDefn()
    {
        if (_Data.LogicalPK.Count < 1) { return "-- No logical PK defined"; }
        if (_Data.LogicalPK.SequenceEqual(_Data.PhysicalPK)) { return "-- logical PK same as physical pk"; }
        return $"CREATE UNIQUE INDEX IF NOT EXISTS {_Data.LogicalPkName} " +
            $"ON {_Data.QualifiedTableName}({_Data.LogicalPkMetadata.JoinSqlNames()}) " +
            ";"; 
        // "NULLS NOT DISTINCT;";  
        // JFD: ToDo: NULLS NOT DISTINCT is correct but only inplemented from PostgreSQL 15
    }

    protected void AddRoleRelatedAmendments()
    {
        Scr();
        Scr($"ALTER TABLE {_Data.QualifiedTableName} OWNER TO {ChosenData.Roles.TargetSchemaOwner};");
        Scr($"GRANT SELECT, REFERENCES, INSERT, UPDATE ON TABLE {_Data.QualifiedTableName} TO {ChosenData.Roles.StagingOwner};");
        Scr($"GRANT SELECT ON TABLE {_Data.QualifiedTableName} TO {ChosenData.Roles.DbPowerUsers};");
        if (ChosenData.FeedDetails.DbName == "epic")
        {
            Scr($"GRANT SELECT ON TABLE {_Data.QualifiedTableName} TO {ChosenData.Roles.TargetSchemaUsers};");
        }
    }
    #endregion


    #region Populate Target schema tables
    public virtual string Populate(int indent = 0)
    {
        Indent = indent;
        InitialisePopulate();
        CreateTempTableClause();
        WithClause();
        InsertClause();
        SelectClause();
        FromClause();
        BackfillJoinClause();
        OnConflictClause();
        ReturningClause();
        FinalClause();
        Scr(";");
        Scr();
        return ReturnAndClear();
    }


    protected virtual void InitialisePopulate()
    {
        foreach (Metadata meta in _Data.ChosenMetadata)
        {
            if (meta is JsonColumn col && col.Parent != null && col.Parent != JsonColumn.Zero)
            {
                _AllParents.Add(col.Parent);
                if (col.JsonType != JsonValueKind.Object)
                {
                    _Cols.Add(col.SqlName);

                    // The source table in the from clause will be t<n>
                    var selectCol = $"_t.{col.SqlName}"; 
                    _Selection.Add(selectCol);
                }
            }
            else if (meta is BackfillId backfillId)
            {
                ++_BackfillCount;
                _Cols.Add(backfillId.SqlName);

                // The source table in the from clause willl be backfill<n>
                var selection = $"{backfillId.Alias()}.{ChosenData.GetMetadata(backfillId.SourceId)}";
                _Selection.Add(selection);
            }
            else if (meta is SystemColumn sys)
            { 
                _Cols.Add(sys.SqlName);

                // System columns must know how to get a select value (e.g. a variable like _data_source_id)
                var selection  = sys.GetSelectClause();
                _Selection.Add(selection);
            }
        }
    }

    protected virtual void CreateTempTableClause()
    { 
    }

    protected virtual void WithClause()
    {
        _IsFirstFromClause = true;
        var ancestorList = GetLongestAncestorList(_Data.ChosenMetadata);
        if (ancestorList != null && ancestorList.Count > 0)
        {
            Scr("WITH _t AS");
            Scr("(", 1);
            Scr("SELECT *");
            Scr("FROM");
            foreach (JsonColumn parent in ancestorList)
            {
                if (_IsFirstFromClause)
                {
                    _IsFirstFromClause = false;
                    GetTableExpression(parent);
                }
                else
                {
                    GetLateralTableExpression(parent);
                }
            }
            Scr("),",-1);
        }
        else
        {
            Scr("WITH ");
        }
    }

    protected virtual void InsertClause()
    {
        Scr("_inserted AS ");
        Scr("(", 1);
        Scr($"INSERT INTO {_Data.QualifiedTableName}(", 1);
        Scr(PrettyJoin(_Cols, ","));
        Scr(")", -1);
    }

    protected virtual void SelectClause()
    {
        Scr("SELECT", 1);
        Scr(PrettyJoin(_Selection, ","));
    }

    protected virtual void FromClause()
    {
        Scr("FROM _t", -1);
    }

    protected virtual void BackfillJoinClause()
    {
        foreach (Metadata meta in _Data.ChosenMetadata)
        {
            if (meta is BackfillId backfillId && backfillId.Table != null)
            {
                if (_IsFirstFromClause) 
                { 
                    _IsFirstFromClause = false; 
                    Scr($"{backfillId.Alias()} t", false, -1); 
                } 
                else 
                { 
                    Scr($"LEFT JOIN {backfillId.Alias()} ON ");
                    TableModel model = new(backfillId.Table);
                    Scr(model.LogicalPkJoin("_t", backfillId.Alias()), false);
                }
            }
        }
    }

    protected void OnConflictClause()
    {
        Scr("ON CONFLICT (");
        bool first = true;
        foreach (Metadata meta in _Data.LogicalPkMetadata)
        { 
            if (first) { first = false; } else { Scr(", ", false); }
            Scr(meta.SqlName, false);
        }
        Scr(") DO NOTHING", false);
    }

    protected virtual void ReturningClause()
    {

    }

    protected virtual void FinalClause()
    { 
    }

    protected static List<JsonColumn> GetLongestAncestorList(List<Metadata> cols)
    {
        if (TableData.ChosenColsAreCompatible(cols, out JsonColumn aLongCol, out JsonColumn? anIncompatibleCol))
        { return aLongCol.AncestorList; }
        else 
        { throw new Exception($"Table has incompatible columns '{aLongCol.SqlName}' and '{anIncompatibleCol?.SqlName}'"); }
    }

    protected void GetTableExpression(JsonColumn parent)
    {
        TypeModel model = new(parent);
        string populateFn = (parent.JsonType == JsonValueKind.Array) ? "jsonb_populate_recordset" : "jsonb_populate_record";
        Scr($"{populateFn}(null::{model.Name},jsonb_extract_path(_jsonArgs,{parent.JsonVariadicPathInDbAsString})) as {GetTableAlias(parent)}");
    }

    protected void GetLateralTableExpression(JsonColumn parent)
    {
        TypeModel model = new(parent);
        string populateFn = (parent.JsonType == JsonValueKind.Array) ? "jsonb_populate_recordset" : "jsonb_populate_record";
        Scr(", LATERAL");
        Scr($"{populateFn}(null::{model.Name},{GetTableAlias(parent.Parent)}.{parent.SqlName}) as {GetTableAlias(parent)}"); ;
    }

    private static string GetTableAlias(JsonColumn parent)
    {
        return $"t{parent.Identifier}";
    }


    internal string LogicalPkJoin(string prefix1, string prefix2)
    {
        StringBuilder builder = new();
        if (prefix1.IsBlack() && !prefix1.EndsWith('.')) { prefix1 += '.'; }
        if (prefix2.IsBlack() && !prefix2.EndsWith('.')) { prefix2 += '.'; }
        bool first = true;
        foreach (Metadata meta in _Data.LogicalPkMetadata)
        {
            if (first) { first = false; } else { builder.Append(" AND "); }
            builder.Append($"{prefix1}{meta.SqlName} = {prefix2}{meta.SqlName}");
        }
        return builder.ToString();
    }

    #endregion
}

