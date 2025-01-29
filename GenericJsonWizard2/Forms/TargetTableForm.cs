using GenericJsonSuite;
using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.BackingData.ColumnMetadata;
using GenericJsonWizard.EtlaToolbelt.Forms;

namespace GenericJsonWizard.Forms;

public partial class TargetTableForm : RepeatingWizardForm
{
    private TargetTableData _BackingData { get; set; }

    private string _WarningMsg = "";

    public TargetTableForm(TargetTableData backingData)
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
        Map.Add(TxtBackfillId, nameof(backingData.BackfillIdName));

        Dependent depdendent = new(TxtIdName, TxtBackfillId, Dependent.MakeSame);
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
            TabDefinitionPages.TabPages.Add(TpgLogicalPK);
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

    protected override bool IsInvalid(out string? msg)
    {
        msg = "";
        var cols = L2lColumns.ChosenItems.Cast<Metadata>().ToList();
        if (!TableData.ChosenColsAreCompatible(cols, out JsonColumn aCol, out JsonColumn? anotherCol))
        { msg += $"Columns '{aCol.SqlName}' and '{anotherCol?.SqlName}' are in different JSON heirarchies and so incompatible\n"; }

        if (RdoPhysicalPkNone.Checked)
        { msg = "No physical primary key chosen\n"; }

        if (RdoPhysicalPkSame.Checked && L2lLogicalPK.GetChosenObjects().Count() < 1)
        { msg = "Chose physical primary to be same as logical but havenot chosen logical primary key\n"; }

        if (RdoPhysicalPkDifferent.Checked && L2lPhysicalPK.GetChosenObjects().Count() < 1)
        { msg = "Physical primary key has not been defined\n"; }

        return msg.IsBlack();
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
