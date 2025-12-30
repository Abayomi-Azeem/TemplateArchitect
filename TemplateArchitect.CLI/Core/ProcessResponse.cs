using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateArchitect.CLI.Core
{
    public class ProcessResponse
    {
        public bool IsSuccess { get; set; }
        public string Output { get; set; }
        public string Error { get; set; }
    }

    public class ProcessResponseBuilder
    {
        public static ProcessResponse Success(string output)
        {
            return new ProcessResponse { Output = output, IsSuccess = true };
        }

        public static ProcessResponse Error(string error)
        {
            return new ProcessResponse { Error = error, IsSuccess = false };
        }
    }
}
