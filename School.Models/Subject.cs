using System;
using System.Collections.Generic;
using System.Text;

namespace School.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name {get; set;}

        public Subject(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Subject()
        {
        }
    }
    
}
