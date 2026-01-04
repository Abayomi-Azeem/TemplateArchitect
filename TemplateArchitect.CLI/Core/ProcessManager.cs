using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateArchitect.CLI.Core
{
    public class ProcessManager
    {
        public static async Task<ProcessResponse> RunProcess(string fileName, string args, string workingDir)
        {
            var response = new ProcessResponse();
            var processStartInfo = new ProcessStartInfo()
            {
                FileName = fileName,
                Arguments = args,
                WorkingDirectory = workingDir,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process
            {
                StartInfo = processStartInfo,
                EnableRaisingEvents = true,
            };

            process.OutputDataReceived += (sender, args) =>
            {
                if (!string.IsNullOrWhiteSpace(args.Data))
                    response = ProcessResponseBuilder.Success(args.Data);
            };
            process.ErrorDataReceived += (sender, args) =>
            {
                if (!string.IsNullOrWhiteSpace(args.Data))
                    response = ProcessResponseBuilder.Error(args.Data);
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync();

            return response;
        }

        public static string CreateDirectories(string workingDir, string appName)
        {
            var newPath = Path.Combine(workingDir, appName);

            var srcPath = Path.Combine(newPath, "src");
            var testPath = Path.Combine(newPath, "test");

            _ = Directory.CreateDirectory(srcPath);
            _ = Directory.CreateDirectory(testPath);

            return srcPath;
        }

        public static string FormulateProjectPath(string workingDir, string appName, string layerName)
        {
            var project = Path.Combine(workingDir, $"{appName}.{layerName}");
            return project;
        }
    }
}
