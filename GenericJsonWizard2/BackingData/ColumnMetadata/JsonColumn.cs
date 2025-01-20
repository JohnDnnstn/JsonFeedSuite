using GenericJsonSuite.EtlaToolbelt.Forms;
using System.Text.Json;

namespace GenericJsonWizard.BackingData.ColumnMetadata;

public class JsonColumn : Metadata, IWizardData, ICloneable
{
    #region static properties
    internal static new readonly JsonColumn Zero = new() { Identifier = 0 };
    #endregion

    #region public properties
    public string JsonName { get; set; } = "";
    public JsonValueKind JsonType { get; set; } = JsonValueKind.Undefined;
    internal string JsonTypeAsString
    {
        get { return JsonType.ToString(); }
        set { JsonType = (JsonValueKind)Enum.Parse(typeof(JsonValueKind), value); }
    }

    // Note: If you want to display these values using my FormMapping, then they have to be public properties
    // This is because the binding mecahnism between a List<T> and DataGridView
    // Alternatively make a DisplayableJsonColumn Type wrapper and use that in the Form, with relevant public fields
    // The wrapper would be IWizardData, ICloneable and this class would be neither with these properties dropped
    public string JsonPathInOriginal { get { return GetJsonPathInOriginal(); } }
    internal string JsonPathInDb { get { return GetJsonPathInDb(); } }
    internal string JsonVariadicPathInDb { get { return GetJsonVariadicPathInDb(); } }

    public bool Ignore { get; set; } = false;
    public string ToNull { get; set; } = "";
    public string FromNull { get; set; } = "";

    public string Standardiser { get; set; } = "";
    public string BadWhenTrue { get; set; } = "";


    public int ParentIdentifier { get; set; } = -1;
    internal JsonColumn Parent { get { ChosenData.TryGetJsonColumn(ParentIdentifier, out JsonColumn json); return json; } }
    public List<int> ChildIdentifiers { get; set; } = [];
    #endregion

    public bool HasEnumOfPermittedValues { get; set; }
    public List<string> PermittedValues = [];

    /// <summary>Constructor used by Persistence</summary>
    public JsonColumn() : base() => Init();

    public JsonColumn(string name) : base(name) => Init();

    private void Init()
    {
        Variety = MetadataVariety.JSON;
    }

    public override string ToString()
    {
        return SqlName;
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    internal virtual string GetJsonPathInOriginal()
    {
        if (this == Zero || this==ChosenData.Root || Parent == null) { return "$"; }
        var suffix = (JsonType == JsonValueKind.Array ? "[*]" : "");
        return $"{Parent.GetJsonPathInOriginal()}.{JsonName}{suffix}";
    }

    internal string GetJsonPathInDb()
    {
        if (this == Zero || this == ChosenData.Root || Parent == null) { return "$"; }
        var suffix = (JsonType == JsonValueKind.Array ? "[*]" : "");
        return $"{Parent.GetJsonPathInDb()}.{SqlName}{suffix}";
    }

    internal string GetJsonVariadicPathInDb()
    {
        if (this == Zero || Parent == null || Parent == ChosenData.Root) { return $"'{SqlName}'"; }
        return $"{Parent.GetJsonVariadicPathInDb()},'{SqlName}'";
    }

    internal bool ColIsLeafType()
    {
        switch (JsonType)
        {
            case JsonValueKind.Undefined:
            case JsonValueKind.Object:
            case JsonValueKind.Array:
                return false;
            case JsonValueKind.String:
            case JsonValueKind.Number:
            case JsonValueKind.True:
            case JsonValueKind.False:
            case JsonValueKind.Null:
                return true;
            default:
                throw new Exception($"Internal error: Bad JsonValueKind '{JsonType}' in original JSON path '{JsonPathInOriginal}'");
        }
    }
}
