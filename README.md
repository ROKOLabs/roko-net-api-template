## Introduction

This is a wrapper project that enables creating a NuGet package for templates of solutions, projects and file items that can help generating web API solutions based on .Net 8.0 with `dotnet new` command.

## Template packages
Information about how to author NuGet packages containing templates can be found in [Tutorial: Create a template package](https://learn.microsoft.com/en-us/dotnet/core/tutorials/cli-templates-create-template-package?pivots=dotnet-8-0).

The package with templates is generated using a special csproj file created by `dotnet new templatepack -n "Roko.Net.Api.Templates.csproj"`. `templatepack` is installed by installing `Microsoft.TemplateEngine.Authoring.Templates` NuGet package.

The content and individual templates can be saved in a structure similar to that shown below.
```
root
│   Roko.Net.Api.Templates.csproj
└───content
    ├───template1
    │   └───.template.config
    │           template.json
    |   <...your files and folders for template1...>
    └───template2
        └───.template.config
                template.json
        <...your files and folders for template2...>
```

Individual templates can be .Net solutions, projects, or files, including console apps, web apps, and classes. What defines them as templates is a `.template.config` subfolder containing the `template.json` file with configuration.

You can also include conditional code in the template by referencing symbols defined in `template.json`. For more information on .Net templating, see the [wiki on GitHub](https://github.com/dotnet/templating/wiki).

## Building the package file
To build the NuGet package with templates, run the following command:
```
  dotnet pack Roko.Net.Api.Template.csproj
```

After a successful build, locate the `Roko.Net.Api.Template.X.Y.Z.nupkg` file in the `bin/Debug` folder (where X.Y.Z is the version number, such as 1.0.0).

## Using the template
To install the package, run the following command:
```
  dotnet new install <PathTo.nupkg>
```

After installing the package, new templates will be set up including the `roko-api` solution. To list your machine's installed packages, run the following command:
```
  dotnet new list
```

To create a new Web API .Net application, create a new folder called MyApplication, go to that folder, and run the following command:
```
  dotnet new roko-api -n My.Application.Name
```

A new solution with multiple projects and many files will be created. `Roko.Template` will be replaced by `My.Application.Name` in both file and folder names, as well as file contents. 

For more information, see the documentation for each template.

## Customizing
To add your own templates, create solution or project folders inside the content folder and include appropriate template configuration files.

To change some of the existing templates, change the files.

To install the package, run the command `dotnet new update <PathTo.nupkg>` after generating a new version with the `dotnet pack`.

> Don't forget to update the package version number as necessary. It is easy to do so by changing the `Roko.Net.Api.Templates.csproj`. 