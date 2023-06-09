using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_BloodDonation.Data;

namespace Project_BloodDonation.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context) {

            _context = context;
        }
        public IActionResult Dashboard()
        {
            var applicationDbContext = _context.Members
                .Include(m => m.Area)
                .Include(m => m.Bloodgroup)
                .Include(m => m.Country)
                .Include(m => m.District)
                .Include(m => m.Division)
                .Include(m => m.Thana);
            return View(applicationDbContext.ToList());

        }
    }
}
