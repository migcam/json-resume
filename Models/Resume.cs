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

            works = new List<Work>();
            works.Add(new Work{
                company = "mediatrix",
                position= "developer",
                website = "https://mediatrix.com.do",
                startDate = new DateTime(2020,11,2),
                summary = "contratista del banco BHD",
            });

            volunteers = new List<Volunteer>();
            volunteers.Add(new Volunteer{
                organization = "scouts dominicanos",
                position ="subguia",
                website = "https://scouts.do",
                startDate = new DateTime(2011,1,1),
                endDate = new DateTime(2019,9,1)
            });

            educations = new List<Education>();
            educations.Add(new Education{
                institution = "INTEC",
                area = "Software",
                studyType = "Bachelor",
                startDate = new DateTime(2017,11,2),
                endDate = new DateTime(2021,6,28),
                gpa = 3.77
            });

            awards = new List<Award>();
            awards.Add(new Award{
                title = "erasmus",
                date = new DateTime(2019,9,23),
                awarder = "Union Europea",
                summary = "Intercambio estudiantil"
            });

            skills = new List<Skill>();
            skills.Add(new Skill{
                name = "programming",
                level = "mid",
                keywords = new List<string>{
                    "ASP.NET core",
                    "Algorithms",
                    "Typescript"
                }
            });

            languages = new List<Language>();
            languages.Add(new Language{
                language  = "spanish",
                fluency = "native"
            });


            interests = new List<Interest>();
            interests.Add(new Interest{
                name = "wildlife",
                keywords = new List<string>{
                    "traveling",
                    "campming",
                    "sports"
                }
            });


            references = new List<Reference>();
            references.Add(new Reference{
                name = "francia mejia",
                reference = "fmejia@intec.edu.do"
            });



        }

    }
}