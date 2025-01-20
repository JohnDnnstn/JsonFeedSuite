using GenericJsonSuite;
using GenericJsonWizard.BackingData.ColumnMetadata;
using GenericJsonWizard.EtlaToolbelt.Scripts;
using System.Text;

namespace GenericJsonWizard.Models.ColumnMetadata;

internal class MetadataModel : Script
{
    private Metadata _Data { get; init; }

    internal MetadataModel(Metadata meta)
    { 
        _Data = meta;
    }

    internal virtual string Defn(bool addDefaults = true)
    {
        Scr($"{_Data.SqlName} {_Data.SqlType} {(!_Data.Nullable ? "NOT " : "")}NULL", false);
        if (addDefaults && _Data.Default.IsBlack()) { Scr($" {_Data.Default}", false); }
        return ReturnAndClear();
    }

    internal string WikiDefn()
    {
        var note = _Data.Description ?? _Data.SqlName;
        return $"|{_Data.SqlName}|{_Data.SqlType}|{note}|";
    }

    internal string Dbml()
    {
        string note = "";
        if (_Data.Description.IsBlack()) { note = $", note:'{_Data.Description}'"; }
        return $"{_Data.SqlName} {_Data.SqlType} [{(!_Data.Nullable ? "not " : "")}null{(_Data.IsId ? ", increment" : "")}{note}]";
    }
}

public static class MetadataExtensions
{
    public static string JoinSqlNames(this List<Metadata> list, string separator = ", ")
    {
        StringBuilder builder = new();
        bool first = true;
        foreach (var meta in list)
        {
            if (first) { first = false; } else { builder.Append(separator); }
            builder.Append(meta.SqlName);
        }
        return builder.ToString();
    }

    public static string EquiJoin(this List<Metadata> list, string prefix1="", string prefix2="")
    {
        if (prefix1.IsBlack()) { prefix1 += "."; }
        if (prefix2.IsBlack()) { prefix2 += "."; }
        StringBuilder builder = new();
        bool first = true;
        foreach (var meta in list)
        {
            if (first) { first = false; } else { builder.Append(" AND"); }
            builder.Append($"{prefix1}{meta.SqlName} = {prefix2}{meta.SqlName}");
        }
        return builder.ToString();
    }
}