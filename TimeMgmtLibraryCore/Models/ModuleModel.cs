using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMgmtLibraryCore.Models
{
    public class ModuleModel
    {
        [Key]
        [Required]
        public string ModuleCode { get; set; }
        [Required]
        public string ModuleName { get; set; }
        [Required]
        public int NoOfCredits { get; set; }
        [Required]
        public double ClassHrsPerWeek { get; set; }
        public double SelfStudyHrsPerWeek { get; set; }
    }
}
