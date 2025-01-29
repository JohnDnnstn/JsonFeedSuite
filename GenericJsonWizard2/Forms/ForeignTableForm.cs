using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.BackingData.ColumnMetadata;
using GenericJsonWizard.EtlaToolbelt.Forms;

namespace GenericJsonWizard.Forms;

public partial class ForeignTableForm : RepeatingWizardForm
{
    private ForeignTableData _BackingData { get; set; }
    //private List<Dependent> _Dependents = [];

    public ForeignTableForm(ForeignTableData backingData)
    {
        InitializeComponent();
        TxtRubric.Text = "Foreign tables contain one or more source fields that will be replaced in the target table with a backfilled identifier";

        _BackingData = backingData;

        Map = new FormMapper(backingData);
        //Map.Add(RdoAddNewValues, nameof(backingData.AddNewValues));
        Map.Add(TxtSchemaName, nameof(backingData.SchemaName));
        Map.Add(TxtTableName, nameof(backingData.TableName));
        Map.Add(TxtIdName, nameof(backingData.IdName));
        Map.Add(CmbIdType, nameof(backingData.IdType));
        Map.Add(ChkHasBackfilledId, nameof(backingData.HasBackfillId));
        Map.Add(TxtBackfilledIdName, nameof(backingData.BackfillIdName));
        Map.Add(L2lColumns, nameof(backingData.AllCandidates), nameof(backingData.ChosenObjects));
    }

    protected override bool IsInvalid(out string? msg)
    {
        var cols = L2lColumns.ChosenItems.Cast<Metadata>().ToList();
        if (!TableData.ChosenColsAreCompatible(cols, out JsonColumn aCol, out JsonColumn? anotherCol))
        {
            msg = $"Columns '{aCol.SqlName}' and '{anotherCol?.SqlName}' are in different JSON heirarchies and so incompatible";
            return true;
        }
        msg = "";
        return false;
    }

    private void ForeignTableForm_Load(object sender, EventArgs e)
    {
        //_Dependents.Clear();
        Dependent dependent = new(TxtTableName, TxtIdName, Dependent.MakeId);
        //_Dependents.Add(dependent);
        dependent = new(TxtIdName, TxtBackfilledIdName, Dependent.MakeSame);
        //_Dependents.Add(dependent);

        ChkHasBackfilledId_CheckedChanged(sender, e);
    }

    private void ChkHasBackfilledId_CheckedChanged(object sender, EventArgs e)
    {
        TxtBackfilledIdName.Visible = TxtBackfilledIdName.Enabled = ChkHasBackfilledId.Checked;
    }

}
