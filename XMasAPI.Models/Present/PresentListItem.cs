using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMasAPI.Models.Present
{
    public class PresentListItem
    {
        public int PresentId { get; set; }
        public string PresentType { get; set; }
        public string Wrapping { get; set; }
        public string Contains { get; set; }
        public int TreeId { get; set; }
    }
}
