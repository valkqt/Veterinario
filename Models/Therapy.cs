using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinario3.Models
{
    public class Therapy
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DataCura { get; set; }
        [Required]
        public string Diagnosi { get; set; }
        [Required]
        public string DescrizioneCura { get; set; }
        public Animal Animal { get; set; }

    }
}