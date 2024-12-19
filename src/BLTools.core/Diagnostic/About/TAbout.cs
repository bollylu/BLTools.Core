using BLTools.Core.Logging;

namespace BLTools.Core.Diagnostic;

/// <inheritdoc/>
public class TAbout : IAbout {

  private readonly Assembly? _Assembly;

  /// <summary>
  /// The logger
  /// </summary>
  public ILogger Logger { get; protected set; } = new TConsoleLogger();

  /// <inheritdoc/>
  public string Name {
    get {
      return string.IsNullOrWhiteSpace(_Name) ? _Name = _Assembly?.GetName().Name ?? "" : _Name;
    }
    set {
      _Name = value;
    }
  }
  private string? _Name;

  /// <inheritdoc/>
  public string Description {
    get {
      return string.IsNullOrWhiteSpace(_Description) ? _Description = _Assembly?.GetName().FullName ?? "" : _Description;
    }
    set {
      _Description = value;
    }
  }
  private string? _Description;

  /// <inheritdoc/>
  public string VersionSource { get; init; } = "_global_.version.txt";
  /// <inheritdoc/>
  public Version CurrentVersion {
    get {
      return _CurrentVersion ??= new Version(0, 0, 0);
    }
    set {
      _CurrentVersion = value;
    }
  }
  private Version? _CurrentVersion;
  /// <inheritdoc/>
  public string ChangeLogSource { get; init; } = "_global_.changelog.txt";
  /// <inheritdoc/>
  public string ChangeLog {
    get {
      return _ChangeLog ??= "";
    }
    set {
      _ChangeLog = value;
    }
  }
  private string? _ChangeLog;

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  /// <summary>
  /// A new TAbout
  /// </summary>
  public TAbout() { }

  /// <summary>
  /// A new TAbout for a specific assembly
  /// </summary>
  /// <param name="assembly">The assembly where the TAbout belongs</param>
  public TAbout(Assembly? assembly) {
    _Assembly = assembly;
  }

  private bool _IsInitialized = false;
  private bool _IsInitializing = false;

  /// <summary>
  /// Initialize the data content by reading values
  /// </summary>
  public void Initialize() {
    if (_IsInitialized) {
      return;
    }
    if (_IsInitializing) {
      return;
    }
    _IsInitializing = true;

    if (VersionSource is not null) {
      ReadVersion(VersionSource);
    }

    if (ChangeLogSource is not null) {
      ReadChangeLog(ChangeLogSource);
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
  public async Task<Version?> ReadVersionAsync(Stream source) {
    #region === Validate parameters ===
    if (source is null) {
      Logger?.LogError("Unable to read version : source is null");
      return null;
    }
    #endregion === Validate parameters ===

    try {
      using (TextReader Reader = new StreamReader(stream: source, leaveOpen: true)) {
        CurrentVersion = Version.Parse(await Reader.ReadToEndAsync());
      }
    } catch (Exception ex) {
      Logger?.LogError($"Unable to read version : {ex.Message}");
      CurrentVersion = new Version(0, 0, 0);
    }
    return CurrentVersion;

  }
  /// <inheritdoc/>
  public async Task<Version?> ReadVersionAsync(string source) {
    #region === Validate parameters ===
    if (_Assembly is null) {
      throw new InvalidOperationException("Unable to read version : assembly is null");
    }

    if (source is null) {
      Logger?.LogError("Unable to read version : source is null");
      return null;
    }
    #endregion === Validate parameters ===

    try {
      string? ResourceName = _GetResourceNameCaseInsensitive(_Assembly, source);
      if (ResourceName is null) {
        return CurrentVersion;
      }
      using (Stream? VersionStream = _Assembly.GetManifestResourceStream(ResourceName)) {
        if (VersionStream is null) {
          return CurrentVersion;
        }
        using (TextReader Reader = new StreamReader(VersionStream)) {
          CurrentVersion = Version.Parse(await Reader.ReadToEndAsync());
        }
        return CurrentVersion;
      }
    } catch (Exception ex) {
      Logger?.LogError($"Unable to read version : {ex.Message}");
      CurrentVersion = new Version(0, 0);
      return CurrentVersion;
    }

  }
  /// <inheritdoc/>
  public async Task<string?> ReadChangeLogAsync(Stream source) {
    #region === Validate parameters ===
    if (source is null) {
      Logger?.LogError("Unable to read change log : source is null");
      return null;
    }
    #endregion === Validate parameters ===

    using (TextReader Reader = new StreamReader(stream: source, leaveOpen: true)) {
      ChangeLog = await Reader.ReadToEndAsync();
    }
    return ChangeLog;
  }
  /// <inheritdoc/>
  public async Task<string?> ReadChangeLogAsync(string source) {
    #region === Validate parameters ===
    if (_Assembly is null) {
      throw new InvalidOperationException("Unable to read changelog : assembly is null");
    }

    if (string.IsNullOrWhiteSpace(source)) {
      Logger?.LogError("Unable to read change log : source is null or invalid");
      return null;
    }
    #endregion === Validate parameters ===

    try {
      string? ResourceName = _GetResourceNameCaseInsensitive(_Assembly, source);
      if (ResourceName is null) {
        return null;
      }
      using (Stream? ChangeLogStream = _Assembly.GetManifestResourceStream(ResourceName)) {
        if (ChangeLogStream is null) {
          return null;
        }
        using (TextReader Reader = new StreamReader(ChangeLogStream)) {
          ChangeLog = await Reader.ReadToEndAsync();
          return ChangeLog;
        }
      }
    } catch (Exception ex) {
      Logger?.LogError($"Unable to read changelog : {ex.Message}");
      ChangeLog = "";
      return null;
    }
  }

  private static string? _GetResourceNameCaseInsensitive(Assembly assembly, string resourceName) {
    if (assembly is null) {
      throw new InvalidOperationException("Unable to search for resource : assembly is null");
    }
    if (string.IsNullOrWhiteSpace(resourceName)) {
      return null;
    }
    string FullResourceName = $"{assembly.GetName().Name}.{resourceName}".ToLowerInvariant();
    string? RetVal = assembly.GetManifestResourceNames().FirstOrDefault(x => x.ToLowerInvariant() == FullResourceName);
    return RetVal;
  }
  #endregion --- I/O async --------------------------------------------

  #region --- I/O --------------------------------------------
  /// <inheritdoc/>
  public Version? ReadVersion(Stream source) {
    #region === Validate parameters ===
    if (source is null) {
      Logger?.LogError("Unable to read version : source is null");
      return null;
    }
    #endregion === Validate parameters ===
    using (TextReader Reader = new StreamReader(stream: source, leaveOpen: true)) {
      try {
        CurrentVersion = Version.Parse(Reader.ReadToEnd());
      } catch (Exception ex) {
        Logger?.LogError($"Unable to read version : {ex.Message}");
        CurrentVersion = new Version(0, 0, 0);
      }
    }

    return CurrentVersion;
  }
  /// <inheritdoc/>
  public Version? ReadVersion(string source) {
    #region === Validate parameters ===
    if (_Assembly is null) {
      throw new InvalidOperationException("Unable to read version : assembly is null");
    }

    if (string.IsNullOrWhiteSpace(source)) {
      Logger?.LogError("Unable to read version : source is null or invalid");
      return null;
    }
    #endregion === Validate parameters ===

    try {
      string? ResourceName = _GetResourceNameCaseInsensitive(_Assembly, source);
      if (ResourceName is null) {
        return CurrentVersion;
      }
      using (Stream? VersionStream = _Assembly.GetManifestResourceStream(ResourceName)) {
        if (VersionStream is null) {
          return CurrentVersion;
        }
        using (TextReader Reader = new StreamReader(VersionStream)) {
          CurrentVersion = Version.Parse(Reader.ReadToEnd());
        }
        return CurrentVersion;
      }
    } catch (Exception ex) {
      Logger?.LogError($"Unable to read version : {ex.Message}");
      CurrentVersion = new Version(0, 0);
      return CurrentVersion;
    }

  }
  /// <inheritdoc/>
  public string? ReadChangeLog(Stream source) {
    #region === Validate parameters ===
    if (source is null) {
      Logger?.LogError("Unable to read change log : source is null");
      return null;
    }
    #endregion === Validate parameters ===

    using (TextReader Reader = new StreamReader(stream: source, leaveOpen: true)) {
      ChangeLog = Reader.ReadToEnd();
    }
    return ChangeLog;
  }
  /// <inheritdoc/>
  public string? ReadChangeLog(string source) {
    #region === Validate parameters ===
    if (_Assembly is null) {
      throw new InvalidOperationException("Unable to read changelog : assembly is null");
    }

    if (string.IsNullOrWhiteSpace(source)) {
      Logger?.LogError("Unable to read change log : source is null or invalid");
      return null;
    }
    #endregion === Validate parameters ===

    try {
      string? ResourceName = _GetResourceNameCaseInsensitive(_Assembly, source);
      if (ResourceName is null) {
        return null;
      }
      using (Stream? ChangeLogStream = _Assembly.GetManifestResourceStream(ResourceName)) {
        if (ChangeLogStream is null) {
          return null;
        }
        using (TextReader Reader = new StreamReader(ChangeLogStream)) {
          ChangeLog = Reader.ReadToEnd();
          return ChangeLog;
        }
      }
    } catch (Exception ex) {
      Logger?.LogError($"Unable to read changelog : {ex.Message}");
      ChangeLog = "";
      return null;
    }
  }

  #endregion --- I/O --------------------------------------------

  #region --- Static instances --------------------------------------------
  /// <summary>
  /// An empty TAbout
  /// </summary>
  public static TAbout Empty => _Empty ??= new TAbout();
  private static TAbout? _Empty;

  /// <summary>
  /// The TAbout for the entry assembly
  /// </summary>
  public static TAbout Entry => _Entry ??= new TAbout(Assembly.GetEntryAssembly());
  private static TAbout? _Entry;

  /// <summary>
  /// The TAbout for the calling assembly
  /// </summary>
  public static TAbout Calling => _Calling ??= new TAbout(Assembly.GetCallingAssembly());
  private static TAbout? _Calling;
  #endregion --- Static instances --------------------------------------------
}
