# SimpleAutoMapper

SimpleAutoMapper is a lightweight and easy-to-use library for automatic object mapping in C#. It allows you to map an object to another one efficiently and intuitively.

## How to install

SimpleAutoMapper is available as a NuGet package. You can install it using the NuGet package manager in Visual Studio or through the command line.

```shell
Install-Package SimpleAutoMapper -Version 1.1.3
```
You can also download the package directly from the NuGet website at https://www.nuget.org/packages/SimpleAutoMapper.

How to use
To use SimpleAutoMapper, first add the following line at the top of your file:

```C#
using SimpleAutoMapper;
var response = AutoMapper.Map<ResponseModel>(request);
```

In this example, request is the object that you want to map and ResponseModel is the type of the target object. The Map<T> method creates a new instance of ResponseModel, maps the properties of the request object to the ResponseModel object and returns the ResponseModel object.

## Features
* Automatic mapping of properties with the same name and type.
* Support for primitive types, strings, Guids, DateTimes and nullable types.
* Recursive mapping for complex objects.
* Support for lists and collections.

## Limitations
* The properties must have public setters to be mapped.
* The mapping is not done in depth for lists or collections of complex objects. Each object in the list or collection is mapped individually.

We hope you find SimpleAutoMapper useful for your C# projects! If you have any questions or suggestions, feel free to open an issue.