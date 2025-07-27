using System.Drawing.Text;
using CapSystemFinal.Data;
using CapSystemFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;

namespace CapSystemFinal.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context) {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }
        public void Initialize() {


            //migrations if they not applied
            try {
                if (_context.Database.GetPendingMigrations().Count()>0)
                {
                    _context.Database.Migrate();
                }
            }
            catch(Exception ex)
            {
                //log the exception
            }
            //create roles if they are not creatd
            if (!_roleManager.RoleExistsAsync(SD.Role_Student).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Student)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Staff)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Dean)).GetAwaiter().GetResult();
            }



            //if roles are not created, then

            //_userManager.CreateAsync(new ApplicationUser
            // {
            //     UserName = "SamirTartir@gmail.com",
            //     Email = "SamirTartir@gmail.com",
            //     Name = "Samir Tartir",
            //     streetAddress = "Street 1",
            //     city = "City 1",
            //     state = "State 1",
            //     postalCode = "12345",


            //}, "SamirTartir123!").GetAwaiter().GetResult();
            //var user = new ApplicationUser
            //{
            //    UserName = "SamirTartir@gmail.com",
            //    Email = "SamirTartir@gmail.com",
            //    Name = "Samir Tartir",
            //    streetAddress = "Street 1",
            //    city = "City 1",
            //    state = "State 1",
            //    postalCode = "12345"
            //};

            //var result = _userManager.CreateAsync(user, "SamirTartir123!").GetAwaiter().GetResult();

            //if (result.Succeeded)
            //{
            //    _userManager.AddToRoleAsync(user, SD.Role_Dean).GetAwaiter().GetResult();

            //    _context.Deans.Add(new Dean
            //    {
            //        Name = user.Name,
            //        UserId = user.Id
            //    });
            //    _context.SaveChanges();
            //}


           
            return;
        }
    }
}
