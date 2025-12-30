using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateArchitect.CLI.Core.Common;

namespace TemplateArchitect.CLI.Core.Abstraction
{
    public sealed class ArchitectureAttribute: Attribute
    {
        public ArchitectureEnums Name { get; }

        public ArchitectureAttribute(ArchitectureEnums name)
        {
            Name = name;
        }
    }
}
