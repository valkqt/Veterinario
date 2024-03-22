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
        [Required(ErrorMessage = "Il campo Data è obbligatorio.")]
        [Column(TypeName = "datetime2")]
        public DateTime DataCura { get; set; }
        [Required(ErrorMessage = "Il campo Diagnosi è obbligatorio.")]
        public string Diagnosi { get; set; }
        [Required(ErrorMessage = "Il campo Descrizione è obbligatorio.")]
        public string DescrizioneCura { get; set; }
        public Animal Animal { get; set; }

    }
}