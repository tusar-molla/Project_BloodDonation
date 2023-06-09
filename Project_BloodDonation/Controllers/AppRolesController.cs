using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Project_BloodDonation.Controllers
{
    [Authorize (Roles = "Admin")]
    public class AppRolesController : Controller
    {
        public readonly RoleManager<IdentityRole> _roleManager;

        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create() { 
        
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model) {

            // Avoid Duplicate Role
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult()) { 
            
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
    }
}
