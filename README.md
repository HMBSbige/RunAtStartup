# RunAtStartup

Channel | Status
-|-
CI | [![CI](https://github.com/HMBSbige/RunAtStartup/workflows/CI/badge.svg)](https://github.com/HMBSbige/RunAtStartup/actions)
NuGet.org | [![NuGet.org](https://img.shields.io/nuget/v/RunAtStartup.svg)](https://www.nuget.org/packages/RunAtStartup/)

# Usage
```csharp
const string key = @"MyApp";
var service = new StartupService(key);

var isSet = service.Check();

service.Set(@"""D:\MyAppPath\MyApp.exe"" -myArgs");

service.Delete();
```