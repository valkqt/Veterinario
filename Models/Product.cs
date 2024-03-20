using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinario3.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipologia { get; set; }
        public int Quantità { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

    }
}