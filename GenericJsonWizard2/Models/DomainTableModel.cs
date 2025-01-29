using GenericJsonWizard.BackingData;
using GenericJsonWizard.BackingData.ColumnMetadata;
using System.Text;
using System.Text.Json;

namespace GenericJsonWizard.Models;

internal class DomainTableModel : TableModel
{
    private DomainTableData _Data { get; set; }

    public DomainTableModel(DomainTableData data) : base(data) => _Data = data;

    public override string Defn(bool isLogged = true, int indent = 0)
    {
        Scr(base.Defn(isLogged, indent));
        Scr(PopulateDomain());
        return ReturnAndClear();
    }

    protected string PopulateDomain()
    {
        bool isQuotedType = false;
        foreach (var domainedCol in _Data.DomainedColumns)
        {
            JsonColumn? underlyingColumn = domainedCol.UnderlyingColumn;
            if (underlyingColumn != null && underlyingColumn.Identifier > 0)
            {
                isQuotedType |= underlyingColumn.IsQuotedType();
            }
        }

        if (_Data.PermittedValues.Count < 1) { return ""; }

        var builder = new StringBuilder();
        builder.Append($"INSERT INTO {_Data.QualifiedTableName}({_Data.DomainColumn}) VALUES\n\t");
        var format = isQuotedType ? "('{0}')" : "({0})";
        bool first = true;
        foreach (var val in _Data.PermittedValues)
        {
            if (first) { first = false; } else { builder.Append(",\n\t"); }
            builder.AppendFormat(format, val);
        }
        builder.AppendLine();
        builder.AppendLine($"ON CONFLICT({_Data.DomainColumn}) DO NOTHING;");
        return builder.ToString();
    }


    public string CheckValues(int indent)
    {
        Indent = indent;
        foreach (var col in _Data.DomainedColumns)
        {
            if (col?.UnderlyingColumn == null) { return ""; }
            Scr("CREATE TEMP TABLE all_vals AS ");
            Scr("select jsonb_path_query.jsonb_path_query #>> '{}' as val ", false);
            Scr($"from jsonb_path_query(_jsonArgs,'{col.UnderlyingColumn.JsonPathInDb}');", false);
            Scr("CREATE TEMP TABLE bad_vals AS ", 1);
            Scr($"select val from all_vals av left join {_Data.QualifiedTableName} d on av.val = d.{_Data.DomainColumn.SqlName} where val is null;", false);
            Scr("SELECT count(*) INTO _bad_count FROM bad_vals;", -1);
            Scr("IF _bad_count > 0 THEN ", 1);
            Scr($"RAISE WARNING 'Bad {col.UnderlyingColumn.SqlName} ({col.UnderlyingColumn.JsonPathInOriginal}) values: %',(select array_agg(bad_vals) from bad_vals)::text;");
            Scr("END IF;", -1);
            Scr("DROP TABLE bad_vals;");
            Scr("DROP TABLE all_vals;");
            Scr();
        }
        return ReturnAndClear();
    }

}
