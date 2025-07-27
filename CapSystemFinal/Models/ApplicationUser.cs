using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CapSystemFinal.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public string Name { get; set; }

        public string? streetAddress { get; set; }

        public string? city { get; set; }
        public string? state { get; set; }
        public string? postalCode { get; set; }

        //enter role

    }
}
