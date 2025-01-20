::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
:: Pre-Compilation command to generate AssemblyInfo version numbering based on git info
:: Assumptions:
:: * The solution is in git
:: * TortoiseGit is installed in the default location
:: * This command file is in the Properties sub-directory of the project
:: * The project's prebuild property is:
::		$(solutionDir)/Properties/Prebuild.cmd $(SolutionDir)
:: N.B.
:: The .csproj project should contain the line 
::		<GenerateAssemblyInfo>false</GenerateAssemblyInfo>
::

SET thisDir=%~dp0
SET solutionDir=%~1
SET outfile="%thisDir%AssemblyInfo.cs"

IF NOT EXIST %solutionDir%.git GOTO ifAssumptionsWrong1

SET TGitDir=C:\Program Files\TortoiseGit\bin\
SET gitWCRev=%TGitDir%GitWCRev.exe

IF NOT EXIST "%gitWCRev%" GOTO ifAssumptionsWrong2
IF NOT EXIST "%thisDir%AssemblyInfo.gitwcrev" GOTO ifAssumptionsWrong3

SET cmdToRun="%gitWCRev%" "%thisDir%.." "%thisDir%AssemblyInfo.gitwcrev" %outfile%
ECHO %cmdToRun%
%cmdToRun%

EXIT /B

:ifAssumptionsWrong1
@ECHO Solution Git missing: %solutionDir%\.git
@GOTO ifAssumptionsWrong

:ifAssumptionsWrong2
@ECHO gitwcrev application missing: "%gitWCRev%"
@GOTO ifAssumptionsWrong

:ifAssumptionsWrong3
@ECHO the .gitwcrev file missing: "%thisDir%AssemblyInfo.gitwcrev"
@GOTO ifAssumptionsWrong

::Create an empty AssemblyInfo.cs file if we cannot do it properly for some reason
::
:ifAssumptionsWrong

ECHO Failed to generate AssemblyInfo.cs from AssemblyInfo.gitwcrev - generating default AssemblyInfo.cs

@ECHO using System.Reflection;>%outfile%
@ECHO using System.Runtime.InteropServices;>>%outfile%
@ECHO.>>%outfile%
@ECHO // General Information about an assembly is controlled through the following>>%outfile%
@ECHO // set of attributes. Change these attribute values to modify the information>>%outfile%
@ECHO // associated with an assembly.>>%outfile%
@ECHO [assembly: AssemblyTitle("")]>>%outfile%
@ECHO [assembly: AssemblyDescription("")]>>%outfile%
@ECHO [assembly: AssemblyConfiguration("")]>>%outfile%
@ECHO [assembly: AssemblyCompany("Etla Services Ltd")] >>%outfile%				// Used to define directory for user.config (in VS?) and also define common, localuser and use AppData directory>>%outfile%
@ECHO [assembly: AssemblyProduct("")] >>%outfile%							// Used to define common, localuser and use AppData directory>>%outfile%
@ECHO [assembly: AssemblyCopyright("Copyright © Etla Services Ltd 2019-2023")] >>%outfile%
@ECHO [assembly: AssemblyTrademark("")]>>%outfile%
@ECHO [assembly: AssemblyCulture("")]>>%outfile%
@ECHO.>>%outfile%
@ECHO // Setting ComVisible to false makes the types in this assembly not visible>>%outfile%
@ECHO // to COM components.  If you need to access a type in this assembly from>>%outfile%
@ECHO // COM, set the ComVisible attribute to true on that type.>>%outfile%
@ECHO [assembly: ComVisible(false)]>>%outfile%
@ECHO.>>%outfile%
@ECHO // The following GUID is for the ID of the typelib if this project is exposed to COM>>%outfile%
@ECHO [assembly: Guid("F9F800FF-53D1-45B7-B579-7C766A43972E")]>>%outfile%
@ECHO.>>%outfile%
@ECHO // Version numbers>>%outfile%
@ECHO [assembly: AssemblyVersion("0.1.0.0")]               // Used to define directory for user.config and by CLR for any referencing assemblies>>%outfile%
@ECHO [assembly: AssemblyFileVersion("0.1.0.12")]			// Displayed in windows explorer as File Version>>%outfile%
@ECHO [assembly: AssemblyInformationalVersion("0.1.0")]  // Displayed in windows explorer as Product Version>>%outfile%

@ECHO.>>%outfile%
@ECHO // Specify we will only run on Windows to save all the warnings>>%outfile%
@ECHO [assembly: System.Runtime.Versioning.SupportedOSPlatform("windows6.1")]>>%outfile%
EXIT /B