# SimpleAutoMapper
**SimpleAutoMapper** is a lightweight and intuitive library designed to simplify object mapping in C#. With this tool, you can effortlessly map properties and nested objects between different models, making it ideal for projects that require quick and efficient object transformation.

## Installation
SimpleAutoMapper is available as a NuGet package. You can install it using the NuGet Package Manager in Visual Studio or via the .NET CLI:

```shell
Install-Package SimpleAutoMapper -Version 1.1.3
```
Alternatively, you can download the package directly from the NuGet website at https://www.nuget.org/packages/SimpleAutoMapper.

## Getting Started
To start using SimpleAutoMapper, add the following reference at the top of your file:

```C#
using SimpleAutoMapper;
var response = AutoMapper.Map<ResponseModel>(request);
```

In this example:

- `request` is the source object you want to map.
- `ResponseModel` is the target type.
- The `Map<T>` method creates a new instance of `ResponseModel`, maps the properties from `request`, and returns the result.

## Features
- Automatic Property Mapping: Automatically maps properties with matching names and types.
- Primitive Type Support: Handles basic data types, including strings, Guids, DateTimes, and nullable types.
- Recursive Mapping: Maps complex nested objects, supporting deep object hierarchies.
- List and Array Handling: Differentiates between collections of primitive types and complex objects, applying recursive mapping where needed.
- Nullable and Read-Only Handling: Manages nullable types and safely skips read-only properties.

## Limitations
- Public Setters Required: Properties must have public setters to be mapped.
- Shallow Mapping for Primitive Collections: Lists and arrays of primitive types are handled without deep inspection.
- Custom Mapping Logic: No support for custom conversion logic or attribute-based mappings.
- Circular References: Circular dependencies between objects are not handled, which can cause stack overflow errors.

## Usage Notes
- Ensure that complex objects are correctly initialized in nested properties to avoid null reference exceptions.
- The library uses reflection extensively; while efficient for many scenarios, it may introduce overhead in high-performance applications.

## Contributing
We welcome contributions! Feel free to open issues or submit pull requests to help improve SimpleAutoMapper. For major changes, please open an issue first to discuss your proposal.

## License
This project is licensed under the MIT License - see the [LICENSE](https://github.com/NetoBatista/simple_auto_mapper?tab=MIT-1-ov-file) file for details.

We hope SimpleAutoMapper makes your object mapping simpler and more efficient! If you have any questions or suggestions, feel free to open an issue.