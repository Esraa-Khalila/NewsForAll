using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsForAll.Data;
using NewsForAll.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NewsForAll.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext _db)
        {
            db = _db;
        }

        
        public IActionResult Index()
        {
            var obj =   db.proprs.ToList();
            return View(obj);
        }

        public IActionResult Release( int id)
        {
            var obj = db.Releas.Where(p => p.PropId==id) .OrderByDescending(d=>d.date).ToList();
            return View(obj);
        }
   
        public IActionResult ContactUs()
        {
            return View();
        }
        
        public IActionResult SaveContact(ContactU model)
        {
            if (ModelState.IsValid)
            {
                db.ContactUs.Add(model);
                db.SaveChanges();
                
            }
            return View();
        }
        [Authorize(Roles = "Admin1")]
        public IActionResult AllContact()
        {
            var obj = db.ContactUs.ToList();
            return View(obj);
        }

        public IActionResult TeamMem ()
        {
            var obj = db.TeamMems.ToList();
            return View(obj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult UploadImage(  )
        {
            return View();
        }
    }
}
