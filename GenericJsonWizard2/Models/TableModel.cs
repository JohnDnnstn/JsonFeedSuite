using GenericJsonWizard.BackingData;
using GenericJsonWizard.BackingData.ColumnMetadata;
using GenericJsonWizard.EtlaToolbelt.Scripts;
using GenericJsonWizard.Models.ColumnMetadata;

namespace GenericJsonWizard.Models;

internal class TableModel : Script
{
    private TableData _Data { get; set; }

    private List<MetadataModel> _AllColumns = [];

    public TableModel(TableData data)
    {
        _Data = data;
        foreach (Metadata meta in data.GetAllTableColumns())
        {
            MetadataModel model = new(meta);
            _AllColumns.Add(model);
        }
    }

    public virtual string Defn(bool isLogged = true, int indent = 0)
    {
        Indent = indent;
        DefnPreamble();
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


    protected virtual void DefnPreamble(bool isLogged = true)
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
        return $"CREATE UNIQUE INDEX {_Data.LogicalPkName} " +
            $"ON {_Data.QualifiedTableName}({_Data.LogicalPkMetadata.JoinSqlNames()}) " +
            "NULLS NOT DISTINCT;";
    }

    protected void AddRoleRelatedAmendments()
    {
        Scr();
        Scr($"ALTER TABLE {_Data.QualifiedTableName} OWNER TO {ChosenData.Roles.TargetSchemaOwner};");
        Scr($"GRANT SELECT, REFERENCE, INSERT, UPDATE ON TABLE {_Data.QualifiedTableName} TO {ChosenData.Roles.StagingOwner};");
        Scr($"GRANT SELECT ON TABLE {_Data.QualifiedTableName} TO {ChosenData.Roles.DbPowerUsers};");
        if (ChosenData.FeedDetails.DbName == "epic")
        {
            Scr($"GRANT SELECT ON TABLE {_Data.QualifiedTableName} TO {ChosenData.Roles.TargetSchemaUsers};");
        }
    }
}

