using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateArchitect.CLI.Core.Core.DTOs
{
    public class SeparateDTOsClassLibraryInfo
    {
        public bool ShouldHaveSeparateDTOsClassLibrary { get; set; } = false;

        public string SeparateDTOsClassLibraryName { get; set; }

        public SeparateDTOsClassLibraryInfo(string? name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                ShouldHaveSeparateDTOsClassLibrary = false;
                SeparateDTOsClassLibraryName = "";
            }
            else
            {
                ShouldHaveSeparateDTOsClassLibrary = true;
                SeparateDTOsClassLibraryName = name;
            }
        }
    }
}
