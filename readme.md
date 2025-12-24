# BLTools.Core

A comprehensive library of extension methods, utilities, and helper classes for modern .NET development.

## Supported Frameworks

- .NET 8.x
- .NET 9.x
- .NET 10.x

## Features

### Extension Methods

Extends various .NET classes with additional utility methods:

- **Byte Array** - Binary data manipulation and conversion
- **Dictionary** - IDictionary and DictionaryEntry helpers
- **Enumerable** - IEnumerable queries, block processing, and testing extensions
- **Date & Time** - DateTime and TimeSpan utilities
- **Exception** - Exception handling and information extraction
- **I/O** - BinaryReader and MemoryStream extensions
- **Math** - Mathematical utility functions
- **Tasks** - Async/await and Task helpers
- **Processes** - Process management utilities
- **Network** - Network-related extensions
- **.NET Reflection** - Type, FieldInfo, MethodInfo, and PropertyInfo utilities
- **Object Introspection** - Object.Dump() for debugging and diagnostics
- **Strings & Characters** - String manipulation and char processing
- **XML** - XDocument and XElement helpers

### Logging

Flexible ILogger implementation for multiple output targets:

- Console logging
- TextWriter-based logging
- File logging
- Trace logging

Supports various log levels and configurable message formatting.

### Command Line Arguments

#### SplitArgs

Robust command-line argument parser supporting:

- Multiple argument prefix styles (`-`, `/`, or no prefix)
- Space-separated arguments with quoted string support
- Automatic type conversion with fallback to default values
- Case-sensitive or case-insensitive matching
- Culture-specific value parsing

**Usage Example:**

### Text Formatting

#### TextBox

Create formatted text boxes and borders around messages for improved console readability.

### Data Structures

- **CircularList** - Fixed-size circular buffer implementation
- **FixedSizeQueue** - Generic queue with maximum capacity enforcement

## Installation

Install via NuGet:

````````

Or via Package Manager:

````````

## Getting Started

## Requirements

- .NET 8.0 or later
- C# 14.0 (latest language features with preview features enabled)

## Author

Luc Bolly

## Changelog

For detailed changes and version history, see the [changelog](./src/BLTools.core/_global_/changelog.txt).

## License

See LICENSE file for details.
