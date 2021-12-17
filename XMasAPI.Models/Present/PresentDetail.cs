using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMasAPI.Models.Tree;

namespace XMasAPI.Models.Present
{
    public class PresentDetail
    {
        public int PresentId { get; set; }
        public string PresentType { get; set; }
        public string Wrapping { get; set; }
        public string Contains { get; set; }
        public int TimesShaken { get; set; }
        public bool IsWrapped { get; set; }
        public TreeListItem Tree { get; set; }
    }
}
