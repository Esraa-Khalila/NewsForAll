using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsForAll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsForAll.Data
{
    public class ApplicationDbContext :IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base (options)
        {
                
        }
        public string ImageUpload { get; set; }

        public DbSet<Propr> proprs { get; set; }
       
        public DbSet<Relea> Releas { get; set; }
        public DbSet<ContactU> ContactUs { get; set; }
        public DbSet<TeamMem> TeamMems { get; set; }


   
    }
}
