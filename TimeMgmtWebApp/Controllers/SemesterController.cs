using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeMgmtLibraryCore.DataAccess;
using TimeMgmtLibraryCore.Models;

namespace TimeMgmt.Controllers
{
    public class SemesterController : Controller
    {
        //Creating an object of the SemesterDAL
        SemesterDAL semesterDAL = new SemesterDAL();

        public IActionResult Index()
        {
            //Using the DAL to retrieve the users semester data.
            SemesterModel semester = semesterDAL.GetUserSemester();
            return View(semester);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] SemesterModel semesterObj)
        {
            if (ModelState.IsValid)
            {
                //Calling the DAL method to create a new semester and remove all this semesters data.
                semesterDAL.CreateNewSemester(semesterObj);
                return RedirectToAction("Index");
            }
            return View(semesterObj);
        }

    }
}

