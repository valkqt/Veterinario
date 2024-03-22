using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Veterinario3.Models
{
    public class Box
    {
        [Key]
        public int Id { get; set; }
        public string CodiceCassetto { get; set; }
        public virtual Usage Usage { get; set; }
        public string CodiceProdotto { get; set; }
        public virtual Product Product { get; set; }
        public int ContainerId { get; set; }
        public virtual Container Container { get; set; }
        public int UsageId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}