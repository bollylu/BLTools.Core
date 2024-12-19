using System.Linq.Expressions;
using System.Numerics;

namespace BLTools.Core;

/// <summary>
/// Extensions for XElement
/// </summary>
public static class XElementExtension {

  /// <summary>
  /// Provides additional debug information when true
  /// </summary>
  public static bool IsDebug { get; set; } = false;

  ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  // Attributes
  ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  ///// <summary>
  ///// Obtains the value of an typed attribute value from an XElement with a default value in case of error.
  ///// </summary>
  ///// <typeparam name="T">Type of the returned value</typeparam>
  ///// <param name="xElement">The source XElement</param>
  ///// <param name="name">The name of the attribute</param>
  ///// <returns>The attribute value in the requested type or the specified default value</returns>
  //public static T SafeReadAttribute<T>(this XElement xElement, string name) {
  //  return SafeReadAttribute<T>(xElement, name, default(T), CultureInfo.CurrentCulture);
  //}

  ///// <summary>
  ///// Obtains the value of an typed attribute value from an XElement with a default value in case of error.
  ///// </summary>
  ///// <typeparam name="T">Type of the returned value</typeparam>
  ///// <param name="xElement">The source XElement</param>
  ///// <param name="name">The name of the attribute</param>
  ///// <returns>The attribute value in the requested type or the specified default value</returns>
  //public static T SafeReadAttribute<T>(this XElement xElement, XName name) {
  //  return SafeReadAttribute<T>(xElement, name, default(T), CultureInfo.CurrentCulture);
  //}

  /// <summary>
  /// Obtains the value of an typed attribute value from an XElement with a default value in case of error.
  /// </summary>
  /// <typeparam name="T">Type of the returned value</typeparam>
  /// <param name="xElement">The source XElement</param>
  /// <param name="name">The name of the attribute</param>
  /// <param name="defaultValue">The default value (same type as the returned value)</param>
  /// <returns>The attribute value in the requested type or the specified default value</returns>
  public static T SafeReadAttribute<T>(this XElement xElement, string name, T defaultValue) {
    return SafeReadAttribute<T>(xElement, name, defaultValue, CultureInfo.CurrentCulture);
  }

  /// <summary>
  /// Obtains the value of an typed attribute value from an XElement with a default value in case of error.
  /// </summary>
  /// <typeparam name="T">Type of the returned value</typeparam>
  /// <param name="xElement">The source XElement</param>
  /// <param name="name">The name of the attribute</param>
  /// <param name="defaultValue">The default value (same type as the returned value)</param>
  /// <returns>The attribute value in the requested type or the specified default value</returns>
  public static T SafeReadAttribute<T>(this XElement xElement, XName name, T defaultValue) {
    return SafeReadAttribute<T>(xElement, name, defaultValue, CultureInfo.CurrentCulture);
  }

  /// <summary>
  /// Obtains the value of an typed attribute value from an XElement with a default value in case of error.
  /// </summary>
  /// <typeparam name="T">Type of the returned value</typeparam>
  /// <param name="xElement">The source XElement</param>
  /// <param name="name">The name of the attribute</param>
  /// <param name="defaultValue">The default value (same type as the returned value)</param>
  /// <param name="culture">The culture info used to convert the return value</param>
  /// <returns>The attribute value in the requested type or the specified default value</returns>
  public static T SafeReadAttribute<T>(this XElement xElement, string name, T defaultValue, CultureInfo culture) {
    #region Validate parameters
    if (name is null) {
      Trace.WriteLineIf(IsDebug, $"Name is null : Unable to read attribute from\r\n{xElement}");
      return defaultValue;
    }
    if (!xElement.HasAttributes) {
      Trace.WriteLineIf(IsDebug, $"No attribute available : Unable to read attribute {name} from\r\n{xElement}");
      return defaultValue;
    }
    XAttribute? NamedAttribute = xElement.Attribute(name);
    if (NamedAttribute is null) {
      Trace.WriteLineIf(IsDebug, $"Missing attribute {name} : Unable to read attribute from\r\n{xElement}");
      return defaultValue;
    }
    #endregion Validate parameters

    return NamedAttribute.Value.Parse(defaultValue, culture);

  }

  /// <summary>
  /// Obtains the value of an typed attribute value from an XElement with a default value in case of error.
  /// </summary>
  /// <typeparam name="T">Type of the returned value</typeparam>
  /// <param name="xElement">The source XElement</param>
  /// <param name="name">The name of the attribute</param>
  /// <param name="defaultValue">The default value (same type as the returned value)</param>
  /// <param name="culture">The culture info used to convert the return value</param>
  /// <returns>The attribute value in the requested type or the specified default value</returns>
  public static T SafeReadAttribute<T>(this XElement xElement, XName name, T defaultValue, CultureInfo culture) {
    #region Validate parameters
    if (name is null) {
      Trace.WriteLineIf(IsDebug, $"Name is null : Unable to read attribute from\r\n{xElement}");
      return defaultValue;
    }
    if (!xElement.HasAttributes) {
      Trace.WriteLineIf(IsDebug, $"No attribute available : Unable to read attribute {name} from\r\n{xElement}");
      return defaultValue;
    }
    #endregion Validate parameters

    XAttribute? NamedAttribute = xElement.Attribute(name);
    if (NamedAttribute is null) {
      Trace.WriteLineIf(IsDebug, $"Missing attribute {name} : Unable to read attribute from\r\n{xElement}");
      return defaultValue;
    }

    return NamedAttribute.Value.Parse(defaultValue, culture);
  }
  ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  // Element
  ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  /// <summary>
  /// Read an XElement from an XElement while handling error cases
  /// </summary>
  /// <param name="xElement">The source XElement</param>
  /// <param name="name">The name of the inner XElement to read</param>
  /// <returns>The requested inner XElement or an empty XElement named after the name parameter</returns>
  public static XElement SafeReadElement(this XElement xElement, string name) {
    #region Validate parameters
    if (string.IsNullOrWhiteSpace(name)) {
      Trace.WriteLineIf(IsDebug, $"Name is null or invalid : Unable to read element from\r\n{xElement}");
      return new XElement("");
    }
    if (!xElement.HasElements) {
      Trace.WriteLineIf(IsDebug, $"No element available : Unable to read element {name} from\r\n{xElement}");
      return new XElement(name);
    }
    #endregion Validate parameters

    try {
      XElement? SafeElement = xElement.Element(name);
      if (SafeElement is null) {
        return new XElement(name);
      }
      return SafeElement;
    } catch (Exception ex) {
      Trace.WriteLineIf(IsDebug, $"Missing element {name.WithQuotes()} : {ex.Message}\r\n{xElement}");
      return new XElement(name);
    }

  }

  /// <summary>
  /// Read an XElement from an XElement while handling error cases
  /// </summary>
  /// <param name="xElement">The source XElement</param>
  /// <param name="name">The name of the inner XElement to read</param>
  /// <returns>The requested inner XElement or an empty XElement named after the name parameter</returns>
  public static XElement SafeReadElement(this XElement xElement, XName name) {
    #region Validate parameters
    if (name is null) {
      Trace.WriteLineIf(IsDebug, $"Name is null or invalid : Unable to read element from\r\n{xElement}");
      return new XElement("");
    }
    if (!xElement.HasElements) {
      Trace.WriteLineIf(IsDebug, $"No element available : Unable to read element {name} from\r\n{xElement}");
      return new XElement(name);
    }
    #endregion Validate parameters

    try {
      XElement? SafeElement = xElement.Element(name);
      if (SafeElement is null) {
        return new XElement(name);
      }
      return SafeElement;
    } catch (Exception ex) {
      Trace.WriteLineIf(IsDebug, $"Missing element {name} : {ex.Message}\r\n{xElement}");
      return new XElement(name);
    }

  }

  ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  // Element as value
  ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  ///// <summary>
  ///// Obtains the value of an inner XElement value from an XElement with a default value in case of error.
  ///// </summary>
  ///// <typeparam name="T">Type of the returned value</typeparam>
  ///// <param name="xElement">The source XElement</param>
  ///// <param name="name">The name of the inner XElemnt</param>
  ///// <returns>The inner XElement value in the requested type or the specified default value</returns>
  //public static T SafeReadElementValue<T>(this XElement xElement, string name) {
  //  return SafeReadElementValue<T>(xElement, name, default(T), CultureInfo.CurrentCulture);
  //}

  ///// <summary>
  ///// Obtains the value of an inner XElement value from an XElement with a default value in case of error.
  ///// </summary>
  ///// <typeparam name="T">Type of the returned value</typeparam>
  ///// <param name="xElement">The source XElement</param>
  ///// <param name="name">The name of the inner XElemnt</param>
  ///// <returns>The inner XElement value in the requested type or the specified default value</returns>
  //public static T SafeReadElementValue<T>(this XElement xElement, XName name) {
  //  return SafeReadElementValue<T>(xElement, name, default(T), CultureInfo.CurrentCulture);
  //}

  /// <summary>
  /// Obtains the value of an inner XElement value from an XElement with a default value in case of error.
  /// </summary>
  /// <typeparam name="T">Type of the returned value</typeparam>
  /// <param name="xElement">The source XElement</param>
  /// <param name="name">The name of the inner XElemnt</param>
  /// <param name="defaultValue">The default value (same type as the returned value)</param>
  /// <returns>The inner XElement value in the requested type or the specified default value</returns>
  public static T SafeReadElementValue<T>(this XElement xElement, string name, T defaultValue) {
    return SafeReadElementValue<T>(xElement, name, defaultValue, CultureInfo.CurrentCulture);
  }

  /// <summary>
  /// Obtains the value of an inner XElement value from an XElement with a default value in case of error.
  /// </summary>
  /// <typeparam name="T">Type of the returned value</typeparam>
  /// <param name="xElement">The source XElement</param>
  /// <param name="name">The name of the inner XElemnt</param>
  /// <param name="defaultValue">The default value (same type as the returned value)</param>
  /// <returns>The inner XElement value in the requested type or the specified default value</returns>
  public static T SafeReadElementValue<T>(this XElement xElement, XName name, T defaultValue) {
    return SafeReadElementValue<T>(xElement, name, defaultValue, CultureInfo.CurrentCulture);
  }

  /// <summary>
  /// Obtains the value of an inner XElement value from an XElement with a default value in case of error.
  /// </summary>
  /// <typeparam name="T">Type of the returned value</typeparam>
  /// <param name="xElement">The source XElement</param>
  /// <param name="name">The name of the inner XElemnt</param>
  /// <param name="defaultValue">The default value (same type as the returned value)</param>
  /// <param name="culture">The culture info used to convert the return value</param>
  /// <returns>The inner XElement value in the requested type or the specified default value</returns>
  public static T SafeReadElementValue<T>(this XElement xElement, string name, T defaultValue, CultureInfo culture) {
    #region Validate parameters
    if (name is null) {
      Trace.WriteLineIf(IsDebug, $"Name is null : Unable to read element from\r\n{xElement}");
      return defaultValue;
    }
    if (!xElement.HasElements) {
      Trace.WriteLineIf(IsDebug, $"No element available : Unable to read element {name} from\r\n{xElement}");
      return defaultValue;
    }

    XElement? NamedElement = xElement.Element(name);
    if (NamedElement is null) {
      Trace.WriteLineIf(IsDebug, $"Missing element {name} : Unable to read element from\r\n{xElement}");
      return defaultValue;
    }
    #endregion Validate parameters

    return NamedElement.Value.Parse(defaultValue, culture);

  }

  /// <summary>
  /// Obtains the value of an inner XElement value from an XElement with a default value in case of error.
  /// </summary>
  /// <typeparam name="T">Type of the returned value</typeparam>
  /// <param name="xElement">The source XElement</param>
  /// <param name="name">The name of the inner XElemnt</param>
  /// <param name="defaultValue">The default value (same type as the returned value)</param>
  /// <param name="culture">The culture info used to convert the return value</param>
  /// <returns>The inner XElement value in the requested type or the specified default value</returns>
  public static T SafeReadElementValue<T>(this XElement xElement, XName name, T defaultValue, CultureInfo culture) {
    #region Validate parameters
    if (name is null) {
      Trace.WriteLineIf(IsDebug, $"Name is null : Unable to read element from\r\n{xElement}");
      return defaultValue;
    }
    if (!xElement.HasElements) {
      Trace.WriteLineIf(IsDebug, $"No element available : Unable to read element {name} from\r\n{xElement}");
      return defaultValue;
    }

    XElement? NamedElement = xElement.Element(name);
    if (NamedElement is null) {
      Trace.WriteLineIf(IsDebug, $"Missing element {name} : Unable to read element from\r\n{xElement}");
      return defaultValue;
    }
    #endregion Validate parameters

    return NamedElement.Value.Parse(defaultValue, culture);
  }

}
