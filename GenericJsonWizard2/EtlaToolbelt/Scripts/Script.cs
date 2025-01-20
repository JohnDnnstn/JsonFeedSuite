// This class is generated from the EtlaTool.Scripts template

using System.Text;

namespace GenericJsonWizard.EtlaToolbelt.Scripts;

/// <summary>A class containing methods to use when generating scripts
/// Normal usage:
/// Scr() : Adds newline to an internal StringBuilder
/// Scr(_text_) : Adds newline, a number of tabs, and _text_ to an internal StringBuilder
/// Scr(_text_,false) : Adds a number of tabs, and _text_ to an internal StringBuilder
/// Scr(_text_,_int>0_) : Adds a number of tabs, and _text_ to an internal StringBuilder and the next line will be further indented
/// Scr(_text_,_int<0_) : Adds a reduced number of tabs, and _text_ to an internal StringBuilder
/// Scr(_text_,_prefix_) : Adds a number of tabs, _prefix_, and _text_ to an internal StringBuilder
/// ScrJoin(_IEnumberable_string_,_infix_) : Adds Scr(_text0_), _infix_, Scr(_text1_), _infix_, ... , _infix_, Scr(_textN_) (i.e. similar to join in NOT adding the final infix)
/// then
/// ReturnAndClear() : returns the contents of the StringBuilder as a string
/// 
/// </summary>
public class Script
{
    private readonly StringBuilder _Builder = new();
    protected int Indent = 0;

    /// <summary>Add a newline (only)/// </summary>
	public void Scr(int indentChange = 0, bool startWithNewline = true)
    {
        if (startWithNewline) { _Builder.AppendLine(); }
        Indent += indentChange;
    }

    /// <summary>Add text to the script, possibly not prepended by a newline and optionally changing indents after this line
    /// If the indentChange is positive, the *next* line and following lines will have that number of extra tabs between the newline and the string
    /// If the indentChange is negative, this line (and following lines) will have that number of indentation tabs removed *before* this string
    /// </summary>
    /// <param name="text">The text to append to the script</param>
    /// <param name="startWithNewline">Whether to start with a newline (true) or without a newline (false)</param>
    /// <param name="changeIndentAfterwards">Optional: Any change to indentation of following lines</param>
    public void Scr(string? text, bool startWithNewline, int indentChange = 0)
    {
        Scr(text, "", startWithNewline, indentChange: indentChange);
    }

    /// <summary>Add a newline to the script, followed by an indentation (number of tabs) followed by a string
    /// If the indentChange is positive, the *next* line and following lines will have that number of extra tabs between the newline and the string
    /// If the indentChange is negative, this line (and following lines) will have that number of indentation tabs removed *before* this string
    /// </summary>
    /// <param name="text">The text to append to the script</param>
    /// <param name="indentChange">The change to the indenting.  -ve happens before text; +ve happens on following lines</param>
    public void Scr(string? text, int indentChange = 0, string preNewline = "")
    {
        Scr(text, "", true, preNewline, indentChange);
    }

    /// <summary>Add text to the script, possibly not prepended by a newline, optionally prepended by a prefix and optionally changing indents after this line
    /// </summary>
    /// <param name="text">The text to append to the script</param>
    /// <param name="preamble">string to add before any newline</param>
    /// <param name="addNewlineAndTabs">Optional: Whether to start with a newline and tabs (true) or without a newline (false).  Default: true</param>
    /// <param name="prefix">String to prepend to the text after any newline and tabs. Default: ""</param>
    /// <param name="indentChange">Optional: Any hange to indentation in this line or following lines.  Default: 0</param>
    public void Scr(string? text, string preamble, bool addNewlineAndTabs = true, string prefix = "", int indentChange = 0)
    {
        _Builder.Append(preamble);
        if (indentChange < 0) { Indent += indentChange; }
        if (addNewlineAndTabs)
        {
            _Builder.AppendLine();
            for (int ix = 0; ix < Indent; ix++) { _Builder.Append('\t'); }
        }
        _Builder.Append(prefix);
        _Builder.Append(text);
        if (indentChange > 0) { Indent += indentChange; }
    }

    /// <summary>Adds a string, combining a set of strings where the elements are separated by a separator, to the internal StringBuilder
    /// Optionally, the first string can be "bare", i.e. not preceded by any elements of the separator
    /// The separator is multi-part consisting of:
    /// * the preamble before any newline
    /// * Optionally: a newline and a number of tabs (the number controlled by the internal Indent number)
    /// * Optionally: the prefix, after any newline and tabs, but before the current string is added
    /// </summary>
    /// <param name="strings">The list of strings to be joined</param>
    /// <param name="preamble">A constant string to add immediately after the previous string in the list of strings</param>
    /// <param name="addNewlineAndTabs"></param>
    /// <param name="prefix"></param>
    protected void ScrJoin
        (
        IEnumerable<string> strings,
        string preamble = "", bool addNewlineAndTabs = true, string prefix = "",
        string? firstPreamble = null, bool? firstNewlineAndTabs = null, string? firstPrefix = null
        )
    {
        bool first = true;
        firstPreamble ??= preamble;
        firstNewlineAndTabs ??= addNewlineAndTabs;
        firstPrefix ??= prefix;
        foreach (string str in strings)
        {
            if (first)
            {
                Scr(str, firstPreamble, firstNewlineAndTabs.Value, firstPrefix);
                first = false;
            }
            else
            {
                Scr(str, preamble, addNewlineAndTabs, prefix);
            }
        }

        //using (var enumerator = strings.GetEnumerator())
        //{
        //    if (enumerator.MoveNext())                  // Move to the 0-th string if it exists
        //    {
        //        string current = enumerator.Current;    // Get the 0th text

        //        // Process all but the last string
        //        while (enumerator.MoveNext())           // move to the 1th, (2th ... N-1th)
        //        {
        //            Scr(current, preamble, addNewlineAndTabs, prefix, 0);     // Add the (possibly newline) plus the 0th, (1th ... N-2th) text to the internal String
        //            current = enumerator.Current;       // Get the 1th (2th ... N-1th) text
        //        }

        //        // Process thelast string
        //        Scr(current, addNewlineAndTabs);         // Add the (possibly newline) plus the N-1th text to the internal String
        //    }
        //}
    }

    protected void ScrJoin(IEnumerable<string> strings, bool addNewlineAndTabs, string prefix)
    {
        bool first = true;
        foreach (var str in strings)
        {
            if (first) { first = false; Scr("", addNewlineAndTabs); } else { Scr(prefix, addNewlineAndTabs); }
            Scr(str, false);
        }
    }

    public string PrettyJoin(IEnumerable<string> items, string separator = ",", bool fill = false, int extraIndent = 0)
    {
        var tempIndent = Indent + extraIndent;
        string[] strings = [.. items];
        if (strings.Length < 1) { return ""; }
        int lineLength = 120;
        int tabLength = tempIndent * 4;
        int maxLength = lineLength - tabLength;
        int separatorLength = separator.Length;
        int length = 0;
        foreach (var str in strings) { length += str.Length + separatorLength; }
        if (length < maxLength && fill) { return string.Join(separator, strings); }

        StringBuilder answer = new(strings[0]);
        string tabs = new('\t', tempIndent);
        if (fill)
        {
            StringBuilder line = new();
            for (int ix = 1; ix < strings.Length; ++ix)
            {
                line.Append(separator);
                line.Append(strings[ix]);
                if (line.Length > maxLength)
                {
                    answer.AppendLine(line.ToString());
                    answer.Append(tabs);
                    line.Clear();
                }
            }
            return answer.ToString();
        }
        for (int ix = 1; ix < strings.Length; ++ix)
        {
            answer.AppendLine(separator);
            answer.Append(tabs);
            answer.Append(strings[ix]);
        }
        return answer.ToString();
    }

    /// <summary>Returns the script that has been constructed and resets everything so that things start from scratch with the next call to Scr.
    /// </summary>
    /// <returns>The script that has been constructed</returns>
    public string ReturnAndClear()
    {
        var temp = _Builder.ToString();
        _Builder.Clear();
        Indent = 0;
        return temp;
    }
}
