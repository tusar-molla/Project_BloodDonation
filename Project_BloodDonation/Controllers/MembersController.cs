using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_BloodDonation.Data;
using Project_BloodDonation.Models;

namespace Project_BloodDonation.Controllers
{
   
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Members
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Members.Include(m => m.Area).Include(m => m.Bloodgroup).Include(m => m.Country).Include(m => m.District).Include(m => m.Division).Include(m => m.Thana);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Members/Details/5
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Area)
                .Include(m => m.Bloodgroup)
                .Include(m => m.Country)
                .Include(m => m.District)
                .Include(m => m.Division)
                .Include(m => m.Thana)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        [Authorize(Roles = "Admin,User")]
        public IActionResult Create()
        {
            try
            {

                //ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id");
                ViewData["BloodgroupId"] = new SelectList(_context.Bloodgroups, "Id", "Id");
                ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id");
                //ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "Id");
                //ViewData["DivisionId"] = new SelectList(_context.Divisions, "Id", "Id");
                //ViewData["ThanaId"] = new SelectList(_context.Thanas, "Id", "Id");
            }

            catch (Exception ex) {

                ModelState.AddModelError("", ex.Message);
            }
            return View(new Member { Email = User.Identity.Name});
        }

        public JsonResult GetDivisionbyCountry(int countryid) { 
        
            var data = _context.Divisions.Where(d => d.CountryId.Equals(countryid)).OrderBy(d=> d.Name).ToList();
            return Json(data);
        }

        public JsonResult GetDistrictbyDivision(int divid) {

            var data = _context.Districts.Where(d => d.DivisionId.Equals(divid)).OrderBy(d => d.Name).ToList();
            return Json(data);
        }

        public JsonResult GetThanabyDistrict(int distid)
        {

            var data = _context.Thanas.Where(d => d.DistricId.Equals(distid)).OrderBy(d => d.Name).ToList();
            return Json(data);
        }

        public JsonResult GetAreabyThana(int thnaid) { 
        
            var data = _context.Areas.Where(a => a.ThanaId.Equals(thnaid)).OrderBy(a => a.Name).ToList();
            return Json(data);
        }



        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Contact,Email,RegistrationDate,UserName,Password,BloodgroupId,AreaId,ThanaId,DistrictId,DivisionId,CountryId")] Member member)
        {
            try {

                if (ModelState.IsValid)
                {
                    _context.Add(member);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        return Redirect("~/Profile/MyProfile");
                    };
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                
            }
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id", member.AreaId);
            ViewData["BloodgroupId"] = new SelectList(_context.Bloodgroups, "Id", "Id", member.BloodgroupId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", member.CountryId);
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "Id", member.DistrictId);
            ViewData["DivisionId"] = new SelectList(_context.Divisions, "Id", "Id", member.DivisionId);
            ViewData["ThanaId"] = new SelectList(_context.Thanas, "Id", "Id", member.ThanaId);
            return View(member);
        }

        // GET: Members/Edit/5
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id", member.AreaId);
            ViewData["BloodgroupId"] = new SelectList(_context.Bloodgroups, "Id", "Id", member.BloodgroupId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", member.CountryId);
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "Id", member.DistrictId);
            ViewData["DivisionId"] = new SelectList(_context.Divisions, "Id", "Id", member.DivisionId);
            ViewData["ThanaId"] = new SelectList(_context.Thanas, "Id", "Id", member.ThanaId);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Contact,Email,RegistrationDate,UserName,Password,BloodgroupId,AreaId,ThanaId,DistrictId,DivisionId,CountryId")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
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
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id", member.AreaId);
            ViewData["BloodgroupId"] = new SelectList(_context.Bloodgroups, "Id", "Id", member.BloodgroupId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", member.CountryId);
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "Id", member.DistrictId);
            ViewData["DivisionId"] = new SelectList(_context.Divisions, "Id", "Id", member.DivisionId);
            ViewData["ThanaId"] = new SelectList(_context.Thanas, "Id", "Id", member.ThanaId);
            return View(member);
        }

        // GET: Members/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Area)
                .Include(m => m.Bloodgroup)
                .Include(m => m.Country)
                .Include(m => m.District)
                .Include(m => m.Division)
                .Include(m => m.Thana)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Members == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Members'  is null.");
            }
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
          return (_context.Members?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
