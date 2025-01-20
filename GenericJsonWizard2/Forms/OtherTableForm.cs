using GenericJsonSuite;
using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.EtlaToolbelt.Forms;

namespace GenericJsonWizard.Forms;

public partial class OtherTableForm : RepeatingWizardForm
{
    private OtherTableData _BackingData { get; set; }

    private string _WarningMsg = "";

    public OtherTableForm(OtherTableData backingData)
    {
        InitializeComponent();
        TxtRubric.Text = "This allows the definition of less standard secondary tables";

        _BackingData = backingData;

        Map = new FormMapper(backingData);
        Map.Add(TxtSchemaName, nameof(backingData.SchemaName));
        Map.Add(TxtTableName, nameof(backingData.TableName));
        Map.Add(ChkStructureOnly, nameof(backingData.IsStructureOnly));
        //Map.Add(ChkIsMultiMap, nameof(backingData.IsMultiMapped));

        Map.Add(L2lColumns, nameof(backingData.AllCandidates), nameof(backingData.ChosenObjects));

        Map.Add(L2lLogicalPK, nameof(backingData.ChosenObjects), nameof(backingData.LogicalPkObjects));

        Map.Add(RdoPhysicalPkNone, nameof(backingData.PKVariety), ChosenData.PKVariety.NONE);
        Map.Add(RdoPhysicalPkSame, nameof(backingData.PKVariety), ChosenData.PKVariety.SAME);
        Map.Add(RdoPhysicalPkDifferent, nameof(backingData.PKVariety), ChosenData.PKVariety.DIFFERENT);
        Map.Add(RdoPhysicalPkId, nameof(backingData.PKVariety), ChosenData.PKVariety.ID);
        Map.Add(L2lPhysicalPK, nameof(backingData.ChosenObjects), nameof(backingData.DifferentPhysicalPkObjects));
        Map.Add(TxtIdName, nameof(backingData.IdName));
        Map.Add(CmbIdType, nameof(backingData.IdType));
        Map.Add(ChkBackfillId, nameof(backingData.HasBackfillId));
        //Map.Add(TxtBackfillId, nameof(backingData.BackfillIdName));
        Map.Add(TxtBackfillId, nameof(backingData.BackfillId.SqlName));
    }

    private void DisplayTabPages(string warning = "", bool forceRedisplay = false)
    {
        if (warning == _WarningMsg && !forceRedisplay) return;
        // Which pages to show...
        TabPage currentPage = TabDefinitionPages.SelectedTab ?? TabDefinitionPages.TabPages[0];
        TabDefinitionPages.TabPages.Clear();
        TabDefinitionPages.TabPages.Add(TpgColumns);
        if (ChkStructureOnly.Checked)
        {
            TabDefinitionPages.TabPages.Add(TpgPhysicalPK);
        }
        else
        {
            TabDefinitionPages.TabPages.Add(TpgLogicalPK);
            TabDefinitionPages.TabPages.Add(TpgPhysicalPK);
            TabDefinitionPages.TabPages.Add(TpgLogicalDuplicates);
        }

        var hasLogicalPk = TabDefinitionPages.TabPages.Contains(TpgLogicalPK);
        if (!hasLogicalPk)
        {
            if (RdoPhysicalPkSame.Checked) { warning += "Warning: Physical Primary Key cannot be set in relation to Logical Primary key given these choices"; }
        }
        RdoPhysicalPkSame.Enabled = hasLogicalPk;

        if (warning.IsBlack())
        {
            TxtWarnings.Text = warning;
            TabDefinitionPages.TabPages.Add(TpgWarnings);
        }
        if (TabDefinitionPages.TabPages.Contains(currentPage))
        {
            TabDefinitionPages.SelectedTab = currentPage;
        }
        _WarningMsg = warning;
    }

    private void OtherTableForm_Load(object sender, EventArgs e)
    {
        DisplayTabPages(forceRedisplay: true);
        ChkStructureOnly_CheckedChanged(sender, e);
        RdoPhysicalPk_CheckedChanged(sender, e);

        Dependent dependent = new(TxtTableName, TxtIdName, Dependent.MakeId);
        dependent = new(TxtIdName, TxtBackfillId, Dependent.MakeSame);
    }

    private void ChkStructureOnly_CheckedChanged(object sender, EventArgs e)
    {
        DisplayTabPages(forceRedisplay: true);
    }

    private void L2lColumns_ListsChanged(object sender, EventArgs e)
    {
        L2lLogicalPK.InitialiseSource(L2lColumns.ChosenItems, broadcastEvent: false);
        L2lLogicalPK.ReinitialiseDestination();

        L2lPhysicalPK.InitialiseSource(L2lColumns.ChosenItems, broadcastEvent: false);
        L2lPhysicalPK.ReinitialiseDestination();

    }

    private void L2lLogicalPK_ListsChanged(object sender, EventArgs e)
    {
        LstPhysicalPK.Items.Clear();
        LstPhysicalPK.Items.AddRange([.. L2lLogicalPK.GetChosen()]);

    }

    private void RdoPhysicalPk_CheckedChanged(object sender, EventArgs e)
    {
        L2lPhysicalPK.Visible = L2lPhysicalPK.Enabled = RdoPhysicalPkDifferent.Checked;
        LstPhysicalPK.Visible = LstPhysicalPK.Enabled = RdoPhysicalPkSame.Checked;
        LblId.Visible = LblId.Enabled = RdoPhysicalPkId.Checked;
        TxtIdName.Visible = TxtIdName.Enabled = RdoPhysicalPkId.Checked;
        CmbIdType.Visible = CmbIdType.Enabled = RdoPhysicalPkId.Checked;
        ChkBackfillId.Visible = ChkBackfillId.Enabled = RdoPhysicalPkId.Checked;
        TxtBackfillId.Visible = TxtBackfillId.Enabled = RdoPhysicalPkId.Checked && ChkBackfillId.Checked;
    }

    private void ChkBackfillId_CheckedChanged(object sender, EventArgs e)
    {
        TxtBackfillId.Visible = TxtBackfillId.Enabled = RdoPhysicalPkId.Checked && ChkBackfillId.Checked;
    }
}
