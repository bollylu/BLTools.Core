using BLTools.Core.Logging;

namespace BLTools.Core;

/// <summary>
/// Extensions for the PropertyInfo type
/// </summary>
public static class PropertyInfoExtensions {
  extension(PropertyInfo Info) {

    /// <summary>
    /// Test if a property has specific attribute
    /// </summary>
    /// <param name="Info">The property info to test</param>
    /// <param name="attributeType">The attribute type that we search for</param>
    /// <returns><see langword="true"/> if the attribute is present, <see langword="false"/> otherwise</returns>
    public bool HasAttribute<T>() {
      return Attribute.GetCustomAttribute(Info, typeof(T)) is not null;
    }
  }
  extension(IEnumerable<PropertyInfo> propertiesInfo) {

    /// <summary>
    /// Get a named property info
    /// </summary>
    /// <param name="propertiesInfo">The source of properties info</param>
    /// <param name="name">The name of the searched property info (case insensitive)</param>
    /// <returns></returns>
    public PropertyInfo? GetPropertyInfo(string name, StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase) {
      #region === Validate parameters ===
      if (propertiesInfo is null || propertiesInfo.IsEmpty()) {
        return default;
      }

      if (string.IsNullOrWhiteSpace(name)) {
        return default;
      }
      #endregion === Validate parameters ===

      return propertiesInfo.FirstOrDefault(x => x.Name.Equals(name, stringComparison));
    }
  }
  extension(PropertyInfo propertyInfo) {

    /// <summary>
    /// Get a value from a PropertyInfo
    /// </summary>
    /// <typeparam name="T">The type that we request</typeparam>
    /// <param name="propertyInfo">The source property info</param>
    /// <param name="o">The untyped value</param>
    /// <returns></returns>
    public T? GetValue<T>(object o) {
      return (T?)propertyInfo.GetValue<T?>(o);
    }



    /// <summary>
    /// Indicate if the PropertyInfo has the attribute DoNotDump
    /// </summary>
    /// <param name="propertyInfo">The property info to evaluate</param>
    /// <returns><see langword="true"/> if no attribute is present, <see langword="false"/> otherwise</returns>
    public bool CanDump() {
      return !propertyInfo.HasAttribute<DoNotDumpAttribute>();
    }
  }
}
