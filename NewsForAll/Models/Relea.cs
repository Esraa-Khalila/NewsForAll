
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewsForAll.Models
{
    public class Relea
    {
        
            public int Id { get; set; }
        [Required]
            public string Title { get; set; }
        [Required]
            public string Topic { get; set; }
       
    
             public string Image { get; set; }
        [Required]
            public DateTime date { get; set; }
        [ForeignKey ("Propr")]
            public int PropId { get; set; }
            public Propr Prop { get; set; }

        
    }
}
