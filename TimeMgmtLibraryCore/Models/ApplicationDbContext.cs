using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMgmtLibraryCore.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        //Creating a database table for Users
        public DbSet<UserModel> Users { get; set; }
        //Creating a database table for UserModules
        public DbSet<UserModule> UserModules { get; set; }
        //Creating a database table for Modules
        public DbSet<ModuleModel> Modules { get; set; }
        //Creating a database table for Semesters
        public DbSet<SemesterModel> Semester { get; set; }
        //Creating a database table for UserSemesters
        public DbSet<UserSemester> UserSemester { get; set; }
    }
}
