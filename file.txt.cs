// You are using Dotnet 
using System;
using System.Collections.Generic;

class Course
{
    public string CourseName;
    public int CourseID;
    public string Department;
    public string Instructor;
    public int Credits;

    public Course(string courseName, int courseID, string department, string instructor, int credits)
    {
        CourseName = courseName;
        CourseID = courseID;
        Department = department;
        Instructor = instructor;
        Credits = credits;
    }

    public double CalculateDiscountedFee(double baseFee)
    {
        if (Credits > 4)
        {
            baseFee -= baseFee * 0.10;
        }
        else if (Credits >= 3)
        {
            baseFee -= baseFee * 0.05;
        }
        return baseFee;
    }
}

class DuplicateCourseIDError : Exception
{
    public DuplicateCourseIDError(string message) : base(message) { }
}

class CourseManager
{
    List<Course> courses = new List<Course>();

    public void AddCourse(Course course)
    {
        try
        {
            foreach (var c in courses)
            {
                if (c.CourseID == course.CourseID)
                {
                    throw new DuplicateCourseIDError("Course ID already exists");
                }
            }

            courses.Add(course);
            Console.WriteLine("Course details added successfully");
        }
        catch (DuplicateCourseIDError e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public void DisplayAllCourses()
    {
        double baseFee = 1000; // Example base fee
        foreach (Course c in courses)
        {
            Console.WriteLine($"Course Name: {c.CourseName}, Course ID: {c.CourseID}, Department: {c.Department}, Instructor: {c.Instructor}, Credits: {c.Credits}, Discounted Fee: {c.CalculateDiscountedFee(baseFee):F2}");
        }
    }

    public void UpdateCourseByID(int id, Course updatedCourse)
    {
        bool found = false;
        foreach (var c in courses)
        {
            if (c.CourseID == id)
            {
                c.CourseName = updatedCourse.CourseName;
                c.Department = updatedCourse.Department;
                c.Instructor = updatedCourse.Instructor;
                c.Credits = updatedCourse.Credits;
                Console.WriteLine("Course updated successfully.");
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("No course available with the given ID.");
        }
    }
}

class Program
{
    public static void Main()
    {
        CourseManager manager = new CourseManager();

        while (true)
        {
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    string name = Console.ReadLine();
                    int id = int.Parse(Console.ReadLine());
                    string dept = Console.ReadLine();
                    string instructor = Console.ReadLine();
                    int credits = int.Parse(Console.ReadLine());

                    Course newCourse = new Course(name, id, dept, instructor, credits);
                    manager.AddCourse(newCourse);
                    break;

                case 2:
                    manager.DisplayAllCourses();
                    break;

                case 3:
                    string updatedName = Console.ReadLine();
                    int updateId = int.Parse(Console.ReadLine());
                    string updatedDept = Console.ReadLine();
                    string updatedInstructor = Console.ReadLine();
                    int updatedCredits = int.Parse(Console.ReadLine());

                    Course updatedCourse = new Course(updatedName, updateId, updatedDept, updatedInstructor, updatedCredits);
                    manager.UpdateCourseByID(updateId, updatedCourse);
                    break;

                case 4:
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}