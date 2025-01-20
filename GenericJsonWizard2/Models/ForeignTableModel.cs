using GenericJsonWizard.BackingData;

namespace GenericJsonWizard.Models;

internal class ForeignTableModel : TableModel
{
    private ForeignTableData _Data { get; set; }

    public ForeignTableModel(ForeignTableData data) : base(data)
    {
        _Data = data;
    }
}
