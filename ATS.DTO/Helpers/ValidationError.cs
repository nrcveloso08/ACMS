using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Helpers
{
    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ValidationErrorMessage { get; set; }
        public object Value { get; set; }
    }
}
