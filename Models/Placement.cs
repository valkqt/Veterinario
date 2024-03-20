using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinario3.Models
{
    public class Placement
    {
        [Key]
        public int id { get; set; }
        public Product Product { get; set; }
        public Container Container { get; set; }
        public Box Box { get; set; }

    }
}