﻿using System.Collections;

//------------------------------------------------------------------------------------------
// This file was generated from the EtlaTool.Wizards vsn:1.0 template
// Created 29/08/2023 17:44:59
// Copyright: Etla Services Ltd 2019-2023
//------------------------------------------------------------------------------------------


namespace GenericJsonSuite.EtlaToolbelt.Forms;

public partial class List2List : UserControl
{

    #region events
    public event EventHandler<EventArgs>? ListsChanged;
    public event EventHandler<DestinationSelectedArgs>? DestinationSelectedIndexChanged;
    public event Action<string>? SourceListInitWithMissingElement;

    public class DestinationSelectedArgs : EventArgs
    {
        public int SelectedIndex { get; set; }
        public string? SelectedText { get; set; }
    }

    protected virtual void BroadcastListChangedEvent(EventArgs e)
    {
        ListsChanged?.Invoke(this, e);
    }

    protected virtual void BroadcastDestinationSelectedIndexChanged(EventArgs e)
    {
        DestinationSelectedArgs args = new()
        {
            SelectedIndex = LstDestination.SelectedIndex,
            SelectedText = LstDestination.SelectedItem?.ToString(),
        };
        DestinationSelectedIndexChanged?.Invoke(this, args);
    }

    protected virtual void BroadcastSourceListInitWithMissingElement(string name)
    {
        SourceListInitWithMissingElement?.Invoke(name);
    }

    protected void ListBox_DrawItem(object sender, DrawItemEventArgs e)
    {
        if (sender is not ListBox box || e == null || e.Index < 0 || e.Index > box.Items.Count) { return; }
        string? item = box.Items[e.Index].ToString();
        if (item == null) { return; }

        Font? font = e.Font;
        if (font == null) { return; }
        Color colour = e.ForeColor;
        Color backColour = e.BackColor;

        foreach (var format in _Formats)
        {
            if (format.Items.Contains(item))
            {
                if (format.Foreground != null) { colour = (Color)format.Foreground; }
                if (format.Background != null) { backColour = (Color)format.Background; }
                if (OperatingSystem.IsWindows())
                {
                    if (format.FontStyle != null) { font = new Font(font, (FontStyle)format.FontStyle); }
                }
            }
        }

        e.DrawBackground();
        TextRenderer.DrawText(e.Graphics, item, font, e.Bounds.Location, colour, backColour);
    }

    #endregion

    private List<FormatInfo> _Formats { get; init; } = [];

    public bool Initialised { get; set; } = false;

    public bool IsValid { get; set; } = true;

    public IEnumerable ChosenItems { get { return GetChosenObjects(); } set { InitialiseDestination(value); } }

    public List2List()
    {
        InitializeComponent();
    }

    public void Initialise(IEnumerable source, IEnumerable destination)
    {
        InitialiseSource(source);
        InitialiseDestination(destination);
    }

    public void InitialiseSource(IEnumerable source, bool broadcastEvent = true)
    {
        var oldSrc = LstSource.ToStringList();
        LstSource.Items.Clear();

        foreach (var srcItem in source)
        {
            if (srcItem != null) { LstSource.Items.Add(srcItem); }
        }

        if (broadcastEvent && !oldSrc.SequenceEqual(LstSource.ToStringList()))
        {
            BroadcastListChangedEvent(new EventArgs());
        }
    }

    public void InitialiseDestination(IEnumerable destination, bool throwIfNotInSource = true)
    {
        IsValid = true;
        var oldDest = LstDestination.ToStringList();
        LstDestination.Items.Clear();
        if (destination != null)
        {
            foreach (var destItem in destination)
            {
                if (destItem != null)
                {
                    if (LstSource.Items.Contains(destItem))
                    {
                        LstSource.Items.Remove(destItem);
                        LstDestination.Items.Add(destItem);
                    }
                    else
                    {
                        IsValid = false;
                        if (throwIfNotInSource)
                        {
                            string name = destItem?.ToString() ?? "UNKNOWN";
                            BroadcastSourceListInitWithMissingElement(name);
                            //throw new Exception($"Destination item {destItem} was not in the source list");
                        }
                    }
                }
            }
        }
        if (!oldDest.SequenceEqual(LstDestination.ToStringList()))
        {
            BroadcastListChangedEvent(new EventArgs());
        }
    }

    /// <summary>Used when the user has already chosen but the underlying set of options has changed as it keeps the choices where possible
    /// 
    /// </summary>
    /// <param name="throwIfNotInSource"></param>
    public void ReinitialiseDestination(bool throwIfNotInSource = false)
    {
        var newDest = LstDestination.ToObjectList();
        InitialiseDestination(newDest, throwIfNotInSource);
    }

    public void ClearFormats()
    {
        _Formats.Clear();
    }

    public void AddFormats(IEnumerable itemsToFormat, Color? foreground, Color? background, FontStyle? style = null)
    {
        HashSet<string?> items = [];
        foreach (var item in itemsToFormat) { items.Add(item.ToString()); }

        if (!OperatingSystem.IsWindows()) { style = null; }

        var info = new FormatInfo() { Items = items, Foreground = foreground, Background = background, FontStyle = style };
        _Formats.Add(info);
    }

    public List<string> GetChosen()
    {
        return LstDestination.ToStringList();
    }

    public List<object> GetChosenObjects()
    {
        return LstDestination.ToObjectList();
    }

    public List<string> GetAll()
    {
        return [.. LstSource.ToStringList(), .. LstDestination.ToStringList()];
    }

    #region Moving items around
    private void BtnAddAll_Click(object sender, EventArgs e)
    {
        LstDestination.Items.AddRange(LstSource.Items);
        LstSource.Items.Clear();
        BroadcastListChangedEvent(e);
    }

    private void BtnAddSelected_Click(object sender, EventArgs e)
    {
        List<object> temp = [.. LstSource.SelectedItems];

        foreach (var item in temp)
        {
            LstDestination.Items.Add(item);
            LstSource.Items.Remove(item);
        }
        BroadcastListChangedEvent(e);
    }

    private void BtnRemoveSelected_Click(object sender, EventArgs e)
    {
        List<object> temp = [.. LstDestination.SelectedItems];

        foreach (var item in temp)
        {
            LstSource.Items.Add(item);
            LstDestination.Items.Remove(item);
        }
        BroadcastListChangedEvent(e);
    }

    private void BtnRemoveAll_Click(object sender, EventArgs e)
    {
        LstSource.Items.AddRange(LstDestination.Items);
        LstDestination.Items.Clear();
        BroadcastListChangedEvent(e);
    }

    private void BtnMoveTop_Click(object sender, EventArgs e)
    {
        List<object> temp = [.. LstDestination.SelectedItems];
        temp.Sort();
        temp.Reverse();

        foreach (var item in temp)
        {
            LstDestination.Items.Remove(item);
            LstDestination.Items.Insert(0, item);
        }
        BroadcastListChangedEvent(e);
    }

    private void BtnMoveUp_Click(object sender, EventArgs e)
    {
        var indices = LstDestination.SelectedIndices;
        var bottom = -1;
        bool inSelection = false;
        for (int ix = LstDestination.Items.Count - 1; ix >= 0; ix--)
        {
            if (indices.Contains(ix)) // i.e. this was selected
            {
                inSelection = true;
                if (bottom == -1) { bottom = ix; }
            }
            else
            {
                if (inSelection)
                {
                    var item = LstDestination.Items[ix];
                    LstDestination.Items.RemoveAt(ix);
                    LstDestination.Items.Insert(bottom, item);
                    inSelection = false;
                    bottom = -1;
                }
            }
        }
    }

    private void BtnMoveDown_Click(object sender, EventArgs e)
    {
        var indices = LstDestination.SelectedIndices;
        var bottom = -1;
        bool inSelection = false;
        for (int ix = 0; ix < LstDestination.Items.Count; ++ix)
        {
            if (indices.Contains(ix)) // i.e. this was selected
            {
                inSelection = true;
                if (bottom == -1) { bottom = ix; }
            }
            else
            {
                if (inSelection)
                {
                    var item = LstDestination.Items[ix];
                    LstDestination.Items.RemoveAt(ix);
                    LstDestination.Items.Insert(bottom, item);
                    inSelection = false;
                    bottom = -1;
                }
            }
        }
    }

    private void BtnMoveBottom_Click(object sender, EventArgs e)
    {
        List<object> temp = [.. LstDestination.SelectedItems];
        temp.Sort();

        foreach (var item in temp)
        {
            LstDestination.Items.Remove(item);
            LstDestination.Items.Add(item);
        }
        BroadcastListChangedEvent(e);
    }
    #endregion

    private void LstDestination_SelectedIndexChanged(object sender, EventArgs e)
    {
        BroadcastDestinationSelectedIndexChanged(e);
    }

    protected class FormatInfo
    {
        public HashSet<string?> Items { get; set; } = [];
        public Color? Foreground { get; set; }
        public Color? Background { get; set; }

        public FontStyle? FontStyle { get; set; }
    }
}

public static class ListBoxExtensions
{
    public static List<string> ToStringList(this ListBox box)
    {
        List<string> answer = [];
        foreach (var item in box.Items)
        {
            answer.Add(item?.ToString() ?? "");
        }
        return answer;
    }

    public static List<object> ToObjectList(this ListBox box)
    {
        List<object> answer = [];
        foreach (var item in box.Items)
        {
            answer.Add(item);
        }
        return answer;
    }
}

public class List2ListControlMapping(List2List ctrl, string dataSourcePropertyName, string dataDestinationPropertyName)
    : ControlMapping(ctrl, nameof(ctrl.ChosenItems), dataDestinationPropertyName)
{
    private string _DataSourcePropertyName { get; set; } = dataSourcePropertyName;
    private string _DataDestinationPropertyName { get; set; } = dataDestinationPropertyName;

    public override void Load(IWizardData data)
    {
        if (Ctrl is List2List list2list)
        {
            var source = data.GetPropertyValue(_DataSourcePropertyName);
            if (source != null && source is IEnumerable sourceEnumerable)
            {
                list2list.InitialiseSource(sourceEnumerable);
                base.Load(data);
            }
        }
    }

    public override void Save(IWizardData data)
    {
        if (Ctrl is List2List list2list)
        {
            var chosen = list2list.GetChosenObjects();
            if (chosen != null && chosen is IEnumerable chosenEnumerable)
            {
                data.SetPropertyValue(_DataDestinationPropertyName, chosenEnumerable);
                base.Save(data);
            }
        }
    }
}

public static class FormMapperExtensions
{
    public static void Add(this FormMapper formMap, List2List list2List, string sourcePropertyName, string destinationPropertyName)
    {
        var controlMapping = new List2ListControlMapping(list2List, sourcePropertyName, destinationPropertyName);
        formMap.AddMapping(controlMapping);
    }
}
