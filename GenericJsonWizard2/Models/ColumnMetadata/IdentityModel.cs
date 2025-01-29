using GenericJsonWizard.BackingData.ColumnMetadata;

namespace GenericJsonWizard.Models.ColumnMetadata;

internal class IdentityModel : MetadataModel
{
    private Identity _Data { get; init; }

    internal IdentityModel(Identity meta) : base(meta)
    {
        _Data = meta;
    }
}
