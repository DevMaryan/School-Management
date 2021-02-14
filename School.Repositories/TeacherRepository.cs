using System;
using System.Collections.Generic;
using System.Text;
using School.Models;

namespace School.Repositories
{
    public class TeacherRepository
    {
        public TeacherRepository()
        {
            Database = new List<Teacher>();
        }
        public List<Teacher> Database { get; set; }

        // Create Teacher
        public void Create(Teacher newTeacher)
        {
            Database.Add(newTeacher);
        }

        // Delete Teacher
        public void Delete(Teacher newTeacher)
        {
            Database.Remove(newTeacher);
        }
    }
}
