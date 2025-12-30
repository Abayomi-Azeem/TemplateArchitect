using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateArchitect.WPF.Core.Enums
{
    public enum DotNetVersions
    {
        [Display(Name = "net6.0")]
        net6,
        [Display(Name = "net7.0")]
        net7,
        [Display(Name = "net8.0")]
        net8,
        [Display(Name = "net9.0")]
        net9
    }
}
