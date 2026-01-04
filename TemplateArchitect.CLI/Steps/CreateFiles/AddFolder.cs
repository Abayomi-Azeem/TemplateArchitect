using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateArchitect.CLI.Core;
using TemplateArchitect.CLI.Core.Common;

namespace TemplateArchitect.CLI.Steps.CreateFiles
{
    public class AddFolder : StepHandler
    {
        private readonly string _layerName;
        private readonly string _newDirName;
        public AddFolder(string layerName, string directoryName)
        {
            _layerName = layerName;
            _newDirName = directoryName;
        }
        public async override Task<ProcessResponse?> Handle(string workingDir, string appName, string dotNetVersion)
        {
            var projectDir = ProcessManager.FormulateProjectPath(workingDir, appName, _layerName);
            var newPath = Path.Combine(projectDir, _newDirName);

            if(!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);

            var classFilePath = Path.Combine(newPath, "Class1.cs");
            _ = File.Create(classFilePath);

            Console.WriteLine($"Added Folder:  {_newDirName}...");
            return await base.Handle(workingDir, appName, dotNetVersion);
        }
    }
}
