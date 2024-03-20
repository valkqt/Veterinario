using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinario3.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        [StringLength(255)]
        public string role { get; set; }

    }
}