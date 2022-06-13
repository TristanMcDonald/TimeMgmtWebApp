using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeMgmtLibraryCore.DataAccess;
using TimeMgmtLibraryCore.Models;

namespace TimeMgmtWebApp.Controllers
{
    public class WeekController : Controller
    {
        //GET: WeekDAL
        //Creating an object of the ModuleDAL
        public WeekDAL weekDAL = new WeekDAL();
        public ViewResult Index()
        {
            //Get all User Modules
            var weekModuleList = new List<UserModule>();
            weekModuleList = weekDAL.GetUserWeekModules().ToList();
            //Return the list of modules for the logged in user to the Modules view.
            return View(weekModuleList);
        }

        [HttpGet]
        [Route("Week/Record/{moduleCode}")]
        public IActionResult Record(string moduleCode)
        {
            if (moduleCode == null)
            {
                return NotFound();
            }

            UserModule um = weekDAL.GetUserModuleByModuleCode(moduleCode);

            if (um == null)
            {
                return NotFound();
            }

            return View(um);
        }

        [Route("Week/Record/{moduleCode}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Record(string moduleCode, [Bind] UserModule userModuleObj)
        {
            if (moduleCode == null)
            {
                return NotFound();
            }
            //Calling the DAL method to check if the module exists.
            if (weekDAL.SearchForModule(userModuleObj).Equals(true))
            {
                //Calling the DAL method to update the self study hours for the module specified.
                weekDAL.updateSelfStudyHrs(userModuleObj);
                return RedirectToAction("Index");
            }
            //if the module does not exist display the error view.
            else if (weekDAL.SearchForModule(userModuleObj).Equals(false))
            {
                return RedirectToAction("ModuleCodeError");
            }

            return View(userModuleObj);
        }

        [HttpGet]
        [Route("Week/Reminder/{moduleCode}")]
        public IActionResult Reminder(string moduleCode)
        {
            if (moduleCode == null)
            {
                return NotFound();
            }
            UserModule um = weekDAL.GetUserModuleByModuleCode(moduleCode);

            if (um == null)
            {
                return NotFound();
            }

            return View(um);
        }

        [Route("Week/Reminder/{moduleCode}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reminder(string moduleCode, [Bind] UserModule userModuleObj)
        {
            if (moduleCode == null)
            {
                return NotFound();
            }

            if (weekDAL.SearchForModule(userModuleObj).Equals(true))
            {
                weekDAL.SetModuleReminder(userModuleObj);
                return RedirectToAction("Index");
            }
            else if (weekDAL.SearchForModule(userModuleObj).Equals(false))
            {
                return RedirectToAction("ModuleCodeError");
            }

            return View(userModuleObj);
        }

        public IActionResult ModuleCodeError()
        {
            return View();
        }

    }
}
