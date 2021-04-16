using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsForAll.Data;
using NewsForAll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsForAll.Controllers
{
    [Authorize(Roles = "Admin1")]
    public class ProprController : Controller
    {
        private readonly ApplicationDbContext db;

        public ProprController(ApplicationDbContext _db)
        {
            db = _db;
        }
        [HttpGet]
        public async Task<IActionResult> AGategories()
        {
            return View(await db.proprs.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var prop = await db.proprs
                .FirstOrDefaultAsync(p => p.Id == id);
            if (prop == null)
            {
                return NotFound();
            }
            return View(prop);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Propr obj)
        {
            if (ModelState.IsValid)
            {
            db.proprs.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
            }
            return View(obj);
            
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id==null || id==0)
            {
                return NotFound();
            }
            var obj = db.proprs.Find(id);
            if (obj==null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Propr obj)
        {
            if (ModelState.IsValid)
            {
                db.proprs.Remove(obj);
                db.SaveChanges();
                return RedirectToAction("AGategories", "Propr");

            }
            return View(obj);
            
        }
        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id==null|| id==0)
            {
                return NotFound();
            }
            var obj = db.proprs.Find(id);
            if (obj==null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Propr obj)
        {
            if (ModelState.IsValid)
            {
                db.proprs.Update(obj);
                db.SaveChanges();
                return RedirectToAction("AGategories", "Propr");

            }
            return View(obj);
        }
        
    }
}
