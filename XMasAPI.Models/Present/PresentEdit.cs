using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMasAPI.Models.Present
{
    public class PresentEdit
    {
        public int PresentId { get; set; }
        [Required]
        public PresentType PresentType { get; set; }
        [Required]
        public string Wrapping { get; set; }
        [Required]
        public string Contains { get; set; }
        [Required]
        public string Hint1 { get; set; }
        [Required]
        public string Hint2 { get; set; }
        [Required]
        public string Hint3 { get; set; }
        [Required]
        public int TreeId { get; set; }
    }
}
