using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.BackingData;

namespace GenericJsonWizard.Forms;

public partial class MultiMappedTableForm : RepeatingWizardForm, IRaisesTableNameChangedEvent
{
    private Dictionary<int, MultiMapItemControl> _Controls = [];

    private XMultiMapTableData _BackingData { get; set; }

    #region events
    public delegate void TableNameChanged_Handler(string newName);
    public event IRaisesTableNameChangedEvent.TableNameChanged_Handler? TableNameChanged_Event;
    public void Raise_TableNameChanged(string newName) { TableNameChanged_Event?.Invoke(newName); }
    #endregion

    public MultiMappedTableForm(XMultiMapTableData backingData)
    {
        InitializeComponent();
        TxtRubric.Text = "Define tables where multiple sets of data map onto this table (e.g. DepartureLocaltoin and DestinationLocation map to Location";

        _BackingData = backingData;

        Map = new FormMapper(backingData);
        Map.Add(TxtSchema, nameof(backingData.SchemaName));
        Map.Add(TxtTable, nameof(backingData.TableName));
        Map.Add(ChkHasId, nameof(backingData.HasId));
        Map.Add(TxtIdName, nameof(backingData.IdName));
        Map.Add(CmbIdType, nameof(backingData.IdType));

        //_BackingData.SynchronisationRequested_Event += SynchroniseMappings;

        //_BackingData.Id.SetForm(this);

    }


    #region Events
    /// <summary>Event to notify the pages that the ChkHasId control has changed
    /// The control may need to change the visibility of the ChkHasBackfilledId and TxtBackfilledIdName controls
    /// </summary>
    public event ChkHasId_Changed_Handler? ChkHasId_Changed_Event;
    public delegate void ChkHasId_Changed_Handler(bool hasId);
    public void Raise_ChkHasId_Changed_Event(bool hasId) { ChkHasId_Changed_Event?.Invoke(hasId); }

    /// <summary>Handles the RemoveMappingRequested event</summary>
    private void RemoveMapping(int mappingIx)
    {
        if (TabControl.SelectedTab != null)
        {
            TabControl.TabPages.Remove(TabControl.SelectedTab);
            _BackingData.ControlBackingData.Remove(mappingIx);
        }
        _Controls.Remove(mappingIx);
    }

    private void AddTableCol()
    {
        _BackingData.AddNewTargetColumn();
        //MultiMapColumn newTableCol = new("");
        //_BackingData.TableColumns.Add(newTableCol);
        //foreach (MultiMapTableMapping mapping in _BackingData.Mappings.PermittedValues)
        //{
        //    if (mapping.Key == key) { continue; }
        //    MultiMapColumnMapping columnMapping = new(newTableCol);
        //    mapping.ColumnMappings.Add(columnMapping);

        //}
        //    //_BackingData.ChosenIdentifiers.Add(targetColId);
        //    //Metadata targetCol = ChosenData.GetMetadata(targetColId);
        //    //foreach (MultiMapTableMapping mapping in _BackingData.Mappings.PermittedValues)
        //    //{
        //    //    if (mapping.Key == mappingIx) { continue; }

        //    //    var newSourceCol = new Metadata();
        //    //    var newMap = new MultiMapColumnMappingData(targetCol,Metadata.Zero);
        //    //    mapping.ColumnMappings.Add(newMap);
        //    //}
        foreach (MultiMapItemControl ctrl in _Controls.Values)
        {
            ctrl.LoadData();
            ctrl.Refresh();
        }
    }

    //private void SynchroniseMappings()
    //{
    //    foreach (MultiMapItemControl ctrl in _Controls.PermittedValues)
    //    {
    //        ctrl.LoadData();
    //    }
    //}


    #endregion Events

    //private int GetNextMappingIx()
    //{
    //    int maxKey = 0;
    //    if (_BackingData.Mappings.Count > 0) { maxKey = _BackingData.Mappings.Max(p => p.Key); }
    //    return maxKey + 1;
    //}

    //private string GetNextPageName(int key)
    //{
    //    return $"mapping_{key}";
    //}

    //private TabPage MakeTabPage(MultiMapTableMapping data, int key)
    //{
    //    data.Table = _BackingData;
    //    TabPage answer = new(GetNextPageName(key));
    //    MultiMapItemControl ctrl = new(data, key);
    //    _Controls.Add(key, ctrl);
    //    answer.Controls.Add(ctrl);
    //    ctrl.Dock = DockStyle.Fill;
    //    ctrl.Initialise();

    //    // Set up events and handlers
    //    // This form needs to know when the user wants to remove a mapping
    //    // The mapping control needs to know if the table no longer has an Id
    //    ctrl.RemoveMappingRequest_Event += RemoveMapping;
    //    ctrl.AddColumnRequest_Event += AddTableCol;
    //    ChkHasId_Changed_Event += ctrl.ChkHasId_Changed_Handler;

    //    return answer;
    //}

    //private void InitialiseTabPages()
    //{
    //    TabControl.TabPages.Clear();

    //    if (_BackingData.Mappings.Count > 0)
    //    {
    //        foreach (var pair in _BackingData.Mappings)
    //        {
    //            var pg = MakeTabPage(pair.Value, pair.Key);
    //            TabControl.TabPages.Add(pg);
    //        }
    //    }
    //    else
    //    {
    //        AddNewMapping();
    //    }
    //}

    //private void AddNewMapping()
    //{
    //    var key = GetNextMappingIx();
    //    MultiMapTableMapping page = new(_BackingData, key);
    //    _BackingData.Mappings.Add(key, page);
    //    var pg = MakeTabPage(page, key);
    //    TabControl.TabPages.Add(pg);
    //}

    private void AddNewTabPage(int mappingIx, YMultiMapControlData controlData)
    {
        // Create a new tab page and add one of our Mapping Controls to it      
        var tabPage = new TabPage($"mapping_{mappingIx}");
        MultiMapItemControl ctrl = new MultiMapItemControl(controlData, mappingIx, _BackingData);
        tabPage.Controls.Add(ctrl);
        ctrl.Dock = DockStyle.Fill;

        // Add the new 
        TabControl.TabPages.Add(tabPage);
    }

    private void InitialiseTabPages()
    {
        TabControl.TabPages.Clear();
        foreach (var (mappingIx,controlData) in _BackingData.ControlBackingData)
        {
            AddNewTabPage(mappingIx, controlData);
        }
        // display first page
    }


    private void MultiMappedTableForm_Load(object sender, EventArgs e)
    {
        //if (_BackingData.ChosenIdentifiers.Count < 1)
        //{
        //    AddTableCol(-1);
        //}
        InitialiseTabPages();
        ChkHasId_CheckedChanged(sender, e);
    }

    private void ChkHasId_CheckedChanged(object? sender, EventArgs e)
    {
        TxtIdName.Visible = TxtIdName.Enabled = ChkHasId.Checked;
        CmbIdType.Visible = CmbIdType.Enabled = ChkHasId.Checked;
        Raise_ChkHasId_Changed_Event(ChkHasId.Checked);
    }

    private void BtnAddMapping_Click(object sender, EventArgs e)
    {
        (int mappingIx, YMultiMapControlData controlData) = _BackingData.AddNewMapping();
        AddNewTabPage(mappingIx,controlData);
    }

    private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Map.Save();
        //_BackingData.Table.SynchroniseTableColumns(Key);
    }

    private void TabControl_Deselecting(object sender, TabControlCancelEventArgs e)
    {
        //// Save the current contents of the mapping (ignoring blank lines)
        //// synchronise all the other tabs
        //// How do we go from a tabPage or tabPageIndex to a MultiMapItemControl?
        //if (e?.TabPage != null)
        //{
        //    foreach (var ctrl in e.TabPage.Controls)
        //    {
        //        if (ctrl is MultiMapItemControl multi)
        //        {
        //            if (multi.RowCount > 0)
        //            {
        //                multi.SaveData();
        //                _BackingData.ChosenMetadata = multi.GetChosenColumns();
        //                //_BackingData.SynchroniseTableColumns(multi.Key);
        //            }
        //        }
        //    }
        //}
        ////Test();
    }

    private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
    {

    }



    private void TxtTable_TextChanged(object sender, EventArgs e)
    {
        Raise_TableNameChanged(TxtTable.Text);
        TxtIdName.Text = _BackingData.Id.SqlName;
    }
}
