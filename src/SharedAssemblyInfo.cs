using System.Reflection;

// General Information about an assembly is controlled through the following set of attributes. Change these attribute
// values to modify the information associated with an assembly.
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Mono Ltd.")]
[assembly: AssemblyProduct("Baasic .NET SDK")]
[assembly: AssemblyCopyright("Copyright © 2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Version information for an assembly consists of the following four values:
//
// Major Version Minor Version Build Number Revision
//
// You can specify all the values or you can default the Build and Revision Numbers by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(AssemblyInfo.Version)]
[assembly: AssemblyFileVersion(AssemblyInfo.Version)]
#if DEBUG
[assembly: AssemblyInformationalVersion(AssemblyInfo.Version + "-alpha")]
#endif

/// <summary>
/// Assembly info.
/// </summary>
static internal class AssemblyInfo
{
    #region Fields

    /// <summary>
    /// The version
    /// </summary>
    internal const string Version = "0.8.160";

    #endregion Fields
}