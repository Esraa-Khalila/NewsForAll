using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsForAll.Data;
using NewsForAll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsForAll.Controllers
{
    [Authorize(Roles = "Admin1")]
    public class TeamMemController : Controller
    {
        private readonly ApplicationDbContext db;
        public TeamMemController(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IActionResult ATeamMem()
        {
            var obj = db.TeamMems.ToList();
            return View(obj);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TeamMem obj)
        {
            if (ModelState.IsValid)
            {
                db.TeamMems.Add(obj);
                db.SaveChanges();
                return RedirectToAction("ATeamMem", "TeamMem");
            }
            return View(obj);
        }
        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = db.TeamMems.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }
        [HttpPost]
        public IActionResult Update(TeamMem obj)
        {
            if (ModelState.IsValid)
            {
                db.TeamMems.Update(obj);
                db.SaveChanges();
                return RedirectToAction("ATeamMem", "TeamMem");
            }
            return View(obj);
            
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = db.TeamMems.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }
        [HttpPost]
        public IActionResult Delete(TeamMem obj)
        {
            if (ModelState.IsValid)
            {
                db.TeamMems.Remove(obj);
                db.SaveChanges();
                return RedirectToAction("ATeamMem", "TeamMem");
            }
            return View(obj);
        }
    }
}
