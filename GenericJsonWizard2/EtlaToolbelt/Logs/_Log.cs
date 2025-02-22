// --------------------------------------------------------------------------------------------------
//	This code was generated by the ETLA Toolbelt Wizard version 1.0.59.11
//	Generated date: 2021-Mar-04 16:30:31
//
//	Changes to this file will be lost if the code is regenerated.
//	To make changes or additions, rerun the ETLA Toolbelt Wizard after editing T4 templates if required
// --------------------------------------------------------------------------------------------------

namespace GenericJsonSuite.EtlaToolbelt.Logs;

/// <summary>This is the default implementation of the logging interface
/// </summary>
public class _Log : ILog
{
    private readonly object _Lock = new();

    /// <summary>Indicates whether a start message has been sent already
    /// This avoids duplicate "Started" messages being sent
    /// </summary>
    protected bool Started; // false

    /// <summary>The list of places where the log messges will be sent
    /// Each sink may filter messages in a different way
    /// </summary>
    public IList<ILogSink> Sinks { get; }

    /// <summary>Constructor
    /// * Defines where log messages will go (the "sinks").  
    /// * Outputs a log saying the application has started
    /// * Sets up an event handler to catch the application exit so that an exit log can be created
    /// </summary>
    public _Log()
    {
        Sinks = new List<ILogSink>{
            new FileSink(LogLevel.SHOW_INFO,0),

        };

        // Log information about the process starting - I normally include information about which application, in which environment, under which user name...
        var startMessage = new LogMessage { Level = LogLevel.START };
        Msg(startMessage, true);
        // When the process exits, log that information
        AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProcessExitEventHandler);
    }

    /// <summary>Sends a log message to the sink or sinks
    /// The log message can be sent either synchronously or asynchronously
    /// </summary>
    /// <param name="logMessage">The message to send to the sinks</param>
    /// <param name="wait"><c>true</c> if the message is to be sent synchronously; <c>false</c> otherwise</param>
    public void Msg(LogMessage logMessage, bool wait)
    {
        // We only want one start message
        if (logMessage.Level == LogLevel.START)
        {
            if (Started) { return; } else { Started = true; wait = true; }
        }

        // Normal messages...
        if (wait)
        {
            SendToSinks(logMessage);
        }
        else
        {
            FireAndForget(logMessage);
        }
    }

    /// <summary>Sends a log message to the sink or synks asynchronously</summary>
    /// <param name="logMessage">The log message to be sent</param>
    private void FireAndForget(LogMessage logMessage)
    {
        Task.Run(() => SendToSinks(logMessage));
    }


    /// <summary>Sends a log message to the sink or sinks/// </summary>
    /// <param name="logMessage">The messsage to be sent</param>
    private void SendToSinks(LogMessage logMessage)
    {
        foreach (var logSink in Sinks)
        {
            try
            {
                if (logSink.Shows(logMessage.Level))
                {
                    lock (_Lock)
                    {
                        logSink.Write(logMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.IgnoreExceptInDev(ex);
            }
        }
    }

    /// <summary>Defines the action to do when the application exits
    /// In this case it sends a synchronous "Exiting Process" message to the sink or sinks
    /// This means the application should wait until the message has been sent to the sinks before actually exiting
    /// </summary>
    /// <param name="sender">Not used</param>
    /// <param name="e">Not used</param>
    private void ProcessExitEventHandler(object? sender, EventArgs e)
    {
        var logMessage = new LogMessage { Level = LogLevel.FINISH, Msg = "Exiting Process" };
        Msg(logMessage, true);
    }
}