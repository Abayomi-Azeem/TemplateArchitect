
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateArchitect.CLI.Core.Abstraction;
using TemplateArchitect.CLI.Core.Common;
using TemplateArchitect.CLI.Steps.CreateFiles;
using TemplateArchitect.CLI.Steps.Referencing;

namespace TemplateArchitect.CLI.Core.Architectures
{
    [Architecture(ArchitectureEnums.Clean)]
    public class CleanArchitecture : Architecture
    {
        public override async Task<ProcessResponse?> Run(string workingDir, string appName, string dotNetVersion)
        {
            workingDir = ProcessManager.CreateDirectories(workingDir, appName);

            var solutionHandler = new CreateEmptySolution();
            solutionHandler.SetNext(new AddAspNetCoreWeb("Api"))
                .SetNext(new AddClassLibrary("Application"))
                .SetNext(new AddClassLibrary("Infrastructure"))
                .SetNext(new AddClassLibrary("Entities"))
                .SetNext(new AddProjectReference("Application", "Entities"))
                .SetNext(new AddProjectReference("Infrastructure", "Application"))
                .SetNext(new AddProjectReference("Api", "Application"))
                .SetNext(new AddProjectReference("Api", "Infrastructure"))
                ;

            var response = await solutionHandler.Handle(workingDir, appName, dotNetVersion);
            return response;

        }
    }
}
