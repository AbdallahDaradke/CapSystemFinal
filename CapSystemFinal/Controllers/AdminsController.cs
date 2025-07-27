using System.Security.Claims;
using CapSystemFinal.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CapSystemFinal.Controllers
{
    public class AdminsController : Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public AdminsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DeansDelete(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var dean = _context.Deans.FirstOrDefault(d => d.UserId == userId);

            return View();
        }
    }
}
