namespace GenericJsonSuite.EtlaToolbelt.Logs;

/// <summary>A Log sink which sends a fire-and-forget UDP message to a UDP listener
/// Not implemented yet
/// </summary>
public class UdpSink : AbstractLogSink
{
    /// <summary>Default Constructor</summary>
    public UdpSink()
    {
        LevelToShow = LogLevel.SHOW_INFO;
        Instance = 0;
    }

    /// <summary>Constructor</summary>
    public UdpSink(LogLevel levelToShow, int instance)
    {
        LevelToShow = levelToShow;
        Instance = instance;
    }

    /// <summary>Sends the log message via UDP - Not implemented yet</summary>
    /// <param name="logMessage">The log message to write</param>
    public override void Write(LogMessage logMessage)
    {
        try
        {
            var message = Format(logMessage);
            //Trace.WriteLine(message);
        }
        catch (Exception ex)
        {
            Log.IgnoreExceptInDev(ex);
        }
    }

}