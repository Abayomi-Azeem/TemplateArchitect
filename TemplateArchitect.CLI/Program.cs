using Microsoft.VisualBasic.FileIO;
using System;
using System.CommandLine;
using System.Diagnostics;
using System.Reflection;
using TemplateArchitect.CLI.Core.Abstraction;
using TemplateArchitect.CLI.Core.Common;

if (args.Length < 1)
{
    Console.WriteLine("Usage:");
    Console.WriteLine("Template Arch <workingDirectory> <efCommand>");
    return 1;
}
Console.WriteLine($"[Starting] with Parameters: {string.Join(" ", args.ToList())}");




//string architectureType = args[0];
//string workingDir = args[1];
//string appName = args[2];
//string version = args[3];
var architectureTypeArg = new Argument<string>("architectureType");
var workingDirArg = new Argument<string>("workingDir");
var appNameArg = new Argument<string>("appName");
var versionArg = new Argument<string>("version");

var separateDtoOptionArg = new Option<string?>(
    aliases: new[] { "-S", "--separateDTOName" },
    name: "separateDtoOption");

var root = new RootCommand("Template Architect CLI Tool")
{
    architectureTypeArg,
    workingDirArg, 
    appNameArg, 
    versionArg,
    separateDtoOptionArg,     
};

root.SetAction(Execute);


return root.Parse(args).Invoke();

async Task<int> Execute(ParseResult parseRes)
{
    var architectureType = parseRes.GetValue(architectureTypeArg);
    var workingDir = parseRes.GetValue(workingDirArg);
    var appName = parseRes.GetValue(appNameArg);
    var version = parseRes.GetValue(versionArg);
    var separateDtoName = parseRes.GetValue(separateDtoOptionArg);

    var isValidArchitectureType = Enum.TryParse<ArchitectureEnums>(architectureType, out var architectureTypeEnum);

    var architecture = Assembly.GetExecutingAssembly()
                        .GetTypes()
                        .Where(x => !x.IsAbstract && typeof(Architecture).IsAssignableFrom(x))
                        .Select(x => new
                        {
                            Type = x,
                            Attribute = x.GetCustomAttribute<ArchitectureAttribute>()
                        })
                        .FirstOrDefault(x =>
                            x.Attribute.Name == architectureTypeEnum)?.Type;

    if (architecture == null)
        return 0;

    var instance = (Architecture)Activator.CreateInstance(architecture)!;
    var result = await instance.Run(workingDir, appName, version, new(separateDtoName));

    if (result == null)
        Console.WriteLine("Process Finished Successfully");
    else if (!result.IsSuccess)
        Console.WriteLine($"[ERR] => {result.Error}");
    else if (result.IsSuccess)
        Console.WriteLine($"Process Terminated Successfully => {result.Output}");
    return 1;
}






