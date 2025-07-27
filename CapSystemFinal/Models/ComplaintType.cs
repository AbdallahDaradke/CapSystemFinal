using System.ComponentModel.DataAnnotations;

namespace CapSystemFinal.Models
{
    public class ComplaintType
    {

        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }

        //public virtual List<Complaint> Complaint { get; set; }
    }
}
