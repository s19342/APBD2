using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APBD2.Models
{
    public class Student
    {

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        [XmlAttribute]
        public String Index { get; set; }
        public String BirthDate { get; set; }
        public String NameOfMother { get; set; }
        public String NameOfFather { get; set; }
        public Studies StudiesSubLevel { get; set; }
    }
}
