using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapSystemFinal.Data;
using CapSystemFinal.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
namespace CapSystemFinal.Controllers
{
    [Authorize(Roles = $"{SD.Role_Staff},{SD.Role_Dean}")]//Adding this will ensure that no user can access to page if they have the url
    public class ComplaintsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public ComplaintsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        public IActionResult Dashboard()
        {
            var total = _context.Complaints.Count();
            var pending = _context.Complaints.Count(c => c.complaintStatus.status == "Pending");
            var resolved = _context.Complaints.Count(c => c.complaintStatus.status == "Resolved");
            var inProgress = _context.Complaints.Count(c => c.complaintStatus.status == "In Progress");
            var closed = _context.Complaints.Count(c => c.complaintStatus.status == "Closed");

            var stats = new
            {
                Total = total,
                Pending = pending,
                Resolved = resolved,
                InProgress = inProgress,
                Closed = closed
            };

            return View(stats);
        }




        // GET: Complaints
        public IActionResult Index()
        {
            //var user = await _userManager.GetUserAsync(User);
            // var userId = user?.Id;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //var dean =  _context.Deans.FirstOrDefaultAsync(d => d.UserId == userId);



            List<Complaint> ListObjComplaint = _context.Complaints
        .Include(c => c.complaintStatus)
        .Include(c => c.transformationDirection)
        .Include(c => c.complaintType)
        .ToList();
            


            // Pass to view
            ViewData["UserId"] = userId;
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //ViewData["UserId"] = userId;
             return View(ListObjComplaint);

        }

        //// GET: Complaints/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ComplaintStatusId"] = _context.ComplaintStatus.Find(id);

        //    var complaint = await _context.Complaints
        //        .Include(c => c.complaintStatus)
        //        .Include(c => c.complaintType)
        //        .Include(c => c.transformationDirection)
        //        .FirstOrDefaultAsync(m => m.ComplaintId == id);
        //    if (complaint == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(complaint);
        //}

        // GET: Complaints/Details/5
        public async Task<IActionResult> Details(int? id)
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

            // Now just assign the values you want to display:
            ViewData["ComplaintStatus"] = complaint.complaintStatus?.status;
            ViewData["ComplaintType"] = complaint.complaintType?.Description;
            ViewData["TransformationDirection"] = complaint.transformationDirection?.Description;

            return View(complaint);
        }
//🖥 In the View(e.g

        // GET: Complaints/Create
        public IActionResult Create()
        {
            ViewData["ComplaintStatus"] = "Pending";


            ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewData["CurrentDate"] = DateTime.Now.ToString("yyyy-MM-dd");

            // Prepare dropdown lists
            ViewData["CompTypeId"] = new SelectList(_context.ComplaintType, "Id", "Description");
            ViewData["ComplaintStatusId"] = new SelectList(_context.ComplaintStatus, "Id", "status");
            ViewData["TransformationDirectionId"] = new SelectList(_context.TransformationDirection, "Id", "Description");


            return View();
        }



        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Complaint complaint)
        {
          
                if (ModelState.IsValid)
                {

                // 🧠 Logic to get Dean by selected TransformationDirection
                var selectedDirection = await _context.TransformationDirection
                    .FirstOrDefaultAsync(td => td.Id == complaint.TransformationDirectionId);

                if (selectedDirection != null)
                {
                    // Match by description (assuming Dean.Name == Direction.Description)
                    var dean = await _context.Deans
                        .FirstOrDefaultAsync(d => d.Name == selectedDirection.Description);

                    if (dean != null)
                    {
                        complaint.DeanId = dean.DeanId;
                    }
                }



                _context.Add(complaint);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }

                return View(complaint);
            

           
        }



        // GET: Complaints/Edit/5
        public  IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintFromDb =  _context.Complaints.FirstOrDefault(u=>u.ComplaintId == id);
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

            if (ModelState.IsValid)
            {
                // 1. Get the existing complaint from database
                var complaintFromDb = _context.Complaints.Find(obj.ComplaintId);

                if(complaintFromDb == null)
                {
                    return NotFound();
                }

                
                // string Oldsubject = complaintFromDb.subject;
                //string Olddescription = complaintFromDb.description;
                //int OldComplaintStatusId = complaintFromDb.ComplaintStatusId;
                //int OldCompTypeId = complaintFromDb.CompTypeId;
                //int OldTransformationDirectionId = complaintFromDb.TransformationDirectionId;
                // 2. Update only the properties that should change
                complaintFromDb.subject = obj.subject;
                complaintFromDb.description = obj.description;
                complaintFromDb.ComplaintStatusId = obj.ComplaintStatusId;
                complaintFromDb.CompTypeId = obj.CompTypeId;
                complaintFromDb.TransformationDirectionId = obj.TransformationDirectionId;




                // 🧠 Logic to get Dean by selected TransformationDirection
                var selectedDirection = await _context.TransformationDirection
                    .FirstOrDefaultAsync(td => td.Id == complaintFromDb.TransformationDirectionId);

                complaintFromDb.DeanId = selectedDirection.Id;


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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMultiple(int[] selectedIds)
        {


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Pass to view
            ViewData["UserId"] = userId;

            if (selectedIds == null || selectedIds.Length == 0)
            {
                TempData["ErrorMessage"] = "No records selected for deletion.";
                return RedirectToAction(nameof(Index));
            }
            // Verify records exist and belong to current user
            var recordsToDelete =  _context.Complaints
                .Where(c => selectedIds.Contains(c.ComplaintId))
                .ToList();

            _context.Complaints.RemoveRange(recordsToDelete);
             _context.SaveChanges();

            TempData["SuccessMessage"] = $"Successfully deleted {recordsToDelete.Count} records.";

            return RedirectToAction("Index");
            
        }



        private bool ComplaintExists(int id)
        {
            return _context.Complaints.Any(e => e.ComplaintId == id);
        }
    }
}
