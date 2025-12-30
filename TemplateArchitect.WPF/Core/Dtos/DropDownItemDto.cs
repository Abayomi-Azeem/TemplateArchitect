using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateArchitect.WPF.Core.Dtos
{
    public class DropDownItemDto<T>
    {
        public T Value { get; set; }
        public string DisplayName { get; set; }

        public DropDownItemDto(T val, string name)
        {
            Value = val;
            DisplayName = name;
        }
    }
}
