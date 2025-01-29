using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.BackingData.ColumnMetadata;

namespace GenericJsonWizard.BackingData;

public class ColumnsData : IWizardData
{
    public static bool ShowJsonEntities { get => ChosenData.ShowJsonEntities; set => ChosenData.ShowJsonEntities = value; }
    public List<int> ColumnIds { get; set; } = [];


    internal static List<JsonColumn> ColumnMetadata
    {
        get { return ChosenData.GetAllVisibleJsonColumns(); }
        //get { return GetAllSchemaColumns(ShowJsonEntities); }
        set { UpdateJsonColumns(value); }
    }


    internal void SetColumnList(List<JsonColumn> cols)
    {
        ColumnIds = [];
        foreach (JsonColumn col in cols)
        {
            ColumnIds.Add(col.Identifier);
        }
    }

    //private List<JsonColumn> GetAllSchemaColumns(bool IncludeNonLeafEntities = true)
    //{
    //    List<JsonColumn> answer = [];
    //    foreach (int identity in ColumnIds)
    //    {
    //        if (ChosenData.TryGetJsonColumn(identity, out JsonColumn col))
    //        {
    //            if (IncludeNonLeafEntities || ColIsLeafType(col))
    //            { answer.Add(col); }
    //        }
    //    }
    //    return answer;
    //}

    //private static bool ColIsLeafType(JsonColumn col)
    //{
    //    switch (col.JsonType)
    //    {
    //        case JsonValueKind.Undefined:
    //        case JsonValueKind.Object:
    //        case JsonValueKind.Array:
    //            return false;
    //        case JsonValueKind.String:
    //        case JsonValueKind.Number:
    //        case JsonValueKind.True:
    //        case JsonValueKind.False:
    //        case JsonValueKind.Null:
    //            return true;
    //        default:
    //            throw new Exception($"Internal error: Bad JsonValueKind '{col.JsonType}' in original JSON path '{col.JsonPathInOriginal}'");
    //    }
    //}


    private static void UpdateJsonColumns(List<JsonColumn> updatedList)
    {
        foreach (JsonColumn newValue in updatedList)
        {
            if (newValue is SystemColumn sysCol)
            {
                int oldIx = ChosenData.SystemColumns.FindIndex(j => j.SystemIdentifier == sysCol.SystemIdentifier);
                if (oldIx > -1) { ChosenData.SystemColumns[oldIx] = sysCol; }
            }
            else
            {
                int oldIx = ChosenData.JsonColumns.FindIndex(j => j.JsonPathInOriginal == newValue.JsonPathInOriginal);
                if (oldIx > -1) { ChosenData.JsonColumns[oldIx] = newValue; }
            }
        }
    }
}
