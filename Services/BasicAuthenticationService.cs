using System.Collections.Generic;

namespace json_resume.Services
{
    public class BasicAuthenticationService
    {
        public Dictionary<string,string> users {get; set;}

        public BasicAuthenticationService(){
            users = new Dictionary<string, string>();
            users.Add("admin","admin");
        }
        
    }
}