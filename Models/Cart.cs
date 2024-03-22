using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinario3.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [StringLength(16)]
        public string CodiceFiscale { get; set; }
        public string RicettaMedica { get; set; }
        public int ProductId { get; set; }
        public virtual Product Products { get; set; }
        public int Quantità { get; set; }
    }
}