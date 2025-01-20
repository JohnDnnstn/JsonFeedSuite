using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.BackingData.ColumnMetadata;

namespace GenericJsonWizard.Forms
{
    public partial class ColumnDetailsForm : BaseWizardForm
    {
        public ColumnDetailsForm(JsonColumn backingData)
        {
            InitializeComponent();
            TxtRubric.Text = "Fine-tune a column definition";

            Map = new FormMapper(backingData);
            Map.Add(TxtJsonName, nameof(backingData.JsonName));
            Map.Add(TxtDbName, nameof(backingData.SqlName));
            Map.Add(TxtJsonType, nameof(backingData.JsonTypeAsString));
            Map.Add(TxtDbType, nameof(backingData.SqlType));
            Map.Add(TxtOriginalPath, nameof(backingData.JsonPathInOriginal));
            Map.Add(TxtDbJsonPath, nameof(backingData.JsonPathInDb));
            Map.Add(ChkNullable, nameof(backingData.Nullable));
            Map.Add(TxtDefault, nameof(backingData.Default));
            Map.Add(TxtToNull, nameof(backingData.ToNull));
            Map.Add(TxtFromNull, nameof(backingData.FromNull));
            Map.Add(TxtDescription, nameof(backingData.Description));
            Map.Add(CmbStandardiser, nameof(backingData.Standardiser), "Text"); // Standardises the value based on the (free-form) Text property of the combobox
            Map.Add(CmbBadWhenTrue, nameof(backingData.BadWhenTrue), "Text"); // Checks on value based on the code in the (free-form) Text property of the combobox
        }

        private void ColumnDetailsForm_Load(object sender, EventArgs e)
        {
            TtpTips.SetToolTip(CmbStandardiser, "The code specified here is applied to try and standardise the value (e.g. for cph like 12-345-6789 standardised to 12/345/6789.  Ensure custom code has brackets.");
            TtpTips.SetToolTip(CmbBadWhenTrue, "If the code specified here returns true then a warning will be issued at load time.  Ensure custom code has brackets.");
        }
    }
}
