using GenericJsonWizard.BackingData;
using GenericJsonWizard.BackingData.ColumnMetadata;

namespace GenericJsonWizard;

public static partial class ChosenData
{
    #region constants and enumerations
    public static string StagingSchema { get { return "staging202303"; } }
    public static string FeedDefinitionSchema { get { return "feed_definitions"; } }

    public enum PKVariety { NONE, SAME, DIFFERENT, ID };
    #endregion

    internal static List<string> FeedList { get; set; } = [];

    public static int NextMetadataIdentifier { get => Metadata.NextIdentifier; set => Metadata.NextIdentifier = value; }

    public static bool ShowJsonEntities { get; set; } = true;
    internal static JsonColumn? Root { get; set; }
    /// <summary>The columns found in the analysis of the sourceFile</summary>
    public static List<JsonColumn> JsonColumns { get; set; } = [];
    public static List<SystemColumn> SystemColumns { get; set; } = [
        new SystemColumn("data_source_id") 
        { 
            SqlType = "smallint", 
            Standardiser ="(_data_source_id)" 
        },
        new SystemColumn("json_top_level_index")
        { 
            SqlType = "int",
            Standardiser = "(_json_top_level_index)"
        }
    ];

    #region Backing data for forms
    public static WelcomeData WelcomeData { get; set; } = new();
    public static FeedDetailsData FeedDetails { get; set; } = new();
    public static ColumnsData ColumnsData { get; set; } = new();

    public static List<DomainTableData> DomainTables { get; set; } = [];
    public static List<ForeignTableData> ForeignTables { get; set; } = [];
    public static List<XMultiMapTableData> MultiMapTables { get; set; } = [];
    public static List<TargetTableData> TargetTables { get; set; } = [];

    public static ScriptData ScriptData { get; set; } = new();

    public static RolesData Roles { get; set; } = new();
    #endregion

    public static object QualifiedProcessProc { get { return $"{StagingSchema}.{FeedDetails.FeedBaseName}_process"; } }
    public static string TargetSchema { get { return FeedDetails.SchemaName; } }

}
