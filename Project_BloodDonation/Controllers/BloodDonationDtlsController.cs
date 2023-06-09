using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_BloodDonation.Data;
using Project_BloodDonation.Models;

namespace Project_BloodDonation.Controllers
{
    public class BloodDonationDtlsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BloodDonationDtlsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BloodDonationDtls
        public async Task<IActionResult> Index()
        {
              return _context.BloodDonationDtls != null ? 
                          View(await _context.BloodDonationDtls.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.BloodDonationDtls'  is null.");
        }

        // GET: BloodDonationDtls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BloodDonationDtls == null)
            {
                return NotFound();
            }

            var bloodDonationDtls = await _context.BloodDonationDtls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bloodDonationDtls == null)
            {
                return NotFound();
            }

            return View(bloodDonationDtls);
        }

        // GET: BloodDonationDtls/Create
        public IActionResult Create()
        {
            ViewData["MemberID"] = new SelectList(_context.Members.OrderBy(o => o.Name), "Id", "Name");
            return View();
        }

        // POST: BloodDonationDtls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DonarId,RecieverId,DonationDate,DonateQty,Donateplace")] BloodDonationDtls bloodDonationDtls)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bloodDonationDtls);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bloodDonationDtls);
        }

        // GET: BloodDonationDtls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BloodDonationDtls == null)
            {
                return NotFound();
            }

            var bloodDonationDtls = await _context.BloodDonationDtls.FindAsync(id);
            if (bloodDonationDtls == null)
            {
                return NotFound();
            }
            return View(bloodDonationDtls);
        }

        // POST: BloodDonationDtls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DonarId,RecieverId,DonationDate,DonateQty,Donateplace")] BloodDonationDtls bloodDonationDtls)
        {
            if (id != bloodDonationDtls.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloodDonationDtls);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BloodDonationDtlsExists(bloodDonationDtls.Id))
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
            return View(bloodDonationDtls);
        }

        // GET: BloodDonationDtls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BloodDonationDtls == null)
            {
                return NotFound();
            }

            var bloodDonationDtls = await _context.BloodDonationDtls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bloodDonationDtls == null)
            {
                return NotFound();
            }

            return View(bloodDonationDtls);
        }

        // POST: BloodDonationDtls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BloodDonationDtls == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BloodDonationDtls'  is null.");
            }
            var bloodDonationDtls = await _context.BloodDonationDtls.FindAsync(id);
            if (bloodDonationDtls != null)
            {
                _context.BloodDonationDtls.Remove(bloodDonationDtls);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BloodDonationDtlsExists(int id)
        {
          return (_context.BloodDonationDtls?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
