using System.Collections.Generic;

namespace json_resume.Models
{
    public class Interest
    {
        public string name { get; set; }
        public IEnumerable<string> keywords { get; set; }

    }
}