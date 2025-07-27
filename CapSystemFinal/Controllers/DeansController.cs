using System.Security.Claims;
using CapSystemFinal.Data;
using CapSystemFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CapSystemFinal.Controllers
{

    [Authorize(Roles = SD.Role_Dean)]
    public class DeansController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DeansController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        //public IActionResult Index()
        //{
        //    return View();
        //}


        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);




            //var dean = await _context.Deans.FirstOrDefaultAsync(d => d.UserId == userId);

            //Dean dean = new Dean
            //{
            //    Name = "Dean John Smith",
            //    UserId = userId // <-- this links the Dean to the IdentityUser
            //};

            //_context.Deans.Add(dean);
            //await _context.SaveChangesAsync();

            var dean = await _context.Deans.FirstOrDefaultAsync(d => d.UserId == userId);

            if (dean == null)
            {
                // Return a custom view or message
                return Content("Dean profile not found for this user.");
            }
            ViewBag.DeanName = dean.Name;

            //var complaints = await _context.Complaints
            //    .Include(c => c.Dean)
            //    .Where(c => c.DeanId == dean.DeanId)
            //    .ToListAsync();
            var complaints = await _context.Complaints
                .Include(c => c.complaintStatus)
    .Include(c => c.transformationDirection)
    .Include(c => c.complaintType)
    .Where(c => c.DeanId == dean.DeanId)
    .ToListAsync();
            return View(complaints);

        }






        // GET: Complaints/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintFromDb = _context.Complaints.FirstOrDefault(u => u.ComplaintId == id);
            if (complaintFromDb == null)
            {
                return NotFound();
            }
            //ViewData["CurrentDate"] = new SelectList(_context.Set<Complaint>(), "id", "id", complaint.date);
            ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewData["CompTypeId"] = new SelectList(_context.ComplaintType, "Id", "Description");
            ViewData["ComplaintStatusId"] = new SelectList(_context.ComplaintStatus, "Id", "status");
            ViewData["TransformationDirectionId"] = new SelectList(_context.TransformationDirection, "Id", "Description");



            return View(complaintFromDb);
        }

        // POST: Complaints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Complaint obj)
        {

            var userId = _userManager.GetUserId(User);

            //var dean = await _context.Deans.FirstOrDefaultAsync(d => d.UserId == userId);

            //if (dean == null)
            //{
            //    // Return a custom view or message
            //    return Content("Dean profile not found for this user.");
            //}


            if (ModelState.IsValid)
            {
                // 1. Get the existing complaint from database
                var complaintFromDb = _context.Complaints.Find(obj.ComplaintId);

                if (complaintFromDb == null)
                {
                    return NotFound();
                }


               
                complaintFromDb.subject = obj.subject;
                complaintFromDb.description = obj.description;
                complaintFromDb.ComplaintStatusId = obj.ComplaintStatusId;
                complaintFromDb.CompTypeId = obj.CompTypeId;
                complaintFromDb.TransformationDirectionId = obj.TransformationDirectionId;



              

                // 🧠 Logic to get Dean by selected TransformationDirection
                var selectedDirection = await _context.TransformationDirection
                    .FirstOrDefaultAsync(td => td.Id == complaintFromDb.TransformationDirectionId);

                if (selectedDirection != null)
                {
                    // Match by description (assuming Dean.Name == Direction.Description)
                    var dean = await _context.Deans
                        .FirstOrDefaultAsync(d => d.Name == selectedDirection.Description);

                    if (dean != null)
                    {
                        complaintFromDb.DeanId = dean.DeanId;
                    }

                }



                // 3. Explicitly mark as modified (optional but good practice)
                _context.Entry(complaintFromDb).State = EntityState.Modified;

                _context.SaveChanges();



                return RedirectToAction("Index");
                //  try
                // {
                //_context.Attach(complaint);
                //_context.Entry(complaint).State = EntityState.Modified;
                //_context.Update(obj);
                //_context.SaveChangesAsync();
                //_context.SaveChanges();

                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!ComplaintExists(complaint.ComplaintId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                //return RedirectToAction("Index");
            }
            ViewData["ComplaintStatusId"] = new SelectList(_context.Set<ComplaintStatus>(), "id", "id", obj.ComplaintStatusId);
            ViewData["CompTypeId"] = new SelectList(_context.Set<ComplaintType>(), "Id", "Id", obj.CompTypeId);
            ViewData["TransformationDirectionId"] = new SelectList(_context.Set<TransformationDirection>(), "Id", "Id", obj.TransformationDirectionId);
            return View(obj);
        }



        // GET: Complaints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.complaintStatus)
                .Include(c => c.complaintType)
                .Include(c => c.transformationDirection)
                .FirstOrDefaultAsync(m => m.ComplaintId == id);
            if (complaint == null)
            {
                return NotFound();
            }

            ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewData["CompTypeId"] = _context.ComplaintType.Find(complaint.CompTypeId)?.Description;
            ViewData["ComplaintStatusId"] = _context.ComplaintStatus.Find(complaint.ComplaintStatusId)?.status;
            //ViewData["TransformationDirectionId"] = new SelectList(_context.TransformationDirection, "Id", "Description");
            ViewData["TransformationDirectionId"] = _context.TransformationDirection.Find(complaint.TransformationDirectionId)?.Description;
            return View(complaint);
        }

        // POST: Complaints/Delete/5
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



    }
}
