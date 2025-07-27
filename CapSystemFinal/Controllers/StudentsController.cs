//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using CapSystemFinal.Data;
//using CapSystemFinal.Models;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Authorization;
//using CapSystemFinal.Migrations;
//using Student = CapSystemFinal.Models.Student;

//namespace CapSystemFinal.Controllers
//{
//    public class StudentsController : Controller
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly UserManager<IdentityUser> _userManager;

//        public StudentsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//            _userManager = userManager;
//        }

//        // GET: Students
//        public async Task<IActionResult> Index()
//        {
//            ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            return View(await _context.students.ToListAsync());
//        }

//        // GET: Students/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var student = await _context.students
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (student == null)
//            {
//                return NotFound();
//            }

//            return View(student);
//        }

//        // GET: Students/Create
//        public IActionResult Create()
//        {
//            //var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            return View();
//        }

//        // POST: Students/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public async Task<IActionResult> Create([Bind("Id,Name,AcademicYear,WarningCount")] Student student)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        _context.Add(student);
//        //        await _context.SaveChangesAsync();
//        //        return RedirectToAction(nameof(Index));
//        //    }
//        //    return View(student);
//        //}
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize] // Ensure only authenticated users can create students
//        public async Task<IActionResult> Create([Bind("Name,AcademicYear,WarningCount")] Student model)
//        {
//            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            if (ModelState.IsValid)
//            {
                
//                // Check if student already exists for this user
//                if (await _context.students.AnyAsync(s => s.UserId == userId))
//                {
//                    ModelState.AddModelError("", "Student profile already exists");
//                    return View(model);
//                }



//                // Set the UserId from Identity
//                model.UserId = userId;

//                // Set default warning count if not provided
//                model.WarningCount = model.WarningCount;
//                _context.Add(model);
//                await _context.SaveChangesAsync();

//                return RedirectToAction(nameof(Index));
//            }
//            return View(model);
//        }
//        // GET: Students/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var student = await _context.students.FindAsync(id);
//            if (student == null)
//            {
//                return NotFound();
//            }
//            return View(student);
//        }

//        // POST: Students/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AcademicYear,WarningCount")] Student student)
//        {
//            if (id != student.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(student);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!StudentExists(student.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(student);
//        }

//        // GET: Students/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var student = await _context.students
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (student == null)
//            {
//                return NotFound();
//            }

//            return View(student);
//        }

//        // POST: Students/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var student = await _context.students.FindAsync(id);
//            if (student != null)
//            {
//                _context.students.Remove(student);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool StudentExists(int id)
//        {
//            return _context.students.Any(e => e.Id == id);
//        }
//    }
//}
