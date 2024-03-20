using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinario3.Models
{
    public class ProductsUsage
    {
        [Key]
        public int id { get; set; }
        public Product product { get; set; }
        public Usage usage { get; set; }

    }
}