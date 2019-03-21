# SWMT | Coding Test C#/.Net/.NetCore 

The purpose of this assignment is to review your experience in .NET (either 4.6 or dotnetcore) &amp; C#. Even thought that the underlying task itself is very simple; we will be looking at following when analysing your solution.

### `Solution Information` section is the next `H1`

## Technical assignment

The purpose of this assignment is to review your experience in .NET (either 4.6
or dotnetcore) & C#. Even thought that the underlying task itself is very simple;
we will be looking at following when analysing your solution;

  * Structure of your application
  * Testability
  * Reusability
  * Readability
  * Performance

In a nutshell, it needs to be “production quality”.

## Supplied

Example file formatted with JSON on every row

```
{ "id": 31, "first": "Jill", "last": "Scott", "age":66 }
```
Note: It might or might not be a valid JSON structure.

## Delivery

1. Please use github – send us the link once you have completed the brief.

## Task

Please write a console application that outputs (no interaction needed):

  * The users full name for id=42
  * All the users first names (comma separated) who are 23
  * The number of genders per Age, displayed from youngest to oldest
    e.g
        Age : 17 Female: 2 Male: 3
        Age : 18 Female: 6 Male: 3

## Considerations

  1. The data source may change.
  2. The way source is fetched may change.
  3. The number of records may change (performance).
  4. The functionality may not always be consumed in a console app.

# SWMT | Solution Information | C#.NetCore 2.2

## JSON DataSource Configuration (Environment Variable)

The JSON data file is require as a data source for this program to fetch the data from. The JSON DataSource component reads the configuration about source file from the environment variable for particular object.

The provided sample JSON file seems like information of the people and to deserialize the information I have created `Swmt.Objects.Person` object in the code.

So, the JSON data source file configuration for the JSON DataSource implementation of `Swmt.Objects.Person` object should be configured in the following environment variable. 

```
SWMT_JSON_FILE_DATA_SOURCE_PERSON
```

How to build environment variable?

```
SWMT_JSON_FILE_DATA_SOURCE
```

`SWMT_JSON_FILE_DATA_SOURCE` is the constant part of any environment variable and the DataSource appends the upper case name of the class/object at the end with `_` to make the unique enviroment variable for the `Swmt.Objects.Person` object. This way we can reuse the JSON DataSource component for other objects in future by providing different JSON data source file for that particular object.

```
Swmt.Objects.Person = PERSON
```

So, combine `SWMT_JSON_FILE_DATA_SOURCE` and `PERSON` with `_` will make `SWMT_JSON_FILE_DATA_SOURCE_PERSON`

Meaning, let's say you add another object in future called `Swmt.Objects.Address` and you want to provide JSON data source file configuration for the JSON DataSource implementation of `Swmt.Objects.Address` object, you can configure `SWMT_JSON_FILE_DATA_SOURCE_ADDRESS` environment variable.

## Used Packages 

#### NO NEED TO RUN THE SCRIPT AND ADD MANUALLY, JUST FOR THE DOCUMENTATION PURPOSE!

```
# RESTORE PACKAGES #
[repo-root]$ dotnet restore 
```

```
dotnet add package Microsoft.CSharp
dotnet add package System.Dynamic.Runtime
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Newtonsoft.Json
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
```

## Used OS, Tools & Frameworks

* Mac
* Linux Ubuntu
* Studio Code
* NetCore 2.2
* NetCore CLI
* **NOTE** Haven't used `Microsoft Visual Studio` & `Resharper` at all.

## Resources For Help

* Microsoft .Net CLI website
* Stack Overflow website

## Run Instructions

For the Mac & the Linux users, from the root of the repository where the file `swmt.sln` resides,

```
# Tested #
[repo-root]$ export SWMT_JSON_FILE_DATA_SOURCE_PERSON=people.json
[repo-root]$ dotnet run --project ./swmt/swmt.csproj
```

```
[repo-root]$ bash swmt.run.sh
```

For the Windows users, from the root of the repository where the file `swmt.sln` resides,

```
# Not Tested #
[repo-root] > set SWMT_JSON_FILE_DATA_SOURCE_PERSON=people.json
[repo-root] > dotnet run --project \swmt\swmt.csproj
```

## Run Tests Instructions

For the Mac & the Linux users, from the root of the repository where the file `swmt.sln` resides,

```
# Tested #
[repo-root]$ export SWMT_JSON_FILE_DATA_SOURCE_PERSON=people.json
[repo-root]$ dotnet test ./swmt.tests/swmt.tests.csproj
```

```
[repo-root]$ bash swmt.tests.sh
```

For the Windows users, from the root of the repository where the file `swmt.sln` resides,

```
# Not Tested #
[repo-root] > set SWMT_JSON_FILE_DATA_SOURCE_PERSON=people.json
[repo-root] > dotnet run --project \swmt.tests\swmt.tests.csproj
```