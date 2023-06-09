using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Project_BloodDonation.Data;

namespace Project_BloodDonation.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context) { 
        
            _context = context;
        }
        public IActionResult Myprofile()
        {

            var data = _context.Members.Include(m => m.Country)
                                       .Include(m => m.Division)
                                       .Include(m => m.District)
                                       .Include(m => m.Thana)
                                       .Include(m => m.Area)
                                       .Include(m => m.Bloodgroup)
                                       .Where(m=> m.Email.Equals(User.Identity.Name))
                                       .FirstOrDefault();
                                       


            return View(data);
        }

        public IActionResult DashBoard()
        { 
        
            return View();
        }
    }
}
