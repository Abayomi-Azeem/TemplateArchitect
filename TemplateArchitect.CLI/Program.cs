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

string architectureType = args[0];
string workingDir = args[1];
string appName = args[2];
string version = args[3];
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
var result = await instance.Run(workingDir, appName, version);

if (result == null)
    Console.WriteLine("Process Finished Successfully");
else if (!result.IsSuccess)
    Console.WriteLine($"[ERR] => {result.Error}");
else if(result.IsSuccess)
    Console.WriteLine($"Process Terminated Successfully => {result.Output}");
return 1;

