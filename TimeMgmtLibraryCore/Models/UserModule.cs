using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMgmtLibraryCore.Models
{
    public class UserModule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Username")]
        public UserModel User { get; set; }
        [Required]
        public string Username { get; set; }

        [ForeignKey("ModuleCode")]
        public ModuleModel Module { get; set; }
        [Required]
        public string ModuleCode { get; set; }

        public double SelfStudyHrsLeft { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReminderDate { get; set; }
        public double NumOfHrsStudied { get; set; }
    }
}
