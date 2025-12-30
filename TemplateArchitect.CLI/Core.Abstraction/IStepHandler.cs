using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateArchitect.CLI.Core.Abstraction
{
    public interface IStepHandler
    {
        IStepHandler SetNext(IStepHandler stepHandler);
        Task<ProcessResponse?> Handle(string workingDir, string appName, string dotNetVersion);
    }
}
