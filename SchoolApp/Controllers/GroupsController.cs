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
using SchoolApp.ViewModels;

namespace SchoolApp.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public GroupsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }

        [Authorize]
        [HttpGet]
        [Route("/new-group")]
        public  IActionResult New()
        {
            CreateGroup createGroup = new() {
                group = new Group(),
                categories = db.Categories 
            };
            return View(createGroup);
        }

        
        [Authorize]
        [HttpPost]
        [Route("/new-group")]
        public IActionResult New(CreateGroup createGroup)
        {
            createGroup.categories = db.Categories;
            if (ModelState.IsValid)
            {
                db.Groups.Add(createGroup.group);
                db.SaveChanges();
                TempData["message"] = "Grupul a fost creat";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(createGroup);
            }
        }
        public IActionResult Show()
        {
            IEnumerable<Group> groups = db.Groups;
            return View(groups);
        }
    }
}

