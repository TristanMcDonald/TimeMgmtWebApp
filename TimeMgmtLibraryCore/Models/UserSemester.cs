using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMgmtLibraryCore.Models
{
    public class UserSemester
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Username")]
        public UserModel User { get; set; }
        [Required]
        public string Username { get; set; }

        [ForeignKey("SemesterId")]
        public SemesterModel Semester { get; set; }
        public int SemesterId { get; set; }
    }
}
