using System;

namespace Enrollment_TextFile
{
    public class Subject
    {
        public string number;
        public string major;
        public string id;
        public string group;
        public string name;
        public string division;
        public string grade;
        public string unit;
        public string time;
        public string classroom;
        public string professor;
        public string language;


        public Subject(string[] args)
        {
            number = args[0];
            major = args[1];
            id = args[2];
            group = args[3];
            name = args[4];
            division = args[5];
            grade = args[6];
            unit = args[7];
            time = args[8];
            classroom = args[9];
            professor = args[10];
            language = args[11];

        }
    }
}
