using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeMgmtLibraryCore.Models;

namespace TimeMgmtLibraryCore.DataAccess
{
    public class WeekDAL
    {
        private ApplicationDbContext _context;

        public WeekDAL()
        {
            _context = new ApplicationDbContext();
        }

        //Creating an object of this class to use the database (_context)
        static WeekDAL wDAL = new WeekDAL();
        //Creating an object of the UserViewModel class to access the User logged in.
        static UserDAL uDAL = new UserDAL();

        public bool SearchForModule(UserModule userModuleObj)
        {
            bool found = false;

            //Checking if the module which the user has entered exists in the database (Lujan, 2016) & (Andrew Troelsen and Philip Japikse, 2017).
            foreach (var module in wDAL._context.Modules)
            {
                found = module.ModuleCode.Equals(userModuleObj.ModuleCode);
                //If the module code is found exit the foreach loop (Lujan, 2016).
                if (found == true)
                {
                    break;
                }
            }
            //Return true if a module was found or return false if it was not.
            return found;
        }

        public UserModule GetUserModuleByModuleCode(string moduleCode)
        {
            UserModule userModule = new UserModule();

            //Getting the logged in user to use in the below Linq query.
            string loggedInUser = uDAL.GetLoggedInUser();

            var module = wDAL._context.UserModules.Where(UserModule => UserModule.Username == loggedInUser && UserModule.ModuleCode == moduleCode);

            foreach (var m in module)
            {
                userModule.ModuleCode = m.ModuleCode;
                userModule.SelfStudyHrsLeft = m.SelfStudyHrsLeft;
            }

            return userModule;
        }
        public void updateSelfStudyHrs(UserModule userModuleObj)
        {
            //Getting the logged in user to use in the below Linq query.
            string loggedInUser = uDAL.GetLoggedInUser();

            //LINQ query to fetch the module that the user entered (Wagner, 2017) & (Andrew Troelsen and Philip Japikse, 2017).
            List<UserModule> ModuleQuery = new List<UserModule>
                (
                    from module in wDAL._context.UserModules
                    where module.ModuleCode.Equals(userModuleObj.ModuleCode) && module.Username.Equals(loggedInUser)
                    select module
                );

            foreach (var item in ModuleQuery)
            {
                userModuleObj.Id = item.Id;
                userModuleObj.Username = item.Username;
                userModuleObj.ModuleCode = item.ModuleCode;
                //Calculating the updated number of self-study hours left for this week for this module (Andrew Troelsen and Philip Japikse, 2017).
                userModuleObj.SelfStudyHrsLeft = item.SelfStudyHrsLeft - userModuleObj.NumOfHrsStudied;
                userModuleObj.ReminderDate = item.ReminderDate;
            }

            //Removing the old version of the specified module from the users weekModules in the database (Andrew Troelsen and Philip Japikse, 2017).
            wDAL._context.UserModules.Remove(ModuleQuery.SingleOrDefault());

            //Adding the new version of the module (updated self-study hours) to the weekModules in the database (Andrew Troelsen and Philip Japikse, 2017).
            wDAL._context.UserModules.Add(userModuleObj);

            //Saving the changes to the data in the database.
            wDAL._context.SaveChanges();

        }

        //Fetching the WeekModules assigned to the user which is logged in.
        public IEnumerable<UserModule> GetUserWeekModules()
        {
            //Creating an object of this class to use the database (_context)
            ModuleDAL mDAL = new ModuleDAL();

            //Creating an object of the UserModule class to access the Modules and WeekModules Lists.
            UserModule userModuleObj = new UserModule();

            //initializing the usersModulesList.
            List<UserModule> usersWeekModules = new List<UserModule>();

            //Getting the logged in user to use in the below Linq query.
            string loggedInUser = uDAL.GetLoggedInUser();


            return wDAL._context.UserModules.Where(UserModule => UserModule.Username == loggedInUser);
        }

        //Method which sets the Reminder property of the UserModule.
        public void SetModuleReminder(UserModule userModuleObj)
        {
            //Getting the logged in user to use in the below Linq query.
            string loggedInUser = uDAL.GetLoggedInUser();

            //LINQ query to fetch the module that the user entered (Wagner, 2017) & (Andrew Troelsen and Philip Japikse, 2017).
            List<UserModule> ModuleQuery = new List<UserModule>
                (
                    from module in wDAL._context.UserModules
                    where module.ModuleCode.Equals(userModuleObj.ModuleCode) && module.Username.Equals(loggedInUser)
                    select module
                );

            foreach (var item in ModuleQuery)
            {
                userModuleObj.Id = item.Id;
                userModuleObj.Username = item.Username;
                userModuleObj.ModuleCode = item.ModuleCode;
                userModuleObj.SelfStudyHrsLeft = item.SelfStudyHrsLeft;
                userModuleObj.ReminderDate = userModuleObj.ReminderDate;
            }

            //Removing the old version of the specified module from the users weekModules in the database (Andrew Troelsen and Philip Japikse, 2017).
            wDAL._context.UserModules.Remove(ModuleQuery.SingleOrDefault());

            //Adding the new version of the module (updated self-study hours) to the weekModules in the database (Andrew Troelsen and Philip Japikse, 2017).
            wDAL._context.UserModules.Add(userModuleObj);

            //Saving the changes to the data in the database.
            wDAL._context.SaveChanges();
        }

        //Getting the module which is scheduled for today.
        public static UserModule GetUserModuleReminder()
        {
            UserModule userModule = new UserModule();

            //Getting the logged in user to use in the below Linq query.
            string loggedInUser = uDAL.GetLoggedInUser();

            var module = wDAL._context.UserModules.Where(UserModule => UserModule.Username == loggedInUser && UserModule.ReminderDate == DateTime.Today);

            if (module.Count() > 1)
            {
                userModule.ModuleCode = "Multiple Modules are";
            }
            else if (module.Count() <= 1)
            {
                foreach (var m in module)
                {
                    userModule.ModuleCode = m.ModuleCode;
                    userModule.ReminderDate = m.ReminderDate;
                }
            }

            return userModule;
        }

    }
}
