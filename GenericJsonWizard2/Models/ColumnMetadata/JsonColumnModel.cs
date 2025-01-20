using GenericJsonWizard.BackingData.ColumnMetadata;

namespace GenericJsonWizard.Models.ColumnMetadata;

internal class JsonColumnModel : MetadataModel
{
    private JsonColumn _Data { get; set; }

    public JsonColumnModel(JsonColumn data) : base(data)
    {
        _Data = data;
    }
}
