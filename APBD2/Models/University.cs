using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.Text.Json.Serialization;

namespace APBD2.Models
{
    public class University
    {
        public University()
        {
            Students = new HashSet<Student>();
            ActiveStudies = new HashSet<ActiveStudies>();
            CreationDate = DateTime.Now.ToString("yyyy-mm-dd");
        }

        [XmlAttribute]
        public string Author { get; set; }

        [JsonPropertyName("CreatedAt")]
        [XmlAttribute(AttributeName = "CreatedAt")]
        public string CreationDate { get; set; }
        public HashSet<Student> Students { get; set; }
        public HashSet<ActiveStudies> ActiveStudies { get; set; }

        public ActiveStudies getActiveStudiesObject(ActiveStudies element)
        {
            if (ActiveStudies.Contains(element))
            {
                foreach (ActiveStudies actstu in ActiveStudies)
                {
                    if (element.Equals(actstu))
                        return actstu;
                }
            }
            return null;
        }
    }
}
