using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeMgmtLibraryCore.DataAccess;
using TimeMgmtLibraryCore.Models;

namespace TimeMgmt.Controllers
{
    public class ModuleController : Controller
    {
        //GET: ModuleDAL
        //Creating an object of the ModuleDAL
        public ModuleDAL moduleDAL = new ModuleDAL();

        public ViewResult Index()
        {
            //Get all User Modules
            var moduleList = new List<ModuleModel>();
            //Calling the DAL method which parses a list with all the users modules.
            moduleList = moduleDAL.GetUserModules().ToList();
            //Return the list of modules for the logged in user to the Modules view.
            return View(moduleList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind] ModuleModel moduleObj)
        {
            if (ModelState.IsValid)
            {
                //Calling the DAL method to add a new module for the user.
                moduleDAL.AddNewModule(moduleObj);
                //once the module is added redirect to the Index view.
                return RedirectToAction("Index");
            }
            return View(moduleObj);
        }

    }
}
