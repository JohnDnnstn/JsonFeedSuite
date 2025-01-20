using GenericJsonWizard.BackingData;

namespace GenericJsonWizard.Models
{
    internal class OtherTableModel : TableModel
    {
        private OtherTableData _Data {  get; set; }

        public OtherTableModel(OtherTableData data) : base(data)
        {
            _Data = data;
        }
    }
}
