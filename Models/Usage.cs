using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinario3.Models
{
    public class Usage
    {
        [Key]
        public int Id { get; set; }
        public string Utilizzo { get; set; }

    }
}