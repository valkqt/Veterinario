using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Veterinario3.Models
{
    public class Admission
    {
        [Key]
        public int id { get; set; }
        [Column(TypeName = "datetime2")]
        [Display(Name = "Data Inizio Ricovero*")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DataInizio { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime2")]
        [Display(Name = "Data Fine Ricovero")]
        [DataType(DataType.Date)]
        public DateTime? DataFine { get; set; } = default;
        [ForeignKey("Animal")]
        public int animalID { get; set; }
        public virtual Animal Animal { get; set; }

    }
}