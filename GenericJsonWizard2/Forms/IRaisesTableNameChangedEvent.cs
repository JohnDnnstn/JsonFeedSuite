namespace GenericJsonWizard.Forms;

public interface IRaisesTableNameChangedEvent
{
    #region events
    delegate void TableNameChanged_Handler(string newName);
    event TableNameChanged_Handler? TableNameChanged_Event;
    //public void Raise_TableNameChanged(string newName) { TableNameChanged_Event?.Invoke(newName); }
    #endregion
}
