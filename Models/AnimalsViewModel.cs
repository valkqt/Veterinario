using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinario3.Models
{
    public class AnimalsViewModel
    {
        public Admission admission { get; set; }
        public Therapy therapy { get; set; }
        public Animal animal { get; set; }

    }
}