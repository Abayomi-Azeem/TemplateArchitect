using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateArchitect.WPF.Core.Dtos;
using TemplateArchitect.WPF.Core.Extensions;

namespace TemplateArchitect.WPF.Core
{
    public static class Utils
    {
        public static List<DropDownItemDto<T>> ToDisplayList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(x => new DropDownItemDto<T>(x, (x as Enum).GetDisplayName()))
                .ToList();
        }
    }
}
