using GenericJsonWizard.EtlaToolbelt.Forms;

namespace GenericJsonSuite.EtlaToolbelt.Forms;

/// <summary>Similar functionality to Binding
/// The primary difference is that mapping back from the form to the original data only happens if the user is accepts the form's state
/// Each control is associated with a particular property of the given backing data
/// Which property of the control is associated, depends on the control's type
/// e.g. A TextBox will associate data with it's Text property but a CheckBox will associate data with its Checked property
/// Special cases can arise for RadioButtons and DataGridViews
/// </summary>
public class FormMapper
{
    protected IWizardData Data { get; set; }

    public FormMapper(IWizardData data)
    {
        Data = data;
    }

    /// <summary>The list of Controls-to-DataProperties associated with this FormMapper</summary>
    private List<IControlMapping> _Mappings { get; set; } = [];

    #region Control-specific Add methods

    /// <summary>Generic Add, e.g. for user-created controls
    /// Means the _Mappings list is not exposed
    /// </summary>
    /// <param name="mapping">An arbitrary ControlMapping to add to the list of control mappings</param>
    public void AddMapping(ControlMapping mapping)
    {
        _Mappings.Add(mapping);
    }

    /// <summary>Creates a mapping between a checkbox's Checked property and a bool property of the backing data</summary>
    /// <param name="ctrl">The CheckBox to map</param>
    /// <param name="dataPropertyName">The name of the property of the backing data object to map to this control</param>
    public void Add(CheckBox ctrl, string dataPropertyName)
    {
        var ctrlPropertyName = nameof(ctrl.Checked);
        _Mappings.Add(new ControlMapping(ctrl, ctrlPropertyName, dataPropertyName));
    }

    /// <summary>Creates a mapping between a CombBox's SelectedItem property and a property of the backing data</summary>
    /// <param name="ctrl">The ComboBox to map</param>
    /// <param name="dataPropertyName">The name of the property of the backing data object to map to this control</param>
    /// <param name="ctrlPropertyName">Optional.  A different control property to set.  Default: SelectedItem</param>
    public void Add(ComboBox ctrl, string dataPropertyName, string? ctrlPropertyName = null)
    {
        ctrlPropertyName ??= nameof(ctrl.SelectedItem);
        _Mappings.Add(new ControlMapping(ctrl, ctrlPropertyName, dataPropertyName));
    }

    /// <summary>Creates a mapping between a CombBox's SelectedItem property and a property of the backing data</summary>
    /// <param name="ctrl">The ComboBox to map</param>
    /// <param name="dataPropertyName">The name of the property of the backing data object to map to this control</param>
    /// <param name="ctrlPropertyName">A different control property to set.  Default: SelectedItem</param>
    public void Add<T>(ComboBox ctrl, string dataPropertyName, string listDataPropertyName, string? listCtrlPropertyName)
    {
        var ctrlPropertyName = nameof(ctrl.SelectedItem);
        _Mappings.Add(new ComboControlMapping<T>(ctrl, ctrlPropertyName, dataPropertyName, listDataPropertyName));
    }

    /// <summary>Creates a mapping between a DataGridView's DataSource property and a List-of-T property of the backing data</summary>
    /// <see cref="GridControlMapping"/> for more details
    /// However what currently happens is that the List-of-T is cloned and the clone is the basis of a BindingList-of-T
    /// The DomainColumnType T must be cloneable
    /// <param name="ctrl">The DataGridView to map</param>
    /// <param name="dataPropertyName">The name of the property of the backing data object to map to this control</param>
    public void Add<T>(DataGridView ctrl, string dataPropertyName) where T : ICloneable, new()
    {
        var answer = new GridControlMapping<T>(ctrl, nameof(ctrl.DataSource), dataPropertyName);
        _Mappings.Add(answer);
        //return answer;
    }

    /// <summary>Creates a mapping between a 2D data source and a Grid where every cell in the grid is of the same type</summary>
    /// <typeparam name="TCol">DataGridCellType</typeparam>
    /// <param name="ctrl">The DataGridView to map</param>
    /// <param name="dataPropertyName">The name of the property of the backing data object to map to this control</param>
    public void Add2D<TCol>(DataGridView ctrl, string dataPropertyName) where TCol : DataGridViewColumn, new()
    {
        var mapping = new GridControlMapping2D<TCol>(ctrl, dataPropertyName);
        _Mappings.Add(mapping);
    }

    /// <summary>Creates a mapping between a NumericUpDown's Value property and an integer property of the backing data</summary>
    /// <param name="ctrl">The NumericUpDown to map</param>
    /// <param name="dataPropertyName">The name of the property of the backing data object to map to this control</param>
    public void Add(NumericUpDown ctrl, string dataPropertyName)
    {
        var ctrlPropertyName = nameof(ctrl.Value);
        _Mappings.Add(new ControlMapping(ctrl, ctrlPropertyName, dataPropertyName));
    }

    /// <summary>Creates a mapping between a RadioButton's Checked property and a bool property of the backing data</summary>
    /// <param name="ctrl">The RadioButton to map</param>
    /// <param name="dataPropertyName">The name of the property of the backing data object to map to this control</param>
    public void Add(RadioButton ctrl, string dataPropertyName)
    {
        var answer = new RadioControlMapping<bool>(ctrl, dataPropertyName, true);
        _Mappings.Add(answer);
    }
    /// <summary>Creates a mapping between a RadioButton's Checked property and an Enum or integer property of the backing data
    /// This allows a group of RadioButtons to "return" a single value
    /// Each RadioButton in the group is associated with a different value
    /// As only one RadioButton in a group can be checked, the value associated with that button is what the backing data property is set to
    /// </summary>
    /// <typeparam name="T">Usually an Enum but could be a bool, or an int, or any value type if it is also IConvertible.</typeparam>
    /// <param name="ctrl">The RadioButton to map</param>
    /// <param name="dataPropertyName">The name of the property of the backing data object to map to this control</param>
    /// <param name="checkedValue">The value to set in the property of the backing data object</param>
    public void Add<T>(RadioButton ctrl, string dataPropertyName, T checkedValue) where T : struct, IConvertible
    {
        var answer = new RadioControlMapping<T>(ctrl, dataPropertyName, checkedValue);
        _Mappings.Add(answer);
    }

    /// <summary>Creates a mapping between a TextBox's Text property and an string property of the backing data</summary>
    /// <param name="ctrl">The TextBox to map</param>
    /// <param name="dataPropertyName">The name of the property of the backing data object to map to this control</param>
    public void Add(TextBox ctrl, string dataPropertyName)
    {
        var ctrlPropertyName = nameof(ctrl.Text);
        _Mappings.Add(new ControlMapping(ctrl, ctrlPropertyName, dataPropertyName));
    }
    #endregion

    /// <summary>Copies the contents of the IWizardData object to the controls of a Form according to the control mappings</summary>
    public void Load()
    {
        foreach (var mapping in _Mappings)
        {
            mapping.Load(Data);
        }
    }

    /// <summary>Copies the controls of a Form object to the contents of the IWizardData according to the control mappings</summary>
    public void Save()
    {
        foreach (var mapping in _Mappings)
        {
            mapping.Save(Data);
        }
    }

    public FormMapper Clone(IWizardData? backingData = null)
    {
        FormMapper answer = (FormMapper)MemberwiseClone();
        if (backingData != null) { answer.Data = backingData; }
        return answer;
    }
}
