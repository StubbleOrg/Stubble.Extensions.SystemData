# Stubble Extensions - System.Data [![Build status](https://img.shields.io/appveyor/ci/Romanx/stubble-extensions-systemdata.svg?style=flat-square)](https://ci.appveyor.com/project/Romanx/stubble-extensions-jsonnet) [![Coverage Status](https://img.shields.io/coveralls/StubbleOrg/Stubble.Extensions.SystemData.svg?style=flat-square)](https://coveralls.io/r/StubbleOrg/Stubble.Extensions.SystemData) [![Prerelease Nuget](https://img.shields.io/nuget/vpre/Stubble.Extensions.SystemData.svg?style=flat-square&label=nuget%20pre)](https://www.nuget.org/packages/Stubble.Extensions.SystemData/) [![Stable Nuget](https://img.shields.io/nuget/v/Stubble.Extensions.SystemData.svg?style=flat-square)](https://www.nuget.org/packages/Stubble.Extensions.SystemData/)

<img align="right" width="160px" height="160px" src="https://raw.githubusercontent.com/StubbleOrg/Stubble/dev/assets/extension-logo-256.png">

This repository contains easy to use extensions for the native C# System.Data types such as DataSets and DataTables.

To use this just include it in your project by downloading the dll from the release section,
or preferably including it from Nuget.org through the badge above.

Example Usage:
```csharp
var builder = new StubbleBuilder().AddSystemData().Build();
```

It's as simple as that, the package contains an Extension method for the StubbleBuilder adding in the ValueGetters required to handle DataTables, DataSets. It also adds the ability to use DataSets and DataTables as Lists.

## Credits

Straight Razor by Vectors Market from the Noun Project