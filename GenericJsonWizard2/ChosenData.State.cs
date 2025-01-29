using GenericJsonWizard.BackingData;
using GenericJsonWizard.BackingData.ColumnMetadata;

namespace GenericJsonWizard;

public static partial class ChosenData
{
    public class State
    {
        public int NextMetadataIdentifier { get => ChosenData.NextMetadataIdentifier; set => ChosenData.NextMetadataIdentifier = value; }
        public bool ShowJsonEntities { get => ChosenData.ShowJsonEntities; set => ChosenData.ShowJsonEntities = value; }
        public JsonColumn? Root { get => ChosenData.Root; set => ChosenData.Root = value; }
        public List<JsonColumn> JsonColumns { get => ChosenData.JsonColumns; set => ChosenData.JsonColumns = value; }
        public List<SystemColumn> SystemColumns { get => ChosenData.SystemColumns; set => ChosenData.SystemColumns = value; }

        #region Backing for forms
        public WelcomeData WelcomeData { get => ChosenData.WelcomeData; set => ChosenData.WelcomeData = value; }
        public FeedDetailsData FeedDetails { get => ChosenData.FeedDetails; set => ChosenData.FeedDetails = value; }
        public ColumnsData ColumnsData { get => ChosenData.ColumnsData; set => ChosenData.ColumnsData = value; }

        public List<DomainTableData> DomainTables { get => ChosenData.DomainTables; set => ChosenData.DomainTables = value; }
        public List<ForeignTableData> ForeignTables { get => ChosenData.ForeignTables; set => ChosenData.ForeignTables = value; }
        public List<XMultiMapTableData> MultiMapTables { get => ChosenData.MultiMapTables; set => ChosenData.MultiMapTables = value; }
        public List<TargetTableData> TargetTables { get => ChosenData.TargetTables; set => ChosenData.TargetTables = value; }
        public List<JunctionTableData> JunctionTables { get => ChosenData.JunctionTables; set => ChosenData.JunctionTables = value; }

        public ScriptData ScriptData { get => ChosenData.ScriptData; set => ChosenData.ScriptData = value; }

        public RolesData Roles { get => ChosenData.Roles; set => ChosenData.Roles = value; }
        #endregion

        public void Reset()
        {
            Root = null;
            JsonColumns = [];
            //WelcomeData = new(); Don't reset this as the details will be the same even after we revert the analysis
            //FeedDetails = new(); Don't reset this as the details will be the same even after we revert the analysis
            ColumnsData = new();
            DomainTables = [];
            ForeignTables = [];
            MultiMapTables = [];
            TargetTables = [];
            ScriptData = new();
        }
    }

    internal static State _State { get; set; } = new();
}
