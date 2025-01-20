using GenericJsonWizard.EtlaToolbelt.Strings;

namespace GenericJsonWizard.EtlaToolbelt.Forms;

/// <summary>Allows one (Target) TextBox to change consistently when another (Source) TextBox changes unless the Target was changed manually 
/// 
/// </summary>
public class Dependent
{
    private TextBox _Source { get; set; }
    private TextBox _Target { get; set; }
    private Func<string, string> _Processor { get; set; }
    private bool _UseProcess { get; set; } = true;
    private bool _CalledFromDependencyUpdate = false;

    public static string MakeSame(string sourceString) { return sourceString; }

    public static string MakeId(string sourceString) { return $"{sourceString.GetSingular()}_id"; }

    public static string MakeSingular(string sourceString) { return sourceString.GetSingular(); }

    public Dependent(TextBox source, TextBox target, Func<string, string> processor)
    {
        source.TextChanged += Source_TextChanged;
        target.TextChanged += Target_TextChanged;
        _Source = source;
        _Target = target;
        _Processor = processor;
        string initially = target.Text;
        string processedValue = _Processor(source.Text);
        _UseProcess = initially.Equals(processedValue);
    }

    private void Source_TextChanged(object? sender, EventArgs e)
    {
        _CalledFromDependencyUpdate = true;
        string processedValue = _Processor(_Source.Text);
        if(_UseProcess) { _Target.Text = processedValue; }
        _CalledFromDependencyUpdate = false;
    }

    private void Target_TextChanged(object? sender, EventArgs e)
    {
        if (!_CalledFromDependencyUpdate)
        {
            string processedValue = _Processor(_Source.Text);
            _UseProcess = _Target.Text.Equals(processedValue);
        }
    }
}
