using System;
using System.Collections.Generic;

namespace json_resume.Models
{
    public class Work
    {
        public string company { get; set; }
        public string position { get; set; }
        public string website { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string summary { get; set; }
        public IEnumerable<string> highlights { get; set; }

    }
}