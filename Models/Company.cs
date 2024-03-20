using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinario3.Models
{
    public class Company
    {
        [Key]
        public int id { get; set; }
        public string Nome { get; set; }
        public string Indirizzo { get; set; }
        public string Recapito { get; set; }
        public ICollection<Product> Product { get; set; }

    }
}