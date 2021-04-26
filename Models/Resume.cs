using System;
using System.Collections.Generic;

namespace json_resume.Models
{
    public class Resume
    {
        public Basic basics { get; set; }
        public List<Work> works { get; set; }
        public List<Volunteer> volunteers { get; set; }
        public IList<Education> educations { get; set; }
        public List<Award> awards { get; set; }
        public List<Publication> publications { get; set; }
        public List<Skill> skills { get; set; }
        public List<Language> languages { get; set; }
        public List<Interest> interests { get; set; }
        public List<Reference> references { get; set; }

        public Resume(){
            basics = new Basic{
                name = "Miguel Camarena",
                label ="Developer",
                picture = "",
                email = "miguelcamarena@gmial.com",
                phone = "809-917-7555",
                website = "",
                summary = "i like coding im from dr",
                location = new Location{
                    address = "antiga duarte",
                    postalCode = "10607",
                    city = "Santo Domingo",
                    region = "Este"
                },
                profiles= new List<Profile>{
                    new Profile{
                        network = "Instagram",
                        username = "miguelcamarena",
                        url = "https://instragram.com/miguelcamarena"
                    }
                }
            };

        }

    }
}