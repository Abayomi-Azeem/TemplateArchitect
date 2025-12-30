using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateArchitect.CLI.Core.Abstraction;

namespace TemplateArchitect.CLI.Core.Common
{
    public class StepHandler: IStepHandler
    {
        private IStepHandler? _nextStepHandler;
        
        public IStepHandler SetNext(IStepHandler stepHandler)
        {
            _nextStepHandler = stepHandler;
            return stepHandler;
        }

        public async virtual Task<ProcessResponse?> Handle(string workingDir, string appName, string dotNetVersion)
        {
            if ( _nextStepHandler != null)
            {
                return await _nextStepHandler.Handle(workingDir, appName, dotNetVersion);
            }
            else
            {
                return null;
            }
        }

    }
}
