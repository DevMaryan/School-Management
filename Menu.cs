using System;
using System.Collections.Generic;
using System.IO;
using Artiflex.Services;

namespace Artiflex
{
    public class Menu
    {

        public static Service Service { get; set; }
        public static void TheMenu()
        {
            Service = new Service();
            Console.WriteLine("Welcome to Artiflex School");
            Console.WriteLine("Please choose one of the following options");


            var Stop = "";

            while (Stop != "y")
            {
                try
                {
                    Console.WriteLine("1. Student Menu");
                    Console.WriteLine("2. Teacher Menu");
                    Console.WriteLine("3. Add Subjects");
                    var userInput = Console.ReadLine();

                    switch (userInput)
                    {
                        case "1":
                            Console.WriteLine("You choose student menu");
                            Console.WriteLine("");
                            StudentMenu();
                            Console.WriteLine("");
                            break;
                        case "2":
                            Console.WriteLine("You choose teacher menu");
                            Console.WriteLine("");
                            TeacherMenu();
                            Console.WriteLine("");
                            break;
                        case "3":
                            Console.WriteLine("You choose Subjects menu");
                            Console.WriteLine("");
                            SubjectsMenu();
                            Console.WriteLine("");
                            break;
                        default:
                            Console.WriteLine("Invalid input");
                            break;
                    }
                    Console.WriteLine("Would you like to exit? type Y for yes");
                    Stop = Console.ReadLine();
                }
                catch (Exception e)
                {
                    var ErrorLog = $"{DateTime.Now} - {e.StackTrace} - {e.Message}";
                    File.AppendAllLines("Artiflex_Logs.txt", new List<string> { ErrorLog });
                }
            }
        }
        public static void StudentMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("1. Create Student Account");
            Console.WriteLine("2. Read Student Account");
            Console.WriteLine("3. Update Student Account");
            Console.WriteLine("4. Delete Student Account");
            Console.WriteLine("");
        }

        public static void TeacherMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("1. Create Teacher Account");
            Console.WriteLine("2. Read Teacher Account");
            Console.WriteLine("3. Update Teacher Account");
            Console.WriteLine("4. Delete Teacher Account");
            Console.WriteLine("");
        }
        public static void SubjectsMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("1. Create Subject");
            Console.WriteLine("2. Read Subject");
            Console.WriteLine("3. Update Subject");
            Console.WriteLine("4. Delete Subject");
            Console.WriteLine("");

        }
    }
}
