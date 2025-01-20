using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.BackingData.ColumnMetadata;
using System.Data;

namespace GenericJsonWizard.BackingData;


////Shown in Control as a line item of the grid
//internal class XMultiMapGridLineItem : ICloneable
//{
//    private XMultiMapTableData? _TableData { get; set; } = null;
//    internal int MappingIx { get; set; } = -1;
//    internal int ColumnIx { get; set; } = -1;

//    internal string Name { get => TargetColumn.SqlName; set => TargetColumn.SqlName = value; }
//    internal string Type { get => TargetColumn.SqlType; set => TargetColumn.SqlType = value; }
//    internal bool Nullable { get => TargetColumn.Nullable; set => TargetColumn.Nullable = value; }
//    internal Metadata MappedColumn { get; set; } = new("");//{ get => GetSourceCol(); set => SetSourceCol(value); }
//    internal MultiMapColumn TargetColumn { get => GetTargetColumn(); set => SetTargetColumn(value); }

//    public XMultiMapGridLineItem() { }
//    internal XMultiMapGridLineItem(int mappingIx, int columnIx, XMultiMapTableData tableData)
//    {
//        MappingIx = mappingIx;
//        ColumnIx = columnIx;
//        _TableData = tableData;
//    }

//    private Metadata GetSourceCol()
//    {
//        if (_TableData == null) 
//            { throw new Exception("Uninitialised MultiMapTableData in XMultiMapGridLineItem"); }
//        int identifier = _TableData.ColumnMaps[ColumnIx].SourceColumnIdentifiers[MappingIx];
//        return ChosenData.GetMetadata(identifier);
//    }

//    private void SetSourceCol(Metadata meta)
//    {
//        if (_TableData == null)
//            { throw new Exception("Uninitialised MultiMapTableData in XMultiMapGridLineItem"); }
//        _TableData.ColumnMaps[ColumnIx].SourceColumnIdentifiers[MappingIx] = meta.Identifier;
//    }

//    private MultiMapColumn GetTargetColumn()
//    {
//        if (_TableData == null)
//            { throw new Exception("Uninitialised MultiMapTableData in XMultiMapGridLineItem"); }
//        return _TableData.ColumnMaps[ColumnIx].TargetColumn;
//    }

//    private void SetTargetColumn(MultiMapColumn targetCol)
//    {
//        if (_TableData == null)
//            { throw new Exception("Uninitialised MultiMapTableData in XMultiMapGridLineItem"); }
//        _TableData.ColumnMaps[ColumnIx].TargetColumn = targetCol;
//    }

//    public object Clone()
//    {
//        return MemberwiseClone();
//    }
//}

///// <summary>Backing data for a tab page
///// Only the BackillId is persisted as all the other data is derived from the Table Data and Column Maps
///// </summary>
//public class XMultiMapControlData : IWizardData
//{
//    internal int MappingIx { get; set; }
//    //internal Dictionary<int, XMultiMapGridLineItem> LineItems { get; set; } = [];
//    internal List<XMultiMapGridLineItem> LineItems { get; set; } = [];

//    public XMultiMapControlData(int mappingIx)
//    {
//        MappingIx = mappingIx;
//    }

//    #region BackfillId-related
//    public bool HasBackfillId { get; set; } = false;
//    public BackfillId BackfillId { get; set; } = new("");
//    internal string BackfillIdName { get => BackfillId.SqlName; set => BackfillId.SqlName = value; }
//    #endregion
//}

public class YMultiMapControlData : IWizardData, ICloneable
{
    internal XMultiMapTableData Data { get; set; }
    internal int MappingIx { get; set; }
    internal DataTable DataTable { get; set; }

    public YMultiMapControlData(XMultiMapTableData data, int mappingIx)
    {
        Data = data;
        MappingIx = mappingIx;
        DataTable = new("MappingTable");

        CreateDataTableStructure();
    }


    public object Clone()
    {
        YMultiMapControlData answer = new(Data, MappingIx);
        return answer;
    }

    #region DataTable related
    private void CreateDataTableStructure()
    {
        DataColumn dataCol;

        dataCol = new()
        {
            DataType = typeof(string),
            ColumnName = "Name",
            ReadOnly = false,
            Unique = false
        };
        DataTable.Columns.Add(dataCol);

        dataCol = new()
        {
            DataType = typeof(string),
            ColumnName = "Type",
            ReadOnly = false,
            Unique = false
        };
        DataTable.Columns.Add(dataCol);

        dataCol = new()
        {
            DataType = typeof(bool),
            ColumnName = "Nullable",
            ReadOnly = false,
            Unique = false
        };
        DataTable.Columns.Add(dataCol);

        dataCol = new()
        {
            DataType = typeof(Metadata),
            ColumnName = "MappedColumn",
            ReadOnly = false,
            Unique = true
        };
        DataTable.Columns.Add(dataCol);

        
        //dataCol = new()
        //{
        //    DataType = typeof(int),
        //    ColumnName = "ColumnIx",
        //    ReadOnly = false,
        //    Unique = true
        //};
        //DataTable.Columns.Add(dataCol);

        //DataColumn[] PKColumns = [dataCol];
        //DataTable.PrimaryKey = PKColumns;

    }

    internal void LoadDataTable()
    {
        DataTable.Clear();
        foreach (int columnIx in Data.ColumnMaps.Keys)
        {
            XMultiMapColumnMap colMap= Data.ColumnMaps[columnIx];

            AddRow(columnIx, colMap);
        }
    }

    internal void SaveDataTable()
    {
        if (DataTable.Rows.Count > Data.ColumnMaps.Count)
        {
            int columnIx = Data.AddNewTargetColumn();
        }

        for(int ix=0; ix<DataTable.Rows.Count; ix++) 
        {
            DataRow dataRow = DataTable.Rows[ix];
            int columnIx = Data.GetColumnIx(ix);

            string name = (string)dataRow["Name"];
            string type = (string)dataRow["Type"];
            bool nullable = (bool)dataRow["Nullable"];
            Metadata sourceCol = (Metadata)dataRow["MappedColumn"];

            XMultiMapColumnMap map = Data.ColumnMaps[columnIx];
            MultiMapColumn targetCol = map.TargetColumn;
            targetCol.SqlName = name;
            targetCol.SqlType = type;
            targetCol.Nullable = nullable;
            map.SourceColumnIdentifiers[MappingIx] = sourceCol.Identifier;
        }
    }

    internal void AddRow(int columnIx, XMultiMapColumnMap colMap)
    {
        MultiMapColumn targetColumn = colMap.TargetColumn;
        int sourceColIdentifier = colMap.SourceColumnIdentifiers[MappingIx];

        DataRow dataRow = DataTable.NewRow();
        dataRow["Name"] = targetColumn.SqlName;
        dataRow["Type"] = targetColumn.SqlType;
        dataRow["Nullable"] = targetColumn.Nullable;
        dataRow["MappedColumn"] = ChosenData.GetMetadata(sourceColIdentifier);
//        dataRow["ColumnIx"] = columnIx;
        DataTable.Rows.Add(dataRow);
    }

    internal void RemoveRow(int columnIx)
    {
        DataRow? rowToDelete = DataTable.Rows.Find(columnIx);
        if (rowToDelete != null) { DataTable.Rows.Remove(rowToDelete); }
    }
    #endregion

    #region BackfillId-related
    public bool HasBackfillId { get; set; } = false;
    public BackfillId BackfillId { get; set; } = new("");
    internal string BackfillIdName { get => BackfillId.SqlName; set => BackfillId.SqlName = value; }
    #endregion
}

    /// <summary>What metadata source columns map to this target column
    /// This is where the target column is persisted
    /// </summary>
    public class XMultiMapColumnMap 
{
    public MultiMapColumn TargetColumn { get; set; } = new("");
    public Dictionary<int,int> SourceColumnIdentifiers = [];
}

/// <summary>Table-level data</summary>
public class XMultiMapTableData : TableData
{
    public int NextMappingIx { get; set; } = 0;
    public int NexColumnIx { get; set; } = 0;


    /// <summary> Key=ColumnIx, Value={targetCol,Dictionary of {MappingIx,SourceColMetadataIdentifier} }</summary>
    public SortedDictionary<int,XMultiMapColumnMap> ColumnMaps = [];

    /// <summary> Key = MappingIx, value = backing data for a tab page/// </summary>
    public SortedDictionary<int,YMultiMapControlData> ControlBackingData = [];

    public XMultiMapTableData() : base() 
    {
        if (ColumnMaps.Count < 1) { AddNewTargetColumn(); }
        if (ControlBackingData.Count < 1) { AddNewMapping(); }
    }

    public (int, YMultiMapControlData) AddNewMapping()
    {
        int mappingIx = ++NextMappingIx;
        YMultiMapControlData controlData = new(this,mappingIx);

        // Add a dummy Source Column for this mapping to all the existing target columns
        foreach (XMultiMapColumnMap colMap in ColumnMaps.Values)
        {
            colMap.SourceColumnIdentifiers.Add(mappingIx, 0);
        }

        //// Add a line item to this control for each existing target column
        //foreach (int columnIx in ColumnMaps.Keys)
        //{
        //    var item = new XMultiMapGridLineItem(mappingIx: mappingIx, columnIx: columnIx, this);
        //    controlData.LineItems.Add(item);
        //}

        ControlBackingData.Add(mappingIx, controlData);

        return (mappingIx, controlData);
    }

    public void RemoveMapping(int mappingIx)
    {
        ControlBackingData.Remove(mappingIx);

        foreach (XMultiMapColumnMap colMap in ColumnMaps.Values)
        {
            colMap.SourceColumnIdentifiers.Remove(mappingIx);
        }
    }

    public int AddNewTargetColumn()
    {
        int columnIx = ++NexColumnIx;
        var colMap = new XMultiMapColumnMap();

        // Add a number of dummy source columns associated with this target col, one for each existing tab page
        foreach(var mappingIx in ControlBackingData.Keys)
        {
            colMap.SourceColumnIdentifiers.Add(mappingIx, 0);
        }
        // Add this new targetColumn to the set of existing target columns
        ColumnMaps.Add(columnIx, colMap);

        // Add this new target column line item to each tab page
        // Yes, I know we can combine it with the one above but that muddles the logic 
        foreach (int mappingIx in ControlBackingData.Keys)
        {
            YMultiMapControlData controlData = ControlBackingData[mappingIx];
            controlData.AddRow(columnIx, colMap);
            //XMultiMapGridLineItem lineItem = new(mappingIx: mappingIx, columnIx: columnIx, this);
            //XMultiMapControlData controlData = ControlBackingData[mappingIx];
            //controlData.LineItems.Add(lineItem);
        }
        return columnIx;
    }

    public void RemoveColumn(int columnIx)
    {
        ColumnMaps.Remove(columnIx);

        foreach (YMultiMapControlData controlData in ControlBackingData.Values)
        {
            controlData.RemoveRow(columnIx);
            //var itemToRemove = controlData.LineItems.Find(i => i.ColumnIx == columnIx);
            //if (itemToRemove != null)
            //{
            //    controlData.LineItems.Remove(itemToRemove);
            //}
        }
    }

    public int GetColumnIx(int nthMap)
    {
        if (nthMap >= ColumnMaps.Count) { return AddNewTargetColumn(); }
        int ix = 0;
        foreach (int columnIx in ColumnMaps.Keys)
        {
            if (ix++ == nthMap) { return columnIx; }
        }
        throw new Exception($"Internal error: Failed to find the {nthMap}th column ix in ColumnMaps");
    }

    internal override List<Metadata> GetAllTableColumns()
    {
        List<Metadata> answer = [];
        foreach (var colMap in ColumnMaps.Values)
        {
            answer.Add(colMap.TargetColumn);
        }
        if (HasId) { answer.Add(Id); }
        return answer;
    }

    internal override List<BackfillId> GetBackfillIds()
    {
        List<BackfillId> answer = [];
        if (HasId)
        {
            foreach (YMultiMapControlData controlData in ControlBackingData.Values)
            {
                if (controlData.HasBackfillId) { answer.Add(controlData.BackfillId); }
            }
        }
        return answer;
    }
}
