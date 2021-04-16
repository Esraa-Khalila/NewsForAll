using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsForAll.Data;
using NewsForAll.Models;
using NewsForAll.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

    
namespace NewsForAll.Controllers
{
    [Authorize(Roles = "Admin1")]
    public class ReleaController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;

            public ReleaController(ApplicationDbContext _db , IWebHostEnvironment _webHostEnvironment)
        {
            db = _db;
            webHostEnvironment = _webHostEnvironment;
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
       
        public async Task<IActionResult> Create(ReleaCreateViewModel model)
        {
      

            if (ModelState.IsValid)
            {
               
                
                    string fileName = null;
                if (model.Image != null)
                {


                    string UploadFplder = Path.Combine(webHostEnvironment.WebRootPath, "Relea");
                    fileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filepath = Path.Combine(UploadFplder, fileName);
                    model.Image.CopyTo(new FileStream(filepath, FileMode.Create));
                }

                Relea newRelea = new Relea
                {
                    Title = model.Title,
                    Topic = model.Topic,
                    date = model.date,
                    Prop = model.Prop,
                    PropId = model.PropId,
                    Image = fileName

                };
               

                db.Add(newRelea);
               await db.SaveChangesAsync();
                return RedirectToAction("ARelease", "Relea");
                } 
                 
                return View();
           
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            ViewData["ProprId"] = new SelectList(db.Set<Propr>(), "Id", "Name");
            if (id == null || id == 0)
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
        public IActionResult Delete(Relea obj)
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
            ViewData["ProprId"] = new SelectList(db.Set<Propr>(), "Id", "Name");

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
            ViewData["ProprId"] = new SelectList(db.Set<Propr>(), "Id", "Name");
            return View(obj);
        }
    }
}
