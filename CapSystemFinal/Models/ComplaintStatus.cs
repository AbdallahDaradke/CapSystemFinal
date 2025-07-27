

using System.ComponentModel.DataAnnotations;

namespace CapSystemFinal.Models
{

   
        public class ComplaintStatus
        {

            [Key]
            public int Id { get; set; }
            public string? status { get; set; }


            //public virtual List<Complaint> complaint { get; set; }
        }
    

}
