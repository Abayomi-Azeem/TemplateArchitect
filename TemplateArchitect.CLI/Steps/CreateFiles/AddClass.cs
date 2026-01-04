using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateArchitect.CLI.Core;
using TemplateArchitect.CLI.Core.Common;

namespace TemplateArchitect.CLI.Steps.CreateFiles
{
    public class AddClass: StepHandler
    {
        private readonly string _layerName;
        private readonly string _className;
        private readonly string _folderName;
        public AddClass(string layerName,string folderName, string className)
        {
            _layerName = layerName;
            _folderName = folderName;
            _className = className;
        }
        public async override Task<ProcessResponse?> Handle(string workingDir, string appName, string dotNetVersion)
        {
            var projectDir = ProcessManager.FormulateProjectPath(workingDir, appName, _layerName);
            var newPath = Path.Combine(projectDir, _folderName);

            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);

            var classFilePath = Path.Combine(newPath, $"{_className}.cs");
            _ = File.Create(classFilePath);

            Console.WriteLine($"Added File:  {_className}...");
            return await base.Handle(workingDir, appName, dotNetVersion);
        }
    }
}
