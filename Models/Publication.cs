using System;

namespace json_resume.Models
{
    public class Publication
    {
        public string name { get; set; }
        public string publisher { get; set; }
        public DateTime releaseDate { get; set; }
        public string website { get; set; }
        public string summary { get; set; }
    }
}