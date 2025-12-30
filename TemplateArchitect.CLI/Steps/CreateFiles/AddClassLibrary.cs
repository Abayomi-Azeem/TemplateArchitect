using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateArchitect.CLI.Core;
using TemplateArchitect.CLI.Core.Common;

namespace TemplateArchitect.CLI.Steps.CreateFiles
{
    public class AddClassLibrary : StepHandler
    {
        private readonly string _layerName;
        public AddClassLibrary(string layerName)
        {
            _layerName = layerName;
        }
        public async override Task<ProcessResponse?> Handle(string workingDir, string appName, string dotNetVersion)
        {
            var projectName = $"{appName}.{_layerName}";
            var processResponse = await ProcessManager.RunProcess("dotnet", $"new classlib --name {projectName} --framework {dotNetVersion}", workingDir);

            if (processResponse == null)
                return new ProcessResponse() { IsSuccess = false, Error = "Unexpected Error Occured" };
            else if (!processResponse.IsSuccess)
                return processResponse;

            var projectPath = Path.Combine(workingDir, projectName);
            var addProjectToSolutionResponse = await ProcessManager.RunProcess("dotnet", $"sln add {projectPath}", workingDir);
            if (addProjectToSolutionResponse == null)
                return new ProcessResponse() { IsSuccess = false, Error = "Unexpected Error Occured" };
            else if (!addProjectToSolutionResponse.IsSuccess)
                return addProjectToSolutionResponse;
            Console.WriteLine($"Added Class Library:  {projectName}...");
            return await base.Handle(workingDir, appName, dotNetVersion);

        }
    }
}
