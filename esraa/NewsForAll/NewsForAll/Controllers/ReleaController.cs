using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsForAll.Data;
using NewsForAll.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

    
namespace NewsForAll.Controllers
{
    [Authorize(Roles = "Admin1")]
    public class ReleaController : Controller
    {
        private readonly ApplicationDbContext db;

            public ReleaController(ApplicationDbContext _db)
        {
            db = _db;
        }
        [HttpGet]
        public IActionResult ARelease()
        {
            ViewData["ProprId"] = new SelectList(db.Set<Propr>(), "Id", "Name");



            var obj = db.Releas.ToList();
           

            return View(obj);
        }
        [HttpGet]
        public IActionResult Create( )
        {
            ViewData["ProprId"] = new SelectList(db.Set<Propr>(), "Id", "Name");
            

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Relea obj)
        {


            if (ModelState.IsValid)
            {
                 db.Releas.Add(obj);
               await db.SaveChangesAsync();
                return RedirectToAction("Arelease", "Relea");
            }
            ViewData["ProprId"] = new SelectList(db.Set<Propr>(), "Id", "Name",obj.PropId);
            return View(obj);
        
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id==null||id==0)
            {
                return NotFound();
            }
            var obj = db.Releas.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete (Relea obj)
        {
            if (ModelState.IsValid)
            {
                db.Releas.Remove(obj);
                db.SaveChanges();
                return RedirectToAction("ARelease", "Relea");

            }
            return View(obj);
        }
        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id==null||id==0)
            {
                return NotFound();
            }

            var obj = db.Releas.Find(id);
            if (obj==null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Relea obj)
        {
            if (ModelState.IsValid)
            {
                db.Releas.Update(obj);
                db.SaveChanges();
                return RedirectToAction("ARelease", "Relea");

            }
            return View(obj);
        }
    }
}
