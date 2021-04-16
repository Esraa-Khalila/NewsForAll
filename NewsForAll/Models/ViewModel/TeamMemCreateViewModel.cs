using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsForAll.Models.ViewModel
{
    public class TeamMemCreateViewModel
    {
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public string TitleJob { get; set; }
    }
}
