using System;
using System.Collections.Generic;
using System.Text;
using School.Models;

namespace School.Repositories
{
    public class StudentRepository
    {
        public StudentRepository()
        {
            Database = new List<Student>();

        }
        public List<Student> Database { get; set; }



        // Create Student
        public void Create(Student newStudent)
        {
            Database.Add(newStudent);
        }

        // Delete Studfent
        public void Delete(Student newStudent)
        {
            Database.Remove(newStudent);
        }
    }
}
