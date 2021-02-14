using System;
using System.Collections.Generic;
using System.Text;
using School.Models;
using School.Repositories;
using System.Linq;
using System.IO;

namespace School.Services
{
    public class Service
    {
        public Service()
        {

            chosenSubjects = new List<Subject>();
            _student = new Student();
            _teacher = new Teacher();
            _subject = new Subject();
            _studentRepository = new StudentRepository();
            _teacherRepository = new TeacherRepository();
            _subjectRepository = new SubjectsRepository();
        }

        public List<Subject> chosenSubjects { get; set; }
        public Student _student { get; set; }
        public Teacher _teacher { get; set; }
        public Subject _subject { get; set; }
        public StudentRepository _studentRepository { get; set; }
        public TeacherRepository _teacherRepository { get; set; }
        public SubjectsRepository _subjectRepository { get; set; }

        // STUDENT //

        // Create Student
        public void CreateStudent()
        {
            // Fill the Info to create student
            Console.WriteLine("Student name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Student surname:");
            var surname = Console.ReadLine();
            Console.WriteLine("Student Day of Birth:");
            var dob = Console.ReadLine();

            List<Subject> chosenSubjects = new List<Subject>();
            if(_subjectRepository.Database.Count > 0)
            {
                Console.WriteLine("Student Subjects:");
                var stop = "";
                while (stop != "y")
                {
                    // List Subjects
                    ViewSubjects();
                    int userSelect = int.Parse(Console.ReadLine());
                    var selectSubject = _subjectRepository.Database.FirstOrDefault(x => x.Id == userSelect);
                    chosenSubjects.Add(selectSubject);

                    Console.WriteLine("Would you like to add more? Press any key or Y to exit");
                    var userChoice = Console.ReadLine();
                    if (userChoice == "y")
                    {
                        break;
                    }
                }

                // Constructor
                Student newStudent = new Student(GenerateStudentIds(), name, surname, dob, DateTime.Now, chosenSubjects);
                // Transfer to Repositories to be added to DB
                _studentRepository.Create(newStudent);
                Console.WriteLine("");
                Console.WriteLine($"Student {newStudent.Name} has been added.");

            }
            else
            {
                Student newStudent = new Student(GenerateStudentIds(), name, surname, dob, DateTime.Now);
                // Transfer to Repositories to be added to DB
                _studentRepository.Create(newStudent);

                Console.WriteLine($"Student {newStudent.Name} has been added.");
            }



        }

        // Read - View - Students 
        public void ViewStudents()
        {
            foreach (var student in _studentRepository.Database)
            {
                Console.WriteLine($"{student.Id} - {student.Name}");
            }
        }

        public void ReadStudent()
        {
            if(_studentRepository.Database.Count > 0)
            {

                // List all students
                foreach (var student in _studentRepository.Database)
                {
                    Console.WriteLine($"{student.Id} - {student.Name}");
                }

                // Choose student 
                Console.WriteLine("Choose ID to see more student details");
                var userChoice = int.Parse(Console.ReadLine());

                // Compare the student with DB
                var selectedStudent = _studentRepository.Database.FirstOrDefault(x => x.Id == userChoice);
                if(selectedStudent.Subjects != null)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Student ID: {selectedStudent.Id} \nStudent Name: {selectedStudent.Name}\nStudent Surname: {selectedStudent.Surname}  \nListening Subjects: {selectedStudent.Subjects.Count} \nCreated: {selectedStudent.Date}");
                    Console.WriteLine("");
                    Console.WriteLine("Student subjects: ");
                    if (selectedStudent.Subjects.Count == 0)
                    {
                        Console.WriteLine("Student is not assigned to any subject");
                    }
                    else
                    {
                        try
                        {
                            foreach (var subject in selectedStudent.Subjects)
                            {
                                Console.WriteLine($"\t{subject.Name}");
                            }
                            Console.WriteLine("");
                        }
                        catch
                        {
                            Console.WriteLine("Error occured at student line 115");
                        }

                    }
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Student ID: {selectedStudent.Id} \nStudent Name: {selectedStudent.Name}\nStudent Surname: {selectedStudent.Surname}  \nCreated: {selectedStudent.Date}");
                    Console.WriteLine("");
                    Console.WriteLine("Student is not assigned to any subject");

                }

            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("No STUDENTS found.");
                Console.WriteLine("");
            }
        }

        // Update Student
        public void UpdateStudent()
        {
            // View all students 
            ViewStudents();

            // Select Student
            Console.WriteLine("Please choose an student by his ID");
            var selectedID = int.Parse(Console.ReadLine());

            // Now we fetch the student from DB
            var selectedStudent = _studentRepository.Database.FirstOrDefault(x => x.Id == selectedID);

            // Ask user what he wants to update
            Console.WriteLine("What would you like to update?");
            Console.WriteLine("1. Student name");
            Console.WriteLine("2. Student surname");
            Console.WriteLine("3. Student day of birth");
            Console.WriteLine("4. Add Subjects");

            var selectedUpdate = Console.ReadLine();
            switch (selectedUpdate)
            {
                case "1":
                    Console.WriteLine("Enter the new name");
                    var newName = Console.ReadLine();
                    selectedStudent.Name = newName;
                    break;
                case "2":
                    Console.WriteLine("Enter the new surname");
                    var newSurname = Console.ReadLine();
                    selectedStudent.Surname = newSurname;
                    Console.WriteLine();
                    Console.WriteLine($"{selectedStudent.Name} surname has been changed to {newSurname}");
                    break;
                case "3":
                    Console.WriteLine("Enter the new day of birth");
                    var newDob = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine($"{selectedStudent.Name} birthday has been changed to {newDob}");
                    break;
                case "4":
                    Console.WriteLine("Select the subjects");

                    if (selectedStudent.Subjects != null)
                    {

                        var stop = "";
                        while(stop != "y")
                        {

                            foreach (var subject in _subjectRepository.Database)
                            {
                                Console.WriteLine($"{subject.Id} {subject.Name}");
                            }
                            var userPicked = int.Parse(Console.ReadLine());
                            var pickSubject = _subjectRepository.Database.FirstOrDefault(x => x.Id == userPicked);

                            selectedStudent.Subjects.Add(pickSubject);
                            Console.WriteLine("");
                            Console.WriteLine($"{pickSubject.Name} has been added.");
                            Console.WriteLine("");
                            Console.WriteLine("Press y to exit");
                            var userChoice = Console.ReadLine();
                            if (userChoice == "y")
                            {
                                break;
                            }
                        }
                        Console.WriteLine();
                    }
                    if(selectedStudent.Subjects == null)
                    {
                        Console.WriteLine("Please select subject that you want to assign");
                        // List all Subjects 
                        foreach (var subject in _subjectRepository.Database)
                        {
                            Console.WriteLine($"{subject.Id} {subject.Name}");
                        }
                        // User picks subject ID
                        var selectedSubject = int.Parse(Console.ReadLine());

                        // We are getting that subject 
                        var getSubject = _subjectRepository.Database.FirstOrDefault(x => x.Id == selectedSubject);


                        // Final step to add it
                        selectedStudent.Subjects = new List<Subject>(){ getSubject };
                        Console.WriteLine($"The {getSubject.Name} has been assigned to {selectedStudent.Name}.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("There are not SUBJECTS at the moment");
                        Console.WriteLine("");
                    }

                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }
        }

        // Delete Student
        public void DeleteStudent()
        {
            // List Student
            ViewStudents();

            // Select ID
            Console.WriteLine("Please choose an student by ID");
            var userChoice = int.Parse(Console.ReadLine());

            // Match the Id with DB
            var selectedStudent = _studentRepository.Database.FirstOrDefault(x => x.Id == userChoice);

            // Now transfer the object to repository to delete it
            _studentRepository.Delete(selectedStudent);
            Console.WriteLine($"{selectedStudent.Name} has been deleted.");
            Console.WriteLine("");


        }

        // ID Student
        private int GenerateStudentIds()
        {
            var newId = 0;
            if (_studentRepository.Database.Count > 0)
            {
                newId = _studentRepository.Database.Max(x => x.Id);
            }
            return newId + 1;
        }


        // TEACHER //

        // Create Teacher
        public void CreateTeacher()
        {
            // Enter Teacher Info
            Console.WriteLine("Enter teacher Name");
            var teacherName = Console.ReadLine();

            Console.WriteLine("Enter teacher Surname");
            var teacherSurname = Console.ReadLine();

            Console.WriteLine("Choose Subjects to teach");

            List<Subject> chosenSubjects = new List<Subject>();

            var stop = "";
            while (stop != "y")
            {
                // List Subjects
                ViewSubjects();
                int userSelect = int.Parse(Console.ReadLine());
                var selectSubject = _subjectRepository.Database.FirstOrDefault(x => x.Id == userSelect);
                chosenSubjects.Add(selectSubject);

                Console.WriteLine("Would you like to add more? Press any key OR y to exit");
                var userChoice = Console.ReadLine();
                if (userChoice == "y")
                {
                    break;
                }
            }

            Console.WriteLine("Add Students to your class");

            List<Student> chosenStudent = new List<Student>();
            var stopStudent = "";
            while (stopStudent != "y")
            {
                // List Subjects
                ViewStudents();
                int userSelect = int.Parse(Console.ReadLine());
                var selectedStudent = _studentRepository.Database.FirstOrDefault(x => x.Id == userSelect);
                chosenStudent.Add(selectedStudent);

                Console.WriteLine("Would you like to add more? Press any key OR y to exit");
                var userChoice = Console.ReadLine();
                if (userChoice == "y")
                {
                    break;
                }
            }

            // Constructor
            Teacher newTeacher = new Teacher(GenerateTeacherIds(), teacherName, teacherSurname, chosenSubjects, chosenStudent, DateTime.Now);

            // Transfer the Object to DB
            _teacherRepository.Create(newTeacher);

        }
        // Read-View Teacher
        public void ViewTeachers()
        {
            foreach(var teacher in _teacherRepository.Database)
            {
                Console.WriteLine($"ID:{teacher.Id} Teacher: {teacher.Name} {teacher.Surname}");
            }
        }

        public void ReadTeacher()
        {
            if(_teacherRepository.Database.Count > 0)
            {
                // List all students
                foreach (var teacher in _teacherRepository.Database)
                {
                    Console.WriteLine($"{teacher.Id} - {teacher.Name}");
                }

                // Choose student 
                Console.WriteLine("Choose ID to see more teacher details");
                var userChoice = int.Parse(Console.ReadLine());

                // Compare the student with DB
                try
                {
                    var selectedTeacher = _teacherRepository.Database.FirstOrDefault(x => x.Id == userChoice);
                    if (selectedTeacher != null)
                    {
                        Console.WriteLine("");
                        Console.WriteLine($"Student ID: {selectedTeacher.Id} \nStudent Name: {selectedTeacher.Name} {selectedTeacher.Surname}  \nTeaching Subjects: {selectedTeacher.Subjects.Count} \nTeaching Students:{selectedTeacher.Students.Count} \nCreated {selectedTeacher.Date}");
                        Console.WriteLine("");
                        Console.WriteLine("Teacher subjects: ");
                        if (selectedTeacher.Subjects.Count == 0)
                        {
                            Console.WriteLine("No teaching subjects");
                        }
                        else
                        {
                            foreach (var subject in selectedTeacher.Subjects)
                            {
                                Console.WriteLine($"\t{subject.Name}");
                            }
                            Console.WriteLine("");
                            Console.WriteLine("Teacher students: ");
                        }

                        if (selectedTeacher.Students.Count == 0)
                        {
                            Console.WriteLine("There is no assigned students");

                        }
                        else
                        {
                            foreach (var student in selectedTeacher.Students)
                            {
                                Console.WriteLine($"\t{student.Name}");
                            }
                            Console.WriteLine("");
                        }

                    }
                    else
                    {
                        Console.WriteLine("No TEACHERS found!");
                    }

                }
                catch (Exception e)
                {
                    var ErrorLog = $"{DateTime.Now} - {e.StackTrace} - {e.Message}";
                    File.AppendAllLines("ReadTeacher.txt", new List<string> { ErrorLog });

                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("No teachers founds");
                Console.WriteLine("");
            }
           



        }

        // Update Teacher
        public void UpdateTeacher()
        {
            // Show all teacers
            ViewTeachers();

            // Choose Teacher
            Console.WriteLine("Choose an teacher by his ID");
            var userChoice = int.Parse(Console.ReadLine());

            // Get the Teacher from DB
            var selectedTeacher = _teacherRepository.Database.FirstOrDefault(x => x.Id == userChoice);

            Console.WriteLine("");
            Console.WriteLine("Choose one of the following options");
            Console.WriteLine("1. To change the name");
            Console.WriteLine("2. To change the surname");
            Console.WriteLine("3. Remove Student");
            Console.WriteLine("4. Remove Subject");
            Console.WriteLine("");

            var userOption = int.Parse(Console.ReadLine());
            switch (userOption)
            {
                case 1:
                    Console.WriteLine("Please enter the new name");
                    var newName = Console.ReadLine();
                    selectedTeacher.Name = newName;
                    break;
                case 2:
                    Console.WriteLine("Please enter the new surname");
                    var newsurname = Console.ReadLine();
                    selectedTeacher.Surname = newsurname;
                    break;
                case 3:
                    Console.WriteLine("Please select student that you want to be removed");
                    // List all students from teacher
                    foreach(var student in selectedTeacher.Students)
                    {
                        Console.WriteLine($"{student.Id} {student.Name}");
                    }
                    // User is Picking a student
                    var selectedStudent = int.Parse(Console.ReadLine());

                    // We are getting the student object
                    var getStudent = selectedTeacher.Students.FirstOrDefault(x => x.Id == selectedStudent);
                    
                    // We are removing the student from teachers subjects
                    selectedTeacher.Students.Remove(getStudent);
                    Console.WriteLine($"The {getStudent.Name} has been removed from the class.");
                    break;
                case 4:
                    Console.WriteLine("Please select subject that you want to be removed");
                    // List all Subjects 
                    foreach (var subject in selectedTeacher.Subjects)
                    {
                        Console.WriteLine($"{subject.Id} {subject.Name}");
                    }
                    // User picks subject ID
                    var selectedSubject = int.Parse(Console.ReadLine());

                    // We are getting that subject 
                    var getSubject = selectedTeacher.Subjects.FirstOrDefault(x => x.Id == selectedSubject);
                    
                    // Final step to remove it
                    selectedTeacher.Subjects.Remove(getSubject);
                    Console.WriteLine($"The {getSubject.Name} has been removed from teaching subjects.");
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;

            }

        }

        // Delete Teacher
        public void DeleteTeacher()
        {
            // Show all Teachers
            ViewTeachers();

            // Choose Teacher
            Console.WriteLine("Choose an teacher by his ID");
            var userChoice = int.Parse(Console.ReadLine());

            // Get the Teacher from DB
            var selectedTeacher = _teacherRepository.Database.FirstOrDefault(x => x.Id == userChoice);

            // Transfer the object to repository for deleting
            _teacherRepository.Delete(selectedTeacher);
        }


        // ID for Teacher
        private int GenerateTeacherIds()
            {
                var newId = 0;
                if (_teacherRepository.Database.Count > 0)
                {
                    newId = _teacherRepository.Database.Max(x => x.Id);
                }
                return newId + 1;
            }

        // SUBJECTS //

        // Create Subject
        public void CreateSubject()
        {
            var stop = "";
            while(stop != "y")
            {
                // Ask user to enter subject name
                Console.WriteLine("Enter Subject Name");
                var subjectName = Console.ReadLine();

                // Creating new Subject
                Subject newSubject = new Subject(GenerateSubjectIds(), subjectName);

                // Adding the Subject to Repository Method to be Created
                _subjectRepository.Create(newSubject);

                // Info subject 
                Console.WriteLine("");
                Console.WriteLine($"{newSubject.Name} is added.");
                Console.WriteLine("");

                Console.WriteLine("Press any key to add more subjects or Press y to exit");
                var userChoice = Console.ReadLine();
                if(userChoice == "y")
                {
                    break;
                }
            }

        }

        // Read-View Subject
        public void ViewSubjects()
        {
            if(_subjectRepository.Database.Count > 0)
            {
                foreach (var subject in _subjectRepository.Database)
                {
                    Console.WriteLine($"{subject.Id} - {subject.Name}");
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("No SUBJECTS found");
                Console.WriteLine("");
            }

        }

        // Update Subject
        public void UpdateSubject()
        {
            Console.WriteLine("Please choose and ID of subject");
            // Show all subjects
            ViewSubjects();

            // Change the Name
            var userChoice = int.Parse(Console.ReadLine());

            // Select Subject
            var selectedSubject = _subjectRepository.Database.FirstOrDefault(x => x.Id == userChoice);

            // Update the new name
            Console.WriteLine("Enter the new name:");
            var newName = Console.ReadLine();
            Console.WriteLine($"Subject {selectedSubject.Name} name has changed to {newName}");
            selectedSubject.Name = newName;

        }

        // Delete Subject
        public void DeleteSubject()
        {
            // Show all Subjects
            ViewSubjects();

            // Choose ID of subject to delete
            var chooseSubject = int.Parse(Console.ReadLine());

            // Select the subject from DB
            var selectedSubject = _subjectRepository.Database.FirstOrDefault(x => x.Id == chooseSubject);

            // Delete the subject from DB
            _subjectRepository.Delete(selectedSubject);
            Console.WriteLine($"The {selectedSubject.Name} has been deleted.");
        }

        // ID for Subject
        private int GenerateSubjectIds()
        {
            var newId = 0;
            if (_subjectRepository.Database.Count > 0)
            {
                newId = _subjectRepository.Database.Max(x => x.Id);
            }
            return newId + 1;
        }
    }
}
