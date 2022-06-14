using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Helpers
{
    public class ItemResult<T>
    {
        public ItemResult()
        {
            this.Errors = new List<string>();
            this.IsValid = true;

        }

        public T Item { get; set; }
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; }

    }
}
