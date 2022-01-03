using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMasAPI.Models.Tree;

namespace XMasAPI.Models.Ornament
{
    public class OrnamentDetail
    {
        public int OrnamentId { get; set; }
        public string Description { get; set; }
        public TreeListItem Tree { get; set; }
    }
}
