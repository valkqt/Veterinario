using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinario3.Models
{
    public class Animal
    {
        [Key]
        public int id { get; set; }
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime DataReg { get; set; } = DateTime.Now;
        public string Name { get; set; }
        [Required]
        public string Tipologia { get; set; }
        public string Colore { get; set; }
        [Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]

        public DateTime DataNascita { get; set; }
        [StringLength(32)]
        [Index(IsUnique = true)]
        public string IdMicrochip { get; set; }
        [Required]
        public string NomeProprietario { get; set; } = "Comune di Napoli";
        public string CognomeProprietario { get; set; }
        [Required]
        public string TelefonoProprietario { get; set; } = "0817951111";
        [Required]
        public string Foto { get; set; }
        [Required]
        [Display(Name = "Presunta?")]
        public bool Presunta { get; set; } = false;

        [NotMapped]
        public Admission admission { get; set; } = new Admission();
        public virtual ICollection<Admission> Admission { get; set; }
        public virtual ICollection<Therapy> Therapy { get; set; }

    }
}