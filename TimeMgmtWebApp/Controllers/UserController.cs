using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeMgmtLibraryCore.DataAccess;
using TimeMgmtLibraryCore.Models;

namespace TimeMgmt.Controllers
{
    public class UserController : Controller
    {
        //Creating an object of the UserDAL
        UserDAL userDAL = new UserDAL();

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind] UserModel userObj)
        {
            if (ModelState.IsValid)
            {
                //Using the DAL to check if the Username exists in the database
                if (userDAL.CheckIfUsernameExists(userObj).Equals(true))
                {
                    //Using the DAL to check if the password that goes with the Username is valid.
                    if (userDAL.CheckUserCredentials(userObj).Equals(true))
                    {
                        //Logging in the User after all checks have been made.
                        userDAL.LoginUser(userObj);
                        //Redirecting to the Semester Index page once signed in.
                        return RedirectToAction("Index","Semester");
                    }
                }
                return RedirectToAction("Login");
            }
            return View(userObj);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind] UserModel userObj)
        {
            if (ModelState.IsValid)
            {
                //Using the DAL to check if the Username exists in the database
                bool found = userDAL.CheckIfUsernameExists(userObj);

                if (found.Equals(false))
                {
                    //Registering the new user using the DAL method.
                    userDAL.RegisterNewUser(userObj);
                    //Redirecting to the Login page once Registered.
                    return RedirectToAction("Login");
                }
                else if(found.Equals(true))
                {
                    return RedirectToAction("UsernameExistsError");
                }
            }
            return View(userObj);
        }
        //Returns the relevant page when the relevant error occurrs.
        public IActionResult UsernameExistsError()
        {
            return View();
        }

    }
}
