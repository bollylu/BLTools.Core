using BLTools.Core.Logging;

namespace BLTools.Core.Diagnostic;

/// <inheritdoc/>
public class TAbout : IAbout {

  private readonly Assembly _Assembly;

  #region --- Public properties ------------------------------------------------------------------------------
  /// <summary>
  /// The logger, default to a <see cref="TTraceLogger"/> if not set
  /// </summary>
  public ILogger Logger { get; init; } = new TTraceLogger();

  /// <inheritdoc/>
  public string Name { get => field ??= _Assembly?.GetName().Name ?? ""; set; }

  /// <inheritdoc/>
  public string Description { get => field ??= _Assembly?.GetName().FullName ?? string.Empty; set; }

  /// <inheritdoc/>
  public string VersionSource { get; init; } = "_global_.version.txt";

  /// <inheritdoc/>
  public Version CurrentVersion { get => field ??= new Version(0, 0, 0); set; }

  /// <inheritdoc/>
  public string ChangeLogSource { get; init; } = "_global_.changelog.md";
  /// <inheritdoc/>
  public string ChangeLog { get; set; } = string.Empty;
  #endregion --- Public properties ---------------------------------------------------------------------------

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  /// <summary>
  /// A new TAbout
  /// </summary>
  public TAbout() {
    _Assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
  }

  /// <summary>
  /// A new TAbout for a specific assembly
  /// </summary>
  /// <param name="assembly">The assembly where the TAbout belongs</param>
  public TAbout(Assembly assembly) {
    _Assembly = assembly;
  }

  private bool _IsInitialized = false;
  private bool _IsInitializing = false;

  /// <summary>
  /// Initialize the data content by reading values
  /// </summary>
  public void Initialize() {
    if (_IsInitialized || _IsInitializing) {
      return;
    }

    _IsInitializing = true;

    if (VersionSource is not null) {
      CurrentVersion = ReadVersion(VersionSource);
    }

    if (ChangeLogSource is not null) {
      ChangeLog = ReadChangeLog(ChangeLogSource);
    }

    _IsInitializing = false;
    _IsInitialized = true;
  }

  /// <summary>
  /// Initialize the data content by reading values asynchronously
  /// </summary>
  public async Task InitializeAsync() {
    if (_IsInitialized) {
      return;
    }
    if (_IsInitializing) {
      return;
    }
    _IsInitializing = true;
    if (VersionSource is not null) {
      CurrentVersion = await ReadVersionAsync(VersionSource);
    }
    if (ChangeLogSource is not null) {
      ChangeLog = await ReadChangeLogAsync(ChangeLogSource);
    }
    _IsInitializing = false;
    _IsInitialized = true;
  }

  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  #region --- Converters -------------------------------------------------------------------------------------
  /// <summary>
  /// Display the class as a string
  /// </summary>
  /// <returns>The string description</returns>
  public override string ToString() {
    StringBuilder RetVal = new();
    RetVal.AppendLine($"{nameof(Name)} : {Name}");
    RetVal.AppendLine($"{nameof(Description)} : {Description}");
    RetVal.AppendLine($"{nameof(CurrentVersion)} : {CurrentVersion}");
    RetVal.AppendLine($"{nameof(ChangeLog)} : {ChangeLog}");
    return RetVal.ToString();
  }
  #endregion --- Converters -------------------------------------------------------------------------------------

  #region --- I/O async --------------------------------------------
  /// <inheritdoc/>
  public async Task<Version> ReadVersionAsync(Stream source) {
    try {
      #region === Validate parameters ===
      if (source is null) {
        throw new ArgumentNullException(nameof(source), "source is null");
      }
      #endregion === Validate parameters ===

      using (TextReader Reader = new StreamReader(stream: source, leaveOpen: true)) {
        return Version.Parse(await Reader.ReadToEndAsync());
      }

    } catch (Exception ex) {
      Logger.LogErrorBox("Unable to read version", ex);
      return new Version(0, 0);
    }
  }

  /// <inheritdoc/>
  public async Task<Version> ReadVersionAsync(string source) {
    try {

      #region === Validate parameters ===
      if (source is null) {
        throw new ArgumentNullException(nameof(source), "source is null");
      }
      #endregion === Validate parameters ===

      string? ResourceName = _GetResourceNameCaseInsensitive(_Assembly, source) ?? throw new InvalidOperationException($"Resource {source.WithQuotes()} not found in assembly {_Assembly.GetName().Name.OrNull().WithQuotes()}");
      using (Stream? VersionStream = _Assembly.GetManifestResourceStream(ResourceName)) {
        if (VersionStream is null) {
          throw new InvalidOperationException($"Resource {source.WithQuotes()} not found in assembly {_Assembly.GetName().Name.OrNull().WithQuotes()}");
        }
        using (TextReader Reader = new StreamReader(VersionStream)) {
          return Version.Parse(await Reader.ReadToEndAsync());
        }
      }
    } catch (Exception ex) {
      Logger.LogError($"Unable to read version : {ex.Message}");
      return new Version(0, 0);
    }

  }

  /// <inheritdoc/>
  public async Task<string> ReadChangeLogAsync(Stream source) {
    try {
      #region === Validate parameters ===
      if (source is null) {
        throw new ArgumentNullException(nameof(source), "source is null");
      }
      #endregion === Validate parameters ===

      using (TextReader Reader = new StreamReader(stream: source, leaveOpen: true)) {
        return await Reader.ReadToEndAsync();
      }
    } catch (Exception ex) {
      Logger.LogError($"Unable to read changelog : {ex.Message}");
      return string.Empty;
    }
  }
  /// <inheritdoc/>
  public async Task<string> ReadChangeLogAsync(string source) {
    try {
      #region === Validate parameters ===
      if (string.IsNullOrWhiteSpace(source)) {
        throw new ArgumentNullException(nameof(source), $"source {source.OrNull().WithQuotes()} is invalid");
      }
      #endregion === Validate parameters ===

      string? ResourceName = _GetResourceNameCaseInsensitive(_Assembly, source) ?? throw new InvalidOperationException($"Resource {source.WithQuotes()} not found in assembly {_Assembly.GetName().Name.OrNull().WithQuotes()}");
      using (Stream? ChangeLogStream = _Assembly.GetManifestResourceStream(ResourceName)) {
        if (ChangeLogStream is null) {
          throw new InvalidOperationException($"Resource {source.WithQuotes()} not found in assembly {_Assembly.GetName().Name.OrNull().WithQuotes()}");
        }
        using (TextReader Reader = new StreamReader(ChangeLogStream)) {
          return await Reader.ReadToEndAsync();
        }
      }
    } catch (Exception ex) {
      Logger.LogError($"Unable to read changelog : {ex.Message}");
      return string.Empty;
    }
  }

  private static string? _GetResourceNameCaseInsensitive(Assembly assembly, string resourceName) {
    if (string.IsNullOrWhiteSpace(resourceName)) {
      return null;
    }
    string FullResourceName = $"{assembly.GetName().Name}.{resourceName}";
    string? RetVal = assembly.GetManifestResourceNames().FirstOrDefault(x => x.Equals(FullResourceName, StringComparison.OrdinalIgnoreCase));
    return RetVal;
  }
  #endregion --- I/O async --------------------------------------------

  #region --- I/O --------------------------------------------
  /// <inheritdoc/>
  public Version ReadVersion(Stream source) {
    try {
      #region === Validate parameters ===
      if (source is null) {
        throw new ArgumentNullException(nameof(source), "source is null");
      }
      #endregion === Validate parameters ===

      using (TextReader Reader = new StreamReader(stream: source, leaveOpen: true)) {
        return Version.Parse(Reader.ReadToEnd());
      }
    } catch (Exception ex) {
      Logger.LogErrorBox("Unable to read version", ex);
      return new Version(0, 0);
    }
  }

  /// <inheritdoc/>
  public Version ReadVersion(string source) {
    #region === Validate parameters ===
    if (string.IsNullOrWhiteSpace(source)) {
      throw new ArgumentNullException(nameof(source), $"source {source.OrNull().WithQuotes()} is invalid");
    }
    #endregion === Validate parameters ===

    try {
      string? ResourceName = _GetResourceNameCaseInsensitive(_Assembly, source) ?? throw new InvalidOperationException($"Resource {source.WithQuotes()} not found in assembly {_Assembly.GetName().Name.OrNull().WithQuotes()}");
      using (Stream? VersionStream = _Assembly.GetManifestResourceStream(ResourceName)) {
        if (VersionStream is null) {
          throw new InvalidOperationException($"Resource {source.WithQuotes()} not found in assembly {_Assembly.GetName().Name.OrNull().WithQuotes()}");
        }
        using (TextReader Reader = new StreamReader(VersionStream)) {
          return Version.Parse(Reader.ReadToEnd());
        }
      }
    } catch (Exception ex) {
      Logger.LogErrorBox("Unable to read version", ex);
      return new Version(0, 0);
    }

  }

  /// <inheritdoc/>
  public string ReadChangeLog(Stream source) {
    try {
      #region === Validate parameters ===
      if (source is null) {
        throw new ArgumentNullException(nameof(source), "source is null");
      }
      #endregion === Validate parameters ===

      using (TextReader Reader = new StreamReader(stream: source, leaveOpen: true)) {
        return Reader.ReadToEnd();
      }
    } catch (Exception ex) {
      Logger.LogErrorBox("Unable to read changelog", ex);
      ChangeLog = "";
      return string.Empty;
    }
  }
  /// <inheritdoc/>
  public string ReadChangeLog(string source) {
    try {
      #region === Validate parameters ===
      if (string.IsNullOrWhiteSpace(source)) {
        throw new ArgumentNullException(nameof(source), $"source {source.OrNull().WithQuotes()} is invalid");
      }
      #endregion === Validate parameters ===

      string? ResourceName = _GetResourceNameCaseInsensitive(_Assembly, source) ?? throw new InvalidOperationException($"Resource {source.WithQuotes()} not found in assembly {_Assembly.GetName().Name.OrNull().WithQuotes()}");
      using (Stream? ChangeLogStream = _Assembly.GetManifestResourceStream(ResourceName)) {
        if (ChangeLogStream is null) {
          throw new InvalidOperationException($"Resource {source.WithQuotes()} not found in assembly {_Assembly.GetName().Name.OrNull().WithQuotes()}");
        }
        using (TextReader Reader = new StreamReader(ChangeLogStream)) {
          return Reader.ReadToEnd();
        }
      }
    } catch (Exception ex) {
      Logger.LogErrorBox("Unable to read changelog", ex);
      return string.Empty;
    }
  }
  #endregion --- I/O --------------------------------------------

  #region --- Static instances --------------------------------------------
  /// <summary>
  /// An empty TAbout
  /// </summary>
  public static TAbout Empty => field ??= new TAbout();

  /// <summary>
  /// The TAbout for the entry assembly
  /// </summary>
  public static TAbout Entry => field ??= new TAbout(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly());

  /// <summary>
  /// The TAbout for the entry assembly
  /// </summary>
  public static TAbout Executing => field ??= new TAbout(Assembly.GetExecutingAssembly());

  /// <summary>
  /// The TAbout for the calling assembly
  /// </summary>
  public static TAbout Calling => field ??= new TAbout(Assembly.GetCallingAssembly());
  #endregion --- Static instances --------------------------------------------
}
