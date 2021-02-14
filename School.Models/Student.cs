using System;
using System.Collections.Generic;
using System.Text;

namespace School.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string DoB { get; set; }

        public DateTime Date { get; set; }
        public List<Subject> Subjects { get; set; }

        public Student(int id, string name, string surname, string dob, DateTime date, List<Subject> subjects)
        {
            Id = id;
            Name = name;
            Surname = surname;
            DoB = dob;
            Date = date;
            Subjects = subjects;
        }
        public Student(int id, string name, string surname, string dob, DateTime date)
        {
            Id = id;
            Name = name;
            Surname = surname;
            DoB = dob;
            Date = date;
        }

        public Student()
        {
        }


    }
}
