using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Students;
using Test.Core.StudyHouses;
using Test.EntityFramework;

namespace Test.EntityFramework.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TestDbContext context)
        {
            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var studyHouse = new StudyHouse[] {
                new StudyHouse {Name="Gryffindor",CreateDate=DateTime.UtcNow},
                new StudyHouse {Name="Hufflepuff",CreateDate=DateTime.UtcNow},
                new StudyHouse {Name="Ravenclaw",CreateDate=DateTime.UtcNow},
                new StudyHouse {Name="Slytherin",CreateDate=DateTime.UtcNow},
            };


            context.StudyHouses.AddRange(studyHouse);
            context.SaveChanges();

            var students = new Student[]
            {
                new Student{Name="Carson",LastName="Alexander",Age=15,Identification="1321321",StudyHouseId=1},
                new Student{Name="Arturo",LastName="Anand",Age=15,Identification="1321321",StudyHouseId=2},
                new Student{Name="Meredith",LastName="Alonso",Age=15,Identification="1321321",StudyHouseId=3},
                new Student{Name="Gytis",LastName="Barzdukas",Age=15,Identification="1321321",StudyHouseId=2},
                new Student{Name="Yan",LastName="Li",Age=15,Identification="1321321",StudyHouseId=1},
                new Student{Name="Peggy",LastName="Justice",Age=15,Identification="1321321",StudyHouseId=4},
                new Student{Name="Laura",LastName="Norman",Age=15,Identification="1321321",StudyHouseId=1},
                new Student{Name="Nino",LastName="Olivetto",Age=15,Identification="1321321",StudyHouseId=1},
            };

            context.Students.AddRange(students);
            context.SaveChanges();

            //var courses = new Course[]
            //{
            //    new Course{CourseID=1050,Title="Chemistry",Credits=3},
            //    new Course{CourseID=4022,Title="Microeconomics",Credits=3},
            //    new Course{CourseID=4041,Title="Macroeconomics",Credits=3},
            //    new Course{CourseID=1045,Title="Calculus",Credits=4},
            //    new Course{CourseID=3141,Title="Trigonometry",Credits=4},
            //    new Course{CourseID=2021,Title="Composition",Credits=3},
            //    new Course{CourseID=2042,Title="Literature",Credits=4}
            //};

            //context.Courses.AddRange(courses);
            //context.SaveChanges();

            //var enrollments = new Enrollment[]
            //{
            //    new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            //    new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            //    new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            //    new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            //    new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            //    new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            //    new Enrollment{StudentID=3,CourseID=1050},
            //    new Enrollment{StudentID=4,CourseID=1050},
            //    new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            //    new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            //    new Enrollment{StudentID=6,CourseID=1045},
            //    new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            //};

            //context.Enrollments.AddRange(enrollments);
            //context.SaveChanges();
        }
    }
}
