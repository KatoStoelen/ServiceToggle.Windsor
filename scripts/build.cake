// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

#tool "nuget:?package=NUnit.ConsoleRunner&version=3.7.0"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target              = Argument("target",        "Build");
var buildConfiguration  = Argument("configuration", "Release");
var nugetFeed           = Argument("nugetFeed",     string.Empty);
var apiKey              = Argument("apiKey",        string.Empty);

//////////////////////////////////////////////////////////////////////
// VARIABLES
//////////////////////////////////////////////////////////////////////

var solutionName        = "Service.Toggle.Windsor";
var projectName         = solutionName;
var solution            = Directory("../src") + File($"{solutionName}.sln");
var artifactsPath       = Directory("../artifacts");
var packagesPath        = artifactsPath + Directory("packages");
var version             = "3.0.0";

var nuspecFile          = File($"../src/{projectName}/Service.Toggle.Windsor.nuspec");

//////////////////////////////////////////////////////////////////////
// SETUP
//////////////////////////////////////////////////////////////////////

Setup(context =>
{
    Information($"Version: {version}");
});

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("NuGet-Restore")
    .Description("Restoring NuGet packages")
    .Does(() =>
{
    NuGetRestore(solution);
});

Task("Create-AssemblyInfo")
    .Description("Creating AssemblyInfo")
    .Does(() =>
{
    var assemblyInfoFile = File($"../src/{projectName}/Properties/AssemblyInfo.cs");
    Information($"Creating assembly info: {assemblyInfoFile}");

    CreateAssemblyInfo(
        assemblyInfoFile,
        new AssemblyInfoSettings
        {
            Title = $"{projectName}",
            Description = string.Empty,
            Configuration = string.Empty,
            Company = "Kato Stoelen",
            Product = solutionName,
            Copyright = $"© Copyright {DateTime.Now.Year}. All Rights reserved.",
            Trademark = string.Empty,
            ComVisible = false,
            Version = version,
            FileVersion = version
        });
    
});

Task("Build")
    .Description($"Building {solutionName}")
    .IsDependentOn("NuGet-Restore")
    .Does(() =>
{
    MSBuild(
        solution,
        settings =>
        {
            settings
                .SetConfiguration(buildConfiguration)
                .SetNodeReuse(false)
                .SetMaxCpuCount(0)
                .SetVerbosity(Context.Log.Verbosity)
                .WithTarget("Clean")
                .WithTarget("Build")
                .WithProperty("DebugSymbols", new[] { "true" });
        });
});

Task("Unit-Test")
    .Description("Running unit tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    var unitTestAssemblies = GetFiles($"../src/**/bin/{buildConfiguration}/**/*UnitTest*.dll");

    NUnit3(
        unitTestAssemblies,
        new NUnit3Settings()
        {
            NoHeader = true,
            NoResults = true
        });
});

Task("Rewrite-NuSpec-Version")
    .Description("Rewriting NuSpec version number")
    .Does(() =>
{
    Information($"Setting version {version} in NuSpec: '{nuspecFile}'");

    XmlPoke(
        nuspecFile,
        "/package/metadata/version",
        version);
});

Task("Pack-Packages")
    .Description("Creating NuGet packages")
    .IsDependentOn("Build")  
    .Does(() =>
{
    if (DirectoryExists(packagesPath))
    {
        CleanDirectory(packagesPath);
    }
    else
    {
        CreateDirectory(packagesPath);
    }

    var nuspecFiles = GetFiles(nuspecFile.Path.ToString());

    NuGetPack(
        nuspecFiles,
        new NuGetPackSettings
        {
            OutputDirectory = packagesPath,
            NoPackageAnalysis = true,
            Properties = new Dictionary<string, string>
            {
                ["configuration"] = buildConfiguration
            }
        });
});

Task("Push-Packages")
    .Description("Pushing NuGet packages")
    .IsDependentOn("Pack-Packages")
    .WithCriteria(() => !string.IsNullOrEmpty(nugetFeed))
    .Does(() =>
{
    var nugetPackages = GetFiles($"{packagesPath}/*.nupkg");

    NuGetPush(
        nugetPackages,
        new NuGetPushSettings
        {
            Source = nugetFeed,
            ApiKey = apiKey
        });
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");

Task("CIBuild")
    .IsDependentOn("NuGet-Restore")
    .IsDependentOn("Create-AssemblyInfo")
    .IsDependentOn("Build")
    .IsDependentOn("Unit-Test")
    .IsDependentOn("Rewrite-NuSpec-Version")
    .IsDependentOn("Pack-Packages")
    .IsDependentOn("Push-Packages");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
