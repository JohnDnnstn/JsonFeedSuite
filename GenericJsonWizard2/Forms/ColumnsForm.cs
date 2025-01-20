using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonSuite.EtlaToolbelt.Wizards;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.BackingData.ColumnMetadata;
using System.ComponentModel;

namespace GenericJsonWizard.Forms;

public partial class ColumnsForm : BaseWizardForm
{
    private ColumnsData _BackingData { get; set; }

    /// <summary>The index of the "Ignore" column/// </summary>
    protected int ColIndexIgnore { get; set; }

    public bool IsAnalysing { get; set; }
    public bool HasAnalysed { get; set; }


    public ColumnsForm(ColumnsData backingData)
    {
        InitializeComponent();
        TxtRubric.Text = "Click the Analyse button and the wizard will look at the feed file to try to guess suitable table column names and types.  Edit as appropriate.";

        _BackingData = backingData;

        Map = new FormMapper(backingData);

        Map.Add(ChkShowJsonEntities, nameof(backingData.ShowJsonEntities));
        Map.Add<JsonColumn>(GrdColumns, nameof(backingData.ColumnMetadata));
    }

    private void InitialiseGrid()
    {
        GrdColumns.Columns.Clear();

        GrdColumns.AutoGenerateColumns = false;
        GrdColumns.AutoSize = true;

        DataGridViewColumn gridColumn = new DataGridViewCheckBoxColumn();
        gridColumn.DataPropertyName = gridColumn.Name = "Ignore";
        gridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        gridColumn.ToolTipText = "If set, this field in the data feed file is not loaded into staging";
        ColIndexIgnore = GrdColumns.Columns.Add(gridColumn);

        gridColumn = new DataGridViewTextBoxColumn();
        gridColumn.DataPropertyName = gridColumn.Name = "JsonName";
        gridColumn.ReadOnly = true;
        gridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        gridColumn.ToolTipText = "The Json element name in the original JSON";
        gridColumn.DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.Cornsilk, };
        if (OperatingSystem.IsWindows()) { gridColumn.DefaultCellStyle.Font = new Font(DefaultFont, FontStyle.Italic); }
        GrdColumns.Columns.Add(gridColumn);

        gridColumn = new DataGridViewTextBoxColumn();
        gridColumn.DataPropertyName = gridColumn.Name = "SqlName";
        gridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        gridColumn.ToolTipText = "The target field name in the database";
        GrdColumns.Columns.Add(gridColumn);


        gridColumn = new DataGridViewTextBoxColumn();
        gridColumn.DataPropertyName = gridColumn.Name = "SqlType";
        gridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        gridColumn.ToolTipText = "The database type of the column in the staging table and the target table";
        GrdColumns.Columns.Add(gridColumn);

        gridColumn = new DataGridViewCheckBoxColumn();
        gridColumn.DataPropertyName = gridColumn.Name = "Nullable";
        gridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        gridColumn.ToolTipText = "If set, this column can be null in the staging table and the target table.  Note: columns in a primary key cannot be null.";
        GrdColumns.Columns.Add(gridColumn);

        gridColumn = new DataGridViewTextBoxColumn();
        gridColumn.DataPropertyName = gridColumn.Name = "JsonPathInOriginal";
        gridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        gridColumn.ToolTipText = "The JSON path to this node in the original Json";
        gridColumn.ReadOnly = true;
        GrdColumns.Columns.Add(gridColumn);

        GrdColumns.Visible = GrdColumns.Enabled = true;

        GrdColumns.Refresh();
    }

    /// <summary>Used to highlight certain results of the analysis of the sample feed file
    /// * ChosenColumns where null was encountered (set the nullable background grey)
    /// * ChosenColumns where the type could not be established (set the text to red)
    /// </summary>
    private void ColourGrid()
    {
        int nullableIx = -1;
        int typeIx = -1;
        string unknownType = "UNKNOWN";

        for (int ix = 0; ix < GrdColumns.Columns.Count; ++ix)
        {
            if (GrdColumns.Columns[ix].Name == "Nullable") { nullableIx = ix; }
            if (GrdColumns.Columns[ix].Name == "SqlType") { typeIx = ix; }
        }
        var data = GrdColumns.GetList<JsonColumn>();
        for (int ix = 0; ix < GrdColumns.Rows.Count && ix < data.Count; ++ix)
        {
            DataGridViewRow row = GrdColumns.Rows[ix];
            var cell = row.Cells[nullableIx];
            if (data[ix].Nullable)
            {
                ((DataGridViewCheckBoxCell)cell).Value = false;
                //((CheckBox)cell.Value).Checked = false;
            }
            else
            {
                cell.Style.BackColor = Color.FromArgb(235, 235, 235);
            }
            if (data[ix].SqlType != null && data[ix].SqlType.Equals(unknownType, StringComparison.InvariantCultureIgnoreCase))
            {
                cell = row.Cells[typeIx];
                cell.Style.ForeColor = Color.Red;
            }
        }
    }


    /// <summary>Allows the user to drill down to an individual column</summary>
    /// <param name="row">Zero-based index of the row in the Grid's backing data list</param>
    protected void ShowColumnDetails(int row)
    {
        var data = GrdColumns.GetList<JsonColumn>();
        var metadata = data[row];

        using (var form = new ColumnDetailsForm(metadata))
        {
            var result = BaseWizard.ShowSingleton(form, this);
            if (result == FormResult.NEXT)
            {
                if (GrdColumns.Rows[row].IsNewRow)
                {
                    NewRowAdded(metadata);
                    // Required in order to show the new column's row and a new blank row
                    GrdColumns.SetList(data);
                }
                DataGridViewDataErrorContexts context = new();
                GrdColumns.CommitEdit(context);
                GrdColumns.EndEdit();
            }
        }
    }

    /// <summary>Initialises a manually added row (either explicitly manually added or via a double-click)</summary>
    /// <param name="metadata">The backing data of the added row</param>
    private void NewRowAdded(JsonColumn metadata)
    {
        if (metadata != null)
        {
            //metadata.TableOrdinal = 80 + _NumberOfManuallyAddedColumns++;
            //metadata.FoundNull = true;
            ColourGrid();
        }
    }

    private void ColumnsForm_Load(object sender, EventArgs e)
    {
        InitialiseGrid();
    }

    private void BtnAnalyse_Click(object sender, EventArgs e)
    {
        try
        {
            Cursor.Current = Cursors.WaitCursor;
            Console.WriteLine($"{DateTime.Now}");
            try
            {
                IsAnalysing = true;
                HasAnalysed = false;

                // This changes ChosenData.JsonColumns and ChosenData.Domains
                JsonSchemaAnalyser.AnalyseFile(ChosenData.FeedDetails.Filepath);
                _BackingData.SetColumnList(ChosenData.JsonColumns);
                ChkShowJsonEntities.Checked = false;
                _BackingData.ShowJsonEntities = false;

                HasAnalysed = true;
                IsAnalysing = false;
                BtnRevert.Visible = BtnRevert.Enabled = true;
                GrdColumns.SetList(_BackingData.ColumnMetadata);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error analysing file '{ChosenData.FeedDetails.Filepath}': {ex.Message}{Environment.NewLine}Have you chosen the correct delimiter?");
                return;
            }
            GrdColumns.Visible = GrdColumns.Enabled = true;
            GrdColumns.Refresh();
        }
        finally
        {
            Cursor.Current = Cursors.Default;
            Console.WriteLine($"{DateTime.Now}");
        }
    }


    private void BtnRevert_Click(object sender, EventArgs e)
    {
        JsonSchemaAnalyser.Revert();
        _BackingData.SetColumnList(ChosenData.JsonColumns);
        LoadFormStateFromBackingData();
        InitialiseGrid();
    }

    private void GrdColumns_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        int row = e.RowIndex;
        int col = e.ColumnIndex;
        if (row >= 0 && row < GrdColumns.Rows.Count)
        {
            ShowColumnDetails(row);
        }
        // 
        if (col == ColIndexIgnore && row != -1 && row >= _BackingData.ColumnMetadata.Count)
        {
            GrdColumns.EndEdit();
        }
    }

    private void ChkShowJsonEntities_CheckedChanged(object sender, EventArgs e)
    {
        _BackingData.ShowJsonEntities = ChkShowJsonEntities.Checked;
        if (Map != null) { Map.Load(); }
    }

    private void GrdColumns_UserAddedRow(object sender, DataGridViewRowEventArgs e)
    {
        int colIx = GrdColumns.CurrentCell.ColumnIndex;
        int rowIx = GrdColumns.CurrentCell.RowIndex;
        string newText = GrdColumns.EditingControl.Text;
        SystemColumn col = new("");
        switch (colIx)
        {
            case 2: col.SqlName = newText; break;
            case 3: col.SqlType = newText; break;
        }
        ChosenData.SystemColumns.Add(col);
        var bind = GrdColumns.DataSource;
        GrdColumns.DataSource = null;
        GrdColumns.DataSource = new BindingList<JsonColumn>(_BackingData.ColumnMetadata);
        GrdColumns.Refresh();
        GrdColumns.CurrentCell = GrdColumns[colIx, rowIx];
        GrdColumns.BeginEdit(false);
        ((TextBox)GrdColumns.EditingControl).SelectionStart = 1;
    }

}
