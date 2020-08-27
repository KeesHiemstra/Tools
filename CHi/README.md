# CHi

This is **not** an application. This folder is a collection
of useful sources.

*chi* is my oldest network account name, back to 1987.

# .\Extensions

This folder is a collection of developed extensions.

### .\Extensions\DbContextExtensions

Show the private method IsDisposed of 
'System.Data.Entity.Internal.InternalContext'.

By some reason the context is closed. 

#### .\Extensions\PathExtensions

%OneDrive% is a environment variable. The system variable 
is added automatically added (to my experience).

#### TranslatePath()

Translate the PathName with %OneDrive% to the full path.

#### SavePath()

Replace the part of PathName to %OneDrive% if it exists.

### .\Extensions\ServiceExtensions

#### IsStarted(string, [bool], [bool]) : bool

Test if the service is running.

##### Example 1

``` C#
Console.WriteLine(ServiceExtensions.IsStarted("MSSQLServer", true));
```

Test if `MSSQLServer` is started. It shows a message box if it is not running.

#### DoesExist(string) : bool

Test if the service does exists.


### .\Extensions\WeatherExtensions

#### ConvertUnixTimeToDate()

Convert Unix time stamp (number of seconds since epoch 
to date/time).

## .\My Code Snippets

This folder contains own developed snippets for Visual Studio 2019.

To import the saved snippets in another environment (select only none existed):

``` 
%OneDrive%\Documents\Visual Studio 2019\Code Snippets\Visual C#\My Code Snippets
```

Copy the files the updated snippets.

# .\Src

## .\Src\Log.cs

A static log source.

