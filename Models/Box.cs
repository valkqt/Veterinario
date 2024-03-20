using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinario3.Models
{
    public class Box
    {
        [Key]
        public int id { get; set; }
        public string CodiceCassetto { get; set; }

        public string CodiceProdotto { get; set; }

        public Product product { get; set; }

    }
}