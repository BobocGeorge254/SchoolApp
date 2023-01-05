using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Data;
using SchoolApp.Models;

namespace SchoolApp.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public MessagesController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }
        public IActionResult New(string id)
        {
            Console.WriteLine("aDOUACHESTIEFHSBAHFBHASBFHSABFHASBKHFBASKHFBSAHKFBSAKHBHKSA: " + id);
            ViewBag.GroupId = id;
            return View();

        }
        [HttpPost]
        public IActionResult New(string id, Message message)
        {
            Console.WriteLine(id);
            Console.WriteLine("Dasdsdasdsadafffwfafwfwa2748712647812648127641286481276" + message.Content);
            if (ModelState.IsValid)
            {
                Message message1 = new Message();
                message1.GroupId = id;
                message1.UserId = _userManager.GetUserId(User);
                message1.Content = message.Content;
                db.Messages.Add(message1);
                return RedirectToAction("Index", "Home");
                
            }
            else
            {
                return View(message);

            }
            
        }
    }
}
