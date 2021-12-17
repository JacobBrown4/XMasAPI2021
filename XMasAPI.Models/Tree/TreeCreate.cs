using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMasAPI.Models.Tree
{
    public class TreeCreate
    {
        [Required]
        public string Description { get; set; }
    }
}
