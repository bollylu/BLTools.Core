using BLTools.Core.Logging;

namespace BLTools.Core.Diagnostic;

/// <summary>
/// Helper for obtaining runtime infos
/// </summary>
public static partial class ApplicationInfo {

  /// <summary>
  /// Generate a banner with startup information and send it to Logger
  /// </summary>
  public static void ApplicationStart(ILogger logger, bool fullDetails = false) {

    if (logger is null) {
      Trace.IndentSize = 2;
      Trace.AutoFlush = true;
      Trace.WriteLine(BuildStartupInfo(fullDetails).Box("Startup info"));
      Trace.WriteLine(BuildRuntimeInfo().Box("Runtime info"));
    } else {
      logger.LogBox("Startup info", BuildStartupInfo(fullDetails));
      logger.LogBox("Runtime info", BuildRuntimeInfo());
    }
  }

  /// <summary>
  /// Log a message indicating the application if completed
  /// </summary>
  /// <param name="logger">The logger for the message</param>
  /// <param name="message">An optional message</param>
  public static void ApplicationStop(ILogger logger, string message = "") {
    AssemblyName? StartupApplication = null;
    StringBuilder Text = new StringBuilder();

    try {
      StartupApplication = Assembly.GetEntryAssembly()?.GetName();
    } catch (Exception ex) {
      Text.AppendLine($"Unable to gather application information : {ex.Message}");
    }

    Text.AppendLine($"{StartupApplication?.Name} is stopping");
    if (!string.IsNullOrWhiteSpace(message)) {
      Text.AppendLine(message);
    }
    string Message = Text.ToString().TrimEnd(Environment.NewLine.ToCharArray());

    if (logger is null) {
      Trace.WriteLine(Message.BoxFixedWidth(80));
      Trace.Flush();
    } else {
      logger.LogBox("Application stop", Message);
    }

  }

  /// <summary>
  /// Send a banner with runtime info to Trace
  /// </summary>
  [Obsolete]
  public static void TraceRuntimeInfo() {
    try {
      Trace.WriteLine(BuildRuntimeInfo());
    } catch { }
  }

  /// <summary>
  /// List of prefixes to ignore when asking for list of referenced assemblies
  /// </summary>
  public static readonly List<string> ExcludedAssemblies = [
    "System",
    "Microsoft",
    "netstandard",
    "Newtonsoft.json",
    "Nuget"
  ];

  /// <summary>
  /// Builds a string with startup information
  /// </summary>
  /// <returns>Startup info</returns>
  public static string BuildStartupInfo(bool fullDetails = false) {
    StringBuilder Text = new StringBuilder();
    try {

      Assembly? EntryAssembly = Assembly.GetEntryAssembly();
      if (EntryAssembly is null) {
        return "";
      }
      AssemblyName? StartupApplication = EntryAssembly.GetName();
      if (StartupApplication is null) {
        return "";
      }
      Text.AppendLine($"Startup of {StartupApplication.Name}, {StartupApplication.Version?.ToString() ?? "0.0.0.0"}");
      Text.AppendLine($".NET Runtime version: {EntryAssembly.ImageRuntimeVersion}");
      Text.AppendLine("Assemblies:");

      AppDomain Domain = AppDomain.CurrentDomain;

      IEnumerable<Assembly> Assemblies;

      if (fullDetails) {
        Assemblies = Domain.GetAssemblies()
                           .OrderBy(a => a.GetName().Name);
      } else {
        Assemblies = Domain.GetAssemblies()
                           .Where(a => !ExcludedAssemblies.Any(x => a.GetName().Name?.StartsWith(x, StringComparison.InvariantCultureIgnoreCase) ?? false))
                           .OrderBy(a => a.GetName().Name);
      }

      foreach (Assembly AssemblyItem in Assemblies) {
        Text.AppendLine($"+ {AssemblyItem.GetName().Name} - {AssemblyItem.GetName().Version}");
        string AssembliesList = GetReferencedAssembliesList(AssemblyItem);
        if (AssembliesList.Trim().Any()) {
          Text.AppendLine(AssembliesList);
        }
      }
    } catch (Exception ex) {
      Text.AppendLine($"Unable to gather startup information : {ex.Message}");
    }
    return Text.ToString().TrimEnd(Environment.NewLine.ToCharArray());
  }

  /// <summary>
  /// Get a list of referenced assemblies, filtered or not
  /// </summary>
  /// <param name="assembly">The parent assembly</param>
  /// <param name="fullDetails">true to obtain full list, false for a filtered list (default is false)</param>
  /// <returns></returns>
  public static string GetReferencedAssembliesList(Assembly assembly, bool fullDetails = false) {
    StringBuilder RetVal = new StringBuilder();

    try {

      IEnumerable<AssemblyName> AssemblyNames;

      if (fullDetails) {
        AssemblyNames = assembly.GetReferencedAssemblies()
                                .OrderBy(a => a.Name);
      } else {
        AssemblyNames = assembly.GetReferencedAssemblies()
                                .Where(a => !ExcludedAssemblies.Any(x => a.Name?.StartsWith(x, StringComparison.InvariantCultureIgnoreCase) ?? false))
                                .OrderBy(a => a.Name);
      }

      foreach (AssemblyName AssemblyItem in AssemblyNames) {
        RetVal.AppendLine($"  - {AssemblyItem.Name} - {AssemblyItem.Version}");
      }
    } catch (Exception ex) {
      RetVal.AppendLine($"Unable to gather startup information : {ex.Message}");
    }
    return RetVal.ToString().TrimEnd();
  }

  /// <summary>
  /// Builds a banner with runtime infos
  /// </summary>
  /// <returns></returns>
  public static string BuildRuntimeInfo() {
    StringBuilder RetVal = new StringBuilder();
    RetVal.AppendLine($"Working folder = {Environment.CurrentDirectory.WithQuotes()}");
    RetVal.AppendLine($"Operating system = {Environment.OSVersion.VersionString}");
    RetVal.AppendLine($"Current user = {Environment.UserName.WithQuotes()}");
    RetVal.AppendLine($"Current domain user = {Environment.UserDomainName.WithQuotes()}");
    RetVal.AppendLine($"Is 64 bits OS = {Environment.Is64BitOperatingSystem}");
    RetVal.AppendLine($"Is 64 bits process = {Environment.Is64BitProcess}");
    RetVal.AppendLine($"Processor count = {Environment.ProcessorCount}");
    RetVal.AppendLine($"Process id = {Environment.ProcessId}");
    RetVal.AppendLine($"Command line = {Environment.CommandLine.WithQuotes()}");

    return RetVal.ToString().TrimEnd(Environment.NewLine.ToCharArray());

  }

}
