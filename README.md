# RunAtStartup

Channel | Status
-|-
CI | [![CI](https://github.com/HMBSbige/RunAtStartup/workflows/CI/badge.svg)](https://github.com/HMBSbige/RunAtStartup/actions)
NuGet.org | [![NuGet.org](https://img.shields.io/nuget/v/RunAtStartup.svg)](https://www.nuget.org/packages/RunAtStartup/)

# Usage
```csharp
const string key = @"MyApp";
const string value = @"D:\114514\1919810.exe";
var service = new StartupService(key);
// var service = new StartupService(key, StartupType.LocalMachine));

var isSet = service.Check(value);

service.Set(@"""D:\MyAppPath\MyApp.exe"" -myArgs");

service.Delete();
```