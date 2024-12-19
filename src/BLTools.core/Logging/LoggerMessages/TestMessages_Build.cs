using System.Collections;

using BLTools.Core;

namespace BLTools.Core.Diagnostic;

public partial class TMessageLogger {

  public const string VALUE_NULL = "(null)";

  internal static string BuildDumpContent<T>(T source, int maxDepth = 1, int indent = 0) {

    Type? SourceClassType = source?.GetType();
    if (SourceClassType is null) {
      return string.Empty;
    }

    if (!SourceClassType.CanDump()) {
      return string.Empty;
    }

    try {

      switch (source) {

        #region --- Simple value --------------------------------------------
        case int:
        case long:
        case double:
        case float:
        case decimal:
        case bool:
        case DateTime:
        case DateOnly:
        case TimeOnly: {
            return source.ToString() ?? VALUE_NULL;
          }
        case string StringValue: {
            return StringValue.WithQuotes();
          }
        case char CharValue: {
            return $"'{CharValue}'";
          }
        case Enum EnumValue: {
            TypeCode ValueTypeCode = EnumValue.GetTypeCode();
            return $"{ValueTypeCode} : {EnumValue} : {Convert.ChangeType(EnumValue, ValueTypeCode)}";
          }
        #endregion --- Simple value -----------------------------------------

        #region --- Enumerable types --------------------------------------------
        case IDictionary DictItems: {
            StringBuilder DumpContent = new();
            DumpContent.AppendLine(TMessageLoggerSupport.ListDictionaryItems(DictItems, maxDepth, indent));
            return DumpContent.ToString();
          }

        case IEnumerable EnumerableItems: {
            StringBuilder DumpContent = new();
            DumpContent.AppendLine(ListEnumerableItems(EnumerableItems, maxDepth, indent));
            return DumpContent.ToString();
          }
        #endregion --- Enumerable types -----------------------------------------

        #region --- Class, struct, interface --------------------------------------------
        default: {
            #region --- Search for Dump method --------------------------------------------
            MethodInfo? DumpMethod = SourceClassType?.GetMethod(DUMP_METHOD, Type.EmptyTypes);
            if (DumpMethod is not null) {
              return $"{DumpMethod.Invoke(source, null) as string ?? VALUE_NULL}{Environment.NewLine}";
            }
            #endregion --- Search for Dump method -----------------------------------------

            StringBuilder DumpContent = new();
            IEnumerable<FieldInfo> Fields = SourceClassType?.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance) ?? Array.Empty<FieldInfo>();
            IEnumerable<FieldInfo> FilteredFields = Fields.Where(x => !x.Name.StartsWith('<'));
            foreach (var FieldInfoItem in FilteredFields.Where(f => f.CanDump())) {
              DumpContent.AppendLine(BuildDumpItem(FieldInfoItem.Name, FieldInfoItem, FieldInfoItem.GetValue(source), maxDepth, indent));
            }

            IEnumerable<PropertyInfo> Properties = SourceClassType?.GetProperties(BindingFlags.Public | BindingFlags.Instance) ?? Array.Empty<PropertyInfo>();
            foreach (var PropertyInfoItem in Properties.Where(p => p.CanDump())) {
              DumpContent.AppendLine(BuildDumpItem(PropertyInfoItem.Name, PropertyInfoItem, PropertyInfoItem.GetValue(source), maxDepth, indent));
            }

            return DumpContent.ToString();
          }
          #endregion --- Class, struct, interface -----------------------------------------

      }

    } catch (Exception ex) {
      return $"Unable to dump : {ex.Message}";
    }
  }

  internal static string BuildDumpItem<T>(string name, MemberInfo info, T itemValue, int maxDepth = 1, int indent = 2) {

    PropertyInfo? Property = info as PropertyInfo;
    FieldInfo? Field = info as FieldInfo;

    #region --- Error case : wrong property or field --------------------------------------------
    if (Property is null && Field is null) {
      throw new ArgumentException($"Invalid member info, must be PropertyInfo or FieldInfo : {name.WithQuotes()}", nameof(info));
    };
    #endregion --- Error case : wrong property or field -----------------------------------------

    Type TypeOfItem;
    string Prefix;
    if (Property is not null) {
      TypeOfItem = Property.PropertyType;
      Prefix = $"P{(Property.CanWrite ? string.Empty : ":R/O")}:{name}:";
    } else {
      TypeOfItem = Field?.FieldType ?? throw new ApplicationException($"Invalid field type : {name}");
      Prefix = $"F:{name}:";
    }

    Type? RealTypeOfItem = itemValue?.GetType();
    string RealTypeName;
    RealTypeOfItem = itemValue?.GetType();
    if (RealTypeOfItem is null) {
      RealTypeName = VALUE_NULL;
    } else {
      if (RealTypeOfItem.BaseType is not null && RealTypeOfItem.BaseType != typeof(object)) {
        RealTypeName = $"{RealTypeOfItem.BaseType.GetNameEx()}";
      } else {
        RealTypeName = RealTypeOfItem.GetNameEx();
      }
    }

    try {

      if (TypeOfItem == typeof(string)) {
        return $"{Prefix}string = {itemValue?.ToString()?.WithQuotes() ?? VALUE_NULL}";
      }

      if (TypeOfItem == typeof(char)) {
        return $"{Prefix}char = '{itemValue}'";
      }

      string TypeName = TypeOfItem.GetNameEx();

      if (TypeOfItem is IDictionary DictItems) {
        StringBuilder RetVal = new StringBuilder();
        RetVal.AppendLine(TMessageLoggerSupport.ListDictionaryItems(DictItems, maxDepth, indent));
        return RetVal.ToString();
      }

      if (TypeOfItem.IsIEnumerable()) {
        StringBuilder RetVal = new StringBuilder();
        RetVal.AppendIndent($"{Prefix}{RealTypeName}");
        IEnumerable EnumerableValue = (itemValue as IEnumerable ?? throw new ApplicationException("Unable to convert value")).Cast<object>();
        RetVal.AppendLine($" ({EnumerableValue.Cast<object>().Count()})");
        RetVal.AppendIndent(ListEnumerableItems(EnumerableValue, maxDepth - 1, indent), indent + 2);
        return RetVal.ToString();
      }

      if (RealTypeOfItem is null) {
        return $"{Prefix}{TypeName} = {VALUE_NULL}";
      }

      if (RealTypeOfItem.CanDigDeeper()) {
        if (maxDepth > 1) {
          StringBuilder RetVal = new StringBuilder();
          RetVal.AppendLine($"{Prefix}{RealTypeName}");
          RetVal.AppendIndent($"{BuildDumpContent(itemValue, maxDepth - 1, indent)}", indent + 2);
          return RetVal.ToString();
        } else {
          return $"{Prefix}{RealTypeName}";
        }
      } else {
        return $"{Prefix}{TypeName}:{RealTypeName} = {itemValue}";
      }

    } catch (Exception ex) {
      return $"{Prefix} = (unable to get value : {ex.Message})";
    }
  }

  internal static string ListEnumerableItems(IEnumerable source, int maxDepth = 1, int indent = 2) {
    if (source is null || source.Cast<object>().IsEmpty()) {
      return string.Empty;
    }

    StringBuilder RetVal = new();
    int Index = 0;
    foreach (var ItemItem in source) {
      if (ItemItem is null) {
        RetVal.AppendLine($"[{Index++}] (null)");
        continue;
      }

      if (ItemItem.GetType().CanDigDeeper()) {
        if (maxDepth > 1) {
          RetVal.AppendLine($"[{Index++}] {ItemItem.GetType().GetNameEx()}");
          RetVal.AppendIndent($"{BuildDumpContent(ItemItem, maxDepth - 1, indent)}", 4);
        } else {
          RetVal.AppendLine($"[{Index++}] {BuildDumpContent(ItemItem, maxDepth - 1, indent)}");
        }
      } else {
        RetVal.AppendLine($"[{Index++}] {BuildDumpContent(ItemItem, maxDepth - 1, indent)}");
      }
    }
    return RetVal.ToString();
  }


}
