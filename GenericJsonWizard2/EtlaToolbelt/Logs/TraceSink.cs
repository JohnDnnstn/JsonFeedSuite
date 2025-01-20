using System.Diagnostics;

namespace GenericJsonSuite.EtlaToolbelt.Logs
{
    /// <summary>A Log Sink that sends a log message via the c# Trace mechanism</summary>
    public class TraceSink : AbstractLogSink
    {
        /// <summary>Default constructor</summary>
        public TraceSink()
        {
            LevelToShow = LogLevel.SHOW_INFO;
            Instance = 0;
        }

        /// <summary>Constructor</summary>
        /// <param name="levelToShow">Indicates which messages to show</param>
        /// <param name="instance">Not relevant for a Trace Sink</param>
        public TraceSink(LogLevel levelToShow, int instance)
        {
            LevelToShow = levelToShow;
            Instance = instance;
        }

        /// <summary>Writes the log message to Trace</summary>
        /// <param name="logMessage">The log message to write</param>
        public override void Write(LogMessage logMessage)
        {
            try
            {
                var message = Format(logMessage);
                Trace.WriteLine(message);
            }
            catch (Exception ex)
            {
                Log.IgnoreExceptInDev(ex);
            }
        }

    }
}