namespace BLTools.Core.Diagnostic;

/// <summary>
/// Describe some application or library
/// </summary>
public interface IAbout {

  /// <summary>
  /// The name of the application or library
  /// </summary>
  string Name { get; }

  /// <summary>
  /// A description of the application or library
  /// </summary>
  string Description { get; }
  /// <summary>
  /// The version of the application or library
  /// </summary>
  Version CurrentVersion { get; }

  /// <summary>
  /// The location where to find the version
  /// </summary>
  string VersionSource { get; }

  /// <summary>
  /// The location where to find the change log
  /// </summary>
  string ChangeLogSource { get; }

  /// <summary>
  /// The changelog of the application or library
  /// </summary>
  string ChangeLog { get; }

  /// <summary>
  /// Initialize the data by reading values from the embeeded info
  /// </summary>
  void Initialize();

  /// <summary>
  /// Read the version from embeeded data, asynchronously
  /// </summary>
  /// <param name="source">The source stream</param>
  /// <returns>The version or <see langword="null"/> when wrong</returns>
  Task<Version?> ReadVersionAsync(Stream source);
  /// <summary>
  /// Read the version from embeeded data, asynchronously
  /// </summary>
  /// <param name="source">The name of the source stream</param>
  /// <returns>The version or <see langword="null"/> when wrong</returns>
  Task<Version?> ReadVersionAsync(string source);
  /// <summary>
  /// Read the changelog from embeeded data, asynchronously
  /// </summary>
  /// <param name="source">The source stream</param>
  /// <returns>The version or <see langword="null"/> when wrong</returns>
  Task<string?> ReadChangeLogAsync(Stream source);
  /// <summary>
  /// Read the changelog from embeeded data, asynchronously
  /// </summary>
  /// <param name="source">The name of the source stream</param>
  /// <returns>The version or <see langword="null"/> when wrong</returns>
  Task<string?> ReadChangeLogAsync(string source);

  /// <summary>
  /// Read the version from embeeded data
  /// </summary>
  /// <param name="source">The source stream</param>
  /// <returns>The version or <see langword="null"/> when wrong</returns>
  Version? ReadVersion(Stream source);
  /// <summary>
  /// Read the version from embeeded data
  /// </summary>
  /// <param name="source">The name of the source stream</param>
  /// <returns>The version or <see langword="null"/> when wrong</returns>
  Version? ReadVersion(string source);
  /// <summary>
  /// Read the changelog from embeeded data
  /// </summary>
  /// <param name="source">The source stream</param>
  /// <returns>The version or <see langword="null"/> when wrong</returns>
  string? ReadChangeLog(Stream source);
  /// <summary>
  /// Read the changelog from embeeded data
  /// </summary>
  /// <param name="source">The name of the source stream</param>
  /// <returns>The version or <see langword="null"/> when wrong</returns>
  string? ReadChangeLog(string source);

}
