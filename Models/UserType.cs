using System.ComponentModel.DataAnnotations;

namespace CapSystemFinal.Models
{
    public class UserType
    {


        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }

        //public virtual List<User> User { get; set; }
    }
}
