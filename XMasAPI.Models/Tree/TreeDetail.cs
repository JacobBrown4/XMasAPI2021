using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMasAPI.Models.Present;

namespace XMasAPI.Models.Tree
{
    public class TreeDetail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool HasStar { get; set; }
        public int PresentCount { get; set; }
        public List<PresentListItem> Presents { get; set; }
    }
}
