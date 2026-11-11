using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BLTools.Core.Logging;

namespace BLTools.Extension_methods.Reflection;
public static class FieldInfoExtension {
  extension(FieldInfo field) {

    /// <summary>
    /// Test if a field has specific attribute
    /// </summary>
    /// <param name="field">The property info to test</param>
    /// <param name="attributeType">The attribute type that we search for</param>
    /// <returns><see langword="true"/> if the attribute is present, <see langword="false"/> otherwise</returns>
    public bool HasAttribute<T>() {
      return Attribute.GetCustomAttribute(field, typeof(T)) is not null;
    }
  }
  extension(FieldInfo fieldInfo) {

    /// <summary>
    /// Check if a field can be dumped
    /// </summary>
    /// <param name="fieldInfo">The field info to check</param>
    /// <returns><see langword="true"/> if the field can be dumped, <see langword="false"/> otherwise</returns>
    public bool CanDump() {
      return !fieldInfo.HasAttribute<DoNotDumpAttribute>();
    }
  }

}
