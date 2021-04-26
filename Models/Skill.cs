using System.Collections.Generic;

namespace json_resume.Models
{
    public class Skill
    {
        public string name { get; set; }
        public string level { get; set; }
        public IEnumerable<string> keywords { get; set; }        
    }
}