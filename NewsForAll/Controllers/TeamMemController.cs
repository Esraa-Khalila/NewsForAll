using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NewsForAll.Data;
using NewsForAll.Models;
using NewsForAll.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewsForAll.Controllers
{
    [Authorize(Roles = "Admin1")]
    public class TeamMemController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;
        public TeamMemController(ApplicationDbContext _db , IWebHostEnvironment _webHostEnvironment)
        {
            db = _db;
            webHostEnvironment = _webHostEnvironment;
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
        public async Task<IActionResult> Create(TeamMemCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (model.Image != null)
                {


                    string UploadFplder = Path.Combine(webHostEnvironment.WebRootPath, "TeamImg");
                    fileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filepath = Path.Combine(UploadFplder, fileName);
                    model.Image.CopyTo(new FileStream(filepath, FileMode.Create));
                }

                TeamMem teamMem = new TeamMem
                {
                    TitleJob = model.TitleJob,
                    Name = model.Name,
                    Image = fileName

                };


                db.Add(teamMem);
                await db.SaveChangesAsync();
                return RedirectToAction("ATeamMem", "TeamMem");
            } 
                 
            return View();
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
