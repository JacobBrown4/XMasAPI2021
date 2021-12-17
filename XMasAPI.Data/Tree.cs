using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMasAPI.Data
{
    public class Tree
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool HasStar { get; set; }
        public virtual List<Present> Presents { get; set; }
    }
}
