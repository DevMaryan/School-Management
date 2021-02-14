using System;
using System.Collections.Generic;
using System.Text;

namespace School.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<Subject> Subjects { get; set; }

        public List<Student> Students { get; set; }

        public DateTime Date { get; set; }

        public Teacher(int id, string name, string surname, List<Subject> subjects,List<Student> students, DateTime date)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Subjects = subjects;
            Students = students;
            Date = date;
        }

        public Teacher()
        {
        }
    }
}
