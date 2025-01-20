using GenericJsonSuite;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.BackingData.ColumnMetadata;
using GenericJsonWizard.EtlaToolbelt.Strings;
using System.Text;
using System.Text.Json;

namespace GenericJsonWizard;

internal class JsonSchemaAnalyser
{
    private const string Snapshot = "__Snapshot__";
    private static List<JsonColumn> _JsonColumns = [];
    private static List<DomainTableData> _DomainTables = [];

    internal static void SavePreviousAnalysisAndReinitialiseAll()
    {
        ChosenData.SaveFeedData(Snapshot, Snapshot);
        ChosenData._State.Reset();
        _JsonColumns = [];
        _DomainTables = [];
    }

    internal static void Revert()
    {
        ChosenData.LoadFeedData(Snapshot, Snapshot);
    }

    internal static void AnalyseFile(string jsonSchemaFilepath)
    {
        SavePreviousAnalysisAndReinitialiseAll();
        using (FileStream stream = File.Open(jsonSchemaFilepath, FileMode.Open, FileAccess.Read))
        {
            using (JsonDocument originalDoc = JsonDocument.Parse(stream))
            {
                Analyse(originalDoc);
            }
        }
    }

    internal static void AnalyseString(string jsonSchemaStr)
    {
        SavePreviousAnalysisAndReinitialiseAll();
        using (JsonDocument originalDoc = JsonDocument.Parse(jsonSchemaStr))
        {
            Analyse(originalDoc);
        }
    }

    private static void Analyse(JsonDocument originalDoc)
    {
        ChosenData.Root = new("Root");
        JsonElement rootElement = originalDoc.RootElement;
        AnalyseSchemaObject(rootElement, ChosenData.Root);
        ChosenData.JsonColumns = _JsonColumns;
        ChosenData.DomainTables = _DomainTables;
    }
    private static void AnalyseSchemaObject(JsonElement jsonObject, JsonColumn parent)
    {
        foreach (JsonProperty property in jsonObject.EnumerateObject())
        {
            if (property.Name == "$schema" || property.Name == "$comment") { continue; }

            switch (property.Name)
            {
                case "type":
                    switch (property.Value.ToString())
                    {
                        case "object":
                            if (parent.JsonType != JsonValueKind.Array)
                            {
                                parent.JsonType = JsonValueKind.Object;
                                parent.SqlType = $"{GetTypeName(parent.SqlName)}";
                            }
                            break;
                        case "array":
                            parent.JsonType = JsonValueKind.Array;
                            parent.SqlType = $"{GetTypeName(parent.SqlName)}";
                            break;
                        case "string":
                            parent.JsonType = JsonValueKind.String;
                            parent.SqlType = "text";
                            break;
                        case "integer":
                            parent.JsonType = JsonValueKind.Number;
                            parent.SqlType = "int";
                            break;
                        case "boolean":
                            parent.JsonType = JsonValueKind.True;
                            parent.SqlType = "boolean";
                            break;
                    }
                    break;
                case "properties":
                    if (property.Value.ValueKind != JsonValueKind.Object) { throw new Exception("Bad json structure"); }

                    JsonElement element = property.Value;
                    foreach (JsonProperty elementProperty in element.EnumerateObject())
                    {
                        var child = new JsonColumn(TransformKey(elementProperty.Name))
                        {
                            ParentIdentifier = parent.Identifier,
                            JsonName = elementProperty.Name,
                            //Parent = parent,
                        };
                        _JsonColumns.Add(child);
                        AnalyseSchemaObject(elementProperty.Value, child);
                        parent.ChildIdentifiers.Add(child.Identifier);
                    }
                    break;
                case "items":
                    if (parent.JsonType != JsonValueKind.Array) { throw new Exception("Unexpected 'items' property - not in array"); }
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        AnalyseSchemaObject(item, parent);
                    }
                    break;
                case "required":
                    // ToDo:
                    var reqVal = property.Value;
                    if (reqVal.ValueKind != JsonValueKind.Array) { throw new Exception("Unexpected 'required' property - not in array"); }
                    foreach (var requiredItem in reqVal.EnumerateArray())
                    {
                        if (requiredItem.ValueKind != JsonValueKind.String) { throw new Exception("Bad required value - not a string"); }
                        string requiredName = requiredItem.GetString()!;
                        JsonColumn? requiredProperty = FindChildByName(parent,requiredName);
                        if (requiredProperty == null) { throw new Exception($"Required property '{requiredName}' does not exist"); }
                        requiredProperty.Nullable = false;
                    }
                    break;
                case "pattern":
                    if (property.Value.ValueKind != JsonValueKind.String) { throw new Exception("Bad json pattern - not a string"); }

                    // Horrible
                    string val = property.Value.GetString()!;
                    if (val.StartsWith("^2")) //possibly a date/timestamp
                    {
                        if (!val.Contains('T')) { parent.SqlType = "date"; }
                        else if (!val.EndsWith("Z$")) { parent.SqlType = "timestamp"; }
                        else { parent.SqlType = "timestamp with time zone"; }
                    }
                    break;
                case "enum":
                    if (property.Value.ValueKind != JsonValueKind.Array) { throw new Exception("Bad json enum - not a array"); }
                    parent.HasEnumOfPermittedValues = true;
                    PopulateEnum(parent, property.Value);
                    break;
                case "description":
                    if (property.Value.ValueKind != JsonValueKind.String) { throw new Exception("Bad json description - not a string"); }
                    parent.Description = property.Value.GetString()!;
                    break;
                case "default":
                    parent.Default = property.Value.ToString();
                    break;
                default:
                    throw new Exception($"Unrecognised JSON Schema element name {property.Name}");
            }

        }
    }

    private static void PopulateEnum(JsonColumn column, JsonElement jsonElement)
    {
        HashSet<object> values = [];
        foreach (var item in jsonElement.EnumerateArray())
        {
            string itemStr = item.ToString();
            column.PermittedValues.Add(itemStr);
            values.Add(itemStr);
        }

        DomainTableData? domainWithSamePossibleValuesExists = _DomainTables.Find(d => d.PermittedValues.SequenceEqual(values));
        if (domainWithSamePossibleValuesExists == null)
        {
            DomainTableData domain = new(column)
            {
                PermittedValues = values,
                IsReadOnly = true,
            };
            _DomainTables.Add(domain);
        }
        else
        {
            DomainedColumn domainedColummn = new(column);
            domainWithSamePossibleValuesExists.DomainedColumns.Add(domainedColummn);
        }
    }


    internal static string TransformKey(string key)
    {
        StringBuilder builder = new();
        bool previousPreviousWasUppercase = false;
        bool previousWasUppercase = true;
        foreach (char c in key)
        {
            if (char.IsUpper(c))
            {
                if (!previousWasUppercase || (previousPreviousWasUppercase && previousWasUppercase)) { builder.Append('_'); }
                builder.Append(char.ToLower(c));
                previousPreviousWasUppercase = previousWasUppercase;
                previousWasUppercase = true;
            }
            else
            {
                builder.Append(c);
                previousPreviousWasUppercase = previousWasUppercase;
                previousWasUppercase = c == '_'; // Special case, treat _ as if it was upper case to avoid adding _ if next is uppercase
            }
        }
        return builder.ToString();
    }

    public static string GetTypeName(string? str)
    {
        if (str.IsWhite()) { return "unknown_type"; }
        return str.GetSingular() + "_type";
    }


    internal class DomainInfo
    {
        internal HashSet<string> Values = [];
        internal List<JsonColumn> Properties = [];
    }

    internal static JsonColumn FindChildByName(JsonColumn parent,string jsonName)
    {
        foreach (var childIdentifier in parent.ChildIdentifiers)
        {
            JsonColumn? child = _JsonColumns.Find(m => m.Identifier == childIdentifier);
            if (child == null)
            {
                throw new Exception($"Unexpected Child Identifier {childIdentifier}");
            }
            if (child.JsonName == jsonName) { return child; }
        }
        throw new Exception($"Failed to find Child called {jsonName}");
    }
}
