using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateArchitect.CLI.Core.Common;

namespace TemplateArchitect.CLI.Core.Abstraction
{
    public abstract class Architecture
    {
        public async virtual Task<ProcessResponse?> Run(string workingDir, string appName, string dotNetVersion)
        {
            return Task.FromResult(new ProcessResponse()).Result;
        }
    }
}
