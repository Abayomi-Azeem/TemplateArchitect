using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateArchitect.CLI.Core;
using TemplateArchitect.CLI.Core.Common;

namespace TemplateArchitect.CLI.Steps.CreateFiles
{
    public class CreateEmptySolution: StepHandler
    {
        public async override Task<ProcessResponse?> Handle(string workingDir, string appName, string dotNetVersion)
        {
            var processResponse = await ProcessManager.RunProcess("dotnet", $"new sln --name {appName}", workingDir);
            if (processResponse == null)
                return new ProcessResponse() { IsSuccess = false, Error = "Unexpected Error Occured" };
            else if (!processResponse.IsSuccess)
                return processResponse;
            else
            {
                Console.WriteLine($"Empty Solution Created...");
                return await base.Handle(workingDir, appName, dotNetVersion);
            }
        }
    }
}
