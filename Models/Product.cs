using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("Company")]
        public int SelectedCompanyId { get; set; }
        public Company Company { get; set; }

        public int UsageId { get; set; }
        public virtual Usage Usage { get; set; }
    }
}