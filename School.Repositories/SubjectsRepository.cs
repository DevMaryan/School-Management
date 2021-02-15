using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using School.Models;
namespace School.Repositories
{
    public class SubjectsRepository
    {
        public SubjectsRepository()
        {
            Database = new List<Subject>();

            File.AppendAllLines("Subjectz.txt", Database.Select(s => s.Id + " " + s.Name));
        }
        public List<Subject> Database { get; set; }



        // Create Subject
        public void Create(Subject newSubject)
        {
            Database.Add(newSubject);
        }

        // Delete Subject
        public void Delete(Subject chooseSubject)
        {
            Database.Remove(chooseSubject);
        }
    }
}
