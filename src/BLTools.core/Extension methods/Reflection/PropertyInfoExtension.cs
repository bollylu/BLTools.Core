using BLTools.Core.Logging;

namespace BLTools.Core;

/// <summary>
/// Extensions for the PropertyInfo type
/// </summary>
public static class PropertyInfoExtensions {

  extension(PropertyInfo info) {

    /// <summary>
    /// Test if a property has specific attribute
    /// </summary>
    /// <param name="attributeType">The attribute type that we search for</param>
    /// <returns><see langword="true"/> if the attribute is present, <see langword="false"/> otherwise</returns>
    public bool HasAttribute<T>() {
      return Attribute.GetCustomAttribute(info, typeof(T)) is not null;
    }



    /// <summary>
    /// Get a value from a PropertyInfo
    /// </summary>
    /// <typeparam name="T">The type that we request</typeparam>
    /// <param name="o">The untyped value</param>
    /// <returns></returns>
    public T? GetValue<T>(object o) {
      return (T?)info.GetValue<T?>(o);
    }

    /// <summary>
    /// Indicate if the PropertyInfo has the attribute DoNotDump
    /// </summary>
    /// <returns><see langword="true"/> if no attribute is present, <see langword="false"/> otherwise</returns>
    public bool CanDump() {
      return !info.HasAttribute<DoNotDumpAttribute>();
    }
  }

  extension(IEnumerable<PropertyInfo> infos) {
    /// <summary>
    /// Get a named property info
    /// </summary>
    /// <param name="name">The name of the searched property info (case insensitive)</param>
    /// <returns></returns>
    public PropertyInfo? GetPropertyInfo(string name, StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase) {
      #region === Validate parameters ===
      if (infos is null || infos.IsEmpty()) {
        return default;
      }

      if (string.IsNullOrWhiteSpace(name)) {
        return default;
      }
      #endregion === Validate parameters ===

      return infos.FirstOrDefault(x => x.Name.Equals(name, stringComparison));
    }
  }
}
