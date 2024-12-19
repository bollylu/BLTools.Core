# BLTOOLS 6.5 core
Library of classes for generic support in .NET

## Compiled for 
.NET 8.x
.NET 9.x

## Content

### Extension methods
Extends various existing classes with methods and functions
- ByteArray
- IDictionnary, DictionaryEntry
- IEnumerable, IEnumerable by blocks, IEnumerable tests and extensions
- Date and time, TimeSpan
- Exception
- Binary reader, Memory streams
- Math
- Tasks
- Processes
- Network
- .NET Reflection (Type, FieldInfo, MethodInfo, PropertyInfo)
- Object Dump()
- Strings, chars
- XML

### Logging
ILogger implementation for
- Console
- TextWriter
- File
- Trace

### Command line arguments

#### SplitArgs
Allows the handling of command line arguments by their name. Arguments could start with '-' or '/' or even nothing.
Arguments are separated by a space. Strings are surrounded by double-quote '"'.
The value of the argument is retrieved already converted to the right type. A default value can be specified in case
the argument is missing or there is a error in the conversion.

### TextBox
Build lines or boxes around a message to make it more readable

### Data structures
- CircularList
- FixedSizeQueue

## Change log
[!INCLUDE [changelog.txt]("/src/BLTools.core/\_global\_")]
