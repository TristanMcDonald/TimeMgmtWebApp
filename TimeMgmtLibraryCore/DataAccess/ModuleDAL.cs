using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeMgmtLibraryCore.Models;

namespace TimeMgmtLibraryCore.DataAccess
{
    public class ModuleDAL
    {
        private ApplicationDbContext _context;

        public ModuleDAL()
        {
            _context = new ApplicationDbContext();
        }


        //Creating an object of this class to use the database (_context)
        //public ModuleDAL mDAL = new ModuleDAL();

        //Method to be called when the user clicks the Add Module button to add a new module to their semester.
        //This method will retrieve the values entered by the user and will assign them to the relevant properties in the relevant classes (Andrew Troelsen and Philip Japikse, 2017).
        public void AddNewModule(ModuleModel moduleObj)
        {
            //Calculating the number of self-study hours per week for the module being added.
            moduleObj.SelfStudyHrsPerWeek = Calculation.calcSelfStudyHrs(moduleObj.NoOfCredits, SemesterDAL.getDbNumOfWeeks(), moduleObj.ClassHrsPerWeek);

            //Creating an object of this class to use the database (_context)
            UserDAL uDAL = new UserDAL();

            //Creating an object of the UserModule class to access the properties.
            UserModule userModule = new UserModule();
            userModule.Username = uDAL.GetLoggedInUser();
            userModule.ModuleCode = moduleObj.ModuleCode;
            //Calculating the number of self-study hours per week for this users module.
            userModule.SelfStudyHrsLeft = Calculation.calcSelfStudyHrs(moduleObj.NoOfCredits, SemesterDAL.getDbNumOfWeeks(), moduleObj.ClassHrsPerWeek);
            userModule.ReminderDate = null;

            //Adding the relationship between a specific user and their modules to the bridging entity (UserModules) table in the database.
            //This will ensure that every user will only see their own data and never that of others.
            _context.UserModules.Add(userModule);

            //Getting the result of the CheckIfModuleExists method.
            bool ModuleExists = CheckIfModuleExists(moduleObj.ModuleCode);

            //Checking if the module that is being entered exists in the database and if not the module will be added.
            if (ModuleExists.Equals(false))
            {
                //Adding the module to the database Modules(Andrew Troelsen and Philip Japikse, 2017).
                _context.Modules.Add(moduleObj);
            }

            //Saving the changes made to the database.
            _context.SaveChanges();
        }

        //Method to check if a module already exists in the database.
        public bool CheckIfModuleExists(string moduleCode)
        {
            bool moduleExists = false;

            //Checking if the moduleCode which the user has entered exists in the database (Lujan, 2016) & (Andrew Troelsen and Philip Japikse, 2017).
            foreach (var module in _context.Modules)
            {
                moduleExists = module.ModuleCode.Equals(moduleCode);
                //If the module is found exit the foreach loop (Lujan, 2016).
                if (moduleExists == true)
                {
                    break;
                }
            }
            return moduleExists;
        }


        //Fetching the modules assigned to the user which is logged in.
        public IEnumerable<ModuleModel> GetUserModules()
        {
            //Creating an object of the UserModule class to access the Modules and WeekModules Lists.
            UserModule userModuleObj = new UserModule();

            //Creating an object of the UserDAL class to access the User logged in.
            UserDAL uDAL = new UserDAL();

            //initializing the usersModulesList.
            List<ModuleModel> usersModules = new List<ModuleModel>();

            //Getting the logged in user to use in the below Linq query.
            string loggedInUser = uDAL.GetLoggedInUser();

            foreach (var UserModule in _context.UserModules)
            {
                usersModules = new List<ModuleModel>
                    (from um in _context.UserModules
                     join u in _context.Users
                     on um.Username equals u.Username
                     join m in _context.Modules
                     on um.ModuleCode equals m.ModuleCode
                     where um.Username.Equals(loggedInUser)
                     select um.Module
                    );
            }
            return usersModules;
        }
    }
}
