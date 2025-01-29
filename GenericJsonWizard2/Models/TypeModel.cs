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
        Scr($"CREATE TYPE {Name} AS (", 1);
        bool first = true;
        foreach (var childId in _Data.ChildIdentifiers)
        {
            if (ChosenData.TryGetJsonColumn(childId, out JsonColumn child))
            {
                if (first) { first = false; } else { Scr(",", false); }
                switch (child.JsonType)
                {
                    case JsonValueKind.Object:
                    case JsonValueKind.Array:
                        Scr($"{child.SqlName} jsonb");
                        break;
                    default:
                        Scr($"{child.SqlName} {child.SqlType}");
                        break;
                }

            }
        }
        Scr(");", -1);
        return ReturnAndClear();
    }
}
