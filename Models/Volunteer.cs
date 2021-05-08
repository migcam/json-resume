using System;
using System.Collections.Generic;

namespace json_resume.Models
{
    public class Volunteer
    {
        public string organization { get; set; }
        public string position { get; set; }
        public string website { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string summary { get; set; }
        public IEnumerable<string> highlights { get; set; }

    }
}