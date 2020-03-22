using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using APBD2.Models;

namespace APBD2
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputPath = args.Length > 0 ? args[0] : "Files\\data.csv";
            var outputPath = args.Length > 1 ? args[1] : "Files\\result";
            var outputType = args.Length > 2 ? args[2] : "xml";


            Console.WriteLine($"{inputPath}\n{outputPath}\n{outputType}");

            try
            {
                if (!File.Exists(inputPath))
                    throw new FileNotFoundException("File doe not exist\n", inputPath.Split("\\")[^1]);
                //^1 index from end 1 index from beginning
                var university = new University
                {
                    Author = "Adnan Azam"
                };
                foreach (var line in File.ReadAllLines(inputPath))
                {
                    //FileAppendAllText(outputPath, line + "\n");
                    var splitted = line.Split(",");
                    if (splitted.Length < 9)
                    {
                        File.AppendAllText("Files\\Log.txt", $"ERR NOT enough infomration in line { line}\n");
                        continue;
                    }
                    if (splitted[0] == "" || splitted[1] == "" || splitted[2] == "" || splitted[3] == "" || splitted[4] == "" ||
                        splitted[5] == "" || splitted[6] == "" || splitted[7] == "" || splitted[8] == "")
                    {
                        File.AppendAllText("Files\\Log.txt", $"ERR Empty column in line { line}\n");
                        continue;
                    }

                    var stud = new Student
                    {
                        FirstName = splitted[0],
                        LastName = splitted[1],
                        Email = splitted[6],
                        Index = splitted[4],
                        BirthDate = splitted[5],
                        NameOfMother = splitted[7],
                        NameOfFather = splitted[8],
                        StudiesSubLevel = new Studies
                        {
                            NameOfStudy = splitted[2],
                            ModeOfStudy = splitted[3]
                        }
                    };

                    var actstu = new ActiveStudies
                    {
                        Name = splitted[2],
                        NumberOfStudents = 1
                    };
                    
                    if(university.getActiveStudiesObject(actstu) != null)
                    {
                        university.getActiveStudiesObject(actstu).NumberOfStudents++;
                    }
                    else
                    {
                        university.ActiveStudies.Add(actstu);
                    }

                    university.Students.Add(stud);
                }

                //xml
                using var writer = new FileStream($"{outputPath}.{outputType}", FileMode.Create);
                var serializer = new XmlSerializer(typeof(University));
                serializer.Serialize(writer, university);

                //json
                var jsonString = JsonSerializer.Serialize(university);
                File.WriteAllText($"{outputPath}.json", jsonString);
            }
            catch(FileNotFoundException e)
            {
                File.AppendAllText("Files\\Log.txt", $"{DateTime.UtcNow} {e.Message} {e.FileName}\n");
            }
        }
    }
}
