using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapSystemFinal.Models
{
    public class Complaint
    {

        [Key]
        public int ComplaintId { get; set; }

        // Optional: Store the Identity GUID directly
        [Required]
        public string? UserId { get; set; }

        [Required]
        public string? subject { get; set; }

        public string? description { get; set; }


        public DateOnly? date { get; set; }

        [DisplayName("Complaint Status")]
        public int ComplaintStatusId { get; set; }

        [ForeignKey("ComplaintStatusId")]
        public ComplaintStatus? complaintStatus { get; set; }

        //public int? StudentId { get; set; }

        //[ForeignKey("StudentId")]
        //public Student? Students { get; set; }


        [DisplayName("Complaint Type")]
        public int CompTypeId { get; set; }
        [ForeignKey("CompTypeId")]
        public ComplaintType? complaintType { get; set; }


        [DisplayName("Transformation Direction")]
        public int TransformationDirectionId { get; set; }
        [ForeignKey("TransformationDirectionId")]
        public TransformationDirection? transformationDirection { get; set; }

        /*-----------------------------------------------------------------------*/
        //public string? CreatedBy { get; set; } // Student ID
        //public string? AssignedTo { get; set; } // Staff ID


        public int? DeanId { get; set; }

        [ForeignKey("DeanId")]
        public Dean? Dean { get; set; }


    }
}
