using GenericJsonWizard.BackingData;
using GenericJsonWizard.BackingData.ColumnMetadata;

namespace GenericJsonWizard;

public static partial class ChosenData
{


    internal static List<Metadata> GetAllMetadata()
    {
        // Start with all the JSON Columns
        List<Metadata> answer = new(JsonColumns);
        answer.AddRange(SystemColumns);
        answer.AddRange(GetAllNonJsonMetatdata(includeNonVisibleCols: true));

        return answer;
    }

    internal static List<Metadata> GetAllCandidateMetadata(TableData thisTable)
    {
        List<Metadata> answer = [];
        answer.AddRange(GetAllVisibleJsonColumns());
        answer.AddRange(GetAllNonJsonMetatdata(includeNonVisibleCols: false, thisTable));

        return answer;
    }



    internal static bool TryGetJsonColumn(int identifier, out JsonColumn jsonCol)
    {
        JsonColumn? answer = JsonColumns.Find(m => m.Identifier == identifier);
        if (answer != null) { jsonCol = answer; return true; }

        answer = SystemColumns.Find(m => m.Identifier == identifier);
        if (answer != null) { jsonCol = answer; return true; }

        jsonCol = JsonColumn.Zero;
        return false;
    }

    internal static bool TryGetMetadata(int identifier, out Metadata meta)
    {
        Metadata? answer = GetAllMetadata().Find(m => m.Identifier == identifier);
        if (answer != null)
        {
            meta = answer;
            return true;
        }

        meta = Metadata.Zero;
        return false;
    }

    internal static Metadata GetMetadata(int identifier)
    {
        if (TryGetMetadata(identifier, out Metadata meta)) { return meta; }
        throw new Exception($"Unexpected Identifier {identifier}");
    }

    internal static List<Metadata> GetMetadataList(List<int> identifiers)
    {
        List<Metadata> answer = [];
        for (int ix = 0; ix < identifiers.Count; ++ix)
        {
            answer.Add(GetMetadata(identifiers[ix]));
        }
        return answer;
    }

    internal static List<object> GetObjectList(List<int> identifiers)
    {
        List<object> answer = [];
        for (int ix = 0; ix < identifiers.Count; ++ix)
        {
            answer.Add(GetMetadata(identifiers[ix]));
        }
        return answer;
    }

    internal static List<int> GetIdentifierList(List<Metadata> metadata)
    {
        List<int> answer = [];
        for (int ix = 0; ix < metadata.Count; ++ix) { answer.Add(metadata[ix].Identifier); }
        return answer;
    }

    internal static List<int> GetIdentifierList(List<object> objects)
    {
        List<int> answer = [];
        for (int ix = 0; ix < objects.Count; ++ix)
        {
            if (objects[ix] is Metadata meta) { answer.Add(meta.Identifier); }
            else
            {
                throw new Exception($"Object '{objects[ix]}' is not a Meatadata");
            }
        }
        return answer;
    }


    /// <summary>Returns either the full JsonColumn list or just the subset of "leaf" ones (i.e. not object or array nodes)</summary>
    /// <returns>All visible JsonColumn where what is visible depends on JsonVariety and ShowJsonEntities</returns>
    internal static List<JsonColumn> GetAllVisibleJsonColumns(bool? showJsonEntities = null)
    {
        showJsonEntities ??= ShowJsonEntities;

        List<JsonColumn> answer = [];
        foreach (JsonColumn jsonCol in JsonColumns)
        {
            if (showJsonEntities.Value || jsonCol.ColIsLeafType()) { answer.Add(jsonCol); }
        }
        answer.AddRange(SystemColumns);
        return answer;
    }

    /// <summary>Returns all the Metadata objects that are not JsonColumn
    /// Used by the GetAllMetadata and GetAllVisibleMetadata which vary according to which JsonColumn should be included
    /// </summary>
    /// <returns>All the Metadata objects that are not JsonColumn</returns>
    private static List<Metadata> GetAllNonJsonMetatdata(bool includeNonVisibleCols, TableData? thisTable = null)
    {
        List<Metadata> answer = [];
        answer.Add(Metadata.Zero);

        // Columns from Domain Tables
        // The Domain Column itself (which is NOT a JsonColumn),
        // zero or one Id
        // zero or one Description column and
        // any BackfillIds (zero or one per Domained col)
        foreach (DomainTableData domain in DomainTables)
        {
            if (domain == thisTable) { return answer; }
            if (includeNonVisibleCols) { answer.Add(domain.DomainColumn); }
            if (includeNonVisibleCols && domain.HasDescription) { answer.Add(domain.DescriptionColumn); }
            if (includeNonVisibleCols && domain.HasId) { answer.Add(domain.Id); }
            answer.AddRange(domain.GetBackfillIds());
        }

        // Columns from Foreign Tables
        // zero or one Id
        // zero or one BackfillId per Foreign table)
        foreach (ForeignTableData foreign in ForeignTables)
        {
            if (foreign == thisTable) { return answer; }
            if (includeNonVisibleCols && foreign.HasId) { answer.Add(foreign.Id); }
            answer.AddRange(foreign.GetBackfillIds());
        }

        // Columns from MultiMapTable
        // zero or one Id
        // zero or one BackfillId per mapping
        // ToDo:

        // Columns from Other tables
        // zero or one Id
        // zero or one Backfilled Id
        foreach (TargetTableData other in TargetTables)
        {
            if (other == thisTable) { return answer; }
            if (includeNonVisibleCols && other.HasId) { answer.Add(other.Id); }
            answer.AddRange(other.GetBackfillIds());
        }

        // Additional System columns
        // ToDo:

        return answer;
    }
}
