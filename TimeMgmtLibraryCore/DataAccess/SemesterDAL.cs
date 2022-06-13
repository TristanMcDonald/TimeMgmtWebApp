using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeMgmtLibraryCore.Models;

namespace TimeMgmtLibraryCore.DataAccess
{
    public class SemesterDAL
    {
        private ApplicationDbContext _context;

        public SemesterDAL()
        {
            _context = new ApplicationDbContext();
        }

        //Initializing SemesterModel and SemesterViewModel class objects
        public static SemesterModel semester = new SemesterModel();
        public static SemesterDAL sDAL = new SemesterDAL();
        //Creating an object of the UserDALclass to use the User object created there.
        public static UserDAL uDAL = new UserDAL();

        //Method to be called when the user clicks the new semester button to start a new semester (Andrew Troelsen and Philip Japikse, 2017).
        //This method will retrieve the values entered by the user and will assign them to the relevant properties in the relevant classes (Andrew Troelsen and Philip Japikse, 2017).
        public void CreateNewSemester(SemesterModel semesterObj)
        {
            //Removing all contents of the Modules and week modules in the database as a new semester is being created when this method is called (Andrew Troelsen and Philip Japikse, 2017).

            //Removing all rows from the UserModules table where the username is equal to the username of the user signed in.
            string username = uDAL.GetLoggedInUser();

            var rows = from o in sDAL._context.UserModules
                       where o.Username.Equals(username)
                       select o;
            foreach (var row in rows)
            {
                sDAL._context.UserModules.Remove(row);
            }

            //Removing all rows from the UserSemetser table where the username is equal to the username of the user signed in.
            var rows2 = from o in sDAL._context.UserSemester
                        where o.Username.Equals(username)
                        select o;
            foreach (var row2 in rows2)
            {
                sDAL._context.UserSemester.Remove(row2);
            }
            //Save changes made to the database.
            sDAL._context.SaveChanges();

            //Calculating the end date of the semetser with the provided values.
            semesterObj.EndDate = Calculation.endDateCalc(semesterObj.StartDate, semesterObj.NumOfWeeks);

            //Adding the semester to the semester table in the database(Andrew Troelsen and Philip Japikse, 2017).
            sDAL._context.Semester.Add(semesterObj);

            //Generating an object of the UserSemester class to access its properties
            UserSemester us = new UserSemester();
            us.SemesterId = semesterObj.Id;
            us.Username = username;

            //Adding the relationship between a specific user and their semetser to the bridging entity (UserSemester) table in the database.
            //This will ensure that every user will only see their own data and never that of others.
            sDAL._context.UserSemester.Add(us);

            //Saving changes made to the database.
            sDAL._context.SaveChanges();
        }

        public SemesterModel GetUserSemester()
        {
            SemesterModel semester = new SemesterModel();

            //Getting the logged in user to use in the below Linq query.
            string loggedInUser = uDAL.GetLoggedInUser();

            //var userSemester = sDAL._context.UserSemester.Where(UserSemester => UserSemester.Username == loggedInUser);

            var userSemester = new List<SemesterModel>
                               (from s in _context.Semester
                                join us in _context.UserSemester
                                on s.Id equals us.SemesterId
                                where us.Username.Equals(loggedInUser)
                                select s);

            foreach (var s in userSemester)
            {
                semester.StartDate = s.StartDate;
                semester.NumOfWeeks = s.NumOfWeeks;
                semester.EndDate = s.EndDate;
            }

            return semester;
        }

        //This method returns the start Date for the semester which is stored in the database.
        public static DateTime getDbSemesterStartDate()
        {
            DateTime DbSemesterStartDate = DateTime.MinValue;

            //Getting the logged in users username.
            string username = uDAL.GetLoggedInUser();

            //Linq query to fetch the users semester details.
            var userSemester = from user in sDAL._context.UserSemester
                               where user.Username.Equals(username)
                               select user.Semester;

            foreach (var semester in userSemester)
            {
                DbSemesterStartDate = semester.StartDate;
            }

            return DbSemesterStartDate;
        }
        //This method returns the number of weeks for the semester which is stored in the database.
        public static double getDbNumOfWeeks()
        {
            double DbNumOfWeeks = 0;

            //Getting the logged in users username.
            string username = uDAL.GetLoggedInUser();

            //Linq query to fetch the users semester details.
            var userSemester = from user in sDAL._context.UserSemester
                               where user.Username.Equals(username)
                               select user.Semester;

            foreach (var semester in userSemester)
            {
                DbNumOfWeeks = semester.NumOfWeeks;
            }
            return DbNumOfWeeks;
        }

        //This method returns the End date for the semester which is stored in the database.
        public static DateTime getDbSemesterEndDate()
        {
            DateTime DbSemesterEndDate = DateTime.MaxValue;

            //Getting the logged in users username.
            string username = uDAL.GetLoggedInUser();

            //Linq query to fetch the users semester details.
            var userSemester = from user in sDAL._context.UserSemester
                               where user.Username.Equals(username)
                               select user.Semester;

            foreach (var semester in userSemester)
            {
                DbSemesterEndDate = semester.EndDate;
            }

            return DbSemesterEndDate;
        }
    }
}
