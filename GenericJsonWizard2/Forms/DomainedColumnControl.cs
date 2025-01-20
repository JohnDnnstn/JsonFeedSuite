using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.BackingData;

namespace GenericJsonWizard.Forms;

public partial class DomainedColumnControl : UserControl
{
    private FormMapper _Map { get; set; }
    private readonly DomainedColumn _BackingData;

    public DomainedColumnControl(DomainedColumn backingData)
    {
        InitializeComponent();
        _BackingData = backingData;

        _Map = new(_BackingData);
        _Map.Add(TxtBackfillIdName, nameof(_BackingData.BackfillIdName));
        _Map.Add(ChkBackillId, nameof(_BackingData.HasBackfillId));
    }

    #region Events
    /// <summary>Event raised when a user indicates that this column should no longer be considered part of this Domain</summary>
    /// <param name="ctrl">The DomainedColumnControl where the button was pressed</param>
    /// <param name="item">The DomainedColumnData associated with the DomainedColumnControl where the button was pressed</param>
    public event RemoveDomainedColumnRequestedHandler? RemoveDomainedColumnRequested;
    public delegate void RemoveDomainedColumnRequestedHandler(DomainedColumnControl ctrl, DomainedColumn? item);
    /// <summary>The method which raises the RemoveDomainedColumnRequested event
    /// <param name="ctrl">The DomainedColumnControl where the button was pressed</param>
    /// <param name="item">The DomainedColumnData associated with the DomainedColumnControl where the button was pressed</param>
    protected virtual void OnRemoveDomainedColumnRequested(DomainedColumnControl ctrl, DomainedColumn? data)
    {
        RemoveDomainedColumnRequested?.Invoke(ctrl, data);
    }
    #endregion

    public void InitialiseColumnList(object item)
    {
        LstColumns.Items.Clear();
        LstColumns.Items.Add(item);
    }

    public void InitialiseColumnList(IEnumerable<object> itemList)
    {
        object[] items = [.. itemList];
        LstColumns.Items.Clear();
        LstColumns.Items.AddRange(items);
    }

    public void SetChosenColumn(object item)
    {
        for (int i = 0; i < LstColumns.Items.Count; i++)
        {
            if (LstColumns.Items[i].ToString() == item.ToString())
            {
                LstColumns.SetSelected(i, true);
                return;
            }
        }
    }

    public List<string> GetChosenColumns()
    {
        List<string> answer = [];
        foreach (var item in LstColumns.SelectedItems)
        {
            if (item?.ToString() != null) { answer.Add(item.ToString()!); }
        }
        return answer;
    }

    public void ChkHasId_HasChanged(bool newValue)
    {
        ChkBackillId.Visible = ChkBackillId.Enabled = newValue;
        LblBackfillIdName.Visible = LblBackfillIdName.Enabled = newValue && ChkBackillId.Checked;
        TxtBackfillIdName.Visible = TxtBackfillIdName.Enabled = newValue && ChkBackillId.Checked;
    }

    private void ChkBackillId_CheckedChanged(object sender, EventArgs e)
    {
        LblBackfillIdName.Visible = LblBackfillIdName.Enabled = ChkBackillId.Checked && ChkBackillId.Visible;
        TxtBackfillIdName.Visible = TxtBackfillIdName.Enabled = ChkBackillId.Checked && ChkBackillId.Visible;
    }

    private void BtnRemove_Click(object sender, EventArgs e)
    {
        OnRemoveDomainedColumnRequested(this, _BackingData);
    }

    private void DomainedColumnControl_Load(object sender, EventArgs e)
    {
        _Map.Load();
    }

    private void DomainedColumnControl_Leave(object sender, EventArgs e)
    {
        _Map.Save();
    }

    public bool HasBackfilledId { get { return ChkBackillId.Checked; } set { ChkBackillId.Checked = value; } }

    public string BackfilledId { get { return TxtBackfillIdName.Text; } set { TxtBackfillIdName.Text = value; } }
}
