using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Helpers
{
    public class ListResult<T>
    {
        public ListResult()
        {
            this.Items = new List<T>();
            this.Errors = new List<string>();
            this.IsValid = true;

        }

        public List<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; }

    }
}
