using System;
using System.Diagnostics;

namespace GenericJsonSuite.EtlaToolbelt.Logs
{
    /// <summary>Intended to be the code that sends log messages to a SQL database - Not Implelemnted Yet</summary>
    public class SqlServerSink : AbstractLogSink
    {
        /// <summary>Default constructor</summary>
        public SqlServerSink()
        {
            LevelToShow = LogLevel.SHOW_INFO;
            Instance = 0;
        }

        /// <summary>Constructor to allow overrides</summary>
        /// <param name="levelToShow">Indicates which messages will be sent ot the database</param>
        /// <param name="instance">Allows multiple database sinks to be defined</param>
        public SqlServerSink(LogLevel levelToShow, int instance)
        {
            LevelToShow = levelToShow;
            Instance = instance;
        }

        /// <summary>Writes the log message to the database - not implemented yet</summary>
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
}