using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CapSystemFinal.Models
{
    public class Dean
    {
        [Key]
        public int DeanId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; } // FK to IdentityUser.Id

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        // Optional: link to complaints
        public ICollection<Complaint> Complaints { get; set; }
    }

}
