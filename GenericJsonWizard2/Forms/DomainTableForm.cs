using GenericJsonSuite;
using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonSuite.EtlaToolbelt.Wizards;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.EtlaToolbelt.Forms;

namespace GenericJsonWizard.Forms
{
    public partial class DomainTableForm : RepeatingWizardForm, IRaisesTableNameChangedEvent
    {
        private DomainTableData _BackingData { get; set; }
        private readonly List<Dependent> _Dependents = [];

        #region events
        public delegate void TableNameChanged_Handler(string newName);
        public event IRaisesTableNameChangedEvent.TableNameChanged_Handler? TableNameChanged_Event;
        public void Raise_TableNameChanged(string newName) { TableNameChanged_Event?.Invoke(newName); }
        #endregion

        public DomainTableForm(DomainTableData backingData)
        {
            InitializeComponent();
            TxtRubric.Text = "Domain table specifying all the valid values in one or more columns";

            _BackingData = backingData;

            Map = new FormMapper(backingData);
            Map.Add(ChkIsReadOnly, nameof(backingData.IsReadOnly));
            Map.Add(TxtSchemaName, nameof(backingData.SchemaName));
            Map.Add(TxtTableName, nameof(backingData.TableName));
            Map.Add(TxtPermittedValues, nameof(backingData.PermittedValuesAsString));
            Map.Add(TxtDomainColumnName, nameof(backingData.DomainColumnSqlName));
            Map.Add(TxtDomainColumnType, nameof(backingData.DomainColumnSqlType));
            Map.Add(ChkHasId, nameof(backingData.HasId));
            Map.Add(TxtIdName, nameof(backingData.IdName));
            Map.Add(TxtIdType, nameof(backingData.IdType));
            Map.Add(ChkHasDescription, nameof(backingData.HasDescription));
            Map.Add(TxtDescriptionColumn, nameof(backingData.DescriptionName));

        }

        #region Events
        /// <summary>Event raised when ChkHasId.Checked changes
        /// Backfilled Ids that replace the Domained UnderlyingColumn may need to hide or show the BackfilledId-related controls
        /// </summary>
        /// <param name="hasId"></param>
        public event HasIdHasChangedHandler? HasId_HasChanged_Event;
        public delegate void HasIdHasChangedHandler(bool hasId);
        public void On_HasId_HasChanged(bool hasId) { HasId_HasChanged_Event?.Invoke(hasId); }

        /// <summary>Handler invoked when the user indicates that a column should no longer be considered to belong to this Domain
        /// </summary>
        /// <param name="ctrl">The DomainedColumnControl where the button was pressed</param>
        /// <param name="item">The DomainedColumnData associated with the DomainedColumnControl where the button was pressed</param>
        /// <exception cref="NotImplementedException"></exception>
        private void RemoveDomainedColumnRequested(DomainedColumnControl ctrl, DomainedColumn? item)
        {
            if (item?.UnderlyingColumn == null || item.UnderlyingColumn.SqlName.IsWhite())
            {
                MessageBox.Show("Internal error - no domained column was specified so nothing can be removed");
                return;
            }
            SaveFormStateToBackingData();

            UndomainColumnData undomainedData = new() { DomainedColumn = item };
            var result = BaseWizard.ShowSingleton(new UndomainColumnForm(undomainedData), this);
            if (result == FormResult.NEXT && undomainedData.Target.IsBlack())
            {
                if (ctrl.Parent is TabPage page) { TabColumns.TabPages.Remove(page); }
                _BackingData.DomainedColumns.Remove(item);
                if (undomainedData.Target == UndomainColumnData.NewDomain)
                {
                    DomainTableData newDomainTable = new(item.UnderlyingColumn, undomainedData.NewDomainName)
                    {
                        SchemaName = TxtSchemaName.Text,
                        PermittedValues = _BackingData.PermittedValues,
                        DomainedColumns = [item],
                    };
                    ChosenData.DomainTables.Add(newDomainTable);
                    Refresh();
                    return;
                }
                if (undomainedData.Target != UndomainColumnData.NewDomain)
                {
                    var movedToDomain = ChosenData.DomainTables.FirstOrDefault(d => d.TableName == undomainedData.Target);
                    if (movedToDomain != null)
                    {
                        movedToDomain.DomainedColumns.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Internal error - cannot find domain to move this column to");
                    }
                }
            }

        }

        #endregion

        private TabPage MakeTabPage(DomainedColumn data, string key = "", bool displayRemoveDomained = true)
        {
            TabPage answer = new(key);
            DomainedColumnControl ctrl = new(data);
            answer.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Fill;

            //ctrl.InitialiseColumnList(_BackingData.AllCandidates.Cast<object>());//ChosenData.SchemaColumns.GetAllSchemaColumns().Cast<object>());
            ctrl.InitialiseColumnList(_BackingData.GetAllCandidateNames());
            if (data != null) { ctrl.SetChosenColumn(data.UnderlyingColumn.SqlName); }


            ctrl.RemoveDomainedColumnRequested += RemoveDomainedColumnRequested;
            HasId_HasChanged_Event += ctrl.ChkHasId_HasChanged;
            if (data?.UnderlyingColumn != null) { ctrl.SetChosenColumn(data.UnderlyingColumn); }

            // Only display/enable the remove domained column button if there is more than one tab
            var removeDomainedColBtn = ctrl?.Controls?["panel1"]?.Controls?["BtnRemoveDomainedColumn"];
            if (removeDomainedColBtn != null)
            {
                removeDomainedColBtn.Visible = removeDomainedColBtn.Enabled = displayRemoveDomained;
            }

            return answer;
        }

        private void InitialiseTabPages()
        {
            TabColumns.TabPages.Clear();

            // Only display/enable the remove domained column button if there is more than one tab
            bool displayButton = (_BackingData.DomainedColumns.Count > 1);

            foreach (var domainedCol in _BackingData.DomainedColumns)
            {
                var pg = MakeTabPage(domainedCol, domainedCol.UnderlyingColumn?.SqlName ?? "", displayButton);
                TabColumns.TabPages.Add(pg);
            }
        }

        private void DomainTableForm_Load(object sender, EventArgs e)
        {
            InitialiseTabPages();

            _Dependents.Clear();
            Dependent dependent = new(TxtTableName, TxtIdName, Dependent.MakeId);
            _Dependents.Add(dependent);
            dependent = new(TxtTableName, TxtDomainColumnName, Dependent.MakeSingular);
            _Dependents.Add(dependent);

            ChkHasId_CheckedChanged(sender, e);
        }

        private void ChkHasId_CheckedChanged(object sender, EventArgs e)
        {
            LblIdName.Visible = ChkHasId.Checked;
            TxtIdName.Visible = TxtIdName.Enabled = ChkHasId.Checked;
            TxtIdType.Visible = TxtIdType.Enabled = ChkHasId.Checked;
            On_HasId_HasChanged(ChkHasId.Checked);
            if (TxtIdName.Text.IsWhite()) { TxtIdName.Text = TxtDomainColumnName.Text + "_id"; }
            if (TxtIdType.Text.IsWhite())
            {
                if (ChkIsReadOnly.Checked) { TxtIdType.Text = "smallint"; } else { TxtIdType.Text = "int"; }
            }
        }

        private void ChkHasDescription_CheckedChanged(object sender, EventArgs e)
        {
            TxtDescriptionColumn.Visible = TxtDescriptionColumn.Enabled = ChkHasDescription.Checked;
        }

        private void TxtTableName_TextChanged(object sender, EventArgs e)
        {
            Raise_TableNameChanged(TxtTableName.Text);
            TxtIdName.Text = _BackingData.Id.SqlName;
        }
    }
}
