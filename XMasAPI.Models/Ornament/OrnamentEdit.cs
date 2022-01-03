using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMasAPI.Models.Ornament
{
    public class OrnamentEdit
    {
        [Key]
        public int OrnamentId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int TreeId { get; set; }
    }
}
