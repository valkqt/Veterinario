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
        [Required(ErrorMessage = "Campo richiesto.")]
        public DateTime DataReg { get; set; } = DateTime.Now;
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Campo richiesto.")]
        [Display(Name = "Tipologia")]
        public string Tipologia { get; set; }
        [Display(Name = "Colore")]
        public string Colore { get; set; }
        [Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        [Display(Name = "Data di Nascita")]
        public DateTime DataNascita { get; set; }
        [StringLength(32, ErrorMessage = "Inserire un codice di massimo 32 caratteri")]
        [Index(IsUnique = true)]
        [Display(Name = "#Microchip")]
        public string IdMicrochip { get; set; }
        [Required(ErrorMessage = "Campo richiesto.")]
        [Display(Name = "Nome")]
        public string NomeProprietario { get; set; } = "Comune di Napoli";
        [Display(Name = "Cognome")]
        public string CognomeProprietario { get; set; }
        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "Campo richiesto.")]
        public string TelefonoProprietario { get; set; } = "0817951111";
        [Display(Name = "Foto Animale")]
        public string Foto { get; set; }
        [NotMapped]
        [Display(Name = "Foto Animale")]
        public string FileFoto { get; set; }
        [Required(ErrorMessage = "Campo richiesto.")]
        [Display(Name = "Presunta?")]
        public bool Presunta { get; set; } = false;
        public virtual ICollection<Admission> Admission { get; set; }
        public virtual ICollection<Therapy> Therapy { get; set; }

    }
}