using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Veterinario3.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        [StringLength(16)]
        public string CodiceFiscale { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DataVendita { get; set; } = DateTime.Now;
        public string RicettaMedica { get; set; }
        public int ProductId{ get; set; }
        public virtual Product Products { get; set; }

        public int Quantità { get; set; }

    }
}