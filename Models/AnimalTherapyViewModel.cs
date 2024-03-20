using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinario3.Models
{
    public class AnimalTherapyViewModel
    {
        public Animal Animal { get; set; }
        public List<Therapy> Therapies { get; set; }

    }
}