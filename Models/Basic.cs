using System.Collections.Generic;

namespace  json_resume.Models
{
    public class Basic
    {
        public string name { get; set; }
        public string label { get; set; }
        public string picture { get; set; }
        public string email  { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public  string summary  { get; set; }

        public Location location { get; set; }

        public IList<Profile> profiles { get; set; }

    }
    
}