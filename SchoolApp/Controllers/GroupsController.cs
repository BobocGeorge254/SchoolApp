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
        public IActionResult New()
        {
            CreateGroup createGroup = new()
            {
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

        public IActionResult ShowGroup(int id)
        {
            ViewBag.GroupId = id;
            Group group = db.Groups.Find(id);
            var messages = db.Messages.Where(m => m.GroupId == id);
            messages.Select(m => m.User).Load();
            ViewBag.Messages = messages;
            return View(group);
        }




        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Group group = db.Groups.Find(id);
            return View(group);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, Group requestGroup)
        {
            Group group = db.Groups.Find(id);
            if (ModelState.IsValid)
            {
                group.GroupName = requestGroup.GroupName;
                db.SaveChanges();
                TempData["message"] = "Grupul a fost modificat!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(requestGroup);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Group group = db.Groups.Find(id);
            if (group != null)
                db.Groups.Remove(group);
            TempData["message"] = "Grupul a fost sters";
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}