using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Helpers
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            this.Errors = new List<string>();
            this.IsValid = true;

        }

        public bool IsValid { get; set; }
        public List<string> Errors { get; set; }

    }
}
