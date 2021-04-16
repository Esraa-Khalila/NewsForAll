using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewsForAll.Models
{
    public class TeamMem
    {
        public int Id { get; set; }
      
        public string Image { get; set; }
        public string Name { get; set; }
        public string TitleJob { get; set; }
    }
}

