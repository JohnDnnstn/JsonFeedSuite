using GenericJsonWizard.EtlaToolbelt.Forms;
using System.ComponentModel;

namespace GenericJsonSuite.EtlaToolbelt.Forms;

/// <summary>This class contains information on how to associate a WinForm control with the property of a backing data object
/// <seealso cref="GridControlMapping"/> and <seealso cref="RadioControlMapping"/>
/// </summary>
/// <remarks>Constructor</remarks>
/// <param name="ctrl">The Winforms Control being associated with a property of the backing data object</param>
/// <param name="ctrlPropertyName">The specific property of the WinForms control whichis being associated with a property of the backing data object</param>
/// <param name="dataPropertyName">The specific property of the backing data object being associated with this control</param>
public class ControlMapping(Control ctrl, string ctrlPropertyName, string dataPropertyName) : IControlMapping
{
    /// <summary>The Winforms Control being associated with a property of the backing data object</summary>
    protected Control Ctrl { get; set; } = ctrl;

    /// <summary>The specific property of the WinForms control whichis being associated with a property of the backing data object</summary>
    protected string CtrlPropertyName { get; set; } = ctrlPropertyName;

    /// <summary>The specific property of the backing data object being associated with this control</summary>
    protected string DataPropertyName { get; set; } = dataPropertyName;

    /// <summary>Sets the Control's property with the value currently in the backing data object's property
    /// <see cref="MappingExtensions"/> for the mechanism for getting and setting properties of objects by name
    /// </summary>
    /// <param name="data">The backing data object</param>
    public virtual void Load(IWizardData data)
    {
        var val = data.GetPropertyValue(DataPropertyName);
        Ctrl.SetPropertyValue(CtrlPropertyName, val);
    }

    /// <summary>Sets the backing data object's property from the current value of the Control's property
    /// <see cref="MappingExtensions"/> for the mechanism for getting and setting properties of objects by name
    /// </summary>
    /// <param name="data">The backing data object</param>
    public virtual void Save(IWizardData data)
    {
        var val = Ctrl.GetPropertyValue(CtrlPropertyName);
        data.SetPropertyValue(DataPropertyName, val);
    }
}

/// <summary>This class is derived from ControlMapping for a Combo box where the list of items can be changed and needs to be recorded
/// This contrasts with the normal mapping which just records the selected item
/// The items in the Items property are mapped to List<typeparamref name="T"/> in the WizardData
/// </summary>
/// <typeparam name="T">The type of each item in the drop-down list</typeparam>
public class ComboControlMapping<T> : ControlMapping
{
    protected string ListDataPropertyName { get; set; }

    /// <summary>Constructor</summary>
    /// <param name="ctrl">The Winforms ComboBox being mapped</param>
    /// <param name="chosenCtrlPropertyName">The name of the property of the ComboBox - usually SelectedItem</param>
    /// <param name="chosenDataPropertyName">The name of the WizardData property that is mapped to the control's chosenCtrlPropertyName property</param>
    /// <param name="listDataPropertyName">The name of the WizardData property that is mapped to the control's Items property </param>
    public ComboControlMapping(ComboBox ctrl, string chosenCtrlPropertyName, string chosenDataPropertyName, string listDataPropertyName)
        : base(ctrl, chosenCtrlPropertyName, chosenDataPropertyName)
    { 
        ListDataPropertyName = listDataPropertyName;
    }

    /// <summary>Copies data to the ComboBox's Items array from a List<typeparamref name="T"/> in the WizardData 
    /// and into the ComboBox's chosenCtrlPropertyName (usually SelectedItem) from the chosenDataPropertyName in the WizardData 
    /// </summary>
    /// <param name="data">The WizardData that maps to the form's controls</param>
    public override void Load(IWizardData data)
    {
        object? valObject = data.GetPropertyValue(ListDataPropertyName);
        if (valObject is List<T> valList)
        {
            object[] val = [.. valList];
            ((ComboBox)Ctrl).Items.AddRange(val);
        }
        base.Load(data);
    }

    /// <summary>Copies data from the ComboBox's Items array to a List<typeparamref name="T"/> in the WizardData 
    /// and from the ComboBox's chosenCtrlPropertyName (usually SelectedItem) into the chosenDataPropertyName in the WizardData 
    /// </summary>
    /// <param name="data">The WizardData that maps to the form's controls</param>
    public override void Save(IWizardData data)
    {
        base.Save(data);
        if (Ctrl is ComboBox combo)
        {
            List<T> list = [];
            foreach(T val in combo.Items) { list.Add(val); }
            data.SetPropertyValue(ListDataPropertyName, list);
        }
    }
}

/// <summary>This class is derived from <see cref="ControlMapping"/> for a DataGridView rather than a generic Control
/// It contains information on how to associate the DataGridView with a List-of-T property of a backing data object
/// </summary>
/// <typeparam name="T">The type which will map to a row in the DataGridView, which must be ICloneable</typeparam>
/// <remarks>Primary Constructor</remarks>
public class GridControlMapping<T> : ControlMapping where T : ICloneable, new()
{
    /// <param name="ctrl">The Winforms Control being associated with a property of the backing data object</param>
    /// <param name="ctrlPropertyName">The specific property of the WinForms control whichis being associated with a property of the backing data object</param>
    /// <param name="dataPropertyName">The specific property of the backing data object being associated with this control</param>
    public GridControlMapping(DataGridView ctrl, string ctrlPropertyName, string dataPropertyName) 
        : base(ctrl, ctrlPropertyName, dataPropertyName)
    {
    }

    /// <summary>Sets the DataGridView's property with the value currently in the backing data object's property
    /// The current List-of-T is cloned and the clone becomes the basis of a BindingList-of-T used in the Control's DataSource
    /// Using DataSource appears to be the easiest way of getting the required functionality
    /// Using a clone of the current List-of-T means the backing data is unaffected if the user exits the form without accepting the new values
    /// <see cref="MappingExtensions"/> for the mechanism for getting and setting properties of objects by name
    /// </summary>
    /// <param name="data">The backing data object</param>
    /// <exception cref="Exception"> thrown if the property is null, or if the property is not a List-of-T</exception>
    public override void Load(IWizardData data)
    {
        var initialValue = data.GetPropertyValue(DataPropertyName);
        if (initialValue != null && initialValue is List<T> initialList)
        {
            var itemList = Clone(initialList);
            var bindingList = new BindingList<T>(itemList);
            ((DataGridView)Ctrl).DataSource = bindingList;
        }
        else 
        {
            var reason = "null";
            if (initialValue != null ) { reason = "not a List<ICloneable>"; }
            var msg = $"Unable to load property '{DataPropertyName}' into the DataGridView as it is {reason}";
            Log.Error(msg);
            throw new Exception(msg);
        }
    }

    /// <summary>Sets the backing data object's property from the current value of data in the DataGridView
    /// <see cref="MappingExtensions"/> for the mechanism for getting and setting properties of objects by name
    /// </summary>
    /// <param name="data">The backing data object</param>
    /// <exception cref="Exception"> thrown if the Control's DataSource property is null, or if the property is not a BindingList-of-T</exception>
    public override void Save(IWizardData data)
    {
        var val = Ctrl.GetPropertyValue(CtrlPropertyName);
        if (val != null && val is BindingList<T> bindingList)
        {
            var valList = bindingList.ToList();
            data.SetPropertyValue(DataPropertyName, valList);
        }
        else
        {
            var reason = "null";
            if (val != null) { reason = "not a BindingList<ICloneable>"; }
            var msg = $"Unable to load property '{DataPropertyName}' from the DataGridView as it is {reason}";
            Log.Error(msg);
            throw new Exception(msg);
        }
    }

    /// <summary>Creates a clone of a List-of-T (T has been defined as ICloneable in the class definition)</summary>
    /// <param name="original">The List-of-T to clone</param>
    /// <returns>A clone of the List-of-T (shallow or deep depends on T's Clone function)</returns>
    private static List<T> Clone(List<T> original)
    {
        var cloneList = new List<T>(original.Count);
        foreach (T item in original) { cloneList.Add((T)item.Clone()); }
        return cloneList;
    }
}

/// <summary>This class maps a 2-D array to a DataGridView and also defines each column as having a particular type
/// 
/// </summary>
/// <typeparam name="TCol">The column type used for each column</typeparam>
public class GridControlMapping2D<TCol> : IControlMapping where TCol : DataGridViewColumn, new()
{
    protected DataGridView Ctrl { get; set; }
    protected string DataPropertyName { get; set; }

    public GridControlMapping2D(DataGridView ctrl, string dataPropertyName)
    { 
        Ctrl = ctrl;
        DataPropertyName = dataPropertyName;
    }

    public void Load(IWizardData backingData) 
    {
        var initialValue = backingData.GetPropertyValue(DataPropertyName);
        if (initialValue is not string[,] data ) { return; }
        
        var rowCount = data.GetLength(0);
        var colCount = data.GetLength(1);

        if (Ctrl is DataGridView grid)
        {
            grid.Columns.Clear();
            for (int ix = 0; ix < colCount; ++ix)
            {
                TCol col = new();
                grid.Columns.Add(col);
            }
            for (int rowIx = 0; rowIx < rowCount; ++rowIx)
            {
                DataGridViewRow row = new();
                row.CreateCells(grid);
                for(int colIx = 0; colIx < colCount; ++colIx) 
                {
                    row.Cells[colIx].Value = data[rowIx,colIx];
                }
                grid.Rows.Add(row);
            }
        }
    }

    public void Save(IWizardData backingData)
    {
        // Remove the blank New Row row if it exists
        var rowCount = Ctrl.Rows.Count;
        if (Ctrl.Rows[rowCount-1].IsNewRow) { --rowCount; }

        // Copy the grid contents into a new array
        string[,] data = new string[rowCount, Ctrl.ColumnCount];
        for (int row=0; row< rowCount; ++row) 
        {
            for(int col = 0; col < Ctrl.ColumnCount; ++col)
            {
                data[row, col] = Ctrl.Rows[row].Cells[col].Value?.ToString() ?? "";
            }
        }
        backingData.SetPropertyValue(DataPropertyName, data);
    }
}


/// <summary>This class is derived from <see cref="ControlMapping"/> for a RadioButton rather than a generic Control
/// It contains information on how to associate the Checked propert of a RadioButton in a group with a T-value property of a backing data object
/// </summary>
/// <typeparam name="T">The type of the named property of the backing data object (normally an Enum but any IConvertible value type is possible)</typeparam>
/// <remarks>Primary Constructor</remarks>
/// <param name="ctrl">The RadioButton being associated with a property of the backing data object</param>
/// <param name="ctrlPropertyName">The Checked property RadioButton which is being associated with a property of the backing data object</param>
/// <param name="dataPropertyName">The specific property of the backing data object being associated with this control</param>
/// <param name="checkedValue">The specific value associated with the Checked property of this RadioButton</param>
public class RadioControlMapping<T>(RadioButton ctrl, string dataPropertyName, T checkedValue) : ControlMapping(ctrl, nameof(ctrl.Checked), dataPropertyName) where T : struct, IConvertible
{

    /// <summary>Sets the RadioButton's Checked property if the value currently in the backing data object's property matches the value specified for this RadioButton
    /// <see cref="MappingExtensions"/> for the mechanism for getting and setting properties of objects by name
    /// </summary>
    /// <param name="data">The backing data object</param>
    /// <exception cref="Exception"> thrown if the property is not a T</exception>
    public override void Load(IWizardData data)
    {
        var initialValue = data.GetPropertyValue(DataPropertyName);
        if (initialValue is T val)
        {
            ((RadioButton)Ctrl).Checked = val.Equals(checkedValue);
        }
        else
        {
            var reason = "not the expected type";
            var msg = $"Unable to set the RadioButton checked proprty as '{DataPropertyName}' is {reason}";
            Log.Error(msg);
            throw new Exception(msg);
        }
    }

    /// <summary>Sets the backing data object's property to the specified value if the RadioButton's Checked value is true
    /// <see cref="MappingExtensions"/> for the mechanism for getting and setting properties of objects by name
    /// </summary>
    /// <param name="data">The backing data object</param>
    /// <exception cref="Exception"> thrown if the backing data object's specified property cannot be set</exception>
    public override void Save(IWizardData data)
    {
        try
        {
            var isChecked = ((RadioButton)Ctrl).Checked;
            if (isChecked) { data.SetPropertyValue(DataPropertyName, checkedValue); }
        }
        catch (Exception ex)
        {
            var msg = $"Internal error.  Failed to set '{DataPropertyName}' from RadioButton {((RadioButton)Ctrl).Text}";
            Log.Error(msg,ex);
            throw new Exception(msg,ex);
        }
    }
}
