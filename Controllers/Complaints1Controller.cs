using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapSystemFinal.Data;
using CapSystemFinal.Models;

namespace CapSystemFinal.Controllers
{
    public class Complaints1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Complaints1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Complaints1
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Complaints.Include(c => c.complaintStatus).Include(c => c.complaintType).Include(c => c.Students).Include(c => c.transformationDirection);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Complaints1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.complaintStatus)
                .Include(c => c.complaintType)
                .Include(c => c.Students)
                .Include(c => c.transformationDirection)
                .FirstOrDefaultAsync(m => m.id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // GET: Complaints1/Create
        public IActionResult Create()
        {
            ViewData["ComplaintStatusId"] = new SelectList(_context.Set<ComplaintStatus>(), "id", "id");
            ViewData["CompTypeId"] = new SelectList(_context.Set<ComplaintType>(), "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.students, "Id", "Id");
            ViewData["TransformationDirectionId"] = new SelectList(_context.Set<TransformationDirection>(), "Id", "Id");
            return View();
        }

        // POST: Complaints1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,subject,description,date,ComplaintStatusId,StudentId,CompTypeId,TransformationDirectionId")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComplaintStatusId"] = new SelectList(_context.Set<ComplaintStatus>(), "id", "id", complaint.ComplaintStatusId);
            ViewData["CompTypeId"] = new SelectList(_context.Set<ComplaintType>(), "Id", "Id", complaint.CompTypeId);
            ViewData["StudentId"] = new SelectList(_context.students, "Id", "Id", complaint.StudentId);
            ViewData["TransformationDirectionId"] = new SelectList(_context.Set<TransformationDirection>(), "Id", "Id", complaint.TransformationDirectionId);
            return View(complaint);
        }

        // GET: Complaints1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }
            ViewData["ComplaintStatusId"] = new SelectList(_context.Set<ComplaintStatus>(), "id", "id", complaint.ComplaintStatusId);
            ViewData["CompTypeId"] = new SelectList(_context.Set<ComplaintType>(), "Id", "Id", complaint.CompTypeId);
            ViewData["StudentId"] = new SelectList(_context.students, "Id", "Id", complaint.StudentId);
            ViewData["TransformationDirectionId"] = new SelectList(_context.Set<TransformationDirection>(), "Id", "Id", complaint.TransformationDirectionId);
            return View(complaint);
        }

        // POST: Complaints1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,subject,description,date,ComplaintStatusId,StudentId,CompTypeId,TransformationDirectionId")] Complaint complaint)
        {
            if (id != complaint.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComplaintStatusId"] = new SelectList(_context.Set<ComplaintStatus>(), "id", "id", complaint.ComplaintStatusId);
            ViewData["CompTypeId"] = new SelectList(_context.Set<ComplaintType>(), "Id", "Id", complaint.CompTypeId);
            ViewData["StudentId"] = new SelectList(_context.students, "Id", "Id", complaint.StudentId);
            ViewData["TransformationDirectionId"] = new SelectList(_context.Set<TransformationDirection>(), "Id", "Id", complaint.TransformationDirectionId);
            return View(complaint);
        }

        // GET: Complaints1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.complaintStatus)
                .Include(c => c.complaintType)
                .Include(c => c.Students)
                .Include(c => c.transformationDirection)
                .FirstOrDefaultAsync(m => m.id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // POST: Complaints1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintExists(int id)
        {
            return _context.Complaints.Any(e => e.id == id);
        }
    }
}
