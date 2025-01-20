using GenericJsonWizard.BackingData;
using GenericJsonWizard.BackingData.ColumnMetadata;
using GenericJsonWizard.EtlaToolbelt.Scripts;
using System.Text.Json;

namespace GenericJsonWizard.Models;

internal class TypeModel : Script
{
    private JsonColumn _Data { get; set; }

    public string Name { get { return $"{ChosenData.TargetSchema}.{_Data.SqlName}_type"; } }
    public TypeModel(JsonColumn data) 
    {
        _Data = data;
    }

    public string Defn(int indent)
    {
        Indent = indent;
        Scr($"DROP TYPE IF EXISTS {Name};");
        Scr($"CREATE TYPE {Name} AS (",1);
        bool first = true;
        foreach (var childId in _Data.ChildIdentifiers)
        {
            if (ChosenData.TryGetJsonColumn(childId, out JsonColumn child))
            {
                if (first) { first = false; } else { Scr(",", false); }
                if (child.JsonType == JsonValueKind.Object)
                {
                    Scr($"{child.SqlName} jsonb");
                }
                else
                {
                    Scr($"{child.SqlName} {child.SqlType}");
                }
            }
        }
        Scr(");",-1);
        return ReturnAndClear();
    }
}
