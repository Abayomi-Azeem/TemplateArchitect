using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateArchitect.CLI.Core;
using TemplateArchitect.CLI.Core.Common;

namespace TemplateArchitect.CLI.Steps.Referencing
{
    public class AddProjectReference : StepHandler
    {
        private readonly string _referencedProject;
        private readonly string _projectName;

        public AddProjectReference(string projectName, string referencedProject)
        {
            _projectName = projectName;
            _referencedProject = referencedProject;
        }

        public async override Task<ProcessResponse?> Handle(string workingDir, string appName, string dotNetVersion)
        {
            var projectDir = FormulateProjectPath(workingDir, appName, _projectName);
            var referencedProjectDir = FormulateProjectPath(workingDir, appName, _referencedProject);
            var processResponse = await ProcessManager.RunProcess("dotnet", $"add {projectDir} reference {referencedProjectDir}", workingDir);

            if (processResponse == null)
                return new ProcessResponse() { IsSuccess = false, Error = "Unexpected Error Occured" };
            else if (!processResponse.IsSuccess)
                return processResponse;
            else
            {
                Console.WriteLine($"Added Project {_referencedProject} Reference TO {_projectName}...");
                return await base.Handle(workingDir, appName, dotNetVersion);
            }
        }

        private string FormulateProjectPath(string workingDir, string appName, string layerName)
        {
            var project = Path.Combine(workingDir, $"{appName}.{layerName}");
            return project;
        }
    }
}
