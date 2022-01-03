using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMasAPI.Models.Tree
{
    public class TreeEdit
    {
        [Required]
        public int TreeId { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public bool HasStar { get; set; }
    }
}
