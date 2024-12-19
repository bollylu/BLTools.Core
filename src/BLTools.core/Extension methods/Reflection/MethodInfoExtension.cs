namespace BLTools.Core;

/// <summary>
/// Extensions for the PropertyInfo type
/// </summary>
public static class MethodInfoExtensions {

  /// <summary>
  /// Test if a method has specific attribute
  /// </summary>
  /// <param name="Info">The method info to test</param>
  /// <param name="attributeType">The attribute type that we search for</param>
  /// <returns><see langword="true"/> if the attribute is present, <see langword="false"/> otherwise</returns>
  public static bool HasAttribute(this MethodInfo Info, Type attributeType) {
    return Attribute.GetCustomAttribute(Info, attributeType) is not null;
  }

  /// <summary>
  /// Get a named method info
  /// </summary>
  /// <param name="methodInfo">The source of properties info</param>
  /// <param name="name">The name of the searched property info (case insensitive)</param>
  /// <returns></returns>
  public static MethodInfo? GetMethodInfo(this IEnumerable<MethodInfo> methodInfo, string name) {
    #region === Validate parameters ===
    if (methodInfo is null) {
      return default;
    }
    if (methodInfo.IsEmpty()) {
      return default;
    }
    if (string.IsNullOrWhiteSpace(name)) {
      return default;
    }
    #endregion === Validate parameters ===

    return methodInfo.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
  }

  /// <summary>
  /// Get a value from a MethodInfo
  /// </summary>
  /// <typeparam name="T">The type that we request</typeparam>
  /// <param name="methodInfo">The source property info</param>
  /// <param name="o">The untyped value</param>
  /// <returns></returns>
  public static T? GetValue<T>(this MethodInfo methodInfo, object o) {
    return (T?)methodInfo.GetValue<T?>(o);
  }
}
