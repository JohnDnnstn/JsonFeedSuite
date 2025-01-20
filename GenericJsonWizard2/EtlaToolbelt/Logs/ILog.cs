using System.Collections.Generic;

// --------------------------------------------------------------------------------------------------
//	This code was generated by the ETLA Toolbelt Wizard version 1.0.59.11
//	Generated date: 2021-Mar-04 16:30:31
//
//	Changes to this file will be lost if the code is regenerated.
//	To make changes or additions, rerun the ETLA Toolbelt Wizard after editing T4 templates if required
// --------------------------------------------------------------------------------------------------

namespace GenericJsonSuite.EtlaToolbelt.Logs;

/// <summary>This is the interface for any class which sends log messages to sinks
/// Note to implementors:
///  The methods defined here must never throw an exception
/// </summary>
public interface ILog
{
    /// <summary>The list of places where the log messges will be sent
    /// Each sink may filter messages in a different way
    /// </summary>
    IList<ILogSink> Sinks { get; }

    /// <summary>Sends a log message to the sink or sinks
    /// The log message can be sent either synchronously or asynchronously
    /// </summary>
    /// <param name="logMessage">The message to send to the sinks</param>
    /// <param name="wait"><c>true</c> if the message is to be sent synchronously; <c>false</c> otherwise</param>
    void Msg(LogMessage logMessage, bool wait);
}