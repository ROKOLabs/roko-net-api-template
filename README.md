## Table of contents
- [Overview](#overview)
- [Introduction](#introduction)
- [Project structure](#project-structure)
  - [Application](#application-layer)
  - [Blocks](#blocks-layer)
  - [Domain](#domain-layer)
  - [Infrastructure](#infrastructure-layer)
  - [Presentation](#presentation-layer)
- [Running project](#running-project)
- [Installing template](#installing-template)
- [Using template](#using-template)

# Overview
![Architecture image](./assets/Architecture.png)

# Introduction

This is a project template for creating .NET Core Web Api applications using .NET 8.0. Template is build using Onion Architecture. Main focus is to separate different infrastructure problems from an actual business requirements which is done by leveraging **Dependency Inversion Principal**.

Purpose of this approach is to easily distinguish different systems our application is communicating with by simply looking at folder structure. If you take a closer look at our infrastructure layer, you should immediately notice what is used as storage.

# Project structure

![Project structure image](./assets/ProjectStructure.png)

## Application Layer

Defines the jobs the software is supposed to do and directs the expressive domain objects to work out problems. The tasks this layer is responsible for are meaningful to the business or necessary for interaction with other systems.

This layer consist of two .NET libraries:
- Roko.Template.Application
- Roko.Template.Application.Contracts

### Roko.Template.Application.Contracts

This is where we are defining contracts our **Roko.Template.Application** Layer needs in order to communicate with Infrastructre layer.

**This layer only consists of models and interfaces.**

### Roko.Template.Application

This is where all business logic should happen. We are doing this by levering out **MediatR** commands and queries which follow **Separation of Concerns Principle** nicely. In order to communicate with database or any other system, we are using interfaces we define in **Roko.Template.Application.Contracts** to achieve this.
For validating our commands and queries, we are using **FluentValidation** which works nicely with **MediatR**

## Blocks Layer

This layers consists of helper classes that are used in one or more .NET libraries. Good examples is when you need specific exceptions in Presentation and Infrastructure layer or you have some extension methods on native .NET types. (eg. List extenisions)


## Domain Layer

 Responsible for representing concepts of the business, information about the business situation, and business rules. State that reflects the business situation is controlled and used here, even though the technical details of storing it are delegated to the infrastructure. This layer is the heart of business software.

This is where you would keep your domain objects.

## Infrastructure Layer

The infrastructure layer is how the data that is initially held in domain entities (in memory) is persisted in databases or another persistent store. An example is using Entity Framework Core code to implement the Repository pattern classes that use a DBContext to persist data in a relational database.

This is where you implementation of contracts lives.

Also, it is always a good practice to have multiple packages in your Infrastructure layer to create better separation between systems you are communicating with.

For example, let's say that your application is needs data from
- MSSQL database called "First"
- MSSQL database called "Second"
- POSTGRES database called "Third"
- HTTP Api database called "Four"

For each of those "system" you would create a separate library and put your implementation there.
- Roko.Template.Infrastructure.Db -> You need to have something shared between all db libraries
- Roko.Template.Infrastructure.Db.Mssql -> you need something shared between First and Second
- Roko.Template.Infrastructure.Db.Mssql.First
- Roko.Template.Infrastructure.Db.Mssql.Second
- Roko.Template.Infrastructure.Db.Postgres.Third
- Roko.Template.Infrastructure.Http.Four

This helps to keep your dependencies clean.


## Running project

- Clone repository
- Open Roko.Template.sln
- In Visual Studio navigate to src
    - Right click on Roko.Template
    - Click on **Set as Startup Project**
    - Click on **Manage User Secrets#**
    - Add following secrets

```
{
  "MssqlSettings:ConnectionString": "Server=localhost; Database=RokoTemplateDatabase;User Id=SA;Password=yourStrong(!)Password;TrustServerCertificate=Yes",
}
```
- In Roko.Template folder
    - open terminal
    - run
```
docker compose up
```
-  Start your application in Visual Studio

## Installing template

This will create your template for later usage via .NET CLI tools

- In Roko.Template folder
  - Open terminal and run
```
  dotnet new install .\
```
- Restart terminal (open it again)
- To check if your template named roko-api is installed run
```
dotnet new --list
```

## Using template

- First, install template
- Navigate to folder where you want to install template
- Run
```
dotnet new roko-api -n Roko.FirstProject
```
- Check if your project is generated correctly


nuget.exe pack "C:\Users\Mario\source\Roko.Template\roko-template.nuspec" -OutputDirectory "C:\Users\Mario\source\Roko.Template\.nupkg"

nuget.exe pack "C:\Users\Mario\source\Roko.Template\roko-template.nuspec" -OutputDirectory "C:\Users\Mario\source\Roko.Template\.nupkg" -NoDefaultExcludes

dotnet new install "C:\Users\Mario\source\Roko.Template\.nupkg\roko.template.api.1.0.0.nupkg"