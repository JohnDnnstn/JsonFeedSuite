// --------------------------------------------------------------------------------------------------
//	This code was generated by the ETLA Toolbelt Wizard version 1.0.59.11
//	Generated date: 2021-Mar-04 16:30:31
//
//	Changes to this file will be lost if the code is regenerated.
//	To make changes or additions, rerun the ETLA Toolbelt Wizard after editing T4 templates if required
// --------------------------------------------------------------------------------------------------

namespace GenericJsonSuite.EtlaToolbelt.Contexts;

/// <summary>This is the default implementation of the EtlaToolbelt Context API
/// Env - Implemented by looking at the environmental variable "NETCORE_ENVIRONMENT" with an "UNKNOWN" fallback
/// App - Implemented by taking the last element of 0th Arg of the Command line
/// UserName - Implememnted with Environment.UserName
/// UserDomain - Implemeted with Environment.UserDomainName
/// IsDev - Compares Env to "DEV", ignoring case
/// IsProduction - Compares Env to"PRODUCTION", ignoring case.
/// </summary>
public class _Context : IContext
{
    private string? _EnvValue { get; set; }
    private const string _EnvVariableName = "NETCORE_ENVIRONMENT";

    /// <summary>The constructor
    /// Attempts to work out the environment the application is running in and the application name
    /// </summary>
    public _Context()
    {
        Env = GetEnv();

        var appPath = Environment.CommandLine.Split(' ')[0].Replace("\"", "");
        App = Path.GetFileNameWithoutExtension(appPath);
    }

    /// <summary>The name of the environment the application is running in (e.g. "DEV")</summary>
    public string Env { get; set; }

    /// <summary>The name of the application</summary>
    public string App { get; set; }

    /// <summary>The name of the account running the application</summary>
    public string UserName => Environment.UserName;

    /// <summary>The "domain" the application is running in (usually company name or similar)</summary>
    public string UserDomain => Environment.UserDomainName;

    /// <summary>Reports on whether the environment the application is running in is a "development" environment (e.g. on a programmers machine)</summary>
    /// <returns><c>true</c> if the environment appears to be a development environment; <c>false</c> otherwise</returns>
    public virtual bool IsDev()
    {
        return "DEV".Equals(_EnvValue, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>Reports on whether the environment the application is running in is a "production" environment (i.e. not development or test, but live)</summary>
    /// <returns><c>true</c> if the environment appears to be a production environment; <c>false</c> otherwise</returns>
    public virtual bool IsProduction()
    {
        return "PRODUCTION".Equals(_EnvValue, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>Returns the best guess as to the name of the environment the application is running in
    /// This implementation looks at a likely environmental variable
    /// </summary>
    /// <returns>The name of the environment the application is running in or UNKNOWN if this cannot be ascertained</returns>
	protected virtual string GetEnv()
    {
        var _EnvValue = Environment.GetEnvironmentVariable(_EnvVariableName);
        if (string.IsNullOrWhiteSpace(_EnvValue)) { _EnvValue = "UNKNOWN"; }
        return _EnvValue;
    }
}