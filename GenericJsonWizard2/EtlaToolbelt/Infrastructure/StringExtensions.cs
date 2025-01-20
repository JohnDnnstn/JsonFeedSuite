namespace GenericJsonSuite;

/// <summary>String utilities</summary>
public static class StringExtensions
{
    /// <summary>Converts null to empty string and replaces Windows directory separators with unix-style ones
    /// This saves having to have special processing for escape characters etc.
    /// It is not foolproof but is usually sufficient
    /// </summary>
    /// <param name="str">The string to </param>
    /// <returns>A "safer" version of the string for use in json documents etc.</returns>
    public static string ToJsonSafePath(this string? str) { var temp = str ?? ""; return temp.Replace(@"\", "/"); }
}
