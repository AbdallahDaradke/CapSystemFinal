using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapSystemFinal.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        //public string UserId { get; set; } // This stores the Identity GUID
        public string? Name { get; set; }
        public string? AcademicYear { get; set; }
        public string? WarningCount { get; set; }

        // Navigation property
        public virtual ICollection<Complaint> Complaints { get; set; }






        //public virtual List<Complaint> complaint { get; set; }




    }
}
