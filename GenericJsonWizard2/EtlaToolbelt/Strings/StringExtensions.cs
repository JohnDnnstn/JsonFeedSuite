#pragma warning disable IDE0130 // Namespace does not match folder structure
using GenericJsonSuite;

namespace GenericJsonWizard.EtlaToolbelt.Strings;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class StringExtensions
{
    public static string GetSingular(this string? str)
    {
        if (str.IsWhite()) { return ""; }
        var lower = str.ToLower();
        if (lower == "species") { return str; }
        if (lower.EndsWith("ies")) { return str[..^3] + "y"; }
        if (lower.EndsWith("statuses")) { return str[..^2]; }
        if (lower.EndsWith('s')) { return str[..^1]; }
        return str;
    }

    public static string GetPlural(this string? str)
    {
        if (str.IsWhite()) { return ""; }
        var lower = str.ToLower();
        if (lower == "species") { return str; }
        if (lower.EndsWith("status")) { return str + "es"; }
        if (lower.EndsWith('s')) { return str; }
        if (lower.EndsWith('y')) { return str[..^1] + "ies"; }
        return str + 's';
    }

    public static string MakeIdentifier(this string raw)
    {
        var chars = raw.Trim().ToCharArray();
        if (chars.Length < 1) { return "_"; }
        if (!char.IsLetter(chars[0])) { chars[0] = '_'; }
        for (int ix = 1; ix < chars.Length; ++ix)
        {
            if (!char.IsLetterOrDigit(chars[ix])) { chars[ix] = '_'; }
        }
        return new string(chars).ToLower();
    }

}
