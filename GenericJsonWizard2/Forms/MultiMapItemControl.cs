using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.BackingData.ColumnMetadata;

namespace GenericJsonWizard.Forms;

public partial class MultiMapItemControl : UserControl
{
    public FormMapper Map { get; set; }
    private YMultiMapControlData _BackingData { get; set; }
    private XMultiMapTableData _TableData { get; set; }

    public int MappingIx { get; set; }

    public MultiMapItemControl(YMultiMapControlData backingData, int mappingIx, XMultiMapTableData tableData)
    {
        InitializeComponent();

        _BackingData = backingData;
        _TableData = tableData;
        MappingIx = mappingIx;
        Name = mappingIx.ToString();

        Map = new(backingData);
        //Map.Add<XMultiMapGridLineItem>(GrdMapping, nameof(backingData.LineItems));
        Map.Add(ChkBackfillId, nameof(backingData.HasBackfillId));
        Map.Add(TxtBackfillIdName, nameof(backingData.BackfillIdName));
    }

    #region Events
    /// <summary>Event to tell the parent that the user wants to remove this page</summary>
    /// <param name="key"></param>
    public delegate void RemoveMappingRequest_Handler(int key);
    public event RemoveMappingRequest_Handler? RemoveMappingRequest_Event;
    public void Raise_RemoveMappingRequest_Event() { RemoveMappingRequest_Event?.Invoke(MappingIx); }

    /// <summary>Handler for when the user has changed whether the table has an id - which affects the visibility of the Backfilled Id controls</summary>
    public void ChkHasId_Changed_Handler(bool hasId)
    {
        ChkBackfillId.Visible = ChkBackfillId.Enabled = hasId;
        TxtBackfillIdName.Visible = TxtBackfillIdName.Enabled = hasId && ChkBackfillId.Checked;
    }

    /// <summary>Event to tell the table level that a new column has been added to the target table</summary>
    /// <param name="tableColId"></param>
    public delegate void AddColumnRequest_Handler(int tableColId, int key);
    public event AddColumnRequest_Handler? AddColumnRequest_Event;
    public void Raise_AddColumn_Request_Event(int tableColId) { AddColumnRequest_Event?.Invoke(tableColId, MappingIx); }
    #endregion Events

    #region
    //public List<Metadata> GetChosenColumns()
    //{
    //    List<Metadata> answer = [];
    //    foreach (var map in _BackingData.ColumnMappings)
    //    {
    //        answer.Add(map.MappedColumn);
    //    }
    //    return answer;
    //}

    //public void RemoveBlankLines()
    //{
    //    List<DataGridViewRow> toDelete = [];
    //    foreach (DataGridViewRow row in GrdMapping.Rows)
    //    {
    //        bool isBlank = true;
    //        foreach (DataGridViewCell col in row.Cells)
    //        {
    //            if (col?.Value == null) { continue; }
    //            if (!(col.Value is bool) && col.Value.ToString().IsBlack())
    //            {
    //                isBlank = false;
    //                break;
    //            }
    //        }
    //        if (isBlank && !row.IsNewRow) { toDelete.Add(row); }
    //    }
    //    foreach (DataGridViewRow row in toDelete)
    //    {
    //        GrdMapping.Rows.Remove(row);
    //    }
    //}
    #endregion

    public void SaveData()
    {
        //RemoveBlankLines();
        Map.Save();
    }

    public void LoadData()
    {
        Map.Load();
    }

    /// <summary>Populates the ComboBox where the user chooses the columns that go in this table</summary>
    /// <returns>The list of all possible Metadata sources for a MultiMapColumn</returns>
    private List<Metadata> GetAllCandidateMetadata()
    {
        return ChosenData.GetAllCandidateMetadata(_TableData);
    }

    internal void InitGrid()
    {
        GrdMapping.Columns.Clear();

        GrdMapping.AutoGenerateColumns = true;
        GrdMapping.AutoSize = true;
        GrdMapping.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        DataGridViewColumn gridColumn = new DataGridViewTextBoxColumn
        {
            //DataSource = _BackingData.DataTable.Columns["Name"],
            DataPropertyName = "Name",
            Name = "Name",
            //            AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
            ToolTipText = "The name of the column in Multi-Mapped Table",
            FillWeight = 25,
        };
        GrdMapping.Columns.Add(gridColumn);

        gridColumn = new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Type",
            Name = "Type",
            //AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
            ToolTipText = "The type of the column in Multi-Mapped Table",
            FillWeight = 15,
        };
        GrdMapping.Columns.Add(gridColumn);

        gridColumn = new DataGridViewCheckBoxColumn
        {
            DataPropertyName = "Nullable",
            Name = "Nullable",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
            ToolTipText = "If set, this column can be null",
        };
        GrdMapping.Columns.Add(gridColumn);

        gridColumn = new DataGridViewComboBoxColumn()
        {
            DataPropertyName = "MappedColumn",
            Name = "MappedColumn",
            //AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
            ToolTipText = "Which JSON element is mapped to this column",
            FillWeight = 50,
        };
        foreach (Metadata meta in GetAllCandidateMetadata())
        {
            ((DataGridViewComboBoxColumn)gridColumn).Items.Add(meta);
        }
        GrdMapping.Columns.Add(gridColumn);

        //#region events associated with the combo box 
        //// See https://stackoverflow.com/questions/11141872/event-that-fires-during-datagridviewcomboboxcolumn-selectedindexchanged
        //GrdMapping.CellValueChanged += GrdMapping_CellValueChanged;
        //GrdMapping.CurrentCellDirtyStateChanged += GrdMapping_CurrentCellDirtyStateChanged;
        //#endregion

        GrdMapping.Visible = GrdMapping.Enabled = true;
        GrdMapping.Refresh();
    }

    public void BindGrid()
    {
        BindingSource bindingSource = new();
        bindingSource.DataSource = _BackingData.DataTable;
        GrdMapping.DataSource = bindingSource;
    }

    public void Initialise()
    {
        InitGrid();
    }

    private void MultiMapItemControl_Load(object sender, EventArgs e)
    {
        Map.Load();
        _BackingData.LoadDataTable();
        InitGrid();
        BindGrid();
    }

    private void ChkBackfillId_CheckedChanged(object sender, EventArgs e)
    {
        TxtBackfillIdName.Visible = TxtBackfillIdName.Enabled = ChkBackfillId.Checked;
    }

    private void BtnRemoveMapping_Click(object sender, EventArgs e)
    {
        Raise_RemoveMappingRequest_Event();
    }


    private void GrdMapping_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
        Console.WriteLine("error");
    }

    //private void GrdMapping_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    //{
    //    int rowIx = e.RowIndex;
    //    int colIx = e.ColumnIndex;
    //    DataGridViewCell cell = GrdMapping.Rows[e.RowIndex].Cells[e.ColumnIndex];
    //    if (cell != null)
    //    {
    //        var val = cell.Value;

    //        switch (colIx)
    //        {
    //            case 0: // Name
    //                _BackingData.LineItems[rowIx].Name = val as string ?? "";
    //                break;
    //            case 1: // Type
    //                _BackingData.LineItems[rowIx].Type = val as string ?? "";
    //                break;
    //            case 2: //nullable
    //                _BackingData.LineItems[rowIx].Nullable = val as bool? ?? true;
    //                break;
    //            case 3: //mapped column
    //                string str = val as string ?? "";
    //                Metadata? meta = ChosenData.GetAllMetadata().Find(m => m.SqlName == str);
    //                if (meta != null) 
    //                {
    //                    _BackingData.LineItems[rowIx].MappedColumn = meta;
    //                }
    //                GrdMapping.Invalidate();
    //                break;
    //        }
    //    }
    //}

    //private void GrdMapping_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    //{
    //    DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)GrdMapping.Rows[e.RowIndex].Cells["MappedColumn"];
    //    if (cell != null)
    //    {
    //        if (cell.Value is string meta)
    //        {
    //            //_BackingData.LineItems[e.RowIndex].MappedColumn = meta;
    //        }
    //        //_BackingData.LineItems[e.RowIndex].MappedColumn = 
    //        GrdMapping.Invalidate();
    //    }
    //}

    //private void GrdMapping_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
    //{
    //    if (GrdMapping.IsCurrentCellDirty)
    //    {
    //        GrdMapping.CommitEdit(DataGridViewDataErrorContexts.Commit);
    //    }
    //}

    private void MultiMapItemControl_Leave(object sender, EventArgs e)
    {
        _BackingData.SaveDataTable();
        SaveData();
    }

    private void GrdMapping_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
    {
        if (GrdMapping.CurrentCell.OwningColumn is DataGridViewComboBoxColumn)
        {
            DataGridViewComboBoxEditingControl editingControl = (DataGridViewComboBoxEditingControl)GrdMapping.EditingControl;
            e.Value = editingControl.SelectedItem;
            e.ParsingApplied = true;
        }
    }

    private void GrdMapping_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        //if (e.Value != null)
        //{
        //    e.Value = e.Value.ToString();
        //    e.FormattingApplied = true;
        //}
    }
}
