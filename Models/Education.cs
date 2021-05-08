using System;
using System.Collections.Generic;

namespace json_resume.Models
{
    public class Education
    {
        public string institution { get; set; }
        public string area { get; set; }
        public string studyType { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public double gpa { get; set; }
        public IEnumerable<string> courses { get; set; }
    }
}