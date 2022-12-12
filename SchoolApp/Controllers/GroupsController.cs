using SchoolApp.Controllers;
using SchoolApp.Data;
using SchoolApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;

namespace SchoolApp.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public ArticlesController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }

        // Se afiseaza lista tuturor articolelor impreuna cu categoria 
        // din care fac parte dar
        // Pentru fiecare articol se afiseaza si userul care a postat articolul respectiv
        // HttpGet implicit
        [Authorize(Roles = "User,Moderator,Admin")]
        [HttpPost]
        public IActionResult New(Group group)
        {
           // group.GroupName = ?;
           // group.CategoryId = ?;


            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                db.SaveChanges();
                TempData["message"] = "Grupul a fost creat";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Moderator,Admin")]
        public ActionResult Delete(int id)
        {
            Group group = db.Groups.Include("Comments")
                                         .Where(group => group.GroupId == id)
                                         .First();

            if (group.GroupId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                db.Groups.Remove(group);
                db.SaveChanges();
                TempData["message"] = "Grupul a fost sters";
                return RedirectToAction("Index");
            }

            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti un articol care nu va apartine";
                return RedirectToAction("Index");
            }
        }

    }
}

