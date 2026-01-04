using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateArchitect.CLI.Core.Abstraction;
using TemplateArchitect.CLI.Core.Common;
using TemplateArchitect.CLI.Core.Core.DTOs;
using TemplateArchitect.CLI.Steps.CreateFiles;
using TemplateArchitect.CLI.Steps.Referencing;

namespace TemplateArchitect.CLI.Core.Architectures
{
    [Architecture(ArchitectureEnums.CleanPlusDDD)]
    public class CleanPlusDDDArchitecture: Architecture
    {
        public override async Task<ProcessResponse?> Run(string workingDir, string appName, string dotNetVersion, SeparateDTOsClassLibraryInfo? separateDTOsClassLibraryInfo)
        {
            workingDir = ProcessManager.CreateDirectories(workingDir, appName);

            var solutionHandler = new CreateEmptySolution();
            solutionHandler.SetNext(new AddAspNetCoreWeb("Api"))
                .SetNext(new AddClassLibrary("Application"))
                .SetNext(new AddClassLibrary("Infrastructure"))
                .SetNext(new AddClassLibrary("Domain"))
                .SetNext(new AddProjectReference("Application", "Domain"))
                .SetNext(new AddProjectReference("Infrastructure", "Application"))
                .SetNext(new AddProjectReference("Api", "Application"))
                .SetNext(new AddProjectReference("Api", "Infrastructure"))
                .SetNext(new AddFolder("Infrastructure", "Persistence"))
                .SetNext(new AddFolder("Infrastructure", "Messaging"))
                .SetNext(new AddFolder("Application", "Abstractions"))
                .SetNext(new AddFolder("Application", "UseCases"))
                .SetNext(new AddFolder("Domain", "Common"))
                .SetNext(new AddClass("Domain", "Common","Entity"))
                .SetNext(new AddClass("Domain", "Common","ValueObject"))
                ;
            var response = await solutionHandler.Handle(workingDir, appName, dotNetVersion);

            if ((response == null || response.IsSuccess) && separateDTOsClassLibraryInfo != null && separateDTOsClassLibraryInfo.ShouldHaveSeparateDTOsClassLibrary)
            {
                var chain = new AddClassLibrary(separateDTOsClassLibraryInfo.SeparateDTOsClassLibraryName);
                chain.SetNext(new AddProjectReference("Application", separateDTOsClassLibraryInfo.SeparateDTOsClassLibraryName))
                    .SetNext(new AddProjectReference("Infrastructure", separateDTOsClassLibraryInfo.SeparateDTOsClassLibraryName))
                    .SetNext(new AddProjectReference("Api", separateDTOsClassLibraryInfo.SeparateDTOsClassLibraryName))
                    .SetNext(new AddProjectReference("Domain", separateDTOsClassLibraryInfo.SeparateDTOsClassLibraryName));

                response = await chain.Handle(workingDir, appName, dotNetVersion);
            }

            return response;

        }
    }
}
