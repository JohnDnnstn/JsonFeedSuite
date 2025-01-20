using System.ComponentModel;

namespace GenericJsonSuite.EtlaToolbelt.Wizards;

/// <summary>Extensions to help with the ControlMapping of DataGridViews</summary>
public static class DataGridViewExtensions
{
    /// <summary>Change the current grid binding to bind the given List<typeparamref name="T"/>
    /// Useful if you want to reset from a saved list or add a row or rows programmatically
    /// </summary>
    /// <typeparam name="T">The underlying data that forms a row in the grid</typeparam>
    /// <param name="grid">The DataGridView</param>
    /// <param name="list">The list of values where each object is a row in the grid</param>
    public static void SetList<T>(this DataGridView grid, List<T> list)
    {
        BindingList<T> bindingList = new(list);
        grid.DataSource = bindingList;
    }

    /// <summary>Gets the actual data behind a BindingList of T behind a DataGridView
    /// Manipulate the actual data if a programmatic chance is require
    /// </summary>
    /// <typeparam name="T">The underlying data that forms a row in the grid</typeparam>
    /// <param name="grid">The DataGridView</param>
    /// <returns>The actual data behind a BindingList of T behind a DataGridView</returns>
    /// <exception cref="Exception"> thrown if either the DataSource is not a BindingList<typeparamref name="T"/> or it is null</exception>
    public static List<T> GetList<T>(this DataGridView grid)
    {
        if (grid.DataSource != null && grid.DataSource is BindingList<T> bindingList)
        {
            return [.. bindingList]; // i.e. bindingList.ToList()
        }

        // Houston we have a problem
        var gridName = grid.Name;
        var tName = typeof(T).Name;
        var msg = $"Failed to get backing data of grid {gridName} as List<{tName}>";
        Log.Error(msg);
        throw new Exception(msg);
    }

    /// <summary>Returns a copy of the actual data behind a BindingList of T behins a DataGridView
    /// Useful if you might want to revert back to a particular state later
    /// </summary>
    /// <typeparam name="T">The underlying data that forms a row in the grid</typeparam>
    /// <param name="grid">The DataGridView</param>
    /// <returns>A copy of the actual data behind a BindingList of T behind a DataGridView</returns>
    /// <exception cref="Exception"> thrown if either the DataSource is not a BindingList<typeparamref name="T"/> or it is null</exception>
    public static List<T> CloneList<T>(this DataGridView grid) where T : ICloneable
    {
        List<T> answer = [];
        var temp = grid.GetList<T>();
        if (temp != null)
        {
            foreach (T item in temp)
            {
                answer.Add((T)item.Clone());
            }
        }
        return answer;
    }
}
