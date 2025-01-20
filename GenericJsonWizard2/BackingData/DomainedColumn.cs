using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.BackingData.ColumnMetadata;

namespace GenericJsonWizard.BackingData
{
    public class DomainedColumn : IWizardData
    {
        public int Identifier { get; set; } = -1;

        public JsonColumn UnderlyingColumn { get { ChosenData.TryGetJsonColumn(Identifier, out JsonColumn meta); return meta; } }

        public bool HasBackfillId { get; set; } = false;
        public BackfillId BackfillId { get; set; }
        #region
        internal string BackfillIdName { get => BackfillId.SqlName; set => BackfillId.SqlName = value; }
        #endregion

        /// <summary>Constructor used by Persistence ONLY</summary>
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public DomainedColumn() { }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public DomainedColumn(JsonColumn col)
        {
           //UnderlyingColumn = col;
            Identifier = col.Identifier;
            BackfillId = new($"{col.SqlName}_id");
        }
    }
}
